using System;
using Shaolinq;

namespace Framework.Database.Tables
{
    [DataAccessObject]
    public abstract class WorldItems : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
        #pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public abstract int Id { get; set; }
        #pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        [PersistedMember]
        public abstract string name { get; set; }

        [PersistedMember]
        public abstract int itemId { get; set; }

        [PersistedMember]
        public abstract int displayId { get; set; }

        [PersistedMember]
        public abstract byte InventoryType { get; set; }

        [PersistedMember]
        public abstract DateTime? created_at { get; set; }

        [PersistedMember]
        public abstract DateTime? updated_at { get; set; }
    }
}
