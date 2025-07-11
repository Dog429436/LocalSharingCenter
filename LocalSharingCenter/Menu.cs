using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalSharingCenter
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void ClientButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
        private void ServerButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void WelcomeLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
