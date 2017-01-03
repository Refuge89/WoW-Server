using System;
using Framework.Contants.Character;
using Framework.Contants.Game;
using Framework.Database;
using Framework.Database.Tables;
using World_Server.Helpers;
using Framework.Database.Xml;
using System.ComponentModel;

namespace World_Server.Game.Entitys
{
    public class PlayerEntity : WorldObject
    {
        public Character character;

        private int GetModel(Character character)
        {
            var charModel = Program.Database.GetCharStarter(character.Race);

            return character.Gender == GenderID.MALE ? charModel.ModelM : charModel.ModelF;
        }

        private int GetAttribute(Character character, string attribute)
        {
            var attRace = XmlManager.GetRaceStats(character.Race);
            var attClas = XmlManager.GetClassStats(character.Class);
            //var AttChar = XmlManager.getRaceStats(race);

            if(attribute == "health")
                return (character.Level == 1) ? attRace.health + attClas.health : 1500;

            if (attribute == "strength")
                return (character.Level == 1) ? attRace.stats.strength + attClas.stats.strength : 1500;

            if (attribute == "agility")
                return (character.Level == 1) ? attRace.stats.agility + attClas.stats.agility : 1500;

            if (attribute == "intellect")
                return (character.Level == 1) ? attRace.stats.intellect + attClas.stats.intellect : 1500;

            if (attribute == "stamina")
                return (character.Level == 1) ? attRace.stats.stamina + attClas.stats.stamina : 1500;

            if (attribute == "spirit")
                return (character.Level == 1) ? attRace.stats.spirit + attClas.stats.spirit : 1500;

            return 1;
        }

        private float GetScale(Character character)
        {
            if (character.Race == RaceID.TAUREN)
            {
                if (character.Gender == GenderID.MALE) return 1.3f;

                return 1.25f;
            }

            return 1f;
        }

        private int GetHealth(Character character)
        {
            // Base do HP (Classe + Race) + Char
            var baseHealth = GetAttribute(character, "health");
            
            // Base de Stamina (Classe + Race) + Char
            var baseStamina = GetAttribute(character, "stamina");

            int StaminaCalc = 0;

            if (baseStamina <= 20)
                StaminaCalc = (baseStamina * (int)1.55); 
            else
                StaminaCalc = ((baseStamina - 20) * 10) + 20;
            
            return (baseHealth + StaminaCalc); // ainda vem os multiplicadores
        }

        private void UpdateMana()
        {
            /*
            uint baseMana = (character.Intellect.Current < 20 ? character.Intellect.Current : 20);
            uint moreMana = character.Intellect.Current - baseMana;
            character.Mana.Maximum = character.Mana.BaseAmount + baseMana + (moreMana * 15);
            */
        }

        public PlayerEntity(Character character) : base((int)EUnitFields.PLAYER_END - 0x4)
        {
            var skin = Program.Database.GetSkin(character);

            // Object Fields
            SetUpdateField((int)EObjectFields.OBJECT_FIELD_GUID,                character.Id);          // Id do Objeto
            SetUpdateField((int)EObjectFields.OBJECT_FIELD_TYPE,                25);                    // 0x19 Player + Unit + Object
            SetUpdateField((int)EObjectFields.OBJECT_FIELD_ENTRY,               0);
            SetUpdateField((int)EObjectFields.OBJECT_FIELD_SCALE_X,             GetScale(character));   // Escala do objeto

            // Unit Fields
            SetUpdateField((int)EUnitFields.UNIT_CHANNEL_SPELL, 0);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_CHANNEL_OBJECT, 0);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_HEALTH, GetAttribute(character, "health"));                    // Se logou agora o Current = Maximum          
            //SetUpdateField((int)EUnitFields.UNIT_FIELD_POWER1, this.Mana.Current);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_POWER2, 1000);
            //SetUpdateField((int)EUnitFields.UNIT_FIELD_POWER3, this.Focus.Current);
            //SetUpdateField((int)EUnitFields.UNIT_FIELD_POWER4, this.Energy.Current);


            SetUpdateField((int)EUnitFields.UNIT_FIELD_MAXHEALTH, GetAttribute(character, "health"));
            //SetUpdateField((int)EUnitFields.UNIT_FIELD_MAXPOWER1, this.Mana.Maximum);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_MAXPOWER2, 1000);
            //SetUpdateField((int)EUnitFields.UNIT_FIELD_MAXPOWER3, this.Focus.Maximum);
            //SetUpdateField((int)EUnitFields.UNIT_FIELD_MAXPOWER4, this.Energy.Maximum);
            


            SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_LEVEL,              character.Level);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_FACTIONTEMPLATE,         5);

            SetUpdateField((int)EUnitFields.UNIT_FIELD_BYTES_0,                 (byte)character.Race, 0);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_BYTES_0,                 (byte)character.Class, 1);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_BYTES_0,                 (byte)character.Gender, 2);
            SetUpdateField((int)EUnitFields.UNIT_FIELD_BYTES_0,                 (byte)1, 3); // [mana 0] [rage 1] [focus 2] [energy 3]

            SetUpdateField((int)EUnitFields.UNIT_FIELD_DISPLAYID,               GetModel(character));
            SetUpdateField((int)EUnitFields.UNIT_FIELD_NATIVEDISPLAYID,         GetModel(character));
            SetUpdateField((int)EUnitFields.PLAYER_BYTES,                       skin.Skin, 0);
            SetUpdateField((int)EUnitFields.PLAYER_BYTES,                       skin.Face, 1);
            SetUpdateField((int)EUnitFields.PLAYER_BYTES,                       skin.HairStyle, 2);
            SetUpdateField((int)EUnitFields.PLAYER_BYTES,                       skin.HairColor, 3);

            // Items Equipamento
            WorldItems[] equipment = InventoryHelper.GenerateInventoryByIDs(character.Equipment);

            for (int i = 0; i < 19; i++)
            {
                if (equipment?[i] != null)
                {
                    int visualBase = (int) EUnitFields.PLAYER_VISIBLE_ITEM_1_0 + (i * 12);
                    Console.WriteLine(equipment[i].name);
                    SetUpdateField(visualBase, (byte)equipment[i].itemId);
                }
            }

            SetUpdateField<byte>((int)EUnitFields.PLAYER_BYTES_2, 0, 0);

            var SkillRace = XmlManager.GetRaceStats(character.Race);
            var SkillClass = XmlManager.GetClassStats(character.Class);
            int a = 0;
            foreach (raceSkill spellid in SkillRace.skills)
            {
                SetUpdateField<Int32>((int)EUnitFields.PLAYER_SKILL_INFO_1_1 + (a * 3), spellid.id);
                SetUpdateField<Int32>((int)EUnitFields.PLAYER_SKILL_INFO_1_1 + (a * 3) + 1, 327681);
                a++;
            }

            foreach (classeSkill spellid in SkillClass.skills)
            {
                SetUpdateField<Int32>((int)EUnitFields.PLAYER_SKILL_INFO_1_1 + (a * 3), spellid.id);
                SetUpdateField<Int32>((int)EUnitFields.PLAYER_SKILL_INFO_1_1 + (a * 3) + 1, 327681);
                a++;
            }

            /*
            SetUpdateField<Int32>((int)EUnitFields.PLAYER_SKILL_INFO_1_1, 26);
            // sdfs
            SetUpdateField<Int32>((int)719, 65537);
            SetUpdateField<Int32>((int)721, 43);
            SetUpdateField<Int32>((int)722, 327681);
            SetUpdateField<Int32>((int)724, 55);
            SetUpdateField<Int32>((int)725, 327681);
            SetUpdateField<Int32>((int)727, 95);
            SetUpdateField<Int32>((int)728, 327681);
            SetUpdateField<Int32>((int)730, 109);
            SetUpdateField<Int32>((int)731, 19661100);
            SetUpdateField<Int32>((int)733, 162);
            SetUpdateField<Int32>((int)734, 327681);
            SetUpdateField<Int32>((int)736, 173);
            SetUpdateField<Int32>((int)737, 327681);
            SetUpdateField<Int32>((int)739, 413);
            SetUpdateField<Int32>((int)740, 65537);
            SetUpdateField<Int32>((int)742, 414);
            SetUpdateField<Int32>((int)743, 65537);
            SetUpdateField<Int32>((int)745, 415);
            SetUpdateField<Int32>((int)746, 65537);
            SetUpdateField<Int32>((int)748, 433);
            SetUpdateField<Int32>((int)749, 65537);
            SetUpdateField<Int32>((int)751, 673);
            SetUpdateField<Int32>((int)752, 19661100);

            SetUpdateField<Int32>((int)EUnitFields.PLAYER_CHARACTER_POINTS2, 2);
            SetUpdateField<Int32>((int)EUnitFields.PLAYER_BLOCK_PERCENTAGE, 1083892040);
            SetUpdateField<Int32>((int)EUnitFields.PLAYER_DODGE_PERCENTAGE, 1060991140);
            SetUpdateField<Int32>((int)EUnitFields.PLAYER_CRIT_PERCENTAGE, 1060991140);
            SetUpdateField<Int32>((int)EUnitFields.PLAYER_RANGED_CRIT_PERCENTAGE, 1060320051);
            SetUpdateField<Int32>((int)1192, 10);
            SetUpdateField<Int32>((int)EUnitFields.PLAYER_FIELD_MOD_DAMAGE_DONE_PCT, 1065353216);
            SetUpdateField<Int32>((int)1216, 1065353216);
            SetUpdateField<Int32>((int)1217, 1065353216);
            SetUpdateField<Int32>((int)1218, 1065353216);
            SetUpdateField<Int32>((int)1219, 1065353216);
            SetUpdateField<Int32>((int)1220, 1065353216);
            SetUpdateField<Int32>((int)1221, 1065353216);
            SetUpdateField<Int32>((int)EUnitFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, -1);
            SetUpdateField<Int32>((int)EUnitFields.PLAYER_FIELD_COINAGE, character.Money);
            */
        }
    }
}
