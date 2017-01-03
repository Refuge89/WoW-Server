using Framework.Contants;
using Framework.Crypt;
using Framework.Database.Tables;
using Framework.Helpers;
using Framework.Network;
using Framework.Sessions;
using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using World_Server.Handlers;
using World_Server.Helpers;

namespace World_Server.Sessions
{
    public class WorldSession : Session
    {
        public VanillaCrypt Crypt;
        public Character Character;
        public Users Users;

        public WorldSession(int connectionId, Socket connectionSocket) : base(connectionId, connectionSocket)
        {
            //sendPacket(WorldOpcodes.SMSG_AUTH_CHALLENGE, new byte[] { 0x33, 0x18, 0x34, 0xC8 });
            sendPacket(WorldOpcodes.SMSG_AUTH_CHALLENGE, new byte[] { 0x31, 0x18, 0x34, 0xC8 });
        }

        public void sendPacket(ServerPacket packet)
        {
            sendPacket((int)packet.Opcode, packet.Packet);
            return;
        }

        public void sendHexPacket(WorldOpcodes opcde, string hex)
        {
            string end = hex.Replace(" ", "").Replace("\n", "");

            byte[] data = StringToByteArray(end);

            sendPacket(opcde, data);
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }


        public void sendPacket(WorldOpcodes opcode, byte[] data)
        {
            sendPacket((int)opcode, data);
        }

        public void sendPacket(int opcode, byte[] data)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            byte[] header = Encode(data.Length, (int)opcode);

            writer.Write(header);
            writer.Write(data);

            Log.Print("World Server", $"Server -> Client [{(WorldOpcodes)opcode}] [0x{opcode.ToString("X")}]", ConsoleColor.Green);

            sendData(((MemoryStream)writer.BaseStream).ToArray());
        }

        public override void OnPacket(byte[] data)
        {
            for (int index = 0; index < data.Length; index++)
            {
                byte[] headerData = new byte[6];
                Array.Copy(data, index, headerData, 0, 6);

                ushort length = 0;
                short opcode = 0;

                Decode(headerData, out length, out opcode);

                WorldOpcodes code = (WorldOpcodes)opcode;

                byte[] packetDate = new byte[length];
                Array.Copy(data, index + 6, packetDate, 0, length - 4);
                Log.Print("World Server", $"Server <- Client [{code}] Packet Length: {length}", ConsoleColor.Green);

                WorldDataRouter.CallHandler(this, code, packetDate);

                index += 2 + (length - 1);
            }
        }

        private byte[] Encode(int size, int opcode)
        {
            int index = 0;
            int newSize = size + 2;
            byte[] header = new byte[4];
            if (newSize > 0x7FFF)
                header[index++] = (byte)(0x80 | (0xFF & (newSize >> 16)));

            header[index++] = (byte)(0xFF & (newSize >> 8));
            header[index++] = (byte)(0xFF & newSize);
            header[index++] = (byte)(0xFF & opcode);
            header[index] = (byte)(0xFF & (opcode >> 8));

            if (Crypt != null) header = Crypt.Encrypt(header);

            return header;
        }

        private void Decode(byte[] header, out ushort length, out short opcode)
        {
            Crypt?.Decrypt(header, 6);

            //PacketReader reader = new PacketReader(header);

            if (Crypt == null)
            {
                length = BitConverter.ToUInt16(new byte[] { header[1], header[0] }, 0);
                opcode = BitConverter.ToInt16(header, 2);
            }
            else
            {
                length = BitConverter.ToUInt16(new byte[] { header[1], header[0] }, 0);
                opcode = BitConverter.ToInt16(new byte[] { header[2], header[3] }, 0);
            }
        }
    }
}
