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
        public static List<Player> Players { get; private set; }

        public static void Boot()
        {
            Players = new List<Player>();

            EntityManager.OnPlayerSpawn += OnPlayerSpawn;
            EntityManager.OnPlayerDespawn += OnPlayerDespawn;

            new Thread(Update).Start();
        }

        private static void OnPlayerDespawn(Player player)
        {
            foreach (Player remotePlayer in Players)
            {
                if (player == remotePlayer) continue;

                if (remotePlayer.KnownPlayers.Contains(player))
                    DespawnPlayer(remotePlayer, player);
            }

            Players.Remove(player);
        }

        private static void OnPlayerSpawn(Player player)
        {
            Players.Add(player);
        }

        private static void Update()
        {
            while (true)
            {
                foreach (Player player in Players)
                {
                    foreach (Player otherPlayer in Players)
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

        private static void DespawnPlayer(Player remote, Player player)
        {
            List<Game.Entitys.Object> despawnPlayer = new List<Game.Entitys.Object> { player };

            // Should be sending player entity
            remote.Session.SendPacket(UpdateObject.CreateOutOfRangeUpdate(despawnPlayer));

            // Add it to known players
            remote.KnownPlayers.Remove(player);
        }

        private static void SpawnPlayer(Player remote, Player player)
        {
            // Should be sending player entity
            remote.Session.SendPacket(UpdateObject.CreateCharacterUpdate(player.Character));

            // Add it to known players
            remote.KnownPlayers.Add(player);
        }

        private static bool InRangeCheck(Player playerA, Player playerB)
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
