using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;
namespace LocalSharingCenter
{
    /// <summary>
    /// A class for handling SQLite database operations
    /// </summary>
    public class SQLiteDataBase
    {
        private const string DBNAME = "users";
        private const string DBFIELDS = "Data Source=users.db;Version=3;";
        private const string DBTABLE = @"CREATE TABLE IF NOT EXISTS users (username TEXT PRIMARY KEY UNIQUE, password TEXT NOT NULL, salt TEXT NOT NULL, isAdmin INTEGER NOT NULL);";
        private SQLiteConnection connection;

        public SQLiteDataBase()
        {
            this.connection = new SQLiteConnection(DBFIELDS);
            this.connection.Open();
            using (var c = new SQLiteCommand(DBTABLE, this.connection))
            {
                c.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Checks whether a username exists in the database.
        /// </summary>
        /// <param name="username">The client's username.</param>
        /// <returns>True if the username exists in the database otherwise, false.</returns>
        public bool UsernameExists(string username)
        {
            using (var write = new SQLiteCommand(string.Format("SELECT COUNT(*) FROM {0} WHERE username = @username", DBNAME), this.connection))
            {
                write.Parameters.AddWithValue("@username", username);
                long count = (long)write.ExecuteScalar();
                if (count != 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Checks whether a user exists in the database.
        /// </summary>
        /// <param name="username">The client's username.</param>
        /// <param name="password">The client's password.</param>
        /// <param name="isAdmin">The client's access level.</param>
        /// <returns>True if the user exists in the database otherwise, false.</returns>
        public bool UserExists(string username, string password, bool isAdmin)
        {
            string savedPassword = "";
            string salt = "";
            using (var c = new SQLiteCommand(string.Format("SELECT password, salt FROM {0} WHERE username = @username AND isAdmin = @isAdmin", DBNAME), this.connection))
            {
                c.Parameters.AddWithValue("@username", username);
                c.Parameters.AddWithValue("@isAdmin", isAdmin ? 1 : 0);
                using (var reader = c.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        savedPassword = reader.GetString(0);
                        salt = reader.GetString(1);

                        byte[] hashedPassword;
                        using (SHA256 hash = SHA256.Create())
                        {
                            hashedPassword = hash.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                            return savedPassword == Convert.ToBase64String(hashedPassword);
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Writes a new user to the database. 
        /// </summary>
        /// <param name="username">The client's username.</param>
        /// <param name="password">The client's hashed password.</param>
        /// <param name="salt">The client's salt used to hash the password.</param>
        public void WriteUser(string username, string password, string salt)
        {
            using (var write = new SQLiteCommand("INSERT OR IGNORE INTO users (username, password, salt, isAdmin) VALUES (@username, @password, @salt, 0)", this.connection))
            {
                write.Parameters.AddWithValue("@username", username);
                write.Parameters.AddWithValue("@password", password);
                write.Parameters.AddWithValue("@salt", salt);
                write.ExecuteNonQuery();
            }
        }
    }
}
