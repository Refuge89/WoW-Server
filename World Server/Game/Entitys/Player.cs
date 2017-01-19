using System.Collections.Generic;
using System.Linq;
using Framework.Contants.Character;
using Framework.Contants.Game;
using Framework.Database.Tables;
using World_Server.Game.Update;
using World_Server.Game.World.Blocks;
using World_Server.Managers;
using World_Server.Sessions;

namespace World_Server.Game.Entitys
{
    public class Player : Unit
    {
        public Character Character;
        public Unit Target;

        public Item Inventory;

        public List<Player> KnownPlayers { get; private set; }
        public List<Unit> KnownUnits { get; private set; }
        public List<GameObject> KnownGameObjects { get; set; }

        public List<Object> OutOfRangeEntitys { get; private set; }
        public List<UpdateBlock> UpdateBlocks { get; private set; }       

        public override string Name => Character.Name;

        public int Xp
        {
            set { SetUpdateField((int)PlayerField.PLAYER_XP, value); }
        }

        public override int DataLength => (int)PlayerField.PLAYER_END - 0x4;

        public WorldSession Session { get; internal set; }

        public Player(Character character) : base(new ObjectGuid((uint)character.Id, TypeID.TYPEID_PLAYER, HighGuid.HighguidMoTransport))
        {
            var skin = Main.Database.GetSkin(character);
            var chrRaces = DatabaseManager.ChrRaces.Values.FirstOrDefault(x => x.Match(character.Race));
            var inventory = Main.Database.GetInventory(character);

            Character = character;
            KnownPlayers = new List<Player>();
            KnownUnits = new List<Unit>();
            KnownGameObjects = new List<GameObject>();

            OutOfRangeEntitys = new List<Object>();
            UpdateBlocks = new List<UpdateBlock>();

            Guid = (uint)character.Id;
            SetUpdateField((int)ObjectFields.OBJECT_FIELD_TYPE, (byte)25);
            Level = character.Level;
            Xp = 0;
            Scale = GetScale(character);
            DisplayId = GetModel(character);
            NativeDisplayID = GetModel(character);
            FactionTemplate = chrRaces.FactionId;

            RaceUnit    = (byte) character.Race;
            ClassUnit   = (byte) character.Class;
            Gender      = (byte) character.Gender;
            Power       = GetPowerType(character);

            // Recupera a lista de Skills do Char
            GenerateSkills();

            // Monta Stats do Char
            GenerateStats();

            SetUpdateField((int)PlayerField.PLAYER_BYTES, skin.Skin, 0);
            SetUpdateField((int)PlayerField.PLAYER_BYTES, skin.Face, 1);
            SetUpdateField((int)PlayerField.PLAYER_BYTES, skin.HairStyle, 2);
            SetUpdateField((int)PlayerField.PLAYER_BYTES, skin.HairColor, 3);

            // PLAYER_BYTES_2 [FacialHair - PlayerBytes2_2 - BankBags.Slots -  RestState
            SetUpdateField((int)PlayerField.PLAYER_BYTES_2, 20, 2);

            // PLAYER_BYTES_3 [ Gender - DrunkState - PlayerBytes3_3 - PvPRank

            SetUpdateField((int)PlayerField.PLAYER_NEXT_LEVEL_XP, 400);

            int i = 0;
            foreach (var Item in inventory)
            {
                // Equipamento Equipado
                SetUpdateField((int)PlayerField.PLAYER_VISIBLE_ITEM_1_0 + (int)Item.Slot * 12, Item.Item);

                if (Item.Slot != 23)
                {
                    SetUpdateField((int)PlayerField.PLAYER_FIELD_INV_SLOT_HEAD + i * 2, Item.Item);
                    i++;
                }
            }

            //PLAYER_EXPLORED_ZONES_1
        }

        private void GenerateStats()
        {
            Health = 120;
            MaxHealth = 150;

            Mana = 110;
            MaxMana = 120;

            MaxRage = 100;
        }

        private void GenerateSkills()
        {
            int a = 0;
            foreach (CharactersSkill skill in Main.Database.GetSkills(Character))
            {
                SetUpdateField((int)PlayerField.PLAYER_SKILL_INFO_1_1 + (a * 3), skill.skill);
                SetUpdateField((int)PlayerField.PLAYER_SKILL_INFO_1_1 + (a * 3) + 1, skill.value + (skill.Max << 16));
                a++;
            }
        }

        private int GetModel(Character character)
        {
            var charModel = Main.Database.GetCharStarter(character.Race);

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