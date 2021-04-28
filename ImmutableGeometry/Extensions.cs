using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmutableGeometry
{
    public static class Extensions
    {
        public static Rectangle Bounds(this IEnumerable<Point> points)
        {
            var minX = int.MaxValue;
            var maxX = int.MinValue;
            var minY = int.MaxValue;
            var maxY = int.MinValue;
            foreach (var point in points)
            {
                if (point.X < minX) minX = point.X;
                if (point.X > maxX) maxX = point.X;
                if (point.Y < minY) minY = point.Y;
                if (point.Y > maxY) maxY = point.Y;
            }
            return Rectangle.FromLTRB(minX, minY, maxX, maxY);
        }

        public static IEnumerable<Point> AsPoints(this IEnumerable<ValueTuple<int, int>> tuples) 
            => tuples.Select(p => (Point) p);

        public static IEnumerable<Point> Translate(this IEnumerable<Point> points, Vector amount)
            => points.Select(p => p + amount);
        
        public static IEnumerable<Point> Translate(this IEnumerable<Point> points, int x, int y)
            => points.Select(p => p.Translate(x, y));

        public static IEnumerable<Point> Scale(this IEnumerable<Point> points, double scaleX, double scaleY) =>
            points.Select(c =>
                new Point((int) (c.X * scaleX), (int) (c.Y * scaleY)));

        /// <summary>
        /// Rotates a set of points by <c>amount</c> radians about 0,0
        /// <remarks>
        /// Because of int rounding, this will likely result in the "shape" of the points to change.
        /// May result in duplicate points.
        /// </remarks>
        /// </summary>
        public static IEnumerable<Point> Rotate(this IEnumerable<Point> points, double amount)
            => points
                .Select(p => (Vector) p)
                .Select(v => v.Rotate(amount))
                .Select(v => (Point) v);
        
        // TODO: not use linq, could be used in hot-path
        public static IEnumerable<Point> Mirror(this IEnumerable<Point> points, bool xAxis, bool yAxis)
            => points.Select(v => new Point(xAxis ? -v.X : v.X, yAxis ? -v.Y : v.Y));

        public static IEnumerable<Point> Sort(this IEnumerable<Point> points)
            => points
                .GroupBy(p => p.Y)
                .OrderBy(g => g.Key)
                .Select(g => g.OrderBy(p => p.X))
                .SelectMany(p => p);
    }
}