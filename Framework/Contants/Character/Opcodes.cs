using System;

namespace Framework.Contants.Character
{
    public enum RaceID : int
    {
        NONE                = 0,
        HUMAN               = 1,
        ORC                 = 2,
        DWARF               = 3,
        NIGHTELF            = 4,
        UNDEAD              = 5,
        TAUREN              = 6,
        GNOME               = 7,
        TROLL               = 8,
        GOBLIN              = 9,
        BLOODELF            = 10,
        DRAENEI             = 11,
        WORGEN              = 22,
        PANDAREN_NEUTRAL    = 24,
        PANDAREN_ALLIANCE   = 25,
        PANDAREN_HORDE      = 26,
    }

    public enum ClassID : int
    {
        PetTalents          = 0,
        WARRIOR             = 1,
        PALADIN             = 2,
        HUNTER              = 3,
        ROGUE               = 4,
        PRIEST              = 5,
        DEATH_KNIGHT        = 6,
        SHAMAN              = 7,
        MAGE                = 8,
        WARLOCK             = 9,
        MONK                = 10,
        DRUID               = 11,
        DEMON_HUNTER        = 12,
    }

    public enum GenderID : byte
    {
        MALE                = 0,
        FEMALE              = 1,
    }

    [Flags]
    public enum MovementFlags
    {
        MOVEFLAG_NONE                   = 0x00000000,
        MOVEFLAG_FORWARD                = 0x00000001,
        MOVEFLAG_BACKWARD               = 0x00000002,
        MOVEFLAG_STRAFE_LEFT            = 0x00000004,
        MOVEFLAG_STRAFE_RIGHT           = 0x00000008,
        MOVEFLAG_TURN_LEFT              = 0x00000010,
        MOVEFLAG_TURN_RIGHT             = 0x00000020,
        MOVEFLAG_PITCH_UP               = 0x00000040,
        MOVEFLAG_PITCH_DOWN             = 0x00000080,
        MOVEFLAG_WALK_MODE              = 0x00000100,               // Walking

        MOVEFLAG_LEVITATING             = 0x00000400,
        MOVEFLAG_ROOT                   = 0x00000800,               // [-ZERO] is it really need and correct value
        MOVEFLAG_FALLING                = 0x00002000,
        MOVEFLAG_FALLINGFAR             = 0x00004000,
        MOVEFLAG_SWIMMING               = 0x00200000,               // appears with fly flag also
        MOVEFLAG_ASCENDING              = 0x00400000,               // [-ZERO] is it really need and correct value
        MOVEFLAG_CAN_FLY                = 0x00800000,               // [-ZERO] is it really need and correct value
        MOVEFLAG_FLYING                 = 0x01000000,               // [-ZERO] is it really need and correct value

        MOVEFLAG_ONTRANSPORT            = 0x02000000,               // Used for flying on some creatures
        MOVEFLAG_SPLINE_ELEVATION       = 0x04000000,               // used for flight paths
        MOVEFLAG_SPLINE_ENABLED         = 0x08000000,               // used for flight paths
        MOVEFLAG_WATERWALKING           = 0x10000000,               // prevent unit from falling through water
        MOVEFLAG_SAFE_FALL              = 0x20000000,               // active rogue safe fall spell (passive)
        MOVEFLAG_HOVER                  = 0x40000000,
    }

    public enum SpellCastFlags
    {
        CastFlagNone = 0x00000000,
        CastFlagHiddenCombatlog = 0x00000001, // hide in combat log?
        CastFlagUnknown2 = 0x00000002,
        CastFlagUnknown3 = 0x00000004,
        CastFlagUnknown4 = 0x00000008,
        CastFlagUnknown5 = 0x00000010,
        CastFlagAmmo = 0x00000020, // Projectiles visual
        CastFlagUnknown7 = 0x00000040, // !0x41 mask used to call CGTradeSkillInfo::DoRecast
        CastFlagUnknown8 = 0x00000080,
        CastFlagUnknown9 = 0x00000100
    }

    public enum PowerTypes : uint
    {
        TYPE_MANA = 0,
        TYPE_RAGE = 1,
        TYPE_FOCUS = 2,
        TYPE_ENERGY = 3,
        TYPE_HAPPINESS = 4,
        POWER_HEALTH = 0xFFFFFFFE
    }

    public enum ActionButtonType
    {
        ACTION_BUTTON_SPELL = 0x00,
        ACTION_BUTTON_C = 0x01,                     // click?
        ACTION_BUTTON_MACRO = 0x40,
        ACTION_BUTTON_CMACRO = ACTION_BUTTON_C | ACTION_BUTTON_MACRO,
        ACTION_BUTTON_ITEM = 0x80
    }

    public enum InventoryTypes : byte
    {
        NONE_EQUIP = 0x00,
        HEAD = 0x01,
        NECK = 0x02,
        SHOULDER = 0x03,
        BODY = 0x04,
        CHEST = 0x05,
        WAIST = 0x06,
        LEGS = 0x07,
        FEET = 0x08,
        WRIST = 0x09,
        HAND = 0x0A,
        FINGER = 0x0B,
        TRINKET = 0x0C,
        WEAPON = 0x0D,
        SHIELD = 0x0E,
        RANGED = 0x0F,
        CLOAK = 0x10,
        TWOHANDEDWEAPON = 0x11,
        BAG = 0x12,
        TABARD = 0x13,
        ROBE = 0x14,
        WEAPONMAINHAND = 0x15,
        WEAPONOFFHAND = 0x16,
        HOLDABLE = 0x17,
        AMMO = 0x18,
        THROWN = 0x19,
        RANGEDRIGHT = 0x1A,
        NUM_TYPES = 0x1B
    }

    public enum InventorySlots
    {
        SLOT_HEAD = 0,
        SLOT_NECK = 1,
        SLOT_SHOULDERS = 2,
        SLOT_SHIRT = 3,
        SLOT_CHEST = 4,
        SLOT_WAIST = 5,
        SLOT_LEGS = 6,
        SLOT_FEET = 7,
        SLOT_WRISTS = 8,
        SLOT_HANDS = 9,
        SLOT_FINGERL = 10,
        SLOT_FINGERR = 11,
        SLOT_TRINKETL = 12,
        SLOT_TRINKETR = 13,
        SLOT_BACK = 14,
        SLOT_MAINHAND = 15,
        SLOT_OFFHAND = 16,
        SLOT_RANGED = 17,
        SLOT_TABARD = 18,

        //! Misc Types
        SLOT_BAG1 = 19,
        SLOT_BAG2 = 20,
        SLOT_BAG3 = 21,
        SLOT_BAG4 = 22,
        SLOT_INBACKPACK = 23,

        SLOT_ITEM_START = 23,
        SLOT_ITEM_END = 39,

        SLOT_BANK_ITEM_START = 39,
        SLOT_BANK_ITEM_END = 63,
        SLOT_BANK_BAG_1 = 63,
        SLOT_BANK_BAG_2 = 64,
        SLOT_BANK_BAG_3 = 65,
        SLOT_BANK_BAG_4 = 66,
        SLOT_BANK_BAG_5 = 67,
        SLOT_BANK_BAG_6 = 68,
        SLOT_BANK_END = 69
    }
}
