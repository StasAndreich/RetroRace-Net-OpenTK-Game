using OpenTK.Input;

namespace RGEngine.Input
{
    /// <summary>
    /// Describes a controller for user input.
    /// </summary>
    public static class InputController
    {
        /// <summary>
        /// Stores current state of Keyboard object.
        /// </summary>
        public static KeyboardState CurrentKeyboardState { get; private set; }

        /// <summary>
        /// Updates CurrentKeyboardState value.
        /// </summary>
        public static void Update()
        {
            CurrentKeyboardState = Keyboard.GetState();
        }
    }
}
