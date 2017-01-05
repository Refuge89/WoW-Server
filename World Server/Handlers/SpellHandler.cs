using System;
using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;
using World_Server.Game.Entitys;
using World_Server.Sessions;
using World_Server.Handlers.World;
using static World_Server.Program;

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
            Write(spellId);
        }
    }
    #endregion

    #region SMSG_LEARNED_SPELL
    class SmsgLearnedSpell : ServerPacket
    {
        public SmsgLearnedSpell(uint spellID) : base(WorldOpcodes.SMSG_LEARNED_SPELL)
        {
            Write((uint)spellID);
            Write((UInt16)0);
        }
    }
    #endregion 

    #region SMSG_SPELL_GO
    public sealed class SmsgSpellGo : ServerPacket
    {
        public SmsgSpellGo(Character caster, Character target, uint spellId) : base(WorldOpcodes.SMSG_SPELL_GO)
        {
            byte[] casterGUID = PSUpdateObject.GenerateGuidBytes((ulong)caster.Id);
            byte[] targetGUID = PSUpdateObject.GenerateGuidBytes((ulong)target.Id);

            PSUpdateObject.WriteBytes(this, casterGUID);
            PSUpdateObject.WriteBytes(this, casterGUID);

            Write((UInt32)spellId);
            Write((UInt16)SpellCastFlags.CAST_FLAG_UNKNOWN9); // Cast Flags!?
            Write((Byte)1); // Target Length
            Write((UInt64)target.Id);
            Write((Byte)0); // End
            Write((UInt16)2); // TARGET_FLAG_UNIT
            PSUpdateObject.WriteBytes(this, targetGUID); // Packed GUID
        }
    }
    #endregion

    enum SpellCastFlags
    {
        CAST_FLAG_NONE              = 0x00000000,
        CAST_FLAG_HIDDEN_COMBATLOG  = 0x00000001, // hide in combat log?
        CAST_FLAG_UNKNOWN2          = 0x00000002,
        CAST_FLAG_UNKNOWN3          = 0x00000004,
        CAST_FLAG_UNKNOWN4          = 0x00000008,
        CAST_FLAG_UNKNOWN5          = 0x00000010,
        CAST_FLAG_AMMO              = 0x00000020, // Projectiles visual
        CAST_FLAG_UNKNOWN7          = 0x00000040, // !0x41 mask used to call CGTradeSkillInfo::DoRecast
        CAST_FLAG_UNKNOWN8          = 0x00000080,
        CAST_FLAG_UNKNOWN9          = 0x00000100,
    };

    class SpellHandler
    {
        internal static void HandleCastSpellOpcode(WorldSession session, CmsgCastSpell handler)
        {
            Character target = (session.Target == null) ? session.Character : session.Target;

            WorldServer.TransmitToAll(new SmsgSpellGo(session.Character, target, handler.SpellId));
            session.sendPacket(new SmsgCastFailed(handler.SpellId));
        }

        internal static void HandleCancelCastOpcode(WorldSession session, CmsgCancelCast handler)
        {
        }
    }
}
