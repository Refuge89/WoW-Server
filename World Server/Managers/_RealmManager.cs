using System.Collections.Generic;
using World_Server.Game.Entitys;
using World_Server.Sessions;

namespace World_Server.Managers
{
    public delegate void PlayerEvent(PlayerEntity player);

    public class EntityManager
    {
        public static event PlayerEvent OnPlayerSpawn;
        public static event PlayerEvent OnPlayerDespawn;

        public static void DispatchOnPlayerSpawn(PlayerEntity player)
        {
            OnPlayerSpawn?.Invoke(player);
        }

        public static void DispatchOnPlayerDespawn(PlayerEntity player)
        {
            OnPlayerDespawn?.Invoke(player);
        }

        // [Helpers]
        public static List<PlayerEntity> PlayersWhoKnow(PlayerEntity player)
        {
            return PlayerManager.Players.FindAll(p => p.KnownPlayers.Contains(player));
        }

        public static List<WorldSession> SessionsWhoKnow(PlayerEntity player, bool includeSelf = false)
        {
            List<WorldSession> sessions = PlayersWhoKnow(player).ConvertAll<WorldSession>(p => p.Session);

            if (includeSelf == true) sessions.Add(player.Session);

            return sessions;
        }

        public static List<PlayerEntity> PlayersWhoKnowUnit(UnitEntity unit)
        {
            return PlayerManager.Players.FindAll(p => p.KnownUnits.Contains(unit));
        }
    }
}
