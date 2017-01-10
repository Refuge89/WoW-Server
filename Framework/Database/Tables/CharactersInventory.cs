using System;
using Shaolinq;

namespace Framework.Database.Tables
{

    [DataAccessObject]
    public abstract class CharactersInventory : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public abstract int Id { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        [PersistedMember]
        public abstract Character Character { get; set; }

        [PersistedMember]
        public abstract ulong Item { get; set; }

        [PersistedMember]
        public abstract ulong Owner { get; set; }

        [PersistedMember]
        public abstract uint Bag { get; set; }

        [PersistedMember]
        public abstract uint Slot { get; set; }

        [PersistedMember]
        public abstract uint Template { get; set; }

        [PersistedMember]
        public abstract uint Stack { get; set; }

        [PersistedMember]
        public abstract uint SpellCharge { get; set; }

        [PersistedMember]
        public abstract DateTime? created_at { get; set; }

        [PersistedMember]
        public abstract DateTime? updated_at { get; set; }

        [PersistedMember]
        public abstract DateTime? deleted_at { get; set; }
    }
}
