using System;
using Framework.Contants.Character;

namespace Framework.DBC.Structs
{
    public class ChrRaces
    {
        public int m_ID;
        public int m_flags;                 // See Below
        public int m_factionID;             // facton template id. The order in the creation screen depends on this.
        public int m_MaleDisplayId;         // only used for the char creation/selection screen. Ingame the server sets the model.
        public int m_FemaleDisplayId;       // only used for the char creation/selection screen. Ingame the server sets the model.
        public string m_ClientPrefix;       // A short form of the name. Used for helmet models.
        public float m_MountScale;
        public int m_BaseLanguage;          // 1 = Horde, 7 = Alliance & not playable
        public int m_creatureType;          // Always 7 (humanoid).
        public int m_LoginEffectSpellID;
        public int m_CombatStunSpellID;
        public int m_ResSicknessSpellID;    // Always 15007.
        public int m_SplashSoundID;         // 1090 for dwarfs, 1096 for the others. 
        public int m_startingTaxiNodes;
        public string m_clientFileString;   // Same as the one used in model-filepathes.
        public int m_cinematicSequenceID;   // Used for the opening cinematic.
        public string m_name_lang;
        public int[] NONE = new int[7];
        public int m_name_flag;

        public bool Match(byte race)
        {
            return m_ID == race;
        }

        public bool Match(RaceID race)
        {
            return m_ID == (int)race;
        }
    }
}
