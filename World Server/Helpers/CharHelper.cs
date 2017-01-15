﻿using System;
using System.Linq;
using Framework.Contants.Character;
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

            // Adiciona Items
            for (int j = 0; j < 12; ++j)
            {
                if (startItems.m_ItemID[j] <= 0)
                    continue;

                uint itemId = (uint)startItems.m_ItemID[j];

                var item = XmlManager.GetItem(itemId);

                if (item == null)
                {
                    Console.WriteLine($"Item nao existe [{itemId}]");
                    continue;
                }

                // special amount for foor/drink
                if (item?.Class == 0) // && Item.SubClass == 5)
                {

                }
                // Adiciona todos os items aqui

                using (var scope = new DataAccessScope())
                {
                    var inventory = model.CharactersInventory.Create();
                        inventory.Character = character;
                        inventory.Item = item.id;
                        inventory.Stack = 1;
                        inventory.Slot = PrefInvSlot(item);
                        inventory.created_at = ServerDateTime.Now;
                    scope.Complete();
                }
            }

            for (int i = 23; i < 39; ++i)
            {
                /*
                if (Item * pItem = GetItemByPos(INVENTORY_SLOT_BAG_0, i))
                {
                    uint16 eDest;
                    // equip offhand weapon/shield if it attempt equipped before main-hand weapon
                    InventoryResult msg = CanEquipItem(NULL_SLOT, eDest, pItem, false);
                    if (msg == EQUIP_ERR_OK)
                    {
                        RemoveItem(INVENTORY_SLOT_BAG_0, i, true);
                        EquipItem(eDest, pItem, true);
                    }
                    // move other items to more appropriate slots (ammo not equipped in special bag)
                    else
                    {
                        ItemPosCountVec sDest;
                        msg = CanStoreItem(NULL_BAG, NULL_SLOT, sDest, pItem, false);
                        if (msg == EQUIP_ERR_OK)
                        {
                            RemoveItem(INVENTORY_SLOT_BAG_0, i, true);
                            pItem = StoreItem(sDest, pItem, true);
                        }

                        // if  this is ammo then use it
                        msg = CanUseAmmo(pItem->GetEntry());
                        if (msg == EQUIP_ERR_OK)
                            SetAmmo(pItem->GetEntry());
                    }
                }
                */
            }
        }

        private uint PrefInvSlot(itemsItem item)
        {
            int[] slotTypes = {
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

            return (uint)slotTypes[item.Type];
        }
    }
}
