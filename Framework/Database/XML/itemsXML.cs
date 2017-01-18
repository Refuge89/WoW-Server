using System.Xml.Serialization;

namespace Framework.Database.XML
{

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class items
    {

        private itemsItem[] itemField;

        /// <remarks/>
        [XmlElement("item")]
        public itemsItem[] item
        {
            get { return itemField; }
            set { itemField = value; }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class itemsItem
    {

        private string nameField;

        private ushort displayIdField;

        private byte inventoryTypeField;

        private bool inventoryTypeFieldSpecified;

        private byte slotField;

        private bool slotFieldSpecified;

        private byte classField;

        private ushort idField;

        /// <remarks/>
        public string name
        {
            get { return nameField; }
            set { nameField = value; }
        }

        /// <remarks/>
        public ushort DisplayId
        {
            get { return displayIdField; }
            set { displayIdField = value; }
        }

        /// <remarks/>
        public byte InventoryType
        {
            get { return inventoryTypeField; }
            set { inventoryTypeField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool InventoryTypeSpecified
        {
            get { return inventoryTypeFieldSpecified; }
            set { inventoryTypeFieldSpecified = value; }
        }

        /// <remarks/>
        public byte Slot
        {
            get { return slotField; }
            set { slotField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool SlotSpecified
        {
            get { return slotFieldSpecified; }
            set { slotFieldSpecified = value; }
        }

        /// <remarks/>
        public byte Class
        {
            get { return classField; }
            set { classField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort id
        {
            get { return idField; }
            set { idField = value; }
        }
    }

}