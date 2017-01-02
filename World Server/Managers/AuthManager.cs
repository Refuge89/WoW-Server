using Framework.Contants;
using Framework.Crypt;
using World_Server.Handlers;
using World_Server.Sessions;

namespace World_Server.Managers
{
    public class AuthManager
    {
        public static void Boot()
        {
            WorldDataRouter.AddHandler<CmsgAuthSession>(WorldOpcodes.CMSG_AUTH_SESSION, OnAuthSession);
            //WorldDataRouter.AddHandler(WorldOpcodes.CMSG_AUTH_SRP6_BEGIN, OnNada);
            //WorldDataRouter.AddHandler(WorldOpcodes.CMSG_AUTH_SRP6_PROOF, OnNada);
            //WorldDataRouter.AddHandler(WorldOpcodes.CMSG_AUTH_SRP6_RECODE, OnNada);
        }

        private static void OnAuthSession(WorldSession session, CmsgAuthSession handler)
        {
            session.Users = Program.Database.GetAccount(handler.AccountName);
            session.Crypt = new VanillaCrypt();
            session.Crypt.Init(session.Users.sessionkey);
            session.sendPacket(new SmsgAuthResponse());
        }
    }
}