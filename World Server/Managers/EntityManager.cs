using System.Collections.Generic;
using Framework.Database.Tables;
using World_Server.Game;
using World_Server.Game.Entitys;
using World_Server.Sessions;

namespace World_Server.Managers
{
    public delegate void PlayerEvent(PlayerEntity playerEntity);

    public class EntityManager
    {
        public static event PlayerEvent OnPlayerSpawn;
        public static event PlayerEvent OnPlayerDespawn;

        public static void DispatchOnPlayerSpawn(PlayerEntity playerEntity)
        {
            OnPlayerSpawn?.Invoke(playerEntity);
        }

        public static void DispatchOnPlayerDespawn(PlayerEntity playerEntity)
        {
            OnPlayerDespawn?.Invoke(playerEntity);
        }

        public static List<PlayerEntity> PlayersWhoKnow(PlayerEntity playerEntity)
        {
            return PlayerManager.Players.FindAll(p => p.KnownPlayers.Contains(playerEntity));
        }

        public static List<WorldSession> SessionsWhoKnow(PlayerEntity playerEntity, bool includeSelf = false)
        {
            List<WorldSession> sessions = PlayersWhoKnow(playerEntity).ConvertAll(p => p.Session);

            if (includeSelf) sessions.Add(playerEntity.Session);

            return sessions;
        }

        public static void SpawnGameObjects(WorldSession worldSession)
        {
            worldSession.Entity.MapX = worldSession.Character.MapX;
            worldSession.Entity.MapY = worldSession.Character.MapY;
            worldSession.Entity.MapZ = worldSession.Character.MapZ;

            List<WorldGameObjects> gameObjects = Main.Database.GetGameObjects(worldSession.Entity, 1000); // DISTANCE

            foreach (WorldGameObjects gameObject in gameObjects)
            {
                worldSession.SendPacket(UpdateObject.CreateGameObject(gameObject));
            }
        }
    }
}
