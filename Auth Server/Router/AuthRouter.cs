using System;
using System.Collections.Generic;
using Auth_Server.Sessions;
using Framework.Contants;
using Framework.Helpers;

namespace Auth_Server.Router
{
    public delegate void ProcessLoginPacketCallback(AuthSession session, byte[] data);

    public delegate void ProcessLoginPacketCallbackTypes<T>(AuthSession session, T handler);

    public class AuthRouter
    {
        private static readonly Dictionary<AuthServerOpcode, ProcessLoginPacketCallback> MCallbacks = new Dictionary<AuthServerOpcode, ProcessLoginPacketCallback>();

        public static void AddHandler(AuthServerOpcode opcode, ProcessLoginPacketCallback handler)
        {
            MCallbacks.Add(opcode, handler);
        }

        public static void AddHandler<T>(AuthServerOpcode opcode, ProcessLoginPacketCallbackTypes<T> callback)
        {
            AddHandler(opcode, (session, data) =>
            {
                T generatedHandler = (T)Activator.CreateInstance(typeof(T), data);
                callback(session, generatedHandler);
            });
        }

        public static void CallHandler(AuthSession session, AuthServerOpcode opcode, byte[] data)
        {
            if (MCallbacks.ContainsKey(opcode))
                MCallbacks[opcode](session, data);
            else
                Log.Print("Auth Battle.NET", $"Missing handler: {opcode}", ConsoleColor.Green);
        }
    }
}
