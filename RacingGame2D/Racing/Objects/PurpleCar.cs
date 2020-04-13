using RGEngine.Input;
using RGEngine.Graphics;
using OpenTK.Input;
using OpenTK;


namespace Racing.Objects
{
    public class PurpleCar : Car
    {
        public PurpleCar()
            : base(@"C:\Users\smedy\source\repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\Cars\lambo.png")
        {
            SetStartCarPosition(new Vector2(300f, 0f));
        }


        public override void FixedUpdate(double fixedDeltaTime)
        {
            UpdateGearboxState(Key.Keypad7, Key.Keypad9);
            GetUserInput(Key.Keypad8, Key.Keypad5, Key.Keypad4, Key.Keypad6);

            base.FixedUpdate(fixedDeltaTime);
        }
    }
}
