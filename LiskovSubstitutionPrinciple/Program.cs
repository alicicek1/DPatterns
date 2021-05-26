using System;
using static System.Console;

namespace LiskovSubstitutionPrinciple
{
    public class Rectangle
    {
        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        public override int Width
        {
            set
            {
                base.Width = base.Height = value;
            }
        }

        public override int Height
        {
            set
            {
                base.Height = base.Width = value;
            }
        }
    }

    class Program
    {
        static public int Area(Rectangle r) => r.Height * r.Width;
        static void Main(string[] args)
        {

            Rectangle rc = new Rectangle(2, 3);
            WriteLine($"{rc} has area {Area(rc)}");

            Rectangle sq = new Square();
            sq.Width = 4;
            WriteLine($"{sq} has area {Area(sq)}");

        }
    }
}
