using Framework.Contants;
using Framework.Crypt;
using Framework.Extensions;
using Framework.Network;

namespace Auth_Server.Handlers.Logon
{
    class PSAuthLogonProof : ServerPacket
    {
        public PSAuthLogonProof(SRP Srp, AuthenticationResult result) : base(AuthServerOpCode.AUTH_LOGON_PROOF)
        {
            Write((byte) 1);
            Write((byte) result);
            Write(Srp.ServerProof.ToByteArray().Pad(20));
            this.WriteNullByte(4);
        }
    }
}