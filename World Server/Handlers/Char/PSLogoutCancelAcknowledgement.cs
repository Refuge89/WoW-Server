using Framework.Contants;
using Framework.Network;

namespace World_Server.Handlers.Char
{
    class PSLogoutCancelAcknowledgement : ServerPacket
    {
        public PSLogoutCancelAcknowledgement() : base(WorldOpcodes.SMSG_LOGOUT_CANCEL_ACK)
        {
            Write((byte)0);
        }
    }
}
