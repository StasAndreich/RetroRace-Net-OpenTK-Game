using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace RGEngine.Physics
{
    public class BoxCollider : Collider
    {
        /// <summary>
        /// Define possible resolving distances for the case of collision.
        /// </summary>
        private float resolvingDistanceA = 0;
        private float resolvingDistanceB = 0;

        /// <summary>
        /// Define possible resolving vectors for the case of collision.
        /// </summary>
        private Vector2 resolvingDirecionA = Vector2.Zero;
        private Vector2 resolvingDirecionB = Vector2.Zero;


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

        /// <summary>
        /// Finds largest distance between support point
        /// and plane of separating axis.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="resolvingDirection"></param>
        /// <returns></returns>
        internal float FindLargestCollisionDistance(BoxCollider other, out Vector2 resolvingDirection)
        {
            var largestDistance = float.MinValue;
            // Associated index of a side face.
            var index = -1;

            for (int i = 0; i < boundingPoly.Count; i++)
            {
                // Get a normal to a bounding poly side.
                var sideNormal = GetSideNormal(i);
                // Get support point in the opposite direction of side normal.
                var supportPoint = GetSupportPoint(-sideNormal);
                float distance = Vector2.Dot(sideNormal, supportPoint - boundingPoly[i] - rigidBody.attachedTo.Position);

                if (distance > largestDistance)
                {
                    largestDistance = distance;
                    index = i;
                }
            }

            // Get vector2 for collision resolution.
            resolvingDirection = GetSideNormal(index);

            return largestDistance;
        }

        /// <summary>
        /// Tests the overlap between box colliders.
        /// </summary>
        /// <param name="other"></param>
        internal override bool DetectCollision(Collider other)
        {
            if (aabbSurrounding.AABBvsAABB(other.aabbSurrounding))
            {
                var boxCollider = other as BoxCollider;
                if (boxCollider != null)
                {
                    // Find separating axis.
                    // If distance is positive, axis found (no collision).
                    // If distance is negative, axis not found (collision occured).

                    var a = FindLargestCollisionDistance(boxCollider, out var dirA);
                    if (a > 0f)
                        return false;

                    var b = FindLargestCollisionDistance(boxCollider, out var dirB);
                    if (b > 0f)
                        return false;

                    this.resolvingDistanceA = a;
                    this.resolvingDistanceB = b;
                    this.resolvingDirecionA = dirA;
                    this.resolvingDirecionB = dirB;

                    rigidBody.IsTriggeredNotify(boxCollider);
                    boxCollider.rigidBody.IsTriggeredNotify(this);

                    return true;
                }
                else
                    throw new ApplicationException("Incorrect collider type.");
            }
            //// If no collision detected.
            //IsTriggered = false;

            // Reset resolving params if no collision occured.
            this.resolvingDistanceA = 0;
            this.resolvingDistanceB = 0;
            this.resolvingDirecionA = Vector2.Zero;
            this.resolvingDirecionB = Vector2.Zero;

            return false;
        }

        /// <summary>
        /// Resolves collisions if there is some.
        /// </summary>
        /// <param name="other"></param>
        internal override void ResolveCollision(Collider other)
        {
            rigidBody.attachedTo.Position += -this.resolvingDirecionB
                * Math.Max(this.resolvingDistanceA, this.resolvingDistanceB);

            //if (resolvingDirecionA.X != 0 && resolvingDirecionB.Y != 0)
            //    rigidBody.velocity = 0f;


            var boxCollider = other as BoxCollider;
            if (boxCollider != null)
            {
                boxCollider.rigidBody.attachedTo.Position += -this.resolvingDirecionA
                    * Math.Max(this.resolvingDistanceA, this.resolvingDistanceB);

                //if (resolvingDirecionB.X != 0 && resolvingDirecionB.Y != 0)
                //    boxCollider.rigidBody.velocity = 0f;
            }
                
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
                var angleInRads = MathHelper.DegreesToRadians(angleInDegrees);

                var tmp = boundingPoly[i];
                tmp.X = (float)(boundingPoly[i].X * Math.Cos(angleInRads) - boundingPoly[i].Y * Math.Sin(angleInRads));
                tmp.Y = (float)(boundingPoly[i].X * Math.Sin(angleInRads) + boundingPoly[i].Y * Math.Cos(angleInRads));
                boundingPoly[i] = tmp;
            }

            UpdateAABBSurrounding();
        }
    }
}