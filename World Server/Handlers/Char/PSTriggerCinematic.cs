using Framework.Contants;
using Framework.Network;
using System;

namespace World_Server.Handlers.Char
{
    public class PSTriggerCinematic : ServerPacket
    {
        public PSTriggerCinematic(int CinematicId) : base(WorldOpcodes.SMSG_TRIGGER_CINEMATIC)
        {
            //Write(BitConverter.GetBytes(0x00000051));
            // 1        vai ra perto d an qiraj
            // 2        Undead
            // 21       ORC
            // 41       Dwarf
            // 61       Night Elf
            // 81       Human
            // 101      Gnome
            // 121      Troll
            // 141      Tauren
            // 161      NAO SEI
            Write((int)CinematicId);
        }
    }
}
