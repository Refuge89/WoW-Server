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

        private void World_OnPlayerSpawn(Player player)
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

        public abstract void GenerateEntitysForPlayer(Player player);

        public virtual void Update()
        {
            // Spawning && Despawning
            foreach (Player player in PlayerManager.Players)
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

        public virtual void DespawnEntityForPlayer(Player player, T entity)
        {
            EntityListFromPlayer(player).Remove(entity);

            player.OutOfRangeEntitys.Add((entity as Game.Entitys.Object));
        }

        public virtual void SpawnEntityForPlayer(Player player, T entity)
        {
            EntityListFromPlayer(player).Add(entity);
        }

        public bool PlayerKnowsEntity(Player player, T entity)
        {
            return EntityListFromPlayer(player).Contains(entity);
        }

        public abstract bool InRange(Player player, T entity, float range);
        public abstract List<T> EntityListFromPlayer(Player player);
    }

    public class UnitComponent : EntityComponent<Unit>
    {
        public override List<Unit> EntityListFromPlayer(Player player)
        {
            return player.KnownUnits;
        }

        public override bool InRange(Player player, Unit entity, float range)
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

        public override void GenerateEntitysForPlayer(Player player)
        {
            //throw new NotImplementedException();
        }
    }
}
