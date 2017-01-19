using System;
using System.Reflection;

namespace World_Server.Game.Update
{
    public struct UpdateFieldEntry
    {
        public PropertyInfo PropertyInfo { get; set; }
        public Byte UpdateField { get; set; }
        public int Index { get; set; }
    }
}
