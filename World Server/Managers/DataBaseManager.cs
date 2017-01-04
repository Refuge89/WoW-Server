using Framework.Contants.Character;
using Framework.Database;
using Framework.Database.Tables;
using Framework.DBC.Structs;
using Shaolinq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using World_Server.Handlers;
using World_Server.Helpers;

namespace World_Server.Managers
{
    public class DatabaseManager : BaseModel<Models>
    {
        internal void CreateChar(CmsgCharCreate handler, Users users)
        {
            using (var scope = new DataAccessScope())
            {
                // Selecting Starter Itens Equipament
                CharStartOutfit startItems = DBCManager.CharStartOutfit.Values.FirstOrDefault(x => x.Match(handler.Race, handler.Class, handler.Gender));

                // Selecting char data creation
                CharacterCreationInfo charStarter = this.GetCharStarter((RaceID)handler.Race);

                // Salva Char
                var Char = this.model.Characters.Create();
                    Char.Users = users;
                    Char.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(handler.Name);
                    Char.Race = (RaceID)handler.Race;
                    Char.Class = (ClassID)handler.Class;
                    Char.Gender = (GenderID)handler.Gender;
                    Char.Level = 1;
                    Char.Money = 0;
                    Char.Online = 0;
                    Char.MapID = charStarter.MapID;
                    Char.MapZone = charStarter.MapZone;
                    Char.MapX = charStarter.MapX;
                    Char.MapY = charStarter.MapY;
                    Char.MapZ = charStarter.MapZ;
                    Char.MapRotation = charStarter.MapRotation;
                    Char.Equipment = String.Join(",", startItems.m_ItemID);
                    Char.firsttime = false;

                // Salva Skin do Char              
                var Skin = this.model.CharactersSkin.Create();
                    Skin.Character = Char;
                    Skin.Face = handler.Face;
                    Skin.HairStyle = handler.HairStyle;
                    Skin.HairColor = handler.HairColor;
                    Skin.Accessory = handler.Accessory;

                // Helper Skill + Spell + Inventory
                CharHelper Helper = new CharHelper();
                Helper.GeraSpells(Char);
                Helper.GeraSkills(Char);

                scope.Complete();
            }



            return;
        }

        public CharacterCreationInfo GetCharStarter(RaceID race)
        {
            return this.model.CharacterCreationInfo.FirstOrDefault(a => a.Race == race);
        }

        // Pega conta do usuario baseado no login
        public Users GetAccount(string username) => !this.model.Users.Any() ? null : model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());

        public Character GetCharacter(int charId)
        {
            return this.model.Characters.FirstOrDefault(a => a.Id == charId);
        }

        public List<Character> GetCharacters(string username)
        {
            Users account = GetAccount(username);
            return this.model.Characters.Where(a => a.Users == account).ToList();
        }

        public CharactersSkin GetSkin(Character character)
        {
            return this.model.CharactersSkin.FirstOrDefault(a => a.Character == character);
        }

        public WorldItems GetItem(int itemId)
        {
            return this.model.WorldItems.FirstOrDefault(a => a.itemId == itemId);
        }

        public async void DeleteCharacter(int charId)
        {
            using (var scope = new DataAccessScope())
            {
                await this.model.Characters.Where(a => a.Id == charId).DeleteAsync();
                await scope.CompleteAsync();
            }
        }

        public async void UpdateCharacter(int charId, string objeto, string value = null)
        {
            using (var scope = new DataAccessScope())
            {
                var character = model.Characters.GetReference(charId);

                // Define Online/Offline
                if(objeto == "online" && character.Online == 1)
                    character.Online = 0;
                 else
                    character.Online = 1;

                // Define primeiro Login
                if (objeto == "firstlogin")
                    character.firsttime = true;
                    
                await scope.CompleteAsync();
            }
        }

        public async void UpdateMovement(Character character)
        {
            using (var scope = new DataAccessScope())
            {
                var update = model.Characters.GetReference(character.Id);
                update.MapX = character.MapX;
                update.MapY = character.MapY;
                update.MapZ = character.MapZ;
                update.MapRotation = character.MapRotation;

                await scope.CompleteAsync();
            }
        }

        internal List<CharactersSkill> GetSkills(Character character)
        {
            return this.model.CharactersSkill.Where(a => a.character == character).ToList();
        }

        internal List<CharactersSpells> GetSpells(Character character)
        {
            return this.model.CharactersSpells.Where(a => a.character == character).ToList();
        }
    }
}
