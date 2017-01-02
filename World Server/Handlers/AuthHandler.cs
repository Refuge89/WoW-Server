using Framework.Contants;
using Framework.Network;

namespace World_Server.Handlers
{
    sealed class CmsgAuthSession : PacketReader
    {
        public int ClientBuild { get; private set; }
        public int Unk2 { get; private set; }
        public string AccountName { get; private set; }

        public CmsgAuthSession(byte[] data) : base(data)
        {
            ClientBuild = ReadInt32();
            Unk2 = ReadInt32();
            AccountName = ReadCString();
        }
    }

    sealed class SmsgAuthResponse : ServerPacket
    {
        public SmsgAuthResponse() : base(WorldOpcodes.SMSG_AUTH_RESPONSE)
        {
            Write((byte)ResponseCodes.AUTH_OK);
        }
    }
}
