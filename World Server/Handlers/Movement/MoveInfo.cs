using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Network;

namespace World_Server.Handlers.Movement
{
    internal static class EnumExtensions
    {
        public static IEnumerable<Enum> GetFlags(this Enum value)
        {
            return GetFlags(value, Enum.GetValues(value.GetType()).Cast<Enum>().ToArray());
        }

        public static IEnumerable<Enum> GetIndividualFlags(this Enum value)
        {
            return GetFlags(value, GetFlagValues(value.GetType()).ToArray());
        }

        private static IEnumerable<Enum> GetFlags(Enum value, Enum[] values)
        {
            var bits = Convert.ToUInt64(value);
            var results = new List<Enum>();
            for (var i = values.Length - 1; i >= 0; i--)
            {
                var mask = Convert.ToUInt64(values[i]);
                if (i == 0 && mask == 0L)
                    break;
                if ((bits & mask) == mask)
                {
                    results.Add(values[i]);
                    bits -= mask;
                }
            }
            if (bits != 0L)
                return Enumerable.Empty<Enum>();
            if (Convert.ToUInt64(value) != 0L)
                return results.Reverse<Enum>();
            if (bits == Convert.ToUInt64(value) && values.Length > 0 && Convert.ToUInt64(values[0]) == 0L)
                return values.Take(1);
            return Enumerable.Empty<Enum>();
        }

        private static IEnumerable<Enum> GetFlagValues(Type enumType)
        {
            ulong flag = 0x1;
            foreach (var value in Enum.GetValues(enumType).Cast<Enum>())
            {
                var bits = Convert.ToUInt64(value);
                if (bits == 0L)
                    //yield return value;
                    continue; // skip the zero value
                while (flag < bits) flag <<= 1;
                if (flag == bits)
                    yield return value;
            }
        }
    }

    [Flags]
    public enum MovementFlags
    {
        MOVEFLAG_NONE = 0x00000000,
        MOVEFLAG_FORWARD = 0x00000001,
        MOVEFLAG_BACKWARD = 0x00000002,
        MOVEFLAG_STRAFE_LEFT = 0x00000004,
        MOVEFLAG_STRAFE_RIGHT = 0x00000008,
        MOVEFLAG_TURN_LEFT = 0x00000010,
        MOVEFLAG_TURN_RIGHT = 0x00000020,
        MOVEFLAG_PITCH_UP = 0x00000040,
        MOVEFLAG_PITCH_DOWN = 0x00000080,
        MOVEFLAG_WALK_MODE = 0x00000100, // Walking

        MOVEFLAG_LEVITATING = 0x00000400,
        MOVEFLAG_ROOT = 0x00000800, // [-ZERO] is it really need and correct value
        MOVEFLAG_FALLING = 0x00002000,
        MOVEFLAG_FALLINGFAR = 0x00004000,
        MOVEFLAG_SWIMMING = 0x00200000, // appears with fly flag also
        MOVEFLAG_ASCENDING = 0x00400000, // [-ZERO] is it really need and correct value
        MOVEFLAG_CAN_FLY = 0x00800000, // [-ZERO] is it really need and correct value
        MOVEFLAG_FLYING = 0x01000000, // [-ZERO] is it really need and correct value

        MOVEFLAG_ONTRANSPORT = 0x02000000, // Used for flying on some creatures
        MOVEFLAG_SPLINE_ELEVATION = 0x04000000, // used for flight paths
        MOVEFLAG_SPLINE_ENABLED = 0x08000000, // used for flight paths
        MOVEFLAG_WATERWALKING = 0x10000000, // prevent unit from falling through water
        MOVEFLAG_SAFE_FALL = 0x20000000, // active rogue safe fall spell (passive)
        MOVEFLAG_HOVER = 0x40000000
    }

    public class MoveInfo : PacketReader
    {
        public MoveInfo(byte[] data) : base(data)
        {
            moveFlags = (MovementFlags)ReadUInt32();
            time = ReadUInt32();
            X = ReadSingle();
            Y = ReadSingle();
            Z = ReadSingle();
            R = ReadSingle();

            if (moveFlags.GetFlags().Contains(MovementFlags.MOVEFLAG_ONTRANSPORT)) //On boat/zeplin
            {
                /*
                Transport.GUID = ReadUInt32();
                Transport.X = ReadSingle();
                Transport.Y = ReadSingle();
                Transport.Z = ReadSingle();
                Transport.R = ReadSingle();
                 */
            }
            if (moveFlags.GetFlags().Contains(MovementFlags.MOVEFLAG_SWIMMING))
                swimPitch = ReadSingle();

            fallTime = ReadUInt32();

            if (moveFlags.GetFlags().Contains(MovementFlags.MOVEFLAG_FALLING))
            {
                /*
                JumpInfo.velocity = ReadSingle();
                JumpInfo.sinAngle = ReadSingle();
                JumpInfo.cosAngle = ReadSingle();
                JumpInfo.xySpeed = ReadSingle();
                 */
            }

            if (moveFlags.GetFlags().Contains(MovementFlags.MOVEFLAG_SPLINE_ELEVATION))
                splineUnknown = ReadSingle();
        }

        public MovementFlags moveFlags { get; set; }
        public uint time { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float R { get; set; }

        public float swimPitch { get; set; }

        public uint fallTime { get; set; }

        public float splineUnknown { get; set; }

        public class Transport
        {
            public float GUID { get; set; }
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
            public float R { get; set; }
        }

        public class JumpInfo
        {
            public float velocity { get; set; }
            public float sinAngle { get; set; }
            public float cosAngle { get; set; }
            public float xySpeed { get; set; }
        }
    }
}