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
            /*
            The rest of the packet is different depending on whether an error occured (error != 0) or not. If there was an error, 
            two fields unk1 and unk2 are sent, with value 3 and 0, respectively. The use of those fields is unknown. 
            These fields are not sent on Vanilla client, except maybe for 1.12.3 Chinese client 
            (according to a condition in the Mangos server -- I didn't verify as I don't have a Chinese client).

            error != 0 && !vanilla
                uint8   unk1
                uint8   unk2
            */

            if (result == AuthServerResult.Success /* check if is Vanilla */)
            {
                Write(srp.ServerProof.ToByteArray().Pad(20));
                this.WriteNullByte(4);
            }

            /*
            other
                uint32  account_flags
                uint32  survey_id
                uint16  unk_flags

                account_flags
                    1: ACCOUNT_FLAG_GM: game master account
                    8: ACCOUNT_FLAG_TRIAL: for trial accounts
                    0x00800000: ACCOUNT_FLAG_PROPASS: enables the arena tournament for the account
            */
            
        }
    }
}
