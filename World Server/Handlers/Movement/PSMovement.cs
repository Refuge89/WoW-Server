using Framework.Contants;
using Framework.Database.Tables;
using Framework.Network;
using World_Server.Handlers.World;

namespace World_Server.Handlers.Movement
{
    public class PSMovement : ServerPacket
    {
        public PSMovement(WorldOpcodes opcode, Character character, MoveInfo moveinfo) : base(opcode)
        {
            var packedGUID = PSUpdateObject.GenerateGuidBytes((ulong) character.Id);
            PSUpdateObject.WriteBytes(this, packedGUID);
            Write((uint) moveinfo.moveFlags);
            Write((uint) 1); // Time
            Write(moveinfo.X);
            Write(moveinfo.Y);
            Write(moveinfo.Z);
            Write(moveinfo.R);
            Write((uint) 0); // ?
        }
    }
}
