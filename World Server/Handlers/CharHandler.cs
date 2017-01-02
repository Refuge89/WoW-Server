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
        None,
        Head,
        Neck,
        Shoulders,
        Shirt,
        Vest,
        Waist,
        Legs,
        Feet,
        Wrist,
        Hands,
        Ring,
        Trinket,
        Onehand,
        Shield,
        Bow,
        Back,
        Twohand,
        Bag,
        Tabbard,
        Robe,
        Mainhand,
        Offhand,
        Held,
        Ammo,
        Thrown,
        Ranged,
        Ranged2,
        Relic
    }

    #region SMSG_CHAR_ENUM
    public sealed class SmsgCharEnum : PacketWriter
    {
        public SmsgCharEnum(List<Character> characters) : base(PacketHeaderType.WorldSmsg)
        {
            Write((byte)characters.Count);

            foreach (Character character in characters)
            {
                var skin = Program.Database.GetSkin(character);

                Write((ulong) character.Id); // Int64
                WriteCString(character.Name);

                Write((byte) character.Race); // Int8
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

                

                WorldItems[] equipment = GenerateInventoryByIDs(character.Equipment);

                for (int i = 0; i < 19; i++)
                {
                    if (equipment != null && equipment[i] != null)
                    {
                        Write(equipment[i].displayId); // Item DisplayID
                        Write((byte)equipment[i].InventoryType); // Item Inventory Type
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

        public static WorldItems[] GenerateInventoryByIDs(string ids)
        {
            if (ids == null) return null;

            string[] ItemList = ids.Split(",");

            WorldItems[] inventory = new WorldItems[19];
            for (int i = 0; i < ItemList.Length; i++)
            {
                if (ItemList[i].Length > 0)
                {
                    var itemEntry = ItemList[i];
                    WorldItems item = Program.Database.GetItem(Int32.Parse(itemEntry));
                    if (item != null)
                    {
                        Console.WriteLine(item.name);
                        switch ((WoWEquipSlot) item.InventoryType)
                        {
                            case WoWEquipSlot.Head:
                                inventory[0] = item;
                                break;
                            case WoWEquipSlot.Shirt:
                                inventory[3] = item;
                                break;
                            case WoWEquipSlot.Vest:
                            case WoWEquipSlot.Robe:
                                inventory[4] = item;
                                break;
                            case WoWEquipSlot.Waist:
                                inventory[5] = item;
                                break;
                            case WoWEquipSlot.Legs:
                                inventory[6] = item;
                                break;
                            case WoWEquipSlot.Feet:
                                inventory[7] = item;
                                break;
                            case WoWEquipSlot.Wrist:
                                inventory[8] = item;
                                break;
                            case WoWEquipSlot.Hands:
                                inventory[9] = item;
                                break;
                            case WoWEquipSlot.Ring:
                                inventory[10] = item;
                                break;
                            case WoWEquipSlot.Trinket:
                                inventory[12] = item;
                                break;
                            case WoWEquipSlot.Mainhand:
                            case WoWEquipSlot.Onehand:
                            case WoWEquipSlot.Twohand:
                                inventory[15] = item;
                                break;
                            case WoWEquipSlot.Offhand:
                            case WoWEquipSlot.Shield:
                            case WoWEquipSlot.Bow:
                                inventory[16] = item;
                                break;

                        }
                    }
                }
            }
            return inventory;
        }

        public byte[] Packet => (BaseStream as MemoryStream)?.ToArray();
    }
    #endregion

}
