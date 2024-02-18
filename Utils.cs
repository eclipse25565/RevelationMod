using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace Revelation
{
    internal static class Utils
    {
        private static double TwoPI = Math.PI * 2;

        public static float AngleTo(this Vector2 origin, Vector2 target)
        {
            var result = Math.Atan2(target.Y, target.X) - Math.Atan2(origin.Y, origin.X);
            if(result < -Math.PI) {
                result += TwoPI;
            }
            else if (result > Math.PI)
            {
                result -= TwoPI;
            }
            return (float)result;
        }

        public static float AngleFrom(this Vector2 target, Vector2 origin)
        {
            return AngleTo(origin, target);
        }

        public static float PackVector2(Vector2 val)
        {
            return (float)Math.Floor(val.Length()) * 8.0f + ((float)Math.Atan2(val.Y, val.X) + 4.0f);
        }

        public static Vector2 UnpackVector2(float val)
        {
            var angle = val % 8.0f;
            var length = (val - angle) / 8.0f;
            angle -= 4.0f;
            return Vector2.UnitX.RotatedBy(angle) * length;
        }
    }
}
