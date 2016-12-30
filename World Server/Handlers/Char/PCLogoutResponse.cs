using Framework.Contants;
using Framework.Network;
using System;

namespace World_Server.Handlers.Char
{
    public class PCLogoutResponse : ServerPacket
    {
        public PCLogoutResponse(): base(WorldOpcodes.SMSG_LOGOUT_RESPONSE)
        {
            Write((UInt32)0);
            Write((byte)0);
        }
    }
}
