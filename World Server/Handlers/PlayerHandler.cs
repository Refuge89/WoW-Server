using Framework.Contants;
using Framework.Database.Tables;
using Framework.Extensions;
using Framework.Network;
using System;
using System.Text;

namespace World_Server.Handlers
{

    #region CMSG_PLAYER_LOGIN
    public class CMSG_PLAYER_LOGIN : PacketReader
    {
        public uint GUID { get; private set; }

        public CMSG_PLAYER_LOGIN(byte[] data) : base(data)
        {
            GUID = ReadUInt32();
        }
    }
    #endregion

    #region SMSG_LOGIN_VERIFY_WORLD
    public class SMSG_LOGIN_VERIFY_WORLD : ServerPacket
    {
        public SMSG_LOGIN_VERIFY_WORLD(Character character) : base(WorldOpcodes.SMSG_LOGIN_VERIFY_WORLD)
        {
            Write(character.MapID);
            Write(character.MapX);
            Write(character.MapY);
            Write(character.MapZ);
            Write(character.MapRotation);
        }
    }
    #endregion

    #region SMSG_ACCOUNT_DATA_TIMES
    class SMSG_ACCOUNT_DATA_TIMES : ServerPacket
    {
        public SMSG_ACCOUNT_DATA_TIMES() : base(WorldOpcodes.SMSG_ACCOUNT_DATA_TIMES)
        {
            this.WriteNullByte(128);
        }
    }
    #endregion

    #region SMSG_SET_REST_START
    class SMSG_SET_REST_START : ServerPacket
    {
        public SMSG_SET_REST_START() : base(WorldOpcodes.SMSG_SET_REST_START)
        {
            Write((byte)0);
        }
    }
    #endregion

    #region SMSG_TUTORIAL_FLAGS
    class SMSG_TUTORIAL_FLAGS : ServerPacket
    {
        //TODO Write the uint ids of 8 tutorial values
        public SMSG_TUTORIAL_FLAGS() : base(WorldOpcodes.SMSG_TUTORIAL_FLAGS)
        {
            this.WriteNullUInt(8);
        }
    }
    #endregion

    #region SMSG_LOGIN_SETTIMESPEED
    public class SMSG_LOGIN_SETTIMESPEED : ServerPacket
    {
        public SMSG_LOGIN_SETTIMESPEED() : base(WorldOpcodes.SMSG_LOGIN_SETTIMESPEED)
        {
            Write((uint)secsToTimeBitFields(DateTime.Now)); // Time
            Write((float)0.01666667f); // Speed
        }

        public static int secsToTimeBitFields(DateTime dateTime)
        {
            return (dateTime.Year - 100) << 24 | dateTime.Month << 20 | (dateTime.Day - 1) << 14 | (int)dateTime.DayOfWeek << 11 | dateTime.Hour << 6 | dateTime.Minute;
        }
    }
    #endregion

    #region SMSG_TRIGGER_CINEMATIC
    public class SMSG_TRIGGER_CINEMATIC : ServerPacket
    {
        public SMSG_TRIGGER_CINEMATIC(int CinematicId) : base(WorldOpcodes.SMSG_TRIGGER_CINEMATIC)
        {
            Write((int)CinematicId);
        }
    }
    #endregion

    #region CMSG_NAME_QUERY
    public class CMSG_NAME_QUERY : PacketReader
    {
        public uint GUID { get; private set; }

        public CMSG_NAME_QUERY(byte[] data) : base(data)
        {
            GUID = ReadUInt32();
        }
    }
    #endregion

    #region SMSG_NAME_QUERY_RESPONSE
    public class SMSG_NAME_QUERY_RESPONSE : ServerPacket
    {
        public SMSG_NAME_QUERY_RESPONSE(Character character) : base(WorldOpcodes.SMSG_NAME_QUERY_RESPONSE)
        {
            Write((ulong)character.Id);
            Write(Encoding.UTF8.GetBytes(character.Name + '\0'));
            Write((byte)0); // realm name for cross realm BG usage
            Write((uint)character.Race);
            Write((uint)character.Gender);
            Write((uint)character.Class);
        }
    }
    #endregion

    #region SMSG_LOGOUT_RESPONSE
    public class SMSG_LOGOUT_RESPONSE : ServerPacket
    {
        public SMSG_LOGOUT_RESPONSE() : base(WorldOpcodes.SMSG_LOGOUT_RESPONSE)
        {
            Write((UInt32)0);
            Write((byte)0);
        }
    }
    #endregion

    #region SMSG_LOGOUT_CANCEL_ACK
    class SMSG_LOGOUT_CANCEL_ACK : ServerPacket
    {
        public SMSG_LOGOUT_CANCEL_ACK() : base(WorldOpcodes.SMSG_LOGOUT_CANCEL_ACK)
        {
            Write((byte)0);
        }
    }
    #endregion

    #region SMSG_LOGOUT_COMPLETE
    class SMSG_LOGOUT_COMPLETE : ServerPacket
    {
        public SMSG_LOGOUT_COMPLETE() : base(WorldOpcodes.SMSG_LOGOUT_COMPLETE)
        {
            Write((byte)0);
        }
    }
    #endregion

}
