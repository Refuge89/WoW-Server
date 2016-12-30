using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using World_Server.Handlers;
using World_Server.Handlers.Char;
using World_Server.Handlers.Player;
using World_Server.Sessions;

namespace World_Server.Managers
{
    class CharacterManager
    {
        public static Dictionary<WorldSession, DateTime> logoutQueue;

        public static void Boot()
        {
            logoutQueue = new Dictionary<WorldSession, DateTime>();

            Thread thread = new Thread(update);
            thread.Start();

            WorldDataRouter.AddHandler(WorldOpcodes.CMSG_CHAR_ENUM, OnCharEnum);
            WorldDataRouter.AddHandler<PCCharCreate>(WorldOpcodes.CMSG_CHAR_CREATE, OnCharCreate);
            WorldDataRouter.AddHandler<PCCharDelete>(WorldOpcodes.CMSG_CHAR_DELETE, OnCHarDelete);
            WorldDataRouter.AddHandler<PacketReader>(WorldOpcodes.CMSG_LOGOUT_REQUEST, OnLogoutRequest);
            WorldDataRouter.AddHandler<PacketReader>(WorldOpcodes.CMSG_LOGOUT_CANCEL, OnLogoutCancel);

            WorldDataRouter.AddHandler<PCNameQuery>(WorldOpcodes.CMSG_NAME_QUERY, OnNameQueryPacket);
        }

        private static void OnCharEnum(WorldSession session, byte[] packet)
        {
            List<Character> characters = Program.Database.GetCharacters(session.users.username);
            session.sendPacket(WorldOpcodes.SMSG_CHAR_ENUM, new PSCharEnum(characters).PacketData);
        }
        
        private static void OnCharCreate(WorldSession session, PCCharCreate packet)
        {
            // Check char Faction if Horder not able to create Alliance
            // creation disabled            CHAR_CREATE_DISABLED
            // server limit de char         CHAR_CREATE_SERVER_LIMIT
            // limite de char alcançado     CHAR_CREATE_ACCOUNT_LIMIT
            try
            {
                Program.Database.CreateChar(packet, session.users);
                session.sendPacket(new PSCharCreate(LoginErrorCode.CHAR_CREATE_SUCCESS));
                return;
            }
            catch (Exception ex)
            {
                // Name in Use
                if (ex.InnerException.Message.Contains("Duplicate entry"))
                    session.sendPacket(new PSCharCreate(LoginErrorCode.CHAR_CREATE_NAME_IN_USE)); return;

                // Failed another Error
                session.sendPacket(new PSCharCreate(LoginErrorCode.CHAR_CREATE_FAILED)); return;
            }

            session.sendPacket(new PSCharCreate(LoginErrorCode.CHAR_CREATE_ERROR)); return;
        }

        private static void OnCHarDelete(WorldSession session, PCCharDelete packet)
        {
            // if failed                CHAR_DELETE_FAILED
            // if waiting for transfer  CHAR_DELETE_FAILED_LOCKED_FOR_TRANSFER
            // if guild leader          CHAR_DELETE_FAILED_GUILD_LEADER
            Program.Database.DeleteCharacter(packet.Id);
            session.sendPacket(new PSCharDelete(LoginErrorCode.CHAR_DELETE_SUCCESS));
        }

        private static void OnLogoutRequest(WorldSession session, PacketReader reader)
        {
            if (logoutQueue.ContainsKey(session)) logoutQueue.Remove(session);

            session.sendPacket(new PCLogoutResponse());
            logoutQueue.Add(session, DateTime.Now);
        }

        private static void OnLogoutCancel(WorldSession session, PacketReader reader)
        {
            logoutQueue.Remove(session);
            session.sendPacket(new PSLogoutCancelAcknowledgement());
        }

        public static void OnNameQueryPacket(WorldSession session, PCNameQuery packet)
        {
            Character target = Program.Database.GetCharacter((int)packet.GUID);

            if (target != null)
            {
                session.sendPacket(new PSNameQueryResponse(target));
            }
        }

        public static void update()
        {
            while (true)
            {
                foreach (KeyValuePair<WorldSession, DateTime> entry in logoutQueue.ToArray())
                {
                    if (DateTime.Now.Subtract(entry.Value).Seconds >= 20)
                    {
                        entry.Key.sendPacket(new PSLogoutComplete());
                        logoutQueue.Remove(entry.Key);
                        //World.DispatchOnPlayerDespawn(entry.Key.Entity);
                    }
                }

                Thread.Sleep(1000);
            }
        }

    }
}
