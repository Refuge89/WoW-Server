using Framework.Contants;
using Framework.Crypt;
using Framework.Database.Tables;
using Framework.Helpers;
using Framework.Network;
using Framework.Sessions;
using System;
using System.IO;
using System.Net.Sockets;
using World_Server.Handlers;

namespace World_Server.Sessions
{
    public class WorldSession : Session
    {
        public VanillaCrypt crypt;
        public Character Character;
        public Users users;

        public WorldSession(int _connectionID, Socket _connectionSocket) : base(_connectionID, _connectionSocket)
        {
            sendPacket(WorldOpcodes.SMSG_AUTH_CHALLENGE, new byte[] { 0x33, 0x18, 0x34, 0xC8 });
        }

        public void sendPacket(ServerPacket packet)
        {
            sendPacket((int)packet.Opcode, packet.Packet);
        }

        public void sendPacket(WorldOpcodes opcode, byte[] data)
        {
            sendPacket((int)opcode, data);
        }

        public void sendPacket(int opcode, byte[] data)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            byte[] header = encode(data.Length, (int)opcode);

            writer.Write(header);
            writer.Write(data);

            Log.Print("World Server", $"Server -> Client [{(WorldOpcodes)opcode}] [0x{opcode.ToString("X")}]", ConsoleColor.Green);

            sendData(((MemoryStream)writer.BaseStream).ToArray());
        }

        public override void onPacket(byte[] data)
        {
            for (int index = 0; index < data.Length; index++)
            {
                byte[] headerData = new byte[6];
                Array.Copy(data, index, headerData, 0, 6);

                ushort length = 0;
                short opcode = 0;

                decode(headerData, out length, out opcode);

                WorldOpcodes code = (WorldOpcodes)opcode;

                byte[] packetDate = new byte[length];
                Array.Copy(data, index + 6, packetDate, 0, length - 4);
                Log.Print("World Server", $"Server <- Client [{code}] Packet Length: {length}", ConsoleColor.Green);

                WorldDataRouter.CallHandler(this, code, packetDate);

                index += 2 + (length - 1);
            }
        }

        private byte[] encode(int size, int opcode)
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


            if (crypt != null) header = crypt.encrypt(header);

            return header;
        }

        private void decode(byte[] header, out ushort length, out short opcode)
        {
            if (crypt != null)
            {
                crypt.decrypt(header, 6);
            }

            PacketReader reader = new PacketReader(header);

            if (crypt == null)
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
