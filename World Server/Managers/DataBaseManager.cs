using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Framework.Contants.Character;
using Framework.Database;
using Framework.Database.Tables;
using Framework.DBC;
using Framework.DBC.Structs;
using Framework.Helpers;
using Shaolinq;
using World_Server.Handlers;
using World_Server.Helpers;

namespace World_Server.Managers
{
    public class DatabaseManager : BaseModel<Models>
    {
        public static ConcurrentDictionary<uint, AreaTable> AreaTable;
        public static ConcurrentDictionary<uint, CharStartOutfit> CharStartOutfit;       
        public static ConcurrentDictionary<int, ChrRaces> ChrRaces;
        public static ConcurrentDictionary<uint, FactionTemplate> FactionTemplate;
        public static ConcurrentDictionary<uint, Spell> Spell;

        static DatabaseManager()
        {
            Log.Print(LogType.Status, "Loading DBCs...");

            AreaTable = DBReader.Read<uint, AreaTable>("AreaTable.dbc", "Id");
            CharStartOutfit = DBReader.Read<uint, CharStartOutfit>("CharStartOutfit.dbc", "ID");
            ChrRaces = DBReader.Read<int, ChrRaces>("ChrRaces.dbc", "Id");
            FactionTemplate = DBReader.Read<uint, FactionTemplate>("FactionTemplate.dbc", "Id");
            Spell = DBReader.Read<uint, Spell>("Spell.dbc", "Id");
        }

        internal void CreateChar(CmsgCharCreate handler, Users users)
        {
            using (var scope = new DataAccessScope())
            {
                // Selecting Starter Itens Equipament
                CharStartOutfit startItems = CharStartOutfit.Values.FirstOrDefault(x => x.Match(handler.Race, handler.Class, handler.Gender));

                // Selecting char data creation
                CharacterCreationInfo charStarter = GetCharStarter((RaceID)handler.Race);

                // Salva Char
                var Char = model.Characters.Create();
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
                    Char.created_at = DateTime.Now;

                // Salva Skin do Char              
                var skin = model.CharactersSkin.Create();
                    skin.Character = Char;
                    skin.Face = handler.Face;
                    skin.HairStyle = handler.HairStyle;
                    skin.HairColor = handler.HairColor;
                    skin.Accessory = handler.Accessory;

                // Helper Skill + Spell + Inventory
                CharHelper helper = new CharHelper();
                           helper.GeraSpells(Char);
                           helper.GeraSkills(Char);
                           helper.GeraActionBar(Char);

                scope.Complete();
            }
        }

        public CharacterCreationInfo GetCharStarter(RaceID race)
        {
            return model.CharacterCreationInfo.FirstOrDefault(a => a.Race == race);
        }

        // Pega conta do usuario baseado no login
        public Users GetAccount(string username) => !model.Users.Any() ? null : model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());

        public Character GetCharacter(int charId)
        {
            return model.Characters.FirstOrDefault(a => a.Id == charId);
        }

        public List<Character> GetCharacters(string username)
        {
            Users account = GetAccount(username);
            return model.Characters.Where(a => a.Users == account).ToList();
        }

        public CharactersSkin GetSkin(Character character)
        {
            return model.CharactersSkin.FirstOrDefault(a => a.Character == character);
        }

        public WorldItems GetItem(int itemId)
        {
            return model.WorldItems.FirstOrDefault(a => a.itemId == itemId);
        }

        public async void DeleteCharacter(int charId)
        {
            using (var scope = new DataAccessScope())
            {
                await model.Characters.Where(a => a.Id == charId).DeleteAsync();
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
            return model.CharactersSkill.Where(a => a.character == character).ToList();
        }

        internal List<CharactersSpells> GetSpells(Character character)
        {
            return model.CharactersSpells.Where(a => a.character == character).ToList();
        }

        internal List<CharactersActionBar> GetActionBar(Character character)
        {
            return model.CharactersActionBar.Where(a => a.character == character).ToList();
        }

        internal async void RemoveActionBar(CmsgSetActionButton handler, Character character)
        {
            using (var scope = new DataAccessScope())
            {
                await model.CharactersActionBar.Where(a => a.character == character).Where(b => b.Action == handler.Action).DeleteAsync();
                await scope.CompleteAsync();
            }
        }

        internal void AddActionBar(CmsgSetActionButton handler, Character character)
        {
            using (var scope = new DataAccessScope())
            {
                var skill = this.model.CharactersActionBar.Create();
                    skill.character = character;
                    skill.Action = (int)handler.Action;
                    skill.Button = handler.Button;
                    skill.Type = (int)handler.Type;
                    skill.created_at = ServerDateTime.Now;
                scope.Complete();
            }
        }
    }
}
