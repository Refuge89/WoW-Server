using Framework.Contants.Character;

namespace Framework.DBC.Structs
{
    public class ChrRaces
    {
        public int Id;
        public int Flags;                 // See Below
        public int FactionId;             // facton template id. The order in the creation screen depends on this.
        public int Exploration;           // Played on exploring zones with SMSG_EXPLORATION_EXPERIENCE.
        public int MaleDisplayId;         // only used for the char creation/selection screen. Ingame the server sets the model.
        public int FemaleDisplayId;       // only used for the char creation/selection screen. Ingame the server sets the model.
        public string[] ClientPrefix;         // A short form of the name. Used for helmet models.
        public float MountScale;
        public int BaseLanguage;          // 1 = Horde, 7 = Alliance & not playable
        public int CreatureType;          // Always 7 (humanoid) . (6 undead)
        public int LoginEffectSpellId;    // 836
        public int CombatStunSpellId;     // 1604
        public int ResSicknessSpellId;    // Always 15007.
        public int SplashSoundId;         // 1090 for dwarfs, 1096 for the others. 
        public int StartingTaxiNodes;
        public string[] ClientFileString;   // Same as the one used in model-filepathes.
        public int CinematicSequenceId;   // Used for the opening cinematic.
        public string[] NameLang;
        public int[] NONE = new int[7];
        public int NameFlag;
        public int FacialHair;
        public int FacialHairCustomization;
        public int HairCustomization;

        public bool Match(byte race)
        {
            return Id == race;
        }

        public bool Match(RaceID race)
        {
            return Id == (int)race;
        }
    }
}
