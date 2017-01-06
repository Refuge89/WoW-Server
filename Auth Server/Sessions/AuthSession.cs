using Auth_Server.Handlers;
using Framework.Contants;
using Framework.Crypt;
using Framework.Helpers;
using Framework.Network;
using Framework.Sessions;
using System;
using System.IO;
using System.Net.Sockets;
using Auth_Server.Router;

namespace Auth_Server.Sessions
{
    public class AuthSession : Session
    {
        public SRP Srp;
        public string AccountName { get; set; }
        public byte[] SessionKey;

        public AuthSession(int connectionId, Socket connectionSocket) : base(connectionId, connectionSocket)
        {
        }

        public override void OnPacket(byte[] data)
        {
            short opcode = BitConverter.ToInt16(data, 0);
            Log.Print("Auth Battle.NET", $"Data Received: {opcode:X2} ({(AuthServerOpcode) opcode})",
                ConsoleColor.Green);

            AuthServerOpcode code = (AuthServerOpcode) opcode;

            AuthRouter.CallHandler(this, code, data);
        }

        public void SendPacket(ServerPacket packet)
        {
            SendPacket((byte) packet.Opcode, packet.Packet);
        }

        public void SendPacket(byte opcode, byte[] data)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write(opcode);
            writer.Write((ushort) data.Length);
            writer.Write(data);

            Log.Print("Auth Battle.NET",
                $"Con ({ConnectionId}) Server -> Client [" + (AuthServerOpcode) opcode + "] [0x" + opcode.ToString("X") +
                "]", ConsoleColor.Green);

            sendData(((MemoryStream) writer.BaseStream).ToArray());
        }

        public bool IsAuthenticated => Srp != null && Srp.ClientProof == Srp.GenerateClientProof();

    }
}