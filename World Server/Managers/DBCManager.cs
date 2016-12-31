using Framework.DBC;
using Framework.DBC.Structs;
using Framework.Helpers;
using System.Collections.Concurrent;

namespace World_Server.Managers
{
    public class DBCManager
    {
        public static ConcurrentDictionary<uint, CharStartOutfit> CharStartOutfit;

        public static void Boot()
        {
            Log.Print(LogType.Status, "Loading DBCs...");

            CharStartOutfit = DBReader.Read<uint, CharStartOutfit>("CharStartOutfit.dbc", "ID");
        }
    }
}
