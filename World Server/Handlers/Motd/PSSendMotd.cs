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

            this.WriteCString("Servidor Omega 3 Beta Alpha, se esta vendo esta mensagem e por que funcionou. Tudo pode não funcionar! fique calmo, estou em desenvolvimento!");
        }
    }
}
