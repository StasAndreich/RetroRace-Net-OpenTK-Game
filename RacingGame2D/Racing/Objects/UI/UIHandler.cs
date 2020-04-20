using OpenTK;
using Racing.Prizes;
using RGEngine;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using System;
using System.Linq;

namespace Racing.Objects.UI
{
    public class UIHandler : GameObject, INonRenderable
    {
        public UIHandler()
        {
            SubscribeOnPlayers();
        }


        private void SubscribeOnPlayers()
        {
            var list = EngineCore.gameObjects.ToList<GameObject>();
            foreach (var @object in list)
            {
                if (@object is Car)
                {
                    ((Car)@object).EndedRace += (sender, e) => DisplayWinner(e.@object);
                }
            }
        }

        private void DisplayWinner(GameObject winner)
        {
            var blackTex = @"C:\Users\smedy\OneDrive\C4D\retro\launcher\UI\WINS\blackWinsText.png";
            var purpleTex = @"C:\Users\smedy\OneDrive\C4D\retro\launcher\UI\WINS\purpleWinsText.png";

            var car = winner as Car;
            if (car != null)
            {
                switch (car.id)
                {
                    case "Black":
                        EngineCore.AddGameObject(new UIElement(blackTex, new Vector2(0f, 0f)));
                        EndGame();
                        break;
                    case "Purple":
                        EngineCore.AddGameObject(new UIElement(purpleTex, new Vector2(0f, 0f)));
                        EndGame();
                        break;
                    default:
                        throw new Exception("There is nothing to display.");
                }
            }
        }
        
        private void EndGame()
        {
            foreach (var @object in EngineCore.gameObjects.ToList<GameObject>())
            {
                if (@object is Car ||
                    @object is Prize ||
                    @object is PrizeGenerator)
                {
                    EngineCore.RemoveGameObject(@object);
                }
            }
        }
    }
}
