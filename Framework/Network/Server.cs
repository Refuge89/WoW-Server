using Framework.Helpers;
using Framework.Sessions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Framework.Network
{
    public class Server
    {
        private Socket socketHandler;

        public Dictionary<int, Session> activeConnections;

        public bool Start(IPEndPoint authPoint)
        {
            activeConnections = new Dictionary<int, Session>();
            socketHandler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socketHandler.Bind(new IPEndPoint(authPoint.Address, authPoint.Port));
                socketHandler.Listen(25);
                socketHandler.BeginAccept(new AsyncCallback(ConnectionRequest), socketHandler);

                return true;
            }
            catch (Exception e)
            {
                Log.Print(LogType.Error, e.ToString());

                return false;
            }
        }

        private void ConnectionRequest(IAsyncResult _asyncResult)
        {
            Socket connectionSocket = ((Socket)_asyncResult.AsyncState).EndAccept(_asyncResult);

            int connectionID = GetFreeID();

            activeConnections.Add(connectionID, GenerateSession(connectionID, connectionSocket));

            socketHandler.BeginAccept(new AsyncCallback(ConnectionRequest), socketHandler);
        }

        private int GetFreeID()
        {
            for (int i = 0; i < 3500; i++)
            {
                if (!activeConnections.ContainsKey(i)) return i;
            }

            return -1;
        }

        public virtual Session GenerateSession(int connectionID, Socket connectionSocket)
        {
            return null;
        }

        public void FreeConnectionID(int _connectionID)
        {
            if (activeConnections.ContainsKey(_connectionID)) activeConnections.Remove(_connectionID);
        }
    }
}
