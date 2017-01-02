using Framework.Contants;
using Framework.Database.Tables;
using Framework.Extensions;
using Framework.Network;

namespace World_Server.Handlers
{

    #region CMSG_PING
    public class CMSG_PING : PacketReader
    {
        public uint Ping { get; private set; }
        public uint Latency { get; private set; }

        public CMSG_PING(byte[] data) : base(data)
        {
            Ping = ReadUInt32();
            Latency = ReadUInt32();
        }
    }
    #endregion

    #region SMSG_PONG
    public class SMSG_PONG : ServerPacket
    {
        public SMSG_PONG(uint ping) : base(WorldOpcodes.SMSG_PONG)
        {
            Write((ulong)ping);
        }
    }
    #endregion

    #region SMSG_BINDPOINTUPDATE
    class SMSG_BINDPOINTUPDATE : ServerPacket
    {
        public SMSG_BINDPOINTUPDATE(Character character) : base(WorldOpcodes.SMSG_BINDPOINTUPDATE)
        {
            Write(character.MapX);
            Write(character.MapY);
            Write(character.MapZ);
            Write((uint)character.MapID);
            Write((short)character.MapZone); // Zone ID
        }
    }
    #endregion

    #region SMSG_SERVER_MESSAGE
    class SMSG_SERVER_MESSAGE : ServerPacket
    {
        public SMSG_SERVER_MESSAGE(int code, string msg) : base(WorldOpcodes.SMSG_SERVER_MESSAGE)
        {
            // 1 = Server Shutdown in {}
            // 2 = Server Restar in
            Write(code);
            this.WriteCString(msg);
        }
    }
    #endregion

}
