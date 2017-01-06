using System;

namespace Framework.Contants.Game
{
    public enum ObjectUpdateType : byte
    {
        UPDATETYPE_VALUES                           = 0,
        UPDATETYPE_MOVEMENT                         = 1,
        UPDATETYPE_CREATE_OBJECT                    = 2,
        UPDATETYPE_CREATE_OBJECT2                   = 3,
        UPDATETYPE_OUT_OF_RANGE_OBJECTS             = 4,
        UPDATETYPE_NEAR_OBJECTS                     = 5,
    }

    [Flags]
    public enum TypeID : byte
    {
        TYPEID_OBJECT                               = 0,
        TYPEID_ITEM                                 = 1,
        TYPEID_CONTAINER                            = 2,
        TYPEID_UNIT                                 = 3,
        TYPEID_PLAYER                               = 4,
        TYPEID_GAMEOBJECT                           = 5,
        TYPEID_DYNAMICOBJECT                        = 6,
        TYPEID_CORPSE                               = 7,
    }

    [Flags]
    public enum ObjectFlags : byte
    {
        UPDATEFLAG_NONE                             = 0x0000,
        UPDATEFLAG_SELF                             = 0x0001,
        UPDATEFLAG_TRANSPORT                        = 0x0002,
        UPDATEFLAG_FULLGUID                         = 0x0004,
        UPDATEFLAG_HIGHGUID                         = 0x0008,
        UPDATEFLAG_ALL                              = 0x0010,
        UPDATEFLAG_LIVING                           = 0x0020,
        UPDATEFLAG_HAS_POSITION                     = 0x0040,
    }
}
