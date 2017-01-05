using System;
using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;
using World_Server.Game.Entitys;
using World_Server.Handlers.World;
using World_Server.Sessions;

namespace World_Server.Handlers
{

    #region SMSG_INITIAL_SPELLS
    sealed class SmsgInitialSpells : ServerPacket
    {
        public SmsgInitialSpells(Character character) : base(WorldOpcodes.SMSG_INITIAL_SPELLS)
        {
            var spells = Program.Database.GetSpells(character);

            Write((byte)0);           
            Write((ushort)(spells.Count));

            ushort slot = 1;
            foreach (CharactersSpells spell in spells)
            {
                Write((ushort)spell.spell);
                Write(slot++);
            }
  
            Write((UInt16)(spells.Count));
        }
    }
    #endregion

    #region CMSG_CAST_SPELL
    sealed class CmsgCastSpell : PacketReader
    {
        public uint SpellId { get; private set; }
        public string Target { get; private set; }

        public CmsgCastSpell(byte[] data) : base(data)
        {
            SpellId = ReadUInt32();
            Target = ReadCString();
        }
    }
    #endregion

    #region CMSG_CANCEL_CAST
    sealed class CmsgCancelCast : PacketReader
    {
        public uint SpellId { get; private set; }

        public CmsgCancelCast(byte[] data) : base(data)
        {
            SpellId = ReadUInt32();
        }
    }
    #endregion

    #region SMSG_CAST_FAILED
    public sealed class SmsgCastFailed : ServerPacket
    {
        public SmsgCastFailed(uint spellId) : base(WorldOpcodes.SMSG_CAST_FAILED)
        {
            Write((uint)spellId);
        }
    }
    #endregion

    #region SMSG_SPELL_GO
    public sealed class SmsgSpellGo : ServerPacket
    {
        public SmsgSpellGo(PlayerEntity caster, PlayerEntity target, uint spellId) : base(WorldOpcodes.SMSG_SPELL_GO)
        {

        }
    }
    #endregion 

    class SpellHandler
    {
        internal static void HandleCastSpellOpcode(WorldSession session, CmsgCastSpell handler)
        {
        }

        internal static void HandleCancelCastOpcode(WorldSession session, CmsgCancelCast handler)
        {
        }
    }
}
