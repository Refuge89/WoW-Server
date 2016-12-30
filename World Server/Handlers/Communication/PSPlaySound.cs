using Framework.Contants;
using Framework.Network;

namespace World_Server.Handlers.Communication
{
    public class PSPlaySound : ServerPacket
    {
        public PSPlaySound(uint soundID) : base(WorldOpcodes.SMSG_PLAY_SOUND)
        {
            Write(soundID);
        }
    }
}
