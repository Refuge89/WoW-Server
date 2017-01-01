using Framework.Contants;
using Framework.Crypt;
using Framework.Extensions;
using Framework.Network;

namespace Auth_Server.Handlers
{
    public sealed class PcAuthLogonProof : PacketReader
    {
        public byte OptCode { get; private set; }
        public byte[] A { get; private set; }
        public byte[] M1 { get; private set; }
        public byte[] CrcHash { get; private set; }
        public byte NKeys { get; private set; }
        public byte Unk { get; private set; }

        public PcAuthLogonProof(byte[] data) : base(data)
        {
            OptCode = ReadByte();
            A = ReadBytes(32);
            M1 = ReadBytes(20);

            CrcHash = ReadBytes(20);
            NKeys = ReadByte();
            Unk = ReadByte();
        }
    }

    sealed class PsAuthLogonProof : ServerPacket
    {
        public PsAuthLogonProof(SRP srp, AuthServerResult result) : base(AuthServerOpcode.AUTH_LOGON_PROOF)
        {
            Write((byte)1);
            Write((byte)result);
            Write(srp.ServerProof.ToByteArray().Pad(20));
            this.WriteNullByte(4);
        }
    }
}
