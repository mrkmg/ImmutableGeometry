using System;

namespace ImmutableGeometry
{
    /// <summary>
    /// Represents a Width and Height
    /// </summary>
    public readonly struct Size : IComparable<Size>, IEquatable<Size>
    {
        public static Size Zero = default;
        public readonly int Width;
        public readonly int Height;
        public int Area => Width * Height;

        public Size(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width), width, "must be greater than 0");
            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height), height, "must be greater than 0");
            
            Width = width;
            Height = height;
        }

        public Size Scale(int scale) 
            => Scale(scale, scale);
        
        public Size Scale(int x, int y)
            => new(Width * x, Height * y);

        public Size Scale(double scale) 
            => Scale(scale, scale);
        
        public Size Scale(double x, double y)
            => new((int) (Width * x), (int) (Height * y));

        public static Size operator +(Size a, Size b) 
            => new(a.Width + b.Width, a.Height + b.Height);

        public static Size operator -(Size a, Size b)
            => new(a.Width - b.Width, a.Height - b.Height);
        
        public static Size operator *(Size a, Size b) 
            => new(a.Width * b.Width, a.Height * b.Height);

        public static Size operator /(Size a, Size b)
            => new(a.Width / b.Width, a.Height / b.Height);

        public static Size operator *(Size size, int scale)
            => new(size.Width * scale, size.Height * scale);

        public static Size operator *(Size size, double scale)
            => new((int) (size.Width * scale), (int) (size.Height * scale));
        
        public static Size operator /(Size size, int divisor)
            => new(size.Width / divisor, size.Height / divisor);
        
        public static Size operator /(Size size, double divisor)
            => new((int) (size.Width / divisor), (int) (size.Height / divisor));

        public static bool operator ==(Size a, Size b)
            => a.Equals(b);

        public static bool operator !=(Size a, Size b) 
            => !a.Equals(b);

        public static implicit operator Size(Vector vector)
            => new(Math.Abs(vector.X), Math.Abs(vector.Y));
        
        public static implicit operator Size(Point point) 
            => new(Math.Abs(point.X), Math.Abs(point.Y));

        public static explicit operator Size(Rectangle rectangle) 
            => rectangle.Size;
        
        public static implicit operator Size(ValueTuple<int, int> tuple) =>
            new(tuple.Item1, tuple.Item2);

        public bool Equals(Size other) 
            => Width == other.Width && Height == other.Height;

        public override bool Equals(object obj) 
            => obj is Size other && Equals(other);

        public override int GetHashCode() 
            => HashCode.Combine(Width, Height);

        public int CompareTo(Size other) 
            => Area.CompareTo(other.Area);
        
        public override string ToString() => $"Size<{Width},{Height}>";
    }
}