using System;
using System.Drawing;
using System.Windows.Forms;

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
            var gameMode = new GameMode();
            gameMode.ShowDialog();
        }
    }
}
