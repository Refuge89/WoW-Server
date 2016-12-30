using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using World_Server.Game.Entitys;

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
            writer.Write((float)0); // R

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

        public static byte[] StringToByteArray(string hex)
        {
            hex = hex.Replace(" ", "");

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

    }

    public enum ObjectUpdateType : byte
    {
        UPDATETYPE_VALUES = 0,
        UPDATETYPE_MOVEMENT = 1,
        UPDATETYPE_CREATE_OBJECT = 2,
        UPDATETYPE_CREATE_OBJECT2 = 3,
        UPDATETYPE_OUT_OF_RANGE_OBJECTS = 4,
        UPDATETYPE_NEAR_OBJECTS = 5
    }

    public enum TypeID : byte
    {
        TYPEID_OBJECT = 0,
        TYPEID_ITEM = 1,
        TYPEID_CONTAINER = 2,
        TYPEID_UNIT = 3,
        TYPEID_PLAYER = 4,
        TYPEID_GAMEOBJECT = 5,
        TYPEID_DYNAMICOBJECT = 6,
        TYPEID_CORPSE = 7
    }

    [Flags]
    public enum ObjectFlags : byte
    {
        UPDATEFLAG_NONE = 0x0000,
        UPDATEFLAG_SELF = 0x0001,
        UPDATEFLAG_TRANSPORT = 0x0002,
        UPDATEFLAG_FULLGUID = 0x0004,
        UPDATEFLAG_HIGHGUID = 0x0008,
        UPDATEFLAG_ALL = 0x0010,
        UPDATEFLAG_LIVING = 0x0020,
        UPDATEFLAG_HAS_POSITION = 0x0040
    }

    [Flags]
    public enum MovementFlags
    {
        MOVEFLAG_NONE = 0x00000000,
        MOVEFLAG_FORWARD = 0x00000001,
        MOVEFLAG_BACKWARD = 0x00000002,
        MOVEFLAG_STRAFE_LEFT = 0x00000004,
        MOVEFLAG_STRAFE_RIGHT = 0x00000008,
        MOVEFLAG_TURN_LEFT = 0x00000010,
        MOVEFLAG_TURN_RIGHT = 0x00000020,
        MOVEFLAG_PITCH_UP = 0x00000040,
        MOVEFLAG_PITCH_DOWN = 0x00000080,
        MOVEFLAG_WALK_MODE = 0x00000100,               // Walking

        MOVEFLAG_LEVITATING = 0x00000400,
        MOVEFLAG_ROOT = 0x00000800,               // [-ZERO] is it really need and correct value
        MOVEFLAG_FALLING = 0x00002000,
        MOVEFLAG_FALLINGFAR = 0x00004000,
        MOVEFLAG_SWIMMING = 0x00200000,               // appears with fly flag also
        MOVEFLAG_ASCENDING = 0x00400000,               // [-ZERO] is it really need and correct value
        MOVEFLAG_CAN_FLY = 0x00800000,               // [-ZERO] is it really need and correct value
        MOVEFLAG_FLYING = 0x01000000,               // [-ZERO] is it really need and correct value

        MOVEFLAG_ONTRANSPORT = 0x02000000,               // Used for flying on some creatures
        MOVEFLAG_SPLINE_ELEVATION = 0x04000000,               // used for flight paths
        MOVEFLAG_SPLINE_ENABLED = 0x08000000,               // used for flight paths
        MOVEFLAG_WATERWALKING = 0x10000000,               // prevent unit from falling through water
        MOVEFLAG_SAFE_FALL = 0x20000000,               // active rogue safe fall spell (passive)
        MOVEFLAG_HOVER = 0x40000000
    }
}