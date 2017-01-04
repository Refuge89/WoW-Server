using System;
using Framework.Network;
using World_Server.Sessions;

namespace World_Server.Handlers
{

    #region CMSG_ZONEUPDATE
    public sealed class CmsgZoneupdate : PacketReader
    {
        public uint ZoneId { get; private set; }

        public CmsgZoneupdate(byte[] data) : base(data)
        {
            ZoneId = ReadUInt32();
        }
    }
    #endregion

    public class MovementHandler
    {
        internal static void HandleZoneUpdate(WorldSession session, CmsgZoneupdate handler)
        {
            Console.WriteLine($"[ZoneUpdate] ID: {handler.ZoneId}");
        }
    }
}
