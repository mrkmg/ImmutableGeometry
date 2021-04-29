using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmutableGeometry
{

    /// <summary>
    /// Represents two <see cref="Point"/>s, connected by a <see cref="Vector"/>
    /// </summary>
    public class Line
    {
        private IReadOnlyList<Point> _points;

        /// <summary>
        /// The starting point of the line.
        /// </summary>
        public readonly Point Start;
        
        /// <summary>
        /// The end point of the line.
        /// </summary>
        public readonly Point End;
        
        /// <summary>
        /// A vector from the start to the end.
        /// </summary>
        public readonly Vector Delta;

        /// <summary>
        /// A list of points the line crosses. Uses Bresenham's algorithm.
        /// <remarks>
        /// The points are generated upon first access, then cached.
        /// </remarks>
        /// </summary>
        public IReadOnlyList<Point> Points => _points ??= GeneratePointsList();

        public Line(int startX, int startY, int endX, int endY)
            : this(new Point(startX, startY), new Point(endX, endY)) { }

        public Line(Point start, Point end)
            : this(start, end, null) { }

        public Line(Point start, Vector delta)
            : this(start, delta, null) { }

        private Line(Point start, Point end, IReadOnlyList<Point> points = null)
            : this(start, end, end - start, points) { }

        private Line(Point start, Vector delta, IReadOnlyList<Point> points = null)
            : this(start, start + delta, delta, points) { }
        
        private Line(Point start, Point end, Vector delta, IReadOnlyList<Point> points = null)
        {
            Start = start;
            End = end;
            Delta = delta;
            _points = points;
        }

        /// <summary>
        /// Make a new line translated by an radians.
        /// </summary>
        /// <param name="x">Amount to move the line in the x-axis.</param>
        /// <param name="y">Amount to move the line in the y-axis.</param>
        /// <returns>The translated line</returns>
        public Line Translate(int x, int y) => Translate(new Vector(x, y));

        /// <summary>
        /// Make a new line translated by an radians.
        /// </summary>
        /// <param name="amount">A vector representing how must to translate the line.</param>
        /// <returns>The translated line.</returns>
        public Line Translate(Vector amount)
            => new(Start + amount, End + amount, Delta, _points?.Translate(amount).ToList().AsReadOnly());

        /// <summary>
        /// Make a new line with the X and Y components transposed.
        /// </summary>
        /// <returns>The transposed line.</returns>
        public Line Transpose()
            => new(Start.Transpose(), End.Transpose(), _points?.Transpose().ToList().AsReadOnly());

        /// <summary>
        /// Make a new line which is rotated about the Start point.
        /// </summary>
        /// <param name="radians">The amount to in radians to rotate.</param>
        /// <returns>The rotated line.</returns>
        public Line Rotate(double radians)
            => new(Start, Delta.Rotate(radians));

        
        /// <summary>
        /// Determine if this line intersects another line.
        /// </summary>
        /// <param name="other">The other line.</param>
        /// <returns>If the lines intersect.</returns>
        public bool IntersectsWith(Line other)
        {
            static int Orientation(Point a, Point b, Point c)
            {
                var val = (b.Y - a.Y) * (c.X - b.X) - (b.X - a.X) * (c.Y - b.Y);
                return val == 0 ? 0 : val > 0 ? 1 : -1;
            }

            static bool OnSegment(Point p, Point q, Point r)
                => q.X <= Math.Max(p.X, r.X) && 
                   q.X >= Math.Min(p.X, r.X) &&
                   q.Y <= Math.Max(p.Y, r.Y) && 
                   q.Y >= Math.Min(p.Y, r.Y);

            var o1 = Orientation(Start, End, other.Start);
            var o2 = Orientation(Start, End, other.End);
            var o3 = Orientation(other.Start, other.End, Start);
            var o4 = Orientation(other.Start, other.End, End);

            if (o1 != o2 && o3 != o4)
                return true;

            return o1 == 0 && OnSegment(Start, other.Start, End) ||
                   o2 == 0 && OnSegment(Start, other.End, End) ||
                   o3 == 0 && OnSegment(other.Start, Start, other.End) ||
                   o4 == 0 && OnSegment(other.Start, End, other.End);
        }

        public static explicit operator Line(Vector vector)
            => new(Point.Zero, vector);

        public static bool operator ==(Line left, Line right)
            => Equals(left, right);

        public static bool operator !=(Line left, Line right)
            => !Equals(left, right);

        public bool Equals(Line other)
            => Start == other.Start && End == other.End;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Line) obj);
        }

        public override int GetHashCode()
            => HashCode.Combine(Start, End);

        public override string ToString()
            => $"Line<({Start.X},{Start.Y})->({End.X},{End.Y})>";

        private IReadOnlyList<Point> GeneratePointsList()
        {
            if (Delta.X == 0 || Delta.Y == 0 || Math.Abs(Delta.X) == Math.Abs(Delta.Y))
            {
                return GenerateOctalSlope();
            }

            var flipVector = new Vector(Delta.X < 0 ? -1 : 1, Delta.Y < 0 ? -1 : 1);
            var target = (Point) Delta * flipVector;
            var swap = target.Y > target.X;
            var offset = (Vector) Start;
            var list = new List<Point>(target.X + 1);
            list.AddRange(GenerateShallowPositiveSlopeLine(swap ? target.Transpose() : target)
                    .Select(point => (swap ? point.Transpose() : point) * flipVector + offset));

            return list.AsReadOnly();
        }

        private IReadOnlyList<Point> GenerateOctalSlope()
        {
            var list = new List<Point>(Math.Abs(Math.Max(Delta.X, Delta.Y)) + 1);
            var offset = new Vector(
                Delta.X == 0 ? 0 : Delta.X > 0 ? 1 : -1,
                Delta.Y == 0 ? 0 : Delta.Y > 0 ? 1 : -1
            );
            var point = Start;
            while (point != End)
            {
                list.Add(point);
                point += offset;
            }
            list.Add(End);
            return list.AsReadOnly();
        }

        private static IEnumerable<Point> GenerateShallowPositiveSlopeLine(Point target)
        {
            var difference = 2 * target.Y - target.X;
            var y = 0;
            for (var x = 0; x <= target.X; x++)
            {
                yield return new Point(x, y);
                while (difference > 0)
                {
                    y++;
                    difference -= 2 * target.X;
                }
                difference += 2 * target.Y;
            }
        }
    }
}