using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;
using System.Collections.Generic;
using System.IO;

namespace World_Server.Handlers.Char
{
    public class PSCharEnum : PacketWriter
    {
        public PSCharEnum(List<Character> characters) : base(PacketHeaderType.WorldSmsg)
        {
            Write((byte)characters.Count);
            
            foreach(Character character in characters)
            {
                var Skin = Program.Database.GetSking(character);

                Write((ulong)character.Id);
                WriteCString(character.Name);
                Write((byte)character.Race);
                Write((byte)character.Class);

                Write((byte)character.Gender);
                
                Write((byte)Skin.Skin);
                Write((byte)Skin.Face);
                Write((byte)Skin.HairStyle);
                Write((byte)Skin.HairColor);
                Write((byte)Skin.Accessory);
                    
                Write((byte)character.Level);

                Write(character.MapZone); // Zone ID
                Write(character.MapID);
                Write(character.MapX);
                Write(character.MapY);
                Write(character.MapZ);

                Write((int)0); // Guild ID
                Write((int)0); // Character Flags

                Write((byte)10); // // Login Flags?

                Write(10); // Pet DisplayID
                Write(10); // Pet Level
                Write(10); // Pet FamilyID

                for (int itemSlot = 0; itemSlot < 19; itemSlot++)
                {
                    Write((int)0);
                    Write((byte)0);
                }

                Write((int)0); // first bag display id
                Write((byte)0); // first bag inventory type
            }
        }

        public byte[] Packet { get { return (BaseStream as MemoryStream).ToArray(); } }
    }


}
