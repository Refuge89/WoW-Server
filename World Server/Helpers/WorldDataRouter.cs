using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Framework.Contants;
using World_Server.Sessions;

namespace World_Server.Helpers
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
                Debug.WriteLine($"Missing handler: {opcode}");
                //Main._Main.Log($"Missing handler: {opcode}", Color.Red);
            }
        }
    }
}
