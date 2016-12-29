using Framework.Contants;
using Framework.Extensions;
using Framework.Network;

namespace World_Server.Handlers.Motd
{
    class PSSendMotd : ServerPacket
    {
        public PSSendMotd() : base(WorldOpcodes.SMSG_SERVER_MESSAGE)
        {
            Write(3);

            this.WriteCString("Serivodor Teste Alpha Omega Beta");
            this.WriteCString("Se esta vendo esta mensagem e por que funcionou.");
            this.WriteCString("Tudo pode não funcionar! fique calmo, estou em desenvolvimento!");
        }
    }
}
