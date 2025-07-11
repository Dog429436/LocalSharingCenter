using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
namespace LocalSharingCenter
{
    public partial class Client : Form
    {
        private ServerConnection client = null;

        public Client()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the client, clears the logs, and prepares for a connection to the server.
        /// </summary>
        private async void Client_Load(object sender, EventArgs e)
        {
            await InterfaceHelper.ClearMessage(MenuLogs);
            Menu.BringToFront();
            ShowLogin.Enabled = false;
            ShowSignUp.Enabled = false;
            this.client = new ServerConnection();
            await this.client.ConnectToServer(MenuLogs);
            ShowLogin.Enabled = true;
            ShowSignUp.Enabled = true;
        }

        /// <summary>
        /// Closes the tcp connection when the program is closing.
        /// </summary>
        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.client.CloseTcpConnection();
        }

        /// <summary>
        /// Checks if a connection is active. If so, presents the login panel.
        /// Otherwise, attempts to reconnect before displaying the panel.
        /// </summary>
        private async void ShowLogin_Click(object sender, EventArgs e)
        {
            if (this.client.IsConnected())
            {
                await InterfaceHelper.ClearMessage(messages);
                LoginPanel.Visible = true; 
                LoginPanel.BringToFront(); 
            }
            else
            {
                ShowLogin.Enabled = false;
                await this.client.ConnectToServer(MenuLogs);
                ShowLogin.Enabled = true;
                if (this.client.IsConnected())
                {
                    await InterfaceHelper.ClearMessage(messages);
                    LoginPanel.Visible = true;
                    LoginPanel.BringToFront();
                }
            }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoginPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void login_Click(object sender, EventArgs e)
        {

        }

        private void Username_TextChanged(object sender, EventArgs e)
        {

        }

        private void Password_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Attempts to authenticate the user with the server.
        /// If the user exists, presents the command panel and starts a server response thread.
        /// </summary>
        private async void LoginButton_Click(object sender, EventArgs e)
        {
            LoginButton.Enabled = false;
            string username = Username.Text;
            string password = Password.Text;
            bool answer = await this.client.TryLogin(username, password, messages);
            if (answer)
            {
                CommandPanel.BringToFront();
                await InterfaceHelper.ClearMessage(FileBox);
                this.client.ResponseHandle(DownloadButton, FileList, FileBox, FileBar);
            }
            LoginButton.Enabled = true;

        }

        private void UsernameLabel_Click(object sender, EventArgs e)
        {

        }

        private void PasswordLabel_Click(object sender, EventArgs e)
        {

        }

        private void messages_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Checks if a connection is active. If so, presents the sign-up panel.
        /// Otherwise, attempts to reconnect before displaying the panel.
        /// </summary>
        private async void ShowSignUp_Click(object sender, EventArgs e)
        {
            
            if (this.client.IsConnected())
            {
                await InterfaceHelper.ClearMessage(signupMessages);
                SignUpPanel.Visible = true; 
                SignUpPanel.BringToFront(); 
            }
            else
            {
                ShowSignUp.Enabled = false;
                await this.client.ConnectToServer(MenuLogs);
                ShowSignUp.Enabled = true;
                if (this.client.IsConnected())
                {
                    await InterfaceHelper.ClearMessage(signupMessages);
                    this.SignUpPanel.Visible = true;
                    SignUpPanel.BringToFront();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SignUpPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// When the arrow is clicked, sends the sign-up panel back.
        /// </summary>
        private void Arrow2_Click(object sender, EventArgs e)
        {
            
            SignUpPanel.SendToBack();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SignUpLabel_Click(object sender, EventArgs e)
        {
            
        }

        private void SignUpUsernameLabel_Click(object sender, EventArgs e)
        {

        }
        private void SignUpPasswordLabel_Click(object sender, EventArgs e)
        {

        }
        private void signupMessages_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Attempts to sign up the user with the server.
        /// </summary>
        private async void SignupButton_Click(object sender, EventArgs e)
        {
            SignupButton.Enabled = false;
            string username = SignupUsername.Text; 
            string password = SignupPassword.Text;
            await this.client.TrySignUp(username, password, signupMessages);
            SignupButton.Enabled = true;
        }

        private void SignupUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void SignupPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void CommandPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Sends a request to the server to retrieve and display the existing files.
        /// </summary>
        private async void ListButton_Click(object sender, EventArgs e)
        {
            await this.client.ListFilesButton(FileBox);
        }

        /// <summary>
        /// Sends a request to the server to download the selected files from the list.
        /// </summary>
        private async void DownloadButton_Click(object sender, EventArgs e)
        {
            DownloadButton.Enabled = false;
            string message = Protocol.Commands.Get.ToString();
            int length = FileList.Items.Count;
            for (int i = 0; i < length; i++)
            {
                if (FileList.GetSelected(i))
                {
                    message += "|" + FileList.Items[i].ToString();
                }
            }
            await this.client.DownloadFilesButton(message, FileBox);
            if (length == 0)
            {
                DownloadButton.Enabled = true;
            }
        }

        /// <summary>
        /// Opens a file dialog for the user to select files and uploads them to the server.
        /// </summary>
        private async void UploadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.InitialDirectory = Environment.CurrentDirectory;
                fileDialog.Multiselect = true;
                fileDialog.Title = "Select files";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] selectedFiles = fileDialog.FileNames;
                    await this.client.UploadFilesButton(selectedFiles, FileBox, FileBar);
                    
                }
            }
        }

        /// <summary>
        /// Closes the TCP connection and disconnects the client from the server.
        /// </summary>
        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            this.client.CloseTcpConnection();
            
        }

        private void FileList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Closes the TCP connection and returns to the main menu.
        /// </summary>
        private async void CommandArrow_Click(object sender, EventArgs e)
        {
            this.client.CloseTcpConnection();
            await InterfaceHelper.ClearMessage(MenuLogs);
            Menu.BringToFront();

        }
        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Selects all of the files from the list to download.
        /// </summary>
        private void DownloadAllButton_Click(object sender, EventArgs e)
        {
            int length = FileList.Items.Count;
            for (int i = 0; i < length; i++)
            {
                FileList.SetSelected(i, true);
            }
        }

        private void FileBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Menu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MenuLogs_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Sends the login panel to the back.
        /// </summary>
        private void Arrow_Click(object sender, EventArgs e)
        {
            LoginPanel.SendToBack();
        }

        private void FileLabel_Click(object sender, EventArgs e)
        {

        }

        private void ServerMessagesLabel_Click(object sender, EventArgs e)
        {

        }

        private void FileBar_Click(object sender, EventArgs e)
        {

        }
    }
}
