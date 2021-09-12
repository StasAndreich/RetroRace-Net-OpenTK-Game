using System;
using System.Drawing;
using System.Windows.Forms;
using Racing.Objects;
using Racing.Objects.UI;
using Racing.Prizes;
using RGEngine;

namespace GameLauncher
{
    public partial class GameMode : Form
    {
        private const int Port = 8888;

        public GameMode()
        {
            InitializeComponent();
        }

        private void Host_Click(object sender, EventArgs e)
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

        private void Connect_Click(object sender, EventArgs e)
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

        private void SingleButton_Click(object sender, EventArgs e)
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
