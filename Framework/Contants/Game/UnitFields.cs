﻿namespace Framework.Contants.Game
{

    public enum EObjectFields
    {
        OBJECT_FIELD_GUID = 0x00, // Size:2
        OBJECT_FIELD_TYPE = 0x02, // Size:1
        OBJECT_FIELD_ENTRY = 0x03, // Size:1
        OBJECT_FIELD_SCALE_X = 0x04, // Size:1
        OBJECT_FIELD_PADDING = 0x05, // Size:1
        OBJECT_END = 0x06,
    };

    public enum EItemFields
    {
        ITEM_FIELD_OWNER = EObjectFields.OBJECT_END + 0x00, // Size:2
        ITEM_FIELD_CONTAINED = EObjectFields.OBJECT_END + 0x02, // Size:2
        ITEM_FIELD_CREATOR = EObjectFields.OBJECT_END + 0x04, // Size:2
        ITEM_FIELD_GIFTCREATOR = EObjectFields.OBJECT_END + 0x06, // Size:2
        ITEM_FIELD_STACK_COUNT = EObjectFields.OBJECT_END + 0x08, // Size:1
        ITEM_FIELD_DURATION = EObjectFields.OBJECT_END + 0x09, // Size:1
        ITEM_FIELD_SPELL_CHARGES = EObjectFields.OBJECT_END + 0x0A, // Size:5
        ITEM_FIELD_SPELL_CHARGES_01 = EObjectFields.OBJECT_END + 0x0B,
        ITEM_FIELD_SPELL_CHARGES_02 = EObjectFields.OBJECT_END + 0x0C,
        ITEM_FIELD_SPELL_CHARGES_03 = EObjectFields.OBJECT_END + 0x0D,
        ITEM_FIELD_SPELL_CHARGES_04 = EObjectFields.OBJECT_END + 0x0E,
        ITEM_FIELD_FLAGS = EObjectFields.OBJECT_END + 0x0F, // Size:1
        ITEM_FIELD_ENCHANTMENT = EObjectFields.OBJECT_END + 0x10, // count=21
        ITEM_FIELD_PROPERTY_SEED = EObjectFields.OBJECT_END + 0x25, // Size:1
        ITEM_FIELD_RANDOM_PROPERTIES_ID = EObjectFields.OBJECT_END + 0x26, // Size:1
        ITEM_FIELD_ITEM_TEXT_ID = EObjectFields.OBJECT_END + 0x27, // Size:1
        ITEM_FIELD_DURABILITY = EObjectFields.OBJECT_END + 0x28, // Size:1
        ITEM_FIELD_MAXDURABILITY = EObjectFields.OBJECT_END + 0x29, // Size:1
        ITEM_END = EObjectFields.OBJECT_END + 0x2A,
    };

    public enum EContainerFields
    {
        CONTAINER_FIELD_NUM_SLOTS = EItemFields.ITEM_END + 0x00, // Size:1
        CONTAINER_ALIGN_PAD = EItemFields.ITEM_END + 0x01, // Size:1
        CONTAINER_FIELD_SLOT_1 = EItemFields.ITEM_END + 0x02, // count=56
        CONTAINER_FIELD_SLOT_LAST = EItemFields.ITEM_END + 0x38,
        CONTAINER_END = EItemFields.ITEM_END + 0x3A,
    };

    public enum EUnitFields
    {
        UNIT_FIELD_CHARM = 0x00 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_SUMMON = 0x02 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_CHARMEDBY = 0x04 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_SUMMONEDBY = 0x06 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_CREATEDBY = 0x08 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_TARGET = 0x0A + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_PERSUADED = 0x0C + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_CHANNEL_OBJECT = 0x0E + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_HEALTH = 0x10 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER1 = 0x11 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER2 = 0x12 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER3 = 0x13 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER4 = 0x14 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER5 = 0x15 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXHEALTH = 0x16 + EObjectFields.OBJECT_END, // Size:1 
        UNIT_FIELD_MAXPOWER1 = 0x17 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER2 = 0x18 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER3 = 0x19 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER4 = 0x1A + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER5 = 0x1B + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_LEVEL = 0x1C + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_FACTIONTEMPLATE = 0x1D + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BYTES_0 = 0x1E + EObjectFields.OBJECT_END, // Size:1
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY = 0x1F + EObjectFields.OBJECT_END, // Size:3
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_01 = 0x20 + EObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_02 = 0x21 + EObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO = 0x22 + EObjectFields.OBJECT_END, // Size:6
        UNIT_VIRTUAL_ITEM_INFO_01 = 0x23 + EObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_02 = 0x24 + EObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_03 = 0x25 + EObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_04 = 0x26 + EObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_05 = 0x27 + EObjectFields.OBJECT_END,
        UNIT_FIELD_FLAGS = 0x28 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_AURA = 0x29 + EObjectFields.OBJECT_END, // Size:48
        UNIT_FIELD_AURA_LAST = 0x58 + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS = 0x59 + EObjectFields.OBJECT_END, // Size:6
        UNIT_FIELD_AURAFLAGS_01 = 0x5a + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_02 = 0x5b + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_03 = 0x5c + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_04 = 0x5d + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_05 = 0x5e + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURALEVELS = 0x5f + EObjectFields.OBJECT_END, // Size:12
        UNIT_FIELD_AURALEVELS_LAST = 0x6a + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURAAPPLICATIONS = 0x6b + EObjectFields.OBJECT_END, // Size:12
        UNIT_FIELD_AURAAPPLICATIONS_LAST = 0x76 + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURASTATE = 0x77 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BASEATTACKTIME = 0x78 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_OFFHANDATTACKTIME = 0x79 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_RANGEDATTACKTIME = 0x7a + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BOUNDINGRADIUS = 0x7b + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_COMBATREACH = 0x7c + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_DISPLAYID = 0x7d + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_NATIVEDISPLAYID = 0x7e + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MOUNTDISPLAYID = 0x7f + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MINDAMAGE = 0x80 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXDAMAGE = 0x81 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MINOFFHANDDAMAGE = 0x82 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXOFFHANDDAMAGE = 0x83 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BYTES_1 = 0x84 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PETNUMBER = 0x85 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PET_NAME_TIMESTAMP = 0x86 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PETEXPERIENCE = 0x87 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PETNEXTLEVELEXP = 0x88 + EObjectFields.OBJECT_END, // Size:1
        UNIT_DYNAMIC_FLAGS = 0x89 + EObjectFields.OBJECT_END, // Size:1
        UNIT_CHANNEL_SPELL = 0x8a + EObjectFields.OBJECT_END, // Size:1
        UNIT_MOD_CAST_SPEED = 0x8b + EObjectFields.OBJECT_END, // Size:1
        UNIT_CREATED_BY_SPELL = 0x8c + EObjectFields.OBJECT_END, // Size:1
        UNIT_NPC_FLAGS = 0x8d + EObjectFields.OBJECT_END, // Size:1
        UNIT_NPC_EMOTESTATE = 0x8e + EObjectFields.OBJECT_END, // Size:1
        UNIT_TRAINING_POINTS = 0x8f + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT0 = 0x90 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT1 = 0x91 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT2 = 0x92 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT3 = 0x93 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT4 = 0x94 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RESISTANCES = 0x95 + EObjectFields.OBJECT_END, // Size:7
        UNIT_FIELD_RESISTANCES_01 = 0x96 + EObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_02 = 0x97 + EObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_03 = 0x98 + EObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_04 = 0x99 + EObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_05 = 0x9a + EObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_06 = 0x9b + EObjectFields.OBJECT_END,
        UNIT_FIELD_BASE_MANA = 0x9c + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BASE_HEALTH = 0x9d + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BYTES_2 = 0x9e + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_ATTACK_POWER = 0x9f + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_ATTACK_POWER_MODS = 0xa0 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER = 0xa1 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER = 0xa2 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER_MODS = 0xa3 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER = 0xa4 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MINRANGEDDAMAGE = 0xa5 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXRANGEDDAMAGE = 0xa6 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER_COST_MODIFIER = 0xa7 + EObjectFields.OBJECT_END, // Size:7
        UNIT_FIELD_POWER_COST_MODIFIER_01 = 0xa8 + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_02 = 0xa9 + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_03 = 0xaa + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_04 = 0xab + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_05 = 0xac + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_06 = 0xad + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER = 0xae + EObjectFields.OBJECT_END, // Size:7
        UNIT_FIELD_POWER_COST_MULTIPLIER_01 = 0xaf + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_02 = 0xb0 + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_03 = 0xb1 + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_04 = 0xb2 + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_05 = 0xb3 + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_06 = 0xb4 + EObjectFields.OBJECT_END,
        UNIT_FIELD_PADDING = 0xb5 + EObjectFields.OBJECT_END,
        UNIT_END = 0xb6 + EObjectFields.OBJECT_END,

        PLAYER_DUEL_ARBITER = 0x00 + EUnitFields.UNIT_END, // Size:2
        PLAYER_FLAGS = 0x02 + EUnitFields.UNIT_END, // Size:1
        PLAYER_GUILDID = 0x03 + EUnitFields.UNIT_END, // Size:1
        PLAYER_GUILDRANK = 0x04 + EUnitFields.UNIT_END, // Size:1
        PLAYER_BYTES = 0x05 + EUnitFields.UNIT_END, // Size:1
        PLAYER_BYTES_2 = 0x06 + EUnitFields.UNIT_END, // Size:1
        PLAYER_BYTES_3 = 0x07 + EUnitFields.UNIT_END, // Size:1
        PLAYER_DUEL_TEAM = 0x08 + EUnitFields.UNIT_END, // Size:1
        PLAYER_GUILD_TIMESTAMP = 0x09 + EUnitFields.UNIT_END, // Size:1
        PLAYER_QUEST_LOG_1_1 = 0x0A + EUnitFields.UNIT_END, // count = 20
        PLAYER_QUEST_LOG_1_2 = 0x0B + EUnitFields.UNIT_END,
        PLAYER_QUEST_LOG_1_3 = 0x0C + EUnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_1 = 0x43 + EUnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_2 = 0x44 + EUnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_3 = 0x45 + EUnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_1_CREATOR = 0x46 + EUnitFields.UNIT_END, // Size:2, count = 19
        PLAYER_VISIBLE_ITEM_1_0 = 0x48 + EUnitFields.UNIT_END, // Size:8
        PLAYER_VISIBLE_ITEM_1_PROPERTIES = 0x50 + EUnitFields.UNIT_END, // Size:1
        PLAYER_VISIBLE_ITEM_1_PAD = 0x51 + EUnitFields.UNIT_END, // Size:1
        PLAYER_VISIBLE_ITEM_LAST_CREATOR = 0x11e + EUnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_0 = 0x120 + EUnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_PROPERTIES = 0x128 + EUnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_PAD = 0x129 + EUnitFields.UNIT_END,
        PLAYER_FIELD_INV_SLOT_HEAD = 0x12a + EUnitFields.UNIT_END, // Size:46
        PLAYER_FIELD_PACK_SLOT_1 = 0x158 + EUnitFields.UNIT_END, // Size:32
        PLAYER_FIELD_PACK_SLOT_LAST = 0x176 + EUnitFields.UNIT_END,
        PLAYER_FIELD_BANK_SLOT_1 = 0x178 + EUnitFields.UNIT_END, // Size:48
        PLAYER_FIELD_BANK_SLOT_LAST = 0x1a6 + EUnitFields.UNIT_END,
        PLAYER_FIELD_BANKBAG_SLOT_1 = 0x1a8 + EUnitFields.UNIT_END, // Size:12
        PLAYER_FIELD_BANKBAG_SLOT_LAST = 0xab2 + EUnitFields.UNIT_END,
        PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = 0x1b4 + EUnitFields.UNIT_END, // Size:24
        PLAYER_FIELD_VENDORBUYBACK_SLOT_LAST = 0x1ca + EUnitFields.UNIT_END,
        PLAYER_FIELD_KEYRING_SLOT_1 = 0x1cc + EUnitFields.UNIT_END, // Size:64
        PLAYER_FIELD_KEYRING_SLOT_LAST = 0x20a + EUnitFields.UNIT_END,
        PLAYER_FARSIGHT = 0x20c + EUnitFields.UNIT_END, // Size:2
        PLAYER_FIELD_COMBO_TARGET = 0x20e + EUnitFields.UNIT_END, // Size:2
        PLAYER_XP = 0x210 + EUnitFields.UNIT_END, // Size:1
        PLAYER_NEXT_LEVEL_XP = 0x211 + EUnitFields.UNIT_END, // Size:1
        PLAYER_SKILL_INFO_1_1 = 0x212 + EUnitFields.UNIT_END, // Size:384
        PLAYER_SKILL_PROP_1_1 = 0x213 + EUnitFields.UNIT_END, // Size:384

        PLAYER_CHARACTER_POINTS1 = 0x392 + EUnitFields.UNIT_END, // Size:1
        PLAYER_CHARACTER_POINTS2 = 0x393 + EUnitFields.UNIT_END, // Size:1
        PLAYER_TRACK_CREATURES = 0x394 + EUnitFields.UNIT_END, // Size:1
        PLAYER_TRACK_RESOURCES = 0x395 + EUnitFields.UNIT_END, // Size:1
        PLAYER_BLOCK_PERCENTAGE = 0x396 + EUnitFields.UNIT_END, // Size:1
        PLAYER_DODGE_PERCENTAGE = 0x397 + EUnitFields.UNIT_END, // Size:1
        PLAYER_PARRY_PERCENTAGE = 0x398 + EUnitFields.UNIT_END, // Size:1
        PLAYER_CRIT_PERCENTAGE = 0x399 + EUnitFields.UNIT_END, // Size:1
        PLAYER_RANGED_CRIT_PERCENTAGE = 0x39a + EUnitFields.UNIT_END, // Size:1
        PLAYER_EXPLORED_ZONES_1 = 0x39b + EUnitFields.UNIT_END, // Size:64
        PLAYER_REST_STATE_EXPERIENCE = 0x3db + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_COINAGE = 0x3dc + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT0 = 0x3DD + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT1 = 0x3DE + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT2 = 0x3DF + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT3 = 0x3E0 + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT4 = 0x3E1 + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT0 = 0x3E2 + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT1 = 0x3E3 + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT2 = 0x3E4 + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT3 = 0x3E5 + EUnitFields.UNIT_END, // Size:1,
        PLAYER_FIELD_NEGSTAT4 = 0x3E6 + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE = 0x3E7 + EUnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE = 0x3EE + EUnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS = 0x3F5 + EUnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = 0x3FC + EUnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = 0x403 + EUnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_BYTES = 0x40A + EUnitFields.UNIT_END, // Size:1
        PLAYER_AMMO_ID = 0x40B + EUnitFields.UNIT_END, // Size:1
        PLAYER_SELF_RES_SPELL = 0x40C + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_PVP_MEDALS = 0x40D + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_BUYBACK_PRICE_1 = 0x40E + EUnitFields.UNIT_END, // count=12
        PLAYER_FIELD_BUYBACK_PRICE_LAST = 0x419 + EUnitFields.UNIT_END,
        PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = 0x41A + EUnitFields.UNIT_END, // count=12
        PLAYER_FIELD_BUYBACK_TIMESTAMP_LAST = 0x425 + EUnitFields.UNIT_END,
        PLAYER_FIELD_SESSION_KILLS = 0x426 + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_YESTERDAY_KILLS = 0x427 + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_KILLS = 0x428 + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_THIS_WEEK_KILLS = 0x429 + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_THIS_WEEK_CONTRIBUTION = 0x42a + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LIFETIME_HONORABLE_KILLS = 0x42b + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LIFETIME_DISHONORABLE_KILLS = 0x42c + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_YESTERDAY_CONTRIBUTION = 0x42d + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_CONTRIBUTION = 0x42e + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_RANK = 0x42f + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_BYTES2 = 0x430 + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_WATCHED_FACTION_INDEX = 0x431 + EUnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_COMBAT_RATING_1 = 0x432 + EUnitFields.UNIT_END, // Size:20

        PLAYER_END = 0x446 + EUnitFields.UNIT_END
    };

    public enum EGameObjectFields
    {
        OBJECT_FIELD_CREATED_BY = EObjectFields.OBJECT_END + 0x00,
        GAMEOBJECT_DISPLAYID = EObjectFields.OBJECT_END + 0x02,
        GAMEOBJECT_FLAGS = EObjectFields.OBJECT_END + 0x03,
        GAMEOBJECT_ROTATION = EObjectFields.OBJECT_END + 0x04,
        GAMEOBJECT_STATE = EObjectFields.OBJECT_END + 0x08,
        GAMEOBJECT_POS_X = EObjectFields.OBJECT_END + 0x09,
        GAMEOBJECT_POS_Y = EObjectFields.OBJECT_END + 0x0A,
        GAMEOBJECT_POS_Z = EObjectFields.OBJECT_END + 0x0B,
        GAMEOBJECT_FACING = EObjectFields.OBJECT_END + 0x0C,
        GAMEOBJECT_DYN_FLAGS = EObjectFields.OBJECT_END + 0x0D,
        GAMEOBJECT_FACTION = EObjectFields.OBJECT_END + 0x0E,
        GAMEOBJECT_TYPE_ID = EObjectFields.OBJECT_END + 0x0F,
        GAMEOBJECT_LEVEL = EObjectFields.OBJECT_END + 0x10,
        GAMEOBJECT_ARTKIT = EObjectFields.OBJECT_END + 0x11,
        GAMEOBJECT_ANIMPROGRESS = EObjectFields.OBJECT_END + 0x12,
        GAMEOBJECT_PADDING = EObjectFields.OBJECT_END + 0x13,
        GAMEOBJECT_END = EObjectFields.OBJECT_END + 0x14,
    };

    public enum EDynamicObjectFields
    {
        DYNAMICOBJECT_CASTER = EObjectFields.OBJECT_END + 0x00,
        DYNAMICOBJECT_BYTES = EObjectFields.OBJECT_END + 0x02,
        DYNAMICOBJECT_SPELLID = EObjectFields.OBJECT_END + 0x03,
        DYNAMICOBJECT_RADIUS = EObjectFields.OBJECT_END + 0x04,
        DYNAMICOBJECT_POS_X = EObjectFields.OBJECT_END + 0x05,
        DYNAMICOBJECT_POS_Y = EObjectFields.OBJECT_END + 0x06,
        DYNAMICOBJECT_POS_Z = EObjectFields.OBJECT_END + 0x07,
        DYNAMICOBJECT_FACING = EObjectFields.OBJECT_END + 0x08,
        DYNAMICOBJECT_PAD = EObjectFields.OBJECT_END + 0x09,
        DYNAMICOBJECT_END = EObjectFields.OBJECT_END + 0x0A,
    };

    public enum ECorpseFields
    {
        CORPSE_FIELD_OWNER = EObjectFields.OBJECT_END + 0x00,
        CORPSE_FIELD_FACING = EObjectFields.OBJECT_END + 0x02,
        CORPSE_FIELD_POS_X = EObjectFields.OBJECT_END + 0x03,
        CORPSE_FIELD_POS_Y = EObjectFields.OBJECT_END + 0x04,
        CORPSE_FIELD_POS_Z = EObjectFields.OBJECT_END + 0x05,
        CORPSE_FIELD_DISPLAY_ID = EObjectFields.OBJECT_END + 0x06,
        CORPSE_FIELD_ITEM = EObjectFields.OBJECT_END + 0x07, // 19
        CORPSE_FIELD_BYTES_1 = EObjectFields.OBJECT_END + 0x1A,
        CORPSE_FIELD_BYTES_2 = EObjectFields.OBJECT_END + 0x1B,
        CORPSE_FIELD_GUILD = EObjectFields.OBJECT_END + 0x1C,
        CORPSE_FIELD_FLAGS = EObjectFields.OBJECT_END + 0x1D,
        CORPSE_FIELD_DYNAMIC_FLAGS = EObjectFields.OBJECT_END + 0x1E,
        CORPSE_FIELD_PAD = EObjectFields.OBJECT_END + 0x1F,
        CORPSE_END = EObjectFields.OBJECT_END + 0x20,
    };
}
