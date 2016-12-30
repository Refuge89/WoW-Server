using Framework.Contants.Game;
using Framework.Network;

namespace World_Server.Handlers.Communication
{
    public class PCMessageChat : PacketReader
    {
        public ChatMessage Type { get; private set; }
        public ChatLanguage Language { get; private set; }
        public string To { get; private set; }
        public string ChannelName { get; private set; }
        public string Message { get; private set; }

        public PCMessageChat(byte[] data) : base(data)
        {
            Type = (ChatMessage)ReadUInt32();
            Language = (ChatLanguage)ReadUInt32();

            if (Type == ChatMessage.CHAT_MSG_CHANNEL) ChannelName = ReadCString();

            if (Type == ChatMessage.CHAT_MSG_WHISPER) To = ReadCString();
            Message = ReadCString();
        }
    }
}
