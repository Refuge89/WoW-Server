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

        public Dictionary<int, Session> activeConnections { get; protected set; }

        public int ConnectionsCount
        {
            get
            {
                return this.activeConnections.Count;
            }
        }


        public bool Start(IPEndPoint authPoint)
        {
            activeConnections = new Dictionary<int, Session>();
            socketHandler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketHandler.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            socketHandler.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);

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

        private void ConnectionRequest(IAsyncResult asyncResult)
        {
            Socket connectionSocket = ((Socket)asyncResult.AsyncState).EndAccept(asyncResult);

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

            throw new Exception("Couldn't find free ID");
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
