using System;
using Framework.Contants.Game;
using Framework.Extensions;
using World_Server.Game.Entitys;

namespace World_Server.Game.World.Blocks
{
    public class GameObjectBlock: UpdateBlock
    {
        public GameObject Entity { get; }

        public GameObjectBlock(GameObject entity)
        {
            Entity = entity;
            Build();
        }

        public override void BuildData()
        {
            Writer.Write((byte) ObjectUpdateType.UPDATETYPE_CREATE_OBJECT);
            Writer.WritePackedUInt64(Entity.ObjectGuid.RawGuid);
            Writer.Write((byte) TypeID.TYPEID_GAMEOBJECT);

            ObjectUpdateFlag updateFlags = ObjectUpdateFlag.UPDATEFLAG_TRANSPORT |
                                           ObjectUpdateFlag.UPDATEFLAG_ALL |
                                           ObjectUpdateFlag.UPDATEFLAG_HAS_POSITION;

            Writer.Write((byte)updateFlags);

            // Position
            Writer.Write((float) Entity.GameObjects.mapX);
            Writer.Write((float) Entity.GameObjects.mapY);
            Writer.Write((float) Entity.GameObjects.mapZ);

            Writer.Write((float) 0); // R

            Writer.Write((uint) 0x1); // Unkown... time?
            Writer.Write((uint) 0); // Unkown... time?

            Entity.WriteUpdateFields(Writer);
        }

        public override string BuildInfo()
        {
            return "[CreateGO] " + Entity.GameObjectTemplate.name;
        }
    }

    [Flags]
    public enum ObjectUpdateFlag : byte
    {
        UPDATEFLAG_NONE = 0x0000,
        UPDATEFLAG_SELF = 0x0001,
        UPDATEFLAG_TRANSPORT = 0x0002,
        UPDATEFLAG_FULLGUID = 0x0004,
        UPDATEFLAG_HIGHGUID = 0x0008,
        UPDATEFLAG_ALL = 0x0010,
        UPDATEFLAG_LIVING = 0x0020,
        UPDATEFLAG_HAS_POSITION = 0x0040
    }
}
