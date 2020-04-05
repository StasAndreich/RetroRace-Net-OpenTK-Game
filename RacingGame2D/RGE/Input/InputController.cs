using System;
using OpenTK.Input;


namespace RGEngine.Input
{
    public static class InputController
    {
        public static KeyboardState CurrentKeyboardState { get; private set; }

        public static void Update()
        {
            CurrentKeyboardState = Keyboard.GetState();
        }
    }
}
