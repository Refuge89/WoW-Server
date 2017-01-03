using System;
using System.Linq;
using Framework.Contants;
using Framework.Contants.Character;
using Framework.Database;
using Framework.Database.Tables;
using Framework.Database.Xml;
using Framework.Network;

namespace World_Server.Handlers
{
    sealed class SmsgInitialSpells : ServerPacket
    {
        private Character character;

        public SmsgInitialSpells(Character character) : base(WorldOpcodes.SMSG_INITIAL_SPELLS)
        {
            var RaceSpells = XmlManager.GetRaceStats(character.Race);
            var ClasSpells = XmlManager.GetClassStats(character.Class);

            Write((byte)0);           
            Write((ushort)(RaceSpells.spells.Length + ClasSpells.spells.Length));

            ushort slot = 1;
            foreach (raceSpell spellid in RaceSpells.spells)
            {
                Write((ushort)spellid.id);
                Write(slot++);
            }

            foreach (classeSpell spellid in ClasSpells.spells)
            {
                Write((ushort)spellid.id);
                Write(slot++);
            }

            Write((UInt16)(RaceSpells.spells.Length + ClasSpells.spells.Length));

        }
    }
}
