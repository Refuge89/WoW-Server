using System;
using System.Collections.Generic;
using Framework.Contants.Game;

namespace World_Server.Game.Update
{
    [Flags]
    public enum TypeFlags : byte
    {
        TypemaskObject = 0x0001,
        TypemaskItem = 0x0002,
        TypemaskContainer = 0x0004,
        TypemaskUnit = 0x0008, // players also have it
        TypemaskPlayer = 0x0010,
        TypemaskGameobject = 0x0020,
        TypemaskDynamicobject = 0x0040,
        TypemaskCorpse = 0x0080
    }

    public enum HighGuid
    {
        HighguidItem = 0x4700,
        HighguidContainer = 0x4700,
        HighguidPlayer = 0x0000,
        HighguidGameobject = 0xF110,
        HighguidTransport = 0xF120,
        HighguidUnit = 0xF130,
        HighguidPet = 0xF140,
        HighguidVehicle = 0xF150,
        HighguidDynamicobject = 0xF100,
        HighguidCorpse = 0xF500,
        HighguidMoTransport = 0x1FC0
    }

    public class ObjectGuid
    {
        private static readonly Dictionary<TypeID, uint> Indexes = new Dictionary<TypeID, uint>();

        public static ObjectGuid GetGameObjectGuid()
        {
            return new ObjectGuid(TypeID.TYPEID_GAMEOBJECT, HighGuid.HighguidGameobject);
        }

        private static uint GetIndex(TypeID type)
        {
            if (!Indexes.ContainsKey(type)) Indexes.Add(type, 1);

            return Indexes[type]++;
        }

        // ----------------------------------------------------------------------------

        public ulong RawGuid { get; }
        public uint Low => (uint)(RawGuid & 0x0000000000FFFFFF);

        public TypeID TypeId { get; private set; }
        public HighGuid HighGuid { get; private set; }

        public ObjectGuid(ulong guid)
        {
            RawGuid = guid;
        }

        public ObjectGuid(uint index, TypeID type, HighGuid high)
        {
            TypeId = type;
            HighGuid = high;

            RawGuid = index | ((ulong)type << 24) | ((ulong)high << 48);
        }

        public ObjectGuid(TypeID type, HighGuid high) : this(GetIndex(type), type, high) { }

    }
}
