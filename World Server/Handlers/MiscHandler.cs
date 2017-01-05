using System;
using Framework.Contants;
using Framework.Network;
using World_Server.Sessions;

namespace World_Server.Handlers
{

    #region SMSG_QUERY_TIME_RESPONSE
    internal sealed class SmsgQueryTimeResponse : ServerPacket
    {
        public SmsgQueryTimeResponse() : base(WorldOpcodes.SMSG_QUERY_TIME_RESPONSE)
        {
            DateTime baseDate = new DateTime(1970, 1, 1);
            TimeSpan ts = DateTime.Now - baseDate;

            Write((uint)ts.TotalSeconds);
        }
    }
    #endregion

    public class MiscHandler
    {
        internal static void OnQueryTime(WorldSession session, byte[] data)
        {
            session.sendPacket(new SmsgQueryTimeResponse());
        }
    }
}
