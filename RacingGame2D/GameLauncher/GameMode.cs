using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Racing.Objects;
using Racing.Objects.UserInterface;
using Racing.Prizes;
using RGEngine;
using RGEngine.Multiplayer;

namespace GameLauncher
{
    public partial class GameMode : Form
    {
        private const int ServerPort = 34500;

        public GameMode()
        {
            InitializeComponent();
        }

        private void Host_Click(object sender, EventArgs e)
        {
            Hide();

            var serverThread = new Thread(new ThreadStart(() =>
            {
                Server.Start(ServerPort, 1);
                Server.ServerLoop();
            }));
            serverThread.Start();

            var gameThread = new Thread(new ThreadStart(HostGame));
            gameThread.Start();

            Application.Exit();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            Hide();

            using var racingGame = new EngineCore(false, isMultiplayerEnabled: true);
            EngineCore.AddGameObject(new Racing.Objects.Environment(@"Contents\Environment\bg_ui_v2.png"));
            EngineCore.AddGameObject(new FinishLine());
            EngineCore.AddGameObject(new OuterFinishLine());
            EngineCore.AddGameObject(new PurpleCar(isHost: true));
            EngineCore.AddGameObject(new BlackCar(isHost: false));
            EngineCore.AddGameObject(new PrizeGenerator());
            EngineCore.AddGameObject(new UserInterfaceHandler());

            ConfigureGameWindow(racingGame);
            racingGame.Run();

            Application.Exit();
        }

        private void SingleButton_Click(object sender, EventArgs e)
        {
            Hide();

            using var racingGame = new EngineCore(false, isMultiplayerEnabled: false);
            EngineCore.AddGameObject(new Racing.Objects.Environment(@"Contents\Environment\bg_ui_v2.png"));
            EngineCore.AddGameObject(new FinishLine());
            EngineCore.AddGameObject(new OuterFinishLine());
            EngineCore.AddGameObject(new PurpleCar(isHost: true));
            EngineCore.AddGameObject(new BlackCar(isHost: true));
            EngineCore.AddGameObject(new PrizeGenerator());
            EngineCore.AddGameObject(new UserInterfaceHandler());

            ConfigureGameWindow(racingGame);
            racingGame.Run();

            Application.Exit();
        }

        private void HostGame()
        {
            using var racingGame = new EngineCore(false, isMultiplayerEnabled: true);
            EngineCore.AddGameObject(new Racing.Objects.Environment(@"Contents\Environment\bg_ui_v2.png"));
            EngineCore.AddGameObject(new FinishLine());
            EngineCore.AddGameObject(new OuterFinishLine());
            //EngineCore.AddGameObject(new PurpleCar(isHost: false));
            EngineCore.AddGameObject(new BlackCar(isHost: true));
            EngineCore.AddGameObject(new PrizeGenerator());
            EngineCore.AddGameObject(new UserInterfaceHandler());

            ConfigureGameWindow(racingGame);
            racingGame.Run();
        }

        private void ConfigureGameWindow(EngineCore engineCore)
        {
            engineCore.Title = "Retro Race";
            engineCore.Icon = new Icon(@"Resources\icon32.ico");
            engineCore.WindowBorder = OpenTK.WindowBorder.Fixed;
            engineCore.WindowState = OpenTK.WindowState.Maximized;
        }
    }
}
