using System;
using Framework.Contants.Game;
using Framework.Database.Tables;
using World_Server.Game.Update;

namespace World_Server.Game.Entitys
{
    public class ContainerEntity : ObjectEntity
    {
        public override TypeID TypeId => TypeID.TYPEID_CONTAINER;
        public override int DataLength => (int)EContainerFields.CONTAINER_END;

        public ContainerEntity(Character character) : base(new ObjectGuid((uint)character.Id, TypeID.TYPEID_CONTAINER, HighGuid.HighguidContainer))
        {
            Guid = 1;
            //this.Type = (byte) TypeID.TYPEID_CONTAINER;
            Entry = 1;
            Scale = 1f;

            NumSlots = 20;

            var Items = Main.Database.GetInventory(character);
            for (int i = 0; i < 20; i++)
            {
                //if(Items[i] != null)
                    //SetUpdateField((int)EContainerFields.CONTAINER_FIELD_SLOT_1, i * 2, (byte)Items[i].Item);
                //else
                    SetUpdateField((int)EContainerFields.CONTAINER_FIELD_SLOT_1, i * 2, (byte)Items[4].Id);
            }
        }

        public int NumSlots
        {
            get { return (int) UpdateData[EContainerFields.CONTAINER_FIELD_NUM_SLOTS]; }
            set { SetUpdateField((int)EContainerFields.CONTAINER_FIELD_NUM_SLOTS, value); }
        }

        public int Slot
        {
            get { return (int)UpdateData[EContainerFields.CONTAINER_FIELD_SLOT_1]; }
            set { SetUpdateField((int)EContainerFields.CONTAINER_FIELD_SLOT_1, value); }
        }

    }
}
