using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapesPlayground
{
    public partial class Form1 : Form
    {
        Graphics g;
        int x, y, size, speed;

        private void p(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.FillEllipse(Brushes.Black, x, y, size, size);
        }

        public Form1()
        {
            InitializeComponent();
            x = 0;
            y = 0;
            size = 25;
            speed = 5;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    y -= speed;
                    break;
                case Keys.S:
                    y += speed;
                    break;
                case Keys.A:
                    x -= speed;
                    break;
                case Keys.D:
                    x += speed;
                    break;
            }
            panel1.Invalidate();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
