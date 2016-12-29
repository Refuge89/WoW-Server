using Shaolinq;

namespace Framework.Database.Tables
{
    [DataAccessObject]
    public abstract class GameObject : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public abstract int Id { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        [PersistedMember]
        public abstract int guid { get; set; }

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
    }

    [DataAccessObject]
    public abstract class GameObjectTemplate : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public abstract int Id { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        [PersistedMember]
        public abstract int type { get; set; }

        [PersistedMember]
        public abstract int displayID { get; set; }

        [PersistedMember]
        public abstract string name { get; set; }

        [PersistedMember]
        public abstract int faction { get; set; }

        [PersistedMember]
        public abstract int flag { get; set; }

        [PersistedMember]
        public abstract float size { get; set; }
    }
}
