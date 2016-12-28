﻿using Framework.Network;

namespace World_Server.Handlers.World
{
    public class PCPing : PacketReader
    {
        public uint Ping { get; private set; }
        public uint Latency { get; private set; }

        public PCPing(byte[] data) : base(data)
        {
            Ping    = ReadUInt32();
            Latency = ReadUInt32();
        }
    }
}
