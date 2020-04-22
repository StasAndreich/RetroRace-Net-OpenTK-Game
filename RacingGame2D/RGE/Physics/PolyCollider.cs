using OpenTK;
using RGEngine.BaseClasses;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace RGEngine.Physics
{
    /// <summary>
    /// Describes a polygon collider for an object.
    /// </summary>
    public class PolyCollider
    {
        /// <summary>
        /// Keeps the list of all objects that use collider.
        /// </summary>
        public static List<GameObject> allCollidersAttached = new List<GameObject>();

        private Vector2[] vertices = new Vector2[4];
        private GameObject attachedTo;
        private Vector2 size;

        /// <summary>
        /// Basic ctor that instantiates SIZED collider to an object.
        /// </summary>
        /// <param name="object"></param>
        /// <param name="size"></param>
        public PolyCollider(GameObject @object, Vector2 size)
        {
            if (@object is ICollidable)
            {
                allCollidersAttached.Add(@object);
                this.attachedTo = @object;
                this.size = size;
                Update(attachedTo.Position);
            }
        }

        /// <summary>
        /// Event that raised when collision occured.
        /// </summary>
        public event EventHandler<CollisionEventArgs> ColliderTriggered;

        /// <summary>
        /// Method that invokes a ColliderTriggered ivent.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnColliderTriggered(CollisionEventArgs e)
        {
            var handler = ColliderTriggered;
            handler?.Invoke(this, e);
        }

        private void Update(Vector2 position)
        {
            var dx = size.X / 2f;
            var dy = size.Y / 2f;

            vertices[0] = new Vector2(position.X - dx, position.Y - dy);
            vertices[1] = new Vector2(position.X - dx, position.Y + dy);
            vertices[2] = new Vector2(position.X + dx, position.Y + dy);
            vertices[3] = new Vector2(position.X + dx, position.Y - dy);
        }

        private Vector2 GetNormal(Vector2[] vertices, int startPoint)
        {
            int endPoint;
            if (startPoint + 1 >= vertices.Length)
                endPoint = 0;
            else
                endPoint = startPoint + 1;

            var vertexA = vertices[startPoint];
            var vertexB = vertices[endPoint];

            var edge = new Vector2(vertexB.X - vertexA.X, vertexB.Y - vertexA.Y);

            return new Vector2(-edge.Y, edge.X);
        }

        private Vector2 GetProjection(Vector2 normal)
        {
            var result = new Vector2();
            bool isNull = true;

            foreach (var vertex in this.vertices)
            {
                var projection = normal.X * vertex.X + normal.Y * vertex.Y;

                if (isNull)
                {
                    result = new Vector2(projection, projection);
                    isNull = false;
                }

                if (projection > result.X)
                    result.X = projection;

                if (projection < result.Y)
                    result.Y = projection;
            }

            return result;
        }

        /// <summary>
        /// Method that checks a collision between this and other collider.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool DetectCollision(GameObject other)
        {
            PolyCollider otherCollider = other.collider;
            
            Update(this.attachedTo.Position);
            otherCollider.Update(otherCollider.attachedTo.Position);

            Rotate(attachedTo.Rotation);

            int allCount = this.vertices.Length + otherCollider.vertices.Length;
            var allVertices = new Vector2[allCount];
            this.vertices.CopyTo(allVertices, 0);
            otherCollider.vertices.CopyTo(allVertices, this.vertices.Length);

            Vector2 normal;
            bool isCollide = false;

            for (int i = 0; i < allCount && !isCollide; i++)
            {
                normal = GetNormal(allVertices, i);

                var currentProjection = GetProjection(normal);
                var otherProjection = otherCollider.GetProjection(normal);

                if (currentProjection.X < otherProjection.Y ||
                    otherProjection.X < currentProjection.Y)
                    return false;
            }

            OnColliderTriggered(new CollisionEventArgs(this.attachedTo, other));

            return true;
        }

        /// <summary>
        /// Rotates collider by some amount of degrees.
        /// </summary>
        /// <param name="angleInDegrees"></param>
        public void Rotate(float angleInDegrees)
        {
            var angle = MathHelper.DegreesToRadians(angleInDegrees);
            Update(Vector2.Zero);

            for (int i = 0; i < vertices.Length; i++)
            {
                var tmp = vertices[i];
                tmp.X = (float)(vertices[i].X * Math.Cos(angle) - vertices[i].Y * Math.Sin(angle));
                tmp.Y = (float)(vertices[i].X * Math.Sin(angle) + vertices[i].Y * Math.Cos(angle));
                vertices[i] = tmp + this.attachedTo.Position;
            }
        }

        /// <summary>
        /// Draws a collider to the scene.
        /// </summary>
        public void Draw()
        {
            GL.Disable(EnableCap.Blend);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();

            GL.Begin(PrimitiveType.LineLoop);

            for (int i = 0; i < vertices.Length; i++)
            {
                GL.Vertex2(vertices[i]);
            }

            GL.End();
            GL.PopMatrix();
            GL.Enable(EnableCap.Blend);
        }
    }

    /// <summary>
    /// Interface for an object that supports collisions.
    /// </summary>
    public interface ICollidable { }

    /// <summary>
    /// Interface for an object that does not need collision resolution.
    /// </summary>
    public interface INonResolveable { }

    /// <summary>
    /// Event args for collision events.
    /// </summary>
    public class CollisionEventArgs : EventArgs
    {
        /// <summary>
        /// First collided object.
        /// </summary>
        public readonly GameObject one;
        /// <summary>
        /// Second collided object.
        /// </summary>
        public readonly GameObject another;

        /// <summary>
        /// Basic ctor that init objects that collided.
        /// </summary>
        /// <param name="one"></param>
        /// <param name="another"></param>
        public CollisionEventArgs(GameObject one, GameObject another)
        {
            this.one = one;
            this.another = another;
        }
    }
}
