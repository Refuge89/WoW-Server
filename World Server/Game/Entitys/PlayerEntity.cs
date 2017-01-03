using System;
using Framework.Contants.Character;
using Framework.Contants.Game;
using Framework.Database;
using Framework.Database.Tables;
using World_Server.Helpers;

namespace World_Server.Game.Entitys
{
    public class PlayerEntity : WorldObject
    {
        private static int GetModel(Character character)
        {
            var charModel = Program.Database.GetCharStarter(character.Race);

            return character.Gender == GenderID.MALE ? charModel.ModelM : charModel.ModelF;
        }

        private static int GetAttribute(Character character, string attribute)
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

        private static float GetScale(Character character)
        {
            if (character.Race == RaceID.TAUREN)
            {
                if (character.Gender == GenderID.MALE) return 1.3f;

                return 1.25f;
            }

            return 1f;
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
            SetUpdateField((int)EUnitFields.UNIT_FIELD_BYTES_0, (byte)1, 3); // [mana 0] [rage 1] [focus 2] [energy 3]

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
                WorldItems entry = equipment[i];

                if (equipment?[i] != null)
                {
                    int visualBase = (int) EUnitFields.PLAYER_VISIBLE_ITEM_1_0 + (i * 14);
                    Console.WriteLine(equipment[i].name);
                    SetUpdateField(visualBase, (byte)equipment[i].itemId);
                }

                if (entry != null)
                {
                    //int visualBase = (int)EUnitFields.PLAYER_VISIBLE_ITEM_1_0 + (i * 12);
                    //Console.WriteLine(equipment[i].name);
                    //SetUpdateField(visualBase, (byte)equipment[i].itemId);
                }
            }

        }
    }
}
