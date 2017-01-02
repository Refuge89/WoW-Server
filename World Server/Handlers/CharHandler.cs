using System;
using System.Collections.Generic;
using System.IO;
using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;
using Platform;

namespace World_Server.Handlers
{

    #region  CMSG_CHAR_CREATE
    public sealed class CmsgCharCreate : PacketReader
    {
        public string Name { get; private set; }
        public byte Race { get; private set; }
        public byte Class { get; private set; }

        public byte Gender { get; private set; }
        public byte Skin { get; private set; }
        public byte Face { get; private set; }
        public byte HairStyle { get; private set; }
        public byte HairColor { get; private set; }
        public byte Accessory { get; private set; }

        public CmsgCharCreate(byte[] data) : base(data)
        {
            Name = ReadCString();
            Race = ReadByte();
            Class = ReadByte();

            Gender = ReadByte();
            Skin = ReadByte();
            Face = ReadByte();
            HairStyle = ReadByte();
            HairColor = ReadByte();
            Accessory = ReadByte();
        }
    }
    #endregion

    #region CMSG_CHAR_DELETE
    class CmsgCharDelete : PacketReader
    {
        public int Id { get; private set; }

        public CmsgCharDelete(byte[] data) : base(data)
        {
            Id = (int)ReadUInt64();
        }
    }
    #endregion

    #region SMSG_CHAR_CREATE
    sealed class SmsgCharCreate : ServerPacket
    {
        public SmsgCharCreate(LoginErrorCode code) : base(WorldOpcodes.SMSG_CHAR_CREATE)
        {
            Write((byte)code);
        }
    }
    #endregion

    #region SMSG_CHAR_DELETE
    sealed class SmsgCharDelete : ServerPacket
    {
        public SmsgCharDelete(LoginErrorCode code) : base(WorldOpcodes.SMSG_CHAR_DELETE)
        {
            Write((byte)code);
        }
    }
    #endregion

    public enum WoWEquipSlot : byte
    {
        //None = -1,
        Head = 0,
        Neck,
        Shoulders,
        Body,
        Chest,
        Waist,
        Legs,
        Feet,
        Wrists,
        Hands,
        Finger1,
        Finger2,
        Trinket1,
        Trinket2,
        Back,
        MainHand,
        OffHand,
        Ranged,
        Tabard,
        Bag1,
        Bag2,
        Bag3,
        Bag4,

        Start = Head,
        End = Bag4,
    }



    #region SMSG_CHAR_ENUM
    public sealed class SmsgCharEnum : PacketWriter
    {
        public SmsgCharEnum(List<Character> characters) : base(PacketHeaderType.WorldSmsg)
        {
            Write((byte)characters.Count);

            foreach (Character character in characters)
            {
                var skin = Program.Database.GetSking(character);

                Write((ulong)character.Id);         // Int64
                WriteCString(character.Name);

                Write((byte)character.Race);        // Int8
                Write((byte)character.Class);
                Write((byte)character.Gender);

                Write((byte)skin.Skin);
                Write((byte)skin.Face);
                Write((byte)skin.HairStyle);
                Write((byte)skin.HairColor);
                Write((byte)skin.Accessory);

                Write((byte)character.Level);

                Write(character.MapZone);
                Write(character.MapID);
                Write(character.MapX);
                Write(character.MapY);
                Write(character.MapZ);

                Write((int)0);          // Guild ID
                Write((int)0);          // Character Flags
                Write((byte)(character.firsttime ? 1 : 0));        //FirstLogin 

                Write(0);               // Pet DisplayID
                Write(0);               // Pet Level
                Write(0);               // Pet FamilyID

                string[] equipamento = character.Equipment.Split(",");
                for (int i = (int) WoWEquipSlot.Head; i < (int) WoWEquipSlot.Tabard + 1; i++)
                {
                    //if (equipamento?[i] != null)
                    //{
                        //Write((int)0);
                        //Write((byte)0);
                    //}
                    //else
                    //{
                        Write((int)0);
                        Write((byte)0);
                    //}
                }

                Write((int)0); // first bag display id
                Write((byte)0); // first bag inventory type
            }
        }

        public byte[] Packet => (BaseStream as MemoryStream)?.ToArray();
    }
    #endregion

}
