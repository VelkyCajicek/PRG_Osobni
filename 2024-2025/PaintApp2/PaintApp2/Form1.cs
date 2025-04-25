using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PaintApp2
{
    public partial class Form1 : Form
    {
        Bitmap bm;
        Graphics g;
        Graphics bmGraphics; // Mirror to bitmap so screen doesn't glitch out

        int x = -1;
        int y = -1;
        bool moving = false;
        Pen pencil;
        SolidBrush pen;
        SolidBrush eraser;
        int width;
        int height;

        public Form1()
        {
            InitializeComponent();
            width = drawingPanel.Size.Width;
            height = drawingPanel.Size.Height;
            bm = new Bitmap(width, height);
            g = drawingPanel.CreateGraphics();
            bmGraphics = Graphics.FromImage(bm); // Graphics for the Bitmap

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            for (int i = 2; i < 76; i += 4)
            {
                sizeComboBox.Items.Add($"{i}");
            }

            pencil = new Pen(Color.Black);
            eraser = new SolidBrush(Color.White);
            pen = new SolidBrush(Color.Black);
        }

        private void drawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            float penWidth = float.Parse(sizeComboBox.Text) / 2;
            mousePositionLabel.Text = $"({e.X}, {e.Y})";
            if (moving && x != -1 && y != -1)
            {
                if (utensilComboBox.SelectedIndex == 0) // Pencil
                {
                    g.DrawLine(pencil, new Point(x, y), e.Location);
                    bmGraphics.DrawLine(pencil, new Point(x, y), e.Location);
                }
                else if (utensilComboBox.SelectedIndex == 1) // Eraser
                {
                    g.FillEllipse(eraser, e.X - penWidth, e.Y - penWidth, penWidth, penWidth);
                    bmGraphics.FillEllipse(eraser, e.X - penWidth, e.Y - penWidth, penWidth, penWidth); 
                }
                else if (utensilComboBox.SelectedIndex == 2) // Brush
                {
                    g.FillEllipse(pen, e.X - penWidth, e.Y - penWidth, penWidth, penWidth);
                    bmGraphics.FillEllipse(pen, e.X - penWidth, e.Y - penWidth, penWidth, penWidth); 
                }
                x = e.X;
                y = e.Y;
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color color = colorDialog1.Color;
                pencil.Color = color;
                pen.Color = color;
                colorButton.ForeColor = color;
            }
        }

        private void drawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            x = e.X;
            y = e.Y;
        }

        private void drawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            x = -1;
            y = -1;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Bitmap Image (*.bmp)|*.bmp|GIF Image (*.gif)|*.gif|JPEG Image (*.jpeg)|*.jpeg" +
                        "|PNG Image (*.png)|*.png|TIFF Image (*.tiff)|*.tiff|WMF Image (*.wmf)|*.wmf";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string path = sf.FileName;
                switch (path.Split('.').Last().ToLower())
                {
                    case "bmp": bm.Save(path, ImageFormat.Bmp); break;
                    case "gif": bm.Save(path, ImageFormat.Gif); break;
                    case "jpeg": bm.Save(path, ImageFormat.Jpeg); break;
                    case "png": bm.Save(path, ImageFormat.Png); break;
                    case "tiff": bm.Save(path, ImageFormat.Tiff); break;
                    case "wmf": bm.Save(path, ImageFormat.Wmf); break;
                }
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Bitmap Image (*.bmp)|*.bmp|GIF Image (*.gif)|*.gif|JPEG Image (*.jpeg)|*.jpeg" +
                        "|PNG Image (*.png)|*.png|TIFF Image (*.tiff)|*.tiff|WMF Image (*.wmf)|*.wmf";
            if(of.ShowDialog() == DialogResult.OK)
            {
                drawingPanel.BackgroundImage = Image.FromFile(of.FileName); // Sets it as the background image
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            bmGraphics.Clear(Color.White);
        }
    }
}