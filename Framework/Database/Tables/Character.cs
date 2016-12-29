using Framework.Contants.Character;
using Platform.Validation;
using Shaolinq;

namespace Framework.Database.Tables
{
    [DataAccessObject]
    public abstract class Character : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
        public abstract int Id { get; set; }

        [PersistedMember]
        public abstract Users Users { get; set; }

        [PersistedMember, Unique]
        public abstract string Name { get; set; }
        
        [PersistedMember]
        public abstract RaceID Race { get; set; }

        [PersistedMember]
        public abstract ClassID Class { get; set; }

        [PersistedMember]
        public abstract GenderID Gender { get; set; }

        [PersistedMember]
        public abstract byte Level { get; set; }

        [PersistedMember]
        public abstract int Money { get; set; }

        [PersistedMember]
        public abstract byte Online { get; set; }

        [PersistedMember]
        public abstract uint Flags { get; set; }

        [PersistedMember]
        public abstract int MapID { get; set; }

        [PersistedMember]
        public abstract int MapZone { get; set; }

        [PersistedMember]
        public abstract float MapX { get; set; }

        [PersistedMember]
        public abstract float MapY { get; set; }

        [PersistedMember]
        public abstract float MapZ { get; set; }

        [PersistedMember]
        public abstract string Equipment { get; set; }
    }
}
