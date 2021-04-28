using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmutableGeometry
{
    public class Line
    {
        private IReadOnlyList<Point> _points;

        public readonly Point Start;
        public readonly Point End;
        public readonly Vector Delta;

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

        public Line Translate(int x, int y) => Translate(new Vector(x, y));

        public Line Translate(Vector amount)
            => new(Start + amount, End + amount, Delta, _points?.Translate(amount).ToList().AsReadOnly());

        public Line Transpose()
            => new(Start.Transpose(), End.Transpose(), _points?.Transpose().ToList().AsReadOnly());

        // Rotates about start point
        public Line Rotate(double amount)
            => new(Start, Delta.Rotate(amount));

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