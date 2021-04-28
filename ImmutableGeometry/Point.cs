using System;

namespace ImmutableGeometry
{
    /// <summary>
    /// Represents an X and Y
    /// </summary>
    public readonly struct Point : IEquatable<Point>
    {
        public static Point Zero = new();
        
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point Translate(int x, int y)
            => new(X + x, Y + y);

        public Point Transpose()
            => new(Y, X);

        public static implicit operator Point(Size size) 
            => new(size.Width, size.Height);

        public static explicit operator Point(Rectangle rectangle)
            => rectangle.Position;

        public static implicit operator Point(Vector vector) =>
            new(vector.X, vector.Y);
        
        public static implicit operator Point(ValueTuple<int, int> tuple) =>
            new(tuple.Item1, tuple.Item2);

        public static Rectangle operator &(Point point, Size size)
            => new(point, size);

        public static Point operator +(Point point, Vector vector)
            => new(point.X + vector.X, point.Y + vector.Y);

        public static Point operator -(Point point, Vector vector)
            => new(point.X - vector.X, point.Y - vector.Y);

        public static Point operator *(Point point, Vector vector)
            => new(point.X * vector.X, point.Y * vector.Y);

        public static Point operator -(Point point)
            => new(-point.X, -point.Y);

        public static Vector operator +(Point from, Point to)
            => new(to.X - from.X, to.Y - from.Y);

        public static Vector operator -(Point to, Point from)
            => new(to.X - from.X, to.Y - from.Y);

        public static bool operator ==(Point a, Point b)
            => a.X == b.X && a.Y == b.Y;
        
        public static bool operator !=(Point a, Point b)
            => a.X != b.X || a.Y != b.Y;

        public static Line operator &(Point point, Vector vector) 
            => new (point, vector);

        public bool Equals(Point other) 
            => X == other.X && Y == other.Y;

        public override bool Equals(object obj) 
            => obj is Point other && Equals(other);

        public override int GetHashCode() 
            => HashCode.Combine(X, Y);

        public override string ToString() => $"Point<{X},{Y}>";
    }
}