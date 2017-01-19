using System;
using Framework.Contants.Game;

namespace World_Server.Game.Update
{
    public class UpdateField : Attribute
    {
        public byte Enum;
        public bool RequiredOnCreation;
        public int Index;

        public UpdateField(byte updateFieldEnum, bool requiredOnCreation = true, int index = -1)
        {
            Enum = updateFieldEnum;
            RequiredOnCreation = requiredOnCreation;
            Index = index;
        }

        public UpdateField(ObjectFields objectFields, bool requiredOnCreation = true) : this((byte)objectFields, requiredOnCreation) { }

        public UpdateField(GameObjectFields gameObjectFields, bool requiredOnCreation = true) : this((byte)gameObjectFields, requiredOnCreation) { }

        public UpdateField(UnitFields eUnitFields, bool requiredOnCreation = true, int index = -1) : this((byte)eUnitFields, requiredOnCreation, index) { }
    }
}
