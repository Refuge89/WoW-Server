﻿using System;
using System.Collections.Generic;
using System.IO;
using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;
using World_Server.Helpers;
using World_Server.Sessions;

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

    public class CmsgCharDelete : PacketReader
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

    #region SMSG_CHAR_ENUM
    public sealed class SmsgCharEnum : PacketWriter
    {
        public SmsgCharEnum(List<Character> characters) : base(PacketHeaderType.WorldSmsg)
        {
            Write((byte)characters.Count);

            foreach (Character character in characters)
            {
                var skin = Program.Database.GetSkin(character);

                Write((ulong) character.Id);
                WriteCString(character.Name);

                Write((byte) character.Race);
                Write((byte) character.Class);
                Write((byte) character.Gender);

                Write((byte) skin.Skin);
                Write((byte) skin.Face);
                Write((byte) skin.HairStyle);
                Write((byte) skin.HairColor);
                Write((byte) skin.Accessory);

                Write((byte) character.Level);

                Write(character.MapZone);
                Write(character.MapID);
                Write(character.MapX);
                Write(character.MapY);
                Write(character.MapZ);

                Write((int) 0); // Guild ID
                Write((int) 0); // Character Flags
                Write((byte) (character.firsttime ? 1 : 0)); //FirstLogin 

                Write(0); // Pet DisplayID
                Write(0); // Pet Level
                Write(0); // Pet FamilyID

                WorldItems[] equipment = InventoryHelper.GenerateInventoryByIDs(character.Equipment);

                for (int i = 0; i < 19; i++)
                {
                    if (equipment?[i] != null)
                    {
                        Write(equipment[i].displayId);
                        Write((byte)equipment[i].InventoryType);
                    }
                    else
                    {
                        Write((int)0);
                        Write((byte)0);
                    }
                }

                Write((int)0); // first bag display id
                Write((byte)0); // first bag inventory type
            }
        }

        public byte[] Packet => (BaseStream as MemoryStream)?.ToArray();
    }
    #endregion

    public class CharHandler
    {
        internal static void OnCharDelete(WorldSession session, CmsgCharDelete handler)
        {
            // if failed                CHAR_DELETE_FAILED
            // if waiting for transfer  CHAR_DELETE_FAILED_LOCKED_FOR_TRANSFER
            // if guild leader          CHAR_DELETE_FAILED_GUILD_LEADER
            Program.Database.DeleteCharacter(handler.Id);
            session.sendPacket(new SmsgCharDelete(LoginErrorCode.CHAR_DELETE_SUCCESS));
        }

        internal static void OnCharCreate(WorldSession session, CmsgCharCreate handler)
        {
            // Precisa fazer o chekin de faccção

            try
            {
                Program.Database.CreateChar(handler, session.Users);
                session.sendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_SUCCESS)); return;
            }
            catch (Exception ex)
            {
                // Name in Use
                if (ex.InnerException != null && ex.InnerException.Message.Contains("Duplicate entry"))
                {
                    session.sendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_NAME_IN_USE));
                    return;
                }

                // Failed another Error
                session.sendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_FAILED));
            }

            session.sendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_ERROR)); return;
        }

        internal static void OnCharEnum(WorldSession session, byte[] data)
        {
            List<Character> characters = Program.Database.GetCharacters(session.Users.username);
            session.sendPacket(WorldOpcodes.SMSG_CHAR_ENUM, new SmsgCharEnum(characters).PacketData);
        }

        internal static void OnNameQuery(WorldSession session, CmsgNameQuery handler)
        {
            Character target = Program.Database.GetCharacter((int)handler.Guid);

            if (target != null)
                session.sendPacket(new SmsgNameQueryResponse(target));
        }
    }
}
