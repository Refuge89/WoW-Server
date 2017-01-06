namespace Framework.DBC.Structs
{
    public class CharStartOutfit
    {
        public uint ID;
        public byte Race;
        public byte Class;
        public byte Sex;
        public byte OutfitID;
        public int[] m_ItemID = new int[12];
        public int[] m_DisplayItemID = new int[12];
        public int[] m_InventoryType = new int[12];

        public bool Match(byte race, byte _class, byte gender)
        {
            return Race == race && Class == _class && Sex == gender;
        }
    }
}
