using Framework.Contants;
using Framework.Crypt;
using Framework.Database.Tables;
using Framework.Helpers;
using System.Collections.Generic;
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
            WorldDataRouter.AddHandler(WorldOpcodes.CMSG_CHAR_ENUM, OnCharEnum);
            WorldDataRouter.AddHandler<PCPing>(WorldOpcodes.CMSG_PING, OnPingPacket);
        }

        private static void OnAuthSession(WorldSession session, PCAuthSession packet)
        {
            session.users = Program.Database.GetAccount(packet.AccountName);
            session.crypt = new VanillaCrypt();
            session.crypt.init(session.users.sessionkey);
            Log.Print(LogType.Debug, "Started Encryption");
            session.sendPacket(new PSAuthResponse());
        }

        private static void OnCharEnum(WorldSession session, byte[] packet)
        {
            List<Character> characters = null;
            session.sendPacket(WorldOpcodes.SMSG_CHAR_ENUM, new PSCharEnum(characters).PacketData);
        }

        public static void OnPingPacket(WorldSession session, PCPing packet)
        {
            session.sendPacket(new PSPong(packet.Ping));
        }
    }
}
