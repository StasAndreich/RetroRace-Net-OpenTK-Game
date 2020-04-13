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
            this.label_Start = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Start
            // 
            this.label_Start.AutoSize = true;
            this.label_Start.Font = new System.Drawing.Font("EngraversGothic BT", 42F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Start.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label_Start.Location = new System.Drawing.Point(1052, 547);
            this.label_Start.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Start.Name = "label_Start";
            this.label_Start.Size = new System.Drawing.Size(142, 59);
            this.label_Start.TabIndex = 1;
            this.label_Start.Text = "Race";
            this.label_Start.Click += new System.EventHandler(this.label_Start_Click);
            this.label_Start.MouseEnter += new System.EventHandler(this.label_Start_OnMouseEnter);
            this.label_Start.MouseLeave += new System.EventHandler(this.label_Start_OnMouseLeave);
            // 
            // LaunchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.BackgroundImage = global::GameLauncher.Properties.Resources.launcherBG_hd;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.label_Start);
            this.MaximizeBox = false;
            this.Name = "LaunchWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Racing Game Launcher";
            this.Load += new System.EventHandler(this.LaunchWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_Start;
    }
}

