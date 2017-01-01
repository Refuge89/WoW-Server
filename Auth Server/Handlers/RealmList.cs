using System;
using System.Collections.Generic;
using Framework.Contants;
using Framework.Database.Tables;
using Framework.Extensions;
using Framework.Network;

namespace Auth_Server.Handlers
{
    sealed class PsRealmList : ServerPacket
    {
        public PsRealmList(List<Realms> realms, string username) : base(AuthServerOpcode.REALM_LIST)
        {
            Write((uint)0x0000);
            Write((byte)realms.Count);

            foreach (var realm in realms)
            {
                // criar a função para comparar o valor abaixo com o tipo de populacao
                var realmCount = Program.DatabaseManager.CountRealmCharacter(realm);
                Console.WriteLine(realmCount);

                // recupera contagem de chars do usuario pelo realm
                var charCount = Program.DatabaseManager.CountUserRealmCharacter(username, realm);
                Console.WriteLine(charCount);

                Write((uint)realm.type);            // Type
                Write((byte)realm.flag);            // Flag
                this.WriteCString(realm.name);      // Name World
                this.WriteCString(realm.ip);        // IP World
                Write(RealmPopulationPreset.Low);   // Pop
                Write((byte)0);                     // Chars
                Write((byte)realm.timezone);        // time
                Write((byte)0);                     // time  
            }

            Write((UInt16)0x0002);
        }
    }
}
