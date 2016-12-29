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
}
