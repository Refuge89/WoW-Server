using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Framework.Contants;
using Framework.Contants.Character;
using Framework.Contants.Game;
using Framework.Database.Tables;
using Framework.Database.XML;
using Framework.Extensions;
using Framework.Network;
using World_Server.Game.Entitys;
using World_Server.Game.Update;
using World_Server.Managers;

namespace World_Server.Game
{
    internal sealed class UpdateObject : ServerPacket
    {
        public static float WalkSpeed = 2.5f;
        public static float RunningSpeed = 7.0f;
        public static float SwimSpeed = 4.7222223f;
        public static float TurnRate = 3.141593f;

        public UpdateObject(List<UpdateBlock> blocks, int hasTansport = 0) : base(WorldOpcodes.SMSG_UPDATE_OBJECT)
        {
            Write((uint)blocks.Count());
            Write((byte)hasTansport); 
            blocks.ForEach(block => Write(block.Data));
        }

        public UpdateObject(List<byte[]> blocks, int hasTansport = 0) : base(WorldOpcodes.SMSG_UPDATE_OBJECT)
        {
            Write((uint)blocks.Count());
            Write((byte)hasTansport);
            blocks.ForEach(b => Write(b));
        }

        public static UpdateObject CreateOwnCharacterUpdate(Character character, out PlayerEntity entity)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte)ObjectUpdateType.UPDATETYPE_CREATE_OBJECT2);

            writer.WritePackedUInt64((ulong)character.Id);

            writer.Write((byte)TypeID.TYPEID_PLAYER);

            ObjectFlags updateFlags = ObjectFlags.UPDATEFLAG_ALL |
                                      ObjectFlags.UPDATEFLAG_HAS_POSITION |
                                      ObjectFlags.UPDATEFLAG_LIVING |
                                      ObjectFlags.UPDATEFLAG_SELF;

            writer.Write((byte)updateFlags);

            writer.Write((uint)MovementFlags.MOVEFLAG_NONE);
            writer.Write((uint)Environment.TickCount); // Time?

            // Position
            writer.Write(character.MapX);
            writer.Write(character.MapY);
            writer.Write(character.MapZ);
            writer.Write(character.MapRotation); // R

            // Movement speeds
            writer.Write((float)0);     // ????

            writer.Write(WalkSpeed);  // MOVE_WALK
            writer.Write(RunningSpeed);    // MOVE_RUN
            writer.Write(4.5f);  // MOVE_RUN_BACK
            writer.Write(SwimSpeed); // MOVE_SWIM
            writer.Write(2.5f);  // MOVE_SWIM_BACK
            writer.Write(TurnRate); // MOVE_TURN_RATE

            writer.Write(0x1); // Unkown...

            entity = new PlayerEntity(character) { ObjectGuid = new ObjectGuid((ulong) character.Id) };
            //entity.GUID = new ObjectGuid((ulong)character.Id);
            entity.WriteUpdateFields(writer);

            //new ContainerEntity(character);

            foreach (var item in Main.Database.GetInventory(character))
            {
                new ItemEntity(character, item);
            }

            return new UpdateObject(new List<byte[]> { (writer.BaseStream as MemoryStream)?.ToArray() });
        }

        internal static ServerPacket UpdateValues(ObjectEntity player)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte)ObjectUpdateType.UPDATETYPE_VALUES);

            byte[] guidBytes = GenerateGuidBytes(player.ObjectGuid.RawGuid);
            WriteBytes(writer, guidBytes, guidBytes.Length);

            player.WriteUpdateFields(writer);

            return new UpdateObject(new List<byte[]> { (writer.BaseStream as MemoryStream)?.ToArray() }, (player is PlayerEntity) ? 0 : 1);
        }

        internal static ServerPacket CreateCharacterUpdate(Character character)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte)ObjectUpdateType.UPDATETYPE_CREATE_OBJECT2);

            byte[] guidBytes = GenerateGuidBytes((ulong)character.Id);
            WriteBytes(writer, guidBytes, guidBytes.Length);

            writer.Write((byte)TypeID.TYPEID_PLAYER);

            ObjectFlags updateFlags = ObjectFlags.UPDATEFLAG_ALL |
                                      ObjectFlags.UPDATEFLAG_HAS_POSITION |
                                      ObjectFlags.UPDATEFLAG_LIVING;

            writer.Write((byte)updateFlags);

            writer.Write((uint)MovementFlags.MOVEFLAG_NONE);
            writer.Write((uint)Environment.TickCount); // Time?

            // Position
            writer.Write(character.MapX);
            writer.Write(character.MapY);
            writer.Write(character.MapZ);
            writer.Write(character.MapRotation); // R

            // Movement speeds
            writer.Write((float)0);     // ????

            writer.Write(2.5f);         // MOVE_WALK
            writer.Write(7f);           // MOVE_RUN
            writer.Write(4.5f);         // MOVE_RUN_BACK
            writer.Write(4.72f * 20);   // MOVE_SWIM
            writer.Write(2.5f);         // MOVE_SWIM_BACK
            writer.Write(3.14f);        // MOVE_TURN_RATE

            writer.Write(0x1); // Unkown...

            PlayerEntity a = new PlayerEntity(character) {Guid = (uint) character.Id};

            a.WriteUpdateFields(writer);

            return new UpdateObject(new List<byte[]> { (writer.BaseStream as MemoryStream)?.ToArray() });
        }

        internal static ServerPacket CreateOutOfRangeUpdate(List<ObjectEntity> despawnPlayer)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte)ObjectUpdateType.UPDATETYPE_OUT_OF_RANGE_OBJECTS);

            writer.Write((uint)despawnPlayer.Count);

            foreach (ObjectEntity entity in despawnPlayer)
            {
                writer.WritePackedUInt64(entity.ObjectGuid.RawGuid);
            }

            return new UpdateObject(new List<byte[]> { (writer.BaseStream as MemoryStream)?.ToArray() });
        }

        public static byte[] GenerateGuidBytes(ulong guid)
        {
            byte[] packedGuid = new byte[9];
            byte length = 1;

            for (byte i = 0; guid != 0; i++)
            {
                if ((guid & 0xFF) != 0)
                {
                    packedGuid[0] |= (byte)(1 << i);
                    packedGuid[length] = (byte)(guid & 0xFF);
                    ++length;
                }

                guid >>= 8;
            }

            byte[] clippedArray = new byte[length];
            Array.Copy(packedGuid, clippedArray, length);

            return clippedArray;
        }

        public static void WriteBytes(BinaryWriter writer, byte[] data, int count = 0)
        {
            if (count == 0)
                writer.Write(data);
            else
                writer.Write(data, 0, count);
        }
    }
}
