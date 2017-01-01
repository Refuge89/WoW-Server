using System;
using Shaolinq;
using Framework.Contants;

namespace Framework.Database.Tables
{
    [DataAccessObject]
    public abstract class Realms : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
        #pragma warning disable CS0114
        public abstract int Id { get; set; }
        #pragma warning restore CS0114

        [PersistedMember]
        public abstract RealmType type { get; set; }

        [PersistedMember]
        public abstract RealmFlag flag { get; set; }

        [PersistedMember]
        public abstract RealmTimezone timezone { get; set; }

        [PersistedMember]
        public abstract string name { get; set; }

        [PersistedMember]
        public abstract string ip { get; set; }

        [PersistedMember]
        public abstract DateTime? created_at { get; set; }

        [PersistedMember]
        public abstract DateTime? updated_at { get; set; }
    }
}

