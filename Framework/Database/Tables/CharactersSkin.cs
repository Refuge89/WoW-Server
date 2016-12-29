using Shaolinq;

namespace Framework.Database.Tables
{
    [DataAccessObject]
    public abstract class CharactersSkin : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public abstract int Id { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

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
