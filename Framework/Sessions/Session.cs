using Framework.Helpers;
using Framework.Network;
using System;
using System.Net.Sockets;

namespace Framework.Sessions
{
    public abstract class Session
    {
        public const int BufferSize = 2048 * 2;
        public const int Timeout = 1000;

        public int ConnectionId { get; private set; }
        public Socket ConnectionSocket { get; private set; }
        public byte[] DataBuffer { get; private set; }

        public string ConnectionRemoteIp => ConnectionSocket.RemoteEndPoint.ToString();

        protected Session(int connectionId, Socket connectionSocket)
        {
            ConnectionId = connectionId;
            ConnectionSocket = connectionSocket;
            DataBuffer = new byte[BufferSize];

            try
            {
                ConnectionSocket.BeginReceive(DataBuffer, 0, DataBuffer.Length, SocketFlags.None, new AsyncCallback(DataArrival), null);
            }
            catch (SocketException)
            {
                Disconnect();
            }
        }

        public virtual void Disconnect()
        {
            try
            {
                Log.Print(LogType.Framework, "User Disconnected");
                ConnectionSocket.Shutdown(SocketShutdown.Both);
                ConnectionSocket.Close();
            }
            catch (Exception socketException)
            {
                Log.Print(LogType.Error, socketException.ToString());
            }
        }

        public abstract void OnPacket(byte[] data);

        public virtual void DataArrival(IAsyncResult asyncResult)
        {
            int bytesRecived = 0;

            try {
                bytesRecived = ConnectionSocket.EndReceive(asyncResult);
            }
            catch (Exception e)
            {
                Console.WriteLine("DataArrival Exception: " + e);
            }

            if (bytesRecived != 0)
            {
                byte[] data = new byte[bytesRecived];
                Array.Copy(DataBuffer, data, bytesRecived);

                OnPacket(data);

                try
                {
                    ConnectionSocket.BeginReceive(DataBuffer, 0, DataBuffer.Length, SocketFlags.None, new AsyncCallback(DataArrival), null);
                }
                catch (Exception e)
                {
                    Console.WriteLine("DataArrival Error: " +  e);
                }
            }
            else
            {
                Disconnect();
            }
        }

        public void sendData(ServerPacket packet)
        {
            sendData(packet.Packet);
        }

        public void sendData(byte[] send)
        {
            byte[] buffer = new byte[send.Length];
            Buffer.BlockCopy(send, 0, buffer, 0, send.Length);

            try
            {
                ConnectionSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, delegate (IAsyncResult result) { }, null);
            }
            catch (SocketException)
            {
                Disconnect();
            }
            catch (NullReferenceException)
            {
                Disconnect();
            }
            catch (ObjectDisposedException)
            {
                Disconnect();
            }
        }
    }
}
