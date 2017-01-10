﻿using System.IO;
using System.Text;
using System;
using System.Net;

namespace Framework.Network
{
    public class PacketReader : BinaryReader
    {
        public PacketReader(Stream input) : base(input, Encoding.UTF8)
        {
        }

        public PacketReader(byte[] data) : base(new MemoryStream(data), Encoding.UTF8)
        {
        }

        public string ReadStringReversed(int length)
        {
            byte[] str = ReadBytes(length);
            Array.Reverse(str);
            return Encoding.UTF8.GetString(str);
        }

        public IPAddress ReadIpAddress()
        {
            return new IPAddress(ReadBytes(4));
        }

        public string ReadPascalString(byte numBytesForLength)
        {
            uint readCount;
            switch (numBytesForLength)
            {
                case 1:
                    readCount = ReadByte();
                    break;
                case 2:
                    readCount = ReadUInt16();
                    break;
                case 4:
                    readCount = ReadUInt32();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(numBytesForLength));
            }
            string ret = "";
            for (int i = 0; i < readCount; i++)
            {
                ret += ReadChar();
            }
            return ret;
        }

        public string ReadCString()
        {
            string ret = string.Empty;

            char c = ReadChar();
            while (c != '\0')
            {
                ret += c;
                c = ReadChar();
            }
            return ret;
        }

        public new ulong ReadUInt64()
        {
            return base.ReadUInt64();
        }
    }
}
