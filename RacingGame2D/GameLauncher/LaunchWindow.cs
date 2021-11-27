using System;
using System.Drawing;
using System.Windows.Forms;

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
            start.BackgroundImage = new Bitmap(@"Resources\start_y.png");
        }

        private void start_OnMouseLeave(object sender, EventArgs e)
        {
            start.BackgroundImage = new Bitmap(@"Resources\start_w.png");
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.Hide();
            //var popupInstruction = new Instruction();
            //popupInstruction.ShowDialog();
            var gameMode = new GameMode();
            gameMode.ShowDialog();
        }
    }
}
