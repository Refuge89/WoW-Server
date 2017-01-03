using Framework.Contants;
using Framework.Crypt;
using System;
using Framework.Database.Tables;
using World_Server.Handlers.World;
using World_Server.Sessions;

namespace World_Server.Managers
{
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
