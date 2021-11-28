using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
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
        private const int HostPort = 9999;
        private const int ClientPort = 7777;

        public GameMode()
        {
            InitializeComponent();
        }

        private void Host_Click(object sender, EventArgs e)
        {
            var multiplayerConfig = new MultiplayerConfig
            {
                LocalPort = 9999,
                RemotePort = 7777,
            };

            UdpMultiplayerController udpController = null;
            try
            {
                udpController = new UdpMultiplayerController(HostPort, IPAddress.Loopback, ClientPort);
            }
            catch (SocketException)
            {
                MessageBox.Show("Host player is already runnning.", "Cannot start a game", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            var isAcceptSuccessful = udpController.TryAcceptHandshake();
            if (isAcceptSuccessful)
            {

            }

            Hide();
            using var racingGame = new EngineCore(false, multiplayerConfig);
            ConfigureGameWindow(racingGame);
            racingGame.Title = "Retro Race - Host";

            EngineCore.AddGameObject(udpController);
            EngineCore.AddGameObject(new Environment(@"Contents\Environment\bg_ui_v2.png"));
            EngineCore.AddGameObject(new FinishLine());
            EngineCore.AddGameObject(new OuterFinishLine());
            EngineCore.AddGameObject(new PurpleCar(false));
            EngineCore.AddGameObject(new BlackCar(true));
            EngineCore.AddGameObject(new PrizeGenerator(true));
            EngineCore.AddGameObject(new UserInterfaceHandler());

            racingGame.Run();

            Application.Exit();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            if (!IPAddress.TryParse(IpAddressTextBox.Text, out var remoteIp))
            {
                MessageBox.Show("IP-address is incorrect.", "Cannot connect to a host", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var multiplayerConfig = new MultiplayerConfig
            {
                LocalPort = 7777,
                RemotePort = 9999,
                RemoteIPAddress = remoteIp
            };

            var udpController = new UdpMultiplayerController(ClientPort, remoteIp, HostPort);
            var isConnectionSuccessful = udpController.TryConnectByHandshake();
            if (!isConnectionSuccessful)
            {
                MessageBox.Show("IP is anavailable for some reason. \nPlease, retry to connect.", "Cannot connect to a host", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            Hide();
            using var racingGame = new EngineCore(false, multiplayerConfig);
            ConfigureGameWindow(racingGame);
            racingGame.Title = "Retro Race - Client";

            EngineCore.AddGameObject(udpController);
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

        private void ConfigureGameWindow(EngineCore engineCore)
        {
            engineCore.Icon = new Icon(@"Resources\icon32.ico");
            engineCore.WindowBorder = OpenTK.WindowBorder.Resizable;
            engineCore.WindowState = OpenTK.WindowState.Maximized;
        }
    }
}
