using System.Collections.Generic;
using Framework.Contants.Game;

namespace World_Server.Game.Update
{
    public class ObjectGuid
    {
        public ulong RawGuid { get; }
        public uint Low => (uint)(RawGuid & 0x0000000000FFFFFF);
        public TypeID TypeId { get; private set; }
        public HighGuid HighGuid { get; private set; }
        public ObjectGuid(TypeID type, HighGuid high) : this(GetIndex(type), type, high) { }

        private static readonly Dictionary<TypeID, uint> Indexes = new Dictionary<TypeID, uint>();

        public static ObjectGuid GetGameObjectGuid()
        {
            return new ObjectGuid(TypeID.TYPEID_GAMEOBJECT, HighGuid.HighguidGameobject);
        }

        public static ObjectGuid GetUnitGuid()
        {
            return new ObjectGuid(TypeID.TYPEID_UNIT, HighGuid.HighguidUnit);
        }

        public static ObjectGuid GetContainerGuid()
        {
            return new ObjectGuid(TypeID.TYPEID_CONTAINER, HighGuid.HighguidContainer);
        }

        public static ObjectGuid GetCorpseGuid()
        {
            return new ObjectGuid(TypeID.TYPEID_CORPSE, HighGuid.HighguidCorpse);
        }

        public static ObjectGuid GetDynamicObjectGuidGuid()
        {
            return new ObjectGuid(TypeID.TYPEID_DYNAMICOBJECT, HighGuid.HighguidDynamicobject);
        }

        public static ObjectGuid GetItemGuid()
        {
            return new ObjectGuid(TypeID.TYPEID_ITEM, HighGuid.HighguidItem);
        }

        public static ObjectGuid GetPlayerGuid()
        {
            return new ObjectGuid(TypeID.TYPEID_PLAYER, HighGuid.HighguidPlayer);
        }

        private static uint GetIndex(TypeID type)
        {
            if (!Indexes.ContainsKey(type)) Indexes.Add(type, 1);

            return Indexes[type]++;
        }

        public ObjectGuid(ulong guid, TypeID type)
        {
            RawGuid = guid;
            TypeId = type;
        }

        public ObjectGuid(uint index, TypeID type, HighGuid high)
        {
            TypeId = type;
            HighGuid = high;

            RawGuid = index | ((ulong)type << 24) | ((ulong)high << 48);
        }

        public TypeID GetId()
        {
            return (TypeID)((RawGuid >> 24) & 0xFFFFF);
        }
    }
}
