using System;
using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;

namespace World_Server.Handlers
{
    sealed class SmsgInitialSpells : ServerPacket
    {
        public SmsgInitialSpells(Character character) : base(WorldOpcodes.SMSG_INITIAL_SPELLS)
        {
            var Spells = Program.Database.GetSpells(character);

            Write((byte)0);           
            Write((ushort)(Spells.Count));

            ushort slot = 1;
            foreach (CharactersSpells Spell in Spells)
            {
                Write((ushort)Spell.spell);
                Write(slot++);
            }
  
            Write((UInt16)(Spells.Count));
        }
    }
}
