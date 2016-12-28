namespace Framework.Contants
{
    public enum RealmType : byte
    {
        Normal          = 0x00,
        PVP             = 0x01,
        RP              = 0x06,
        RPPVP           = 0x08,
    }

    public enum RealmFlag : byte
    {
        None            = 0x00,
        Invalid         = 0x01,
        Offline         = 0x02,
        SpecifyBuild    = 0x04,
        NewPlayers      = 0x20,
        Recommended     = 0x40,
        Full            = 0x80,
    }

    public enum RealmTimezone : byte
    {
        AnyLocale       = 0,
        UnitedStates    = 1,
        Korea           = 2,
        English         = 3,
        Taiwan          = 4,
        China           = 5,
        TestServer      = 99,
        QAServer        = 101,
    };

    public static class RealmPopulationPreset
    {
        public const float Low      = 0.5f;
        public const float Medium   = 1.0f;
        public const float High     = 2.0f;
        public const float Full     = 400.0f;
    }

    public enum PacketHeaderType : byte
    {
        AuthCmsg        = 1,
        AuthSmsg        = 3,
        WorldSmsg       = 4,
        WorldCmsg       = 6
    }
}
