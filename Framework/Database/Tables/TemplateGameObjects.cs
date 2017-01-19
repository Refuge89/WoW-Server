using System;
using Shaolinq;

namespace Framework.Database.Tables
{

    [DataAccessObject]
    public abstract class TemplateGameObjects : DataAccessObject<int>
    {
        [PersistedMember]
        public abstract int type { get; set; }

        [PersistedMember]
        public abstract float displayId { get; set; }

        [PersistedMember]
        public abstract string name { get; set; }

        [PersistedMember]
        public abstract float faction { get; set; }

        [PersistedMember]
        public abstract float flags { get; set; }

        [PersistedMember]
        public abstract float size { get; set; }

        [PersistedMember]
        public abstract DateTime? created_at { get; set; }

        [PersistedMember]
        public abstract DateTime? updated_at { get; set; }
    }
}
