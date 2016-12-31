using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;
using World_Server.Handlers.World;

namespace World_Server.Handlers.Movement
{
    public class PSMoveHeartbeat : ServerPacket
    {
        public PSMoveHeartbeat(Character character) : base(WorldOpcodes.MSG_MOVE_HEARTBEAT)
        {
            var packedGUID = PSUpdateObject.GenerateGuidBytes((ulong)character.Id);
            PSUpdateObject.WriteBytes(this, packedGUID);
            Write((uint)MovementFlags.MOVEFLAG_NONE);
            Write((uint)1); // Time
            Write(character.MapX);
            Write(character.MapY);
            Write(character.MapZ);
            Write(character.MapRotation);
            Write((uint)0); // ?
        }
    }
}