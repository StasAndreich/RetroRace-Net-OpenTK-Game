using OpenTK;
using RGEngine.BaseClasses;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;


namespace RGEngine.Physics
{
    public class PolyCollider
    {
        public static List<GameObject> allCollidersAttached = new List<GameObject>();

        public Vector2[] vertices = new Vector2[4];

        private GameObject attachedTo;
        private Vector2 size;

        public PolyCollider(GameObject @object, Vector2 size)
        {
            allCollidersAttached.Add(@object);
            this.attachedTo = @object;
            this.size = size;
        }


        private void Update()
        {
            var position = this.attachedTo.Position;

            var dx = size.X / 2f;
            var dy = size.Y / 2f;

            vertices[0] = new Vector2(position.X - dx, position.Y - dy);
            vertices[1] = new Vector2(position.X - dx, position.Y + dy);
            vertices[2] = new Vector2(position.X + dx, position.Y + dy);
            vertices[3] = new Vector2(position.X + dx, position.Y - dy);
        }

        private Vector2 GetNormal(Vector2[] vertices, int startPoint)
        {
            int endPoint = 0;
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

        public bool DetectCollision(GameObject other)
        {
            PolyCollider otherCollider = other.collider;
            
            Update();
            otherCollider.Update();


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

            return true;
        }

        /// <summary>
        /// Rotates collider by some amount of degrees.
        /// </summary>
        /// <param name="angleInDegrees"></param>
        private void Rotate(float angleInDegrees)
        {
            var angle = MathHelper.DegreesToRadians(angleInDegrees);
            for (int i = 0; i < vertices.Length; i++)
            {
                var tmp = vertices[i];
                tmp.X = (float)(vertices[i].X * Math.Cos(angle) - vertices[i].Y * Math.Sin(angle));
                tmp.X = (float)(vertices[i].X * Math.Sin(angle) + vertices[i].Y * Math.Cos(angle));
            }

            //Update();
        }

        private void OnGameObjRotate(object sender, GameEventArgs e)
        {
            Rotate(e.rotation);
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
            //if (IsTriggered)
            //    GL.Color3(Color.Red);
            //else
            //    GL.Color3(Color.LightGreen);

            for (int i = 0; i < vertices.Length; i++)
            {
                GL.Vertex2(vertices[i]);
            }

            GL.End();
            GL.PopMatrix();
            GL.Enable(EnableCap.Blend);
        }
    }

    public class GameEventArgs : EventArgs
    {
        public readonly Collider other;
        public readonly float rotation;

        public GameEventArgs(float rotation)
        {
            //this.other = other;
            this.rotation = rotation;
        }
    }
}
