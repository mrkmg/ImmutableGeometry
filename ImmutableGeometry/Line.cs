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
        
        public IReadOnlyList<Point> Points => _points ??= GeneratePoints().ToList().AsReadOnly();

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
            Delta = end - start;
        }

        public Line(Point start, Vector delta)
        {
            Start = start;
            End = start + delta;
            Delta = delta;
        }

        public Line Translate(int x, int y) => Translate(new Vector(x, y));
        public Line Translate(Vector amount)
            => new (Start + amount, Delta);
        
        // Rotates about start point
        public Line Rotate(double amount)
            => new (Start, Delta.Rotate(amount));

        public static explicit operator Line(Vector vector)
            => new (Point.Zero, vector);

        public static bool operator ==(Line left, Line right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Line left, Line right)
        {
            return !Equals(left, right);
        }
        
        protected bool Equals(Line other)
        {
            return Start == other.Start && End == other.End;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return this == (Line)obj;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

        public override string ToString() => $"Line<({Start}),({End})>";

        private IEnumerable<Point> GeneratePoints()
        {
            if (Delta.X == 0 || Delta.Y == 0 || Math.Abs(Delta.X) == Math.Abs(Delta.Y))
            {
                var offset =  new Vector(
                    Delta.X == 0 ? 0 : Delta.X > 0 ? 1 : -1, 
                    Delta.Y == 0 ? 0 : Delta.Y > 0 ? 1 : -1
                );
                var point = Start;
                while (point != End)
                {
                    yield return point;
                    point += offset;
                }
                yield return End;
                yield break;
            }
            
            
            var flipX = Delta.X < 0;
            var flipY = Delta.Y < 0;
            var target = new Point(flipX ? -Delta.X : Delta.X, flipY ? -Delta.Y : Delta.Y);
            var swap = target.Y > target.X;
            if (swap)
                target = new Point(target.Y, target.X);

            var difference = 2 * target.Y - target.X;
            var y = 0;
            for (var x = 0; x <= target.X; x++)
            {
                yield return (swap, flipX, flipY) switch
                {
                    (true, true, true) => new Point(Start.X - y, Start.Y - x),
                    (true, true, false) => new Point(Start.X - y, Start.Y + x),
                    (true, false, true) => new Point(Start.X + y, Start.Y - x),
                    (true, false, false) => new Point(Start.X + y, Start.Y + x),
                    (false, true, true) => new Point(Start.X - x, Start.Y - y),
                    (false, true, false) => new Point(Start.X - x, Start.Y + y),
                    (false, false, true) => new Point(Start.X + x, Start.Y - y),
                    (false, false, false) => new Point(Start.X + x, Start.Y + y)
                };
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