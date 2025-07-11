namespace LocalSharingCenter
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
            this.LoginPanel = new System.Windows.Forms.Panel();
            this.Arrow = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.messages = new System.Windows.Forms.RichTextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.Password = new System.Windows.Forms.TextBox();
            this.Username = new System.Windows.Forms.TextBox();
            this.login = new System.Windows.Forms.Label();
            this.SignUpPanel = new System.Windows.Forms.Panel();
            this.SignupPassword = new System.Windows.Forms.TextBox();
            this.SignupUsername = new System.Windows.Forms.TextBox();
            this.signupMessages = new System.Windows.Forms.RichTextBox();
            this.SignupButton = new System.Windows.Forms.Button();
            this.SignUpPasswordLabel = new System.Windows.Forms.Label();
            this.SignUpUsernameLabel = new System.Windows.Forms.Label();
            this.SignUpLabel = new System.Windows.Forms.Label();
            this.Arrow2 = new System.Windows.Forms.Button();
            this.CommandPanel = new System.Windows.Forms.Panel();
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.MenuLogs = new System.Windows.Forms.RichTextBox();
            this.MenuLabel = new System.Windows.Forms.Label();
            this.ShowLogin = new System.Windows.Forms.Button();
            this.ShowSignUp = new System.Windows.Forms.Button();
            this.FileBar = new System.Windows.Forms.ProgressBar();
            this.LoginPanel.SuspendLayout();
            this.SignUpPanel.SuspendLayout();
            this.CommandPanel.SuspendLayout();
            this.Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginPanel
            // 
            this.LoginPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.LoginPanel.Controls.Add(this.Arrow);
            this.LoginPanel.Controls.Add(this.panel1);
            this.LoginPanel.Controls.Add(this.messages);
            this.LoginPanel.Controls.Add(this.PasswordLabel);
            this.LoginPanel.Controls.Add(this.UsernameLabel);
            this.LoginPanel.Controls.Add(this.LoginButton);
            this.LoginPanel.Controls.Add(this.Password);
            this.LoginPanel.Controls.Add(this.Username);
            this.LoginPanel.Controls.Add(this.login);
            this.LoginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoginPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.LoginPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.LoginPanel.Location = new System.Drawing.Point(0, 0);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(879, 461);
            this.LoginPanel.TabIndex = 4;
            this.LoginPanel.Visible = false;
            this.LoginPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.LoginPanel_Paint);
            // 
            // Arrow
            // 
            this.Arrow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Arrow.FlatAppearance.BorderSize = 0;
            this.Arrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Arrow.Image = global::LocalSharingCenter.Properties.Resources.arrow;
            this.Arrow.Location = new System.Drawing.Point(38, 46);
            this.Arrow.Name = "Arrow";
            this.Arrow.Size = new System.Drawing.Size(99, 33);
            this.Arrow.TabIndex = 9;
            this.Arrow.UseVisualStyleBackColor = false;
            this.Arrow.Click += new System.EventHandler(this.Arrow_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel1.Location = new System.Drawing.Point(131, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(617, 1);
            this.panel1.TabIndex = 8;
            // 
            // messages
            // 
            this.messages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.messages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.messages.Location = new System.Drawing.Point(277, 364);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(246, 74);
            this.messages.TabIndex = 7;
            this.messages.Text = "";
            this.messages.TextChanged += new System.EventHandler(this.messages_TextChanged);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.PasswordLabel.Location = new System.Drawing.Point(167, 219);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(82, 20);
            this.PasswordLabel.TabIndex = 6;
            this.PasswordLabel.Text = "Password:";
            this.PasswordLabel.Click += new System.EventHandler(this.PasswordLabel_Click);
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.UsernameLabel.Location = new System.Drawing.Point(167, 161);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(87, 20);
            this.UsernameLabel.TabIndex = 5;
            this.UsernameLabel.Text = "Username:";
            this.UsernameLabel.Click += new System.EventHandler(this.UsernameLabel_Click);
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.LoginButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.LoginButton.Location = new System.Drawing.Point(277, 306);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(94, 34);
            this.LoginButton.TabIndex = 3;
            this.LoginButton.Text = "Log in";
            this.LoginButton.UseVisualStyleBackColor = false;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // Password
            // 
            this.Password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Password.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Password.Location = new System.Drawing.Point(277, 219);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(194, 22);
            this.Password.TabIndex = 2;
            this.Password.TextChanged += new System.EventHandler(this.Password_TextChanged);
            // 
            // Username
            // 
            this.Username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Username.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Username.Location = new System.Drawing.Point(277, 161);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(194, 22);
            this.Username.TabIndex = 1;
            this.Username.TextChanged += new System.EventHandler(this.Username_TextChanged);
            // 
            // login
            // 
            this.login.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.login.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.login.Location = new System.Drawing.Point(208, 42);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(462, 83);
            this.login.TabIndex = 0;
            this.login.Text = "To log in enter your username and password";
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // SignUpPanel
            // 
            this.SignUpPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.SignUpPanel.Controls.Add(this.SignupPassword);
            this.SignUpPanel.Controls.Add(this.SignupUsername);
            this.SignUpPanel.Controls.Add(this.signupMessages);
            this.SignUpPanel.Controls.Add(this.SignupButton);
            this.SignUpPanel.Controls.Add(this.SignUpPasswordLabel);
            this.SignUpPanel.Controls.Add(this.SignUpUsernameLabel);
            this.SignUpPanel.Controls.Add(this.SignUpLabel);
            this.SignUpPanel.Controls.Add(this.Arrow2);
            this.SignUpPanel.Location = new System.Drawing.Point(0, 0);
            this.SignUpPanel.Name = "SignUpPanel";
            this.SignUpPanel.Size = new System.Drawing.Size(879, 461);
            this.SignUpPanel.TabIndex = 8;
            this.SignUpPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.SignUpPanel_Paint);
            // 
            // SignupPassword
            // 
            this.SignupPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.SignupPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SignupPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.SignupPassword.Location = new System.Drawing.Point(301, 266);
            this.SignupPassword.Name = "SignupPassword";
            this.SignupPassword.PasswordChar = '*';
            this.SignupPassword.Size = new System.Drawing.Size(194, 20);
            this.SignupPassword.TabIndex = 15;
            this.SignupPassword.TextChanged += new System.EventHandler(this.SignupPassword_TextChanged);
            // 
            // SignupUsername
            // 
            this.SignupUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.SignupUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SignupUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.SignupUsername.Location = new System.Drawing.Point(301, 212);
            this.SignupUsername.Name = "SignupUsername";
            this.SignupUsername.Size = new System.Drawing.Size(194, 20);
            this.SignupUsername.TabIndex = 14;
            this.SignupUsername.TextChanged += new System.EventHandler(this.SignupUsername_TextChanged);
            // 
            // signupMessages
            // 
            this.signupMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.signupMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.signupMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.signupMessages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.signupMessages.Location = new System.Drawing.Point(269, 365);
            this.signupMessages.Name = "signupMessages";
            this.signupMessages.Size = new System.Drawing.Size(246, 74);
            this.signupMessages.TabIndex = 12;
            this.signupMessages.Text = "";
            this.signupMessages.TextChanged += new System.EventHandler(this.signupMessages_TextChanged);
            // 
            // SignupButton
            // 
            this.SignupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.SignupButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SignupButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.SignupButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.SignupButton.Location = new System.Drawing.Point(202, 308);
            this.SignupButton.Name = "SignupButton";
            this.SignupButton.Size = new System.Drawing.Size(75, 32);
            this.SignupButton.TabIndex = 11;
            this.SignupButton.Text = "Sign up";
            this.SignupButton.UseVisualStyleBackColor = false;
            this.SignupButton.Click += new System.EventHandler(this.SignupButton_Click);
            // 
            // SignUpPasswordLabel
            // 
            this.SignUpPasswordLabel.AutoSize = true;
            this.SignUpPasswordLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.SignUpPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.SignUpPasswordLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.SignUpPasswordLabel.Location = new System.Drawing.Point(190, 264);
            this.SignUpPasswordLabel.Name = "SignUpPasswordLabel";
            this.SignUpPasswordLabel.Size = new System.Drawing.Size(82, 20);
            this.SignUpPasswordLabel.TabIndex = 10;
            this.SignUpPasswordLabel.Text = "Password:";
            this.SignUpPasswordLabel.Click += new System.EventHandler(this.SignUpPasswordLabel_Click);
            // 
            // SignUpUsernameLabel
            // 
            this.SignUpUsernameLabel.AutoSize = true;
            this.SignUpUsernameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.SignUpUsernameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.SignUpUsernameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.SignUpUsernameLabel.Location = new System.Drawing.Point(190, 210);
            this.SignUpUsernameLabel.Name = "SignUpUsernameLabel";
            this.SignUpUsernameLabel.Size = new System.Drawing.Size(84, 21);
            this.SignUpUsernameLabel.TabIndex = 9;
            this.SignUpUsernameLabel.Text = "Username:";
            this.SignUpUsernameLabel.Click += new System.EventHandler(this.SignUpUsernameLabel_Click);
            // 
            // SignUpLabel
            // 
            this.SignUpLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SignUpLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.SignUpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.SignUpLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.SignUpLabel.Location = new System.Drawing.Point(209, 27);
            this.SignUpLabel.Name = "SignUpLabel";
            this.SignUpLabel.Size = new System.Drawing.Size(395, 171);
            this.SignUpLabel.TabIndex = 6;
            this.SignUpLabel.Text = "To sign in please create a password with this policy:\r\n\r\n• At least 8 characters\r" +
    "\n• At least 1 uppercase letter\r\n• At least 1 number\r\n• At least 1 special charac" +
    "ter\r\n• No whitespaces\r\n";
            this.SignUpLabel.Click += new System.EventHandler(this.SignUpLabel_Click);
            // 
            // Arrow2
            // 
            this.Arrow2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Arrow2.FlatAppearance.BorderSize = 0;
            this.Arrow2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Arrow2.Image = global::LocalSharingCenter.Properties.Resources.arrow;
            this.Arrow2.Location = new System.Drawing.Point(13, 13);
            this.Arrow2.Name = "Arrow2";
            this.Arrow2.Size = new System.Drawing.Size(114, 51);
            this.Arrow2.TabIndex = 5;
            this.Arrow2.UseVisualStyleBackColor = false;
            this.Arrow2.Click += new System.EventHandler(this.Arrow2_Click);
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
            this.CommandArrow.Image = global::LocalSharingCenter.Properties.Resources.arrow;
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
            this.Menu.Controls.Add(this.panel4);
            this.Menu.Controls.Add(this.MenuLogs);
            this.Menu.Controls.Add(this.MenuLabel);
            this.Menu.Controls.Add(this.ShowLogin);
            this.Menu.Controls.Add(this.ShowSignUp);
            this.Menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Menu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(879, 461);
            this.Menu.TabIndex = 14;
            this.Menu.Paint += new System.Windows.Forms.PaintEventHandler(this.Menu_Paint);
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
            this.MenuLabel.Text = "Hello Client, please login or sign up";
            this.MenuLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.MenuLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // ShowLogin
            // 
            this.ShowLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ShowLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ShowLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ShowLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ShowLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.ShowLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.ShowLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ShowLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ShowLogin.Location = new System.Drawing.Point(242, 237);
            this.ShowLogin.Name = "ShowLogin";
            this.ShowLogin.Size = new System.Drawing.Size(166, 49);
            this.ShowLogin.TabIndex = 1;
            this.ShowLogin.Text = "Login";
            this.ShowLogin.UseVisualStyleBackColor = false;
            this.ShowLogin.Click += new System.EventHandler(this.ShowLogin_Click);
            // 
            // ShowSignUp
            // 
            this.ShowSignUp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ShowSignUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ShowSignUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ShowSignUp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ShowSignUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.ShowSignUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.ShowSignUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowSignUp.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ShowSignUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ShowSignUp.Location = new System.Drawing.Point(483, 237);
            this.ShowSignUp.Name = "ShowSignUp";
            this.ShowSignUp.Size = new System.Drawing.Size(166, 49);
            this.ShowSignUp.TabIndex = 2;
            this.ShowSignUp.Text = "Sign up";
            this.ShowSignUp.UseVisualStyleBackColor = false;
            this.ShowSignUp.Click += new System.EventHandler(this.ShowSignUp_Click);
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
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(879, 461);
            this.Controls.Add(this.CommandPanel);
            this.Controls.Add(this.LoginPanel);
            this.Controls.Add(this.Menu);
            this.Controls.Add(this.SignUpPanel);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(499, 498);
            this.Name = "Client";
            this.Text = "Local sharing center";
            this.Load += new System.EventHandler(this.Client_Load);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.SignUpPanel.ResumeLayout(false);
            this.SignUpPanel.PerformLayout();
            this.CommandPanel.ResumeLayout(false);
            this.CommandPanel.PerformLayout();
            this.Menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel LoginPanel;
        private System.Windows.Forms.Label login;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.RichTextBox messages;
        private System.Windows.Forms.Panel SignUpPanel;
        private System.Windows.Forms.Button Arrow2;
        private System.Windows.Forms.Label SignUpLabel;
        private System.Windows.Forms.Label SignUpPasswordLabel;
        private System.Windows.Forms.Label SignUpUsernameLabel;
        private System.Windows.Forms.Button SignupButton;
        private System.Windows.Forms.RichTextBox signupMessages;
        private System.Windows.Forms.TextBox SignupPassword;
        private System.Windows.Forms.TextBox SignupUsername;
        private System.Windows.Forms.Panel CommandPanel;
        private System.Windows.Forms.Button CommandArrow;
        private System.Windows.Forms.Button ListButton;
        private System.Windows.Forms.ListBox FileList;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Button SelectAllButton;
        private System.Windows.Forms.RichTextBox FileBox;
        private System.Windows.Forms.Button ShowSignUp;
        private System.Windows.Forms.Button ShowLogin;
        private System.Windows.Forms.Panel Menu;
        private System.Windows.Forms.Label MenuLabel;
        private System.Windows.Forms.RichTextBox MenuLogs;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Arrow;
        private System.Windows.Forms.Label ServerMessagesLabel;
        private System.Windows.Forms.Label FileLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar FileBar;
    }
}