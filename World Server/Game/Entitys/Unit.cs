using Framework.Contants.Game;
using World_Server.Game.Update;

namespace World_Server.Game.Entitys
{
    public class Unit : Object
    {
        public Unit(ObjectGuid guid) : base(guid)
        {
        }

        public float X, Y, Z, R;

        public override int DataLength => (int) EUnitFields.UNIT_END - 0x4;

        public byte PowerType = 0;

        public int Health
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_HEALTH]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_HEALTH, value); }
        }

        public int MaxHealth
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_MAXHEALTH]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_MAXHEALTH, value); }
        }

        public int Level
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_LEVEL]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_LEVEL, value); }
        }

        public int EmoteState
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_NPC_EMOTESTATE]; }
            set { SetUpdateField((int) EUnitFields.UNIT_NPC_EMOTESTATE, value); }
        }

        public int DisplayId
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_DISPLAYID]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_DISPLAYID, value); }
        }

        public int UnitFlag
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_FLAGS]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_FLAGS, value); }
        }

        public int StandState
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_BYTES_1]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_BYTES_1, value, 1); }
        }

        public int StandStateFlags
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_BYTES_1]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_BYTES_1, value, 3); }
        }

        public int NativeDisplayID
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_NATIVEDISPLAYID]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_NATIVEDISPLAYID, value); }
        }

        public int FactionTemplate
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_FACTIONTEMPLATE]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_FACTIONTEMPLATE, value); }
        }

        public int RaceUnit
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_BYTES_0]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_BYTES_0, (byte)value, 0); }
        }

        public int ClassUnit
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_BYTES_0]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_BYTES_0, (byte)value, 1); }
        }

        public int Gender
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_BYTES_0]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_BYTES_0, (byte)value, 2); }
        }

        public int Power
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_BYTES_0]; }
            set { SetUpdateField<byte>((int) EUnitFields.UNIT_FIELD_BYTES_0, (byte)value, 3); }
        }

        public int Mana
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_POWER1]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_POWER1, value); }
        }

        public int MaxMana
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_MAXPOWER1]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_MAXPOWER1, value); }
        }

        public int MaxRage
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_MAXPOWER2]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_MAXPOWER2, value); }
        }

        public int SpawnBytes0
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_BYTES_0]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_BYTES_0, value, 1); }
        }

        public int SpawnBytes1
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_BYTES_1]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_BYTES_1, value, 1); }
        }

        public int SpawnBytes2
        {
            get { return (int) UpdateData[(int) EUnitFields.UNIT_FIELD_BYTES_2]; }
            set { SetUpdateField((int) EUnitFields.UNIT_FIELD_BYTES_2, value, 1); }
        }
    }
}
