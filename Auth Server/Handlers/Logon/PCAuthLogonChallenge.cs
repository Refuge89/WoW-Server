﻿using Framework.Network;
using System;
using System.Net;

namespace Auth_Server.Handlers.Logon
{
    public class PCAuthLogonChallenge : PacketReader
    {
        public byte OptCode;
        public byte Error;
        public UInt16 Size;

        public string GameName;
        public string Version;
        public UInt16 Build;

        public string Platform;
        public string OS;
        public string Country;

        public UInt32 TimeZone;
        public IPAddress IP;
        public string Name;

        public PCAuthLogonChallenge(byte[] data) : base(data)
        {
            OptCode     = ReadByte();
            Error       = ReadByte();
            Size        = ReadUInt16();

            GameName    = ReadStringReversed(4);
            Version     = ReadByte().ToString() + '.' + ReadByte().ToString() + '.' + ReadByte().ToString();

            Build       = ReadUInt16();
            Platform    = ReadStringReversed(4);
            OS          = ReadStringReversed(4);
            Country     = ReadStringReversed(4);

            TimeZone    = ReadUInt32();
            IP          = ReadIpAddress();
            Name        = ReadPascalString(1);
        }
    }
}