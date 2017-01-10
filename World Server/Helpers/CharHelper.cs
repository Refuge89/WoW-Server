using System;
using System.Linq;
using Framework.Database;
using Framework.Database.Tables;
using Framework.Database.Xml;
using Framework.Database.XML;
using Framework.DBC.Structs;
using Shaolinq;
using World_Server.Managers;

namespace World_Server.Helpers
{
    class CharHelper : BaseModel<Models>
    {
        public itemsItem Template;

        internal void GeraSpells(Character character)
        {
            #region Select spell of race

            foreach (raceSpell spellid in XmlManager.GetRaceStats(character.Race).spells)
            {
                using (var scope = new DataAccessScope())
                {
                    var spell = model.CharactersSpells.Create();
                    spell.character = character;
                    spell.spell = spellid.id;
                    spell.created_at = ServerDateTime.Now;
                    scope.Complete();
                }
            }

            #endregion

            #region Select spell of class

            foreach (classeSpell spellid in XmlManager.GetClassStats(character.Class).spells)
            {
                using (var scope = new DataAccessScope())
                {
                    var spell = model.CharactersSpells.Create();
                    spell.character = character;
                    spell.spell = spellid.id;
                    spell.created_at = ServerDateTime.Now;
                    scope.Complete();
                }
            }

            #endregion

            #region Select spell combo (race + class)

            foreach (raceClass spellid in XmlManager.GetRaceStats(character.Race).classes)
            {
                if (spellid.id == character.Class.ToString())
                {
                    foreach (raceClassSpell spell2Id in spellid.spells)
                    {
                        using (var scope = new DataAccessScope())
                        {
                            var spell = model.CharactersSpells.Create();
                            spell.character = character;
                            spell.spell = spell2Id.id;
                            spell.created_at = ServerDateTime.Now;
                            scope.Complete();
                        }
                    }
                }
            }

            #endregion
        }

        internal void GeraSkills(Character character)
        {
            #region Select skill of race

            foreach (raceSkill skillId in XmlManager.GetRaceStats(character.Race).skills)
            {
                using (var scope = new DataAccessScope())
                {
                    var skill = model.CharactersSkill.Create();
                    skill.character = character;
                    skill.skill = skillId.id;
                    skill.Max = skillId.max;
                    skill.value = skillId.min;
                    skill.created_at = ServerDateTime.Now;
                    scope.Complete();
                }
            }

            #endregion

            #region Select skill of class

            foreach (classeSkill skillId in XmlManager.GetClassStats(character.Class).skills)
            {
                using (var scope = new DataAccessScope())
                {
                    var skill = model.CharactersSkill.Create();
                    skill.character = character;
                    skill.skill = skillId.id;
                    skill.Max = skillId.max;
                    skill.value = skillId.min;
                    skill.created_at = ServerDateTime.Now;
                    scope.Complete();
                }
            }

            #endregion

            #region Select skill combo (race + class)

            foreach (raceClass skillId in XmlManager.GetRaceStats(character.Race).classes)
            {
                if (skillId.id == character.Class.ToString())
                {
                    foreach (raceClassSkill skill2Id in skillId.skills)
                    {
                        using (var scope = new DataAccessScope())
                        {
                            var skill = model.CharactersSkill.Create();
                            skill.character = character;
                            skill.skill = skill2Id.id;
                            skill.Max = skill2Id.max;
                            skill.value = skill2Id.min;
                            skill.created_at = ServerDateTime.Now;
                            scope.Complete();
                        }
                    }
                }
            }

            #endregion
        }

        internal void GeraActionBar(Character character)
        {
            #region Select ActionBar of Race/Class

            foreach (raceClass classId in XmlManager.GetRaceStats(character.Race).classes)
            {
                if (classId.id == character.Class.ToString())
                {
                    foreach (raceClassAction actionId in classId.actions)
                    {
                        using (var scope = new DataAccessScope())
                        {
                            var skill = model.CharactersActionBar.Create();
                            skill.character = character;
                            skill.Action = actionId.action;
                            skill.Button = actionId.button;
                            skill.Type = actionId.type;
                            skill.created_at = ServerDateTime.Now;
                            scope.Complete();
                        }
                    }
                }
            }

            #endregion
        }

        internal void GeraInventory(Character character)
        {
            CharStartOutfit startItems =
                DatabaseManager.CharStartOutfit.Values.FirstOrDefault(
                    x => x.Match((byte) character.Race, (byte) character.Class, (byte) character.Gender));

            if (startItems == null)
                return;

            for (int i = 0; i < startItems.m_InventoryType.Count(); i++)
            {
                uint entry = 0;

                if (startItems.m_InventoryType[i] < 1 || !uint.TryParse(startItems.m_ItemID[i].ToString(), out entry) ||
                    XmlManager.GetItem(entry) == null)
                {
                    Console.WriteLine($"Item not Found: {entry}");
                    continue;
                }

                if (Template == null)
                    Template = XmlManager.GetItem(entry);

                using (var scope = new DataAccessScope())
                {
                    var item = XmlManager.GetItem(entry);

                    var inventory = model.CharactersInventory.Create();
                        inventory.Character = character;
                        inventory.Item = item.id;
                        inventory.Slot = PrefInvSlot();
                        inventory.created_at = ServerDateTime.Now;
                    scope.Complete();
                }
            }
        }

        public enum InventoryTypes : byte
        {
            NONE_EQUIP = 0x00,
            HEAD = 0x01,
            NECK = 0x02,
            SHOULDER = 0x03,
            BODY = 0x04,
            CHEST = 0x05,
            WAIST = 0x06,
            LEGS = 0x07,
            FEET = 0x08,
            WRIST = 0x09,
            HAND = 0x0A,
            FINGER = 0x0B,
            TRINKET = 0x0C,
            WEAPON = 0x0D,
            SHIELD = 0x0E,
            RANGED = 0x0F,
            CLOAK = 0x10,
            TWOHANDEDWEAPON = 0x11,
            BAG = 0x12,
            TABARD = 0x13,
            ROBE = 0x14,
            WEAPONMAINHAND = 0x15,
            WEAPONOFFHAND = 0x16,
            HOLDABLE = 0x17,
            AMMO = 0x18,
            THROWN = 0x19,
            RANGEDRIGHT = 0x1A,
            NUM_TYPES = 0x1B
        }

        protected uint PrefInvSlot()
        {
            int[] slotTypes = new int[(int)InventoryTypes.NUM_TYPES]{
                (int)InventorySlots.SLOT_INBACKPACK, // NONE EQUIP
	            (int)InventorySlots.SLOT_HEAD,
                (int)InventorySlots.SLOT_NECK,
                (int)InventorySlots.SLOT_SHOULDERS,
                (int)InventorySlots.SLOT_SHIRT,
                (int)InventorySlots.SLOT_CHEST,
                (int)InventorySlots.SLOT_WAIST,
                (int)InventorySlots.SLOT_LEGS,
                (int)InventorySlots.SLOT_FEET,
                (int)InventorySlots.SLOT_WRISTS,
                (int)InventorySlots.SLOT_HANDS,
                (int)InventorySlots.SLOT_FINGERL,
                (int)InventorySlots.SLOT_TRINKETL,
                (int)InventorySlots.SLOT_MAINHAND, // 1h
	            (int)InventorySlots.SLOT_OFFHAND, // shield
	            (int)InventorySlots.SLOT_RANGED,
                (int)InventorySlots.SLOT_BACK,
                (int)InventorySlots.SLOT_MAINHAND, // 2h
	            (int)InventorySlots.SLOT_BAG1,
                (int)InventorySlots.SLOT_TABARD,
                (int)InventorySlots.SLOT_CHEST, // robe
	            (int)InventorySlots.SLOT_MAINHAND, // mainhand
	            (int)InventorySlots.SLOT_OFFHAND, // offhand
	            (int)InventorySlots.SLOT_MAINHAND, // held
	            (int)InventorySlots.SLOT_INBACKPACK, // ammo
	            (int)InventorySlots.SLOT_RANGED, // thrown
	            (int)InventorySlots.SLOT_RANGED // rangedright
            };

            return (uint)slotTypes[Template.Type];
        }
    }

    public enum InventorySlots
    {
        SLOT_HEAD = 0,
        SLOT_NECK = 1,
        SLOT_SHOULDERS = 2,
        SLOT_SHIRT = 3,
        SLOT_CHEST = 4,
        SLOT_WAIST = 5,
        SLOT_LEGS = 6,
        SLOT_FEET = 7,
        SLOT_WRISTS = 8,
        SLOT_HANDS = 9,
        SLOT_FINGERL = 10,
        SLOT_FINGERR = 11,
        SLOT_TRINKETL = 12,
        SLOT_TRINKETR = 13,
        SLOT_BACK = 14,
        SLOT_MAINHAND = 15,
        SLOT_OFFHAND = 16,
        SLOT_RANGED = 17,
        SLOT_TABARD = 18,

        //! Misc Types
        SLOT_BAG1 = 19,
        SLOT_BAG2 = 20,
        SLOT_BAG3 = 21,
        SLOT_BAG4 = 22,
        SLOT_INBACKPACK = 23,

        SLOT_ITEM_START = 23,
        SLOT_ITEM_END = 39,

        SLOT_BANK_ITEM_START = 39,
        SLOT_BANK_ITEM_END = 63,
        SLOT_BANK_BAG_1 = 63,
        SLOT_BANK_BAG_2 = 64,
        SLOT_BANK_BAG_3 = 65,
        SLOT_BANK_BAG_4 = 66,
        SLOT_BANK_BAG_5 = 67,
        SLOT_BANK_BAG_6 = 68,
        SLOT_BANK_END = 69
    }
}
