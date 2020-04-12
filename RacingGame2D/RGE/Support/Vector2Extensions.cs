using OpenTK;
using System;


namespace RGEngine.Support
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Returns the vector rotated by 'phi' radians.
        /// </summary>
        /// <param name="vec2"></param>
        /// <param name="phi"></param>
        /// <returns></returns>
        public static Vector2 Rotated(this Vector2 vec2, float phi)
        {
            var nx = vec2.X * Math.Cos(phi) - vec2.Y * Math.Sin(phi);
            var ny = vec2.X * Math.Sin(phi) + vec2.Y * Math.Cos(phi);

            return new Vector2((float)nx, (float)ny);
        }

        /// <summary>
        /// Returns the vector’s angle in radians with respect to the X axis, or (1, 0) vector.
        /// </summary>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static float Angle(this Vector2 vec2)
        {
            Vector2 axisX = new Vector2(1, 0);
            var angle = (float)Math.Atan2(vec2.X - axisX.X, vec2.Y - axisX.Y);
            
            return angle;
        }
    }
}
