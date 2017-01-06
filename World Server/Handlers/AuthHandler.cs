using System;
using Framework.Contants;
using Framework.Crypt;
using Framework.Network;
using World_Server.Sessions;

namespace World_Server.Handlers
{

    #region CMSG_UPDATE_ACCOUNT_DATA
    public sealed class CmsgUpdateAccountData : PacketReader
    {
        public int Type { get; private set; }
        public int Time { get; private set; }
        public int Size { get; private set; }
        public string Data;

        public CmsgUpdateAccountData(byte[] data) : base(data)
        {
            Type = ReadInt32();
            Time = ReadInt32();
            Size = ReadInt32();
            //Data = ReadByte().ToString();
        }
    }
    #endregion

    #region SMSG_UPDATE_ACCOUNT_DATA

    #endregion

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
            Write((byte)LoginErrorCode.AUTH_OK);
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

        internal static void OnUpdateaccountData(WorldSession session, CmsgUpdateAccountData handler)
        {
            /*
            Console.WriteLine(handler.Data);
            Console.WriteLine(handler.Type);
            Console.WriteLine(handler.Time);
            Console.WriteLine(handler.Size);

            //SMSG_UPDATE_ACCOUNT_DATA
            /*
            packet.Write(0x00000002);
            packet.Write((UInt32)0);
            */

            /*
            response.AddInt32(DataID)
            response.AddInt32(0) 'Unk
            */

            /*
            var guid = (UInt64)0; //TODO: replace with active character if they are there!

            p.Write(guid);
            p.Write(id);

            if (State.Data == null || State.Data.TimeStamp == null || State.Data.Size == null || State.Data.Data == null)
            {
                p.Write((int)0); //time
                p.Write((int)0); //size
            }
            else
            {
                p.Write(State.Data.TimeStamp[id]);
                p.Write(State.Data.Size[id]);
                p.Write(State.Data.Data[id]);
            }
            */
        }
    }
}
 