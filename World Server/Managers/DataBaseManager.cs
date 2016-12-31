using Framework.Contants.Character;
using Framework.Database;
using Framework.Database.Tables;
using Framework.DBC.Structs;
using Shaolinq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using World_Server.Handlers.Char;

namespace World_Server.Managers
{
    public class DataBaseManager : BaseModel<Models>
    {
        public void CreateChar(PCCharCreate packet, Users User)
        {
            using (var scope = new DataAccessScope())
            {
                //CharacterCreationInfo newCharacterInfo = DBCharacters.GetCreationInfo((RaceID)packet.Race, (ClassID)packet.Class);
                // Selecting Starter Itens Equipament
                CharStartOutfit startItems = DBCManager.CharStartOutfit.Values.FirstOrDefault(x => x.Match(packet.Race, packet.Class, packet.Gender));

                // Selecting char data creation
                CharacterCreationInfo CharStarter = this.GetCharStarter((RaceID)packet.Race);

                // Salva Char
                var Char = this.model.Characters.Create();
                    Char.Users = User;
                    Char.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(packet.Name);
                    Char.Race = (RaceID)packet.Race;
                    Char.Class = (ClassID)packet.Class;
                    Char.Gender = (GenderID)packet.Gender;
                    Char.Level = 1;
                    Char.Money = 0;
                    Char.Online = 0;
                    Char.MapID = CharStarter.MapID;
                    Char.MapZone = CharStarter.MapZone;
                    Char.MapX = CharStarter.MapX;
                    Char.MapY = CharStarter.MapY;
                    Char.MapZ = CharStarter.MapZ;
                    Char.MapRotation = CharStarter.MapRotation;
                    Char.Equipment = String.Join(",", startItems.m_InventoryType);
                    Char.firsttime = false;
                
                // Salva Skin do Char              
                var Skin = this.model.CharactersSkin.Create();
                    Skin.Character = Char;
                    Skin.Face = packet.Face;
                    Skin.HairStyle = packet.HairStyle;
                    Skin.HairColor = packet.HairColor;
                    Skin.Accessory = packet.Accessory;

                scope.Complete();
            }
            return;
        }

        public CharacterCreationInfo GetCharStarter(RaceID Race)
        {
            return this.model.CharacterCreationInfo.FirstOrDefault(a => a.Race == Race);
        }

        public Character GetCharacter(int CharId)
        {
            return this.model.Characters.FirstOrDefault(a => a.Id == CharId);
        }

        public List<Character> GetCharacters(string username)
        {
            Users account = Program.DBManager.GetAccount(username);
            return this.model.Characters.Where(a => a.Users == account).ToList();
        }

        public CharactersSkin GetSking(Character Character)
        {
            return this.model.CharactersSkin.FirstOrDefault(a => a.Character == Character);
        }

        public async void DeleteCharacter(int CharId)
        {
            using (var scope = new DataAccessScope())
            {
                await this.model.Characters.Where(a => a.Id == CharId).DeleteAsync();
                await scope.CompleteAsync();
            }

            return;
        }

        public async void UpdateCharacter(int CharId, string Objeto, string Value = null)
        {
            using (var scope = new DataAccessScope())
            {
                var character = model.Characters.GetReference(CharId);

                // Define Online/Offline
                if(Objeto == "online" && character.Online == 1)
                    character.Online = 0;
                 else
                    character.Online = 1;

                // Define primeiro Login
                if (Objeto == "firstlogin")
                    character.firsttime = true;
                    
                await scope.CompleteAsync();
            }

            return;
        }
    }
}
