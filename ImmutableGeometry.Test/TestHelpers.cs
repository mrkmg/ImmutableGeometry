using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ImmutableGeometry.Test
{
    public static class TestHelpers
    {
        public static void DebugShape(Shape shape)
        {
            TestContext.WriteLine("==Corners==");
            LogPoints(shape.Corners);
            TestContext.WriteLine("==EdgePoints==");
            LogPoints(shape.EdgePoints);
            TestContext.WriteLine("==Points==");
            LogPoints(shape.Points.Sort());
        }

        public static void DebugLine(Line line)
        {
            
            TestContext.WriteLine($"==Start,End==\n{line.Start},{line.End}");
            TestContext.WriteLine($"==Points==");
            LogPoints(line.Points);
        }
        
        private static void LogPoints(IEnumerable<Point> points)
        {
            TestContext.Write(string.Join(", ", points.Select(p => $"({p.X},{p.Y})")));
            TestContext.Write("\n");
        }
    }
}