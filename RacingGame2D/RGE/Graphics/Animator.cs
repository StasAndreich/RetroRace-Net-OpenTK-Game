using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGEngine.BaseClasses;


namespace RGEngine.Graphics
{
    public sealed class Animator : Component
    {
        public Animator(GameObject gameObject)
            : base(gameObject)
        {
            FramesGrid = new SpriteBatch();
            FramesGrid.OnBatchUpdate += FramesGrid_OnBatchUpdate;
        }

        private void FramesGrid_OnBatchUpdate(object sender, EventArgs e)
        {
            if (FramesGrid.Quantity > 0)
            {
                ienumerator = FramesGrid.GetEnumerator();
                ienumerator.MoveNext();
            }
        }


        public SpriteBatch FramesGrid { get; set; }

        private int fps;
        private double frameLiveTime;
        private IEnumerator<Sprite> ienumerator;


        internal override void PerformComponent(double deltaTime)
        {
            var delay = 1f / fps;
            if (ienumerator != null)
            {
                base.attachedTo.GetComponent<SpriteRenderer>().RenderQueue.RemoveSprite(ienumerator.Current.Texture.Path);
                frameLiveTime += deltaTime;
                if (frameLiveTime >= delay)
                {
                    frameLiveTime = 0;

                    if (!ienumerator.MoveNext())
                    {
                        ienumerator.Reset();
                        ienumerator.MoveNext();
                    }
                }

                base.attachedTo.GetComponent<SpriteRenderer>().RenderQueue.AddSprite(ienumerator.Current);
            }
        }
    }
}
