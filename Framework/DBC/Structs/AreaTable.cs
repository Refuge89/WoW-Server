namespace Framework.DBC.Structs
{
    public class AreaTable
    {
        public uint Id;
        public int AreaNumber;
        public int ContinentId;
        public int ParentAreaNum;
        public int AreaBit;
        public int Flags;
        public int SoundProviderPref;
        public int SoundProviderPrefUnderwater;
        public int MidiAmbience;
        public int MidiAmbienceUnderwater;
        public int ZoneMusic;
        public int IntroSound;
        public int IntroPriority;
        public string[] AreaNameLang = new string[8];
        public int AreaNameFlag;
    }
}
