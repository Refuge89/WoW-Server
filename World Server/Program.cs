using Framework.Database;
using Framework.Helpers;
using Framework.Network;
using Framework.Sessions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using World_Server.Managers;
using World_Server.Sessions;

namespace World_Server
{
    class Program
    {
        private static Assembly m_Assembly = Assembly.GetEntryAssembly();

        public static WorldServer World { get; private set; }

        public static DataBaseManager Database;
        public static DBManager DBManager;

        static void Main(string[] args)
        {
            var time = Time.getMSTime();

            Version ver = m_Assembly.GetName().Version;

            Log.Print("World Server", $"Version {ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}", ConsoleColor.Green);
            Log.Print("World Server", $"Running on .NET Framework Version {Environment.Version.Major}.{Environment.Version.Minor}.{Environment.Version.Build}", ConsoleColor.Green);

            var WorldPoint = new IPEndPoint(IPAddress.Any, 1001);

            World = new WorldServer();
            if (World.Start(WorldPoint))
            {
                // Iniciando Database
                Database = new DataBaseManager();
                DBManager = new DBManager();

                // Iniciando Sequencia
                RealmManager.Boot();
                CharacterManager.Boot();
                ChatManager.Boot();
                

                Log.Print("World Server", $"Server is now listening at {WorldPoint.Address}:{WorldPoint.Port}", ConsoleColor.Green);
                Log.Print("World Server", $"Successfully started in {Time.getMSTimeDiff(time, Time.getMSTime()) / 1000}s", ConsoleColor.Green);
            }

            while (true) Console.ReadLine();
        }

        public class WorldServer : Server
        {
            public static List<WorldSession> Sessions = new List<WorldSession>();
            public int connectionID = 0;

            public override Session GenerateSession(int connectionID, System.Net.Sockets.Socket connectionSocket)
            {
                connectionID++;
                WorldSession session = new WorldSession(connectionID, connectionSocket);
                Sessions.Add(session);

                return session;
            }

            public static void TransmitToAll(ServerPacket packet)
            {
                WorldServer.Sessions.FindAll(s => s.Character != null).ForEach(s => s.sendPacket(packet));
            }

            public static WorldSession GetSessionByPlayerName(string playerName)
            {
                return WorldServer.Sessions.Find(user => user.Character.Name.ToLower() == playerName.ToLower());
            }
        }
    }
}
