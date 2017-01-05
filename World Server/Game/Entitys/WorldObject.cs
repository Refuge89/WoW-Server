﻿using System;
using System.Collections;
using System.IO;
using System.Linq;
using World_Server.Game.Update;

namespace World_Server.Game.Entitys
{
    public class WorldObject
    {
        //public UInt64 GUID;
        public ObjectGuid GUID;

        public int MaskSize;
        public BitArray Mask;
        public Hashtable UpdateData;

        public WorldObject(int dataLength)
        {
            MaskSize = ((dataLength) + 32) / 32;
            Mask = new BitArray(dataLength, false);
            UpdateData = new Hashtable();
        }

        public void SetUpdateField<T>(int index, T value, byte offset = 0)
        {
            switch (value.GetType().Name)
            {
                case "SByte":
                case "Int16":
                {
                    Mask.Set(index, true);

                    if (UpdateData.ContainsKey(index))
                        UpdateData[index] = (int) UpdateData[index] |
                                            (int) Convert.ChangeType(value, typeof(int)) <<
                                            (offset * (value.GetType().Name == "Byte" ? 8 : 16));
                    else
                        UpdateData[index] = (int) Convert.ChangeType(value, typeof(int)) <<
                                            (offset * (value.GetType().Name == "Byte" ? 8 : 16));

                    break;
                }
                case "Byte":
                case "UInt16":
                {
                    Mask.Set(index, true);

                    if (UpdateData.ContainsKey(index))
                        UpdateData[index] = (uint) UpdateData[index] |
                                            (uint) Convert.ChangeType(value, typeof(uint)) <<
                                            (offset * (value.GetType().Name == "Byte" ? 8 : 16));
                    else
                        UpdateData[index] = (uint) Convert.ChangeType(value, typeof(uint)) <<
                                            (offset * (value.GetType().Name == "Byte" ? 8 : 16));

                    break;
                }
                case "Int64":
                {
                    Mask.Set(index, true);
                    Mask.Set(index + 1, true);

                    long tmpValue = (long) Convert.ChangeType(value, typeof(long));

                    UpdateData[index] = (uint) (tmpValue & int.MaxValue);
                    UpdateData[index + 1] = (uint) ((tmpValue >> 32) & int.MaxValue);

                    break;
                }
                case "UInt64":
                {
                    Mask.Set(index, true);
                    Mask.Set(index + 1, true);

                    ulong tmpValue = (ulong) Convert.ChangeType(value, typeof(ulong));

                    UpdateData[index] = (uint) (tmpValue & uint.MaxValue);
                    UpdateData[index + 1] = (uint) ((tmpValue >> 32) & uint.MaxValue);

                    break;
                }
                default:
                {
                    Mask.Set(index, true);
                    UpdateData[index] = value;

                    break;
                }
            }
        }

        public void WriteBytes(BinaryWriter writer, byte[] data, int count = 0)
        {
            if (count == 0)
                writer.Write(data);
            else
                writer.Write(data, 0, count);
        }

        public void WriteBitArray(BinaryWriter writer, BitArray buffer, int Len)
        {
            byte[] bufferarray = new byte[Convert.ToByte((buffer.Length + 8) / 8) + 1];
            buffer.CopyTo(bufferarray, 0);

            WriteBytes(writer, bufferarray.ToArray(), Len);
        }

        public void WriteUpdateFields(BinaryWriter packet)
        {
            packet.Write((byte) MaskSize);
            WriteBitArray(packet, Mask, (MaskSize * 4)); // Int32 = 4 Bytes

            for (int i = 0; i < Mask.Count; i++)
            {
                if (Mask.Get(i))
                {

                    switch (UpdateData[i].GetType().Name)
                    {
                        case "UInt32":
                            packet.Write((uint) UpdateData[i]);
                            break;
                        case "Single":
                            packet.Write((float) UpdateData[i]);
                            break;
                        default:
                            packet.Write((int) UpdateData[i]);
                            break;
                    }
                }
            }

            Mask.SetAll(false);
        }
    }
}