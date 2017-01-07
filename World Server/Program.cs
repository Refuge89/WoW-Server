using Framework.Helpers;
using Framework.Network;
using Framework.Sessions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using World_Server.Handlers.World;
using World_Server.Managers;
using World_Server.Sessions;

namespace World_Server
{
    class Program
    {
        private static readonly Assembly MAssembly = Assembly.GetEntryAssembly();

        public static WorldServer World { get; private set; }

        public static DatabaseManager Database;

        public static UnitComponent UnitComponent { get; private set; }
        //public static GameObjectComponent GameObjectComponent { get; private set; }

        static void Main()
        {
            var time = Time.getMSTime();

            Version ver = MAssembly.GetName().Version;

            Log.Print("World Server", $"World of Warcraft (Realm Server/World Server)", ConsoleColor.Green);
            Log.Print("World Server", $"Supported WoW Client 1.2.1", ConsoleColor.Green);
            Log.Print("World Server", $"Version {ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}", ConsoleColor.Green);
            Log.Print("World Server", $"Running on .NET Framework Version {Environment.Version.Major}.{Environment.Version.Minor}.{Environment.Version.Build}", ConsoleColor.Green);

            var worldPoint = new IPEndPoint(IPAddress.Any, 1001);
            World = new WorldServer();

            if (World.Start(worldPoint))
            {
                Database = new DatabaseManager();

                // Handlers
                HandlerManager.Boot();
                PlayerManager.Boot();

                // World Spawn
                UnitComponent = new UnitComponent();

                Log.Print("World Server", $"Server is now listening at {worldPoint.Address}:{worldPoint.Port}", ConsoleColor.Green);
                Log.Print("World Server", $"Successfully started in {Time.getMSTimeDiff(time, Time.getMSTime()) / 1000}s", ConsoleColor.Green);
            }

            GC.Collect();
            Log.Print(LogType.Status, $"Total Memory: {Convert.ToSingle(GC.GetTotalMemory(false) / 1024 / 1024)}MB");

            while (true) Console.ReadLine();
        }

        public class WorldServer : Server
        {
            public static List<WorldSession> Sessions = new List<WorldSession>();
            public int ConnectionId = 0;

            public override Session GenerateSession(int connectionId, System.Net.Sockets.Socket connectionSocket)
            {
                connectionId++;
                WorldSession session = new WorldSession(connectionId, connectionSocket);
                Sessions.Add(session);

                return session;
            }

            public static void TransmitToAll(ServerPacket packet)
            {
                WorldServer.Sessions.FindAll(s => s.Character != null).ForEach(s => s.SendPacket(packet));
            }

            public static WorldSession GetSessionByPlayerName(string playerName)
            {
                return WorldServer.Sessions.Find(user => user.Character.Name.ToLower() == playerName.ToLower());
            }
        }
    }
}
