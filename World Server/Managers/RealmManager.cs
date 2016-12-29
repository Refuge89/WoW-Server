using Framework.Contants;
using Framework.Crypt;
using Framework.Helpers;
using System;
using World_Server.Handlers;
using World_Server.Handlers.Auth;
using World_Server.Handlers.Motd;
using World_Server.Handlers.Updates;
using World_Server.Handlers.World;
using World_Server.Sessions;

namespace World_Server.Managers
{
    public class RealmManager
    {
        public static void Boot()
        {
            WorldDataRouter.AddHandler<PCAuthSession>(WorldOpcodes.CMSG_AUTH_SESSION, OnAuthSession);
            WorldDataRouter.AddHandler<PCPing>(WorldOpcodes.CMSG_PING, OnPingPacket);
            WorldDataRouter.AddHandler<PCPlayerLogin>(WorldOpcodes.CMSG_PLAYER_LOGIN, OnPlayerLogin);
            WorldDataRouter.AddHandler(WorldOpcodes.CMSG_UPDATE_ACCOUNT_DATA, onUpdateAccount);
        }

        private static void OnAuthSession(WorldSession session, PCAuthSession packet)
        {
            session.users = Program.DBManager.GetAccount(packet.AccountName);
            session.crypt = new VanillaCrypt();
            session.crypt.init(session.users.sessionkey);
            Log.Print(LogType.Debug, "Started Encryption");
            session.sendPacket(new PSAuthResponse());
        }

        public static void OnPingPacket(WorldSession session, PCPing packet)
        {
            session.sendPacket(new PSPong(packet.Ping));
        }

        private static void OnPlayerLogin(WorldSession session, PCPlayerLogin packet)
        {
            session.Character = Program.Database.GetCharacter(Convert.ToInt32(packet.GUID));

            PSUpdateObject playerEntity = PSUpdateObject.CreateOwnCharacterUpdate(session.Character, out session.Entity);

            session.sendPacket(new LoginVerifyWorld(0, -616.383f, -4261.2148437f, 38.2418174743f, 5.50078153610229f));

            session.sendPacket(new PSAccountDataTimes());

            //session.sendPacket(new PSSendMotd());

            session.sendPacket(playerEntity);

            session.Entity.Session = session;
        }

        private static void onUpdateAccount(WorldSession session, byte[] data)
        {
            // Update Login Character

            //Log.Print(LogType.Map, "Length: " + length + " Real Length: " + _dataBuffer.Length);
            //crypt.decrypt(new byte[(int)2 * 6]);
        }
    }
}
