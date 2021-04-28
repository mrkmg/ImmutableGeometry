using System;
using System.Collections.Generic;

namespace ImmutableGeometry
{
    /// <summary>
    /// Represents a Position and Size
    /// </summary>
    public readonly struct Rectangle : IEquatable<Rectangle>
    {
        public readonly Point Position;
        public readonly Size Size;
        
        public int Left => Position.X;
        public int Top => Position.Y;
        public int Right => Position.X + Size.Width - 1;
        public int Bottom => Position.Y + Size.Height - 1;
        public Point TopLeft => new (Left, Top);
        public Point TopRight => new (Right, Top);
        public Point BottomLeft => new (Left, Bottom);
        public Point BottomRight => new (Right, Bottom);
        public int X => Position.X;
        public int Y => Position.Y;
        public int Width => Size.Width;
        public int Height => Size.Height;
        public int Area => Size.Area;
        public Point Center => new(Left + Width / 2, Top + Height / 2);
        
        public Rectangle(Point position, Size size)
        {
            Position = position;
            Size = size;
        }

        public Rectangle(Size size) 
            : this(new Point(0, 0), size) {}

        public Rectangle(Point topLeft, Point bottomRight)
            : this(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y) {}

        public Rectangle(int width, int height)
            : this(new Point(0, 0), new Size(width, height)) {}
        
        public Rectangle(int x, int y, int width, int height) 
            : this(new Point(x, y), new Size(width, height)) {}

        public static Rectangle FromLTRB(int left, int top, int right, int bottom)
            => new (left, top, right - left + 1, bottom - top + 1);

        public bool Contains(Point point) =>
            point.X >= Left && point.X <= Right && point.Y <= Bottom && point.Y >= Top;

        public bool Contains(Rectangle rectangle) =>
            rectangle.Left >= Left && rectangle.Right <= Right &&
            rectangle.Top >= Top && rectangle.Bottom <= Bottom;
        
        public bool IntersectsWith(Rectangle rectangle) =>
            (rectangle.X < X + Width) && (X < rectangle.X + rectangle.Width) &&
            (rectangle.Y < Y + Height) && (Y < rectangle.Y + rectangle.Height);

        public Rectangle Inflate(Size size) 
            => new(Position - (Vector) (size / 2), Size + size);

        public Rectangle Deflate(Size size) 
            => new(Position + (Vector) (size / 2), Size - size);
        
        public IEnumerable<Point> Points()
        {
            for (var x = Left; x <= Right; x++)
            for (var y = Top; y <= Bottom; y++)
                yield return new Point(x, y);
        }

        public static Rectangle operator +(Rectangle rectangle, Size size)
            => new(rectangle.Position, rectangle.Size + size);

        public static Rectangle operator -(Rectangle rectangle, Size size)
            => new(rectangle.Position, rectangle.Size - size);

        public static Rectangle operator *(Rectangle rectangle, Size size)
            => new(rectangle.Position, rectangle.Size * size);

        public static Rectangle operator /(Rectangle rectangle, Size size)
            => new(rectangle.Position, rectangle.Size / size);

        public static Rectangle operator +(Rectangle rectangle, Vector vector)
            => new(rectangle.Position + vector, rectangle.Size);

        public static Rectangle operator -(Rectangle rectangle, Vector vector)
            => new(rectangle.Position - vector, rectangle.Size);
        
        public static explicit operator Rectangle(Size size) 
            => new(size);
        
        public static explicit operator Rectangle(ValueTuple<int, int, int, int> tuple) =>
            new(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        
        public bool Equals(Rectangle other) 
            => Position.Equals(other.Position) && Size.Equals(other.Size);

        public override bool Equals(object obj) 
            => obj is Rectangle other && Equals(other);

        public override int GetHashCode() 
            => HashCode.Combine(Position, Size);
        
        public override string ToString() => $"Rectangle<({X},{Y})({Width}x{Height})>";
    } 
}