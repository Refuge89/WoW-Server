using System;
using System.Collections.Generic;
using Framework.Database.Tables;
using World_Server.Game.Entitys;
using World_Server.Game.World.Blocks;

namespace World_Server.Game.World.Components
{
    public class GameObjectComponent : EntityComponent<GameObjectEntityEntity>
    {
        public override void GenerateEntitysForPlayer(PlayerEntity playerEntity)
        {
            List<WorldGameObjects> gameObjects = Main.Database.GetGameObjects(playerEntity, 1000); // DISTANCE

            gameObjects.ForEach(closeGo =>
            {
                AddEntityToWorld(new GameObjectEntityEntity(closeGo));
            });
        }

        public override void SpawnEntityForPlayer(PlayerEntity playerEntity, GameObjectEntityEntity entityEntity)
        {
            lock (playerEntity.UpdateBlocks)
            {
                playerEntity.UpdateBlocks.Add(new GameObjectBlock(entityEntity));
            }

            base.SpawnEntityForPlayer(playerEntity, entityEntity);
        }

        public override bool InRange(PlayerEntity playerEntity, GameObjectEntityEntity entityEntity, float range)
        {
            double distance = GetDistance(playerEntity.Character.MapX, playerEntity.Character.MapY, entityEntity.GameObjects.mapX, entityEntity.GameObjects.mapY);
            return distance < 30; // DISTANCE
        }

        private static double GetDistance(float aX, float aY, float bX, float bY)
        {
            double a = aX - bX;
            double b = bY - aY;

            return Math.Sqrt(a * a + b * b);
        }

        public override List<GameObjectEntityEntity> EntityListFromPlayer(PlayerEntity playerEntity)
        {
            return playerEntity.KnownGameObjects;
        }
    }
}
