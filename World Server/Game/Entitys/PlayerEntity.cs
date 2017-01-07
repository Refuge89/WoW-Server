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



}