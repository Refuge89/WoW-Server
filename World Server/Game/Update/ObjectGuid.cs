using System;
using System.Collections.Generic;
using Framework.Contants.Game;

namespace World_Server.Game.Update
{
    [Flags]
    public enum TypeFlags : byte
    {
        TYPEMASK_OBJECT = 0x0001,
        TYPEMASK_ITEM = 0x0002,
        TYPEMASK_CONTAINER = 0x0004,
        TYPEMASK_UNIT = 0x0008, // players also have it
        TYPEMASK_PLAYER = 0x0010,
        TYPEMASK_GAMEOBJECT = 0x0020,
        TYPEMASK_DYNAMICOBJECT = 0x0040,
        TYPEMASK_CORPSE = 0x0080
    }

    public enum HighGUID
    {
        HIGHGUID_ITEM = 0x4700,
        HIGHGUID_CONTAINER = 0x4700,
        HIGHGUID_PLAYER = 0x0000,
        HIGHGUID_GAMEOBJECT = 0xF110,
        HIGHGUID_TRANSPORT = 0xF120,
        HIGHGUID_UNIT = 0xF130,
        HIGHGUID_PET = 0xF140,
        HIGHGUID_VEHICLE = 0xF150,
        HIGHGUID_DYNAMICOBJECT = 0xF100,
        HIGHGUID_CORPSE = 0xF500,
        HIGHGUID_MO_TRANSPORT = 0x1FC0
    }

    public class ObjectGuid
    {
        private static readonly Dictionary<TypeID, uint> Indexes = new Dictionary<TypeID, uint>();

        public static ObjectGuid GetGameObjectGuid()
        {
            return new ObjectGuid(TypeID.TYPEID_OBJECT, HighGUID.HIGHGUID_MO_TRANSPORT);
        }

        private static uint GetIndex(TypeID type)
        {
            if (!Indexes.ContainsKey(type)) Indexes.Add(type, 0);

            return Indexes[type]++;
        }

        // ----------------------------------------------------------------------------

        public ulong RawGuid { get; private set; }
        public uint Low => (uint) (RawGuid & (ulong) 0x0000000000FFFFFF);

        public ObjectGuid(ulong guid)
        {
            RawGuid = guid;
        }

        public ObjectGuid(uint index, TypeID type, HighGUID high)
        {
            RawGuid = index | ((ulong) type << 24) | ((ulong) high << 48);
        }

        public ObjectGuid(TypeID type, HighGUID high) : this(GetIndex(type), type, high)
        {

        }

    }
}