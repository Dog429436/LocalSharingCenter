using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace LocalSharingCenter
{
    public class Connection
    {
        protected TcpClient client;
        protected NetworkStream stream;
        protected StreamReader reader;
        protected StreamWriter writer;
        protected byte[] clientAesKey;
        protected byte[] clientAesVector;
        protected List<byte[]> parts;
        protected string fileName;
        protected int currentFileNum;
        protected int numOfFiles;
        protected bool isRecievingFiles;

        public Connection()
        {
            this.parts = new List<byte[]>();
            this.fileName = "";
            this.currentFileNum = 0;
            this.numOfFiles = 0;
            this.isRecievingFiles = false;
        }
        public Connection(TcpClient client)
        {
            this.client = client;
            this.stream = client.GetStream();
            this.reader = new StreamReader(stream);
            this.writer = new StreamWriter(stream) { AutoFlush = true };
        }
        public async void HandleFileBegin(string response, RichTextBox textBox)
        {
            string header = Protocol.fileTransferFiels.FILE_BEGIN.ToString();
            string[] fields = response.Substring(header.Length).Split('|');
            fileName = fields[1];
            currentFileNum = int.Parse(fields[2]) + 1;
            numOfFiles = int.Parse(fields[3]);
            fileName = Path.GetFileName(fileName);
            if (!Directory.Exists(Protocol.TARGETFOLDER))
            {
                await InterfaceHelper.WriteMessage("No target folder found, creating...", textBox, true);
                Directory.CreateDirectory(Protocol.TARGETFOLDER);
            }
            parts.Clear();
            isRecievingFiles = true;
            await InterfaceHelper.WriteMessage(string.Format("File transfer started: ({0}/{1})", currentFileNum, numOfFiles), textBox, true);
        }

        public void HandleFilePiece(string response)
        {
            if (isRecievingFiles)
            {
                string header = Protocol.fileTransferFiels.FILE_PIECE.ToString() + "|";
                string encodedData = response.Substring(header.Length);
                byte[] encryptedData = Convert.FromBase64String(encodedData);
                byte[] data = AesHelper.DecryptAesMessageBytes(encryptedData, this.clientAesKey, this.clientAesVector);
                parts.Add(data);

            }
        }

        public async void HandleFileEnd(string response, RichTextBox textBox, Button button)
        {
            if (isRecievingFiles && fileName != "")
            {
                string fullPath = Path.Combine(Protocol.TARGETFOLDER, fileName);
                using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    foreach (byte[] part in parts)
                    {
                        fs.Write(part, 0, part.Length);
                    }
                }
                isRecievingFiles = false;
                fileName = "";
                parts.Clear();
                this.writer.WriteLine(AesHelper.EncryptToAesMessageString(string.Format("{0}|{1}", Protocol.status.ok.ToString(), currentFileNum - 1), this.clientAesKey, this.clientAesVector));
                await InterfaceHelper.WriteMessage(string.Format("File transfer ended, recieved file: ({0}/{1})", currentFileNum, numOfFiles), textBox, true);
                if (currentFileNum == numOfFiles)
                {
                    await InterfaceHelper.WriteMessage(string.Format("Successfully recieved all of the files: ({0}/{1})", currentFileNum, numOfFiles), textBox, true);
                    currentFileNum = 1;
                    numOfFiles = 0;
                }
                else
                {
                    currentFileNum++;
                }
                if (button.InvokeRequired)
                {
                    button.Invoke(new Action(() => button.Enabled = true));
                }
                else
                {
                    button.Invoke(new Action(() => button.Enabled = true));
                }
            }
        }
        public virtual void CloseConnection()
        {
            this.reader.Close();
            this.writer.Close();
            this.stream.Close();
            this.client.Close();
        }
    }
}
