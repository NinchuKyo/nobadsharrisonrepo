using AOERandomizer.View.SpinningWheel.Enums;
using System;
using System.Windows;

namespace AOERandomizer.View.SpinningWheel.Helpers
{
    /// <summary>
    /// Helper utility for quadrants.
    /// </summary>
    public static class QuadrantHelper
    {
        public static Quadrants GetQuadrant(double angle)
        {
            if (angle <= 90.0)
            {
                return Quadrants.NE;
            }
            else if (angle <= 180.0)
            {
                return Quadrants.SE;
            }
            else if (angle <= 270.0)
            {
                return Quadrants.SW;
            }
            else
            {
                return Quadrants.NW;
            }
        }

        public static Point Calculate(double radius, double startAngle, double angle)
        {
            double halfAngle = startAngle + angle / 2.0;
            Quadrants quadrant = GetQuadrant(halfAngle);
            double quadrantAngle = halfAngle - 90.0 * (int)quadrant;

            double adjacent = Math.Cos(Math.PI / 180.0 * quadrantAngle) * radius;
            double opposite = Math.Sin(Math.PI / 180.0 * quadrantAngle) * radius;

            return quadrant switch
            {
                Quadrants.NE => new Point(opposite, -1.0 * adjacent),
                Quadrants.SE => new Point(adjacent, opposite),
                Quadrants.SW => new Point(-1.0 * opposite, adjacent),
                Quadrants.NW => new Point(-1.0 * adjacent, -1.0 * opposite),
                _ => throw new NotSupportedException(),
            };
        }

        public static double GetAngle(Point touchPoint, Size circleSize)
        {
            double x = touchPoint.X - (circleSize.Width / 2.0);
            double y = circleSize.Height - touchPoint.Y - (circleSize.Height / 2.0);

            double hypot = Math.Sqrt(x * x + y * y);
            double value = Math.Asin(y / hypot) * 180.0 / Math.PI;

            if (x >= 0.0)
            {
                return 90.0 - value;
            }
            else
            {
                return 270.0 + value;
            }
        }
    }
}