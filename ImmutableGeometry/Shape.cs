using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmutableGeometry
{
    public class Shape : IEquatable<Shape>
    {
        private static readonly Vector[] Winding = {new (0, -1), new (-1, 0), new (0, 1), new (1, 0)};
        
        private IReadOnlyList<Point> _edgePoints;
        private IReadOnlyList<Point> _points;
        private IReadOnlyList<Line> _outlines;
        
        public readonly IReadOnlyList<Point> Corners;
        public Rectangle Bounds;
        
        public IReadOnlyList<Point> EdgePoints => _edgePoints ??= GenerateEdgePoints().ToList().AsReadOnly();
        public IReadOnlyList<Point> Points => _points ??= GeneratePoints().ToList().AsReadOnly();
        public IReadOnlyList<Line> Outlines => _outlines ??= GenerateLines().ToList().AsReadOnly();

        public Shape(params (int X, int Y)[] xyValueTuples) 
            : this(xyValueTuples.Select(t => new Point(t.X, t.Y))) { }

        public Shape(IEnumerable<Point> corners)
        {
            var cornersArray = corners.ToArray();
            var startPointIndex = 0;
            var startPoint = cornersArray[0];
            for (var i = 1; i < cornersArray.Length; i++)
            {
                var p = cornersArray[i];
                if (p.X >= startPoint.X && (p.X != startPoint.X || p.Y >= startPoint.Y)) continue;
                startPointIndex = i;
                startPoint = p;
            }

            if (startPointIndex > 0)
            {
                var tempArray = new Point[cornersArray.Length];
                var count = cornersArray.Length - startPointIndex;
                Array.Copy(cornersArray, startPointIndex, tempArray, 0, count);
                Array.Copy(cornersArray, 0, tempArray, count, startPointIndex);
                Corners = tempArray.ToList().AsReadOnly();
            }
            else Corners = cornersArray.ToList().AsReadOnly();
            Bounds = Corners.Bounds();
        }
        
        public Shape Scale(double amount) => Scale(amount, amount);
        public Shape Scale(Vector amount) => Scale(amount.X, amount.Y);
        public Shape Scale(double x, double y) 
            => new (Corners.Translate(-Bounds.Center.X, -Bounds.Center.Y)
                .Scale(x, y)
                .Translate(Bounds.Center.X, Bounds.Center.Y));


        public Shape Translate(int x, int y) => Translate(new Vector(x, y));
        public Shape Translate(Vector amount) 
            => new (Corners.Translate(amount));

        public Shape Rotate(double amount)
            => new (Corners.Translate(-Bounds.Center.X, -Bounds.Center.Y)
                .Rotate(amount)
                .Translate(Bounds.Center.X, Bounds.Center.Y));

        public Shape Mirror(bool xAxis, bool yAxis)
            => new (Corners.Mirror(xAxis, yAxis));

        public bool IntersectsWith(Shape other)
        {
            // Use the cheaper bounds check first
            if (!Bounds.IntersectsWith(other.Bounds)) return false;
            if (Bounds.Area > other.Bounds.Area)
            {
                var myHash = Points.ToHashSet();
                return other.Points.Any(p => myHash.Contains(p));
            }

            var otherHash = other.Points.ToHashSet();
            return Points.Any(p => otherHash.Contains(p));
        }

        public bool Equals(Shape other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Corners.Count != other.Corners.Count) return false;
            for (var i = 0; i < Corners.Count; i++)
                if (Corners[i] != other.Corners[i])
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Shape) obj);
        }

        public override int GetHashCode()
        {
            var hash = Corners[0].GetHashCode();
            for (var i = 1; i < Corners.Count; i++) hash = HashCode.Combine(hash, Corners[i].GetHashCode());
            return hash;
        }

        public override string ToString() => "Shape";

        // this is probably a terrible way of doing this, but should support
        // any type of shape (convex, self-intersecting, anything)
        private IEnumerable<Point> GeneratePoints()
        {
            var remainingPoints = new HashSet<Point>(Bounds.Points());
            var points = new Point[Bounds.Area];
            var pointsValidLength = 0;
            
            foreach (var point in EdgePoints)
            {
                remainingPoints.Remove(point);
                points[pointsValidLength++] = point;
            }

            while (remainingPoints.Count > 0)
            {
                var currentOpenNodes = new Queue<Point>();
                var escaped = false;
                var pointsTempLength = pointsValidLength;
                var startPoint = remainingPoints.First();
                
                remainingPoints.Remove(startPoint);
                currentOpenNodes.Enqueue(startPoint);
                
                while (currentOpenNodes.Count > 0)
                {
                    var node = currentOpenNodes.Dequeue();
                    points[pointsTempLength++] = node;
                    
                    foreach (var v in Winding)
                    {
                        var c = node + v;
                        if (!Bounds.Contains(c))
                        {
                            escaped = true;
                            continue;
                        }
                        if (!remainingPoints.Contains(c)) continue;
                        remainingPoints.Remove(c);
                        currentOpenNodes.Enqueue(c);
                    }
                }

                if (!escaped) pointsValidLength = pointsTempLength;
            }

            var finalPoints = new Point[pointsValidLength];
            Array.Copy(points, 0, finalPoints, 0, pointsValidLength);
            return finalPoints;
        }

        private IEnumerable<Line> GenerateLines()
        {
            for (var i = 0; i < Corners.Count; i++)
            {
                var point = Corners[i];
                var nextPoint = Corners[(i + 1) % Corners.Count];
                // yield return GetLine(point, nextPoint);
                var outline = point & (nextPoint - point);
                yield return outline;
            }
        }

        private IEnumerable<Point> GenerateEdgePoints() 
            => Outlines.Select(line => line.Points.SkipLast(1)).SelectMany(p => p);
    }
}