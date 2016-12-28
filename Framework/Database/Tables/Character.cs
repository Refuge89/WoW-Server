using Shaolinq;
using System;

namespace Framework.Database.Tables
{
    [DataAccessObject]
    public abstract class Character : DataAccessObject<Guid>
    {
        [AutoIncrement]
        [PersistedMember]
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public abstract Guid Id { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        [PersistedMember]
        public abstract int AccountID { get; set; }

        [PersistedMember]
        public abstract string Name { get; set; }
    }
}
