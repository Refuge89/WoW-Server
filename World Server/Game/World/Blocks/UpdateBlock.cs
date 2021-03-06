﻿using System.IO;

namespace World_Server.Game.World.Blocks
{
    public abstract class UpdateBlock
    {
        public string Info { get; internal set; }
        public byte[] Data { get; internal set; }

        internal BinaryWriter Writer;

        protected UpdateBlock()
        {
            Writer = new BinaryWriter(new MemoryStream());
        }

        public void Build()
        {
            BuildData();
            Data = (Writer.BaseStream as MemoryStream)?.ToArray();
        }

        public abstract void BuildData();
    }
}
