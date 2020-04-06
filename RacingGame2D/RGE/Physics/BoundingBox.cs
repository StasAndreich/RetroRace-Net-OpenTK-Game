using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RGEngine.Physics
{
    public class BoundingBox
    {
        public Vector2 Position { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        private Vector2 centerPoint;
        private Vector2[] rectVertices;

        public BoundingBox(Vector2 centerPointPosition, int width, int height)
        {
            centerPoint = centerPointPosition;

            rectVertices = new Vector2[4]
            {
                new Vector2(centerPoint.X - width / 2, centerPoint.Y + height / 2),
                new Vector2(centerPoint.X + width / 2, centerPoint.Y + height / 2),
                new Vector2(centerPoint.X + width / 2, centerPoint.Y - height / 2),
                new Vector2(centerPoint.X - width / 2, centerPoint.Y - height / 2)
            };
        }

        public void Rotate(float angleInDegrees)
        {
            for (int i = 0; i < rectVertices.Length; i++)
            {
                rectVertices[i].X += (int)(Math.Cos(angleInDegrees) * rectVertices[i].X -
                    Math.Sin(angleInDegrees) * rectVertices[i].Y);
                rectVertices[i].Y += (int)(Math.Sin(angleInDegrees) * rectVertices[i].X +
                    Math.Cos(angleInDegrees) * rectVertices[i].Y);
            }
        }
    }
}
