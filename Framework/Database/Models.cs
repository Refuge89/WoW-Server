using Framework.Database.Tables;
using Shaolinq;

namespace Framework.Database
{
    [DataAccessModel]
    public abstract class Models : DataAccessModel
    {
        [DataAccessObjects]
        public abstract DataAccessObjects<Users> Users { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<Character> Character { get; }
    }
}
