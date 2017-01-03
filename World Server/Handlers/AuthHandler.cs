using Framework.Contants;
using Framework.Crypt;
using Framework.Network;
using World_Server.Sessions;

namespace World_Server.Handlers
{

    #region CMSG_AUTH_SESSION
    public sealed class CmsgAuthSession : PacketReader
    {
        public int ClientBuild { get; private set; }
        public int Unk2 { get; private set; }
        public string AccountName { get; private set; }

        public CmsgAuthSession(byte[] data) : base(data)
        {
            ClientBuild = ReadInt32();
            Unk2 = ReadInt32();
            AccountName = ReadCString();
        }
    }
    #endregion
    
    #region SMSG_AUTH_RESPONSE
    sealed class SmsgAuthResponse : ServerPacket
    {
        public SmsgAuthResponse() : base(WorldOpcodes.SMSG_AUTH_RESPONSE)
        {
            Write((byte) ResponseCodes.AUTH_OK);
        }
    }
    #endregion

    public class AuthHandler
    {
        public static void OnAuthSession(WorldSession session, CmsgAuthSession handler)
        {
            session.Users = Program.Database.GetAccount(handler.AccountName);
            session.Crypt = new VanillaCrypt();
            session.Crypt.Init(session.Users.sessionkey);
            session.sendPacket(new SmsgAuthResponse());
        }
    }
}