using System;
using Framework.Network;
using World_Server.Sessions;

namespace World_Server.Handlers
{
    #region CMSG_ITEM_QUERY_SINGLE
    public sealed class CmsgItemQuerySingle : PacketReader
    {
        public uint Entry { get; private set; }

        public CmsgItemQuerySingle(byte[] data) : base(data)
        {
            Entry = ReadUInt32();
        }
    }
    #endregion


    class ItemHandler
    {
        internal static void OnItemQuerySingle(WorldSession session, CmsgItemQuerySingle handler)
        {
            Console.WriteLine($"OnItemQuerySingle {handler.Entry}");
        }
    }
}