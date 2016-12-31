using Framework.Network;

namespace World_Server.Handlers.Communication
{
    public class PCEmote : PacketReader
    {
        public PCEmote(byte[] data) : base(data)
        {
            TextID = ReadUInt32();
            EmoteID = ReadUInt32();
            GUID = ReadInt32();
        }

        public uint TextID { get; private set; }
        public uint EmoteID { get; private set; }
        public int GUID { get; private set; }
    }
}