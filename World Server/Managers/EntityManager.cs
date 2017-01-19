using System.Collections.Generic;
using Framework.Database.Tables;
using World_Server.Game;
using World_Server.Game.Entitys;
using World_Server.Sessions;

namespace World_Server.Managers
{
    public delegate void PlayerEvent(Player player);

    public class EntityManager
    {
        public static event PlayerEvent OnPlayerSpawn;
        public static event PlayerEvent OnPlayerDespawn;

        public static void DispatchOnPlayerSpawn(Player player)
        {
            OnPlayerSpawn?.Invoke(player);
        }

        public static void DispatchOnPlayerDespawn(Player player)
        {
            OnPlayerDespawn?.Invoke(player);
        }

        public static List<Player> PlayersWhoKnow(Player player)
        {
            return PlayerManager.Players.FindAll(p => p.KnownPlayers.Contains(player));
        }

        public static List<WorldSession> SessionsWhoKnow(Player player, bool includeSelf = false)
        {
            List<WorldSession> sessions = PlayersWhoKnow(player).ConvertAll(p => p.Session);

            if (includeSelf) sessions.Add(player.Session);

            return sessions;
        }

        public static void SpawnGameObjects(WorldSession worldSession)
        {
            worldSession.Entity.MapX = worldSession.Character.MapX;
            worldSession.Entity.MapY = worldSession.Character.MapY;
            worldSession.Entity.MapZ = worldSession.Character.MapZ;

            List<WorldGameObjects> gameObjects = Main.Database.GetGameObjects(worldSession.Entity, 1000);

            foreach (WorldGameObjects gameObject in gameObjects)
            {
                worldSession.SendPacket(UpdateObject.CreateGameObject(gameObject));
            }
        }
    }
}
