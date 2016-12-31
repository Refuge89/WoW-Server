using Framework.Contants.Character;
using Shaolinq;

namespace Framework.Database.Tables
{
    [DataAccessObject]
    public abstract class CharacterCreationInfo : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
        #pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public abstract int Id { get; set; }
        #pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        [PersistedMember]
        public abstract RaceID Race { get; set; }
                
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
        public abstract float MapRotation { get; set; }

        [PersistedMember]
        public abstract int Cinematic { get; set; }

        [PersistedMember]
        public abstract int FactionId { get; set; }

        [PersistedMember]
        public abstract int ModelM { get; set; }

        [PersistedMember]
        public abstract int ModelF { get; set; }

        [PersistedMember]
        public abstract int TeamId { get; set; }

        [PersistedMember]
        public abstract int TaxiMask { get; set; }
    }
}
