using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using Framework.Contants;
using Framework.Crypt;
using Framework.Database.Tables;
using Framework.Network;
using Framework.Sessions;
using World_Server.Handlers;
using World_Server.Helpers;
using World_Server.Game.Entitys;

namespace World_Server.Sessions
{
    public class WorldSession : Session
    {
        public VanillaCrypt Crypt;
        public Character Character;
        public Users Users;
        public Character Target;

        public uint OutOfSyncDelay { get; set; }
        public PlayerEntity Entity;

        #region SMSG_AUTH_CHALLENGE
        sealed class SmsgAuthChallenge : ServerPacket
        {
            private readonly uint _serverSeed = (uint) new Random().Next(0, int.MaxValue);

            public SmsgAuthChallenge() : base(WorldOpcodes.SMSG_AUTH_CHALLENGE)
            {
                Write(1);
                Write(_serverSeed);
                Write(0);
                Write(0);
                Write(0);
                Write(0);
            }
        }
        #endregion

        public WorldSession(int connectionId, Socket connectionSocket) : base(connectionId, connectionSocket)
        {
            SendPacket(new SmsgAuthChallenge());
        }

        public void SendPacket(ServerPacket packet)
        {
            SendPacket(packet.Opcode, packet.Packet);
        }

        public void SendHexPacket(WorldOpcodes opcde, string hex)
        {
            string end = hex.Replace(" ", "").Replace("\n", "");

            byte[] data = StringToByteArray(end);

            SendPacket(opcde, data);
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public void SendPacket(WorldOpcodes opcode, byte[] data)
        {
            SendPacket((int)opcode, data);
        }

        public void SendPacket(int opcode, byte[] data)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            byte[] header = Encode(data.Length, opcode);

            writer.Write(header);
            writer.Write(data);

            sendData(((MemoryStream)writer.BaseStream).ToArray());
        }

        public override void OnPacket(byte[] data)
        {
            for (int index = 0; index < data.Length; index++)
            {
                byte[] headerData = new byte[6];
                Array.Copy(data, index, headerData, 0, 6);

                ushort length;
                short opcode;

                Decode(headerData, out length, out opcode);

                WorldOpcodes code = (WorldOpcodes)opcode;

                byte[] packetDate = new byte[length];
                Array.Copy(data, index + 6, packetDate, 0, length - 4);
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

            if (Crypt == null)
            {
                length = BitConverter.ToUInt16(new[] { header[1], header[0] }, 0);
                opcode = BitConverter.ToInt16(header, 2);
            }
            else
            {
                length = BitConverter.ToUInt16(new[] { header[1], header[0] }, 0);
                opcode = BitConverter.ToInt16(new[] { header[2], header[3] }, 0);
            }
        }

        internal void SendMessage(string v)
        {
            ChatHandler.SendSytemMessage(this, v);
        }
    }
}
