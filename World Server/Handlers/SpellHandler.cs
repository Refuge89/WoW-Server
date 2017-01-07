using System;
using Framework.Contants;
using Framework.Database.Tables;
using Framework.Extensions;
using Framework.Network;
using World_Server.Sessions;
using static World_Server.Program;

namespace World_Server.Handlers
{

    #region SMSG_INITIAL_SPELLS
    sealed class SmsgInitialSpells : ServerPacket
    {
        public SmsgInitialSpells(Character character) : base(WorldOpcodes.SMSG_INITIAL_SPELLS)
        {
            var spells = Database.GetSpells(character);

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
            Write(spellId);
        }
    }
    #endregion

    #region SMSG_LEARNED_SPELL
    sealed class SmsgLearnedSpell : ServerPacket
    {
        public SmsgLearnedSpell(uint spellId) : base(WorldOpcodes.SMSG_LEARNED_SPELL)
        {
            Write(spellId);
            Write((ushort)0);
        }
    }
    #endregion 

    #region SMSG_SPELL_GO
    public sealed class SmsgSpellGo : ServerPacket
    {
        public SmsgSpellGo(Character caster, Character target, uint spellId) : base(WorldOpcodes.SMSG_SPELL_GO)
        {
            this.WritePackedUInt64((ulong)caster.Id);
            this.WritePackedUInt64((ulong)target.Id);

            Write(spellId);
            Write((ushort)SpellCastFlags.CastFlagUnknown9); // Cast Flags!?
            Write((byte)1); // Target Length
            Write(target.Id);
            Write((byte)0); // End
            Write((ushort)2); // TARGET_FLAG_UNIT
            this.WritePackedUInt64((ulong)target.Id);
        }
    }
    #endregion

    enum SpellCastFlags
    {
        CastFlagNone              = 0x00000000,
        CastFlagHiddenCombatlog  = 0x00000001, // hide in combat log?
        CastFlagUnknown2          = 0x00000002,
        CastFlagUnknown3          = 0x00000004,
        CastFlagUnknown4          = 0x00000008,
        CastFlagUnknown5          = 0x00000010,
        CastFlagAmmo              = 0x00000020, // Projectiles visual
        CastFlagUnknown7          = 0x00000040, // !0x41 mask used to call CGTradeSkillInfo::DoRecast
        CastFlagUnknown8          = 0x00000080,
        CastFlagUnknown9          = 0x00000100
    }

    class SpellHandler
    {
        internal static void HandleCastSpellOpcode(WorldSession session, CmsgCastSpell handler)
        {
            Character target = session.Target ?? session.Character;

            WorldServer.TransmitToAll(new SmsgSpellGo(session.Character, target, handler.SpellId));
            session.SendPacket(new SmsgCastFailed(handler.SpellId));
        }

        internal static void HandleCancelCastOpcode(WorldSession session, CmsgCancelCast handler)
        {
        }
    }
}
