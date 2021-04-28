using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ImmutableGeometry.Test
{
    public static class TestHelpers
    {
        public static Point[] Points(params (int, int)[] points)
            => points.AsPoints().ToArray();
        
        public static void Debug(Shape shape)
        {
            TestContext.WriteLine("==Corners==");
            InternalListPoints(shape.Corners);
            TestContext.WriteLine("==EdgePoints==");
            InternalListPoints(shape.EdgePoints);
            TestContext.WriteLine("==Points==");
            InternalListPoints(shape.Points.Sort());
            throw new InconclusiveException("Debug Data, not a test\n");
        }

        public static void Debug(Line line)
        {
            TestContext.WriteLine($"==Start,End==\n{line.Start},{line.End}");
            TestContext.WriteLine($"==Points==");
            InternalListPoints(line.Points);
            throw new InconclusiveException("Debug Data, not a test\n");
        }
        
        public static void Debug(IEnumerable<Point> points)
        {
            TestContext.WriteLine("==Points==");
            InternalListPoints(points);
            throw new InconclusiveException("Debug Data, not a test\n");
        }

        private static void InternalListPoints(IEnumerable<Point> points)
        {
            TestContext.Write(string.Join(", ", points.Select(p => $"({p.X},{p.Y})")));
            TestContext.Write("\n");
        }
    }
}