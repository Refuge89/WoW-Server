using Framework.Contants;
using Framework.Database.Tables;
using Framework.Extensions;
using Framework.Network;

namespace World_Server.Handlers
{

    #region CMSG_PING
    public sealed class CmsgPing : PacketReader
    {
        public uint Ping { get; private set; }
        public uint Latency { get; private set; }

        public CmsgPing(byte[] data) : base(data)
        {
            Ping = ReadUInt32();
            Latency = ReadUInt32();
        }
    }
    #endregion

    #region SMSG_PONG
    public sealed class SmsgPong : ServerPacket
    {
        public SmsgPong(uint ping) : base(WorldOpcodes.SMSG_PONG)
        {
            Write((ulong)ping);
        }
    }
    #endregion

    #region SMSG_BINDPOINTUPDATE
    sealed class SmsgBindpointupdate : ServerPacket
    {
        public SmsgBindpointupdate(Character character) : base(WorldOpcodes.SMSG_BINDPOINTUPDATE)
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
    sealed class SmsgServerMessage : ServerPacket
    {
        public SmsgServerMessage(int code, string msg) : base(WorldOpcodes.SMSG_SERVER_MESSAGE)
        {
            // 1 = Server Shutdown in {}
            // 2 = Server Restar in
            Write(code);
            this.WriteCString(msg);
        }
    }
    #endregion

}
