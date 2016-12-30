﻿using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Framework.Extensions;

namespace World_Server.Handlers.World
{
    public class PSUpdateObject : ServerPacket
    {
        public PSUpdateObject(List<byte[]> blocks) : base(WorldOpcodes.SMSG_UPDATE_OBJECT)
        {
            Write((uint)blocks.Count());
            Write((byte)0); // Has transport
            blocks.ForEach(b => Write(b));
        }
        
        public static PSUpdateObject CreateOwnCharacterUpdate(Character character)
        {
            BinaryWriter writer = new BinaryWriter(new MemoryStream());
            writer.Write((byte)ObjectUpdateType.UPDATETYPE_CREATE_OBJECT2);

            // MERDA AQUI NESSA PORCARIA
            Console.WriteLine((UInt16)257);
            writer.Write((UInt16)257 /*character.GUID*/);
            //writer.WritePackedUInt64((ulong)character.Id);

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

            writer.Write(StringToByteArray("29 15 00 40 54 1D C0 00 00 00 00 00 80 20 00 00 C0 D9 04 C2 4F 38 19 00 00 06 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 E0 B6 6D DB B6 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 6C 80 00 00 00 00 00 00 80 00 40 00 00 80 3F 00 00 00 00 20 00 00 00 00 00 00 01 00 00 00 19 00 00 00 CD CC AC 3F 54 00 00 00 64 00 00 00 54 00 00 00 E8 03 00 00 64 00 00 00 01 00 00 00 06 00 00 00 06 01 00 01 08 00 00 00 99 09 00 00 09 00 00 00 01 00 00 00 D0 07 00 00 D0 07 00 00 D0 07 00 00 3B 00 00 00 3B 00 00 00 25 49 D2 40 25 49 F2 40 00 EE 11 00 00 00 80 3F 1C 00 00 00 0F 00 00 00 18 00 00 00 0F 00 00 00 16 00 00 00 1E 00 00 00 0A 00 00 00 14 00 00 00 00 28 00 00 27 00 00 00 06 00 00 00 DC B6 ED 3F 6E DB 36 40 07 00 07 01 02 00 00 01 90 01 00 00 1A 00 00 00 01 00 01 00 2C 00 00 00 01 00 05 00 36 00 00 00 01 00 05 00 5F 00 00 00 01 00 05 00 6D 00 00 00 2C 01 2C 01 73 00 00 00 2C 01 2C 01 A0 00 00 00 01 00 05 00 A2 00 00 00 01 00 05 00 9D 01 00 00 01 00 01 00 9E 01 00 00 01 00 01 00 9F 01 00 00 01 00 01 00 B1 01 00 00 01 00 01 00 02 00 00 00 48 E1 9A 40 3E 0A 17 3F 3E 0A 17 3F CD CC 0C 3F 00 00 04 00 29 00 00 00 0A 00 00 00 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F FF FF FF FF"));

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