namespace Framework.Database.Xml
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class race
    {

        private byte healthField;

        private raceStats statsField;

        private raceLeveling levelingField;

        private raceSpell[] spellsField;

        private raceSkill[] skillsField;

        private byte idField;

        /// <remarks/>
        public byte health
        {
            get
            {
                return this.healthField;
            }
            set
            {
                this.healthField = value;
            }
        }

        /// <remarks/>
        public raceStats stats
        {
            get
            {
                return this.statsField;
            }
            set
            {
                this.statsField = value;
            }
        }

        /// <remarks/>
        public raceLeveling leveling
        {
            get
            {
                return this.levelingField;
            }
            set
            {
                this.levelingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("spell", IsNullable = false)]
        public raceSpell[] spells
        {
            get
            {
                return this.spellsField;
            }
            set
            {
                this.spellsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("skill", IsNullable = false)]
        public raceSkill[] skills
        {
            get
            {
                return this.skillsField;
            }
            set
            {
                this.skillsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class raceStats
    {

        private byte strengthField;

        private byte agilityField;

        private byte staminaField;

        private byte intellectField;

        private byte spiritField;

        /// <remarks/>
        public byte strength
        {
            get
            {
                return this.strengthField;
            }
            set
            {
                this.strengthField = value;
            }
        }

        /// <remarks/>
        public byte agility
        {
            get
            {
                return this.agilityField;
            }
            set
            {
                this.agilityField = value;
            }
        }

        /// <remarks/>
        public byte stamina
        {
            get
            {
                return this.staminaField;
            }
            set
            {
                this.staminaField = value;
            }
        }

        /// <remarks/>
        public byte intellect
        {
            get
            {
                return this.intellectField;
            }
            set
            {
                this.intellectField = value;
            }
        }

        /// <remarks/>
        public byte spirit
        {
            get
            {
                return this.spiritField;
            }
            set
            {
                this.spiritField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class raceLeveling
    {

        private decimal strengthField;

        private decimal agilityField;

        private decimal staminaField;

        private decimal intellectField;

        private decimal spiritField;

        /// <remarks/>
        public decimal strength
        {
            get
            {
                return this.strengthField;
            }
            set
            {
                this.strengthField = value;
            }
        }

        /// <remarks/>
        public decimal agility
        {
            get
            {
                return this.agilityField;
            }
            set
            {
                this.agilityField = value;
            }
        }

        /// <remarks/>
        public decimal stamina
        {
            get
            {
                return this.staminaField;
            }
            set
            {
                this.staminaField = value;
            }
        }

        /// <remarks/>
        public decimal intellect
        {
            get
            {
                return this.intellectField;
            }
            set
            {
                this.intellectField = value;
            }
        }

        /// <remarks/>
        public decimal spirit
        {
            get
            {
                return this.spiritField;
            }
            set
            {
                this.spiritField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class raceSpell
    {

        private ushort idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class raceSkill
    {

        private ushort minField;

        private ushort maxField;

        private ushort idField;

        /// <remarks/>
        public ushort min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }

        /// <remarks/>
        public ushort max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }


}