using Framework.Crypt;
using Framework.Network;
using System;
using System.Net;
using Framework.Contants;
using Framework.Extensions;

namespace Auth_Server.Handlers
{
    public sealed class PcAuthLogonChallenge : PacketReader
    {
        public byte OptCode;
        public byte Error;
        public UInt16 Size;

        public string GameName;
        public string Version;
        public UInt16 Build;

        public string Platform;
        public string Os;
        public string Country;

        public UInt32 TimeZone;
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
                Write((byte)0);
                Write(srp.ServerEphemeral.ToProperByteArray());
                Write((byte)1);
                Write(srp.Generator.ToByteArray());
                Write((byte)32);
                Write(srp.Modulus.ToProperByteArray().Pad(32));
                Write(srp.Salt.ToProperByteArray().Pad(32));
                this.WriteNullByte(17);
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
