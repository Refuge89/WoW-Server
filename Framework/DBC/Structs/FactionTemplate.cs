namespace Framework.DBC.Structs
{
    public class FactionTemplate
    {
        public uint Id;
        public int Faction;
        public int FactionGroup;
        public int FriendGroup;
        public int EnemyGroup;
        public int[] Enemies = new int[4];
        public int[] Friend = new int[4];

        public bool IsFriendlyTo(FactionTemplate entry)
        {
            if (entry.Faction > 0)
            {
                for (int i = 0; i < 4; ++i)
                    if (Enemies[i] == entry.Faction)
                        return false;
                for (int i = 0; i < 4; ++i)
                    if (Friend[i] == entry.Faction)
                        return true;
            }

            return (FactionGroup & entry.FactionGroup) == entry.FactionGroup ||
                   (FactionGroup & entry.FriendGroup) == entry.FriendGroup;
        }

        public bool IsEnemyTo(FactionTemplate entry)
        {
            if (entry.Faction > 0)
            {
                for (int i = 0; i < 4; ++i)
                    if (Enemies[i] == entry.Faction)
                        return true;
                for (int i = 0; i < 4; ++i)
                    if (Friend[i] == entry.Faction)
                        return false;
            }

            return (EnemyGroup & entry.FactionGroup) == EnemyGroup;
        }

        public bool NeutralToAll()
        {
            for (int i = 0; i < 4; ++i)
                if (Enemies[i] != 0)
                    return false;

            return EnemyGroup == 0 && FriendGroup == 0;
        }
    }
}