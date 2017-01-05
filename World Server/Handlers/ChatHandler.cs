using System;
using System.Text;
using Framework.Contants;
using Framework.Contants.Game;
using Framework.Network;
using World_Server.Sessions;

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
            session.sendPacket(new SmsgMessagechat(ChatMessageType.CHAT_MSG_SYSTEM, ChatMessageLanguage.LANG_COMMON, 0, message));
        }

        internal static void OnJoinChannel(WorldSession session, CmsgJoinChannel handler)
        {
            // Precisa inserir na base que entrou no canal
            session.sendPacket(new SmsgChannelNotify(ChatChannelNotify.CHAT_YOU_JOINED_NOTICE, (ulong)session.Character.Id, handler.ChannelName));
        }

        internal static void OnTextEmote(WorldSession session, CmsgTextEmote handler)
        {
            // Implementar alguns emotes na unha ?!?!?!?!?!?
            Program.WorldServer.TransmitToAll(new SmsgTextEmote((int)session.Character.Id, (int)handler.EmoteId, (int)handler.TextId));
        }

        internal static void OnMessageChat(WorldSession session, CmsgMessagechat handler)
        {
            Console.WriteLine(handler.Type);
            switch (handler.Type)
            {
                case ChatMessageType.CHAT_MSG_SAY:
                    string[] splitMessage = handler.Message.Split(' ');
                    if (splitMessage.Length == 2)
                    {
                        if (splitMessage[0].ToLower() == "spell")
                            session.sendPacket(new PSLearnSpell((uint)int.Parse(splitMessage[1])));

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

    class PSLearnSpell : ServerPacket
    {
        public PSLearnSpell(uint spellID) : base(WorldOpcodes.SMSG_LEARNED_SPELL)
        {
            Write((uint) spellID);
            Write((UInt16) 0);
        }
    }

    public enum StandState
    {
        UNIT_STANDING = 0x0,
        UNIT_SITTING = 0x1,
        UNIT_SITTINGCHAIR = 0x2,
        UNIT_SLEEPING = 0x3,
        UNIT_SITTINGCHAIRLOW = 0x4,
        UNIT_FIRSTCHAIRSIT = 0x4,
        UNIT_SITTINGCHAIRMEDIUM = 0x5,
        UNIT_SITTINGCHAIRHIGH = 0x6,
        UNIT_LASTCHAIRSIT = 0x6,
        UNIT_DEAD = 0x7,
        UNIT_KNEEL = 0x8,
        UNIT_NUMSTANDSTATES = 0x9,
        UNIT_NUMCHAIRSTATES = 0x3,
    }


    public enum Emotes
    {
        NONE = 0,
        AGREE = 1,
        AMAZE = 2,
        ANGRY = 3,
        APOLOGIZE = 4,
        APPLAUD = 5,
        BASHFUL = 6,
        BECKON = 7,
        BEG = 8,
        BITE = 9,
        BLEED = 10,
        BLINK = 11,
        BLUSH = 12,
        BONK = 13,
        BORED = 14,
        BOUNCE = 15,
        BRB = 16,
        BOW = 17,
        BURP = 18,
        BYE = 19,
        CACKLE = 20,
        CHEER = 21,
        CHICKEN = 22,
        CHUCKLE = 23,
        CLAP = 24,
        CONFUSED = 25,
        CONGRATULATE = 26,
        COUGH = 27,
        COWER = 28,
        CRACK = 29,
        CRINGE = 30,
        CRY = 31,
        CURIOUS = 32,
        CURTSEY = 33,
        DANCE = 34,
        DRINK = 35,
        DROOL = 36,
        EAT = 37,
        EYE = 38,
        FART = 39,
        FIDGET = 40,
        FLEX = 41,
        FROWN = 42,
        GASP = 43,
        GAZE = 44,
        GIGGLE = 45,
        GLARE = 46,
        GLOAT = 47,
        GREET = 48,
        GRIN = 49,
        GROAN = 50,
        GROVEL = 51,
        GUFFAW = 52,
        HAIL = 53,
        HAPPY = 54,
        HELLO = 55,
        HUG = 56,
        HUNGRY = 57,
        KISS = 58,
        KNEEL = 59,
        LAUGH = 60,
        LAYDOWN = 61,
        MASSAGE = 62,
        MOAN = 63,
        MOON = 64,
        MOURN = 65,
        NO = 66,
        NOD = 67,
        NOSEPICK = 68,
        PANIC = 69,
        PEER = 70,
        PLEAD = 71,
        POINT = 72,
        POKE = 73,
        PRAY = 74,
        ROAR = 75,
        ROFL = 76,
        RUDE = 77,
        SALUTE = 78,
        SCRATCH = 79,
        SEXY = 80,
        SHAKE = 81,
        SHOUT = 82,
        SHRUG = 83,
        SHY = 84,
        SIGH = 85,
        SIT = 86,
        SLEEP = 87,
        SNARL = 88,
        SPIT = 89,
        STARE = 90,
        SURPRISED = 91,
        SURRENDER = 92,
        TALK = 93,
        TALKEX = 94,
        TALKQ = 95,
        TAP = 96,
        THANK = 97,
        THREATEN = 98,
        TIRED = 99,
        VICTORY = 100,
        WAVE = 101,
        WELCOME = 102,
        WHINE = 103,
        WHISTLE = 104,
        WORK = 105,
        YAWN = 106,
        BOGGLE = 107,
        CALM = 108,
        COLD = 109,
        COMFORT = 110,
        CUDDLE = 111,
        DUCK = 112,
        INSULT = 113,
        INTRODUCE = 114,
        JK = 115,
        LICK = 116,
        LISTEN = 117,
        LOST = 118,
        MOCK = 119,
        PONDER = 120,
        POUNCE = 121,
        PRAISE = 122,
        PURR = 123,
        PUZZLE = 124,
        RAISE = 125,
        READY = 126,
        SHIMMY = 127,
        SHIVER = 128,
        SHOO = 129,
        SLAP = 130,
        SMIRK = 131,
        SNIFF = 132,
        SNUB = 133,
        SOOTHE = 134,
        STINK = 135,
        TAUNT = 136,
        TEASE = 137,
        THIRSTY = 138,
        VETO = 139,
        SNICKER = 140,
        STAND = 141,
        TICKLE = 142,
        VIOLIN = 143,
        SMILE = 163,
    }
}
