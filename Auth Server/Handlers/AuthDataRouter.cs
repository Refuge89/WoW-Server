using Auth_Server.Sessions;
using Framework.Contants;
using Framework.Helpers;
using System;
using System.Collections.Generic;

namespace Auth_Server.Handlers
{
    public delegate void ProcessLoginPacketCallback(AuthSession Session, byte[] data);
    public delegate void ProcessLoginPacketCallbackTypes<T>(AuthSession Session, T handler);

    public class AuthDataRouter
    {
        private static Dictionary<AuthServerOpCode, ProcessLoginPacketCallback> mCallbacks = new Dictionary<AuthServerOpCode, ProcessLoginPacketCallback>();

        public static void AddHandler(AuthServerOpCode opcode, ProcessLoginPacketCallback handler)
        {
            mCallbacks.Add(opcode, handler);
        }

        public static void AddHandler<T>(AuthServerOpCode opcode, ProcessLoginPacketCallbackTypes<T> callback)
        {
            AddHandler(opcode, (session, data) =>
            {
                T generatedHandler = (T)Activator.CreateInstance(typeof(T), data);
                callback(session, generatedHandler);
            });
        }

        public static void CallHandler(AuthSession session, AuthServerOpCode opcode, byte[] data)
        {
            if (mCallbacks.ContainsKey(opcode))
                mCallbacks[opcode](session, data);
            else
                Log.Print("Auth Battle.NET", $"Missing handler: {opcode}", ConsoleColor.Green);
        }
    }
}
