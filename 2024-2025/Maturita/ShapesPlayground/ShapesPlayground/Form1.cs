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
        Brush brush;
        Graphics g;
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Locks the size of the form
        }

        private void drawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            positionLabel.Text = $"({e.X}, {e.Y})";
        }

        private void drawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void drawingPanel_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

        }
    }
}
