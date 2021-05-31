using System;

namespace PointExample
{
    public enum CoordinateSystem
    {
        Cartesian, Polar
    }

    public class Point
    {
        private double x, y;

        /// <summary>
        /// Initializes a point from either cartesian or polar
        /// </summary>
        /// <param name="a">x if cartesian, rho if polar</param>
        /// <param name="b"></param>
        /// <param name="system"></param>
        public Point(double a, double b, CoordinateSystem system)
        {
            switch (system)
            {
                case CoordinateSystem.Cartesian:
                    this.x = a;
                    this.y = b;
                    break;
                case CoordinateSystem.Polar:
                    this.x = a * Math.Cos(b);
                    this.y = a * Math.Sin(b);
                    break;
                default:
                    throw new ArgumentOutOfRangeException;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
