using Microsoft.Xna.Framework;
using System;

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
    }
}
