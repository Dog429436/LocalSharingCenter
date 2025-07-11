using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LocalSharingCenter
{
    /// <summary>
    /// A static helper class for AES related functions
    /// </summary>
    public static class AesHelper
    {

        private static string RSAFILEPATH = @"server_private_rsa_key.enc";
        private static string SALTFILEPATH = @"server_private_key_salt";

        /// <summary>
        /// Encrypts a plain message using AES and returns a Base64 AES encrypted string.
        /// </summary>
        /// <param name="message">The plain text that will be encrypted.</param>
        /// <param name="aesKey">The AES key.</param>
        /// <param name="aesVector">The AES initialization vector (IV).</param>
        /// <returns>Base64 AES encrypted string.</returns>
        public static string EncryptToAesMessageString(string message, byte[] aesKey, byte[] aesVector)
        {
            byte[] messageBytes;
            byte[] encryptedBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = aesKey;
                aes.IV = aesVector;
                using (ICryptoTransform encrypt = aes.CreateEncryptor())
                {
                    messageBytes = Encoding.UTF8.GetBytes(message);
                    encryptedBytes = encrypt.TransformFinalBlock(messageBytes, 0, messageBytes.Length);
                }
            }
            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Encrypts a byte array using AES and returns a byte array of the encrypted data.
        /// </summary>
        /// <param name="data">The plain byte array to encrypt.</param>
        /// <param name="aesKey">The AES key.</param>
        /// <param name="aesVector">The AES initialization vector (IV).</param>
        /// <returns>Byte array of the encrypted data.</returns>
        public static byte[] EncryptToAesMessageBytes(byte[] data, byte[] aesKey, byte[] aesVector)
        {
            byte[] encryptedBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = aesKey;
                aes.IV = aesVector;
                using (ICryptoTransform encrypt = aes.CreateEncryptor())
                {
                    encryptedBytes = encrypt.TransformFinalBlock(data, 0, data.Length);
                }
            }
            return encryptedBytes;
        }

        /// <summary>
        /// Decrypts the server's RSA private key for signature authentication.
        /// </summary>
        /// <param name="password">Static password used to derive the AES encryption parameters.</param>
        /// <returns>The decrypted RSA private key as a UTF-8 string.</returns>
        public static string DecryptServerRsaPrivateKey(string password)
        {
            byte[] encryptedData = File.ReadAllBytes(RSAFILEPATH);
            byte[] salt = File.ReadAllBytes(SALTFILEPATH);

            //Create a key derivation function with the static password and the salt from the file
            using (var kdf = new Rfc2898DeriveBytes(password, salt, 100000))
            {
                byte[] aesKey = kdf.GetBytes(32);
                byte[] aesVector = kdf.GetBytes(16);

                //Decrypt the RSA private key with the derived AES parameters
                byte[] rsaKey = DecryptAesMessageBytes(encryptedData, aesKey, aesVector);
                return Encoding.UTF8.GetString(rsaKey);
            }
        }

        /// <summary>
        /// Encrypts a portion of a byte array using AES and returns the encrypted data.
        /// </summary>
        /// <param name="data">The plain byte array to encrypt.</param>
        /// <param name="offset">The offset to start the encryption from.</param>
        /// <param name="count">The number of bytes to encrypt from the offset.</param>
        /// <param name="aesKey">The AES key.</param>
        /// <param name="aesVector">The AES initialization vector (IV).</param>
        /// <returns>The encrypted byte array.</returns>
        public static byte[] EncryptToAesMessageBytes(byte[] data, int offset, int count, byte[] aesKey, byte[] aesVector)
        {
            byte[] encryptedBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = aesKey;
                aes.IV = aesVector;
                using (ICryptoTransform encrypt = aes.CreateEncryptor())
                {
                    encryptedBytes = encrypt.TransformFinalBlock(data, offset, count);
                }
            }
            return encryptedBytes;
        }

        /// <summary>
        /// Decrypts a Base64 encoded AES encrypted string and returns the plain text message.
        /// </summary>
        /// <param name="aesMessage">The Base64 encoded AES encrypted message to decrypt.</param>
        /// <param name="aesKey">The AES key.</param>
        /// <param name="aesVector">The AES initialization vector (IV).</param>
        /// <returns>The decrypted plain text string.</returns>
        public static string DecryptAesMessageString(string aesMessage, byte[] aesKey, byte[] aesVector)
        {
            byte[] aesMessageBytes;
            byte[] messageBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = aesKey;
                aes.IV = aesVector;
                ICryptoTransform decrypt = aes.CreateDecryptor();
                aesMessageBytes = Convert.FromBase64String(aesMessage);
                messageBytes = decrypt.TransformFinalBlock(aesMessageBytes, 0, aesMessageBytes.Length);
            }
            return Encoding.UTF8.GetString(messageBytes);
        }

        /// <summary>
        /// Decrypts an AES encrypted byte array and returns the plain text bytes.
        /// </summary>
        /// <param name="aesMessage">The AES encrypted byte array to decrypt.</param>
        /// <param name="aesKey">The AES key.</param>
        /// <param name="aesVector">The AES initialization vector (IV).</param>
        /// <returns>The decrypted byte array of the plain text.</returns>
        public static byte[] DecryptAesMessageBytes(byte[] aesMessage, byte[] aesKey, byte[] aesVector)
        {
            byte[] messageBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = aesKey;
                aes.IV = aesVector;
                ICryptoTransform decrypt = aes.CreateDecryptor();
                messageBytes = decrypt.TransformFinalBlock(aesMessage, 0, aesMessage.Length);
            }
            return messageBytes;
            
        }
    }
}
