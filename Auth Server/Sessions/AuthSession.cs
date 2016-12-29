using Auth_Server.Handlers;
using Framework.Contants;
using Framework.Crypt;
using Framework.Helpers;
using Framework.Network;
using Framework.Sessions;
using System;
using System.IO;
using System.Net.Sockets;

namespace Auth_Server.Sessions
{
    public class AuthSession : Session
    {
        public SRP Srp;
        public String accountName { get; set; }
        public byte[] SessionKey;

        public AuthSession(int _connectionID, Socket _connectionSocket) : base(_connectionID, _connectionSocket)
        {
        }

        public override void onPacket(byte[] data)
        {
            short opcode = BitConverter.ToInt16(data, 0);
            Log.Print("Auth Battle.NET", $"Data Received: {opcode.ToString("X2")} ({((AuthServerOpCode)opcode)})", ConsoleColor.Green);

            AuthServerOpCode code = (AuthServerOpCode)opcode;

            AuthDataRouter.CallHandler(this, code, data);
        }

        public void sendPacket(ServerPacket packet)
        {
            sendPacket((byte)packet.Opcode, packet.Packet);
        }

        public void sendPacket(byte opcode, byte[] data)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write(opcode);
            writer.Write((ushort)data.Length);
            writer.Write(data);

            Log.Print("Auth Battle.NET", $"Con ({connectionID}) Server -> Client [" + (AuthServerOpCode)opcode + "] [0x" + opcode.ToString("X") + "]", ConsoleColor.Green);

            sendData(((MemoryStream)writer.BaseStream).ToArray());
        }


        public bool IsAuthenticated
        {
            get
            {
                return Srp != null && Srp.ClientProof == Srp.GenerateClientProof();
            }
        }
    }
}
