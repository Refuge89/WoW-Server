using Framework.Contants;
using Framework.Crypt;
using Framework.Helpers;
using World_Server.Handlers;
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
            //session.Entity.Session = session;
            //World.DispatchOnPlayerSpawn(session.Entity);
        }
    }
}
