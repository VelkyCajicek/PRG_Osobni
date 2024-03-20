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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PaintApp
{
    public partial class Form1 : Form
    {
        Graphics g;
        int x = -1;
        int y = -1;
        bool moving = false;
        Pen pencil;
        SolidBrush pen;
        SolidBrush eraser;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            float penWidth = float.Parse(comboBox1.Text);
            pencil = new Pen(Color.Black, penWidth);
            eraser = new SolidBrush(Color.White);
            pen = new SolidBrush(Color.Black);
            for (int i = 2; i < 76; i += 4)
            {
                comboBox1.Items.Add(i);
            }
            this.Cursor = Cursors.WaitCursor;
            panel1.Cursor = new Cursor("Pencil.cur");
            SizeLabel.Text = $" □ {panel1.Width} x {panel1.Height} px ";
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            float penWidth = float.Parse(comboBox1.Text)/2;
            PositionLabel.Text = $"➣ {e.X} x {e.Y} px";
            if (moving && x != 1 && y != 1 && comboBox2.SelectedIndex == 0)
            {
                g.DrawLine(pencil, new Point(x, y), e.Location);
                x=e.X; 
                y=e.Y;
            }
            if (moving && x != 1 && y != 1 && comboBox2.SelectedIndex == 1)
            {
                g.FillEllipse(eraser, e.X - penWidth, e.Y - penWidth, penWidth, penWidth);
                x = e.X;
                y = e.Y;
            }
            if (moving && x != 1 && y != 1 && comboBox2.SelectedIndex == 2)
            {
                g.FillEllipse(pen, e.X - penWidth, e.Y - penWidth, penWidth, penWidth);
                x = e.X;
                y = e.Y;
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
                    pencil.Color = colorDialog1.Color;
                    pen.Color = colorDialog1.Color;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pencil.Width = float.Parse(comboBox1.Text);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0: panel1.Cursor = new Cursor("Pencil.cur"); break;
                case 1: panel1.Cursor = new Cursor("Eraser.cur"); break;
                case 2: panel1.Cursor = new Cursor("Pen.cur"); break;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            panel1.Refresh();
        }
    }
}
