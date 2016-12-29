using Framework.Network;
using System;

namespace World_Server.Handlers
{
    class PCAuthSession : PacketReader
    {
        public Int32 ClientBuild { get; private set; }
        public Int32 Unk2 { get; private set; }
        public string AccountName { get; private set; }

        public PCAuthSession(byte[] data) : base(data)
        {
            ClientBuild = ReadInt32();
            Unk2        = ReadInt32();
            AccountName = ReadCString();
        }
    }
}
