using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalSharingCenter
{
    /// <summary>
    /// A static class for Controls and text display related functios
    /// </summary>
    static class InterfaceHelper
    {
        
        private static readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Writes the given text to the specified RichTextBox, with optional typing effect.
        /// Thread-safe and can be called from any thread.
        /// </summary>
        /// <param name="message">The message to print.</param>
        /// <param name="textBox">The RichTextBox to write the message to.</param>
        /// <param name="effect">Whether to display a typing animation effect.</param>
        /// <returns>A Task representing the asynchronous write operation.</returns>
        public static async Task WriteMessage(string message, RichTextBox textBox, bool effect)
        {
            if (textBox == null)
            {
                return;
            }
            await _lock.WaitAsync();
            try
            {
                message += "\n";
                foreach (char c in message)
                {
                    if (textBox.InvokeRequired) 
                    {
                        textBox.Invoke(new Action(() => textBox.AppendText(c.ToString())));
                    }
                    else
                    {
                        textBox.AppendText(c.ToString());
                    }
                    if (effect)
                    {
                        await Task.Delay(15);

                    }
                }
                if (textBox.InvokeRequired) 
                {
                    textBox.Invoke(new Action(() =>
                    {
                        textBox.SelectionStart = textBox.Text.Length;
                        textBox.ScrollToCaret();
                    })); 
                }
                else
                {
                    textBox.SelectionStart = textBox.Text.Length;
                    textBox.ScrollToCaret();
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        /// <summary>
        /// Clears the specified RichTextBox.
        /// Thread-safe and can be called from any thread.
        /// </summary>
        /// <param name="textBox">The RichTextBox to clear.</param>
        /// <returns>A Task representing the asynchronous clear operation.</returns>
        public static async Task ClearMessage(RichTextBox textBox)
        {
            if (textBox == null)
            {
                return;
            }
            await _lock.WaitAsync();
            try
            {
                if (textBox.InvokeRequired) 
                {
                    textBox.Invoke(new Action(() => textBox.Text = ""));
                }
                else
                {
                    textBox.Text = "";
                }
                if (textBox.InvokeRequired)
                {
                    textBox.Invoke(new Action(() =>
                    {
                        textBox.SelectionStart = textBox.Text.Length;
                        textBox.ScrollToCaret();
                    }));
                }
                else
                {
                    textBox.SelectionStart = textBox.Text.Length;
                    textBox.ScrollToCaret();
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        /// <summary>
        /// Adds the given string to the specified ListBox.
        /// Thread-safe and can be called from any thread.
        /// </summary>
        /// <param name="item">The string to add to the ListBox.</param>
        /// <param name="listBox">The ListBox to add the item to.</param>
        /// <returns>A Task representing the asynchronous add operation.</returns>
        public static async Task WriteToList(string item, ListBox listBox)
        {
            if (listBox == null)
            {
                return;
            }
            await _lock.WaitAsync();
            try
            {
                if (listBox.InvokeRequired)
                {
                    listBox.Invoke(new Action(() =>
                    {
                        listBox.Items.Add(item);
                        listBox.TopIndex = listBox.Items.Count - 1;
                    }));
                }
                else
                {
                    listBox.Items.Add(item);
                    listBox.TopIndex = listBox.Items.Count - 1;
                }
            }
            finally
            {
                _lock.Release();
            }
        }


        /// <summary>
        /// Removes the given string from the specified ListBox.
        /// Thread-safe and can be called from any thread.
        /// </summary>
        /// <param name="item">The string to remove from the ListBox.</param>
        /// <param name="listBox">The ListBox to remove the item from.</param>
        public static void RemoveFromList(string item, ListBox listBox)
        {
            if (listBox == null)
            {
                return;
            }
            if (listBox.InvokeRequired)
            {
                listBox.Invoke(new Action(() => { listBox.Items.Remove(item); }));
            }
            else
            {
                listBox.Items.Remove(item);
            }
        }
    }
}
