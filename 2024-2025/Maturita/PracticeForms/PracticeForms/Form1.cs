using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeForms
{
    public partial class Form1 : Form
    {
        Graphics g;
        Circle cRed, cBlue;
        int x, y, size;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.A):
                    cRed.Decrease(5);
                    cBlue.Decrease(5);
                    break;
                case (Keys.D):
                    cRed.Increase(5);
                    cBlue.Increase(5);
                    break;
            }
            panelCircles.Invalidate();
        }

        public Form1()
        {
            InitializeComponent();
            x = 100;
            y = 100;
            size = 20;
            cRed = new Circle(x, y, size, Brushes.Red);
            cBlue = new Circle(panelCircles.Width - x, panelCircles.Height - y, size, Brushes.Blue);
        }

        private void panelCircles_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            g.FillEllipse(cRed.col, cRed.x, cRed.y, cRed.size, cRed.size);
            g.FillEllipse(cBlue.col, cBlue.x, cBlue.y, cBlue.size, cBlue.size);
        }
    }
}
