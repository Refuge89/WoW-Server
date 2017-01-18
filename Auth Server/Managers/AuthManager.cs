using Auth_Server.Handlers;
using Auth_Server.Sessions;
using Framework.Contants;
using Framework.Crypt;
using Framework.Helpers;
using System;
using Auth_Server.Router;
using Framework.Database.Tables;

namespace Auth_Server.Managers
{
    public class AuthManager
    {
        private static Users _user;

        public static void Boot()
        {
            AuthRouter.AddHandler<PcAuthLogonChallenge>(AuthServerOpcode.AUTH_LOGON_CHALLENGE, OnAuthLogonChallenge);
            AuthRouter.AddHandler<PcAuthLogonProof>(AuthServerOpcode.AUTH_LOGON_PROOF, OnLogonProof);
            AuthRouter.AddHandler<PcAuthLogonChallenge>(AuthServerOpcode.AUTH_RECONNECT_CHALLENGE, OnAuthLogonChallenge);
            AuthRouter.AddHandler<PcAuthLogonProof>(AuthServerOpcode.AUTH_RECONNECT_PROOF, OnLogonProof);
            AuthRouter.AddHandler(AuthServerOpcode.REALM_LIST, OnRealmList);
        }

        private static void OnAuthLogonChallenge(AuthSession session, PcAuthLogonChallenge packet)
        {
            Log.Print("Auth Battle.NET",
                $"New Connection {packet.Name} ({packet.Version} {packet.Build}) {packet.Ip} - {packet.Os}/{packet.Platform}",
                ConsoleColor.Green);

            // Aqui se vc fica tentanto loga multiplas vezes ou erra, ele trava por um tempo acho que e do client

            // Check Build Pass
            if (packet.Build != 5875)
            {
                session.Srp = new SRP(packet.Name.ToUpper(), packet.Name.ToUpper());
                session.sendData(new PsAuthLogonChallange(session.Srp, AuthServerResult.WrongBuild));
                return;
            }

            // Retrieve User information of Database
            try
            {
                _user = Program.DatabaseManager.GetAccount(packet.Name);
            }
            catch (Exception)
            {
                session.Srp = new SRP(packet.Name.ToUpper(), packet.Name.ToUpper());
                session.sendData(new PsAuthLogonChallange(session.Srp, AuthServerResult.Failure));
                return;
            }

            // Error Unknow Account
            if (_user == null)
            {
                session.Srp = new SRP(packet.Name.ToUpper(), packet.Name.ToUpper());
                session.sendData(new PsAuthLogonChallange(session.Srp, AuthServerResult.UnknownAccount));
                return;
            }

            // Check Ban
            if (_user.bannet_at != null)
            {
                session.Srp = new SRP(_user.username.ToUpper(), _user.password.ToUpper());
                session.sendData(new PsAuthLogonChallange(session.Srp, AuthServerResult.AccountBanned));
                return;
            }

            // Check User Pass
            session.AccountName = packet.Name;
            session.Srp = new SRP(_user.username.ToUpper(), _user.password.ToUpper());
            session.sendData(new PsAuthLogonChallange(session.Srp, AuthServerResult.Success));
        }

        private static async void OnLogonProof(AuthSession session, PcAuthLogonProof packet)
        {
            session.Srp.ClientEphemeral = packet.A.ToPositiveBigInteger();
            session.Srp.ClientProof = packet.M1.ToPositiveBigInteger();

            // Causa Warning aqui tem que dar uma olhada nessa merda
            await Program.DatabaseManager.SetSessionKey(session.AccountName, session.Srp.SessionKey.ToProperByteArray());

            session.sendData(new PsAuthLogonProof(session.Srp, AuthServerResult.Success));
        }

        private static void OnRealmList(AuthSession session, byte[] packet)
        {
            // Get Realms
            var realms = Program.DatabaseManager.GetRealms();
            session.SendPacket(new PsRealmList(realms, session.AccountName));
        }
    }
}