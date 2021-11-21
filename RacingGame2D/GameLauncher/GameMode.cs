using System;
using System.Drawing;
using System.Net;
using System.Diagnostics;
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
        private static readonly IPAddress LocalServerIp = IPAddress.Parse("127.0.0.1");

        public GameMode()
        {
            InitializeComponent();
        }

        private void Host_Click(object sender, EventArgs e)
        {
            Hide();


            ////var gameThread = new Thread(new ThreadStart(HostGameCallback))
            ////{
            ////    Name = "Game"
            ////};
            ////gameThread.Start();

            ////var serverThread = new Thread(new ThreadStart(() =>
            ////{
            ////    Server.Start(ServerPort, 2);
            ////}))
            ////{
            ////    Name = "Server"
            ////};
            ////serverThread.Start();

            HostGameCallback();

            //serverThread.Abort();
            Application.Exit();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            Hide();

            if (!IPAddress.TryParse(IpAddressTextBox.Text, out var serverIp))
            {
                return;
            }

            var multiplayerConfig = new MultiplayerConfig
            {
                LocalPort = 7777,
                RemotePort = ServerPort,
                RemoteIPAddress = serverIp
            };

            using var racingGame = new EngineCore(false, multiplayerConfig);
            ConfigureGameWindow(racingGame);
            racingGame.Title = "Retro Race - Client";

            EngineCore.AddGameObject(new Racing.Objects.Environment(@"Contents\Environment\bg_ui_v2.png"));
            EngineCore.AddGameObject(new FinishLine());
            EngineCore.AddGameObject(new OuterFinishLine());
            EngineCore.AddGameObject(new PurpleCar(true));
            EngineCore.AddGameObject(new BlackCar(false));
            EngineCore.AddGameObject(new PrizeGenerator());
            EngineCore.AddGameObject(new UserInterfaceHandler());

            racingGame.Run();

            Application.Exit();
        }

        private void SingleButton_Click(object sender, EventArgs e)
        {
            Hide();

            using var racingGame = new EngineCore(false);
            ConfigureGameWindow(racingGame);
            racingGame.Title = "Retro Race - Single mode";

            EngineCore.AddGameObject(new Racing.Objects.Environment(@"Contents\Environment\bg_ui_v2.png"));
            EngineCore.AddGameObject(new FinishLine());
            EngineCore.AddGameObject(new OuterFinishLine());
            EngineCore.AddGameObject(new PurpleCar(true));
            EngineCore.AddGameObject(new BlackCar(true));
            EngineCore.AddGameObject(new PrizeGenerator());
            EngineCore.AddGameObject(new UserInterfaceHandler());

            ConfigureGameWindow(racingGame);
            racingGame.Run();

            Application.Exit();
        }

        private void HostGameCallback()
        {
            var multiplayerConfig = new MultiplayerConfig
            {
                LocalPort = 6666,
                RemotePort = ServerPort,
                RemoteIPAddress = LocalServerIp
            };

            using var racingGame = new EngineCore(false, multiplayerConfig);
            ConfigureGameWindow(racingGame);
            racingGame.Title = "Retro Race - Host";

            EngineCore.AddGameObject(new Racing.Objects.Environment(@"Contents\Environment\bg_ui_v2.png"));
            EngineCore.AddGameObject(new FinishLine());
            EngineCore.AddGameObject(new OuterFinishLine());
            EngineCore.AddGameObject(new PurpleCar(false));
            EngineCore.AddGameObject(new BlackCar(true));
            EngineCore.AddGameObject(new PrizeGenerator());
            EngineCore.AddGameObject(new UserInterfaceHandler());

            racingGame.Run();

            ////while (true)
            ////{
            ////    if (EngineCore.IsReadyToStart)
            ////    {
            ////        racingGame.Run();
            ////        break;
            ////    }
            ////}
        }

        private void ConfigureGameWindow(EngineCore engineCore)
        {
            //engineCore.Title = "Retro Race";
            engineCore.Icon = new Icon(@"Resources\icon32.ico");
            engineCore.WindowBorder = OpenTK.WindowBorder.Resizable;
            engineCore.WindowState = OpenTK.WindowState.Maximized;
        }
    }
}
