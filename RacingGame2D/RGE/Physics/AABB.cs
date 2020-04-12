using OpenTK;


namespace RGEngine.Physics
{
    /// <summary>
    /// Defines a simple Axis Aligned Bounding Box class.
    /// </summary>
    internal sealed class AABB
    {
        internal Vector2 min;
        internal Vector2 max;

        internal float Width { get; set; }
        internal float Heigth { get; set; }

        internal AABB(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// Collision detection between AABB's.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        internal bool AABBvsAABB(AABB other)
        {
            if (max.X < other.min.X || min.X > other.max.X)
                return false;
            if (max.Y < other.min.Y || min.Y > other.max.Y)
                return false;

            return true;
        }
    }
}
