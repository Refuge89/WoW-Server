using Framework.Contants;
using Framework.Network;
using System.Collections.Generic;
using World_Server.Handlers;
using World_Server.Helpers;
using System;

namespace World_Server.Managers
{
    public class HandlerManager
    {
        private static readonly List<WorldOpcodes> MovementOpcodes = new List<WorldOpcodes>()
        {
            WorldOpcodes.MSG_MOVE_HEARTBEAT,
            WorldOpcodes.MSG_MOVE_JUMP,
            WorldOpcodes.MSG_MOVE_START_FORWARD,
            WorldOpcodes.MSG_MOVE_START_BACKWARD,
            WorldOpcodes.MSG_MOVE_SET_FACING,
            WorldOpcodes.MSG_MOVE_STOP,
            WorldOpcodes.MSG_MOVE_START_STRAFE_LEFT,
            WorldOpcodes.MSG_MOVE_START_STRAFE_RIGHT,
            WorldOpcodes.MSG_MOVE_STOP_STRAFE,
            WorldOpcodes.MSG_MOVE_START_TURN_LEFT,
            WorldOpcodes.MSG_MOVE_START_TURN_RIGHT,
            WorldOpcodes.MSG_MOVE_STOP_TURN,
            WorldOpcodes.MSG_MOVE_START_PITCH_UP,
            WorldOpcodes.MSG_MOVE_START_PITCH_DOWN,
            WorldOpcodes.MSG_MOVE_STOP_PITCH,
            WorldOpcodes.MSG_MOVE_SET_RUN_MODE,
            WorldOpcodes.MSG_MOVE_SET_WALK_MODE,
            WorldOpcodes.MSG_MOVE_SET_PITCH,
            WorldOpcodes.MSG_MOVE_START_SWIM,
            WorldOpcodes.MSG_MOVE_STOP_SWIM,
            WorldOpcodes.MSG_MOVE_FALL_LAND,
            WorldOpcodes.MSG_MOVE_HOVER,
            WorldOpcodes.MSG_MOVE_KNOCK_BACK
        };

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
            WorldDataRouter.AddHandler<CmsgAreatrigger>(WorldOpcodes.CMSG_AREATRIGGER, MiscHandler.OnAreaTrigger);

            // Chat opcodes
            WorldDataRouter.AddHandler<CmsgJoinChannel>(WorldOpcodes.CMSG_JOIN_CHANNEL, ChatHandler.OnJoinChannel);
            WorldDataRouter.AddHandler<CmsgJoinChannel>(WorldOpcodes.CMSG_LEAVE_CHANNEL, ChatHandler.OnLeaveChannel);
            //WorldDataRouter.AddHandler<CmsgJoinChannel>(WorldOpcodes.CMSG_CHANNEL_LIST, ChatHandler.OnListChannel);
            WorldDataRouter.AddHandler<CmsgMessagechat>(WorldOpcodes.CMSG_MESSAGECHAT, ChatHandler.OnMessageChat);
            WorldDataRouter.AddHandler<CmsgTextEmote>(WorldOpcodes.CMSG_TEXT_EMOTE, ChatHandler.OnTextEmote);

            // Movement opcodes
            WorldDataRouter.AddHandler<CmsgMoveTimeSkipped>(WorldOpcodes.CMSG_MOVE_TIME_SKIPPED, MovementHandler.OnMoveTimeSkipped);
            MovementOpcodes.ForEach(code => WorldDataRouter.AddHandler(code, MovementHandler.GenerateResponse(code)));

            // Spell Opcodes
            WorldDataRouter.AddHandler<CmsgCastSpell>(WorldOpcodes.CMSG_CAST_SPELL, SpellHandler.HandleCastSpellOpcode);
            WorldDataRouter.AddHandler<CmsgCancelCast>(WorldOpcodes.CMSG_CANCEL_CAST, SpellHandler.HandleCancelCastOpcode);

            //Character Opcodes
            WorldDataRouter.AddHandler<CmsgSetSelection>(WorldOpcodes.CMSG_SET_SELECTION, CharHandler.OnSetSelectionPacket);


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
