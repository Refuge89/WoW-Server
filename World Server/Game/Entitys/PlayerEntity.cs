using Framework.Contants.Character;
using Framework.Contants.Game;
using Framework.Database;
using Framework.Database.Tables;
using System;

namespace World_Server.Game.Entitys
{
    public class PlayerEntity : WorldObject
    {
        public static int GetModel(Character character)
        {
            var CharModel = Program.Database.GetCharStarter(character.Race);

            return character.Gender == GenderID.MALE ? CharModel.ModelM : CharModel.ModelF;
        }

        public static int GetAttribute(Character character, string attribute)
        {
            var AttRace = XmlManager.getRaceStats(character.Race);
            var AttClas = XmlManager.getClassStats(character.Class);
            //var AttChar = XmlManager.getRaceStats(race);

            if(attribute == "health")
                return AttRace.health + AttClas.health;

            if (attribute == "strength")
                return AttRace.stats.strength + AttClas.stats.strength;

            if (attribute == "agility")
                return AttRace.stats.agility + AttClas.stats.agility;

            if (attribute == "intellect")
                return AttRace.stats.intellect + AttClas.stats.intellect;

            if (attribute == "stamina")
                return AttRace.stats.stamina + AttClas.stats.stamina;

            if (attribute == "spirit")
                return AttRace.stats.spirit + AttClas.stats.spirit;

            return 1;
        }
                        

        public PlayerEntity(Character character) : base((int)EUnitFields.PLAYER_END - 0x4)
        {
            var Skin = Program.Database.GetSkin(character);
            SetUpdateField((int)EObjectFields.OBJECT_FIELD_GUID,                character.Id);  // Id do Objeto
            SetUpdateField((int)EObjectFields.OBJECT_FIELD_SCALE_X,             1f);            // Scala do Objeto "Tauren
            SetUpdateField((int)EObjectFields.OBJECT_FIELD_TYPE,                25);



            SetUpdateField((int)EUnitFields.UNIT_FIELD_HEALTH,                  100);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_MAXHEALTH,               1000);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_BASE_HEALTH,             10000); 

            SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_LEVEL,              character.Level);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_FACTIONTEMPLATE,         5);

            SetUpdateField((int)EUnitFields.UNIT_FIELD_BYTES_0,                 (byte)character.Race, 0);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_BYTES_0,                 (byte)character.Class, 1);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_BYTES_0,                 (byte)character.Gender, 2);

            SetUpdateField((int)EUnitFields.UNIT_FIELD_DISPLAYID,               GetModel(character));
            SetUpdateField((int)EUnitFields.UNIT_FIELD_NATIVEDISPLAYID,         GetModel(character));
            SetUpdateField((int)EUnitFields.PLAYER_BYTES,                       Skin.Skin, 0);
            SetUpdateField((int)EUnitFields.PLAYER_BYTES,                       Skin.Face, 1);
            SetUpdateField((int)EUnitFields.PLAYER_BYTES,                       Skin.HairStyle, 2);
            SetUpdateField((int)EUnitFields.PLAYER_BYTES,                       Skin.HairColor, 3);
            


            // FIXME: outfitId not used in player creating
/*


for (int i = 0; i < PLAYER_SLOTS_COUNT; ++i)
m_items[i] = nullptr;

SetLocationMapId(info->mapId);
Relocate(info->positionX, info->positionY, info->positionZ, info->orientation);

SetMap(sMapMgr.CreateMap(info->mapId, this));

uint8 powertype = cEntry->powerType;



SetByteValue(UNIT_FIELD_BYTES_0, 0, race);
SetByteValue(UNIT_FIELD_BYTES_0, 1, class_);
SetByteValue(UNIT_FIELD_BYTES_0, 2, gender);
SetByteValue(UNIT_FIELD_BYTES_0, 3, powertype);

InitDisplayIds();                                       // model, scale and model data

// is it need, only in pre-2.x used and field byte removed later?
if (powertype == POWER_RAGE || powertype == POWER_MANA)
SetByteValue(UNIT_FIELD_BYTES_1, 1, 0xEE);

SetByteValue(UNIT_FIELD_BYTES_2, 1, UNIT_BYTE2_FLAG_SUPPORTABLE | UNIT_BYTE2_FLAG_UNK5);
SetUInt32Value(UNIT_FIELD_FLAGS, UNIT_FLAG_PVP_ATTACKABLE);
SetFloatValue(UNIT_MOD_CAST_SPEED, 1.0f);               // fix cast time showed in spell tooltip on client

SetInt32Value(PLAYER_FIELD_WATCHED_FACTION_INDEX, -1);  // -1 is default value

SetByteValue(PLAYER_BYTES, 0, skin);
SetByteValue(PLAYER_BYTES, 1, face);
SetByteValue(PLAYER_BYTES, 2, hairStyle);
SetByteValue(PLAYER_BYTES, 3, hairColor);

SetByteValue(PLAYER_BYTES_2, 0, facialHair);
SetByteValue(PLAYER_BYTES_2, 3, REST_STATE_NORMAL);

SetUInt16Value(PLAYER_BYTES_3, 0, gender);              // only GENDER_MALE/GENDER_FEMALE (1 bit) allowed, drunk state = 0
SetByteValue(PLAYER_BYTES_3, 3, 0);                     // BattlefieldArenaFaction (0 or 1)

SetUInt32Value(PLAYER_GUILDID, 0);
SetUInt32Value(PLAYER_GUILDRANK, 0);
SetUInt32Value(PLAYER_GUILD_TIMESTAMP, 0);

// set starting level
if (GetSession()->GetSecurity() >= SEC_MODERATOR)
SetUInt32Value(UNIT_FIELD_LEVEL, sWorld.getConfig(CONFIG_UINT32_START_GM_LEVEL));
else
SetUInt32Value(UNIT_FIELD_LEVEL, sWorld.getConfig(CONFIG_UINT32_START_PLAYER_LEVEL));

SetUInt32Value(PLAYER_FIELD_COINAGE, sWorld.getConfig(CONFIG_UINT32_START_PLAYER_MONEY));

// Played time
m_Last_tick = time(nullptr);
m_Played_time[PLAYED_TIME_TOTAL] = 0;
m_Played_time[PLAYED_TIME_LEVEL] = 0;

// base stats and related field values
InitStatsForLevel();
InitTaxiNodes();
InitTalentForLevel();
InitPrimaryProfessions();                               // to max set before any spell added

// apply original stats mods before spell loading or item equipment that call before equip _RemoveStatsMods()
UpdateMaxHealth();                                      // Update max Health (for add bonus from stamina)
SetHealth(GetMaxHealth());

if (GetPowerType() == POWER_MANA)
{
UpdateMaxPower(POWER_MANA);                         // Update max Mana (for add bonus from intellect)
SetPower(POWER_MANA, GetMaxPower(POWER_MANA));
}

// original spells
learnDefaultSpells();

// original action bar
for (PlayerCreateInfoActions::const_iterator action_itr = info->action.begin(); action_itr != info->action.end(); ++action_itr)
addActionButton(action_itr->button, action_itr->action, action_itr->type);

// original items
uint32 raceClassGender = GetUInt32Value(UNIT_FIELD_BYTES_0) & 0x00FFFFFF;

CharStartOutfitEntry const* oEntry = nullptr;
for (uint32 i = 1; i < sCharStartOutfitStore.GetNumRows(); ++i)
{
if (CharStartOutfitEntry const* entry = sCharStartOutfitStore.LookupEntry(i))
{
if (entry->RaceClassGender == raceClassGender)
{
oEntry = entry;
break;
}
}
}

if (oEntry)
{
for (int j = 0; j<MAX_OUTFIT_ITEMS; ++j)
{
if (oEntry->ItemId[j] <= 0)
continue;

uint32 item_id = oEntry->ItemId[j];

// just skip, reported in ObjectMgr::LoadItemPrototypes
ItemPrototype const* iProto = ObjectMgr::GetItemPrototype(item_id);
if (!iProto)
continue;

// BuyCount by default
int32 count = iProto->BuyCount;

// special amount for foor/drink
if (iProto->Class == ITEM_CLASS_CONSUMABLE && iProto->SubClass == ITEM_SUBCLASS_FOOD)
{
switch (iProto->Spells[0].SpellCategory)
{
case 11:                                // food
if (iProto->Stackable > 4)
count = 4;
break;
case 59:                                // drink
if (iProto->Stackable > 2)
count = 2;
break;
}
}

StoreNewItemInBestSlots(item_id, count);
}
}

for (PlayerCreateInfoItems::const_iterator item_id_itr = info->item.begin(); item_id_itr != info->item.end(); ++item_id_itr)
StoreNewItemInBestSlots(item_id_itr->item_id, item_id_itr->item_amount);

// bags and main-hand weapon must equipped at this moment
// now second pass for not equipped (offhand weapon/shield if it attempt equipped before main-hand weapon)
// or ammo not equipped in special bag
for (int i = INVENTORY_SLOT_ITEM_START; i<INVENTORY_SLOT_ITEM_END; ++i)
{
if (Item* pItem = GetItemByPos(INVENTORY_SLOT_BAG_0, i))
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
}
// all item positions resolved
*/
        }
    }
}
