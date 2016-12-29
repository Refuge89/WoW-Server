using Shaolinq;

namespace Framework.Database.Tables
{
    [DataAccessObject]
    public abstract class CharactersSkin : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
        public abstract int Id { get; set; }
        
        [PersistedMember]
        public abstract Character Character { get; set; }

        [PersistedMember]
        public abstract byte Skin { get; set; }

        [PersistedMember]
        public abstract byte Face { get; set; }

        [PersistedMember]
        public abstract byte HairStyle { get; set; }

        [PersistedMember]
        public abstract byte HairColor { get; set; }

        [PersistedMember]
        public abstract byte Accessory { get; set; }
        
    }
}
