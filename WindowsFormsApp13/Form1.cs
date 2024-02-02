using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp13
{
    public partial class Form1 : Form
    {
        int columns, rows;
        MyTesttube mytube;
        public Form1()
        {
            InitializeComponent();
            label6.Hide();
            label4.Hide();
            label5.Hide();
            tableLayoutPanel1.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label4.Show();
            label5.Show();
            numericUpDown3.Enabled = false;
            label3.Enabled = false;
            mytube = new MyTesttube(columns * rows, Convert.ToInt32(numericUpDown3.Value), tableLayoutPanel1);
            mytube.SetArr();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            mytube.NextStep();
            tableLayoutPanel1.Refresh();
            mytube.DrawG();
            label5.Text = Convert.ToString(mytube._RealCount);
            button3.BackColor = Color.Transparent;
            if (mytube._RealCount == 0)
            {
                label6.Text = "Все организмы умерли.";
                label6.Show();
                timer1.Stop();
            }
            if (mytube._RealCount == columns * rows)
            {
                label6.Text = "Организмы заняли всю площаль пробирки";
                label6.Show();
                timer1.Stop();
            }
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(!timer1.Enabled)
            {
                timer1.Start();
                button3.BackColor = Color.Green;
            }
            else
            {
                timer1.Stop();
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            label5.Text = Convert.ToString(numericUpDown3.Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(timer2.Enabled)
            {
                timer2.Stop();
                button4.BackColor = Color.Transparent;
            }
            else
            {
                timer2.Start();
                button4.BackColor = Color.Green;
            }    
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(timer2.Enabled)
            {
                mytube.NextStep();
                tableLayoutPanel1.Refresh();
                mytube.DrawG();
                label5.Text = Convert.ToString(mytube._RealCount);
                if (mytube._RealCount == 0)
                {
                    label6.Text = "Все организмы умерли.";
                    label6.Show();
                    button4.BackColor = Color.Transparent;
                    timer2.Stop();
                    timer2.Dispose();
                    
                }
                if (mytube._RealCount == columns * rows)
                {
                    label6.Text = "Организмы заняли всю площаль пробирки";
                    label6.Show();
                    button4.BackColor = Color.Transparent;
                    timer2.Stop();
                    timer2.Dispose();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                    "Колач Андрей 2221",
                    "Автор",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mytube = null;
            this.Controls.Clear();
            InitializeComponent();
            tableLayoutPanel1.Hide();
            label6.Hide();
            label4.Hide();
            label5.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Show();
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            label1.Enabled = false;
            label2.Enabled = false;
            columns = Convert.ToInt32(numericUpDown1.Value);
            rows = Convert.ToInt32(numericUpDown2.Value);
            while (tableLayoutPanel1.ColumnCount != columns)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
                tableLayoutPanel1.ColumnCount++;
            }
            while (tableLayoutPanel1.RowCount != rows)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle());
                tableLayoutPanel1.RowCount++;
            }
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                tableLayoutPanel1.ColumnStyles[i].SizeType = SizeType.Absolute;
                tableLayoutPanel1.ColumnStyles[i].Width = tableLayoutPanel1.Width / columns;
            }
            {
                for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
                {
                    tableLayoutPanel1.RowStyles[i].SizeType = SizeType.Absolute;
                    tableLayoutPanel1.RowStyles[i].Height = tableLayoutPanel1.Height / rows;
                }
            }
        }
    }
}
