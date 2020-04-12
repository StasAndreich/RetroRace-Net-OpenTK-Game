using RGEngine.Input;
using RGEngine.Graphics;
using OpenTK.Input;
using OpenTK;


namespace Racing.Objects
{
    public class BlackCar : Car
    {
        public BlackCar()
            : base(@"C:\Users\smedy\source\repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\Cars\lambo.png")
        {

        }


        public override void FixedUpdate(double fixedDeltaTime)
        {
            UpdateGearboxState(Key.Q, Key.E);
            GetUserInput(Key.W, Key.S, Key.A, Key.D);

            base.FixedUpdate(fixedDeltaTime);
        }
    }
}
