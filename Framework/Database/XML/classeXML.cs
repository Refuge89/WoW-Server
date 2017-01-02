namespace Framework.Database.Xml
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class classe
    {

        private byte healthField;

        private string atackspeedField;

        private classeStats statsField;

        private classeLeveling levelingField;

        private classeModificadores modificadoresField;

        private classePowers powersField;

        private classeSpell[] spellsField;

        private classeSkill[] skillsField;

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
        public string atackspeed
        {
            get
            {
                return this.atackspeedField;
            }
            set
            {
                this.atackspeedField = value;
            }
        }

        /// <remarks/>
        public classeStats stats
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
        public classeLeveling leveling
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
        public classeModificadores modificadores
        {
            get
            {
                return this.modificadoresField;
            }
            set
            {
                this.modificadoresField = value;
            }
        }

        /// <remarks/>
        public classePowers powers
        {
            get
            {
                return this.powersField;
            }
            set
            {
                this.powersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("spell", IsNullable = false)]
        public classeSpell[] spells
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
        public classeSkill[] skills
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
    public partial class classeStats
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
    public partial class classeLeveling
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
    public partial class classeModificadores
    {

        private classeModificadoresStamina staminaField;

        private classeModificadoresIntellect intellectField;

        /// <remarks/>
        public classeModificadoresStamina stamina
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
        public classeModificadoresIntellect intellect
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
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class classeModificadoresStamina
    {

        private byte baseField;

        private byte modField;

        /// <remarks/>
        public byte @base
        {
            get
            {
                return this.baseField;
            }
            set
            {
                this.baseField = value;
            }
        }

        /// <remarks/>
        public byte mod
        {
            get
            {
                return this.modField;
            }
            set
            {
                this.modField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class classeModificadoresIntellect
    {

        private byte baseField;

        private byte modField;

        /// <remarks/>
        public byte @base
        {
            get
            {
                return this.baseField;
            }
            set
            {
                this.baseField = value;
            }
        }

        /// <remarks/>
        public byte mod
        {
            get
            {
                return this.modField;
            }
            set
            {
                this.modField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class classePowers
    {

        private byte manaField;

        private ushort rageField;

        private byte energyField;

        /// <remarks/>
        public byte mana
        {
            get
            {
                return this.manaField;
            }
            set
            {
                this.manaField = value;
            }
        }

        /// <remarks/>
        public ushort rage
        {
            get
            {
                return this.rageField;
            }
            set
            {
                this.rageField = value;
            }
        }

        /// <remarks/>
        public byte energy
        {
            get
            {
                return this.energyField;
            }
            set
            {
                this.energyField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class classeSpell
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
    public partial class classeSkill
    {

        private byte minField;

        private ushort maxField;

        private ushort idField;

        /// <remarks/>
        public byte min
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