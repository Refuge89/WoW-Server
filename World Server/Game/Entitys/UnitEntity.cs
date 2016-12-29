using Framework.Contants.Game;
using Framework.Helpers;

namespace World_Server.Game.Entitys
{
    public class UnitEntity : ObjectEntity
    {
        public UnitEntity(ObjectGUID objectGUID) : base(objectGUID)
        {
        }

        public int Health
        {
            get { return (int)UpdateData[(int)EUnitFields.UNIT_FIELD_HEALTH]; }
            set { SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_HEALTH, value); }
        }

        public int MaxHealth
        {
            get { return (int)UpdateData[(int)EUnitFields.UNIT_FIELD_MAXHEALTH]; }
            set { SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_MAXHEALTH, value); }
        }

        public int Level
        {
            get { return (int)UpdateData[(int)EUnitFields.UNIT_FIELD_LEVEL]; }
            set { SetUpdateField<int>((int)EUnitFields.UNIT_FIELD_LEVEL, value); }
        }
    }
}
