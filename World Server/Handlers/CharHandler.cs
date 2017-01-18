using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Framework.Contants;
using Framework.Contants.Character;
using Framework.Database;
using Framework.Database.Tables;
using Framework.Database.XML;
using Framework.Network;
using World_Server.Sessions;
using static World_Server.Main;

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
                var skin = Main.Database.GetSkin(character);
                var inventory = Main.Database.GetInventory(character);

                Write((ulong) character.Id);

                WriteCString(character.Name);
                Write((byte) character.Race);
                Write((byte) character.Class);
                Write((byte) character.Gender);

                Write(skin.Skin);
                Write(skin.Face);
                Write(skin.HairStyle);
                Write(skin.HairColor);
                Write(skin.Accessory); // facial hair

                Write(character.Level);
                Write(character.MapZone);
                Write(character.MapID);

                Write(character.MapX);
                Write(character.MapY);
                Write(character.MapZ);

                Write(0); // Guild ID
                Write(0); // Flags [1024] PLAYER_FLAGS_HIDE_HELM / [2048] CHARACTER_FLAG_HIDE_CLOAK / [8192] CHARACTER_FLAG_GHOST / [16384] CHARACTER_FLAG_RENAME
                Write((byte) (character.firsttime ? 1 : 0)); //FirstLogin 

                Write(0); // Pet DisplayID
                Write(0); // Pet Level
                Write(0); // Pet FamilyID

                for (int i = 0; i < 19; i++)
                {
                    CharactersInventory abab = inventory.FirstOrDefault(b => b.Slot == i);

                    if (abab != null)
                    {
                        itemsItem itm = XmlManager.GetItem((uint)abab.Item);
                        Write((int)itm.DisplayId);
                        Write(itm.InventoryType);
                    }
                    else
                    {
                        Write(0);
                        Write((byte)0);
                    }
                }

                Write(0); // first bag display id
                Write((byte)0); // first bag inventory type
            }
        }

        public byte[] Packet => (BaseStream as MemoryStream)?.ToArray();
    }
    #endregion

    #region CMSG_SET_SELECTION
    public class CmsgSetSelection : PacketReader
    {
        public UInt64 Guid { get; private set; }

        public CmsgSetSelection(byte[] data) : base(data)
        {
            Guid = ReadUInt64();
        }
    }
    #endregion

    #region CMSG_SET_ACTION_BUTTON
    public sealed class CmsgSetActionButton : PacketReader
    {
        public byte Button { get; private set; }
        public uint Action { get; private set; }
        public ActionButtonType Type { get; private set; }

        public CmsgSetActionButton(byte[] data) : base(data)
        {
            Button = ReadByte();
            var packedData = ReadUInt32();
            Action = packedData & 0x00FFFFFF;
            Type = (ActionButtonType)((packedData & 0xFF000000) >> 24);
        }
    }
    #endregion

    public class CharHandler
    {
        internal static void OnCharDelete(WorldSession session, CmsgCharDelete handler)
        {
            // if failed                CHAR_DELETE_FAILED
            // if waiting for transfer  CHAR_DELETE_FAILED_LOCKED_FOR_TRANSFER
            // if guild leader          CHAR_DELETE_FAILED_GUILD_LEADER
            Main.Database.DeleteCharacter(handler.Id);
            session.SendPacket(new SmsgCharDelete(LoginErrorCode.CHAR_DELETE_SUCCESS));
        }

        internal static void OnCharCreate(WorldSession session, CmsgCharCreate handler)
        {
            // Precisa fazer o chekin de faccção

            try
            {
                Main.Database.CreateChar(handler, session.Users);
                session.SendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_SUCCESS)); return;
            }
            catch (Exception ex)
            {
                // Name in Use
                if (ex.InnerException != null && ex.InnerException.Message.Contains("Duplicate entry"))
                {
                    session.SendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_NAME_IN_USE));
                    return;
                }

                // Failed another Error
                session.SendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_FAILED));
            }

            session.SendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_ERROR));
        }

        internal static void OnCharEnum(WorldSession session, byte[] data)
        {
            List<Character> characters = Main.Database.GetCharacters(session.Users.username);
            session.SendPacket(WorldOpcodes.SMSG_CHAR_ENUM, new SmsgCharEnum(characters).PacketData);
        }

        internal static void OnNameQuery(WorldSession session, CmsgNameQuery handler)
        {
            Character target = Main.Database.GetCharacter((int)handler.Guid);

            if (target != null)
                session.SendPacket(new SmsgNameQueryResponse(target));
        }

        internal static void OnSetSelectionPacket(WorldSession session, CmsgSetSelection handler)
        {
            if (handler.Guid == 0)
            {
                session.Target = null;
                return;
            }

            session.Target = WorldServer.Sessions.FirstOrDefault(s => s.Character.Id == (int)handler.Guid)?.Character;
            ChatHandler.SendSytemMessage(session, $"Targeted: {session.Target?.Name}");
        }

        internal static void OnSetActionButton(WorldSession session, CmsgSetActionButton handler)
        {
            if (handler.Action == 0)
                Database.RemoveActionBar(handler, session.Character);
            else
                Database.AddActionBar(handler, session.Character);
        }
    }
}
