using System;


namespace Racing.Prizes
{
    public class FuelPrize : Prize
    {
        public FuelPrize()
            : base(@"C:\Users\smedy\Source\Repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\low-poly-texture.jpg",
            @"C:\Users\smedy\Source\Repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\gold-coin-illustration-png-clip-art.png")
        {
            //base.rigidBody.OnTriggered += FuelPrize_OnTriggered;
        }

        protected override void ApplyDecorator()
        {
            
        }
    }
}
