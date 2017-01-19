using System.Collections.Generic;
using System.Linq;
using System.Threading;
using World_Server.Game.Entitys;
using World_Server.Managers;

namespace World_Server.Game.World.Components
{
    public abstract class EntityComponent<T> where T : EntityBase
    {
        public List<T> Entitys;
        public abstract bool InRange(PlayerEntity playerEntity, T entity, float range);
        public abstract List<T> EntityListFromPlayer(PlayerEntity playerEntity);
        public abstract void GenerateEntitysForPlayer(PlayerEntity playerEntity);

        protected EntityComponent()
        {
            Entitys = new List<T>();

            new Thread(UpdateThread).Start();

            EntityManager.OnPlayerSpawn += World_OnPlayerSpawn;
        }

        private void World_OnPlayerSpawn(PlayerEntity playerEntity)
        {
            GenerateEntitysForPlayer(playerEntity);
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
            return Entitys.FindAll(e => (e as ObjectEntity).ObjectGuid.RawGuid == (entity as ObjectEntity).ObjectGuid.RawGuid).Any();
        }

        public virtual void AddEntityToWorld(T entity)
        {
            if (!Contains(entity))
                Entitys.Add(entity);
        }

        public virtual void Update()
        {
            // Spawning && Despawning
            foreach (PlayerEntity player in PlayerManager.Players)
            {
                foreach (T entity in Entitys)
                {
                    if (InRange(player, entity, 1000) && !PlayerKnowsEntity(player, entity)) // DISTANCE
                       SpawnEntityForPlayer(player, entity);

                    if (!InRange(player, entity, 5000) && PlayerKnowsEntity(player, entity)) // DISTANCE
                        DespawnEntityForPlayer(player, entity);
                }
            }
        }

        public virtual void DespawnEntityForPlayer(PlayerEntity playerEntity, T entity)
        {
            EntityListFromPlayer(playerEntity).Remove(entity);
            playerEntity.Session.SendPacket(UpdateObject.CreateOutOfRangeUpdate(entity as GameObjectEntityEntity));
        }

        public virtual void SpawnEntityForPlayer(PlayerEntity playerEntity, T entity)
        {
            EntityListFromPlayer(playerEntity).Add(entity);
        }

        public bool PlayerKnowsEntity(PlayerEntity playerEntity, T entity)
        {
            return EntityListFromPlayer(playerEntity).Contains(entity);
        }

    }
}
