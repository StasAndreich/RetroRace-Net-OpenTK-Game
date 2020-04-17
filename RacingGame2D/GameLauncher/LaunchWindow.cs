using System;
using System.Drawing;
using System.Windows.Forms;
using Racing.Objects;
using Racing.Prizes;
using RGEngine;


namespace GameLauncher
{
    public partial class LaunchWindow : Form
    {
        public LaunchWindow()
        {
            InitializeComponent();
        }

        private void LaunchWindow_Load(object sender, EventArgs e)
        {

        }

        private void start_OnMouseEnter(object sender, EventArgs e)
        {
            start.BackgroundImage = new Bitmap(@"C:\Users\smedy\OneDrive\C4D\retro\launcher\start_y.png");
        }

        private void start_OnMouseLeave(object sender, EventArgs e)
        {
            start.BackgroundImage = new Bitmap(@"C:\Users\smedy\OneDrive\C4D\retro\launcher\start_w.png");
        }

        private void start_Click(object sender, EventArgs e)
        {
            start.Enabled = false;

            using (var racingGame = new EngineCore(true))
            {
                //EngineCore.AddGameObject(new Racing.Objects.Environment(@"C:\Users\smedy\Source\Repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\Environment\track.png"));
                EngineCore.AddGameObject(new DefaultCar(@"C:\Users\smedy\source\repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\Cars\lambo.png"));
                //EngineCore.AddGameObject(new PurpleCar());
                EngineCore.AddGameObject(new PrizeGenerator());

                racingGame.Title = "Retro Race";
                racingGame.Icon = new Icon(@"C:\Users\smedy\OneDrive\C4D\retro\launcher\icon32.ico");
                racingGame.WindowBorder = OpenTK.WindowBorder.Fixed;
                racingGame.WindowState = OpenTK.WindowState.Maximized;
                racingGame.Run();
            }

            Application.Exit();
        }
    }
}
