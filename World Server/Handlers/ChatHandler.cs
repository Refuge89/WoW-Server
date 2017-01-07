using System;
using System.Text;
using Framework.Contants;
using Framework.Contants.Game;
using Framework.Network;
using World_Server.Sessions;
using System.Globalization;
using Framework.Database.Tables;
using World_Server.Game.Entitys;

namespace World_Server.Handlers
{

    #region CMSG_JOIN_CHANNEL
    class CmsgJoinChannel : PacketReader
    {
        public string ChannelName { get; private set; }
        public string Password { get; private set; }

        public CmsgJoinChannel(byte[] data) : base(data)
        {
            ChannelName = ReadCString();
            Password = ReadCString();
        }
    }
    #endregion

    #region SMSG_CHANNEL_NOTIFY
    sealed class SmsgChannelNotify : ServerPacket
    {
        public SmsgChannelNotify(ChatChannelNotify type, ulong id, string channelName) : base(WorldOpcodes.SMSG_CHANNEL_NOTIFY)
        {
            Write((byte)type);
            Write(Encoding.UTF8.GetBytes(channelName + '\0'));
            Write(id);
        }
    }
    #endregion

    #region CMSG_MESSAGECHAT
    public sealed class CmsgMessagechat : PacketReader
    {
        public ChatMessageType Type { get; private set; }
        public ChatMessageLanguage Language { get; private set; }
        public string To { get; private set; }
        public string ChannelName { get; private set; }
        public string Message { get; private set; }

        public CmsgMessagechat(byte[] data) : base(data)
        {
            Type = (ChatMessageType)ReadUInt32();
            Language = (ChatMessageLanguage)ReadUInt32();

            if (Type == ChatMessageType.CHAT_MSG_CHANNEL) ChannelName = ReadCString();

            if (Type == ChatMessageType.CHAT_MSG_WHISPER) To = ReadCString();
            Message = ReadCString();
        }
    }
    #endregion

    #region SMSG_MESSAGECHAT
    internal sealed class SmsgMessagechat : ServerPacket
    {
        public SmsgMessagechat(ChatMessageType type, ChatMessageLanguage language, ulong id, string message, string channelName = null) : base(WorldOpcodes.SMSG_MESSAGECHAT)
        {
            Write((byte)type);
            Write((uint)language);

            if (type == ChatMessageType.CHAT_MSG_CHANNEL)
            {
                Write(Encoding.UTF8.GetBytes(channelName + '\0'));
                Write((uint)0);
            }

            Write((ulong)id);

            if (type == ChatMessageType.CHAT_MSG_SAY || type == ChatMessageType.CHAT_MSG_YELL || type == ChatMessageType.CHAT_MSG_PARTY)
                Write((ulong)id);

            Write((uint)message.Length + 1);
            Write(Encoding.UTF8.GetBytes(message + '\0'));
            Write((byte)0);
        }
    }
    #endregion

    #region CMSG_TEXT_EMOTE
    public sealed class CmsgTextEmote : PacketReader
    {
        public uint TextId { get; private set; }
        public uint EmoteId { get; private set; }

        public CmsgTextEmote(byte[] data) : base(data)
        {
            TextId = ReadUInt32();
            EmoteId = ReadUInt32();
        }
    }
    #endregion

    #region SMSG_TEXT_EMOTE
    public sealed class SmsgTextEmote : ServerPacket
    {
        public SmsgTextEmote(int guid, int emoteId, int textId) : base(WorldOpcodes.SMSG_TEXT_EMOTE)
        {
            Write((ulong)guid);
            Write((uint)textId);
            Write((uint)emoteId);
            Write((uint)1);
            Write((byte)0);
        }
    }
    #endregion

    public delegate void ProcessChatCallback(WorldSession session, CmsgMessagechat message);

    class ChatHandler
    {
        public static void SendSytemMessage(WorldSession session, string message)
        {
            session.SendPacket(new SmsgMessagechat(ChatMessageType.CHAT_MSG_SYSTEM, ChatMessageLanguage.LANG_COMMON, 0, message));
        }

        internal static void OnJoinChannel(WorldSession session, CmsgJoinChannel handler)
        {
            // Precisa inserir na base que entrou no canal
            session.SendPacket(new SmsgChannelNotify(ChatChannelNotify.CHAT_YOU_JOINED_NOTICE, (ulong)session.Character.Id, handler.ChannelName));
        }

        internal static void OnLeaveChannel(WorldSession session, CmsgJoinChannel handler)
        {
            // remove da base que saiu do canal
            session.SendPacket(new SmsgChannelNotify(ChatChannelNotify.CHAT_YOU_LEFT_NOTICE, (ulong)session.Character.Id, handler.ChannelName));
        }

        internal static void OnTextEmote(WorldSession session, CmsgTextEmote handler)
        {
            // Implementar alguns emotes na unha ?!?!?!?!?!?
            Program.WorldServer.TransmitToAll(new SmsgTextEmote(session.Character.Id, (int)handler.EmoteId, (int)handler.TextId));
        }

        internal static void OnMessageChat(WorldSession session, CmsgMessagechat handler)
        {
            Console.WriteLine(handler.Type);
            switch (handler.Type)
            {
                case ChatMessageType.CHAT_MSG_SAY:
                    string[] splitMessage = handler.Message.Split(' ');

                    UnitEntity entity = session.Entity.Target ?? session.Entity;

                    if (splitMessage.Length == 2)
                    {
                        if (splitMessage[0].ToLower() == "spell" && splitMessage[1] != "")
                            session.SendPacket(new SmsgLearnedSpell((uint) int.Parse(splitMessage[1])));

                        if (splitMessage[0].ToLower() == "sound")
                            session.SendPacket(new SmsgPlaySound((uint) int.Parse(splitMessage[1])));

                        

                        if (splitMessage[0].ToLower() == "scale")
                            entity.Scale = float.Parse(splitMessage[1]);

                        if (splitMessage[0].ToLower() == "level")
                            entity.Level = int.Parse(splitMessage[1]);

                        if (splitMessage[0].ToLower() == "xp")
                        {
                            ((PlayerEntity) entity).Xp = int.Parse(splitMessage[1]);
                        }

                        session.SendMessage($"Applied {splitMessage[0].ToLower()} = {splitMessage[1]}");
                    }

                    if (splitMessage[0].ToLower() == "vem")
                    {
                        Console.WriteLine("vem comando");
                        try
                        {
                            Console.WriteLine(splitMessage[2]);
                            entity.SetUpdateField((int)(EUnitFields)Enum.Parse(typeof(EUnitFields), splitMessage[1]), int.Parse(splitMessage[2]));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }

                    Program.WorldServer.TransmitToAll(new SmsgMessagechat(handler.Type, ChatMessageLanguage.LANG_UNIVERSAL, (ulong)session.Character.Id, handler.Message));
                    break;
                case ChatMessageType.CHAT_MSG_EMOTE:
                    Program.WorldServer.TransmitToAll(new SmsgMessagechat(handler.Type, ChatMessageLanguage.LANG_UNIVERSAL, (ulong)session.Character.Id, handler.Message));
                    break;
                case ChatMessageType.CHAT_MSG_YELL:
                    Program.WorldServer.TransmitToAll(new SmsgMessagechat(handler.Type, ChatMessageLanguage.LANG_UNIVERSAL, (ulong)session.Character.Id, handler.Message));
                    break;
                case ChatMessageType.CHAT_MSG_CHANNEL:
                    Program.WorldServer.TransmitToAll(new SmsgMessagechat(handler.Type, ChatMessageLanguage.LANG_UNIVERSAL, (ulong)session.Character.Id, handler.Message, handler.ChannelName));
                    break;
                case ChatMessageType.CHAT_MSG_WHISPER:
                case ChatMessageType.CHAT_MSG_PARTY:
                case ChatMessageType.CHAT_MSG_OFFICER:
                case ChatMessageType.CHAT_MSG_DND:
                case ChatMessageType.CHAT_MSG_AFK:
                default:
                    break;
            }
        }
    }
}
