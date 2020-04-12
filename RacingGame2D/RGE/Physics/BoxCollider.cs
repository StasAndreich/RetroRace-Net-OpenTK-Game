using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace RGEngine.Physics
{
    public class BoxCollider : Collider
    {
        public BoxCollider(int width, int height)
            : base(width, height)
        {
            UpdateAABBSurrounding();
        }

        /// <summary>
        /// Updates the state of AABB that surrounds main box collider.
        /// </summary>
        private void UpdateAABBSurrounding()
        {
            var min = new Vector2(float.MaxValue, float.MaxValue);
            var max = new Vector2(float.MinValue, float.MinValue);

            for (int i = 0; i < boundingPoly.Count; i++)
            {
                if (boundingPoly[i].X < min.X || boundingPoly[i].Y < min.Y)
                    min = boundingPoly[i];

                if (boundingPoly[i].X > max.X || boundingPoly[i].Y > max.Y)
                    max = boundingPoly[i];
            }

            base.aabbSurrounding = new AABB(min, max);
            aabbSurrounding.Width = max.X - min.X;
            aabbSurrounding.Heigth = max.Y - min.Y;
        }

        /// <summary>
        /// Returns a vector of normal to some side of a polygon.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <returns></returns>
        private Vector2 GetSideNormal(int startPoint)
        {
            int endPoint = 0;
            if (startPoint + 1 >= boundingPoly.Count)
                endPoint = 0;
            else
                endPoint = startPoint + 1;

            var sideX = boundingPoly[endPoint].X - boundingPoly[startPoint].X;
            var sideY = boundingPoly[endPoint].Y - boundingPoly[startPoint].Y;

            var normal = new Vector2(-sideY, sideX);
            normal.Normalize();
            return normal;
        }

        /// <summary>
        /// Returns a support point of a polygon.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private Vector2 GetSupportPoint(Vector2 direction)
        {
            var bestProjection = float.MinValue;
            var bestPoint = Vector2.Zero;

            for (int i = 0; i < boundingPoly.Count; i++)
            {
                var projection = Vector2.Dot(boundingPoly[i] + rigidBody.attachedTo.Position, direction);
                if (projection > bestProjection)
                {
                    bestProjection = projection;
                    bestPoint = boundingPoly[i] + rigidBody.attachedTo.Position;
                }
            }

            return bestPoint;
        }


        internal override void DetectCollision(Collider other)
        {
            if (aabbSurrounding.AABBvsAABB(other.aabbSurrounding))
            {
                if (other  )
            }
        }


        internal override void ResolveCollision(Collider other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws a collider to the scene.
        /// </summary>
        internal override void Draw()
        {
            GL.Disable(EnableCap.Blend);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();

            GL.Begin(PrimitiveType.LineLoop);
            if (IsTriggered)
                GL.Color3(Color.Red);
            else
                //GL.Color3(Color.LightGreen);

            for (int i = 0; i < boundingPoly.Count; i++)
            {
                GL.Vertex2(boundingPoly[i]);
            }

            GL.End();
            GL.PopMatrix();
            GL.Enable(EnableCap.Blend);
        }

        /// <summary>
        /// Rotates collider by some amount of degrees.
        /// </summary>
        /// <param name="angleInDegrees"></param>
        protected override void Rotate(float angleInDegrees)
        {
            for (int i = 0; i < boundingPoly.Count; i++)
            {
                var tmp = boundingPoly[i];
                tmp.X = (float)(boundingPoly[i].X * Math.Cos(MathHelper.DegreesToRadians(angle)) - points[i].Y * Math.Sin(MathHelper.DegreesToRadians(angle)));
                tmp.Y = (float)(boundingPoly[i].X * Math.Sin(MathHelper.DegreesToRadians(angle)) + points[i].Y * Math.Cos(MathHelper.DegreesToRadians(angle)));
                boundingPoly[i] = tmp;
            }

            UpdateAABBSurrounding();
        }
    }
}
