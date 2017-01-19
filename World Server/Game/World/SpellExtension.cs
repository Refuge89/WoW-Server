using Framework.Contants;
using Framework.Network;
using World_Server.Game.World.Components;
using World_Server.Sessions;

namespace World_Server.Game.World
{
    #region MSG_CHANNEL_START
    public sealed class MsgChannelStart : ServerPacket
    {
        public MsgChannelStart(WorldSession channel, int spellId) : base(WorldOpcodes.MSG_CHANNEL_START)
        {
            Write(spellId);
            Write(5 * 1000);
        }

    }
    #endregion

    public static class SpellExtension
    {
        public static void PrepareSpell(this WorldSession u, SpellComponent spell)
        {
            spell.Initialize();
            spell.SendSpellStart();
            u.SendPacket(new MsgChannelStart(u, (int)spell.Spell.Id));
        }
    }
}
