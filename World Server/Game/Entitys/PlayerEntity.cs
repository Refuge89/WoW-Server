using Framework.Contants.Game;
using Framework.Database.Tables;
using Framework.Helpers;
using System;
using System.Collections.Generic;
using World_Server.Handlers.Updates;
using World_Server.Sessions;

namespace World_Server.Game.Entitys
{
    public class PlayerEntity : UnitEntity
    {
        public List<ObjectEntity> OutOfRangeEntitys { get; private set; }
        public List<UpdateBlock> UpdateBlocks { get; private set; }

        public List<PlayerEntity> KnownPlayers { get; private set; }
        public List<UnitEntity> KnownUnits { get; private set; }
        public List<GOEntity> KnownGameObjects { get; private set; }

        public Character Character;
        public UnitEntity Target;

        // public Equipment
        // public Inventory
        // public Spells

        public float lastUpdateX, lastUpdateY;
        public float X, Y, Z;

        public override string Name
        {
            get
            {
                return Character.Name;
            }
        }

        public int XP
        {
            get { return (int)UpdateData[(int)EUnitFields.PLAYER_XP]; }
            set { SetUpdateField<int>((int)EUnitFields.PLAYER_XP, value); }
        }

        public override int DataLength
        {
            get { return (int)EUnitFields.PLAYER_END - 0x4; }
        }

        public WorldSession Session { get; set; }

        public PlayerEntity(Character character) : base(new ObjectGUID((uint)character.Id, TypeID.TYPEID_PLAYER, HighGUID.HIGHGUID_MO_TRANSPORT))
        {
            Character = character;
            KnownPlayers = new List<PlayerEntity>();
            KnownUnits = new List<UnitEntity>();
            KnownGameObjects = new List<GOEntity>();

            //TODO Fix spellCollection DBC
            //SpellCollection = new SpellCollection(this);

            GUID = (uint)character.Id;
            //SetUpdateField<Int32>((int)EObjectFields.OBJECT_FIELD_GUID, character.GUID);

            SetUpdateField<byte>((int)EObjectFields.OBJECT_FIELD_TYPE, (byte)25);
            //SetUpdateField<byte>((int)EObjectFields.OBJECT_FIELD_TYPE, (byte)TypeID.TYPEID_UNIT);

            OutOfRangeEntitys = new List<ObjectEntity>();
            UpdateBlocks = new List<UpdateBlock>();

            Character = character;
            //Type = (byte)25;
            Health = 70;
            MaxHealth = 70;
            Level = 1;
            XP = 0;
            Scale = 1;
        }
    }
}
