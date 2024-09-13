using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintApp
{
    public partial class Form1 : Form
    {
        Graphics g;
        int x = -1;
        int y = -1;
        bool moving = false;
        Pen pen;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            float penWidth = float.Parse(comboBox1.Text);
            pen = new Pen(Color.Black, penWidth);
            for (int i = 1; i < 12; i+=1)
            {
                comboBox1.Items.Add(i);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(moving && x != 1 && y != 1)
            {
                g.DrawLine(pen, new Point(x, y), e.Location);
                x=e.X; 
                y=e.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            x = e.X;
            y = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            x = -1;
            y = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button2.ForeColor = colorDialog1.Color;
                button2.BackColor = colorDialog1.Color;
                if(comboBox2.SelectedIndex != 1)
                {
                    pen.Color = colorDialog1.Color;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pen.Width = float.Parse(comboBox1.Text);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                pen.Color = colorDialog1.Color;
            }
            if (comboBox2.SelectedIndex == 1)
            {
                pen.Color = Color.White;
            }
        }
    }
}
