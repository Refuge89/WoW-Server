using System.Collections.Generic;
using Framework.Contants;
using World_Server.Handlers;
using World_Server.Handlers.Movement;
using World_Server.Sessions;
using static World_Server.Program;

namespace World_Server.Managers
{
    public class MovementManager
    {
        private static readonly List<WorldOpcodes> MOVEMENT_CODES = new List<WorldOpcodes>
        {
            WorldOpcodes.MSG_MOVE_START_TURN_LEFT,
            WorldOpcodes.MSG_MOVE_START_TURN_RIGHT,
            WorldOpcodes.MSG_MOVE_START_STRAFE_LEFT,
            WorldOpcodes.MSG_MOVE_START_STRAFE_RIGHT,
            WorldOpcodes.MSG_MOVE_START_BACKWARD,
            WorldOpcodes.MSG_MOVE_START_FORWARD,
            WorldOpcodes.MSG_MOVE_JUMP,
            WorldOpcodes.MSG_MOVE_SET_FACING,
            WorldOpcodes.MSG_MOVE_STOP,
            WorldOpcodes.MSG_MOVE_STOP_STRAFE,
            WorldOpcodes.MSG_MOVE_STOP_TURN
            /* Opcodes.MSG_MOVE_HEARTBEAT */
        };

        public static void Boot()
        {
            MOVEMENT_CODES.ForEach(code => WorldDataRouter.AddHandler(code, GenerateResponce(code)));

            WorldDataRouter.AddHandler<MoveInfo>(WorldOpcodes.MSG_MOVE_HEARTBEAT, OnHeartBeat);
        }

        private static void OnHeartBeat(WorldSession session, MoveInfo handler)
        {
            session.Character.MapX = handler.X;
            session.Character.MapY = handler.Y;
            session.Character.MapZ = handler.Z;
            session.Character.MapRotation = handler.R;

            Database.UpdateMovement(session.Character);
        }

        private static ProcessWorldPacketCallbackTypes<MoveInfo> GenerateResponce(WorldOpcodes opcode)
        {
            return delegate (WorldSession session, MoveInfo handler) { TransmitMovement(session, handler, opcode); };
        }

        private static void TransmitMovement(WorldSession session, MoveInfo handler, WorldOpcodes code)
        {
            WorldServer.Sessions.FindAll(s => s != session)
                .ForEach(s => s.sendPacket(new PSMovement(code, session.Character, handler)));
        }
    }
}