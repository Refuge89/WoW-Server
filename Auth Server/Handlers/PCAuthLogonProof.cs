using Framework.Network;

namespace Auth_Server.Handlers
{
    public class PCAuthLogonProof : PacketReader
    {
        public byte OptCode { get; private set; }
        public byte[] A { get; private set; }
        public byte[] M1 { get; private set; }
        public byte[] CRC_Hash { get; private set; }
        public byte nKeys { get; private set; }
        public byte unk { get; private set; }

        public PCAuthLogonProof(byte[] data) : base(data)
        {
            OptCode     = ReadByte();
            A           = ReadBytes(32);
            M1          = ReadBytes(20);

            CRC_Hash    = ReadBytes(20);
            nKeys       = ReadByte();
            unk         = ReadByte();
        }
    }
}
