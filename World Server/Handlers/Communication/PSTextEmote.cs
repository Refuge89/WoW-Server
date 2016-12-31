using System.Text;
using Framework.Contants;
using Framework.Network;

namespace World_Server.Handlers.Communication
{
    public class PSTextEmote : ServerPacket
    {
        public PSTextEmote(int GUID, int emoteID, int textID, string targetName = null) : base(WorldOpcodes.SMSG_TEXT_EMOTE)
        {
            Write((ulong)GUID);
            Write((uint)textID);
            Write((uint)emoteID);

            if (targetName != null)
            {
                Write((uint)targetName.Length);
                Write(Encoding.UTF8.GetBytes(targetName));
            }
            else
            {
                Write((uint)1);
                Write((byte)0);
            }
        }
    }
}