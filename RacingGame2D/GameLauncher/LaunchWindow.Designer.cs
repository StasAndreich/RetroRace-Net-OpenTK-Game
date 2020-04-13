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
            this.label_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Start.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label_Start.Location = new System.Drawing.Point(345, 265);
            this.label_Start.Name = "label_Start";
            this.label_Start.Size = new System.Drawing.Size(86, 38);
            this.label_Start.TabIndex = 1;
            this.label_Start.Text = "Start";
            this.label_Start.Click += new System.EventHandler(this.label_Start_Click);
            this.label_Start.MouseEnter += new System.EventHandler(this.label_Start_OnMouseEnter);
            this.label_Start.MouseLeave += new System.EventHandler(this.label_Start_OnMouseLeave);
            // 
            // LaunchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(779, 383);
            this.Controls.Add(this.label_Start);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LaunchWindow";
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

