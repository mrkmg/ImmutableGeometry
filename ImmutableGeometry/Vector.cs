using System;

namespace ImmutableGeometry
{
    /// <summary>
    /// Represents a change in X and Y
    /// </summary>
    public readonly struct Vector : IEquatable<Vector>
    {
        public readonly int X;
        public readonly int Y;
        public double UnitX => !IsZero ? X / Magnitude : 0;
        public double UnitY => !IsZero ? Y / Magnitude : 0;
        public double Magnitude => Math.Sqrt(X*X + Y*Y);
        public double Angle => !IsZero ? Math.Atan2(Y, X) : throw new InvalidOperationException("vector is zero-length");
        public bool IsZero => this == default;

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector FromPolar(double angle, double length)
            => new((int) (length * Math.Cos(angle)), (int) (length * Math.Sin(angle)));
        
        public Vector Rotate(double angle)
            => FromPolar(Angle + angle, Magnitude + 0.0000001d); // adding a small delta to deal with doubles precision

        public Vector Extend(double amount)
            => FromPolar(Angle, Magnitude + amount);

        public Vector Shrink(double amount)
            => FromPolar(Angle, Magnitude - amount);

        public Vector Scale(double amount)
            => FromPolar(Angle, Magnitude * amount);

        public int Cross(Vector other) 
            => X * other.Y - Y * other.X;

        public static Vector operator +(Vector a, Vector b) 
            => new(a.X + b.X, a.Y + b.Y);

        public static Vector operator -(Vector a, Vector b)
            => new(a.X - b.X, a.Y - b.Y);
        
        /// <summary>
        /// Dot Product
        /// </summary>
        public static int operator *(Vector a, Vector b) 
            => a.X * b.X + a.Y * b.Y;

        public static Vector operator *(Vector vector, int multiplier)
            => new(vector.X * multiplier, vector.Y * multiplier);
        
        public static Vector operator /(Vector vector, int divisor)
            => new(vector.X / divisor, vector.Y / divisor);
        
        public static Vector operator -(Vector vector)
            => new(-vector.X, -vector.Y);

        public static bool operator ==(Vector a, Vector b)
            => a.Equals(b);

        public static bool operator !=(Vector a, Vector b) 
            => !(a.Equals(b));

        public static implicit operator Vector(Size size)
            => new(size.Width, size.Height);

        public static implicit operator Vector(Point size)
            => new(size.X, size.Y);

        public static explicit operator Vector(Line line)
            => line.Delta;
        
        public static implicit operator Vector(ValueTuple<int, int> tuple) =>
            new(tuple.Item1, tuple.Item2);

        public bool Equals(Vector other) 
            => X == other.X && Y == other.Y;

        public override bool Equals(object obj) 
            => obj is Vector other && Equals(other);

        public override int GetHashCode() 
            => HashCode.Combine(X, Y);
        
        public override string ToString() => $"Vector<{X},{Y}>";
    }
}