using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using World_Server.Game.Entitys;
using Framework.Contants.Character;
using Framework.Contants.Game;
using World_Server.Sessions;

namespace World_Server.Handlers.World
{
    public class PSUpdateObject : ServerPacket
    {
        public PSUpdateObject(List<byte[]> blocks) : base(WorldOpcodes.SMSG_UPDATE_OBJECT)
        {
            Write((uint)blocks.Count());
            Write((byte)0);
            blocks.ForEach(b => Write(b));
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


        public static PSUpdateObject CreateOwnCharacterUpdate(Character character, PCPlayerLogin packet)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte)ObjectUpdateType.UPDATETYPE_CREATE_OBJECT2);

            byte[] guidBytes = GenerateGuidBytes((ulong)character.Id);
            WriteBytes(writer, guidBytes, guidBytes.Length);

            writer.Write((byte)TypeID.TYPEID_PLAYER);

            ObjectFlags updateFlags = ObjectFlags.UPDATEFLAG_ALL |
                                      ObjectFlags.UPDATEFLAG_HAS_POSITION |
                                      ObjectFlags.UPDATEFLAG_LIVING |
                                      ObjectFlags.UPDATEFLAG_SELF;

            writer.Write((byte)updateFlags);

            writer.Write((UInt32)MovementFlags.MOVEFLAG_NONE);
            writer.Write((UInt32)Environment.TickCount); // Time?

            // Position
            writer.Write((float)character.MapX);
            writer.Write((float)character.MapY);
            writer.Write((float)character.MapZ);
            writer.Write((float)character.MapRotation); // R

            // Movement speeds
            writer.Write((float)0);     // ????

            writer.Write((float)2.5f);  // MOVE_WALK
            writer.Write((float)7);     // MOVE_RUN
            writer.Write((float)4.5f);  // MOVE_RUN_BACK
            writer.Write((float)4.72f); // MOVE_SWIM
            writer.Write((float)2.5f);  // MOVE_SWIM_BACK
            writer.Write((float)3.14f); // MOVE_TURN_RATE

            writer.Write(0x1); // Unkown...

            new PlayerEntity(character).WriteUpdateFields(writer);

            return new PSUpdateObject(new List<byte[]> { (writer.BaseStream as MemoryStream).ToArray() });
        }


        public static PSUpdateObject CreateCharacterUpdate(Character character)
        {
            var writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte)ObjectUpdateType.UPDATETYPE_CREATE_OBJECT2);

            var guidBytes = GenerateGuidBytes((ulong)character.Id);
            WriteBytes(writer, guidBytes, guidBytes.Length);

            writer.Write((byte)TypeID.TYPEID_PLAYER);

            var updateFlags = ObjectFlags.UPDATEFLAG_ALL |
                              ObjectFlags.UPDATEFLAG_HAS_POSITION |
                              ObjectFlags.UPDATEFLAG_LIVING;

            writer.Write((byte)updateFlags);

            writer.Write((uint)MovementFlags.MOVEFLAG_NONE);
            writer.Write((uint)55675); // Time?

            // Position
            writer.Write(character.MapX);
            writer.Write(character.MapY);
            writer.Write(character.MapZ);
            writer.Write(character.MapRotation);

            // Movement speeds
            writer.Write((float)0); // ????

            writer.Write(2.5f); // MOVE_WALK
            writer.Write((float)7); // MOVE_RUN
            writer.Write(4.5f); // MOVE_RUN_BACK
            writer.Write(4.72f * 20); // MOVE_SWIM
            writer.Write(2.5f); // MOVE_SWIM_BACK
            writer.Write(3.14f); // MOVE_TURN_RATE

            writer.Write(0x1); // Unkown...

            new PlayerEntity(character).WriteUpdateFields(writer);

            return new PSUpdateObject(new List<byte[]> { (writer.BaseStream as MemoryStream).ToArray() });
        }

        public static PSUpdateObject Poop(WorldSession session, Character character)
        {
            var writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte)ObjectUpdateType.UPDATETYPE_MOVEMENT);

            var guidBytes = GenerateGuidBytes((ulong)character.Id);
            WriteBytes(writer, guidBytes, guidBytes.Length);


            writer.Write((byte)TypeID.TYPEID_PLAYER);

            var updateFlags = ObjectFlags.UPDATEFLAG_HAS_POSITION;


            writer.Write((byte)updateFlags);

            writer.Write((uint)MovementFlags.MOVEFLAG_NONE);
            //writer.Write((UInt32)55675); // Time?

            // Position
            writer.Write((float)1);
            writer.Write((float)0);
            writer.Write((float)0);
            writer.Write((float)0); // R
            /*
            // Movement speeds
            writer.Write((float)0);     // ????
            writer.Write((float)2.5f);  // MOVE_WALK
            writer.Write((float)7);     // MOVE_RUN
            writer.Write((float)7 * 20);     // MOVE_RUN
            writer.Write((float)4.5f);  // MOVE_RUN_BACK
            writer.Write((float)4.72f); // MOVE_SWIM
            writer.Write((float)4.72f * 20); // MOVE_SWIM
            writer.Write((float)2.5f);  // MOVE_SWIM_BACK
            writer.Write((float)3.14f); // MOVE_TURN_RATE

            writer.Write(0x1); // Unkown...
            */
            //new Player(character).WriteUpdateFields(writer);

            new PlayerEntity(character).WriteUpdateFields(writer);

            return new PSUpdateObject(new List<byte[]> { (writer.BaseStream as MemoryStream).ToArray() });
        }

        public static byte[] StringToByteArray(string hex)
        {
            hex = hex.Replace(" ", "");

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}