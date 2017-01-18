using Framework.Contants.Game;
using World_Server.Game.Update;

namespace World_Server.Game.Entitys
{
    public class Object : EntityBase
    {
        public ObjectGuid ObjectGuid { get; set; }

        public override int DataLength => (int)EObjectFields.OBJECT_END;

        public ulong Guid
        {
            get { return (ulong) UpdateData[EObjectFields.OBJECT_FIELD_GUID]; }
            set { SetUpdateField((int) EObjectFields.OBJECT_FIELD_GUID, value); }
        }

        public byte Type
        {
            get { return (byte) UpdateData[(int) EObjectFields.OBJECT_FIELD_TYPE]; }
            set { SetUpdateField((int) EObjectFields.OBJECT_FIELD_TYPE, value); }
        }

        public byte Entry
        {
            get { return (byte) UpdateData[(int) EObjectFields.OBJECT_FIELD_ENTRY]; }
            set { SetUpdateField((int) EObjectFields.OBJECT_FIELD_ENTRY, value); }
        }

        public float Scale
        {
            get { return (float) UpdateData[(int) EObjectFields.OBJECT_FIELD_SCALE_X]; }
            set { SetUpdateField((int) EObjectFields.OBJECT_FIELD_SCALE_X, value); }
        }

        public Object(ObjectGuid objectGuid)
        {
            ObjectGuid = objectGuid;
            Guid = ObjectGuid.RawGuid;
        }
    }
}