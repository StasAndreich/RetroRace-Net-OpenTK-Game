using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace RGEngine.Physics
{
    public class BoxCollider : Collider
    {
        public BoxCollider(int width, int height) : base(width, height)
        {

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

        internal override void Draw()
        {
            GL.Disable(EnableCap.Blend);
            GL.MatrixMode(MatrixMode.Modelview);

            GL.Begin(PrimitiveType.LineLoop);
            if (IsTriggered)
                GL.Color3(Color.Red);
            else
                GL.Color3(Color.LightGreen);

            for (int i = 0; i < boundingPoly.Count; i++)
            {
                GL.Vertex2(boundingPoly[i]);
            }

            GL.End();
            GL.Enable(EnableCap.Blend);
        }
    }
}
