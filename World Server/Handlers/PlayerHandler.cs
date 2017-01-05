using Framework.Contants;
using Framework.Database.Tables;
using Framework.Extensions;
using Framework.Network;
using System;
using System.Collections.Generic;
using System.IO;
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

    #region SMSG_PLAY_SOUND
    internal class SmsgPlaySound : ServerPacket
    {
        public SmsgPlaySound(uint soundID) : base(WorldOpcodes.SMSG_PLAY_SOUND)
        {
            Write(soundID);
        }
    }
    #endregion

    #region SMSG_ACTION_BUTTONS
    class SmsgActionButtons : ServerPacket
    {
        public SmsgActionButtons(Character character) : base(WorldOpcodes.SMSG_ACTION_BUTTONS)
        {
            List<CharactersActionBar> savedButtons = Program.Database.GetActionBar(character);

            for (int button = 0; button < 120; button++)
            {
                int index = savedButtons.FindIndex(b => b.Button == button);

                CharactersActionBar currentButton = index != -1 ? savedButtons[index] : null;

                if (currentButton != null)
                {
                    UInt32 packedData = (UInt32)currentButton.Action | (UInt32)currentButton.Type << 24;
                    Write((UInt32)packedData);
                }
                else
                {
                    Write((UInt32)0);
                }
            }
        }
    }
    #endregion

    public class Weather : BinaryWriter
    {
        public Weather(WeatherState state, float grad, WeatherSounds sound) : base(new MemoryStream())
        {
            Write((uint)state);
            Write(grad);
            Write((uint)sound);
        }

        public byte[] Packet { get { return (BaseStream as MemoryStream).ToArray(); } }
    }

    public enum WeatherState : uint
    {
        WEATHER_STATE_FINE = 0,
        WEATHER_STATE_LIGHT_RAIN = 3,
        WEATHER_STATE_MEDIUM_RAIN = 4,
        WEATHER_STATE_HEAVY_RAIN = 5,
        WEATHER_STATE_LIGHT_SNOW = 6,
        WEATHER_STATE_MEDIUM_SNOW = 7,
        WEATHER_STATE_HEAVY_SNOW = 8,
        WEATHER_STATE_LIGHT_SANDSTORM = 22,
        WEATHER_STATE_MEDIUM_SANDSTORM = 41,
        WEATHER_STATE_HEAVY_SANDSTORM = 42,
        WEATHER_STATE_THUNDERS = 86,
        WEATHER_STATE_BLACKRAIN = 90
    };

    public enum WeatherSounds : uint
    {
        WEATHER_NOSOUND = 0,
        WEATHER_RAINLIGHT = 8533,
        WEATHER_RAINMEDIUM = 8534,
        WEATHER_RAINHEAVY = 8535,
        WEATHER_SNOWLIGHT = 8536,
        WEATHER_SNOWMEDIUM = 8537,
        WEATHER_SNOWHEAVY = 8538,
        WEATHER_SANDSTORMLIGHT = 8556,
        WEATHER_SANDSTORMMEDIUM = 8557,
        WEATHER_SANDSTORMHEAVY = 8558
    };
}
