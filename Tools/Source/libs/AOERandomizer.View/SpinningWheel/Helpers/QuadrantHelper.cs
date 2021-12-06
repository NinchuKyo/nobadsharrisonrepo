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
        /// <summary>
        /// Gets the quadrant the given angle is in.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>The quadrant containing the angle.</returns>
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

        /// <summary>
        /// Returns a point within the given slice dimensions to place text.
        /// </summary>
        /// <param name="radius">The radius of the wheel the text will be in.</param>
        /// <param name="halfAngle">The half-angle of the pie slice.</param>
        /// <returns>The point to place centered text.</returns>
        /// <exception cref="NotSupportedException">Should never throw (unless a 5th quadrant is invented).</exception>
        public static Point Calculate(double radius, double halfAngle)
        {
            Quadrants quadrant = GetQuadrant(halfAngle);
            double quadrantAngle = halfAngle - 90.0 * (int)quadrant;

            double adjacent = Math.Cos(Math.PI / 180.0 * quadrantAngle) * radius;
            double opposite = Math.Sin(Math.PI / 180.0 * quadrantAngle) * radius;

            return quadrant switch
            {
                Quadrants.NE => new(opposite, -1.0 * adjacent),
                Quadrants.SE => new(adjacent, opposite),
                Quadrants.SW => new(-1.0 * opposite, adjacent),
                Quadrants.NW => new(-1.0 * adjacent, -1.0 * opposite),
                _ => throw new NotSupportedException(),
            };
        }
    }
}