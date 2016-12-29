using System;
using Framework.Database.Tables;
using Framework.Network;
using World_Server.Game.Entitys;
using System.Collections.Generic;
using Framework.Contants;
using System.IO;
using Framework.Contants.Game;
using Framework.Extensions;
using Framework.Helpers;

namespace World_Server.Handlers.Updates
{
    public class PSUpdateObject : ServerPacket
    {
        public PSUpdateObject(List<byte[]> blocks, int hasTansport = 0) : base(WorldOpcodes.SMSG_UPDATE_OBJECT)
        {
            //Console.WriteLine(blocks.Count);
            Write((uint)blocks.Count);
            Write((byte)hasTansport); // Has transport
            Write(1);

            // Write each block
            //blocks.ForEach(b => Write(b));
        }

        public PSUpdateObject(List<UpdateBlock> blocks, int hasTansport = 0) : base(WorldOpcodes.SMSG_UPDATE_OBJECT)
        {
            Write((uint)blocks.Count);
            Write((byte)hasTansport); // Has transport

            // Write each block
            blocks.ForEach(block => Write(block.Data));
        }

        public static PSUpdateObject CreateOwnCharacterUpdate(Character character, out PlayerEntity entity)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            writer.Write((byte)ObjectUpdateType.UPDATETYPE_CREATE_OBJECT2);

            writer.WritePackedUInt64((ulong)character.Id);

            writer.Write((byte)TypeID.TYPEID_PLAYER);

            ObjectUpdateFlag updateFlags = ObjectUpdateFlag.UPDATEFLAG_ALL |
                          ObjectUpdateFlag.UPDATEFLAG_HAS_POSITION |
                          ObjectUpdateFlag.UPDATEFLAG_LIVING |
                          ObjectUpdateFlag.UPDATEFLAG_SELF;

            writer.Write((byte)updateFlags);

            writer.Write((UInt32)MovementFlags.MOVEFLAG_NONE);
            writer.Write((UInt32)Environment.TickCount); // Time?

            writer.Write((float)character.MapX);
            writer.Write((float)character.MapY);
            writer.Write((float)character.MapZ);
            writer.Write((float)character.MapRotation); // R

            // Movement speeds
            writer.Write((float)0);     // ????

            writer.Write((float)2.5f);  // MOVE_WALK
            writer.Write((float)7 * 3);     // MOVE_RUN
            writer.Write((float)4.5f);  // MOVE_RUN_BACK
            writer.Write((float)4.72f); // MOVE_SWIM
            writer.Write((float)2.5f);  // MOVE_SWIM_BACK
            writer.Write((float)3.14f); // MOVE_TURN_RATE

            writer.Write(0x1); // Unkown...

            entity = new PlayerEntity(character);
            entity.ObjectGUID = new ObjectGUID((ulong)character.Id);

            entity.WriteUpdateFields(writer);

            return new PSUpdateObject(new List<byte[]> { (writer.BaseStream as MemoryStream).ToArray() });
        }
    }
}
