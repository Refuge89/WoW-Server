using Framework.Contants.Character;
using Framework.Database;
using Framework.Database.Tables;
using Shaolinq;
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
                var Char = this.model.Characters.Create();
                Char.Users = User;
                Char.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(packet.Name);
                Char.Race = (RaceID)packet.Race;
                Char.Class = (ClassID)packet.Class;
                Char.Gender = (GenderID)packet.Gender;
                Char.Level = 1;
                Char.Money = 0;
                Char.Online = 0;
                Char.MapID = 1;
                Char.MapX = 1235.54f;
                Char.MapY = 1427.1f;
                Char.MapZ = 309.715f;
                Char.Equipment = "36,117,159,6134,6135,9562,6948,-1,-1,-1,-1,-1";
                               
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
    }
}
