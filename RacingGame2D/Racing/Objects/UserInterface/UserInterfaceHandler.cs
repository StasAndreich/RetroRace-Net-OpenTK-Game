using OpenTK;
using Racing.Constants;
using Racing.Prizes;
using RGEngine;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Multiplayer;
using RGEngine.Physics;
using System;
using System.Linq;

namespace Racing.Objects.UserInterface
{
    /// <summary>
    /// Defines a control object for the UI.
    /// </summary>
    public class UserInterfaceHandler : GameObject, INonRenderable, INonResolveable
    {
        private const string BlackCarWinnerTexture = @"Contents\UI\Winners\blackWinsText.png";
        private const string PurpleCarWinnerTexture = @"Contents\UI\Winners\purpleWinsText.png";

        private static readonly Vector2 SymbolScale = new Vector2(1.05f, 1.05f);
        private static readonly Vector2 CenterScreenPosition = new Vector2(0f, 0f);

        private int _lastLapsBlack;
        private int _lastLapsPurple;
        private int _lastFuelBlack;
        private int _lastFuelPurple;

        public UserInterfaceHandler()
        {
            SubscribeOnPlayers();

            // Add info ui elements.
            EngineCore.AddGameObject(new TextElement("lapsBlack", new Vector2(-355, -208), new Vector2(1.05f, 1.05f)));
            EngineCore.AddGameObject(new TextElement("lapsPurple", new Vector2(505, -208), new Vector2(1.05f, 1.05f)));
            EngineCore.AddGameObject(new TextElement("fuelBlack", new Vector2(-388, -147), new Vector2(1.05f, 1.05f)));
            EngineCore.AddGameObject(new TextElement("fuelPurple", new Vector2(474, -147), new Vector2(1.05f, 1.05f)));
        }

        /// <summary>
        /// Overrides FixedUpdate with UI update.
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        public override void FixedUpdate(double fixedDeltaTime)
        {
            // Check cars' states.
            foreach (var gameObject in EngineCore.GameObjects)
            {
                if (gameObject is Car car)
                {
                    if (car.Id == CarConstants.BlackCarName)
                    {
                        // Check for changes.
                        if (Math.Abs(_lastLapsBlack - car.LapsPassed) > 0)
                        {
                            _lastLapsBlack = car.LapsPassed;
                            DisplayTextUIElement("lapsBlack", _lastLapsBlack.ToString());
                        }

                        if (Math.Abs(_lastFuelBlack - (int)car.FuelLevel) > 0)
                        {
                            _lastFuelBlack = (int)car.FuelLevel;
                            DisplayTextUIElement("fuelBlack", _lastFuelBlack.ToString());
                        }
                    }
                    else if (car.Id == CarConstants.PurpleCarName)
                    {
                        // Check for changes.
                        if (Math.Abs(_lastLapsPurple - car.LapsPassed) > 0)
                        {
                            _lastLapsPurple = car.LapsPassed;
                            DisplayTextUIElement("lapsPurple", _lastLapsPurple.ToString());
                        }

                        if (Math.Abs(_lastFuelPurple - (int)car.FuelLevel) > 0)
                        {
                            _lastFuelPurple = (int)car.FuelLevel;
                            DisplayTextUIElement("fuelPurple", _lastFuelPurple.ToString());
                        }
                    }
                }
            }
            
            base.FixedUpdate(fixedDeltaTime);
        }

        private void SubscribeOnPlayers()
        {
            foreach (var gameObject in EngineCore.GameObjects)
            {
                if (gameObject is Car car)
                {
                    car.EndedRace += (sender, e) => DisplayWinner(e.Id);
                }
            }
        }

        private void DisplayWinner(string winnerId)
        {
            if (!string.IsNullOrEmpty(winnerId))
            {
                switch (winnerId)
                {
                    case CarConstants.BlackCarName:
                        EngineCore.AddGameObject(new UserInterfaceElement(BlackCarWinnerTexture, CenterScreenPosition));
                        EndGame();
                        break;
                    case CarConstants.PurpleCarName:
                        EngineCore.AddGameObject(new UserInterfaceElement(PurpleCarWinnerTexture, CenterScreenPosition));
                        EndGame();
                        break;
                    default:
                        throw new ApplicationException("There is nothing to display.");
                }
            }
        }

        private void DisplayWinner(GameObject winner)
        {
            if (winner is Car car)
            {
                switch (car.Id)
                {
                    case CarConstants.BlackCarName:
                        EngineCore.AddGameObject(new UserInterfaceElement(BlackCarWinnerTexture, CenterScreenPosition));
                        EndGame();
                        break;
                    case CarConstants.PurpleCarName:
                        EngineCore.AddGameObject(new UserInterfaceElement(PurpleCarWinnerTexture, CenterScreenPosition));
                        EndGame();
                        break;
                    default:
                        throw new ApplicationException("There is nothing to display.");
                }
            }
        }

        private void EndGame()
        {
            foreach (var gameObject in EngineCore.GameObjects.ToList())
            {
                if (gameObject is Car ||
                    gameObject is Prize ||
                    gameObject is PrizeGenerator)
                {
                    EngineCore.RemoveGameObject(gameObject);
                }
            }
            UdpHandlerObject.MessageToSend.IsGameEnded = true;
        }

        private void DisplayTextUIElement(string elementName, string text)
        {
            foreach (var element in EngineCore.GameObjects)
            {
                if (element is TextElement textElement)
                {
                    if (textElement.Name == elementName)
                    {
                        textElement.SpriteRenderer.RenderQueue = TextElement.ParseStringToSprites(text, Position, SymbolScale);
                    }
                }
            }
        }
    }
}
