using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Framework.Network;
using World_Server.Game;
using World_Server.Game.Entitys;
using World_Server.Game.World.Blocks;

namespace World_Server.Managers
{
    public class PlayerManager
    {
        public static List<PlayerEntity> Players { get; private set; }

        public static void Boot()
        {
            Players = new List<PlayerEntity>();

            EntityManager.OnPlayerSpawn += OnPlayerSpawn;
            EntityManager.OnPlayerDespawn += OnPlayerDespawn;

            new Thread(Update).Start();
        }

        private static void OnPlayerDespawn(PlayerEntity playerEntity)
        {
            foreach (PlayerEntity remotePlayer in Players)
            {
                if (playerEntity == remotePlayer) continue;

                if (remotePlayer.KnownPlayers.Contains(playerEntity))
                    DespawnPlayer(remotePlayer, playerEntity);
            }

            Players.Remove(playerEntity);
        }

        private static void OnPlayerSpawn(PlayerEntity playerEntity)
        {
            Players.Add(playerEntity);
        }

        private static void Update()
        {
            while (true)
            {
                foreach (PlayerEntity player in Players)
                {
                    foreach (PlayerEntity otherPlayer in Players)
                    {
                        // Ignore self
                        if (player == otherPlayer) continue;

                        if (InRangeCheck(player, otherPlayer))
                        {
                            if (!player.KnownPlayers.Contains(otherPlayer))
                                SpawnPlayer(player, otherPlayer);
                        }
                        else
                        {
                            if (player.KnownPlayers.Contains(otherPlayer))
                                DespawnPlayer(player, otherPlayer);
                        }
                    }

                    if (player.UpdateCount > 0)
                    {
                        ServerPacket packet = UpdateObject.UpdateValues(player);
                        player.Session.SendPacket(packet);
                        EntityManager.SessionsWhoKnow(player).ForEach(s => s.SendPacket(packet));
                    }
                }

                Thread.Sleep(100);
            }
        }

        private static void DespawnPlayer(PlayerEntity remote, PlayerEntity playerEntity)
        {
            List<ObjectEntity> despawnPlayer = new List<ObjectEntity> { playerEntity };

            // Should be sending playerEntity entityEntity
            remote.Session.SendPacket(UpdateObject.CreateOutOfRangeUpdate(despawnPlayer));

            // Add it to known players
            remote.KnownPlayers.Remove(playerEntity);
        }

        private static void SpawnPlayer(PlayerEntity remote, PlayerEntity playerEntity)
        {
            // Should be sending playerEntity entityEntity
            remote.Session.SendPacket(UpdateObject.CreateCharacterUpdate(playerEntity.Character));

            // Add it to known players
            remote.KnownPlayers.Add(playerEntity);
        }

        private static bool InRangeCheck(PlayerEntity playerEntityA, PlayerEntity playerEntityB)
        {
            double distance = GetDistance(playerEntityA.Character.MapX, playerEntityA.Character.MapY, playerEntityB.Character.MapX, playerEntityB.Character.MapY);
            return distance < 10; // DISTANCE
        }

        private static double GetDistance(float aX, float aY, float bX, float bY)
        {
            double a = aX - bX;
            double b = bY - aY;

            return Math.Sqrt(a * a + b * b);
        }
    }
}
