using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Contants.Game;
using Framework.Extensions;
using Object = World_Server.Game.Entitys.Object;

namespace World_Server.Game.World.Blocks
{
    public class OutOfRangeBlock : UpdateBlock
    {
        public List<Object> Entitys;

        public OutOfRangeBlock(List<Object> entitys)
        {
            Entitys = entitys;
            Build();
        }

        public override void BuildData()
        {
            Writer.Write((byte)ObjectUpdateType.UPDATETYPE_OUT_OF_RANGE_OBJECTS);
            Writer.Write((uint)Entitys.Count);

            foreach (Object entity in Entitys)
            {
                Writer.WritePackedUInt64(entity.ObjectGuid.RawGuid);
            }
        }

        public override string BuildInfo()
        {
            Console.WriteLine($"[OutOfRange] " + string.Join(", ", Entitys.ToArray().ToList().ConvertAll<string>(e => e.Name).ToArray()));
            return "ok";
        }
    }
}
