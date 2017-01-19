﻿using System;

namespace Framework.Contants.Game
{

    public enum GameObjectType : byte
    {
        GAMEOBJECT_TYPE_DOOR = 0,
        GAMEOBJECT_TYPE_BUTTON = 1,
        GAMEOBJECT_TYPE_QUESTGIVER = 2,
        GAMEOBJECT_TYPE_CHEST = 3,
        GAMEOBJECT_TYPE_BINDER = 4,
        GAMEOBJECT_TYPE_GENERIC = 5,
        GAMEOBJECT_TYPE_TRAP = 6,
        GAMEOBJECT_TYPE_CHAIR = 7,
        GAMEOBJECT_TYPE_SPELL_FOCUS = 8,
        GAMEOBJECT_TYPE_TEXT = 9,
        GAMEOBJECT_TYPE_GOOBER = 10,
        GAMEOBJECT_TYPE_TRANSPORT = 11,
        GAMEOBJECT_TYPE_AREADAMAGE = 12,
        GAMEOBJECT_TYPE_CAMERA = 13,
        GAMEOBJECT_TYPE_MAPOBJECT = 14,
        GAMEOBJECT_TYPE_MO_TRANSPORT = 15,
        GAMEOBJECT_TYPE_DUELFLAG = 16,
        GAMEOBJECT_TYPE_FISHINGNODE = 17,
        GAMEOBJECT_TYPE_SUMMONING_RITUAL = 18,
        GAMEOBJECT_TYPE_MAILBOX = 19,
        GAMEOBJECT_TYPE_DONOTUSE = 20,
        GAMEOBJECT_TYPE_GUARDPOST = 21,
        GAMEOBJECT_TYPE_SPELLCASTER = 22,
        GAMEOBJECT_TYPE_MEETINGSTONE = 23,
        GAMEOBJECT_TYPE_FLAGSTAND = 24,
        GAMEOBJECT_TYPE_FISHINGHOLE = 25,
        GAMEOBJECT_TYPE_FLAGDROP = 26,
        GAMEOBJECT_TYPE_MINI_GAME = 27,
        GAMEOBJECT_TYPE_LOTTERYKIOSK = 28,
        GAMEOBJECT_TYPE_CAPTURE_POINT = 29,
        GAMEOBJECT_TYPE_AURA_GENERATOR = 30,
        GAMEOBJECT_TYPE_DUNGEON_DIFFICULTY = 31,
        GAMEOBJECT_TYPE_BARBER_CHAIR = 32,
        GAMEOBJECT_TYPE_DESTRUCTIBLE_BUILDING = 33,
        GAMEOBJECT_TYPE_GUILD_BANK = 34,
        GAMEOBJECT_TYPE_TRAPDOOR = 35
    }

    public enum GameObjectFields
    {
        OBJECT_FIELD_CREATED_BY = ObjectFields.OBJECT_END + 0x00,
        GAMEOBJECT_DISPLAYID = ObjectFields.OBJECT_END + 0x02,
        GAMEOBJECT_FLAGS = ObjectFields.OBJECT_END + 0x03,
        GAMEOBJECT_ROTATION = ObjectFields.OBJECT_END + 0x04,
        GAMEOBJECT_STATE = ObjectFields.OBJECT_END + 0x08,
        GAMEOBJECT_POS_X = ObjectFields.OBJECT_END + 0x09,
        GAMEOBJECT_POS_Y = ObjectFields.OBJECT_END + 0x0A,
        GAMEOBJECT_POS_Z = ObjectFields.OBJECT_END + 0x0B,
        GAMEOBJECT_FACING = ObjectFields.OBJECT_END + 0x0C,
        GAMEOBJECT_DYN_FLAGS = ObjectFields.OBJECT_END + 0x0D,
        GAMEOBJECT_FACTION = ObjectFields.OBJECT_END + 0x0E,
        GAMEOBJECT_TYPE_ID = ObjectFields.OBJECT_END + 0x0F,
        GAMEOBJECT_LEVEL = ObjectFields.OBJECT_END + 0x10,
        GAMEOBJECT_ARTKIT = ObjectFields.OBJECT_END + 0x11,
        GAMEOBJECT_ANIMPROGRESS = ObjectFields.OBJECT_END + 0x12,
        GAMEOBJECT_PADDING = ObjectFields.OBJECT_END + 0x13,
        GAMEOBJECT_END = ObjectFields.OBJECT_END + 0x14,
    };

    public enum DynamicObjectFields
    {
        DYNAMICOBJECT_CASTER = ObjectFields.OBJECT_END + 0x00,
        DYNAMICOBJECT_BYTES = ObjectFields.OBJECT_END + 0x02,
        DYNAMICOBJECT_SPELLID = ObjectFields.OBJECT_END + 0x03,
        DYNAMICOBJECT_RADIUS = ObjectFields.OBJECT_END + 0x04,
        DYNAMICOBJECT_POS_X = ObjectFields.OBJECT_END + 0x05,
        DYNAMICOBJECT_POS_Y = ObjectFields.OBJECT_END + 0x06,
        DYNAMICOBJECT_POS_Z = ObjectFields.OBJECT_END + 0x07,
        DYNAMICOBJECT_FACING = ObjectFields.OBJECT_END + 0x08,
        DYNAMICOBJECT_PAD = ObjectFields.OBJECT_END + 0x09,
        DYNAMICOBJECT_END = ObjectFields.OBJECT_END + 0x0A,
    };

    public enum CorpseFields
    {
        CORPSE_FIELD_OWNER = ObjectFields.OBJECT_END + 0x00,
        CORPSE_FIELD_FACING = ObjectFields.OBJECT_END + 0x02,
        CORPSE_FIELD_POS_X = ObjectFields.OBJECT_END + 0x03,
        CORPSE_FIELD_POS_Y = ObjectFields.OBJECT_END + 0x04,
        CORPSE_FIELD_POS_Z = ObjectFields.OBJECT_END + 0x05,
        CORPSE_FIELD_DISPLAY_ID = ObjectFields.OBJECT_END + 0x06,
        CORPSE_FIELD_ITEM = ObjectFields.OBJECT_END + 0x07, // 19
        CORPSE_FIELD_BYTES_1 = ObjectFields.OBJECT_END + 0x1A,
        CORPSE_FIELD_BYTES_2 = ObjectFields.OBJECT_END + 0x1B,
        CORPSE_FIELD_GUILD = ObjectFields.OBJECT_END + 0x1C,
        CORPSE_FIELD_FLAGS = ObjectFields.OBJECT_END + 0x1D,
        CORPSE_FIELD_DYNAMIC_FLAGS = ObjectFields.OBJECT_END + 0x1E,
        CORPSE_FIELD_PAD = ObjectFields.OBJECT_END + 0x1F,
        CORPSE_END = ObjectFields.OBJECT_END + 0x20,
    };

    public enum ObjectFields
    {
        OBJECT_FIELD_GUID = 0x00, // Size:2
        OBJECT_FIELD_DATA = 0x01, // Size:2
        OBJECT_FIELD_TYPE = 0x02, // Size:1
        OBJECT_FIELD_ENTRY = 0x03, // Size:1
        OBJECT_FIELD_SCALE_X = 0x04, // Size:1
        OBJECT_FIELD_PADDING = 0x05, // Size:1
        OBJECT_END = 0x06,
    };

    public enum ItemFields
    {
        ITEM_FIELD_OWNER = ObjectFields.OBJECT_END + 0x00, // Size:2
        ITEM_FIELD_CONTAINED = ObjectFields.OBJECT_END + 0x02, // Size:2
        ITEM_FIELD_CREATOR = ObjectFields.OBJECT_END + 0x04, // Size:2
        ITEM_FIELD_GIFTCREATOR = ObjectFields.OBJECT_END + 0x06, // Size:2
        ITEM_FIELD_STACK_COUNT = ObjectFields.OBJECT_END + 0x08, // Size:1
        ITEM_FIELD_DURATION = ObjectFields.OBJECT_END + 0x09, // Size:1
        ITEM_FIELD_SPELL_CHARGES = ObjectFields.OBJECT_END + 0x0A, // Size:5
        ITEM_FIELD_SPELL_CHARGES_01 = ObjectFields.OBJECT_END + 0x0B,
        ITEM_FIELD_SPELL_CHARGES_02 = ObjectFields.OBJECT_END + 0x0C,
        ITEM_FIELD_SPELL_CHARGES_03 = ObjectFields.OBJECT_END + 0x0D,
        ITEM_FIELD_SPELL_CHARGES_04 = ObjectFields.OBJECT_END + 0x0E,
        ITEM_FIELD_FLAGS = ObjectFields.OBJECT_END + 0x0F, // Size:1
        ITEM_FIELD_ENCHANTMENT = ObjectFields.OBJECT_END + 0x10, // count=21
        ITEM_FIELD_PROPERTY_SEED = ObjectFields.OBJECT_END + 0x25, // Size:1
        ITEM_FIELD_RANDOM_PROPERTIES_ID = ObjectFields.OBJECT_END + 0x26, // Size:1
        ITEM_FIELD_ITEM_TEXT_ID = ObjectFields.OBJECT_END + 0x27, // Size:1
        ITEM_FIELD_DURABILITY = ObjectFields.OBJECT_END + 0x28, // Size:1
        ITEM_FIELD_MAXDURABILITY = ObjectFields.OBJECT_END + 0x29, // Size:1
        ITEM_END = ObjectFields.OBJECT_END + 0x2A,
    };

    public enum ContainerFields
    {
        CONTAINER_FIELD_NUM_SLOTS = ItemFields.ITEM_END + 0x00, // Size:1
        CONTAINER_ALIGN_PAD = ItemFields.ITEM_END + 0x01, // Size:1
        CONTAINER_FIELD_SLOT_1 = ItemFields.ITEM_END + 0x02, // count=56
        CONTAINER_FIELD_SLOT_LAST = ItemFields.ITEM_END + 0x38,
        CONTAINER_END = ItemFields.ITEM_END + 0x3A,
    };

    public enum UnitFields
    {
        UNIT_FIELD_CHARM = 0x00 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_SUMMON = 0x02 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_CHARMEDBY = 0x04 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_SUMMONEDBY = 0x06 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_CREATEDBY = 0x08 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_TARGET = 0x0A + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_PERSUADED = 0x0C + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_CHANNEL_OBJECT = 0x0E + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_HEALTH = 0x10 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER1 = 0x11 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER2 = 0x12 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER3 = 0x13 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER4 = 0x14 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER5 = 0x15 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXHEALTH = 0x16 + ObjectFields.OBJECT_END, // Size:1 
        UNIT_FIELD_MAXPOWER1 = 0x17 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER2 = 0x18 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER3 = 0x19 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER4 = 0x1A + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER5 = 0x1B + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_LEVEL = 0x1C + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_FACTIONTEMPLATE = 0x1D + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BYTES_0 = 0x1E + ObjectFields.OBJECT_END, // Size:1
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY = 0x1F + ObjectFields.OBJECT_END, // Size:3
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_01 = 0x20 + ObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_02 = 0x21 + ObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO = 0x22 + ObjectFields.OBJECT_END, // Size:6
        UNIT_VIRTUAL_ITEM_INFO_01 = 0x23 + ObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_02 = 0x24 + ObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_03 = 0x25 + ObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_04 = 0x26 + ObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_05 = 0x27 + ObjectFields.OBJECT_END,
        UNIT_FIELD_FLAGS = 0x28 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_AURA = 0x29 + ObjectFields.OBJECT_END, // Size:48
        UNIT_FIELD_AURA_LAST = 0x58 + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS = 0x59 + ObjectFields.OBJECT_END, // Size:6
        UNIT_FIELD_AURAFLAGS_01 = 0x5a + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_02 = 0x5b + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_03 = 0x5c + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_04 = 0x5d + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_05 = 0x5e + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURALEVELS = 0x5f + ObjectFields.OBJECT_END, // Size:12
        UNIT_FIELD_AURALEVELS_LAST = 0x6a + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURAAPPLICATIONS = 0x6b + ObjectFields.OBJECT_END, // Size:12
        UNIT_FIELD_AURAAPPLICATIONS_LAST = 0x76 + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURASTATE = 0x77 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BASEATTACKTIME = 0x78 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_OFFHANDATTACKTIME = 0x79 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_RANGEDATTACKTIME = 0x7a + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BOUNDINGRADIUS = 0x7b + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_COMBATREACH = 0x7c + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_DISPLAYID = 0x7d + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_NATIVEDISPLAYID = 0x7e + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MOUNTDISPLAYID = 0x7f + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MINDAMAGE = 0x80 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXDAMAGE = 0x81 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MINOFFHANDDAMAGE = 0x82 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXOFFHANDDAMAGE = 0x83 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BYTES_1 = 0x84 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PETNUMBER = 0x85 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PET_NAME_TIMESTAMP = 0x86 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PETEXPERIENCE = 0x87 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PETNEXTLEVELEXP = 0x88 + ObjectFields.OBJECT_END, // Size:1
        UNIT_DYNAMIC_FLAGS = 0x89 + ObjectFields.OBJECT_END, // Size:1
        UNIT_CHANNEL_SPELL = 0x8a + ObjectFields.OBJECT_END, // Size:1
        UNIT_MOD_CAST_SPEED = 0x8b + ObjectFields.OBJECT_END, // Size:1
        UNIT_CREATED_BY_SPELL = 0x8c + ObjectFields.OBJECT_END, // Size:1
        UNIT_NPC_FLAGS = 0x8d + ObjectFields.OBJECT_END, // Size:1
        UNIT_NPC_EMOTESTATE = 0x8e + ObjectFields.OBJECT_END, // Size:1
        UNIT_TRAINING_POINTS = 0x8f + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT0 = 0x90 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT1 = 0x91 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT2 = 0x92 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT3 = 0x93 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT4 = 0x94 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RESISTANCES = 0x95 + ObjectFields.OBJECT_END, // Size:7
        UNIT_FIELD_RESISTANCES_01 = 0x96 + ObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_02 = 0x97 + ObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_03 = 0x98 + ObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_04 = 0x99 + ObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_05 = 0x9a + ObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_06 = 0x9b + ObjectFields.OBJECT_END,
        UNIT_FIELD_BASE_MANA = 0x9c + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BASE_HEALTH = 0x9d + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BYTES_2 = 0x9e + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_ATTACK_POWER = 0x9f + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_ATTACK_POWER_MODS = 0xa0 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER = 0xa1 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER = 0xa2 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER_MODS = 0xa3 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER = 0xa4 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MINRANGEDDAMAGE = 0xa5 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXRANGEDDAMAGE = 0xa6 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER_COST_MODIFIER = 0xa7 + ObjectFields.OBJECT_END, // Size:7
        UNIT_FIELD_POWER_COST_MODIFIER_01 = 0xa8 + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_02 = 0xa9 + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_03 = 0xaa + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_04 = 0xab + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_05 = 0xac + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_06 = 0xad + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER = 0xae + ObjectFields.OBJECT_END, // Size:7
        UNIT_FIELD_POWER_COST_MULTIPLIER_01 = 0xaf + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_02 = 0xb0 + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_03 = 0xb1 + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_04 = 0xb2 + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_05 = 0xb3 + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_06 = 0xb4 + ObjectFields.OBJECT_END,
        UNIT_FIELD_PADDING = 0xb5 + ObjectFields.OBJECT_END,
        UNIT_END = 0xb6 + ObjectFields.OBJECT_END,
    }
    
    public enum PlayerField
    {
        PLAYER_DUEL_ARBITER = 0x00 + UnitFields.UNIT_END, // Size:2
        PLAYER_FLAGS = 0x02 + UnitFields.UNIT_END, // Size:1
        PLAYER_GUILDID = 0x03 + UnitFields.UNIT_END, // Size:1
        PLAYER_GUILDRANK = 0x04 + UnitFields.UNIT_END, // Size:1
        PLAYER_BYTES = 0x05 + UnitFields.UNIT_END, // Size:1
        PLAYER_BYTES_2 = 0x06 + UnitFields.UNIT_END, // Size:1
        PLAYER_BYTES_3 = 0x07 + UnitFields.UNIT_END, // Size:1
        PLAYER_DUEL_TEAM = 0x08 + UnitFields.UNIT_END, // Size:1
        PLAYER_GUILD_TIMESTAMP = 0x09 + UnitFields.UNIT_END, // Size:1
        PLAYER_QUEST_LOG_1_1 = 0x0A + UnitFields.UNIT_END, // count = 20
        PLAYER_QUEST_LOG_1_2 = 0x0B + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_1_3 = 0x0C + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_1 = 0x43 + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_2 = 0x44 + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_3 = 0x45 + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_1_CREATOR = 0x46 + UnitFields.UNIT_END, // Size:2, count = 19
        PLAYER_VISIBLE_ITEM_1_0 = 0x48 + UnitFields.UNIT_END, // Size:8
        PLAYER_VISIBLE_ITEM_1_PROPERTIES = 0x50 + UnitFields.UNIT_END, // Size:1
        PLAYER_VISIBLE_ITEM_1_PAD = 0x51 + UnitFields.UNIT_END, // Size:1
        PLAYER_VISIBLE_ITEM_LAST_CREATOR = 0x11e + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_0 = 0x120 + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_PROPERTIES = 0x128 + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_PAD = 0x129 + UnitFields.UNIT_END,
        PLAYER_FIELD_INV_SLOT_HEAD = 0x12a + UnitFields.UNIT_END, // Size:46
        PLAYER_FIELD_PACK_SLOT_1 = 0x158 + UnitFields.UNIT_END, // Size:32
        PLAYER_FIELD_PACK_SLOT_LAST = 0x176 + UnitFields.UNIT_END,
        PLAYER_FIELD_BANK_SLOT_1 = 0x178 + UnitFields.UNIT_END, // Size:48
        PLAYER_FIELD_BANK_SLOT_LAST = 0x1a6 + UnitFields.UNIT_END,
        PLAYER_FIELD_BANKBAG_SLOT_1 = 0x1a8 + UnitFields.UNIT_END, // Size:12
        PLAYER_FIELD_BANKBAG_SLOT_LAST = 0xab2 + UnitFields.UNIT_END,
        PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = 0x1b4 + UnitFields.UNIT_END, // Size:24
        PLAYER_FIELD_VENDORBUYBACK_SLOT_LAST = 0x1ca + UnitFields.UNIT_END,
        PLAYER_FIELD_KEYRING_SLOT_1 = 0x1cc + UnitFields.UNIT_END, // Size:64
        PLAYER_FIELD_KEYRING_SLOT_LAST = 0x20a + UnitFields.UNIT_END,
        PLAYER_FARSIGHT = 0x20c + UnitFields.UNIT_END, // Size:2
        PLAYER_FIELD_COMBO_TARGET = 0x20e + UnitFields.UNIT_END, // Size:2
        PLAYER_XP = 0x210 + UnitFields.UNIT_END, // Size:1
        PLAYER_NEXT_LEVEL_XP = 0x211 + UnitFields.UNIT_END, // Size:1
        PLAYER_SKILL_INFO_1_1 = 0x212 + UnitFields.UNIT_END, // Size:384
        PLAYER_SKILL_PROP_1_1 = 0x213 + UnitFields.UNIT_END, // Size:384

        PLAYER_CHARACTER_POINTS1 = 0x392 + UnitFields.UNIT_END, // Size:1
        PLAYER_CHARACTER_POINTS2 = 0x393 + UnitFields.UNIT_END, // Size:1
        PLAYER_TRACK_CREATURES = 0x394 + UnitFields.UNIT_END, // Size:1
        PLAYER_TRACK_RESOURCES = 0x395 + UnitFields.UNIT_END, // Size:1
        PLAYER_BLOCK_PERCENTAGE = 0x396 + UnitFields.UNIT_END, // Size:1
        PLAYER_DODGE_PERCENTAGE = 0x397 + UnitFields.UNIT_END, // Size:1
        PLAYER_PARRY_PERCENTAGE = 0x398 + UnitFields.UNIT_END, // Size:1
        PLAYER_CRIT_PERCENTAGE = 0x399 + UnitFields.UNIT_END, // Size:1
        PLAYER_RANGED_CRIT_PERCENTAGE = 0x39a + UnitFields.UNIT_END, // Size:1
        PLAYER_EXPLORED_ZONES_1 = 0x39b + UnitFields.UNIT_END, // Size:64
        PLAYER_REST_STATE_EXPERIENCE = 0x3db + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_COINAGE = 0x3dc + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT0 = 0x3DD + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT1 = 0x3DE + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT2 = 0x3DF + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT3 = 0x3E0 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT4 = 0x3E1 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT0 = 0x3E2 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT1 = 0x3E3 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT2 = 0x3E4 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT3 = 0x3E5 + UnitFields.UNIT_END, // Size:1,
        PLAYER_FIELD_NEGSTAT4 = 0x3E6 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE = 0x3E7 + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE = 0x3EE + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS = 0x3F5 + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = 0x3FC + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = 0x403 + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_BYTES = 0x40A + UnitFields.UNIT_END, // Size:1
        PLAYER_AMMO_ID = 0x40B + UnitFields.UNIT_END, // Size:1
        PLAYER_SELF_RES_SPELL = 0x40C + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_PVP_MEDALS = 0x40D + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_BUYBACK_PRICE_1 = 0x40E + UnitFields.UNIT_END, // count=12
        PLAYER_FIELD_BUYBACK_PRICE_LAST = 0x419 + UnitFields.UNIT_END,
        PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = 0x41A + UnitFields.UNIT_END, // count=12
        PLAYER_FIELD_BUYBACK_TIMESTAMP_LAST = 0x425 + UnitFields.UNIT_END,
        PLAYER_FIELD_SESSION_KILLS = 0x426 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_YESTERDAY_KILLS = 0x427 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_KILLS = 0x428 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_THIS_WEEK_KILLS = 0x429 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_THIS_WEEK_CONTRIBUTION = 0x42a + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LIFETIME_HONORABLE_KILLS = 0x42b + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LIFETIME_DISHONORABLE_KILLS = 0x42c + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_YESTERDAY_CONTRIBUTION = 0x42d + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_CONTRIBUTION = 0x42e + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_RANK = 0x42f + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_BYTES2 = 0x430 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_WATCHED_FACTION_INDEX = 0x431 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_COMBAT_RATING_1 = 0x432 + UnitFields.UNIT_END, // Size:20

        PLAYER_END = 0x446 + UnitFields.UNIT_END
    };

    public enum HighGuid
    {
        HighguidItem = 0x4700,
        HighguidContainer = 0x4700,
        HighguidPlayer = 0x0000,
        HighguidGameobject = 0xF110,
        HighguidTransport = 0xF120,
        HighguidUnit = 0xF130,
        HighguidPet = 0xF140,
        HighguidVehicle = 0xF150,
        HighguidDynamicobject = 0xF100,
        HighguidCorpse = 0xF500,
        HighguidMoTransport = 0x1FC0
    }

    public enum ObjectUpdateType : byte
    {
        UPDATETYPE_VALUES = 0,
        UPDATETYPE_MOVEMENT = 1,
        UPDATETYPE_CREATE_OBJECT = 2,
        UPDATETYPE_CREATE_OBJECT2 = 3,
        UPDATETYPE_OUT_OF_RANGE_OBJECTS = 4,
        UPDATETYPE_NEAR_OBJECTS = 5,
    }

    [Flags]
    public enum TypeMaskFlags : byte
    {
        TypemaskObject = 0x0001,
        TypemaskItem = 0x0002,
        TypemaskContainer = 0x0004,
        TypemaskUnit = 0x0008, // players also have it
        TypemaskPlayer = 0x0010,
        TypemaskGameobject = 0x0020,
        TypemaskDynamicobject = 0x0040,
        TypemaskCorpse = 0x0080
    }

    [Flags]
    public enum TypeID : byte
    {
        TYPEID_OBJECT = 0,
        TYPEID_ITEM = 1,
        TYPEID_CONTAINER = 2,
        TYPEID_UNIT = 3,
        TYPEID_PLAYER = 4,
        TYPEID_GAMEOBJECT = 5,
        TYPEID_DYNAMICOBJECT = 6,
        TYPEID_CORPSE = 7,
    }

    [Flags]
    public enum ObjectFlags : byte
    {
        UPDATEFLAG_NONE = 0x0000,
        UPDATEFLAG_SELF = 0x0001,
        UPDATEFLAG_TRANSPORT = 0x0002,
        UPDATEFLAG_FULLGUID = 0x0004,
        UPDATEFLAG_HIGHGUID = 0x0008,
        UPDATEFLAG_ALL = 0x0010,
        UPDATEFLAG_LIVING = 0x0020,
        UPDATEFLAG_HAS_POSITION = 0x0040,
    }

    [Flags]
    public enum ObjectUpdateFlag : byte
    {
        UPDATEFLAG_NONE = 0x0000,
        UPDATEFLAG_SELF = 0x0001,
        UPDATEFLAG_TRANSPORT = 0x0002,
        UPDATEFLAG_FULLGUID = 0x0004,
        UPDATEFLAG_HIGHGUID = 0x0008,
        UPDATEFLAG_ALL = 0x0010,
        UPDATEFLAG_LIVING = 0x0020,
        UPDATEFLAG_HAS_POSITION = 0x0040
    }
}
