using Auth_Server.Handlers;
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
            AuthDataRouter.AddHandler(AuthServerOpCode.REALM_LIST, onRealmList);

            // need implement reconnect code

        }

        private static void OnAuthLogonChallenge(AuthSession session, PCAuthLogonChallenge packet)
        {
            Log.Print("Auth Battle.NET", $"New Connection {packet.Name} ({packet.Version} {packet.Build}) {packet.IP} - {packet.OS}/{packet.Platform}", ConsoleColor.Green);
            // Quando o size vem 33 ele fica em loading sem fazer nada 
            // no 37 ele passa dboa
            //Log.Print(packet.Size);             // 33 ou 37

            session.accountName = packet.Name;
            var user = Program.Database.GetAccount(packet.Name);

            if (user != null)
            {
                session.Srp = new SRP(user.username.ToUpper(), user.password.ToUpper());
                session.sendData(new PSAuthLogonChallange(session.Srp, AuthenticationResult.Success));
            }
            else
            {
                session.Srp = new SRP(packet.Name.ToUpper(), packet.Name.ToUpper());
                session.sendData(new PSAuthLogonChallange(session.Srp, AuthenticationResult.UnknownAccount));
            }
        }

        private static void OnLogonProof(AuthSession session, PCAuthLogonProof packet)
        {
            session.Srp.ClientEphemeral = packet.A.ToPositiveBigInteger();
            session.Srp.ClientProof     = packet.M1.ToPositiveBigInteger();

            // Causa Warning aqui tem que dar uma olhada nessa merda
            Program.Database.SetSessionKey(session.accountName, session.Srp.SessionKey.ToProperByteArray());

            session.sendData(new PSAuthLogonProof(session.Srp, AuthenticationResult.Success));
        }

        private static void onRealmList(AuthSession session, byte[] packet)
        {
            session.sendPacket(new PSRealmList());
        }
    }
}
