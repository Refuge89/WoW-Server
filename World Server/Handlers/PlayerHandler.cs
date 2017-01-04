using Framework.Contants;
using Framework.Database.Tables;
using Framework.Extensions;
using Framework.Network;
using System;
using World_Server.Sessions;

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
        public SmsgTriggerCinematic(WorldSession session, int cinematicId) : base(WorldOpcodes.SMSG_TRIGGER_CINEMATIC)
        {
            Program.Database.UpdateCharacter(session.Character.Id, "firstlogin");

            Write(cinematicId);
        }
    }
    #endregion

}
