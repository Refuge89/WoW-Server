using System.Collections.Generic;
using Framework.Contants.Game;

namespace World_Server.Game.Update
{
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
