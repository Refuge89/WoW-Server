using System;
using System.Collections.Generic;
using System.Threading;
using World_Server.Game.Entitys;
using World_Server.Managers;

namespace World_Server.Handlers.World
{
    public abstract class EntityComponent<T> where T : EntityBase
    {
        public List<T> Entitys;

        protected EntityComponent()
        {
            Entitys = new List<T>();

            new Thread(UpdateThread).Start();

            EntityManager.OnPlayerSpawn += World_OnPlayerSpawn;
        }

        private void World_OnPlayerSpawn(PlayerEntity player)
        {
            GenerateEntitysForPlayer(player);
        }

        private void UpdateThread()
        {
            while (true)
            {
                Update();
                Thread.Sleep(500);
            }
        }

        public abstract void GenerateEntitysForPlayer(PlayerEntity player);

        public virtual void Update()
        {
            // Spawning && Despawning
            foreach (PlayerEntity player in PlayerManager.Players)
            {
                foreach (T entity in Entitys.ToArray())
                {
                    if (InRange(player, entity, 50))
                    {
                        if (!PlayerKnowsEntity(player, entity))
                            SpawnEntityForPlayer(player, entity);
                    }

                    if (!InRange(player, entity, 100) && PlayerKnowsEntity(player, entity))
                        DespawnEntityForPlayer(player, entity);
                }
            }
        }

        public virtual void DespawnEntityForPlayer(PlayerEntity player, T entity)
        {
            EntityListFromPlayer(player).Remove(entity);

            player.OutOfRangeEntitys.Add((entity as ObjectEntity));
        }

        public virtual void SpawnEntityForPlayer(PlayerEntity player, T entity)
        {
            EntityListFromPlayer(player).Add(entity);
        }

        public bool PlayerKnowsEntity(PlayerEntity player, T entity)
        {
            return EntityListFromPlayer(player).Contains(entity);
        }

        public abstract bool InRange(PlayerEntity player, T entity, float range);
        public abstract List<T> EntityListFromPlayer(PlayerEntity player);
    }

    public class UnitComponent : EntityComponent<UnitEntity>
    {
        public override List<UnitEntity> EntityListFromPlayer(PlayerEntity player)
        {
            return player.KnownUnits;
        }

        public override bool InRange(PlayerEntity player, UnitEntity entity, float range)
        {
            double distance = GetDistance(player.X, player.Y, entity.X, entity.Y);

            return distance < range;
        }

        private static double GetDistance(float aX, float aY, float bX, float bY)
        {
            double a = aX - bX;
            double b = bY - aY;

            return Math.Sqrt(a * a + b * b);
        }

        public override void GenerateEntitysForPlayer(PlayerEntity player)
        {
            //throw new NotImplementedException();
        }
    }
}
