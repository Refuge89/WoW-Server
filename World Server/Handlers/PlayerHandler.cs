using Framework.Contants;
using Framework.Database.Tables;
using Framework.Extensions;
using Framework.Network;
using System;
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
    sealed class SmsgTutorialFlags : ServerPacket
    {
        //TODO Write the uint ids of 8 tutorial values
        public SmsgTutorialFlags() : base(WorldOpcodes.SMSG_TUTORIAL_FLAGS)
        {
            for (int i = 0; i < 8; i++)
            {
                Write((byte)0xff);
            }
        }
    }
    #endregion

    #region SMSG_LOGIN_SETTIMESPEED
    public sealed class SmsgLoginSettimespeed : ServerPacket
    {
        public SmsgLoginSettimespeed() : base(WorldOpcodes.SMSG_LOGIN_SETTIMESPEED)
        {
            Write((uint)SecsToTimeBitFields(DateTime.Now)); // Time
            Write(0.01666667f); // Speed
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
        public SmsgPlaySound(uint soundId) : base(WorldOpcodes.SMSG_PLAY_SOUND)
        {
            Write(soundId);
        }
    }
    #endregion

    #region SMSG_ACTION_BUTTONS
    class SmsgActionButtons : ServerPacket
    {
        public SmsgActionButtons(Character character) : base(WorldOpcodes.SMSG_ACTION_BUTTONS)
        {
            /*
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
            */
        }
    }
    #endregion

    public sealed class Weather : BinaryWriter
    {
        public Weather(WeatherState state, float grad, WeatherSounds sound) : base(new MemoryStream())
        {
            Write((uint)state);
            Write(grad);
            Write((uint)sound);
        }

        public byte[] Packet => (BaseStream as MemoryStream)?.ToArray();
    }

    public enum WeatherState : uint
    {
        WeatherStateFine = 0,
        WeatherStateLightRain = 3,
        WeatherStateMediumRain = 4,
        WeatherStateHeavyRain = 5,
        WeatherStateLightSnow = 6,
        WeatherStateMediumSnow = 7,
        WeatherStateHeavySnow = 8,
        WeatherStateLightSandstorm = 22,
        WeatherStateMediumSandstorm = 41,
        WeatherStateHeavySandstorm = 42,
        WeatherStateThunders = 86,
        WeatherStateBlackrain = 90
    };

    public enum WeatherSounds : uint
    {
        WeatherNosound = 0,
        WeatherRainlight = 8533,
        WeatherRainmedium = 8534,
        WeatherRainheavy = 8535,
        WeatherSnowlight = 8536,
        WeatherSnowmedium = 8537,
        WeatherSnowheavy = 8538,
        WeatherSandstormlight = 8556,
        WeatherSandstormmedium = 8557,
        WeatherSandstormheavy = 8558
    };
}
