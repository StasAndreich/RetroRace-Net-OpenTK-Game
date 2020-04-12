using System;
using OpenTK;


namespace RGEngine.Physics
{
    public class BoundingPoly
    {
        private const int pointsAmt = 4;
        private Vector2[] rectVertices;
        private Vector2 rectCenterPosition;     

        public int Count { get; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Vector2 Position
        {
            get => this.rectCenterPosition;
            set
            {
                this.rectCenterPosition = value;
                rectVertices = GetRectVertices(this.Width, this.Height);
            }
        }

        public Vector2 this[int index]
        {
            get => rectVertices[index];
            set => rectVertices[index] = value;
        }


        public BoundingPoly(Vector2 rectCenterPosition, int width, int height)
        {
            this.Count = pointsAmt;
            this.Width = width;
            this.Height = height;
            this.rectCenterPosition = rectCenterPosition;
            rectVertices = GetRectVertices(width, height);
        }


        //public void Rotate(float angleInDegrees)
        //{
        //    var X = this.Position.X;
        //    var Y = this.Position.Y;

        //    Translate(X, Y);

        //    var rotation = MathHelper.DegreesToRadians(angleInDegrees);
        //    for (int i = 0; i < rectVertices.Length; i++)
        //    {
        //        rectVertices[i].X += (int)(Math.Cos(rotation) * rectVertices[i].X -
        //            Math.Sin(rotation) * rectVertices[i].Y);
        //        rectVertices[i].Y += (int)(Math.Sin(rotation) * rectVertices[i].X +
        //            Math.Cos(rotation) * rectVertices[i].Y);
        //    }

        //    Translate(-X, -Y);
        //}

        //public void Translate(float xTranslate, float yTranslate)
        //{
        //    this.Position += new Vector2(xTranslate, yTranslate);
        //}

        private Vector2[] GetRectVertices(int width, int height)
        {
            return new Vector2[pointsAmt]
            {
                new Vector2(rectCenterPosition.X - width / 2, rectCenterPosition.Y + height / 2),
                new Vector2(rectCenterPosition.X + width / 2, rectCenterPosition.Y + height / 2),
                new Vector2(rectCenterPosition.X + width / 2, rectCenterPosition.Y - height / 2),
                new Vector2(rectCenterPosition.X - width / 2, rectCenterPosition.Y - height / 2)
            };
        }
    }
}
