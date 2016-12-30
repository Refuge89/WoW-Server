using Framework.Contants;
using Framework.Network;

namespace World_Server.Handlers.Char
{
    class PSLogoutComplete : ServerPacket
    {
        public PSLogoutComplete() : base(WorldOpcodes.SMSG_LOGOUT_COMPLETE)
        {
            Write((byte)0);
        }
    }
}
