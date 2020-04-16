using System;
using System.Collections.Generic;
using RGEngine.BaseClasses;


namespace RGEngine.Graphics
{
    public sealed class Animator : Component
    {
        public Animator(GameObject gameObject)
            : base(gameObject)
        {
            AnimationSprites = new SpriteBatch();
            AnimationSprites.BatchUpdated += AnimationSprites_OnBatchUpdated;
        }

        private void AnimationSprites_OnBatchUpdated(object sender, EventArgs e)
        {
            if (AnimationSprites.Quantity > 0)
            {
                ienumerator = AnimationSprites.GetEnumerator();
                ienumerator.MoveNext();
            }
        }


        public SpriteBatch AnimationSprites { get; set; }

        public int FPS { get; set; }
        private double frameLiveTime;
        private IEnumerator<Sprite> ienumerator;


        internal override void PerformComponent(double deltaTime)
        {
            var delay = 1f / FPS;
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
