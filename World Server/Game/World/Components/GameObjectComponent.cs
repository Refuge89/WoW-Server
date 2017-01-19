using System;
using System.Collections.Generic;
using Framework.Database.Tables;
using World_Server.Game.Entitys;
using World_Server.Game.World.Blocks;

namespace World_Server.Game.World.Components
{
    public class GameObjectComponent : EntityComponent<GameObject>
    {
        public override void GenerateEntitysForPlayer(Player player)
        {
            List<WorldGameObjects> gameObjects = Main.Database.GetGameObjects(player, 1000);

            gameObjects.ForEach(closeGo =>
            {
                AddEntityToWorld(new GameObject(closeGo));
            });
        }

        public override void SpawnEntityForPlayer(Player player, GameObject entity)
        {
            lock (player.UpdateBlocks)
            {
                player.UpdateBlocks.Add(new GameObjectBlock(entity));
            }

            base.SpawnEntityForPlayer(player, entity);
        }

        public override bool InRange(Player player, GameObject entity, float range)
        {
            double distance = GetDistance(player.Character.MapX, player.Character.MapY, entity.GameObjects.mapX, entity.GameObjects.mapY);
            return distance < 30;
        }

        private static double GetDistance(float aX, float aY, float bX, float bY)
        {
            double a = aX - bX;
            double b = bY - aY;

            return Math.Sqrt(a * a + b * b);
        }

        public override List<GameObject> EntityListFromPlayer(Player player)
        {
            return player.KnownGameObjects;
        }
    }
}
