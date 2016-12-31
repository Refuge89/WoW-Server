using Framework.Network;
using System;

namespace Auth_Server.Handlers.Reconnect
{
    public class PCAuthReconnectChallenge : PacketReader
    {
        public string OptCode;
        public UInt16 ProofData;
        public byte[] ClientProof;

        public PCAuthReconnectChallenge(byte[] data) : base(data)
        {
            OptCode = ReadByte().ToString(); // 2
            ProofData = ReadUInt16(); // 8706
            ClientProof = ReadBytes(20); // byte
        }
    }
}