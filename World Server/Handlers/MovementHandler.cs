using System;
using Framework.Network;
using World_Server.Sessions;
using System.IO;
using Framework.Contants.Character;
using Framework.Contants;
using Framework.Database.Tables;
using World_Server.Handlers.World;
using World_Server.Helpers;

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

    #region CMSG_MOVE_TIME_SKIPPED
    public sealed class CmsgMoveTimeSkipped : PacketReader
    {
        public ulong GUID;

        public uint OutOfSyncDelay;

        public CmsgMoveTimeSkipped(byte[] data) : base(data)
        {
            GUID = this.ReadPackedGuid(this);
            OutOfSyncDelay = ReadUInt32();
        }

        public ulong ReadPackedGuid(BinaryReader reader)
        {
            byte mask = reader.ReadByte();

            if (mask == 0)
            {
                return 0;
            }

            ulong res = 0;

            int i = 0;
            while (i < 8)
            {
                if ((mask & 1 << i) != 0)
                {
                    res += (ulong)reader.ReadByte() << (i * 8);
                }

                i++;
            }

            return res;
        }
    }
    #endregion

    #region MSG MOVE INFO
    public class MsgMoveInfo : PacketReader
    {
        public MovementFlags moveFlags { get; set; }
        public float MapX { get; set; }
        public float MapY { get; set; }
        public float MapZ { get; set; }
        public float MapR { get; set; }       
        public uint time { get; set; }

        public MsgMoveInfo(byte[] data) : base(data)
        {
            moveFlags = (MovementFlags)this.ReadUInt32();
            time = ReadUInt32();
            MapX = ReadSingle();
            MapY = ReadSingle();
            MapZ = ReadSingle();
            MapR = ReadSingle();
        }
    }
    #endregion
    
    public class MovementHandler
    {
        public static WorldOpcodes opcode { get; private set; }

        internal static void HandleZoneUpdate(WorldSession session, CmsgZoneupdate handler)
        {
            session.Character.MapZone = (int)handler.ZoneId;

            Console.WriteLine($"[ZoneUpdate] ID: {handler.ZoneId}");
        }

        internal static void OnMoveTimeSkipped(WorldSession session, CmsgMoveTimeSkipped handler)
        {
            session.OutOfSyncDelay = handler.OutOfSyncDelay;
        }
        
        internal static ProcessWorldPacketCallbackTypes<MsgMoveInfo> GenerateResponse(WorldOpcodes code)
        {
            return delegate (WorldSession session, MsgMoveInfo handler) { TransmitMovement(session, handler, code); };
        }

        private static void TransmitMovement(WorldSession session, MsgMoveInfo handler, WorldOpcodes code)
        {
            session.Character.MapX = handler.MapX;
            session.Character.MapY = handler.MapY;
            session.Character.MapZ = handler.MapZ;
            session.Character.MapRotation = handler.MapR;
            Program.Database.UpdateMovement(session.Character);
            Program.WorldServer.Sessions.FindAll(s => s != session)
                .ForEach(s => s.sendPacket(new PSMovement(session, handler, code)));
        }
    }

    internal class PSMovement : ServerPacket
    {
        public PSMovement(WorldSession session, MsgMoveInfo handler , WorldOpcodes opcode) : base(opcode)
        {
            Console.WriteLine(handler.moveFlags);

            var packedGUID = PSUpdateObject.GenerateGuidBytes((ulong)session.Character.Id);
            PSUpdateObject.WriteBytes(this, packedGUID);

            if (handler.moveFlags == MovementFlags.MOVEFLAG_NONE)
            {
                Write((uint)handler.moveFlags);
                Write((uint)session.OutOfSyncDelay);
                Write(handler.MapX);
                Write(handler.MapY);
                Write(handler.MapZ);
                Write(handler.MapR);
                Write((uint)0); // ?
            }
            else
            {
                Write((uint)handler.moveFlags);
                Write((uint)1);
                Write(handler.MapX);
                Write(handler.MapY);
                Write(handler.MapZ);
                Write(handler.MapR);
                Write((uint)0); // ?
            }

            //System.Threading.Thread.Sleep(1000);
        }
    }
}
