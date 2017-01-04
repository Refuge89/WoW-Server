using System;
using System.Text;
using Framework.Network;
using World_Server.Sessions;
using Framework.Contants;

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
            Write((ulong)id);
        }
    }
    #endregion

    class ChatHandler
    {
        internal static void OnJoinChannel(WorldSession session, CmsgJoinChannel handler)
        {
            // Precisa inserir na base que entrou no canal
            session.sendPacket(new SmsgChannelNotify(ChatChannelNotify.CHAT_YOU_JOINED_NOTICE, (ulong)session.Character.Id, handler.ChannelName));
        }
    }

    public enum ChatChannelNotify
    {
        CHAT_JOINED_NOTICE = 0x00, //+ "%s joined channel.";
        CHAT_LEFT_NOTICE = 0x01, //+ "%s left channel.";
        CHAT_YOU_JOINED_NOTICE = 0x02, //+ "Joined Channel: [%s]"; -- You joined
        CHAT_YOU_LEFT_NOTICE = 0x03, //+ "Left Channel: [%s]"; -- You left
        CHAT_WRONG_PASSWORD_NOTICE = 0x04, //+ "Wrong password for %s.";
        CHAT_NOT_MEMBER_NOTICE = 0x05, //+ "Not on channel %s.";
        CHAT_NOT_MODERATOR_NOTICE = 0x06, //+ "Not a moderator of %s.";
        CHAT_PASSWORD_CHANGED_NOTICE = 0x07, //+ "[%s] Password changed by %s.";
        CHAT_OWNER_CHANGED_NOTICE = 0x08, //+ "[%s] Owner changed to %s.";
        CHAT_PLAYER_NOT_FOUND_NOTICE = 0x09, //+ "[%s] Player %s was not found.";
        CHAT_NOT_OWNER_NOTICE = 0x0A, //+ "[%s] You are not the channel owner.";
        CHAT_CHANNEL_OWNER_NOTICE = 0x0B, //+ "[%s] Channel owner is %s.";
        CHAT_MODE_CHANGE_NOTICE = 0x0C, //?
        CHAT_ANNOUNCEMENTS_ON_NOTICE = 0x0D, //+ "[%s] Channel announcements enabled by %s.";
        CHAT_ANNOUNCEMENTS_OFF_NOTICE = 0x0E, //+ "[%s] Channel announcements disabled by %s.";
        CHAT_MODERATION_ON_NOTICE = 0x0F, //+ "[%s] Channel moderation enabled by %s.";
        CHAT_MODERATION_OFF_NOTICE = 0x10, //+ "[%s] Channel moderation disabled by %s.";
        CHAT_MUTED_NOTICE = 0x11, //+ "[%s] You do not have permission to speak.";
        CHAT_PLAYER_KICKED_NOTICE = 0x12, //? "[%s] Player %s kicked by %s.";
        CHAT_BANNED_NOTICE = 0x13, //+ "[%s] You are banned from that channel.";
        CHAT_PLAYER_BANNED_NOTICE = 0x14, //? "[%s] Player %s banned by %s.";
        CHAT_PLAYER_UNBANNED_NOTICE = 0x15, //? "[%s] Player %s unbanned by %s.";
        CHAT_PLAYER_NOT_BANNED_NOTICE = 0x16, //+ "[%s] Player %s is not banned.";
        CHAT_PLAYER_ALREADY_MEMBER_NOTICE = 0x17, //+ "[%s] Player %s is already on the channel.";
        CHAT_INVITE_NOTICE = 0x18, //+ "%2$s has invited you to join the channel '%1$s'.";
        CHAT_INVITE_WRONG_FACTION_NOTICE = 0x19, //+ "Target is in the wrong alliance for %s.";
        CHAT_WRONG_FACTION_NOTICE = 0x1A, //+ "Wrong alliance for %s.";
        CHAT_INVALID_NAME_NOTICE = 0x1B, //+ "Invalid channel name";
        CHAT_NOT_MODERATED_NOTICE = 0x1C, //+ "%s is not moderated";
        CHAT_PLAYER_INVITED_NOTICE = 0x1D, //+ "[%s] You invited %s to join the channel";
        CHAT_PLAYER_INVITE_BANNED_NOTICE = 0x1E, //+ "[%s] %s has been banned.";
        CHAT_THROTTLED_NOTICE = 0x1F, //+ "[%s] The number of messages that can be sent to this channel is limited, please wait to send another message.";
    }
}
