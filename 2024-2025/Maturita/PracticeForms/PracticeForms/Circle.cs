using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PracticeForms
{
    internal class Circle
    {
        public int x, y, size;
        public Brush col;
        public Rectangle rect;
        public Circle(int x, int y, int size, Brush col)
        {
            this.x = x;
            this.y = y;
            this.size = size;
            this.col = col;
        }
        public void Increase(int value)
        {
            this.size += value;
        }
        public void Decrease(int value)
        {
            this.size -= value;
        }
    }
}
