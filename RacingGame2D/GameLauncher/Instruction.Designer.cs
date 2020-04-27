namespace GameLauncher
{
    partial class Instruction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Instruction));
            this.GO = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.GO)).BeginInit();
            this.SuspendLayout();
            // 
            // GO
            // 
            this.GO.BackColor = System.Drawing.Color.Transparent;
            this.GO.BackgroundImage = global::GameLauncher.Properties.Resources.go_w;
            this.GO.ErrorImage = null;
            this.GO.InitialImage = null;
            this.GO.Location = new System.Drawing.Point(1104, 599);
            this.GO.Name = "GO";
            this.GO.Size = new System.Drawing.Size(100, 50);
            this.GO.TabIndex = 0;
            this.GO.TabStop = false;
            this.GO.Click += new System.EventHandler(this.GO_Click);
            this.GO.MouseEnter += new System.EventHandler(this.GO_OnMouseEnter);
            this.GO.MouseLeave += new System.EventHandler(this.GO_OnMouseLeave);
            // 
            // Instruction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GameLauncher.Properties.Resources.Instruction;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.GO);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Instruction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Instruction";
            this.Load += new System.EventHandler(this.Instruction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox GO;
    }
}