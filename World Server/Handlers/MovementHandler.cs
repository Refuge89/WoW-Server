﻿using System;
using Framework.Network;
using World_Server.Sessions;
using System.IO;
using Framework.Contants.Character;
using Framework.Contants;
using World_Server.Helpers;
using World_Server.Game;
using World_Server.Managers;

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
        public ulong Guid;

        public uint OutOfSyncDelay;

        public CmsgMoveTimeSkipped(byte[] data) : base(data)
        {
            Guid = this.ReadPackedGuid(this);
            OutOfSyncDelay = ReadUInt32();
        }

        public ulong ReadPackedGuid(BinaryReader reader)
        {
            byte mask = reader.ReadByte();

            if (mask == 0)
                return 0;

            ulong res = 0;

            int i = 0;
            while (i < 8)
            {
                if ((mask & 1 << i) != 0)
                    res += (ulong)reader.ReadByte() << (i * 8);

                i++;
            }

            return res;
        }
    }
    #endregion

    #region MSG MOVE INFO
    public sealed class MsgMoveInfo : PacketReader
    {
        public MovementFlags MoveFlags { get; set; }
        public float MapX { get; set; }
        public float MapY { get; set; }
        public float MapZ { get; set; }
        public float MapR { get; set; }       
        public uint Time { get; set; }

        public MsgMoveInfo(byte[] data) : base(data)
        {
            MoveFlags = (MovementFlags)ReadUInt32();
            Time = ReadUInt32();
            MapX = ReadSingle();
            MapY = ReadSingle();
            MapZ = ReadSingle();
            MapR = ReadSingle();
        }
    }
    #endregion

    #region PS Movement handler
    internal sealed class PsMovement : ServerPacket
    {
        public PsMovement(WorldSession session, MsgMoveInfo handler, WorldOpcodes opcode) : base(opcode)
        {
            byte[] packedGuid = UpdateObject.GenerateGuidBytes((ulong)session.Character.Id);
            UpdateObject.WriteBytes(this, packedGuid);
            UpdateObject.WriteBytes(this, (handler.BaseStream as MemoryStream)?.ToArray());

            // We then overwrite the original moveTime (sent from the client) with ours
            ((MemoryStream) BaseStream).Position = 4 + packedGuid.Length;
            UpdateObject.WriteBytes(this, BitConverter.GetBytes((uint)Environment.TickCount));
        }
    }
    #endregion

    public class MovementHandler
    {
        public static WorldOpcodes Opcode { get; set; }

        internal static void HandleZoneUpdate(WorldSession session, CmsgZoneupdate handler)
        {
            session.Character.MapZone = (int)handler.ZoneId;
            session.SendMessage($"[ZoneUpdate] ID: {handler.ZoneId}");
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

            Main.Database.UpdateMovement(session.Character);

            Main.WorldServer.Sessions.FindAll(s => s != session)
                .ForEach(s => s.SendPacket(new PsMovement(session, handler, code)));
        }
    }
}
