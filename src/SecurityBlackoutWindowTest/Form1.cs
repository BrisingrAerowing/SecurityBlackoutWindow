using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecurityBlackoutWindowTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            IntPtr hParent = (checkBox1.Checked) ? IntPtr.Zero : this.Handle;

            SecurityBlackoutWindow.BlackoutWindow.Blackout(hParent, () => MessageBox.Show("Testing", "Testing", MessageBoxButtons.OK, MessageBoxIcon.Information));

        }
    }
}
