namespace LocalSharingCenterHome
{
    partial class Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.CommandPanel = new System.Windows.Forms.Panel();
            this.FileBar = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ServerMessagesLabel = new System.Windows.Forms.Label();
            this.FileLabel = new System.Windows.Forms.Label();
            this.FileBox = new System.Windows.Forms.RichTextBox();
            this.SelectAllButton = new System.Windows.Forms.Button();
            this.FileList = new System.Windows.Forms.ListBox();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.UploadButton = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.CommandArrow = new System.Windows.Forms.Button();
            this.ListButton = new System.Windows.Forms.Button();
            this.Menu = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.MenuLogs = new System.Windows.Forms.RichTextBox();
            this.MenuLabel = new System.Windows.Forms.Label();
            this.CommandPanel.SuspendLayout();
            this.Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // CommandPanel
            // 
            this.CommandPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.CommandPanel.Controls.Add(this.FileBar);
            this.CommandPanel.Controls.Add(this.panel2);
            this.CommandPanel.Controls.Add(this.ServerMessagesLabel);
            this.CommandPanel.Controls.Add(this.FileLabel);
            this.CommandPanel.Controls.Add(this.FileBox);
            this.CommandPanel.Controls.Add(this.SelectAllButton);
            this.CommandPanel.Controls.Add(this.FileList);
            this.CommandPanel.Controls.Add(this.DisconnectButton);
            this.CommandPanel.Controls.Add(this.UploadButton);
            this.CommandPanel.Controls.Add(this.DownloadButton);
            this.CommandPanel.Controls.Add(this.CommandArrow);
            this.CommandPanel.Controls.Add(this.ListButton);
            this.CommandPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.CommandPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CommandPanel.Location = new System.Drawing.Point(0, 0);
            this.CommandPanel.Name = "CommandPanel";
            this.CommandPanel.Size = new System.Drawing.Size(879, 461);
            this.CommandPanel.TabIndex = 8;
            this.CommandPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.CommandPanel_Paint);
            // 
            // FileBar
            // 
            this.FileBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.FileBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.FileBar.Location = new System.Drawing.Point(194, 427);
            this.FileBar.Name = "FileBar";
            this.FileBar.Size = new System.Drawing.Size(493, 23);
            this.FileBar.TabIndex = 17;
            this.FileBar.Click += new System.EventHandler(this.FileBar_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel2.Location = new System.Drawing.Point(143, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(617, 1);
            this.panel2.TabIndex = 16;
            // 
            // ServerMessagesLabel
            // 
            this.ServerMessagesLabel.AutoSize = true;
            this.ServerMessagesLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ServerMessagesLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ServerMessagesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ServerMessagesLabel.Location = new System.Drawing.Point(197, 328);
            this.ServerMessagesLabel.Name = "ServerMessagesLabel";
            this.ServerMessagesLabel.Size = new System.Drawing.Size(112, 30);
            this.ServerMessagesLabel.TabIndex = 15;
            this.ServerMessagesLabel.Text = "Messages:";
            this.ServerMessagesLabel.Click += new System.EventHandler(this.ServerMessagesLabel_Click);
            // 
            // FileLabel
            // 
            this.FileLabel.AutoSize = true;
            this.FileLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.FileLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FileLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.FileLabel.Location = new System.Drawing.Point(399, 19);
            this.FileLabel.Name = "FileLabel";
            this.FileLabel.Size = new System.Drawing.Size(87, 30);
            this.FileLabel.TabIndex = 14;
            this.FileLabel.Text = "File List";
            this.FileLabel.Click += new System.EventHandler(this.FileLabel_Click);
            // 
            // FileBox
            // 
            this.FileBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.FileBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FileBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.FileBox.Location = new System.Drawing.Point(194, 361);
            this.FileBox.Name = "FileBox";
            this.FileBox.Size = new System.Drawing.Size(493, 60);
            this.FileBox.TabIndex = 13;
            this.FileBox.Text = "";
            this.FileBox.TextChanged += new System.EventHandler(this.FileBox_TextChanged);
            // 
            // SelectAllButton
            // 
            this.SelectAllButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.SelectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectAllButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.SelectAllButton.Location = new System.Drawing.Point(386, 281);
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.Size = new System.Drawing.Size(100, 48);
            this.SelectAllButton.TabIndex = 12;
            this.SelectAllButton.Text = "Select all";
            this.SelectAllButton.UseVisualStyleBackColor = false;
            this.SelectAllButton.Click += new System.EventHandler(this.DownloadAllButton_Click);
            // 
            // FileList
            // 
            this.FileList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.FileList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FileList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.FileList.FormattingEnabled = true;
            this.FileList.ItemHeight = 20;
            this.FileList.Location = new System.Drawing.Point(191, 65);
            this.FileList.Name = "FileList";
            this.FileList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.FileList.Size = new System.Drawing.Size(496, 142);
            this.FileList.TabIndex = 11;
            this.FileList.SelectedIndexChanged += new System.EventHandler(this.FileList_SelectedIndexChanged);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.DisconnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DisconnectButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DisconnectButton.Location = new System.Drawing.Point(587, 225);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(100, 50);
            this.DisconnectButton.TabIndex = 10;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = false;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // UploadButton
            // 
            this.UploadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.UploadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UploadButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.UploadButton.Location = new System.Drawing.Point(454, 225);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(100, 50);
            this.UploadButton.TabIndex = 9;
            this.UploadButton.Text = "Upload File";
            this.UploadButton.UseVisualStyleBackColor = false;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // DownloadButton
            // 
            this.DownloadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.DownloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DownloadButton.Location = new System.Drawing.Point(317, 225);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(100, 50);
            this.DownloadButton.TabIndex = 8;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = false;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // CommandArrow
            // 
            this.CommandArrow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.CommandArrow.FlatAppearance.BorderSize = 0;
            this.CommandArrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CommandArrow.Image = global::LocalSharingCenterHome.Properties.Resources.arrow1;
            this.CommandArrow.Location = new System.Drawing.Point(13, 12);
            this.CommandArrow.Name = "CommandArrow";
            this.CommandArrow.Size = new System.Drawing.Size(114, 51);
            this.CommandArrow.TabIndex = 4;
            this.CommandArrow.UseVisualStyleBackColor = false;
            this.CommandArrow.Click += new System.EventHandler(this.CommandArrow_Click);
            // 
            // ListButton
            // 
            this.ListButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ListButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ListButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ListButton.Location = new System.Drawing.Point(191, 225);
            this.ListButton.Name = "ListButton";
            this.ListButton.Size = new System.Drawing.Size(100, 50);
            this.ListButton.TabIndex = 3;
            this.ListButton.Text = "List";
            this.ListButton.UseVisualStyleBackColor = false;
            this.ListButton.Click += new System.EventHandler(this.ListButton_Click);
            // 
            // Menu
            // 
            this.Menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Menu.Controls.Add(this.button1);
            this.Menu.Controls.Add(this.panel4);
            this.Menu.Controls.Add(this.MenuLogs);
            this.Menu.Controls.Add(this.MenuLabel);
            this.Menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Menu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(879, 461);
            this.Menu.TabIndex = 14;
            this.Menu.Paint += new System.Windows.Forms.PaintEventHandler(this.Menu_Paint);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(375, 246);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 40);
            this.button1.TabIndex = 19;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel4.Location = new System.Drawing.Point(126, 125);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(617, 1);
            this.panel4.TabIndex = 18;
            // 
            // MenuLogs
            // 
            this.MenuLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.MenuLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MenuLogs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.MenuLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.MenuLogs.Location = new System.Drawing.Point(207, 313);
            this.MenuLogs.Name = "MenuLogs";
            this.MenuLogs.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.MenuLogs.Size = new System.Drawing.Size(469, 95);
            this.MenuLogs.TabIndex = 17;
            this.MenuLogs.Text = "";
            this.MenuLogs.TextChanged += new System.EventHandler(this.MenuLogs_TextChanged);
            // 
            // MenuLabel
            // 
            this.MenuLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.MenuLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.MenuLabel.Location = new System.Drawing.Point(20, 78);
            this.MenuLabel.Name = "MenuLabel";
            this.MenuLabel.Size = new System.Drawing.Size(844, 44);
            this.MenuLabel.TabIndex = 0;
            this.MenuLabel.Text = "Hello, establishing connection";
            this.MenuLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.MenuLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(879, 461);
            this.Controls.Add(this.CommandPanel);
            this.Controls.Add(this.Menu);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(499, 498);
            this.Name = "Client";
            this.Text = "Local sharing center";
            this.Load += new System.EventHandler(this.Client_Load);
            this.CommandPanel.ResumeLayout(false);
            this.CommandPanel.PerformLayout();
            this.Menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel CommandPanel;
        private System.Windows.Forms.Button CommandArrow;
        private System.Windows.Forms.Button ListButton;
        private System.Windows.Forms.ListBox FileList;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Button SelectAllButton;
        private System.Windows.Forms.RichTextBox FileBox;
        private System.Windows.Forms.Panel Menu;
        private System.Windows.Forms.Label MenuLabel;
        private System.Windows.Forms.RichTextBox MenuLogs;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label ServerMessagesLabel;
        private System.Windows.Forms.Label FileLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar FileBar;
        private System.Windows.Forms.Button button1;
    }
}