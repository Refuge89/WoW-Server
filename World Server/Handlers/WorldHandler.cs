using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Framework.Contants;
using Framework.Network;
using World_Server.Sessions;
using Framework.Database.Tables;
using World_Server.Handlers.Objects;
using World_Server.Managers;

namespace World_Server.Handlers
{

    #region CMSG_NAME_QUERY
    public sealed class CmsgNameQuery : PacketReader
    {
        public uint Guid { get; private set; }

        public CmsgNameQuery(byte[] data) : base(data)
        {
            Guid = ReadUInt32();
        }
    }
    #endregion

    #region SMSG_NAME_QUERY_RESPONSE
    public sealed class SmsgNameQueryResponse : ServerPacket
    {
        public SmsgNameQueryResponse(Character character) : base(WorldOpcodes.SMSG_NAME_QUERY_RESPONSE)
        {
            Write((ulong)character.Id);
            Write(Encoding.UTF8.GetBytes(character.Name + '\0'));
            Write((byte)0); // realm name for cross realm BG usage
            Write((uint)character.Race);
            Write((uint)character.Gender);
            Write((uint)character.Class);
        }
    }
    #endregion

    #region SMSG_LOGOUT_RESPONSE
    public sealed class SmsgLogoutResponse : ServerPacket
    {
        public SmsgLogoutResponse() : base(WorldOpcodes.SMSG_LOGOUT_RESPONSE)
        {
            Write((UInt32)0);
            Write((byte)0);
        }
    }
    #endregion

    #region SMSG_LOGOUT_CANCEL_ACK
    sealed class SmsgLogoutCancelAck : ServerPacket
    {
        public SmsgLogoutCancelAck() : base(WorldOpcodes.SMSG_LOGOUT_CANCEL_ACK)
        {
            Write((byte)0);
        }
    }
    #endregion

    #region SMSG_LOGOUT_COMPLETE
    sealed class SmsgLogoutComplete : ServerPacket
    {
        public SmsgLogoutComplete() : base(WorldOpcodes.SMSG_LOGOUT_COMPLETE)
        {
            Write((byte)0);
        }
    }
    #endregion
    
    class WorldHandler
    {
        public static Dictionary<WorldSession, DateTime> LogoutQueue;

        internal static void OnLogoutRequest(WorldSession session, PacketReader handler)
        {
            LogoutQueue = new Dictionary<WorldSession, DateTime>();

            if (LogoutQueue.ContainsKey(session)) LogoutQueue.Remove(session);

            session.sendPacket(new SmsgLogoutResponse());
            LogoutQueue.Add(session, DateTime.Now);

            Thread thread = new Thread(Update);
            thread.Start();
        }

        internal static void OnLogoutCancel(WorldSession session, PacketReader handler)
        {
            LogoutQueue.Remove(session);
            session.sendPacket(new SmsgLogoutCancelAck());
        }

        internal static void OnPlayerLogin(WorldSession session, CmsgPlayerLogin packet)
        {
            // Recupera informação do Char
            session.Character = Program.Database.GetCharacter(Convert.ToInt32(packet.Guid));
            Program.Database.UpdateCharacter(session.Character.Id, "online");

            PreLoad(session);

            if (session.Character.firsttime == false)
                session.sendPacket(new SmsgTriggerCinematic(session, Program.Database.GetCharStarter(session.Character.Race).Cinematic));

            session.sendHexPacket(WorldOpcodes.SMSG_INIT_WORLD_STATES, "01 00 00 00 6C 00 AE 07 01 00 32 05 01 00 31 05 00 00 2E 05 00 00 F9 06 00 00 F3 06 00 00 F1 06 00 00 EE 06 00 00 ED 06 00 00 71 05 00 00 70 05 00 00 67 05 01 00 66 05 01 00 50 05 01 00 44 05 00 00 36 05 00 00 35 05 01 00 C6 03 00 00 C4 03 00 00 C2 03 00 00 A8 07 00 00 A3 07 0F 27 74 05 00 00 73 05 00 00 72 05 00 00 6F 05 00 00 6E 05 00 00 6D 05 00 00 6C 05 00 00 6B 05 00 00 6A 05 01 00 69 05 01 00 68 05 01 00 65 05 00 00 64 05 00 00 63 05 00 00 62 05 00 00 61 05 00 00 60 05 00 00 5F 05 00 00 5E 05 00 00 5D 05 00 00 5C 05 00 00 5B 05 00 00 5A 05 00 00 59 05 00 00 58 05 00 00 57 05 00 00 56 05 00 00 55 05 00 00 54 05 01 00 53 05 01 00 52 05 01 00 51 05 01 00 4F 05 00 00 4E 05 00 00 4D 05 01 00 4C 05 00 00 4B 05 00 00 45 05 00 00 43 05 01 00 42 05 00 00 40 05 00 00 3F 05 00 00 3E 05 00 00 3D 05 00 00 3C 05 00 00 3B 05 00 00 3A 05 01 00 39 05 00 00 38 05 00 00 37 05 00 00 34 05 00 00 33 05 00 00 30 05 00 00 2F 05 00 00 2D 05 01 00 16 05 01 00 15 05 00 00 B6 03 00 00 45 07 02 00 36 07 01 00 35 07 01 00 34 07 01 00 33 07 01 00 32 07 01 00 02 07 00 00 01 07 00 00 00 07 00 00 FE 06 00 00 FD 06 00 00 FC 06 00 00 FB 06 00 00 F8 06 00 00 F7 06 00 00 F6 06 00 00 F4 06 D0 07 F2 06 00 00 F0 06 00 00 EF 06 00 00 EC 06 00 00 EA 06 00 00 E9 06 00 00 E8 06 00 00 E7 06 00 00 18 05 00 00 17 05 00 00 03 07 00 00 ");                   

            session.sendPacket(ObjectHandler.CreateOwnCharacterUpdate(session.Character, packet));
            
            session.sendPacket(new SmsgServerMessage(3, "Welcome to the World of Warcraft"));

            EntityManager.SpawnPlayer(session.Character);
            EntityManager.SendPlayers(session);
        }

        private static void PreLoad(WorldSession session)
        {
            session.sendPacket(new SmsgLoginVerifyWorld(session.Character));
            session.sendPacket(new SmsgLoginSettimespeed());
            session.sendPacket(new SmsgAccountDataTimes());
            session.sendPacket(new SmsgSetRestStart());
            session.sendPacket(new SmsgBindpointupdate(session.Character));
            session.sendPacket(new SmsgTutorialFlags());
            session.sendPacket(new SmsgInitialSpells(session.Character));
            // Envia Action Bar do Char
        }

        private static void Update()
        {
            while (true)
            {
                foreach (KeyValuePair<WorldSession, DateTime> entry in LogoutQueue.ToArray())
                {
                    if (DateTime.Now.Subtract(entry.Value).Seconds >= 20)
                    {
                        entry.Key.sendPacket(new SmsgLogoutComplete());
                        LogoutQueue.Remove(entry.Key);
                        //World.DispatchOnPlayerDespawn(entry.Key.Entity);
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}
