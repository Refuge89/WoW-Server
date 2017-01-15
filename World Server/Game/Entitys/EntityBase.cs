using Framework.Contants.Game;
using Framework.Extensions;
using System;
using System.Collections;
using System.IO;

namespace World_Server.Game.Entitys
{
    public class EntityBase
    {
        public int MaskSize { get; }
        public BitArray Mask { get; }
        public Hashtable UpdateData { get; }
        public int UpdateCount { get; private set; }

        public virtual int DataLength { get; set; }
        public virtual TypeID TypeId { get; internal set; }
        public virtual string Name { get; set; }

        public EntityBase()
        {
            MaskSize = ((DataLength) + 32) / 32;
            Mask = new BitArray(DataLength, false);
            UpdateData = new Hashtable();
        }

        public void SetUpdateField<T>(int index, T value, byte offset = 0)
        {
            UpdateCount++;
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

        public void WriteUpdateFields(BinaryWriter packet)
        {
            packet.Write((byte)MaskSize);
            packet.WriteBitArray(Mask, (MaskSize * 4));

            for (int i = 0; i < Mask.Count; i++)
            {
                if (!Mask.Get(i)) continue;
                try
                {
                    switch (UpdateData[i].GetType().Name)
                    {
                        case "UInt32":
                            packet.Write((uint)UpdateData[i]);
                            break;
                        case "Single":
                            packet.Write((float)UpdateData[i]);
                            break;
                        default:
                            packet.Write((int)UpdateData[i]);
                            break;
                    }
                }
                catch
                {
                    // ignored
                }
            }

            Mask.SetAll(false);
            UpdateCount = 0;
        }

    }
}
