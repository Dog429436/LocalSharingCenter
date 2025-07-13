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
namespace LocalSharingCenterHome
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
            button1.Enabled = false;
            this.client = new ServerConnection();
            await this.client.ConnectToServer(MenuLogs);
            if (this.client.IsConnected())
            {
                await InterfaceHelper.ClearMessage(FileBox);
                CommandPanel.BringToFront();
                this.client.ResponseHandle(DownloadButton, FileList, FileBox, FileBar);
            }
            button1.Enabled = true;
        }

        /// <summary>
        /// Closes the tcp connection when the program is closing.
        /// </summary>
        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.client.CloseTcpConnection();
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

        private void UsernameLabel_Click(object sender, EventArgs e)
        {

        }

        private void PasswordLabel_Click(object sender, EventArgs e)
        {

        }

        private void messages_TextChanged(object sender, EventArgs e)
        {

        }


        private void label2_Click(object sender, EventArgs e)
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

        private void FileLabel_Click(object sender, EventArgs e)
        {

        }

        private void ServerMessagesLabel_Click(object sender, EventArgs e)
        {

        }

        private void FileBar_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Checks if the client is connected to the server.
        /// If not, then attempts to connect.
        /// </summary>
        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (!this.client.IsConnected())
            {
                await this.client.ConnectToServer(MenuLogs);
            }
            if (this.client.IsConnected())
            {
                await InterfaceHelper.ClearMessage(FileBox);
                CommandPanel.BringToFront();
                this.client.ResponseHandle(DownloadButton, FileList, FileBox, FileBar);
            }
            button1.Enabled = true;
        }
    }
}
