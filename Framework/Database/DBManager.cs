using Framework.Database.Tables;
using Shaolinq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Database
{
    public class DBManager : BaseModel<Models>
    {
        public void Boot()
        {
            // Recria a base inteira
            this.model.Create(DatabaseCreationOptions.DeleteExistingDatabase);

            using (var scope = new DataAccessScope())
            {
                var User = this.model.Users.Create();
                User.name     = "John Doe";
                User.username = "john";
                User.email    = "john@doe.com";
                User.password = "doe";

                scope.Complete();
            }
        }

        public Users GetAccount(string username)
        {
            if (this.model.Users.Count() == 0) return null;
            return this.model.Users.FirstOrDefault(a => a.username.ToLower() == username.ToLower());
        }

        public async Task<Users> SetSessionKey(String username, byte[] key)
        {
            Users account = GetAccount(username);

            using (var scope = new DataAccessScope())
            {
                var User = this.model.Users.GetReference(account.Id);
                    User.sessionkey = key;
                await scope.CompleteAsync();
            }

            return null;
        }
    }
}
