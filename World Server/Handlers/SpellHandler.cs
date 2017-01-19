using System;
using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;
using World_Server.Game.World;
using World_Server.Game.World.Components;
using World_Server.Managers;
using World_Server.Sessions;
using static World_Server.Program;

namespace World_Server.Handlers
{

    #region SMSG_INITIAL_SPELLS
    sealed class SmsgInitialSpells : ServerPacket
    {
        public SmsgInitialSpells(Character character) : base(WorldOpcodes.SMSG_INITIAL_SPELLS)
        {
            var spells = Main.Database.GetSpells(character);

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
        public SmsgSpellGo(WorldSession session, Character target, uint spellId) : base(WorldOpcodes.SMSG_SPELL_GO)
        {
            session.SendMessage($"Spell GO {spellId}");
            try
            {
                SpellComponent cast = new SpellComponent(session)
                {
                    Targets = target,
                    Spell = DatabaseManager.Spell[spellId],
                    Triggered = false
                };

                session.PrepareSpell(cast);
            }
            catch (Exception)
            {
                session.SendMessage($"Spell ainda não implementada: {spellId}");
            }
        }
    }
    #endregion

    class SpellHandler
    {
        internal static void HandleCastSpellOpcode(WorldSession session, CmsgCastSpell handler)
        {
            Character target = session.Target ?? session.Character;

            Main.WorldServer.TransmitToAll(new SmsgSpellGo(session, target, handler.SpellId));
            session.SendPacket(new SmsgCastFailed(handler.SpellId));
        }

        internal static void HandleCancelCastOpcode(WorldSession session, CmsgCancelCast handler)
        {
        }
    }
}
