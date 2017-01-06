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
        private static Dictionary<TypeID, uint> Indexes = new Dictionary<TypeID, uint>();


        public static ObjectGuid GetUnitGUID()
        {
            return new ObjectGuid(TypeID.TYPEID_UNIT, HighGUID.HIGHGUID_UNIT);
        }

        public static ObjectGuid GetUnitGUID(uint low)
        {
            return new ObjectGuid(low, TypeID.TYPEID_UNIT, HighGUID.HIGHGUID_UNIT);
        }

        public static ObjectGuid GetGameObjectGUID()
        {
            return new ObjectGuid(TypeID.TYPEID_GAMEOBJECT, HighGUID.HIGHGUID_GAMEOBJECT);
        }

        public static ObjectGuid GetGameObjectGUID(uint index)
        {
            return new ObjectGuid(index, TypeID.TYPEID_OBJECT, HighGUID.HIGHGUID_MO_TRANSPORT);
        }

        private static uint GetIndex(TypeID type)
        {
            if (!Indexes.ContainsKey(type)) Indexes.Add(type, 1);

            return Indexes[type]++;
        }

        // ----------------------------------------------------------------------------

        public ulong RawGUID { get; private set; }
        public uint Low { get { return (uint)(RawGUID & (ulong)0x0000000000FFFFFF); } }

        public TypeID TypeID { get; private set; }
        public HighGUID HighGUID { get; private set; }

        public ObjectGuid(ulong GUID)
        {
            RawGUID = GUID;
        }

        public ObjectGuid(uint index, TypeID type, HighGUID high)
        {
            TypeID = type;
            HighGUID = high;

            RawGUID = (ulong)((ulong)index |
                      ((ulong)type << 24) |
                      ((ulong)high << 48));
        }

        public ObjectGuid(TypeID type, HighGUID high) : this(GetIndex(type), type, high) { }


        public TypeID GetId()
        {
            return (TypeID)((RawGUID >> 24) & 0xFFFFF);
        }

        public HighGUID GetGuidType()
        {
            return (HighGUID)(RawGUID >> 48);
        }
    }
}
