using Framework.Contants;
using Framework.Extensions;
using Framework.Network;
using System;

namespace World_Server.Handlers.Auth
{
    class PSInitialSpells : ServerPacket
    {

        public PSInitialSpells() : base(WorldOpcodes.SMSG_INITIAL_SPELLS)
        {
            Write((byte)0);
            Write((short)0);
            Write((short)0);
        }
    }

    class PSActionButtons : ServerPacket
    {
        public PSActionButtons() : base(WorldOpcodes.SMSG_ACTION_BUTTONS)
        {
            for (int button = 0; button < 120; button++)
            {
                Write((uint)0);
            }
        }
    }

    class PSFriendList : ServerPacket
    {

        public PSFriendList() : base(WorldOpcodes.SMSG_FRIEND_LIST)
        {
            Write((byte)0);
        }
    }

    class PSIgnoreList : ServerPacket
    {

        public PSIgnoreList() : base(WorldOpcodes.SMSG_IGNORE_LIST)
        {
            Write((byte)0);
        }
    }
}
