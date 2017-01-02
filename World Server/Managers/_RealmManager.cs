using Framework.Contants;
using Framework.Crypt;
using System;
using Framework.Database.Tables;
using World_Server.Handlers;
using World_Server.Handlers.Auth;
using World_Server.Handlers.Char;
using World_Server.Handlers.Motd;
using World_Server.Handlers.World;
using World_Server.Sessions;

namespace World_Server.Managers
{
    public class RealmManager
    {
        public static void Boot()
        {
            WorldDataRouter.AddHandler<PCAuthSession>(WorldOpcodes.CMSG_AUTH_SESSION, OnAuthSession);
            WorldDataRouter.AddHandler<PCPing>(WorldOpcodes.CMSG_PING, OnPingPacket);
            WorldDataRouter.AddHandler<PCPlayerLogin>(WorldOpcodes.CMSG_PLAYER_LOGIN, OnPlayerLogin);
            WorldDataRouter.AddHandler(WorldOpcodes.CMSG_UPDATE_ACCOUNT_DATA, onUpdateAccount);
        }

        private static void OnAuthSession(WorldSession session, PCAuthSession packet)
        {
            session.Users = Program.Database.GetAccount(packet.AccountName);
            session.Crypt = new VanillaCrypt();
            session.Crypt.Init(session.Users.sessionkey);
            session.sendPacket(new PSAuthResponse());
        }

        public static void OnPingPacket(WorldSession session, PCPing packet)
        {
            session.sendPacket(new PSPong(packet.Ping));
        }

        private static void OnPlayerLogin(WorldSession session, PCPlayerLogin packet)
        {
            session.Character = Program.Database.GetCharacter(Convert.ToInt32(packet.GUID));
            session.sendPacket(new LoginVerifyWorld(session.Character.MapID, session.Character.MapX, session.Character.MapY, session.Character.MapZ, 0));
            session.sendPacket(new PSAccountDataTimes());
            session.sendPacket(new PSSetRestStart());
            session.sendPacket(new PSBindPointUpdate());
            session.sendPacket(new PSTutorialFlags());
            session.sendPacket(new PSLoginSetTimeSpeed());
            session.sendHexPacket(WorldOpcodes.SMSG_INIT_WORLD_STATES, "01 00 00 00 6C 00 AE 07 01 00 32 05 01 00 31 05 00 00 2E 05 00 00 F9 06 00 00 F3 06 00 00 F1 06 00 00 EE 06 00 00 ED 06 00 00 71 05 00 00 70 05 00 00 67 05 01 00 66 05 01 00 50 05 01 00 44 05 00 00 36 05 00 00 35 05 01 00 C6 03 00 00 C4 03 00 00 C2 03 00 00 A8 07 00 00 A3 07 0F 27 74 05 00 00 73 05 00 00 72 05 00 00 6F 05 00 00 6E 05 00 00 6D 05 00 00 6C 05 00 00 6B 05 00 00 6A 05 01 00 69 05 01 00 68 05 01 00 65 05 00 00 64 05 00 00 63 05 00 00 62 05 00 00 61 05 00 00 60 05 00 00 5F 05 00 00 5E 05 00 00 5D 05 00 00 5C 05 00 00 5B 05 00 00 5A 05 00 00 59 05 00 00 58 05 00 00 57 05 00 00 56 05 00 00 55 05 00 00 54 05 01 00 53 05 01 00 52 05 01 00 51 05 01 00 4F 05 00 00 4E 05 00 00 4D 05 01 00 4C 05 00 00 4B 05 00 00 45 05 00 00 43 05 01 00 42 05 00 00 40 05 00 00 3F 05 00 00 3E 05 00 00 3D 05 00 00 3C 05 00 00 3B 05 00 00 3A 05 01 00 39 05 00 00 38 05 00 00 37 05 00 00 34 05 00 00 33 05 00 00 30 05 00 00 2F 05 00 00 2D 05 01 00 16 05 01 00 15 05 00 00 B6 03 00 00 45 07 02 00 36 07 01 00 35 07 01 00 34 07 01 00 33 07 01 00 32 07 01 00 02 07 00 00 01 07 00 00 00 07 00 00 FE 06 00 00 FD 06 00 00 FC 06 00 00 FB 06 00 00 F8 06 00 00 F7 06 00 00 F6 06 00 00 F4 06 D0 07 F2 06 00 00 F0 06 00 00 EF 06 00 00 EC 06 00 00 EA 06 00 00 E9 06 00 00 E8 06 00 00 E7 06 00 00 18 05 00 00 17 05 00 00 03 07 00 00 ");

            session.sendPacket(PSUpdateObject.CreateOwnCharacterUpdate(session.Character, packet));

            // Envia MOTD do Servidor
            session.sendPacket(new PSSendMotd());

            // Envia Cinematic ao Char
            if(session.Character.firsttime == false)
            {
                session.sendPacket(new PSTriggerCinematic(Program.Database.GetCharStarter(session.Character.Race).Cinematic));
                Program.Database.UpdateCharacter(session.Character.Id, "firstlogin");
            }

            // Define Char como online
            Program.Database.UpdateCharacter(session.Character.Id, "online");

            EntityManager.SpawnPlayer(session.Character);
            EntityManager.SendPlayers(session);
        }

        private static void onUpdateAccount(WorldSession session, byte[] data)
        {
            // Update Login Character
        }
    }


    public class EntityManager
    {
        public static void Boot()
        {
        }

        public static void SpawnPlayer(Character character)
        {
            Program.WorldServer.Sessions.FindAll(s => s.Character != character).ForEach(s =>
            {
                Console.WriteLine("Spawning: " + character.Name);
                s.sendPacket(PSUpdateObject.CreateCharacterUpdate(character));
            });
        }

        public static void SendPlayers(WorldSession session)
        {
            Program.WorldServer.Sessions.FindAll(s => s.Character != null)
                .FindAll(s => s.Character != session.Character)
                .ForEach(s =>
                {
                    Console.WriteLine("Spawning: " + s.Character);
                    session.sendPacket(PSUpdateObject.CreateCharacterUpdate(s.Character));
                });
        }
    }
}
