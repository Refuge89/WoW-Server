using Shaolinq;
using System;

namespace Framework.Database.Tables
{
    [DataAccessObject]
    public abstract class Users : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public abstract int Id { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        [PersistedMember]
        public abstract string name { get; set; }

        [PersistedMember]
        public abstract string email { get; set; }

        [PersistedMember]
        public abstract string username { get; set; }

        [PersistedMember]
        public abstract string password { get; set; }

        [PersistedMember]
        public abstract byte[] sessionkey { get; set; }

        [PersistedMember]
        public abstract DateTime? created_at { get; set; }

        [PersistedMember]
        public abstract DateTime? updated_at { get; set; }
    }
}
