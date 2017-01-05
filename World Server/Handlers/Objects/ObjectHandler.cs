﻿using Framework.Contants;
using Framework.Contants.Character;
using Framework.Contants.Game;
using Framework.Database.Tables;
using Framework.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using World_Server.Game.Entitys;
using World_Server.Game.Update;
using World_Server.Helpers;

namespace World_Server.Handlers.Objects
{
    public sealed class ObjectHandler : ServerPacket
    {
        public ObjectHandler(List<byte[]> blocks) : base(WorldOpcodes.SMSG_UPDATE_OBJECT)
        {
            Write((uint)blocks.Count());
            Write((byte)0);
            blocks.ForEach(b => Write(b));
        }

        public static ObjectHandler CreateOwnCharacterUpdate(Character character, out PlayerEntity entity) //CmsgPlayerLogin packet)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte)ObjectUpdateType.UPDATETYPE_CREATE_OBJECT2);

            byte[] guidBytes = ObjectHelper.GenerateGuidBytes((ulong)character.Id);
            ObjectHelper.WriteBytes(writer, guidBytes, guidBytes.Length);

            writer.Write((byte)TypeID.TYPEID_PLAYER);

            ObjectFlags updateFlags = ObjectFlags.UPDATEFLAG_ALL |
                                      ObjectFlags.UPDATEFLAG_HAS_POSITION |
                                      ObjectFlags.UPDATEFLAG_LIVING |
                                      ObjectFlags.UPDATEFLAG_SELF;

            writer.Write((byte)updateFlags);

            writer.Write((UInt32)MovementFlags.MOVEFLAG_NONE);
            writer.Write((UInt32)Environment.TickCount);

            // Position
            writer.Write(character.MapX);
            writer.Write(character.MapY);
            writer.Write(character.MapZ);
            writer.Write(character.MapRotation);

            // Movement speeds
            writer.Write(0f);     // ????
            writer.Write(2.5f);  // MOVE_WALK
            writer.Write(7f * 10);     // MOVE_RUN
            writer.Write(4.5f);  // MOVE_RUN_BACK
            writer.Write(4.72f); // MOVE_SWIM
            writer.Write(2.5f);  // MOVE_SWIM_BACK
            writer.Write(3.14f); // MOVE_TURN_RATE

            writer.Write(0x1); // Unkown...

            //new PlayerEntity(character).WriteUpdateFields(writer);

            entity = new PlayerEntity(character);
            entity.GUID = new ObjectGuid((ulong)character.Id);
            entity.WriteUpdateFields(writer);

            return new ObjectHandler(new List<byte[]> { (writer.BaseStream as MemoryStream)?.ToArray() });
        }
    }
}
