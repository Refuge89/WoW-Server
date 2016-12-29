using Framework.Network;

namespace World_Server.Handlers.Char
{
    class PCCharDelete : PacketReader
    {
        public int Id { get; private set; }

        public PCCharDelete(byte[] data) : base(data)
        {
            Id = (int)ReadUInt64();
        }
    }
}
