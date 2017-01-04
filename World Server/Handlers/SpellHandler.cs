using System;
using Framework.Contants;
using Framework.Database;
using Framework.Database.Tables;
using Framework.Database.Xml;
using Framework.Network;

namespace World_Server.Handlers
{
    sealed class SmsgInitialSpells : ServerPacket
    {
        public SmsgInitialSpells(Character character) : base(WorldOpcodes.SMSG_INITIAL_SPELLS)
        {
            var raceSpells = XmlManager.GetRaceStats(character.Race);
            var clasSpells = XmlManager.GetClassStats(character.Class);

            Write((byte)0);           
            Write((ushort)(raceSpells.spells.Length + clasSpells.spells.Length));

            ushort slot = 1;
            foreach (raceSpell spellid in raceSpells.spells)
            {
                Write((ushort)spellid.id);
                Write(slot++);
            }

            foreach (classeSpell spellid in clasSpells.spells)
            {
                Write((ushort)spellid.id);
                Write(slot++);
            }

            Write((UInt16)(raceSpells.spells.Length + clasSpells.spells.Length));

        }
    }
}
