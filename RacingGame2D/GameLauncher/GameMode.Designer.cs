
namespace GameLauncher
{
    partial class GameMode
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
            this.HostButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.SingleButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.IpAddressTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // HostButton
            // 
            this.HostButton.Location = new System.Drawing.Point(9, 34);
            this.HostButton.Name = "HostButton";
            this.HostButton.Size = new System.Drawing.Size(210, 23);
            this.HostButton.TabIndex = 0;
            this.HostButton.Text = "Play as Host";
            this.HostButton.UseVisualStyleBackColor = true;
            this.HostButton.Click += new System.EventHandler(this.Host_Click);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(144, 80);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Connect To";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.Connect_Click);
            // 
            // SingleButton
            // 
            this.SingleButton.Enabled = false;
            this.SingleButton.Location = new System.Drawing.Point(279, 46);
            this.SingleButton.Name = "SingleButton";
            this.SingleButton.Size = new System.Drawing.Size(94, 69);
            this.SingleButton.TabIndex = 2;
            this.SingleButton.Text = "Single Player";
            this.SingleButton.UseVisualStyleBackColor = true;
            this.SingleButton.Click += new System.EventHandler(this.SingleButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.IpAddressTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.HostButton);
            this.groupBox1.Controls.Add(this.ConnectButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 133);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Multiplayer";
            // 
            // IpAddressTextBox
            // 
            this.IpAddressTextBox.Location = new System.Drawing.Point(28, 81);
            this.IpAddressTextBox.Name = "IpAddressTextBox";
            this.IpAddressTextBox.Size = new System.Drawing.Size(109, 20);
            this.IpAddressTextBox.TabIndex = 4;
            this.IpAddressTextBox.Text = "192.168.0.109";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP:";
            // 
            // GameMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(402, 157);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SingleButton);
            this.MaximizeBox = false;
            this.Name = "GameMode";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game mode";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameMode_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button HostButton;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button SingleButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox IpAddressTextBox;
    }
}