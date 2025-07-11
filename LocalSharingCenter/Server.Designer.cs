namespace LocalSharingCenter
{
    partial class Server
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server));
            this.ServerLogin = new System.Windows.Forms.Panel();
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.LoginButton = new System.Windows.Forms.Button();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.UserNameBox = new System.Windows.Forms.TextBox();
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ServerLogsLabel = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ServerShutdownButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ServerLogs = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ClientsList = new System.Windows.Forms.ListBox();
            this.ConnectedUsersLabel = new System.Windows.Forms.Label();
            this.ServerLabel = new System.Windows.Forms.Label();
            this.ServerLogin.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ServerLogin
            // 
            this.ServerLogin.Controls.Add(this.LogBox);
            this.ServerLogin.Controls.Add(this.panel4);
            this.ServerLogin.Controls.Add(this.LoginButton);
            this.ServerLogin.Controls.Add(this.PasswordLabel);
            this.ServerLogin.Controls.Add(this.UserNameLabel);
            this.ServerLogin.Controls.Add(this.PasswordBox);
            this.ServerLogin.Controls.Add(this.UserNameBox);
            this.ServerLogin.Controls.Add(this.WelcomeLabel);
            this.ServerLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerLogin.Location = new System.Drawing.Point(1, 0);
            this.ServerLogin.Margin = new System.Windows.Forms.Padding(4);
            this.ServerLogin.Name = "ServerLogin";
            this.ServerLogin.Size = new System.Drawing.Size(1066, 554);
            this.ServerLogin.TabIndex = 8;
            this.ServerLogin.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // LogBox
            // 
            this.LogBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.LogBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.LogBox.Location = new System.Drawing.Point(405, 438);
            this.LogBox.Margin = new System.Windows.Forms.Padding(4);
            this.LogBox.Name = "LogBox";
            this.LogBox.Size = new System.Drawing.Size(228, 41);
            this.LogBox.TabIndex = 15;
            this.LogBox.Text = "";
            this.LogBox.TextChanged += new System.EventHandler(this.LogBox_TextChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel4.Location = new System.Drawing.Point(121, 129);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(823, 1);
            this.panel4.TabIndex = 14;
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.LoginButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.LoginButton.Location = new System.Drawing.Point(405, 311);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(4);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(237, 60);
            this.LoginButton.TabIndex = 13;
            this.LoginButton.Text = "Login as Admin";
            this.LoginButton.UseVisualStyleBackColor = false;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.PasswordLabel.Location = new System.Drawing.Point(317, 247);
            this.PasswordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(97, 28);
            this.PasswordLabel.TabIndex = 12;
            this.PasswordLabel.Text = "Password:";
            this.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.PasswordLabel.Click += new System.EventHandler(this.PasswordLabel_Click);
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.UserNameLabel.Location = new System.Drawing.Point(311, 183);
            this.UserNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(103, 28);
            this.UserNameLabel.TabIndex = 11;
            this.UserNameLabel.Text = "Username:";
            this.UserNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UserNameLabel.Click += new System.EventHandler(this.UserNameLabel_Click);
            // 
            // PasswordBox
            // 
            this.PasswordBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.PasswordBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PasswordBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.PasswordBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.PasswordBox.Location = new System.Drawing.Point(444, 247);
            this.PasswordBox.Margin = new System.Windows.Forms.Padding(4);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '*';
            this.PasswordBox.Size = new System.Drawing.Size(221, 26);
            this.PasswordBox.TabIndex = 10;
            this.PasswordBox.TextChanged += new System.EventHandler(this.PasswordBox_TextChanged);
            // 
            // UserNameBox
            // 
            this.UserNameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.UserNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserNameBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.UserNameBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.UserNameBox.Location = new System.Drawing.Point(444, 183);
            this.UserNameBox.Margin = new System.Windows.Forms.Padding(4);
            this.UserNameBox.Name = "UserNameBox";
            this.UserNameBox.Size = new System.Drawing.Size(221, 26);
            this.UserNameBox.TabIndex = 9;
            this.UserNameBox.TextChanged += new System.EventHandler(this.UserNameBox_TextChanged);
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.AutoSize = true;
            this.WelcomeLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.WelcomeLabel.Location = new System.Drawing.Point(399, 75);
            this.WelcomeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(203, 37);
            this.WelcomeLabel.TabIndex = 8;
            this.WelcomeLabel.Text = "SERVER LOGIN";
            this.WelcomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WelcomeLabel.Click += new System.EventHandler(this.WelcomeLabel_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ServerLogsLabel);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.ServerShutdownButton);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.ConnectedUsersLabel);
            this.panel3.Controls.Add(this.ServerLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1066, 554);
            this.panel3.TabIndex = 9;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint_1);
            // 
            // ServerLogsLabel
            // 
            this.ServerLogsLabel.AutoSize = true;
            this.ServerLogsLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ServerLogsLabel.Location = new System.Drawing.Point(417, 100);
            this.ServerLogsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ServerLogsLabel.Name = "ServerLogsLabel";
            this.ServerLogsLabel.Size = new System.Drawing.Size(117, 28);
            this.ServerLogsLabel.TabIndex = 16;
            this.ServerLogsLabel.Text = "Server Logs:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel5.Location = new System.Drawing.Point(121, 85);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(823, 1);
            this.panel5.TabIndex = 15;
            // 
            // ServerShutdownButton
            // 
            this.ServerShutdownButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ServerShutdownButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ServerShutdownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ServerShutdownButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ServerShutdownButton.Location = new System.Drawing.Point(425, 428);
            this.ServerShutdownButton.Margin = new System.Windows.Forms.Padding(4);
            this.ServerShutdownButton.Name = "ServerShutdownButton";
            this.ServerShutdownButton.Size = new System.Drawing.Size(168, 81);
            this.ServerShutdownButton.TabIndex = 12;
            this.ServerShutdownButton.Text = "Shutdown Server";
            this.ServerShutdownButton.UseVisualStyleBackColor = false;
            this.ServerShutdownButton.Click += new System.EventHandler(this.ServerShutdownButton_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel2.Controls.Add(this.ServerLogs);
            this.panel2.Location = new System.Drawing.Point(423, 134);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Size = new System.Drawing.Size(423, 267);
            this.panel2.TabIndex = 11;
            // 
            // ServerLogs
            // 
            this.ServerLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ServerLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ServerLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ServerLogs.Location = new System.Drawing.Point(3, 2);
            this.ServerLogs.Margin = new System.Windows.Forms.Padding(4);
            this.ServerLogs.Name = "ServerLogs";
            this.ServerLogs.Size = new System.Drawing.Size(417, 263);
            this.ServerLogs.TabIndex = 0;
            this.ServerLogs.Text = "";
            this.ServerLogs.TextChanged += new System.EventHandler(this.ServerLogs_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.ClientsList);
            this.panel1.Location = new System.Drawing.Point(224, 175);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Size = new System.Drawing.Size(168, 229);
            this.panel1.TabIndex = 10;
            // 
            // ClientsList
            // 
            this.ClientsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ClientsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientsList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientsList.FormattingEnabled = true;
            this.ClientsList.ItemHeight = 16;
            this.ClientsList.Location = new System.Drawing.Point(3, 2);
            this.ClientsList.Margin = new System.Windows.Forms.Padding(4);
            this.ClientsList.Name = "ClientsList";
            this.ClientsList.Size = new System.Drawing.Size(162, 225);
            this.ClientsList.TabIndex = 2;
            this.ClientsList.SelectedIndexChanged += new System.EventHandler(this.ClientsList_SelectedIndexChanged);
            // 
            // ConnectedUsersLabel
            // 
            this.ConnectedUsersLabel.AutoSize = true;
            this.ConnectedUsersLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ConnectedUsersLabel.Location = new System.Drawing.Point(220, 132);
            this.ConnectedUsersLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ConnectedUsersLabel.Name = "ConnectedUsersLabel";
            this.ConnectedUsersLabel.Size = new System.Drawing.Size(162, 28);
            this.ConnectedUsersLabel.TabIndex = 9;
            this.ConnectedUsersLabel.Text = "Connected Users:";
            this.ConnectedUsersLabel.Click += new System.EventHandler(this.ConnectedUsersLabel_Click);
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ServerLabel.Location = new System.Drawing.Point(369, 44);
            this.ServerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(306, 37);
            this.ServerLabel.TabIndex = 8;
            this.ServerLabel.Text = "SERVER ADMIN PANEL";
            this.ServerLabel.Click += new System.EventHandler(this.ServerLabel_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.ServerLogin);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Server";
            this.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Server_Load);
            this.ServerLogin.ResumeLayout(false);
            this.ServerLogin.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel ServerLogin;
        private System.Windows.Forms.RichTextBox LogBox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.TextBox UserNameBox;
        private System.Windows.Forms.Label WelcomeLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button ServerShutdownButton;
        private System.Windows.Forms.ListBox ClientsList;
        private System.Windows.Forms.Label ConnectedUsersLabel;
        private System.Windows.Forms.Label ServerLabel;
        private System.Windows.Forms.RichTextBox ServerLogs;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ServerLogsLabel;
        private System.Windows.Forms.Panel panel5;
    }
}