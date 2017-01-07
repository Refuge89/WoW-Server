using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Framework.Contants.Character;
using Framework.Contants.Game;
using Framework.Database.Tables;
using World_Server.Game.Update;
using World_Server.Helpers;
using World_Server.Managers;
using World_Server.Sessions;

namespace World_Server.Game.Entitys
{
    public class PlayerEntity : UnitEntity
    {
        public Character Character;
        public UnitEntity Target;

        public List<PlayerEntity> KnownPlayers { get; private set; }
        public List<UnitEntity> KnownUnits { get; private set; }

        public List<ObjectEntity> OutOfRangeEntitys { get; private set; }
        public List<UpdateBlock> UpdateBlocks { get; private set; }

        public override string Name => Character.Name;

        public int Xp
        {
            set { SetUpdateField((int)EUnitFields.PLAYER_XP, value); }
        }

        public override int DataLength => (int)EUnitFields.PLAYER_END - 0x4;

        public WorldSession Session { get; internal set; }

        public PlayerEntity(Character character) : base(new ObjectGuid((uint)character.Id, TypeID.TYPEID_PLAYER, HighGuid.HighguidMoTransport))
        {
            var skin = Program.Database.GetSkin(character);
            var chrRaces = DatabaseManager.ChrRaces.Values.FirstOrDefault(x => x.Match(character.Race));

            this.Character = character;
            this.KnownPlayers = new List<PlayerEntity>();
            this.KnownUnits = new List<UnitEntity>();
            this.OutOfRangeEntitys = new List<ObjectEntity>();
            this.UpdateBlocks = new List<UpdateBlock>();

            this.Guid = (uint)character.Id;
            this.SetUpdateField((int)EObjectFields.OBJECT_FIELD_TYPE, (byte)25);
            this.Level = character.Level;
            this.Xp = 0;
            this.Scale = GetScale(character);
            this.DisplayId = GetModel(character);
            this.NativeDisplayID = GetModel(character);
            this.FactionTemplate = chrRaces.FactionId;

            this.RaceUnit    = (byte) character.Race;
            this.ClassUnit   = (byte) character.Class;
            this.Gender      = (byte) character.Gender;
            this.Power       = GetPowerType(character);

            // Recupera a lista de Skills do Char
            this.GenerateSkills();

            // Monta Stats do Char
            this.GenerateStats();

            SetUpdateField((int)EUnitFields.PLAYER_BYTES, skin.Skin, 0);
            SetUpdateField((int)EUnitFields.PLAYER_BYTES, skin.Face, 1);
            SetUpdateField((int)EUnitFields.PLAYER_BYTES, skin.HairStyle, 2);
            SetUpdateField((int)EUnitFields.PLAYER_BYTES, skin.HairColor, 3);

            SetUpdateField((int)EUnitFields.PLAYER_NEXT_LEVEL_XP, 400);

            // Items Equipamento
            WorldItems[] equipment = InventoryHelper.GenerateInventoryByIDs(character.Equipment);

            for (int i = 0; i < 19; i++)
            {
                if (equipment?[i] != null)
                {
                    SetUpdateField((int) EUnitFields.PLAYER_VISIBLE_ITEM_1_0 + (i * 12), equipment[i].itemId);
                    SetUpdateField((int) EUnitFields.PLAYER_FIELD_BANKBAG_SLOT_1 + (i * 12), equipment[i].itemId);
                }
            }
        }

        private void GenerateStats()
        {
            this.Health = 120;
            this.MaxHealth = 150;

            this.Mana = 110;
            this.MaxMana = 120;

            this.MaxRage = 100;
        }

        private void GenerateSkills()
        {
            int a = 0;
            foreach (CharactersSkill skill in Program.Database.GetSkills(this.Character))
            {
                SetUpdateField((int)EUnitFields.PLAYER_SKILL_INFO_1_1 + (a * 3), skill.skill);
                SetUpdateField((int)EUnitFields.PLAYER_SKILL_INFO_1_1 + (a * 3) + 1, (Int32)(skill.value + (skill.Max << 16)));
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

        private byte GetPowerType(Character character)
        {
            switch (character.Class)
            {
                case ClassID.WARRIOR:
                    return (byte)PowerTypes.TYPE_RAGE;
                case ClassID.HUNTER:
                    return (byte)PowerTypes.TYPE_FOCUS;
                case ClassID.ROGUE:
                    return (byte)PowerTypes.TYPE_ENERGY;
                default:
                    return (byte)PowerTypes.TYPE_MANA;
            }
        }
    }

    public enum PowerTypes : uint
    {
        TYPE_MANA = 0,
        TYPE_RAGE = 1,
        TYPE_FOCUS = 2,
        TYPE_ENERGY = 3,
        TYPE_HAPPINESS = 4,
        POWER_HEALTH = 0xFFFFFFFE
    }

    public enum UnitFlags
    {
        UNIT_FLAG_NONE = 0x00000000,
        UNIT_FLAG_UNK_0 = 0x00000001,
        UNIT_FLAG_NON_ATTACKABLE = 0x00000002,           // not attackable
        UNIT_FLAG_DISABLE_MOVE = 0x00000004,
        UNIT_FLAG_PVP_ATTACKABLE = 0x00000008,           // allow apply pvp rules to attackable state in addition to faction dependent state, UNIT_FLAG_UNKNOWN1 in pre-bc mangos
        UNIT_FLAG_RENAME = 0x00000010,           // rename creature
        UNIT_FLAG_RESTING = 0x00000020,
        UNIT_FLAG_UNK_6 = 0x00000040,
        UNIT_FLAG_OOC_NOT_ATTACKABLE = 0x00000100,           // (OOC Out Of Combat) Can not be attacked when not in combat. Removed if unit for some reason enter combat (flag probably removed for the attacked and it's party/group only)
        UNIT_FLAG_PASSIVE = 0x00000200,           // makes you unable to attack everything. Almost identical to our "civilian"-term. Will ignore it's surroundings and not engage in combat unless "called upon" or engaged by another unit.
        UNIT_FLAG_PVP = 0x00001000,
        UNIT_FLAG_SILENCED = 0x00002000,           // silenced, 2.1.1
        UNIT_FLAG_UNK_14 = 0x00004000,
        UNIT_FLAG_UNK_15 = 0x00008000,
        UNIT_FLAG_UNK_16 = 0x00010000,           // removes attackable icon
        UNIT_FLAG_PACIFIED = 0x00020000,
        UNIT_FLAG_DISABLE_ROTATE = 0x00040000,
        UNIT_FLAG_IN_COMBAT = 0x00080000,
        UNIT_FLAG_NOT_SELECTABLE = 0x02000000,
        UNIT_FLAG_SKINNABLE = 0x04000000,
        UNIT_FLAG_AURAS_VISIBLE = 0x08000000,           // magic detect
        UNIT_FLAG_SHEATHE = 0x40000000,
        // UNIT_FLAG_UNK_31              = 0x80000000           // no affect in 1.12.1

        // [-ZERO] TBC enumerations [?]
        UNIT_FLAG_NOT_ATTACKABLE_1 = 0x00000080,           // ?? (UNIT_FLAG_PVP_ATTACKABLE | UNIT_FLAG_NOT_ATTACKABLE_1) is NON_PVP_ATTACKABLE
        UNIT_FLAG_LOOTING = 0x00000400,           // loot animation
        UNIT_FLAG_PET_IN_COMBAT = 0x00000800,           // in combat?, 2.0.8
        UNIT_FLAG_STUNNED = 0x00040000,           // stunned, 2.1.1
        UNIT_FLAG_TAXI_FLIGHT = 0x00100000,           // disable casting at client side spell not allowed by taxi flight (mounted?), probably used with 0x4 flag
        UNIT_FLAG_DISARMED = 0x00200000,           // disable melee spells casting..., "Required melee weapon" added to melee spells tooltip.
        UNIT_FLAG_CONFUSED = 0x00400000,
        UNIT_FLAG_FLEEING = 0x00800000,
        UNIT_FLAG_PLAYER_CONTROLLED = 0x01000000,           // used in spell Eyes of the Beast for pet... let attack by controlled creature
        //[-ZERO]    UNIT_FLAG_MOUNT                 = 0x08000000,
        UNIT_FLAG_UNK_28 = 0x10000000,
        UNIT_FLAG_UNK_29 = 0x20000000,           // used in Feing Death spell
    };

    public enum UnitStandStateType : byte
    {
        UnitStandStateStand = 0,
        UnitStandStateSit = 1,
        UnitStandStateSitChair = 2,
        UnitStandStateSleep = 3,
        UnitStandStateSitLowChair = 4,
        UnitStandStateSitMediumChair = 5,
        UnitStandStateSitHighChair = 6,
        UnitStandStateDead = 7,
        UnitStandStateKneel = 8
    }

    public enum ShapeshiftForm : byte
    {
        FormNone = 0x00,
        FormCat = 0x01,
        FormTree = 0x02,
        FormTravel = 0x03,
        FormAqua = 0x04,
        FormBear = 0x05,
        FormAmbient = 0x06,
        FormGhoul = 0x07,
        FormDirebear = 0x08,
        FormCreaturebear = 0x0E,
        FormCreaturecat = 0x0F,
        FormGhostwolf = 0x10,
        FormBattlestance = 0x11,
        FormDefensivestance = 0x12,
        FormBerserkerstance = 0x13,
        FormShadow = 0x1C,
        FormStealth = 0x1E,
        FormMoonkin = 0x1F,
        FormSpiritofredemption = 0x20
    }
}