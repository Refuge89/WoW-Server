using Framework.Database.Tables;
using Shaolinq;

namespace Framework.Database
{
    [DataAccessModel]
    public abstract class Models : DataAccessModel
    {
        [DataAccessObjects]
        public abstract DataAccessObjects<WorldItems> WorldItems { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<Users> Users { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<Realms> Realms { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<Character> Characters { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<CharacterCreationInfo> CharacterCreationInfo { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<CharactersSkin> CharactersSkin { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<CharactersSpells> CharactersSpells { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<CharactersSkill> CharactersSkill { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<CharactersActionBar> CharactersActionBar { get; }
    }
}
