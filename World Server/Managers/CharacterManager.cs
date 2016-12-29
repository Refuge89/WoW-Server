using Framework.Contants;
using Framework.Database.Tables;
using System;
using System.Collections.Generic;
using World_Server.Handlers;
using World_Server.Handlers.Char;
using World_Server.Sessions;

namespace World_Server.Managers
{
    class CharacterManager
    {
        public static void Boot()
        {
            WorldDataRouter.AddHandler(WorldOpcodes.CMSG_CHAR_ENUM, OnCharEnum);
            WorldDataRouter.AddHandler<PCCharCreate>(WorldOpcodes.CMSG_CHAR_CREATE, OnCharCreate);
            WorldDataRouter.AddHandler<PCCharDelete>(WorldOpcodes.CMSG_CHAR_DELETE, OnCHarDelete);
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
    }
}
