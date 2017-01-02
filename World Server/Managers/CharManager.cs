using System;
using System.Collections.Generic;
using Framework.Contants;
using Framework.Database.Tables;
using World_Server.Handlers;
using World_Server.Sessions;

namespace World_Server.Managers
{
    public class CharManager
    {
        public static void Boot()
        {
            WorldDataRouter.AddHandler<CmsgCharCreate>(WorldOpcodes.CMSG_CHAR_CREATE, OnCharCreate);
            WorldDataRouter.AddHandler(WorldOpcodes.CMSG_CHAR_ENUM, OnCharEnum);
            WorldDataRouter.AddHandler<CmsgCharDelete>(WorldOpcodes.CMSG_CHAR_DELETE, OnCharDelete);
            //WorldDataRouter.AddHandler<CmsgCharRename>(WorldOpcodes.CMSG_CHAR_RENAME, OnCharRename);
        }

        private static void OnCharDelete(WorldSession session, CmsgCharDelete handler)
        {
            // if failed                CHAR_DELETE_FAILED
            // if waiting for transfer  CHAR_DELETE_FAILED_LOCKED_FOR_TRANSFER
            // if guild leader          CHAR_DELETE_FAILED_GUILD_LEADER
            Program.Database.DeleteCharacter(handler.Id);
            session.sendPacket(new SmsgCharDelete(LoginErrorCode.CHAR_DELETE_SUCCESS));
        }

        private static void OnCharCreate(WorldSession session, CmsgCharCreate handler)
        {
            // Precisa fazer o chekin de faccção

            try
            {
                Program.Database.CreateChar(handler, session.Users);
                session.sendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_SUCCESS)); return;
            }
            catch (Exception ex)
            {
                // Name in Use
                if (ex.InnerException != null && ex.InnerException.Message.Contains("Duplicate entry"))
                {
                    session.sendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_NAME_IN_USE));
                    return;
                }

                // Failed another Error
                session.sendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_FAILED));
            }

            session.sendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_ERROR)); return;
        }

        private static void OnCharEnum(WorldSession session, byte[] data)
        {
            List<Character> characters = Program.Database.GetCharacters(session.Users.username);
            session.sendPacket(WorldOpcodes.SMSG_CHAR_ENUM, new SmsgCharEnum(characters).PacketData);
        }

    }
}
