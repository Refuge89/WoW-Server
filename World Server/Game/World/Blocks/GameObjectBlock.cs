using System;
using Framework.Contants.Game;
using Framework.Extensions;
using World_Server.Game.Entitys;

namespace World_Server.Game.World.Blocks
{
    public class GameObjectBlock: UpdateBlock
    {
        public GameObjectEntityEntity EntityEntity { get; }

        public GameObjectBlock(GameObjectEntityEntity entityEntity)
        {
            EntityEntity = entityEntity;
            Build();
        }

        public override void BuildData()
        {
            Writer.Write((byte) ObjectUpdateType.UPDATETYPE_CREATE_OBJECT);
            Writer.WritePackedUInt64(EntityEntity.ObjectGuid.RawGuid);
            Writer.Write((byte) TypeID.TYPEID_GAMEOBJECT);

            ObjectUpdateFlag updateFlags = ObjectUpdateFlag.UPDATEFLAG_TRANSPORT |
                                           ObjectUpdateFlag.UPDATEFLAG_ALL |
                                           ObjectUpdateFlag.UPDATEFLAG_HAS_POSITION;

            Writer.Write((byte)updateFlags);

            // Position
            Writer.Write(EntityEntity.GameObjects.mapX);
            Writer.Write(EntityEntity.GameObjects.mapY);
            Writer.Write(EntityEntity.GameObjects.mapZ);

            Writer.Write((float) 0); // R

            Writer.Write((uint) 0x1); // Unkown... time?
            Writer.Write((uint) 0); // Unkown... time?

            EntityEntity.WriteUpdateFields(Writer);
        }
    }
}
