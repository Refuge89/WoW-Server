using System.Xml.Serialization;

namespace Framework.Database.Xml
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class race
    {

        private byte healthField;

        private raceStats statsField;

        private raceLeveling levelingField;

        private raceSpell[] spellsField;

        private raceSkill[] skillsField;

        private raceClass[] classesField;

        private byte idField;

        /// <remarks/>
        public byte health
        {
            get { return healthField; }
            set { healthField = value; }
        }

        /// <remarks/>
        public raceStats stats
        {
            get { return statsField; }
            set { statsField = value; }
        }

        /// <remarks/>
        public raceLeveling leveling
        {
            get { return levelingField; }
            set { levelingField = value; }
        }

        /// <remarks/>
        [XmlArrayItem("spell", IsNullable = false)]
        public raceSpell[] spells
        {
            get { return spellsField; }
            set { spellsField = value; }
        }

        /// <remarks/>
        [XmlArrayItem("skill", IsNullable = false)]
        public raceSkill[] skills
        {
            get { return skillsField; }
            set { skillsField = value; }
        }

        /// <remarks/>
        [XmlArrayItem("class", IsNullable = false)]
        public raceClass[] classes
        {
            get { return classesField; }
            set { classesField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte id
        {
            get { return idField; }
            set { idField = value; }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class raceStats
    {

        private byte strengthField;

        private byte agilityField;

        private byte staminaField;

        private byte intellectField;

        private byte spiritField;

        /// <remarks/>
        public byte strength
        {
            get { return strengthField; }
            set { strengthField = value; }
        }

        /// <remarks/>
        public byte agility
        {
            get { return agilityField; }
            set { agilityField = value; }
        }

        /// <remarks/>
        public byte stamina
        {
            get { return staminaField; }
            set { staminaField = value; }
        }

        /// <remarks/>
        public byte intellect
        {
            get { return intellectField; }
            set { intellectField = value; }
        }

        /// <remarks/>
        public byte spirit
        {
            get { return spiritField; }
            set { spiritField = value; }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class raceLeveling
    {

        private decimal strengthField;

        private decimal agilityField;

        private decimal staminaField;

        private decimal intellectField;

        private decimal spiritField;

        /// <remarks/>
        public decimal strength
        {
            get { return strengthField; }
            set { strengthField = value; }
        }

        /// <remarks/>
        public decimal agility
        {
            get { return agilityField; }
            set { agilityField = value; }
        }

        /// <remarks/>
        public decimal stamina
        {
            get { return staminaField; }
            set { staminaField = value; }
        }

        /// <remarks/>
        public decimal intellect
        {
            get { return intellectField; }
            set { intellectField = value; }
        }

        /// <remarks/>
        public decimal spirit
        {
            get { return spiritField; }
            set { spiritField = value; }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class raceSpell
    {

        private ushort idField;

        /// <remarks/>
        [XmlAttribute]
        public ushort id
        {
            get { return idField; }
            set { idField = value; }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class raceSkill
    {

        private ushort idField;

        private ushort minField;

        private ushort maxField;

        /// <remarks/>
        [XmlAttribute]
        public ushort id
        {
            get { return idField; }
            set { idField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort min
        {
            get { return minField; }
            set { minField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort max
        {
            get { return maxField; }
            set { maxField = value; }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class raceClass
    {

        private raceClassSpell[] spellsField;

        private raceClassSkill[] skillsField;

        private raceClassItem[] itemsField;

        private raceClassAction[] actionsField;

        private string idField;

        /// <remarks/>
        [XmlArrayItem("spell", IsNullable = false)]
        public raceClassSpell[] spells
        {
            get { return spellsField; }
            set { spellsField = value; }
        }

        /// <remarks/>
        [XmlArrayItem("skill", IsNullable = false)]
        public raceClassSkill[] skills
        {
            get { return skillsField; }
            set { skillsField = value; }
        }

        /// <remarks/>
        [XmlArrayItem("item", IsNullable = false)]
        public raceClassItem[] items
        {
            get { return itemsField; }
            set { itemsField = value; }
        }

        /// <remarks/>
        [XmlArrayItem("action", IsNullable = false)]
        public raceClassAction[] actions
        {
            get { return actionsField; }
            set { actionsField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string id
        {
            get { return idField; }
            set { idField = value; }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class raceClassSpell
    {

        private ushort idField;

        /// <remarks/>
        [XmlAttribute]
        public ushort id
        {
            get { return idField; }
            set { idField = value; }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class raceClassSkill
    {

        private byte idField;

        private byte minField;

        private ushort maxField;

        /// <remarks/>
        [XmlAttribute]
        public byte id
        {
            get { return idField; }
            set { idField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte min
        {
            get { return minField; }
            set { minField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort max
        {
            get { return maxField; }
            set { maxField = value; }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class raceClassItem
    {

        private ushort idField;

        /// <remarks/>
        [XmlAttribute]
        public ushort id
        {
            get { return idField; }
            set { idField = value; }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class raceClassAction
    {

        private byte buttonField;

        private ushort actionField;

        private byte typeField;

        /// <remarks/>
        [XmlAttribute]
        public byte button
        {
            get { return buttonField; }
            set { buttonField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort action
        {
            get { return actionField; }
            set { actionField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte type
        {
            get { return typeField; }
            set { typeField = value; }
        }
    }
}