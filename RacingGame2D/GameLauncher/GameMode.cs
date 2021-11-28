using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using Racing.Objects;
using Racing.Objects.UserInterface;
using Racing.Prizes;
using RGEngine;
using RGEngine.Multiplayer;
using Environment = Racing.Objects.Environment;

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

            HostGameCallback();

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
                RemotePort = 9999,
                RemoteIPAddress = serverIp
            };

            using var racingGame = new EngineCore(false, multiplayerConfig);
            ConfigureGameWindow(racingGame);
            racingGame.Title = "Retro Race - Client";

            EngineCore.AddGameObject(new UdpMultiplayerController(7777));
            EngineCore.AddGameObject(new Environment(@"Contents\Environment\bg_ui_v2.png"));
            EngineCore.AddGameObject(new FinishLine());
            EngineCore.AddGameObject(new OuterFinishLine());
            EngineCore.AddGameObject(new PurpleCar(true));
            EngineCore.AddGameObject(new BlackCar(false));
            EngineCore.AddGameObject(new PrizeGenerator(false));
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
            //EngineCore.AddGameObject(new PrizeGenerator());
            EngineCore.AddGameObject(new UserInterfaceHandler());

            ConfigureGameWindow(racingGame);
            racingGame.Run();

            Application.Exit();
        }

        private void HostGameCallback()
        {
            var multiplayerConfig = new MultiplayerConfig
            {
                LocalPort = 9999,
                RemotePort = 7777,
                RemoteIPAddress = LocalServerIp
            };

            using var racingGame = new EngineCore(false, multiplayerConfig);
            ConfigureGameWindow(racingGame);
            racingGame.Title = "Retro Race - Host";

            EngineCore.AddGameObject(new UdpMultiplayerController(9999));
            EngineCore.AddGameObject(new Environment(@"Contents\Environment\bg_ui_v2.png"));
            EngineCore.AddGameObject(new FinishLine());
            EngineCore.AddGameObject(new OuterFinishLine());
            EngineCore.AddGameObject(new PurpleCar(false));
            EngineCore.AddGameObject(new BlackCar(true));
            EngineCore.AddGameObject(new PrizeGenerator(true));
            EngineCore.AddGameObject(new UserInterfaceHandler());

            racingGame.Run();
        }

        private void ConfigureGameWindow(EngineCore engineCore)
        {
            engineCore.Icon = new Icon(@"Resources\icon32.ico");
            engineCore.WindowBorder = OpenTK.WindowBorder.Resizable;
            engineCore.WindowState = OpenTK.WindowState.Maximized;
        }
    }
}
