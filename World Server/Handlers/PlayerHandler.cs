using Framework.Contants;
using Framework.Database.Tables;
using Framework.Extensions;
using Framework.Network;
using System;
using System.Text;

namespace World_Server.Handlers
{

    #region CMSG_PLAYER_LOGIN
    public sealed class CmsgPlayerLogin : PacketReader
    {
        public uint Guid { get; private set; }

        public CmsgPlayerLogin(byte[] data) : base(data)
        {
            Guid = ReadUInt32();
        }
    }
    #endregion

    #region SMSG_LOGIN_VERIFY_WORLD
    public sealed class SmsgLoginVerifyWorld : ServerPacket
    {
        public SmsgLoginVerifyWorld(Character character) : base(WorldOpcodes.SMSG_LOGIN_VERIFY_WORLD)
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
    class SmsgAccountDataTimes : ServerPacket
    {
        public SmsgAccountDataTimes() : base(WorldOpcodes.SMSG_ACCOUNT_DATA_TIMES)
        {
            this.WriteNullByte(128);
        }
    }
    #endregion

    #region SMSG_SET_REST_START
    sealed class SmsgSetRestStart : ServerPacket
    {
        public SmsgSetRestStart() : base(WorldOpcodes.SMSG_SET_REST_START)
        {
            Write((byte)0);
        }
    }
    #endregion

    #region SMSG_TUTORIAL_FLAGS
    class SmsgTutorialFlags : ServerPacket
    {
        //TODO Write the uint ids of 8 tutorial values
        public SmsgTutorialFlags() : base(WorldOpcodes.SMSG_TUTORIAL_FLAGS)
        {
            this.WriteNullUInt(8);
        }
    }
    #endregion

    #region SMSG_LOGIN_SETTIMESPEED
    public sealed class SmsgLoginSettimespeed : ServerPacket
    {
        public SmsgLoginSettimespeed() : base(WorldOpcodes.SMSG_LOGIN_SETTIMESPEED)
        {
            Write((uint)SecsToTimeBitFields(DateTime.Now)); // Time
            Write((float)0.01666667f); // Speed
        }

        public static int SecsToTimeBitFields(DateTime dateTime)
        {
            return (dateTime.Year - 100) << 24 | dateTime.Month << 20 | (dateTime.Day - 1) << 14 | (int)dateTime.DayOfWeek << 11 | dateTime.Hour << 6 | dateTime.Minute;
        }
    }
    #endregion

    #region SMSG_TRIGGER_CINEMATIC
    public sealed class SmsgTriggerCinematic : ServerPacket
    {
        public SmsgTriggerCinematic(int cinematicId) : base(WorldOpcodes.SMSG_TRIGGER_CINEMATIC)
        {
            Write((int)cinematicId);
        }
    }
    #endregion

    #region CMSG_NAME_QUERY
    public sealed class CmsgNameQuery : PacketReader
    {
        public uint Guid { get; private set; }

        public CmsgNameQuery(byte[] data) : base(data)
        {
            Guid = ReadUInt32();
        }
    }
    #endregion

    #region SMSG_NAME_QUERY_RESPONSE
    public sealed class SmsgNameQueryResponse : ServerPacket
    {
        public SmsgNameQueryResponse(Character character) : base(WorldOpcodes.SMSG_NAME_QUERY_RESPONSE)
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
    public sealed class SmsgLogoutResponse : ServerPacket
    {
        public SmsgLogoutResponse() : base(WorldOpcodes.SMSG_LOGOUT_RESPONSE)
        {
            Write((UInt32)0);
            Write((byte)0);
        }
    }
    #endregion

    #region SMSG_LOGOUT_CANCEL_ACK
    sealed class SmsgLogoutCancelAck : ServerPacket
    {
        public SmsgLogoutCancelAck() : base(WorldOpcodes.SMSG_LOGOUT_CANCEL_ACK)
        {
            Write((byte)0);
        }
    }
    #endregion

    #region SMSG_LOGOUT_COMPLETE
    sealed class SmsgLogoutComplete : ServerPacket
    {
        public SmsgLogoutComplete() : base(WorldOpcodes.SMSG_LOGOUT_COMPLETE)
        {
            Write((byte)0);
        }
    }
    #endregion

}
