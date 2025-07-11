using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace LocalSharingCenter
{
    /// <summary>
    /// A static class for file transfer related functions.
    /// </summary>
    public static class FileTransfer
    {
        public const string FOLDER = "SharedFiles";
        const int bufferSize = 4096;

        /// <summary>
        /// Sends the selected files and updates the progress bar.
        /// </summary>
        /// <param name="files">An array of file names to send.</param>
        /// <param name="username1">The username of the sender.</param>
        /// <param name="username2">The username of the recipient.</param>
        /// <param name="clientAesKey">The AES key used to encrypt the files.</param>
        /// <param name="clientAesVector">The AES vector (IV) used to encrypt the files.</param>
        /// <param name="reader">The StreamReader used to read the status update from the recipient.</param>
        /// <param name="writer">The StreamWriter used to send the files.</param>
        /// <param name="textBox">The RichTextBox that displays transfer logs.</param>
        /// <param name="FileBar">The progress bar that indicates the file transfer progress.</param>
        /// <returns>A Task representing the asynchronous file transfer operation.</returns>
        public static async Task SendFiles(string[] files, string username1, string username2, byte[] clientAesKey, byte[] clientAesVector, StreamReader reader, StreamWriter writer, RichTextBox textBox, ProgressBar fileBar)
        {
            string fullPath = "";
            string startResponse = "";
            string response = "";
            string endResponse = "";
            int lastPercent = 0;
            int successSend = 0;
            for (int i = 0; i < files.Length; i++)
            {
                fullPath = Path.Combine(FOLDER, files[i]);
                if (File.Exists(fullPath))
                {
                    FileInfo fileInfo = new FileInfo(fullPath);
                    long totalBytes = fileInfo.Length;
                    long sentBytes = 0;
                    if (textBox != null)
                    {
                        await InterfaceHelper.WriteMessage(string.Format("{0} requested file: {1}", username1, files[i]), textBox, true);
                    }
                    startResponse = string.Format("{0}|{1}|{2}|{3}|{4}", Protocol.FileTransferFields.FILE_BEGIN.ToString(), files[i], i, files.Length, totalBytes);
                    string encryptedRespond = AesHelper.EncryptToAesMessageString(startResponse, clientAesKey, clientAesVector);
                    writer.WriteLine(encryptedRespond);
                    using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[bufferSize];
                        int bytesRead = 0;
                        while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            sentBytes += bytesRead;
                            byte[] encrypted = AesHelper.EncryptToAesMessageBytes(buffer, 0, bytesRead, clientAesKey, clientAesVector);
                            string encodedData = Convert.ToBase64String(encrypted);
                            response = AesHelper.EncryptToAesMessageString(string.Format("{0}|{1}|{2}", Protocol.FileTransferFields.FILE_PIECE.ToString(), encodedData, sentBytes), clientAesKey, clientAesVector);
                            int percent = (int)((sentBytes * 100) / totalBytes);
                            if (fileBar != null && percent != lastPercent)
                            {
                                fileBar.Invoke(new Action(() =>
                                {
                                    fileBar.Value = percent;
                                }));
                                lastPercent = percent;
                            }
                            writer.WriteLine(response);
                        }
                        endResponse = AesHelper.EncryptToAesMessageString(string.Format("{0}|", Protocol.FileTransferFields.FILE_END.ToString()), clientAesKey, clientAesVector);
                        writer.WriteLine(endResponse);
                    }
                    if (reader != null)
                    {
                        string encryptedResponse = reader.ReadLine();
                        string responseForFile = AesHelper.DecryptAesMessageString(encryptedResponse, clientAesKey, clientAesVector);
                        string[] fileFields = responseForFile.Split('|');
                        if (fileFields.Length == 2 && fileFields[0] == Protocol.Status.ok.ToString())
                        {
                            if (textBox != null)
                            {
                                await InterfaceHelper.WriteMessage(string.Format("{0} successfully received a file: ({1}/{2})", username2, int.Parse(fileFields[1]) + 1, files.Length), textBox, true);
                            }
                            successSend++;
                        }
                    }
                }
                else
                {
                    response = AesHelper.EncryptToAesMessageString(Protocol.FileTransferFields.NO_FILE.ToString(), clientAesKey, clientAesVector);
                    writer.WriteLine(response);
                }
            }
            if (reader != null)
            {
                if (successSend == files.Length)
                {
                    if (textBox != null)
                    {
                        await InterfaceHelper.WriteMessage(string.Format("{0} successfully received all of the files: ({1}/{2})", username2, successSend, files.Length), textBox, true);
                    }
                }
                else
                {
                    if (textBox != null)
                    {
                        await InterfaceHelper.WriteMessage(string.Format("{0} only received some of the files: ({1}/{2})", username2, successSend, files.Length), textBox, true);
                    }
                }
            }
        }
    }
}
