using Framework.Contants;
using Framework.Contants.Game;
using System;
using System.Collections.Generic;
using World_Server.Handlers;
using World_Server.Handlers.Communication;
using World_Server.Sessions;
using static World_Server.Program;

namespace World_Server.Managers
{
    public delegate void ProcessChatCallback(WorldSession Session, PCMessageChat message);

    public delegate void ChatCommandDelegate(WorldSession Session, String[] args);

    public class ChatManager
    {
        public static Dictionary<ChatMessage, ProcessChatCallback> ChatHandlers;

        public static void Boot()
        {
            WorldDataRouter.AddHandler<PCMessageChat>(WorldOpcodes.CMSG_MESSAGECHAT, OnMsgMessageChat);
            //WorldDataRouter.AddHandler<PCChannel>(WorldOpcodes.CMSG_JOIN_CHANNEL, OnJoinChannel);
            //WorldDataRouter.AddHandler<PCChannel>(WorldOpcodes.CMSG_LEAVE_CHANNEL, OnLeaveChannel);
            //WorldDataRouter.AddHandler<PCChannel>(WorldOpcodes.CMSG_CHANNEL_LIST, OnListChannel);

            ChatHandlers = new Dictionary<ChatMessage, ProcessChatCallback>();
            ChatHandlers.Add(ChatMessage.CHAT_MSG_SAY, OnMsg);
            ChatHandlers.Add(ChatMessage.CHAT_MSG_YELL, OnMsg);
            ChatHandlers.Add(ChatMessage.CHAT_MSG_EMOTE, OnMsg);
            ChatHandlers.Add(ChatMessage.CHAT_MSG_WHISPER, OnMsgWhisper);
            //ChatHandlers.Add(ChatMessage.CHAT_MSG_CHANNEL, OnChannelMessage);
        }

        public static void OnMsg(WorldSession session, PCMessageChat packet)
        {
            WorldServer.TransmitToAll(new PSMessageChat(packet.Type, ChatLanguage.LANG_COMMON, (ulong)session.Character.Id, packet.Message));
        }

        public static void OnMsgWhisper(WorldSession session, PCMessageChat packet)
        {
            //WorldSession remoteSession = WorldServer.GetSessionByPlayerName(packet.To);

            Console.WriteLine("[Chat] Whisper:" + " To:" + packet.To + " From:" + session.Character.Name + " Message:" + packet.Message);
            /*
            if (remoteSession != null)
            {
                session.sendPacket(new PSMessageChat(ChatMessage.CHAT_MSG_WHISPER_INFORM, ChatLanguage.LANG_UNIVERSAL, (ulong)remoteSession.Character.Id, packet.Message));
                remoteSession.sendPacket(new PSMessageChat(ChatMessage.CHAT_MSG_WHISPER, ChatLanguage.LANG_UNIVERSAL, (ulong)session.Character.Id, packet.Message));
            }
            else
            {
                session.sendPacket(new PSMessageChat(ChatMessage.CHAT_MSG_SYSTEM, ChatLanguage.LANG_COMMON, 0, "Player not found."));
            }
            */
        }

        public static void OnMsgMessageChat(WorldSession session, PCMessageChat packet)
        {
            Console.WriteLine("[Chat] Type:" + packet.Type.ToString() + " Language:" + packet.Language.ToString() + " Message:" + packet.Message);

            if (ChatHandlers.ContainsKey(packet.Type))
            {
                ChatHandlers[packet.Type](session, packet);
            }
        }
    }
}
