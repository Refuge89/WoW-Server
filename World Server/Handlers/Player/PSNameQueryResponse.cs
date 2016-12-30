using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;
using System.Text;

namespace World_Server.Handlers.Player
{
    public class PSNameQueryResponse : ServerPacket
    {
        public PSNameQueryResponse(Character character) : base(WorldOpcodes.SMSG_NAME_QUERY_RESPONSE)
        {
            Write((ulong)character.Id);
            Write(Encoding.UTF8.GetBytes(character.Name + '\0'));
            Write((byte)0); // realm name for cross realm BG usage
            Write((uint)character.Race);
            Write((uint)character.Gender);
            Write((uint)character.Class);
        }
    }
}
