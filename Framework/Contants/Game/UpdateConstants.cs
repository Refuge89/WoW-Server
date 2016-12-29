using System;

namespace Framework.Contants.Game
{
    [Flags]
    public enum ObjectUpdateFlag : byte
    {
        UPDATEFLAG_NONE                 = 0x0000,
        UPDATEFLAG_SELF                 = 0x0001,
        UPDATEFLAG_TRANSPORT            = 0x0002,
        UPDATEFLAG_FULLGUID             = 0x0004,
        UPDATEFLAG_HIGHGUID             = 0x0008,
        UPDATEFLAG_ALL                  = 0x0010,
        UPDATEFLAG_LIVING               = 0x0020,
        UPDATEFLAG_HAS_POSITION         = 0x0040,
    }

    public enum ObjectUpdateType : byte
    {
        UPDATETYPE_VALUES               = 0,
        UPDATETYPE_MOVEMENT             = 1,
        UPDATETYPE_CREATE_OBJECT        = 2,
        UPDATETYPE_CREATE_OBJECT2       = 3,
        UPDATETYPE_OUT_OF_RANGE_OBJECTS = 4,
        UPDATETYPE_NEAR_OBJECTS         = 5,
    }

    public enum TypeID : byte
    {
        TYPEID_OBJECT                   = 0,
        TYPEID_ITEM                     = 1,
        TYPEID_CONTAINER                = 2,
        TYPEID_UNIT                     = 3,
        TYPEID_PLAYER                   = 4,
        TYPEID_GAMEOBJECT               = 5,
        TYPEID_DYNAMICOBJECT            = 6,
        TYPEID_CORPSE                   = 7,
    }

    [Flags]
    public enum MovementFlags
    {
        MOVEFLAG_NONE                   = 0x00000000,
        MOVEFLAG_FORWARD                = 0x00000001,
        MOVEFLAG_BACKWARD               = 0x00000002,
        MOVEFLAG_STRAFE_LEFT            = 0x00000004,
        MOVEFLAG_STRAFE_RIGHT           = 0x00000008,
        MOVEFLAG_TURN_LEFT              = 0x00000010,
        MOVEFLAG_TURN_RIGHT             = 0x00000020,
        MOVEFLAG_PITCH_UP               = 0x00000040,
        MOVEFLAG_PITCH_DOWN             = 0x00000080,
        MOVEFLAG_WALK_MODE              = 0x00000100,               // Walking

        MOVEFLAG_LEVITATING             = 0x00000400,
        MOVEFLAG_ROOT                   = 0x00000800,               // [-ZERO] is it really need and correct value
        MOVEFLAG_FALLING                = 0x00002000,
        MOVEFLAG_FALLINGFAR             = 0x00004000,
        MOVEFLAG_SWIMMING               = 0x00200000,               // appears with fly flag also
        MOVEFLAG_ASCENDING              = 0x00400000,               // [-ZERO] is it really need and correct value
        MOVEFLAG_CAN_FLY                = 0x00800000,               // [-ZERO] is it really need and correct value
        MOVEFLAG_FLYING                 = 0x01000000,               // [-ZERO] is it really need and correct value

        MOVEFLAG_ONTRANSPORT            = 0x02000000,               // Used for flying on some creatures
        MOVEFLAG_SPLINE_ELEVATION       = 0x04000000,               // used for flight paths
        MOVEFLAG_SPLINE_ENABLED         = 0x08000000,               // used for flight paths
        MOVEFLAG_WATERWALKING           = 0x10000000,               // prevent unit from falling through water
        MOVEFLAG_SAFE_FALL              = 0x20000000,               // active rogue safe fall spell (passive)
        MOVEFLAG_HOVER                  = 0x40000000,
    }

    public enum HighGUID
    {
        HIGHGUID_ITEM                   = 0x4700,
        HIGHGUID_CONTAINER              = 0x4700,
        HIGHGUID_PLAYER                 = 0x0000,
        HIGHGUID_GAMEOBJECT             = 0xF110,
        HIGHGUID_TRANSPORT              = 0xF120,
        HIGHGUID_UNIT                   = 0xF130,
        HIGHGUID_PET                    = 0xF140,
        HIGHGUID_VEHICLE                = 0xF150,
        HIGHGUID_DYNAMICOBJECT          = 0xF100,
        HIGHGUID_CORPSE                 = 0xF500,
        HIGHGUID_MO_TRANSPORT           = 0x1FC0,
    }

    public enum EObjectFields : int
    {
        OBJECT_FIELD_GUID               = 0x00, // Size:2
        OBJECT_FIELD_DATA               = 0x01, // Size:2
        OBJECT_FIELD_TYPE               = 0x02, // Size:1
        OBJECT_FIELD_ENTRY              = 0x03, // Size:1
        OBJECT_FIELD_SCALE_X            = 0x04, // Size:1
        OBJECT_FIELD_PADDING            = 0x05, // Size:1
        OBJECT_END                      = 0x06,
    };

    public enum EGameObjectFields
    {
        OBJECT_FIELD_CREATED_BY         = EObjectFields.OBJECT_END + 0x00,
        GAMEOBJECT_DISPLAYID            = EObjectFields.OBJECT_END + 0x02,
        GAMEOBJECT_FLAGS                = EObjectFields.OBJECT_END + 0x03,
        GAMEOBJECT_ROTATION             = EObjectFields.OBJECT_END + 0x04,
        GAMEOBJECT_STATE                = EObjectFields.OBJECT_END + 0x08,
        GAMEOBJECT_POS_X                = EObjectFields.OBJECT_END + 0x09,
        GAMEOBJECT_POS_Y                = EObjectFields.OBJECT_END + 0x0A,
        GAMEOBJECT_POS_Z                = EObjectFields.OBJECT_END + 0x0B,
        GAMEOBJECT_FACING               = EObjectFields.OBJECT_END + 0x0C,
        GAMEOBJECT_DYN_FLAGS            = EObjectFields.OBJECT_END + 0x0D,
        GAMEOBJECT_FACTION              = EObjectFields.OBJECT_END + 0x0E,
        GAMEOBJECT_TYPE_ID              = EObjectFields.OBJECT_END + 0x0F,
        GAMEOBJECT_LEVEL                = EObjectFields.OBJECT_END + 0x10,
        GAMEOBJECT_ARTKIT               = EObjectFields.OBJECT_END + 0x11,
        GAMEOBJECT_ANIMPROGRESS         = EObjectFields.OBJECT_END + 0x12,
        GAMEOBJECT_PADDING              = EObjectFields.OBJECT_END + 0x13,
        GAMEOBJECT_END                  = EObjectFields.OBJECT_END + 0x14,
    };
}
