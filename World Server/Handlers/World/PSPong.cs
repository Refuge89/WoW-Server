using Framework.Contants;
using Framework.Network;

namespace World_Server.Handlers.World
{
    public class PSPong : ServerPacket
    {
        public PSPong(uint ping) : base(WorldOpcodes.SMSG_PONG)
        {
            Write((ulong)ping);
        }
    }
}
