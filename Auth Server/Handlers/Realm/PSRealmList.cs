using Framework.Contants;
using Framework.Extensions;
using Framework.Network;
using System;

namespace Auth_Server.Handlers.Realm
{
    class PSRealmList : ServerPacket
    {
        public PSRealmList() : base(AuthServerOpCode.REALM_LIST)
        {
            Write((uint)0x0000);

            Write((byte)1);

            Write((uint)RealmType.PVP);                 // Type
            Write((byte)RealmFlag.NewPlayers);          // Flag
            this.WriteCString("Reino Alpha");           // Name World
            this.WriteCString("127.0.0.1:1001");        // IP World
            Write((float)RealmPopulationPreset.Low);   // Pop
            Write((byte)0);                             // Chars
            Write((byte)RealmTimezone.AnyLocale);       // time
            Write((byte)0);                             // time
            
            Write((UInt16)0x0002);
        }
    }
}
