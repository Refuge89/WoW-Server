using Framework.Network;

namespace World_Server.Handlers.World
{
    public class PCPlayerLogin : PacketReader
    {
        public uint GUID { get; private set; }

        public PCPlayerLogin(byte[] data) : base(data)
        {
            GUID = ReadUInt32();
        }
    }
}
