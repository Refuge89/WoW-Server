using Framework.Contants;
using Framework.Crypt;
using Framework.Extensions;
using Framework.Network;

namespace Auth_Server.Handlers.Logon
{
    class PSAuthLogonChallange : ServerPacket
    {
        public PSAuthLogonChallange(SRP Srp, AuthenticationResult result) : base(AuthServerOpCode.AUTH_LOGON_CHALLENGE)
        {
            Write((byte) 0);

            if (result == AuthenticationResult.Success)
            {
                Write((byte) result);
                Write((byte) 0);
                Write(Srp.ServerEphemeral.ToProperByteArray());
                Write((byte) 1);
                Write(Srp.Generator.ToByteArray());
                Write((byte) 32);
                Write(Srp.Modulus.ToProperByteArray().Pad(32));
                Write(Srp.Salt.ToProperByteArray().Pad(32));
                this.WriteNullByte(17);
            }
            else
            {
                Write((byte) 0);
                Write((byte) result);
                this.WriteNullByte(6);
            }
        }
    }
}