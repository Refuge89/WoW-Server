using System;
using System.Collections.Generic;
using System.Threading;
using Framework.Network;
using World_Server.Game;
using World_Server.Game.Entitys;

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

        private static void OnPlayerDespawn(PlayerEntity player)
        {
            foreach (PlayerEntity remotePlayer in Players)
            {
                // Skip own player
                if (player == remotePlayer) continue;

                if (remotePlayer.KnownPlayers.Contains(player))
                    DespawnPlayer(remotePlayer, player);
            }

            Players.Remove(player);
        }

        private static void OnPlayerSpawn(PlayerEntity player)
        {
            // Player Requesting Spawn
            Players.Add(player);
        }

        private static void Update()
        {
            while (true)
            {
                // Spawning && Despawning
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
                }

                // Update (Maybe have one for all entitys (GO, Unit & Players)
                foreach (PlayerEntity player in Players)
                {
                    if (player.UpdateCount > 0)
                    {
                        // Generate update packet
                        ServerPacket packet = UpdateObject.UpdateValues(player);

                        player.Session.SendPacket(packet);
                        EntityManager.SessionsWhoKnow(player).ForEach(s => s.SendPacket(packet));
                    }
                }

                Thread.Sleep(100);
            }
        }

        private static void DespawnPlayer(PlayerEntity remote, PlayerEntity player)
        {
            List<ObjectEntity> despawnPlayer = new List<ObjectEntity> { player };

            // Should be sending player entity
            remote.Session.SendPacket(UpdateObject.CreateOutOfRangeUpdate(despawnPlayer));

            // Add it to known players
            remote.KnownPlayers.Remove(player);
        }

        private static void SpawnPlayer(PlayerEntity remote, PlayerEntity player)
        {
            // Should be sending player entity
            remote.Session.SendPacket(UpdateObject.CreateCharacterUpdate(player.Character));

            // Add it to known players
            remote.KnownPlayers.Add(player);
        }

        private static bool InRangeCheck(PlayerEntity playerA, PlayerEntity playerB)
        {
            double distance = GetDistance(playerA.Character.MapX, playerA.Character.MapY, playerB.Character.MapX, playerB.Character.MapY);
            return distance < 10;
        }

        private static double GetDistance(float aX, float aY, float bX, float bY)
        {
            double a = aX - bX;
            double b = bY - aY;

            return Math.Sqrt(a * a + b * b);
        }
    }
}
