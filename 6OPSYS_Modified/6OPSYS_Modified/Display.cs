using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6OPSYS_Modified
{
    public partial class Display : Form
    {
        public Display()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        public void addtoGrid(object pid, object bt)
        {
            dataGridView1.Rows.Add(pid,bt);
        }
    }
}
