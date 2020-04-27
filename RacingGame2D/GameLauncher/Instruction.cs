using System;
using System.Drawing;
using System.Windows.Forms;
using Racing.Objects;
using Racing.Objects.UI;
using Racing.Prizes;
using RGEngine;

namespace GameLauncher
{
    public partial class Instruction : Form
    {
        public Instruction()
        {
            InitializeComponent();
        }

        private void Instruction_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void GO_OnMouseEnter(object sender, EventArgs e)
        {
            GO.BackgroundImage = new Bitmap(@"Resources\go_y.png");
        }

        private void GO_OnMouseLeave(object sender, EventArgs e)
        {
            GO.BackgroundImage = new Bitmap(@"Resources\go_w.png");
        }

        private void GO_Click(object sender, EventArgs e)
        {
            this.Hide();

            using (var racingGame = new EngineCore(false))
            {
                EngineCore.AddGameObject(new Racing.Objects.Environment(@"Contents\Environment\bg_ui_v2.png"));
                EngineCore.AddGameObject(new FinishLine());
                EngineCore.AddGameObject(new OuterFinishLine());
                EngineCore.AddGameObject(new PurpleCar());
                EngineCore.AddGameObject(new BlackCar());
                EngineCore.AddGameObject(new PrizeGenerator());
                EngineCore.AddGameObject(new UIHandler());

                racingGame.Title = "Retro Race";
                racingGame.Icon = new Icon(@"Resources\icon32.ico");
                racingGame.WindowBorder = OpenTK.WindowBorder.Fixed;
                racingGame.WindowState = OpenTK.WindowState.Maximized;
                racingGame.Run();
            }

            Application.Exit();
        }
    }
}
