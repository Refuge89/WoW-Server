using Framework.Contants;
using Framework.Helpers;
using System;
using System.Collections.Generic;
using World_Server.Sessions;

namespace World_Server.Handlers
{
    public delegate void ProcessWorldPacketCallback(WorldSession session, byte[] data);
    public delegate void ProcessWorldPacketCallbackTypes<T>(WorldSession session, T handler);

    public class WorldDataRouter
    {
        private static readonly Dictionary<WorldOpcodes, ProcessWorldPacketCallback> MCallbacks = new Dictionary<WorldOpcodes, ProcessWorldPacketCallback>();

        public static void AddHandler(WorldOpcodes opcode, ProcessWorldPacketCallback handler)
        {
            MCallbacks.Add(opcode, handler);
        }

        public static void AddHandler<T>(WorldOpcodes opcode, ProcessWorldPacketCallbackTypes<T> callback)
        {
            AddHandler(opcode, (session, data) =>
            {
                T generatedHandler = (T)Activator.CreateInstance(typeof(T), data);
                callback(session, generatedHandler);
            });
        }

        public static void CallHandler(WorldSession session, WorldOpcodes opcode, byte[] data)
        {
            if (MCallbacks.ContainsKey(opcode))
            {
                MCallbacks[opcode](session, data);
            }
            else
            {
                Log.Print(LogType.Warning, "Missing handler: " + opcode);
            }
        }
    }
}
