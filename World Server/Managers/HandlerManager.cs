using Framework.Contants;
using Framework.Network;
using World_Server.Handlers;
using World_Server.Helpers;

namespace World_Server.Managers
{
    public class HandlerManager
    {
        public static void Boot()
        {
            // Login related opcodes
            WorldDataRouter.AddHandler<CmsgAuthSession>(WorldOpcodes.CMSG_AUTH_SESSION, AuthHandler.OnAuthSession);
            WorldDataRouter.AddHandler<CmsgUpdateAccountData>(WorldOpcodes.CMSG_UPDATE_ACCOUNT_DATA, AuthHandler.OnUpdateaccountData); 
            WorldDataRouter.AddHandler(WorldOpcodes.CMSG_CHAR_ENUM, CharHandler.OnCharEnum);
            WorldDataRouter.AddHandler<CmsgCharCreate>(WorldOpcodes.CMSG_CHAR_CREATE, CharHandler.OnCharCreate);
            WorldDataRouter.AddHandler<CmsgCharDelete>(WorldOpcodes.CMSG_CHAR_DELETE, CharHandler.OnCharDelete);
            WorldDataRouter.AddHandler<CmsgPing>(WorldOpcodes.CMSG_PING, InternalHandler.OnPingPacket);
            WorldDataRouter.AddHandler<CmsgPlayerLogin>(WorldOpcodes.CMSG_PLAYER_LOGIN, WorldHandler.OnPlayerLogin);
            WorldDataRouter.AddHandler<PacketReader>(WorldOpcodes.CMSG_LOGOUT_REQUEST, WorldHandler.OnLogoutRequest);
            WorldDataRouter.AddHandler<PacketReader>(WorldOpcodes.CMSG_LOGOUT_CANCEL, WorldHandler.OnLogoutCancel);
            WorldDataRouter.AddHandler<CmsgZoneupdate>(WorldOpcodes.CMSG_ZONEUPDATE, MovementHandler.HandleZoneUpdate);
            WorldDataRouter.AddHandler<CmsgNameQuery>(WorldOpcodes.CMSG_NAME_QUERY, CharHandler.OnNameQuery);

            // Misc Opcodes
            WorldDataRouter.AddHandler(WorldOpcodes.CMSG_QUERY_TIME, MiscHandler.OnQueryTime);

            // Chat opcodes
            WorldDataRouter.AddHandler<CmsgJoinChannel>(WorldOpcodes.CMSG_JOIN_CHANNEL, ChatHandler.OnJoinChannel);

            // CMSG_REQUEST_RAID_INFO => Aqui verifica se esta em raid group

            /*      CMSG_GMTICKET_GETTICKET
			    if (ticket != null)
				{
					packet.Write((uint)TicketInfoResponse.Pending);
					packet.WriteCString(ticket.Message);
					packet.Write((byte)ticket.Type);
					packet.Write((float)0);
					packet.Write((float)0);
					packet.Write((float)0);
					packet.Write((ushort)0);
					client.Send(packet);
				}
				else
				{
					packet.Write((uint)TicketInfoResponse.NoTicket);
				}

	            public enum TicketInfoResponse : uint
                {
                    Fail = 1,
                    Saved = 2,
                    Pending = 6,
                    Deleted = 9,
                    NoTicket = 10,
                }
            */

            // MSG_QUERY_NEXT_MAIL_TIME => acho que aq liga um timer de envio de email sei la [https://github.com/WCell/WCell/blob/master/Services/WCell.RealmServer/Handlers/MailHandler.cs]

            // CMSG_BATTLEFIELD_STATUS => aqui verifica se esta no queue ou desertor??

            // CMSG_MEETINGSTONE_INFO

            // CMSG_MOVE_TIME_SKIPPED

            // MSG_MOVE_FALL_LAND
        }
    }
}
