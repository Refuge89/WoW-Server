using Framework.Contants;
using System.IO;

namespace Framework.Network
{
    public class ServerPacket : BinaryWriter
    {
        public int Opcode;

        public ServerPacket(int opcode) : base(new MemoryStream())
        {
            Opcode = opcode;
        }

        public ServerPacket(WorldOpcodes worldOpcode) : this((int)worldOpcode) { }

        public ServerPacket(AuthServerOpcode opcode) : this((byte)opcode) { }

        public byte[] Packet
        {
            get { return (BaseStream as MemoryStream).ToArray(); }
        }
    }
}
