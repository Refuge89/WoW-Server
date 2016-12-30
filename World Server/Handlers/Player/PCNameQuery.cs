using Framework.Network;

namespace World_Server.Handlers.Player
{
    public class PCNameQuery : PacketReader
    {
        public uint GUID { get; private set; }

        public PCNameQuery(byte[] data) : base(data)
        {
            GUID = ReadUInt32();
        }
    }
}
