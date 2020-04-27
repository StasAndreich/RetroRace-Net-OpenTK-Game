namespace GameLauncher
{
    partial class LaunchWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaunchWindow));
            this.start = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.start)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.BackColor = System.Drawing.Color.Transparent;
            this.start.BackgroundImage = global::GameLauncher.Properties.Resources.start_w;
            this.start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.start.Location = new System.Drawing.Point(1052, 546);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(191, 60);
            this.start.TabIndex = 2;
            this.start.TabStop = false;
            this.start.Click += new System.EventHandler(this.start_Click);
            this.start.MouseEnter += new System.EventHandler(this.start_OnMouseEnter);
            this.start.MouseLeave += new System.EventHandler(this.start_OnMouseLeave);
            // 
            // LaunchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.BackgroundImage = global::GameLauncher.Properties.Resources.launcherBG_hd_logo;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.start);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LaunchWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Racing Game Launcher";
            this.Load += new System.EventHandler(this.LaunchWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.start)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox start;
    }
}

