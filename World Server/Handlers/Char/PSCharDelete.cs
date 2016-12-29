using Framework.Contants;
using Framework.Network;

namespace World_Server.Handlers.Char
{
    class PSCharDelete : ServerPacket
    {
        public PSCharDelete(LoginErrorCode code) : base(WorldOpcodes.SMSG_CHAR_DELETE)
        {
            Write((byte)code);
        }
    }
}
