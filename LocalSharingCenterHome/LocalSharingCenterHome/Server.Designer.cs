namespace LocalSharingCenterHome
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
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(799, 450);
            this.panel3.TabIndex = 9;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint_1);
            // 
            // ServerLogsLabel
            // 
            this.ServerLogsLabel.AutoSize = true;
            this.ServerLogsLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ServerLogsLabel.Location = new System.Drawing.Point(313, 81);
            this.ServerLogsLabel.Name = "ServerLogsLabel";
            this.ServerLogsLabel.Size = new System.Drawing.Size(95, 21);
            this.ServerLogsLabel.TabIndex = 16;
            this.ServerLogsLabel.Text = "Server Logs:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel5.Location = new System.Drawing.Point(91, 69);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(617, 1);
            this.panel5.TabIndex = 15;
            // 
            // ServerShutdownButton
            // 
            this.ServerShutdownButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ServerShutdownButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ServerShutdownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ServerShutdownButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ServerShutdownButton.Location = new System.Drawing.Point(319, 348);
            this.ServerShutdownButton.Name = "ServerShutdownButton";
            this.ServerShutdownButton.Size = new System.Drawing.Size(126, 66);
            this.ServerShutdownButton.TabIndex = 12;
            this.ServerShutdownButton.Text = "Shutdown Server";
            this.ServerShutdownButton.UseVisualStyleBackColor = false;
            this.ServerShutdownButton.Click += new System.EventHandler(this.ServerShutdownButton_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel2.Controls.Add(this.ServerLogs);
            this.panel2.Location = new System.Drawing.Point(317, 109);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(2);
            this.panel2.Size = new System.Drawing.Size(317, 217);
            this.panel2.TabIndex = 11;
            // 
            // ServerLogs
            // 
            this.ServerLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ServerLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ServerLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ServerLogs.Location = new System.Drawing.Point(2, 2);
            this.ServerLogs.Name = "ServerLogs";
            this.ServerLogs.Size = new System.Drawing.Size(313, 213);
            this.ServerLogs.TabIndex = 0;
            this.ServerLogs.Text = "";
            this.ServerLogs.TextChanged += new System.EventHandler(this.ServerLogs_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.ClientsList);
            this.panel1.Location = new System.Drawing.Point(168, 142);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(126, 186);
            this.panel1.TabIndex = 10;
            // 
            // ClientsList
            // 
            this.ClientsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ClientsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientsList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientsList.FormattingEnabled = true;
            this.ClientsList.Location = new System.Drawing.Point(2, 2);
            this.ClientsList.Name = "ClientsList";
            this.ClientsList.Size = new System.Drawing.Size(122, 182);
            this.ClientsList.TabIndex = 2;
            this.ClientsList.SelectedIndexChanged += new System.EventHandler(this.ClientsList_SelectedIndexChanged);
            // 
            // ConnectedUsersLabel
            // 
            this.ConnectedUsersLabel.AutoSize = true;
            this.ConnectedUsersLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ConnectedUsersLabel.Location = new System.Drawing.Point(165, 107);
            this.ConnectedUsersLabel.Name = "ConnectedUsersLabel";
            this.ConnectedUsersLabel.Size = new System.Drawing.Size(130, 21);
            this.ConnectedUsersLabel.TabIndex = 9;
            this.ConnectedUsersLabel.Text = "Connected Users:";
            this.ConnectedUsersLabel.Click += new System.EventHandler(this.ConnectedUsersLabel_Click);
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ServerLabel.Location = new System.Drawing.Point(277, 36);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(241, 30);
            this.ServerLabel.TabIndex = 8;
            this.ServerLabel.Text = "SERVER ADMIN PANEL";
            this.ServerLabel.Click += new System.EventHandler(this.ServerLabel_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel3);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Server";
            this.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Server_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
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