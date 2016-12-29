﻿using Framework.Contants;
using Framework.Network;

namespace World_Server.Handlers.World
{
    public class LoginVerifyWorld : ServerPacket
    {
        public LoginVerifyWorld(int mapID, float X, float Y, float Z, float Rotation) : base(WorldOpcodes.SMSG_LOGIN_VERIFY_WORLD)
        {
            Write(mapID);
            Write(X);
            Write(Y);
            Write(Z);
            Write(Rotation); // orientation
        }
    }
}
