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
    public partial class Form1 : Form
    {
        static SJF sjf = new SJF();
        int pid = 1;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "Process " + pid;
            textBox1.Enabled = false;
        }

        private void btnAddJob_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                if (checkContents())
                {
                        addToGrid();
                }
            }
            else
            {
                MessageBox.Show("Fill all fields.");
            }
            

            
        }

        public bool checkContents()
        {
            bool two = true, three = true; int i = 0;
            while (i < textBox2.Text.Length)
            {
                if (Char.IsLetter(textBox2.Text[i]))
                {
                    two = false;
                    break;
                }
                i++;
            }
            i = 0;
            while (i < textBox3.Text.Length)
            {
                if (Char.IsLetter(textBox3.Text[i]))
                {
                    three = false;
                    break;
                }
                i++;
            }

            if (two && three)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Input only numbers in arrival time and burst time");
                textBox2.ResetText();
                textBox3.ResetText();
                return false;
            }
        }

        public void addToGrid()
        {
            DialogResult dr = MessageBox.Show(null, "Add the process?", "ADD", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                sjf.addData(pid, int.Parse(textBox2.Text), int.Parse(textBox3.Text));
                dataGridView1.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text);
                pid++;
                textBox1.Text = "Process " + pid;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            sjf.scheduleIt();
        }
    }
}
