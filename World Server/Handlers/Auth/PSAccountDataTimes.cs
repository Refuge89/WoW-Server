﻿using Framework.Contants;
using Framework.Extensions;
using Framework.Network;
using System;

namespace World_Server.Handlers.Auth
{
    class PSAccountDataTimes : ServerPacket
    {
        public PSAccountDataTimes() : base(WorldOpcodes.SMSG_ACCOUNT_DATA_TIMES)
        {
            this.WriteNullByte(128);
        }
    }

    class PSSetRestStart : ServerPacket
    {
        //TODO Implement
        public PSSetRestStart() : base(WorldOpcodes.SMSG_SET_REST_START)
        {
            Write((byte)0);
        }
    }

    class PSBindPointUpdate : ServerPacket
    {
        public PSBindPointUpdate() : base(WorldOpcodes.SMSG_BINDPOINTUPDATE)
        {
            Write((float)-8949.95f); //X
            Write((float)-132.493f); //Y
            Write((float)-83.5312f); //Z
            Write((uint)0); //MAPID
            Write((short)12); //AREAID
        }
    }

    class PSTutorialFlags : ServerPacket
    {
        //TODO Write the uint ids of 8 tutorial values
        public PSTutorialFlags() : base(WorldOpcodes.SMSG_TUTORIAL_FLAGS)
        {
            this.WriteNullUInt(8);
        }
    }

    class PSInitialSpells : ServerPacket
    {

        public PSInitialSpells() : base(WorldOpcodes.SMSG_INITIAL_SPELLS)
        {
            Write((byte)0);
            Write((short)0);
            Write((short)0);
        }
    }

    class PSActionButtons : ServerPacket
    {
        public PSActionButtons() : base(WorldOpcodes.SMSG_ACTION_BUTTONS)
        {
            for (int button = 0; button < 120; button++)
            {
                Write((uint)0);
            }
        }
    }

    public class PSLoginSetTimeSpeed : ServerPacket
    {
        public PSLoginSetTimeSpeed() : base(WorldOpcodes.SMSG_LOGIN_SETTIMESPEED)
        {
            Write((uint)secsToTimeBitFields(DateTime.Now)); // Time
            Write((float)0.01666667f); // Speed
        }

        public static int secsToTimeBitFields(DateTime dateTime)
        {
            return (dateTime.Year - 100) << 24 | dateTime.Month << 20 | (dateTime.Day - 1) << 14 | (int)dateTime.DayOfWeek << 11 | dateTime.Hour << 6 | dateTime.Minute;
        }
    }

    class PSFriendList : ServerPacket
    {

        public PSFriendList() : base(WorldOpcodes.SMSG_FRIEND_LIST)
        {
            Write((byte)0);
        }
    }

    class PSIgnoreList : ServerPacket
    {

        public PSIgnoreList() : base(WorldOpcodes.SMSG_IGNORE_LIST)
        {
            Write((byte)0);
        }
    }

    public class PSInitWorldStates : ServerPacket
    {
        //TODO Implement this, found at https://github.com/mangoszero/server/blob/master/src/game/Player.cpp#L7676
        public PSInitWorldStates() : base(WorldOpcodes.SMSG_INIT_WORLD_STATES)
        {
            /*
            packet.Append(player.MapID);
            packet.Append(player.ZoneID);
            packet.Append((ushort)0);
            */
            this.WriteHexPacket("01 00 00 00 6C 00 AE 07 01 00 32 05 01 00 31 05 00 00 2E 05 00 00 F9 06 00 00 F3 06 00 00 F1 06 00 00 EE 06 00 00 ED 06 00 00 71 05 00 00 70 05 00 00 67 05 01 00 66 05 01 00 50 05 01 00 44 05 00 00 36 05 00 00 35 05 01 00 C6 03 00 00 C4 03 00 00 C2 03 00 00 A8 07 00 00 A3 07 0F 27 74 05 00 00 73 05 00 00 72 05 00 00 6F 05 00 00 6E 05 00 00 6D 05 00 00 6C 05 00 00 6B 05 00 00 6A 05 01 00 69 05 01 00 68 05 01 00 65 05 00 00 64 05 00 00 63 05 00 00 62 05 00 00 61 05 00 00 60 05 00 00 5F 05 00 00 5E 05 00 00 5D 05 00 00 5C 05 00 00 5B 05 00 00 5A 05 00 00 59 05 00 00 58 05 00 00 57 05 00 00 56 05 00 00 55 05 00 00 54 05 01 00 53 05 01 00 52 05 01 00 51 05 01 00 4F 05 00 00 4E 05 00 00 4D 05 01 00 4C 05 00 00 4B 05 00 00 45 05 00 00 43 05 01 00 42 05 00 00 40 05 00 00 3F 05 00 00 3E 05 00 00 3D 05 00 00 3C 05 00 00 3B 05 00 00 3A 05 01 00 39 05 00 00 38 05 00 00 37 05 00 00 34 05 00 00 33 05 00 00 30 05 00 00 2F 05 00 00 2D 05 01 00 16 05 01 00 15 05 00 00 B6 03 00 00 45 07 02 00 36 07 01 00 35 07 01 00 34 07 01 00 33 07 01 00 32 07 01 00 02 07 00 00 01 07 00 00 00 07 00 00 FE 06 00 00 FD 06 00 00 FC 06 00 00 FB 06 00 00 F8 06 00 00 F7 06 00 00 F6 06 00 00 F4 06 D0 07 F2 06 00 00 F0 06 00 00 EF 06 00 00 EC 06 00 00 EA 06 00 00 E9 06 00 00 E8 06 00 00 E7 06 00 00 18 05 00 00 17 05 00 00 03 07 00 00");
        }
    }

    public class PSTriggerCinematic : ServerPacket
    {
        public PSTriggerCinematic() : base(WorldOpcodes.SMSG_TRIGGER_CINEMATIC)
        {
            Write(BitConverter.GetBytes(0x00000051));
        }
    }
   
}
