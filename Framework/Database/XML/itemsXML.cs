namespace Framework.Database.XML
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class items
    {

        private itemsItem[] itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item")]
        public itemsItem[] item
        {
            get { return this.itemField; }
            set { this.itemField = value; }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class itemsItem
    {

        private string nameField;

        private ushort displayIdField;

        private byte classField;

        private bool classFieldSpecified;

        private byte typeField;

        private bool typeFieldSpecified;

        private byte slotField;

        private bool slotFieldSpecified;

        private ushort idField;

        /// <remarks/>
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }

        /// <remarks/>
        public ushort DisplayId
        {
            get { return this.displayIdField; }
            set { this.displayIdField = value; }
        }

        /// <remarks/>
        public byte Class
        {
            get { return this.classField; }
            set { this.classField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ClassSpecified
        {
            get { return this.classFieldSpecified; }
            set { this.classFieldSpecified = value; }
        }

        /// <remarks/>
        public byte Type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TypeSpecified
        {
            get { return this.typeFieldSpecified; }
            set { this.typeFieldSpecified = value; }
        }

        /// <remarks/>
        public byte Slot
        {
            get { return this.slotField; }
            set { this.slotField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SlotSpecified
        {
            get { return this.slotFieldSpecified; }
            set { this.slotFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort id
        {
            get { return this.idField; }
            set { this.idField = value; }
        }
    }
}