using System;
using System.Windows.Forms;
using Racing.Objects;
using RGEngine;


namespace GameLauncher
{
    public partial class LaunchWindow : Form
    {
        public LaunchWindow()
        {
            InitializeComponent();
        }


        private void StartButton_Click(object sender, EventArgs e)
        {
            using (var racingGame = new EngineCore())
            {
                EngineCore.AddGameObject(new DefaultCar(@"C:\Users\smedy\source\repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\Cars\lambo.png"));

                racingGame.Run();
            }
        }
    }
}
