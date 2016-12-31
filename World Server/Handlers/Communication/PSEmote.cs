using Framework.Contants;
using Framework.Network;

namespace World_Server.Handlers.Communication
{
    public class PSEmote : ServerPacket
    {
        public PSEmote(int emoteID, int GUID) : base(WorldOpcodes.SMSG_EMOTE)
        {
            Write((uint)emoteID);
            Write((ulong)GUID);
        }
    }
}