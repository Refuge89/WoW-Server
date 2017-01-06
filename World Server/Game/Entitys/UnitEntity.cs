using Framework.Contants.Game;
using World_Server.Game.Update;

namespace World_Server.Game.Entitys
{
    public class UnitEntity : ObjectEntity
    {
        public UnitEntity(ObjectGuid objectGUID) : base(objectGUID)
        {
        }

        public float X, Y, Z, R;

        public override int DataLength
        {
            get { return (int)EUnitFields.UNIT_END - 0x4; }
        }

        public override string Name { get { return "Abacate";/*Template.name;*/ } }

        public int Health
        {
            get { return (int)UpdateData[(int)EUnitFields.UNIT_FIELD_HEALTH]; }
            set { SetUpdateField((int)EUnitFields.UNIT_FIELD_HEALTH, value); }
        }

        public int MaxHealth
        {
            get { return (int)UpdateData[(int)EUnitFields.UNIT_FIELD_MAXHEALTH]; }
            set { SetUpdateField((int)EUnitFields.UNIT_FIELD_MAXHEALTH, value); }
        }

        public int Level
        {
            get { return (int)UpdateData[(int)EUnitFields.UNIT_FIELD_LEVEL]; }
            set { SetUpdateField((int)EUnitFields.UNIT_FIELD_LEVEL, value); }
        }

        public int EmoteState
        {
            get { return (int)UpdateData[(int)EUnitFields.UNIT_NPC_EMOTESTATE]; }
            set { SetUpdateField((int)EUnitFields.UNIT_NPC_EMOTESTATE, value); }
        }

        public int DisplayID
        {
            get { return (int)UpdateData[(int)EUnitFields.UNIT_FIELD_DISPLAYID]; }
            set { SetUpdateField((int)EUnitFields.UNIT_FIELD_DISPLAYID, value); }
        }
    }
}
