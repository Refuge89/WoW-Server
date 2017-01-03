using System;
using Framework.Contants;
using World_Server.Handlers;
using World_Server.Sessions;
using Framework.Network;

namespace World_Server.Managers
{
    public class VaiBuceta : PacketReader
    {
        public string Teste { get; private set; }

        public VaiBuceta(byte[] data) : base(data)
        {
            //Teste = ReadUInt32();       // 6
            //Teste = ReadUInt32();       // 6
            //Teste = (int)ReadUInt64();  // 6
            //Teste = ReadByte();         // 6
            //Teste = ReadCString();
        }
    }

    public class InternalManager
    {
        public static void Boot()
        {
            WorldDataRouter.AddHandler<CmsgPing>(WorldOpcodes.CMSG_PING, OnPingPacket);
            WorldDataRouter.AddHandler<VaiBuceta>(WorldOpcodes.CMSG_UPDATE_ACCOUNT_DATA, OnUpdateAccountData);
        }

        private static void OnUpdateAccountData(WorldSession session, VaiBuceta handler)
        {
            Console.WriteLine("CMSG_UPDATE_ACCOUNT_DATA");
        }

        public static void OnPingPacket(WorldSession session, CmsgPing packet)
        {
            session.sendPacket(new SmsgPong(packet.Ping));
        }
    }
}
