using Framework.Contants.Character;
using Framework.Contants.Game;
using Framework.Database.Tables;
using System;
using System.Collections.Generic;
using World_Server.Game.Update;
using World_Server.Helpers;
using World_Server.Sessions;

namespace World_Server.Game.Entitys
{
    public class PlayerEntity : UnitEntity
    {
        public Character Character;
        public UnitEntity Target;

        public List<PlayerEntity> KnownPlayers { get; private set; }
        public List<UnitEntity> KnownUnits { get; private set; }
        public List<GOEntity> KnownGameObjects { get; private set; }

        public List<ObjectEntity> OutOfRangeEntitys { get; private set; }
        public List<UpdateBlock> UpdateBlocks { get; private set; }

        public override string Name
        {
            get { return Character.Name; }
        }

        public int XP
        {
            get { return (int)UpdateData[(int)EUnitFields.PLAYER_XP]; }
            set { SetUpdateField((int)EUnitFields.PLAYER_XP, value); }
        }

        public override int DataLength
        {
            get { return (int)EUnitFields.PLAYER_END - 0x4; }
        }

        public WorldSession Session { get; internal set; }

        public PlayerEntity(Character character) : base(new ObjectGuid((uint)character.Id, TypeID.TYPEID_PLAYER, HighGUID.HIGHGUID_MO_TRANSPORT))
        {
            var skin = Program.Database.GetSkin(character);

            Character = character;
            KnownPlayers = new List<PlayerEntity>();
            KnownUnits = new List<UnitEntity>();
            KnownGameObjects = new List<GOEntity>();

            GUID = (uint)character.Id;
            //SetUpdateField<Int32>((int)EObjectFields.OBJECT_FIELD_GUID, character.Id);

            SetUpdateField<byte>((int)EObjectFields.OBJECT_FIELD_TYPE, (byte)25);
            //SetUpdateField<byte>((int)EObjectFields.OBJECT_FIELD_TYPE, (byte)TypeID.TYPEID_UNIT);

            OutOfRangeEntitys = new List<ObjectEntity>();
            UpdateBlocks = new List<UpdateBlock>();

            Character   = character;
            Health      = 70;
            MaxHealth   = 70;
            Level       = 1;
            //XP          = 0;
            Scale       = GetScale(character);

            //ChrRaces ChrRaces = DBCManager.ChrRaces.Values.FirstOrDefault(x => x.Match(character.Race));

            SetUpdateField((int)EUnitFields.UNIT_FIELD_FACTIONTEMPLATE, 5);
            
            SetUpdateField((int)EUnitFields.UNIT_FIELD_POWER1, 1000);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_POWER2, 1000);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_POWER3, 1000);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_POWER4, 1000);

            SetUpdateField((int)EUnitFields.UNIT_FIELD_MAXPOWER1, 1000);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_MAXPOWER2, 1000);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_MAXPOWER3, 1000);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_MAXPOWER4, 1000);

            SetUpdateField((int)EUnitFields.UNIT_FIELD_BYTES_0, (byte)character.Race, 0);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_BYTES_0, (byte)character.Class, 1);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_BYTES_0, (byte)character.Gender, 2);
            SetUpdateField<byte>((int)EUnitFields.UNIT_FIELD_BYTES_0, 0, 3); // [mana 0] [rage 1] [focus 2] [energy 3]
            SetUpdateField<Int32>((int)EUnitFields.UNIT_FIELD_BASEATTACKTIME, 2000);
            SetUpdateField<Int32>((int)EUnitFields.UNIT_FIELD_OFFHANDATTACKTIME, 2000);
            SetUpdateField<Int32>((int)EUnitFields.UNIT_FIELD_RANGEDATTACKTIME, 2000);

            SetUpdateField((int)EUnitFields.UNIT_FIELD_DISPLAYID, GetModel(character));
            SetUpdateField((int)EUnitFields.UNIT_FIELD_NATIVEDISPLAYID, GetModel(character));

            SetUpdateField((int)EUnitFields.UNIT_FIELD_MINDAMAGE, 1083927991);
            SetUpdateField<Int32>((int)EUnitFields.UNIT_FIELD_MAXDAMAGE, 1086025143);

            SetUpdateField((int)EUnitFields.UNIT_FIELD_BYTES_1, (byte)UnitStandStateType.UNIT_STAND_STATE_STAND, 0); // Stand State?
            SetUpdateField<byte>((int)EUnitFields.UNIT_FIELD_BYTES_1, 0xEE, 1); //  if (getPowerType() == POWER_RAGE || getPowerType() == POWER_MANA)
            SetUpdateField((int)EUnitFields.UNIT_FIELD_BYTES_1, (character.Class == ClassID.WARRIOR) ? (byte)ShapeshiftForm.FORM_BATTLESTANCE : (byte)ShapeshiftForm.FORM_NONE, 2); // ShapeshiftForm?
            SetUpdateField<byte>((int)EUnitFields.UNIT_FIELD_BYTES_1, /* (byte)UnitBytes1_Flags.UNIT_BYTE1_FLAG_ALL */ 0, 3); // StandMiscFlags

            SetUpdateField<float>((int)EUnitFields.UNIT_MOD_CAST_SPEED, 1);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_STAT0, 22);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_STAT1, 18);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_STAT2, 23);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_STAT3, 18);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_STAT4, 25);

            SetUpdateField<Int32>((int)EUnitFields.UNIT_FIELD_RESISTANCES, 36);
            SetUpdateField<Int32>((int)EUnitFields.UNIT_FIELD_RESISTANCES_05, 10);
            SetUpdateField<Int32>((int)EUnitFields.UNIT_FIELD_BASE_HEALTH, 20);

            SetUpdateField<Int32>((int)EUnitFields.UNIT_FIELD_ATTACK_POWER, 27);
            SetUpdateField<Int32>((int)EUnitFields.UNIT_FIELD_RANGED_ATTACK_POWER, 9);
            SetUpdateField<Int32>((int)EUnitFields.UNIT_FIELD_MINRANGEDDAMAGE, 1074940196);
            SetUpdateField<Int32>((int)EUnitFields.UNIT_FIELD_MAXRANGEDDAMAGE, 1079134500);

            SetUpdateField<byte>((int)EUnitFields.PLAYER_BYTES, skin.Skin, 0);
            SetUpdateField<byte>((int)EUnitFields.PLAYER_BYTES, skin.Face, 1);
            SetUpdateField<byte>((int)EUnitFields.PLAYER_BYTES, skin.HairStyle, 2);
            SetUpdateField<byte>((int)EUnitFields.PLAYER_BYTES, skin.HairColor, 3);

            SetUpdateField<byte>((int)EUnitFields.PLAYER_BYTES_2, 0, 0);

            // Items Equipamento
            WorldItems[] equipment = InventoryHelper.GenerateInventoryByIDs(character.Equipment);

            for (int i = 0; i < 19; i++)
            {
                if (equipment?[i] != null)
                    SetUpdateField((int)EUnitFields.PLAYER_VISIBLE_ITEM_1_0 + (i * 12), equipment[i].itemId);
            }

            SetUpdateField<byte>((int)EUnitFields.PLAYER_BYTES_2, 0, 0);

            SetUpdateField<Int32>((int)EUnitFields.PLAYER_NEXT_LEVEL_XP, 400);
            SetUpdateField<Int32>((int)EUnitFields.PLAYER_SKILL_INFO_1_1, 26);

            int a = 0;
            foreach (CharactersSkill Skill in Program.Database.GetSkills(character))
            {
                SetUpdateField<Int32>((int)EUnitFields.PLAYER_SKILL_INFO_1_1 + (a * 3), Skill.skill);
                SetUpdateField<Int32>((int)EUnitFields.PLAYER_SKILL_INFO_1_1 + (a * 3) + 1, (Int16)Skill.value + Skill.Max);
                SetUpdateField<Int32>((int)EUnitFields.PLAYER_SKILL_INFO_1_1 + (a * 3) + 2, 3); // Bonus de Skill
                a++;
            }

        }

        private int GetModel(Character character)
        {
            var charModel = Program.Database.GetCharStarter(character.Race);

            return character.Gender == GenderID.MALE ? charModel.ModelM : charModel.ModelF;
        }

        private float GetScale(Character character)
        {
            if (character.Race == RaceID.TAUREN && character.Gender == GenderID.MALE)
                return 1.3f;

            if (character.Race == RaceID.TAUREN)
                return 1.25f;

            return 1f;
        }
    }

    public enum UnitStandStateType : byte
    {
        UNIT_STAND_STATE_STAND = 0,
        UNIT_STAND_STATE_SIT = 1,
        UNIT_STAND_STATE_SIT_CHAIR = 2,
        UNIT_STAND_STATE_SLEEP = 3,
        UNIT_STAND_STATE_SIT_LOW_CHAIR = 4,
        UNIT_STAND_STATE_SIT_MEDIUM_CHAIR = 5,
        UNIT_STAND_STATE_SIT_HIGH_CHAIR = 6,
        UNIT_STAND_STATE_DEAD = 7,
        UNIT_STAND_STATE_KNEEL = 8
    };

    public enum ShapeshiftForm : byte
    {
        FORM_NONE = 0x00,
        FORM_CAT = 0x01,
        FORM_TREE = 0x02,
        FORM_TRAVEL = 0x03,
        FORM_AQUA = 0x04,
        FORM_BEAR = 0x05,
        FORM_AMBIENT = 0x06,
        FORM_GHOUL = 0x07,
        FORM_DIREBEAR = 0x08,
        FORM_CREATUREBEAR = 0x0E,
        FORM_CREATURECAT = 0x0F,
        FORM_GHOSTWOLF = 0x10,
        FORM_BATTLESTANCE = 0x11,
        FORM_DEFENSIVESTANCE = 0x12,
        FORM_BERSERKERSTANCE = 0x13,
        FORM_SHADOW = 0x1C,
        FORM_STEALTH = 0x1E,
        FORM_MOONKIN = 0x1F,
        FORM_SPIRITOFREDEMPTION = 0x20
    }
}
