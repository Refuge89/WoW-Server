﻿namespace Framework.Contants.Game
{
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

    public enum ChatFlags
    {
        CHAT_TAG_NONE = 0,
        CHAT_TAG_AFK = 1,
        CHAT_TAG_DND = 2,
        CHAT_TAG_GM = 3,
    }

    public enum ChatMessageLanguage : byte
    {
        LANG_UNIVERSAL = 0,
        LANG_ORCISH = 1,
        LANG_DARNASSIAN = 2,
        LANG_TAURAHE = 3,
        LANG_DWARVISH = 6,
        LANG_COMMON = 7,
        LANG_DEMONIC = 8,
        LANG_TITAN = 9,
        LANG_THALASSIAN = 10,
        LANG_DRACONIC = 11,
        LANG_KALIMAG = 12,
        LANG_GNOMISH = 13,
        LANG_TROLL = 14,
        LANG_GUTTERSPEAK = 33
    }

    public enum ChatMessageType : byte
    {
        CHAT_MSG_SAY = 0x00,
        CHAT_MSG_PARTY = 0x01,
        CHAT_MSG_RAID = 0x02,
        CHAT_MSG_GUILD = 0x03,
        CHAT_MSG_OFFICER = 0x04,
        CHAT_MSG_YELL = 0x05,
        CHAT_MSG_WHISPER = 0x06,
        CHAT_MSG_WHISPER_INFORM = 0x07,
        CHAT_MSG_EMOTE = 0x08,
        CHAT_MSG_TEXT_EMOTE = 0x09,
        CHAT_MSG_SYSTEM = 0x0A,
        CHAT_MSG_MONSTER_SAY = 0x0B,
        CHAT_MSG_MONSTER_YELL = 0x0C,
        CHAT_MSG_MONSTER_EMOTE = 0x0D,
        CHAT_MSG_CHANNEL = 0x0E,
        CHAT_MSG_CHANNEL_JOIN = 0x0F,
        CHAT_MSG_CHANNEL_LEAVE = 0x10,
        CHAT_MSG_CHANNEL_LIST = 0x11,
        CHAT_MSG_CHANNEL_NOTICE = 0x12,
        CHAT_MSG_CHANNEL_NOTICE_USER = 0x13,
        CHAT_MSG_AFK = 0x14,
        CHAT_MSG_DND = 0x15,
        CHAT_MSG_IGNORED = 0x16,
        CHAT_MSG_SKILL = 0x17,
        CHAT_MSG_LOOT = 0x18,
        CHAT_MSG_BG_SYSTEM_NEUTRAL = 0x52,
        CHAT_MSG_BG_SYSTEM_ALLIANCE = 0x53,
        CHAT_MSG_BG_SYSTEM_HORDE = 0x54,
        CHAT_MSG_RAID_LEADER = 0x57,
        CHAT_MSG_RAID_WARNING = 0x58,
        CHAT_MSG_BATTLEGROUND = 0x5C,
        CHAT_MSG_BATTLEGROUND_LEADER = 0x5D,
        CHAT_MSG_REPLY = 0x09,
        CHAT_MSG_MONSTER_PARTY = 0x30, // 0x0D, just selected some free random value for avoid duplicates with really existed values
        CHAT_MSG_MONSTER_WHISPER = 0x31, // 0x0F, just selected some free random value for avoid duplicates with really existed values
        // CHAT_MSG_MONEY                  = 0x1C,
        // CHAT_MSG_OPENING                = 0x1D,
        // CHAT_MSG_TRADESKILLS            = 0x1E,
        // CHAT_MSG_PET_INFO               = 0x1F,
        // CHAT_MSG_COMBAT_MISC_INFO       = 0x20,
        // CHAT_MSG_COMBAT_XP_GAIN         = 0x21,
        // CHAT_MSG_COMBAT_HONOR_GAIN      = 0x22,
        // CHAT_MSG_COMBAT_FACTION_CHANGE  = 0x23,
        CHAT_MSG_RAID_BOSS_WHISPER = 0x29,
        CHAT_MSG_RAID_BOSS_EMOTE = 0x2A
        // CHAT_MSG_FILTERED               = 0x2B,
        // CHAT_MSG_RESTRICTED             = 0x2E,
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
        CHAT_THROTTLED_NOTICE = 0x1F //+ "[%s] The number of messages that can be sent to this channel is limited, please wait to send another message.";
    }
}
