using System.Drawing;
using Framework.Contants.Game;
using Framework.Database.Tables;
using World_Server.Game.Update;

namespace World_Server.Game.Entitys
{
    public class GameObject : Object
    {
        public override TypeID TypeId => TypeID.TYPEID_GAMEOBJECT;
        public override int DataLength => (int)GameObjectFields.GAMEOBJECT_END;

        public TemplateGameObjects GameObjectTemplate { get; }
        public WorldGameObjects GameObjects { get; private set; }

        public override string Name => GameObjectTemplate.name;

        public GameObject(WorldGameObjects gameObject) : base(ObjectGuid.GetGameObjectGuid())
        {
            GameObjects = gameObject;
            GameObjectTemplate = gameObject.entry;

            Type = 0x21;
            Entry = (byte)GameObjectTemplate.Id;
            Scale = (GameObjectTemplate.size > 100) ? 1 : GameObjectTemplate.size;
            DisplayId = (int)GameObjectTemplate.displayId;
            Flags = (int)GameObjectTemplate.flags;
            GoTypeId = GameObjectTemplate.type;
            X = gameObject.mapX;
            Y = gameObject.mapY;
            Z = gameObject.mapZ;

            SetUpdateField((int)GameObjectFields.GAMEOBJECT_FACING, gameObject.mapR);
            SetUpdateField((int)GameObjectFields.GAMEOBJECT_DYN_FLAGS, GameObjectTemplate.flags);

            #if DEBUG
            Main._Main.Log($"Spawn Object [{GameObjectTemplate.name}] para [{(GameObjectType)GameObjectTemplate.type}]", Color.Black);
            #endif
        }

        public int DisplayId
        {
            get { return (int)UpdateData[GameObjectFields.GAMEOBJECT_DISPLAYID]; }
            set { SetUpdateField((int)GameObjectFields.GAMEOBJECT_DISPLAYID, value); }
        }

        public int Flags
        {
            get { return (int)UpdateData[GameObjectFields.GAMEOBJECT_FLAGS]; }
            set { SetUpdateField((int)GameObjectFields.GAMEOBJECT_FLAGS, value); }
        }

        public int GoTypeId
        {
            get { return (int)UpdateData[GameObjectFields.GAMEOBJECT_TYPE_ID]; }
            set { SetUpdateField((int)GameObjectFields.GAMEOBJECT_TYPE_ID, value); }
        }

        public float X
        {
            get { return (float)UpdateData[GameObjectFields.GAMEOBJECT_POS_X]; }
            set { SetUpdateField((int)GameObjectFields.GAMEOBJECT_POS_X, value); }
        }

        public float Y
        {
            get { return (float)UpdateData[GameObjectFields.GAMEOBJECT_POS_Y]; }
            set { SetUpdateField((int)GameObjectFields.GAMEOBJECT_POS_Y, value); }
        }

        public float Z
        {
            get { return (float)UpdateData[GameObjectFields.GAMEOBJECT_POS_Z]; }
            set { SetUpdateField((int)GameObjectFields.GAMEOBJECT_POS_Z, value); }
        }
    }

}
