using OpenTK;
using Racing.Objects.UI;
using RGEngine;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine.Support;
using System;
using System.Linq;

namespace Racing.Objects
{
    public class Environment : GameObject, ICollidable
    {
        public Environment(string backgroundPath)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();

            var bgTexture = ContentLoader.LoadTexture(backgroundPath);
            var bgSprite = new Sprite(bgTexture, new Vector2(1f, 1f),
                new Vector2(0f, 0f), -1);
            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(bgSprite);

            base.Position = new Vector2(0f, 0f);

            // Middle collider.
            base.collider = new PolyCollider(this, new Vector2(1310f, 480f));
            // Add screen bounds.
            EngineCore.AddGameObject(new Bound(new Vector2(-945f, 0f), new Vector2(20f, 1080f)));
            EngineCore.AddGameObject(new Bound(new Vector2(945f, 0f), new Vector2(20f, 1080f)));
            EngineCore.AddGameObject(new Bound(new Vector2(0f, 525f), new Vector2(1920f, 20f)));
            EngineCore.AddGameObject(new Bound(new Vector2(0f, -525f), new Vector2(1920f, 20f)));

            SubscribeOnPlayers();
        }

        private void SubscribeOnPlayers()
        {
            //var list = EngineCore.gameObjects.ToList<GameObject>();
            //foreach (var @object in list)
            //{
            //    if (@object is Car)
            //    {
            //        ((Car)@object).EndedRace += (sender, e) => DisplayWinner(e.@object);
            //    }
            //}
        }

        //private void DisplayWinner(GameObject winner)
        //{
        //    var blackTex = @"C:\Users\smedy\OneDrive\C4D\retro\launcher\UI\WINS\blackWinsText.png";
        //    var purpleTex = @"C:\Users\smedy\OneDrive\C4D\retro\launcher\UI\WINS\purpleWinsText.png";

        //    var car = winner as Car;
        //    if (car != null)
        //    {
        //        switch (car.id)
        //        {
        //            case "Black":
        //                EngineCore.AddGameObject(new UIElement(blackTex, new Vector2(0f, 0f)));
        //                break;
        //            case "Purple":
        //                EngineCore.AddGameObject(new UIElement(purpleTex, new Vector2(0f, 0f)));
        //                break;
        //            default:
        //                throw new Exception("There is nothing to display.");
        //        }
        //    }
        //}

        private class Bound : GameObject, ICollidable, INonRenderable
        {
            public Bound(Vector2 position, Vector2 size)
            {
                base.Position = position;
                base.collider = new PolyCollider(this, size);
            }   
        }

        private SpriteRenderer spriteRenderer;
    }

    public class GameEventArgs : EventArgs
    {
        public readonly GameObject @object;

        public GameEventArgs(GameObject @object)
        {
            this.@object = @object;
        }
    }
}
