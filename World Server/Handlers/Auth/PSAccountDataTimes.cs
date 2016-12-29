using Framework.Contants;
using Framework.Extensions;
using Framework.Network;

namespace World_Server.Handlers.Auth
{
    class PSAccountDataTimes : ServerPacket
    {
        public PSAccountDataTimes() : base(WorldOpcodes.SMSG_ACCOUNT_DATA_TIMES)
        {
            this.WriteNullByte(128);
        }
    }
}
