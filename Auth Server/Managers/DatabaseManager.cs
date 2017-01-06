using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Database;
using Framework.Database.Tables;
using Shaolinq;

namespace Auth_Server.Managers
{
    public class DatabaseManager : BaseModel<Models>
    {
        // Pega lista de Realms
        public List<Realms> GetRealms() => model.Realms.Select(row => row).ToList();

        // Pega conta do usuario baseado no login
        public Users GetAccount(string username) => !model.Users.Any() ? null : model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());

        // Retorna os chars do player por realm
        public int CountUserRealmCharacter(string username, Realms realm)
        {
            Users users = GetAccount(username);
            return model.Characters.Count(row => row.Users == users && row.Realms == realm);
        }

        // Conta quantidade de chars no realm
        public int CountRealmCharacter(Realms realms) => model.Characters.Count(row => row.Realms == realms);

        // Define a sessionkey do usuario autenticado
        public async Task<Users> SetSessionKey(string username, byte[] key)
        {
            Users account = GetAccount(username);

            using (var scope = new DataAccessScope())
            {
                var user = model.Users.GetReference(account.Id);
                user.sessionkey = key;
                await scope.CompleteAsync();
            }

            return null;
        }
    }
}
    