using Framework.Contants;
using Framework.Contants.Game;
using Framework.Network;
using System.Text;

namespace World_Server.Handlers.Communication
{
    public class PSMessageChat : ServerPacket
    {
        public PSMessageChat(ChatMessage type, ChatLanguage language, ulong GUID, string message, string channelName = null) : base(WorldOpcodes.SMSG_MESSAGECHAT)
        {
            Write((byte)type);
            Write((uint)0); // language);

            if (type == ChatMessage.CHAT_MSG_CHANNEL)
            {
                Write(Encoding.UTF8.GetBytes(channelName + '\0'));
                Write((uint)0);
            }

            Write((ulong)GUID);

            if (type == ChatMessage.CHAT_MSG_SAY || type == ChatMessage.CHAT_MSG_YELL || type == ChatMessage.CHAT_MSG_PARTY)
            {
                Write((ulong)GUID);
            }

            Write((uint)message.Length + 1);
            Write(Encoding.UTF8.GetBytes(message + '\0'));
            Write((byte)0);

        }
    }
}
