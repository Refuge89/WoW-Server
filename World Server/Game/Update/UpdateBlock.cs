using Framework.Contants.Game;
using Framework.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World_Server.Game.Entitys;

namespace World_Server.Game.Update
{
    public abstract class UpdateBlock
    {
        public string Info { get; internal set; }
        public byte[] Data { get; internal set; }

        internal BinaryWriter Writer;

        public UpdateBlock()
        {
            Writer = new BinaryWriter(new MemoryStream());
        }

        public void Build()
        {
            BuildData();

            Data = (Writer.BaseStream as MemoryStream).ToArray();
            Info = BuildInfo();
        }

        public abstract void BuildData();
        public abstract string BuildInfo();
    }

    public class OutOfRangeBlock : UpdateBlock
    {
        public List<ObjectEntity> Entitys;

        public OutOfRangeBlock(List<ObjectEntity> entitys) : base()
        {
            Entitys = entitys;

            Build(); // ):
        }

        public override void BuildData()
        {
            Writer.Write((byte)ObjectUpdateType.UPDATETYPE_OUT_OF_RANGE_OBJECTS);
            Writer.Write((uint)Entitys.Count);

            foreach (ObjectEntity entity in Entitys)
            {
                Writer.WritePackedUInt64(entity.ObjectGUID.RawGUID);
            }
        }

        public override string BuildInfo()
        {
            return "[OutOfRange] " + string.Join(", ", Entitys.ToArray().ToList().ConvertAll<string>(e => e.Name).ToArray());
        }
    }
}
