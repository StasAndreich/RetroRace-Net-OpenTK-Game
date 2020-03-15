using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
