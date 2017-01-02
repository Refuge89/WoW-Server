using Auth_Server.Managers;
using Auth_Server.Sessions;
using Framework.Database;
using Framework.Helpers;
using Framework.Network;
using Framework.Sessions;
using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace Auth_Server
{
    internal class Program
    {
        private static readonly Assembly MAssembly = Assembly.GetEntryAssembly();

        public static AuthServer Auth { get; private set; }

        public static DBManager Database;
        public static DatabaseManager DatabaseManager;

        private static void Main()
        {
            var time = Time.getMSTime();

            Version ver = MAssembly.GetName().Version;

            Log.Print("Auth Battle.NET", $"Version {ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}",
                ConsoleColor.Green);
            Log.Print("Auth Battle.NET",
                $"Running on .NET Framework Version {Environment.Version.Major}.{Environment.Version.Minor}.{Environment.Version.Build}",
                ConsoleColor.Green);

            var authPoint = new IPEndPoint(IPAddress.Any, port: 3724);

            Auth = new AuthServer();
            if (Auth.Start(authPoint))
            {
                // Iniciando Autenticador
                AuthManager.Boot();

                // Iniciando Database
                Database = new DBManager();

                // Insere Registros primarios
                Database.Boot();

                // Database do Auth
                DatabaseManager = new DatabaseManager();

                Log.Print("Auth Battle.NET", $"Server is now listening at {authPoint.Address}:{authPoint.Port}",
                    ConsoleColor.Green);
                Log.Print("Auth Battle.NET",
                    $"Successfully started in {Time.getMSTimeDiff(time, Time.getMSTime()) / 1000}s", ConsoleColor.Green);
            }

            while (true) Console.ReadLine();
        }

        public class AuthServer : Server
        {
            public override Session GenerateSession(int connectionId, Socket connectionSocket)
            {
                return new AuthSession(connectionId, connectionSocket);
            }
        }
    }
}