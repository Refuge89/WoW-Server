using Framework.DBC;
using Framework.DBC.Structs;
using Framework.Helpers;
using System.Collections.Concurrent;

namespace World_Server.Managers
{
    public class DBCManager
    {
        public static ConcurrentDictionary<uint, CharStartOutfit> CharStartOutfit;
        public static ConcurrentDictionary<uint, AreaTable> AreaTable;
        public static ConcurrentDictionary<uint, ChrRaces> ChrRaces;

        public static void Boot()
        {
            Log.Print(LogType.Status, "Loading DBCs...");

            CharStartOutfit = DBReader.Read<uint, CharStartOutfit>("CharStartOutfit.dbc", "ID");
            AreaTable = DBReader.Read<uint, AreaTable>("AreaTable.dbc", "Id");
            //ChrRaces = DBReader.Read<uint, ChrRaces>("ChrRaces.dbc", "m_ID");
        }
    }
}
