using System;
using Framework.Contants;
using Framework.Network;
using World_Server.Game.World.Components;
using World_Server.Sessions;

namespace World_Server.Game.World
{
    public sealed class MSG_CHANNEL_START : ServerPacket
    {
        public MSG_CHANNEL_START(WorldSession channel, int spellId) : base(WorldOpcodes.MSG_CHANNEL_START)
        {
            Console.WriteLine("MSG_CHANNEL_START");
            Write(spellId);
            Write(5 * 1000);
        }

    }
    public static class SpellExtension
    {
        public static void PrepareSpell(this WorldSession u, SpellComponent spell)
        {
            spell.Initialize();

            spell.SendSpellStart();

            Console.WriteLine($"PrepareSpell: {spell.Spell.Id}");
            Console.WriteLine(spell.Spell.Id);

            u.SendPacket(new MSG_CHANNEL_START(u, (int)spell.Spell.Id));
        }
    }
}
