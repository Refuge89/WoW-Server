using Framework.Crypt;
using Framework.Network;
using System.Net;
using Framework.Contants;
using Framework.Extensions;

namespace Auth_Server.Handlers
{
    public sealed class PcAuthLogonChallenge : PacketReader
    {
        public byte OptCode;
        public byte Error;
        public ushort Size;

        public string GameName;
        public string Version;
        public ushort Build;

        public string Platform;
        public string Os;
        public string Country;

        public uint TimeZone;
        public IPAddress Ip;
        public string Name;

        public PcAuthLogonChallenge(byte[] data) : base(data)
        {
            OptCode = ReadByte();
            Error = ReadByte();
            Size = ReadUInt16();

            GameName = ReadStringReversed(4);
            Version = ReadByte().ToString() + '.' + ReadByte() + '.' + ReadByte();

            Build = ReadUInt16();
            Platform = ReadStringReversed(4);
            Os = ReadStringReversed(4);
            Country = ReadStringReversed(4);

            TimeZone = ReadUInt32();
            Ip = ReadIpAddress();
            Name = ReadPascalString(1);
        }
    }

    sealed class PsAuthLogonChallange : ServerPacket
    {
        public PsAuthLogonChallange(SRP srp, AuthServerResult result) : base(AuthServerOpcode.AUTH_LOGON_CHALLENGE)
        {
            Write((byte)0);

            if (result == AuthServerResult.Success)
            {
                Write((byte)result);
                Write((byte)0); // unkown1 is set to 0 by all private servers.
                Write(srp.ServerEphemeral.ToProperByteArray()); // SRP6 server public ephemeral
                Write((byte)1); // generator_len is the length of the generator field following it. All servers (including ours) set this to 1.
                Write(srp.Generator.ToByteArray()); // All servers (including ours) hardcode this to 7
                Write((byte)32); // All servers (including ours) set this to 32.
                Write(srp.Modulus.ToProperByteArray().Pad(32)); // All servers (including ours) set this to 0x894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7
                Write(srp.Salt.ToProperByteArray().Pad(32)); // A salt is a randomly generated value used to strengthen a user's password against attacks where pre-computations are performed
                this.WriteNullByte(17); // unknown2 is set to 16 random bytes by all servers.

                // uint8   security_flags
                // uint8 security_bytes[]
            }
            else
            {
                Write((byte)0);
                Write((byte)result);
                this.WriteNullByte(6);
            }
        }
    }
}
