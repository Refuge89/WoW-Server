using System;
using System.Collections.Generic;
using Shaolinq;

namespace Framework.Database.Tables
{
    [DataAccessObject]
    public abstract class WorldGameObjects : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
        #pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public abstract int Id { get; set; }
        #pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        [PersistedMember]
        public abstract TemplateGameObjects entry { get; set; }

        [PersistedMember]
        public abstract int map { get; set; }

        [PersistedMember]
        public abstract float mapX { get; set; }

        [PersistedMember]
        public abstract float mapY { get; set; }

        [PersistedMember]
        public abstract float mapZ { get; set; }

        [PersistedMember]
        public abstract float mapR { get; set; }

        [PersistedMember]
        public abstract DateTime? created_at { get; set; }

        [PersistedMember]
        public abstract DateTime? updated_at { get; set; }
    }
}
