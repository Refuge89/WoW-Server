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
    class Program
    {
        private static Assembly m_Assembly = Assembly.GetEntryAssembly();

        public static AuthServer Auth { get; private set; }

        public static DBManager Database;

        static void Main(string[] args)
        {
            var time = Time.getMSTime();

            Version ver = m_Assembly.GetName().Version;

            Log.Print("Auth Battle.NET", $"Version {ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}", ConsoleColor.Green);
            Log.Print("Auth Battle.NET", $"Running on .NET Framework Version {Environment.Version.Major}.{Environment.Version.Minor}.{Environment.Version.Build}", ConsoleColor.Green);

            var AuthPoint = new IPEndPoint(IPAddress.Any, 3724);

            Auth = new AuthServer();
            if (Auth.Start(AuthPoint))
            {
                // Iniciando Autenticador
                AuthManager.Boot();

                // Iniciando Database
                Database = new DBManager();
                Database.Boot();

                Log.Print("Auth Battle.NET", $"Server is now listening at {AuthPoint.Address}:{AuthPoint.Port}", ConsoleColor.Green);
                Log.Print("Auth Battle.NET", $"Successfully started in {Time.getMSTimeDiff(time, Time.getMSTime()) / 1000}s", ConsoleColor.Green);
            }

            while (true) Console.ReadLine();
        }

        public class AuthServer : Server
        {
            public override Session GenerateSession(int connectionID, Socket connectionSocket)
            {
                return new AuthSession(connectionID, connectionSocket);
            }
        }
    }
}
