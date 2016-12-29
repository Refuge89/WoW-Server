using Auth_Server.Handlers;
using Auth_Server.Handlers.Logon;
using Auth_Server.Handlers.Realm;
using Auth_Server.Handlers.Reconnect;
using Auth_Server.Sessions;
using Framework.Contants;
using Framework.Crypt;
using Framework.Helpers;
using System;

namespace Auth_Server.Managers
{
    public class AuthManager
    {
        public static void Boot()
        {
            AuthDataRouter.AddHandler<PCAuthLogonChallenge>(AuthServerOpCode.AUTH_LOGON_CHALLENGE, OnAuthLogonChallenge);
            AuthDataRouter.AddHandler<PCAuthLogonProof>(AuthServerOpCode.AUTH_LOGON_PROOF, OnLogonProof);
            AuthDataRouter.AddHandler(AuthServerOpCode.REALM_LIST, OnRealmList);

            // need implement reconnect code
            AuthDataRouter.AddHandler<PCAuthReconnectChallenge>(AuthServerOpCode.AUTH_RECONNECT_CHALLENGE, OnAuthReconnectChallenge);
            //AuthDataRouter.AddHandler<PCAuthReconnectProof>(AuthServerOpCode.AUTH_RECONNECT_PROOF, OnReconnectProof);
        }

        private static void OnAuthLogonChallenge(AuthSession session, PCAuthLogonChallenge packet)
        {
            Log.Print("Auth Battle.NET", $"New Connection {packet.Name} ({packet.Version} {packet.Build}) {packet.IP} - {packet.OS}/{packet.Platform}", ConsoleColor.Green);

            // Check Build Pass
            if (packet.Build != 5875)
            {
                session.Srp = new SRP(packet.Name.ToUpper(), packet.Name.ToUpper());
                session.sendData(new PSAuthLogonChallange(session.Srp, AuthenticationResult.WrongBuild));
                return;
            }

            // Check User Pass
            var user = Program.Database.GetAccount(packet.Name);
            if (user != null)
            {
                session.accountName = packet.Name;
                session.Srp = new SRP(user.username.ToUpper(), user.password.ToUpper());
                session.sendData(new PSAuthLogonChallange(session.Srp, AuthenticationResult.Success));
                return;
            }

            // Error Unknow Account
            session.Srp = new SRP(packet.Name.ToUpper(), packet.Name.ToUpper());
            session.sendData(new PSAuthLogonChallange(session.Srp, AuthenticationResult.UnknownAccount));
            return;
        }

        private static void OnLogonProof(AuthSession session, PCAuthLogonProof packet)
        {
            session.Srp.ClientEphemeral = packet.A.ToPositiveBigInteger();
            session.Srp.ClientProof     = packet.M1.ToPositiveBigInteger();

            // Causa Warning aqui tem que dar uma olhada nessa merda
            Program.Database.SetSessionKey(session.accountName, session.Srp.SessionKey.ToProperByteArray());

            session.sendData(new PSAuthLogonProof(session.Srp, AuthenticationResult.Success));
        }

        private static void OnAuthReconnectChallenge(AuthSession Session, PCAuthReconnectChallenge packet)
        {
            Log.Print("OnAuthReconnectChallenge");
            Log.Print(packet.OptCode); // 2
            Log.Print(packet.ClientProof);
            Log.Print(packet.ProofData);
            //throw new NotImplementedException();
        }

        private static void OnRealmList(AuthSession session, byte[] packet)
        {
            session.sendPacket(new PSRealmList());
        }
    }
}
