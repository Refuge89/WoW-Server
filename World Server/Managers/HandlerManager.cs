using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Framework.Contants;
using Framework.Network;
using World_Server.Handlers;
using World_Server.Helpers;
using World_Server.Sessions;

namespace World_Server.Managers
{
    public class HandlerManager
    {
        public static void Boot()
        {
            // Login related opcodes
            WorldDataRouter.AddHandler<CmsgAuthSession>(WorldOpcodes.CMSG_AUTH_SESSION, AuthHandler.OnAuthSession);
            WorldDataRouter.AddHandler(WorldOpcodes.CMSG_CHAR_ENUM, CharHandler.OnCharEnum);
            WorldDataRouter.AddHandler<CmsgCharCreate>(WorldOpcodes.CMSG_CHAR_CREATE, CharHandler.OnCharCreate);
            WorldDataRouter.AddHandler<CmsgCharDelete>(WorldOpcodes.CMSG_CHAR_DELETE, CharHandler.OnCharDelete);
            WorldDataRouter.AddHandler<CmsgPing>(WorldOpcodes.CMSG_PING, InternalHandler.OnPingPacket);
            WorldDataRouter.AddHandler<CmsgPlayerLogin>(WorldOpcodes.CMSG_PLAYER_LOGIN, WorldHandler.OnPlayerLogin);
            WorldDataRouter.AddHandler<PacketReader>(WorldOpcodes.CMSG_LOGOUT_REQUEST, WorldHandler.OnLogoutRequest);
            WorldDataRouter.AddHandler<PacketReader>(WorldOpcodes.CMSG_LOGOUT_CANCEL, WorldHandler.OnLogoutCancel);
            //WorldDataRouter.AddHandler(WorldOpcodes.CMSG_ZONEUPDATE, MovementHandler.HandleZoneUpdate);
            WorldDataRouter.AddHandler<CmsgNameQuery>(WorldOpcodes.CMSG_NAME_QUERY, CharHandler.OnNameQuery);
        }


    }
}
