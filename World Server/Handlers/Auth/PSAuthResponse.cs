using Framework.Contants;
using Framework.Network;

namespace World_Server.Handlers
{
    class PSAuthResponse : ServerPacket
    {
        public PSAuthResponse() : base(WorldOpcodes.SMSG_AUTH_RESPONSE)
        {
            Write((byte)ResponseCodes.AUTH_OK);
        }
    }
}
