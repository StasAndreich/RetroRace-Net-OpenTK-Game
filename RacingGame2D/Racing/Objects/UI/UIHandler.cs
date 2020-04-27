using OpenTK;
using Racing.Prizes;
using RGEngine;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Physics;
using System;
using System.Linq;

namespace Racing.Objects.UI
{
    /// <summary>
    /// Defines a control object for the UI.
    /// </summary>
    public class UIHandler : GameObject, INonRenderable, INonResolveable
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public UIHandler()
        {
            SubscribeOnPlayers();

            // Add info ui elements.
            EngineCore.AddGameObject(new UIText("lapsBlack", new Vector2(-355, -208), new Vector2(1.05f, 1.05f)));
            EngineCore.AddGameObject(new UIText("lapsPurple", new Vector2(505, -208), new Vector2(1.05f, 1.05f)));
            EngineCore.AddGameObject(new UIText("fuelBlack", new Vector2(-388, -147), new Vector2(1.05f, 1.05f)));
            EngineCore.AddGameObject(new UIText("fuelPurple", new Vector2(474, -147), new Vector2(1.05f, 1.05f)));
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
            var blackTex = @"Contents\UI\Winners\blackWinsText.png";
            var purpleTex = @"Contents\UI\Winners\purpleWinsText.png";

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


        int lastLapsBlack;
        int lastLapsPurple;
        int lastFuelBlack;
        int lastFuelPurple;


        /// <summary>
        /// Overrides FixedUpdate with UI update.
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        public override void FixedUpdate(double fixedDeltaTime)
        {
            // Check cars' states.
            foreach (var @object in EngineCore.gameObjects)
            {
                if (@object is Car)
                {
                    if (((Car)@object).id == "Black")
                    {
                        // Check for changes.
                        if (Math.Abs(lastLapsBlack - ((Car)@object).LapsPassed) > 0)
                        {
                            this.lastLapsBlack = ((Car)@object).LapsPassed;
                            DisplayTextUIElement("lapsBlack", lastLapsBlack.ToString());
                        }

                        if (Math.Abs(lastFuelBlack - (int)((Car)@object).FuelLevel) > 0)
                        {
                            this.lastFuelBlack = (int)((Car)@object).FuelLevel;
                            DisplayTextUIElement("fuelBlack", ((int)lastFuelBlack).ToString());
                        }
                    }
                    else if (((Car)@object).id == "Purple")
                    {
                        // Check for changes.
                        if (Math.Abs(lastLapsPurple - ((Car)@object).LapsPassed) > 0)
                        {
                            this.lastLapsPurple = ((Car)@object).LapsPassed;
                            DisplayTextUIElement("lapsPurple", lastLapsPurple.ToString());
                        }

                        if (Math.Abs(lastFuelPurple - (int)((Car)@object).FuelLevel) > 0)
                        {
                            this.lastFuelPurple = (int)((Car)@object).FuelLevel;
                            DisplayTextUIElement("fuelPurple", ((int)lastFuelPurple).ToString());
                        }
                    }
                }
            }
            
            base.FixedUpdate(fixedDeltaTime);
        }

        private void DisplayTextUIElement(string elementName, string text)
        {
            foreach (var element in EngineCore.gameObjects)
            {
                if (element is UIText)
                {
                    if (((UIText)element).name == elementName)
                    {
                        ((UIText)element).spriteRenderer.RenderQueue =
                            UIText.ParseStringToSprites(text, base.Position, new Vector2(1.05f, 1.05f));
                    }
                }
            }
        }
    }
}
