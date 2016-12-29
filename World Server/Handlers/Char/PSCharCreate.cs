using Framework.Contants;
using Framework.Network;

namespace World_Server.Handlers.Char
{
    class PSCharCreate : ServerPacket
    {
        public PSCharCreate(LoginErrorCode code) : base(WorldOpcodes.SMSG_CHAR_CREATE)
        {
            Write((byte)code);
        }
    }
}
