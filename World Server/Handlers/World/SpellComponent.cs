﻿using System;
using Framework.Contants;
using Framework.Contants.Character;
using Framework.Database.Tables;
using Framework.DBC.Structs;
using Framework.Extensions;
using Framework.Network;
using World_Server.Sessions;

namespace World_Server.Handlers.World
{
    public sealed class SMSG_SPELL_START : ServerPacket
    {
        public SMSG_SPELL_START(WorldSession caster, Character target, int spellId) : base(WorldOpcodes.SMSG_SPELL_START)
        {
            Console.WriteLine("SMSG_SPELL_START");
            this.WritePackedUInt64((ulong) caster.Character.Id);
            this.WritePackedUInt64((ulong) target.Id);

            Write(spellId);
            Write((ushort)SpellCastFlags.CastFlagUnknown9); // Cast Flags!?
            Write((byte)1); // Target Length
            Write(target.Id);
            Write((byte)0); // End
            Write((ushort)2); // TARGET_FLAG_UNIT
            this.WritePackedUInt64((ulong)target.Id);
        }
    }

    public class SpellComponent
    {
        private Character caster;
        private WorldSession session;

        internal void SendSpellStart()
        {
            this.State = SpellState.SPELL_STATE_PREPARING;
            this.SetCastTime();

            session.SendPacket(new SMSG_SPELL_START(session, Targets, (int)Spell.Id));
        }

        private void SetCastTime()
        {
            //throw new NotImplementedException();
        }

        private float _orientation;
        private uint _health;

        internal void Initialize()
        {
            session.SendMessage($"Initialize Spell {Spell.Name} / {Spell.Description}");
        }

        public SpellComponent(WorldSession session)
        {
            this.session = session;
        }

        public SpellComponent(Character caster)
        {
            this.caster = caster;
        }

        public Spell Spell { get; internal set; }
        public Character Targets { get; internal set; }
        public bool Triggered { get; internal set; }
        public float Location { get; private set; }
        public float MapX { get; private set; }
        public float MapY { get; private set; }
        public float MapZ { get; private set; }
        public volatile SpellState State = SpellState.SPELL_STATE_DELAYED;
    }

    public enum SpellState
    {
        SPELL_STATE_PREPARING = 0,                              // cast time delay period, non channeled spell
        SPELL_STATE_CASTING = 1,                              // channeled time period spell casting state
        SPELL_STATE_FINISHED = 2,                              // cast finished to success or fail
        SPELL_STATE_DELAYED = 3                               // spell casted but need time to hit target(s)
    }
}