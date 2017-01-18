using System;
using Framework.Contants.Game;
using Framework.Database;
using Framework.Database.Tables;
using World_Server.Game.Update;

namespace World_Server.Game.Entitys
{
    public class Item : Object
    {
        public override TypeID TypeId => TypeID.TYPEID_ITEM;
        public override int DataLength => (int)EItemFields.ITEM_END;

        public Item(Character character, CharactersInventory item) : base(new ObjectGuid((uint)character.Id, TypeID.TYPEID_ITEM, HighGuid.HighguidItem))
        {
            var ItemAtributes = XmlManager.GetItem((uint)item.Item);

            this.Guid = (uint)item.Id;
            this.Type = (byte)TypeID.TYPEID_ITEM;
            this.Entry = (byte)item.Item;
            this.Scale = 1f;

            this.Owner = 1;
            this.Contained = 0;

            Console.WriteLine($"ItemEntity: [ID {item.Id}] / [Entry {item.Item}]");

            this.Durability = 20;
            this.MaxDurability = 20;
            this.StackCount = 1;
        }

        public int Owner
        {
            get { return (int)UpdateData[EItemFields.ITEM_FIELD_OWNER]; }
            set { SetUpdateField<int>((int)EItemFields.ITEM_FIELD_OWNER, value); }
        }

        public int Contained
        {
            get { return (int)UpdateData[EItemFields.ITEM_FIELD_CONTAINED]; }
            set { SetUpdateField<int>((int)EItemFields.ITEM_FIELD_CONTAINED, value); }
        }

        public int Creator
        {
            get { return (int)UpdateData[EItemFields.ITEM_FIELD_CREATOR]; }
            set { SetUpdateField<int>((int)EItemFields.ITEM_FIELD_CREATOR, value); }
        }

        public int GiftCreator
        {
            get { return (int)UpdateData[EItemFields.ITEM_FIELD_GIFTCREATOR]; }
            set { SetUpdateField<int>((int)EItemFields.ITEM_FIELD_GIFTCREATOR, value); }
        }

        public int StackCount
        {
            get { return (int)UpdateData[EItemFields.ITEM_FIELD_STACK_COUNT]; }
            set { SetUpdateField<int>((int)EItemFields.ITEM_FIELD_STACK_COUNT, value); }
        }

        public int Duration
        {
            get { return (int)UpdateData[EItemFields.ITEM_FIELD_DURATION]; }
            set { SetUpdateField<int>((int)EItemFields.ITEM_FIELD_DURATION, value); }
        }

        public int Flags
        {
            get { return (int)UpdateData[EItemFields.ITEM_FIELD_FLAGS]; }
            set { SetUpdateField<int>((int)EItemFields.ITEM_FIELD_FLAGS, value); }
        }

        public int PropertySeed
        {
            get { return (int)UpdateData[EItemFields.ITEM_FIELD_PROPERTY_SEED]; }
            set { SetUpdateField<int>((int)EItemFields.ITEM_FIELD_PROPERTY_SEED, value); }
        }

        public int RandomPropertiesId
        {
            get { return (int)UpdateData[EItemFields.ITEM_FIELD_RANDOM_PROPERTIES_ID]; }
            set { SetUpdateField<int>((int)EItemFields.ITEM_FIELD_RANDOM_PROPERTIES_ID, value); }
        }

        public int Durability
        {
            get { return (int)UpdateData[EItemFields.ITEM_FIELD_DURABILITY]; }
            set { SetUpdateField<int>((int)EItemFields.ITEM_FIELD_DURABILITY, value); }
        }

        public int MaxDurability
        {
            get { return (int)UpdateData[EItemFields.ITEM_FIELD_MAXDURABILITY]; }
            set { SetUpdateField<int>((int)EItemFields.ITEM_FIELD_MAXDURABILITY, value); }
        }

        public int ItemTextId
        {
            get { return (int)UpdateData[EItemFields.ITEM_FIELD_ITEM_TEXT_ID]; }
            set { SetUpdateField<int>((int)EItemFields.ITEM_FIELD_ITEM_TEXT_ID, value); }
        }
    }
}
