using System;
using Framework.Network;
using World_Server.Sessions;
using System.IO;
using Framework.Contants.Character;
using Framework.Extensions;
using System.Linq;
using World_Server.Game.Entitys;
using Framework.Contants;
using Framework.Helpers;
using static World_Server.Program;
using World_Server.Helpers;
using Framework.Database.Tables;
using World_Server.Handlers.World;

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
            Console.WriteLine($"[ZoneUpdate] ID: {handler.ZoneId}");
        }

        internal static void OnMoveTimeSkipped(WorldSession session, CmsgMoveTimeSkipped handler)
        {
            session.OutOfSyncDelay = handler.OutOfSyncDelay;
        }

        internal static void HandleMovementStatus(WorldSession session, MsgMoveInfo handler)
        {
            if (session.Character == null)
                return;

            //Console.WriteLine($"Opcode [{handler.moveFlags}] Timer [{handler.time}] Postion X {handler.MapX} @ Y {handler.MapY} @ Z {handler.MapZ} @ R {handler.MapR} [{session.Character.Name}]");

            session.Character.MapX = handler.MapX;
            session.Character.MapY = handler.MapY;
            session.Character.MapZ = handler.MapZ;
            session.Character.MapRotation = handler.MapR;
            Database.UpdateMovement(session.Character);

            // Transmitindo a todos os players
            TransmitMovement(session, handler);
        }

        private static void TransmitMovement(WorldSession session, MsgMoveInfo handler)
        {
            if(handler.moveFlags == MovementFlags.MOVEFLAG_FALLING)
            {
                opcode = WorldOpcodes.MSG_MOVE_JUMP;
            } else
            {
                opcode = WorldOpcodes.MSG_MOVE_HEARTBEAT;
            }
            WorldServer.Sessions.FindAll(s => s != session)
                .ForEach(s => s.sendPacket(new PSMovement(session.Character, handler, opcode)));
        }
    }

    internal class PSMovement : ServerPacket
    {
        public PSMovement(Character character, MsgMoveInfo handler , WorldOpcodes opcode) : base(opcode)
        {
            var packedGUID = PSUpdateObject.GenerateGuidBytes((ulong)character.Id);
            PSUpdateObject.WriteBytes(this, packedGUID);
            Write((uint)handler.moveFlags);
            Write((uint)1);
            Write(handler.MapX);
            Write(handler.MapY);
            Write(handler.MapZ);
            Write(handler.MapR);
            Write((uint)0); // ?
        }
    }
}
