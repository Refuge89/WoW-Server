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
        public abstract DataAccessObjects<Character> Characters { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<CharactersSkin> CharactersSkin { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<GameObject> GameObject { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<GameObjectTemplate> GameObjectTemplate { get; }
        
    }
}
