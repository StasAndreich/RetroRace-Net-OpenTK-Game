using System;
using System.Windows.Forms;
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
                racingGame.Run();
            }
        }
    }
}
