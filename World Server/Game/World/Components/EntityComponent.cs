using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using World_Server.Game.Entitys;
using World_Server.Managers;
using Object = World_Server.Game.Entitys.Object;

namespace World_Server.Game.World.Components
{
    public abstract class EntityComponent<T> where T : EntityBase
    {
        public List<T> Entitys;
        public abstract bool InRange(Player player, T entity, float range);
        public abstract List<T> EntityListFromPlayer(Player player);
        public abstract void GenerateEntitysForPlayer(Player player);

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

        private bool Contains(T entity)
        {
            return Entitys.FindAll(e => (e as Object).ObjectGuid.RawGuid == (entity as Object).ObjectGuid.RawGuid).Any();
        }

        public virtual void AddEntityToWorld(T entity)
        {
            if (!Contains(entity))
                Entitys.Add(entity);
        }

        public virtual void Update()
        {
            // Spawning && Despawning
            foreach (Player player in PlayerManager.Players)
            {
                foreach (T entity in Entitys)
                {
                    if (InRange(player, entity, 1000) && !PlayerKnowsEntity(player, entity))
                       SpawnEntityForPlayer(player, entity);

                    if (!InRange(player, entity, 5000) && PlayerKnowsEntity(player, entity))
                        DespawnEntityForPlayer(player, entity);
                }
            }
        }

        public virtual void DespawnEntityForPlayer(Player player, T entity)
        {
            EntityListFromPlayer(player).Remove(entity);
            player.Session.SendPacket(UpdateObject.CreateOutOfRangeUpdate(entity as GameObject));
        }

        public virtual void SpawnEntityForPlayer(Player player, T entity)
        {
            EntityListFromPlayer(player).Add(entity);
        }

        public bool PlayerKnowsEntity(Player player, T entity)
        {
            return EntityListFromPlayer(player).Contains(entity);
        }

    }
}
