using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace ImmutableGeometry.Test.RectangleTests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    [Parallelizable(ParallelScope.All)]
    public class RectangleTests
    {
        
    private static readonly Rectangle Test = new (new Point(3, 7), new Size(11, 13));

    public class Constructs
    {
        [Test] public void XYWH() => Assert.AreEqual(Test, new Rectangle(3, 7, 11, 13));
        [Test] public void Size() => Assert.AreEqual(new Rectangle(0, 0, 11, 13), new Rectangle(new Size(11, 13)));
        [Test] public void WH() => Assert.AreEqual(new Rectangle(0, 0, 11, 13), new Rectangle(11, 13));
        [Test] public void PointPoint() => Assert.AreEqual(Test, new Rectangle(new Point(3, 7), new Point(14, 20)));
        [Test] public void FromLTRB() => Assert.AreEqual(Test, Rectangle.FromLTRB(3, 7, 13, 19));
    }

    public class Fields
    {
        [Test] public void FieldPosition() => Assert.AreEqual(new Point(3, 7), Test.Position); 
        [Test] public void FieldSize() => Assert.AreEqual(new Size(11, 13), Test.Size);
    }

    public class Properties
    {
        [Test] public void PropertyArea() => Assert.AreEqual(143, Test.Area);
        [Test] public void Bottom() => Assert.AreEqual(19, Test.Bottom);
        [Test] public void Height() => Assert.AreEqual(13, Test.Height);
        [Test] public void Left() => Assert.AreEqual(3, Test.Left);
        [Test] public void Right() => Assert.AreEqual(13, Test.Right);
        [Test] public void Top() => Assert.AreEqual(7, Test.Top);
        [Test] public void Width() => Assert.AreEqual(11, Test.Width);
        [Test] public void TopLeft() => Assert.AreEqual(new Point(3, 7), Test.TopLeft);
        [Test] public void TopRight() => Assert.AreEqual(new Point(13, 7), Test.TopRight);
        [Test] public void BottomLeft() => Assert.AreEqual(new Point(3, 19), Test.BottomLeft);
        [Test] public void BottomRight() => Assert.AreEqual(new Point(13, 19), Test.BottomRight);
        [Test] public void X() => Assert.AreEqual(3, Test.X);
        [Test] public void Y() => Assert.AreEqual(7, Test.Y);
    }

    public class Methods
    {
        [Test] public void ContainsPoint() => Assert.Multiple(() =>
        {
            Assert.True(Test.Contains(new Point(5, 10)));
            Assert.False(Test.Contains(new Point(1, 10)));
        });
    
        [Test] public void ContainsRectangle() => Assert.Multiple(() =>
        {
            Assert.True(Test.Contains(new Rectangle(5, 10, 2, 3)));
            Assert.False(Test.Contains(new Rectangle(0, 0, 5, 10)));
            Assert.False(Test.Contains(new Rectangle(5, 10, 20, 20)));
        });

        [Test] public void Intersects() => Assert.Multiple(() =>
        {
            Assert.True(Test.IntersectsWith(new Rectangle(1, 9, 13, 11)));
            Assert.False(Test.IntersectsWith(new Rectangle(20, 5, 10, 10)));
        });

        [Test] public void Inflate() => Assert.AreEqual(new Rectangle(2, 5, 13, 17), Test.Inflate(new Size(2, 4)));
        [Test] public void Deflate() => Assert.AreEqual(new Rectangle(4, 9, 9, 9), Test.Deflate(new Size(2, 4)));
    }

    public class Operators
    {
        [Test] public void AddVector() => Assert.AreEqual(new Rectangle(20, 30, 11, 13), Test + new Vector(17, 23));
        [Test] public void SubtractVector() => Assert.AreEqual(new Rectangle(-14, -16, 11, 13), Test - new Vector(17, 23));
        [Test] public void AddSize() => Assert.AreEqual(new Rectangle(3, 7, 28, 36), Test + new Size(17, 23));
        [Test] public void SubtractSize() => Assert.AreEqual(new Rectangle(3, 7, 4, 10), Test - new Size(7, 3));
        [Test] public void MultiplySize() => Assert.AreEqual(new Rectangle(3, 7, 44, 65), Test * new Size(4, 5));
        [Test] public void DivideSize() => Assert.AreEqual(new Rectangle(3, 7, 5, 3), Test / new Size(2, 4));
        // ReSharper disable EqualExpressionComparison
        [Test] public void Equals() => Assert.True(new Size(5, 10) == new Size(5, 10));
        [Test] public void NotEquals() => Assert.True(new Size(5, 10) != new Size(10, 5));
        // ReSharper enable EqualExpressionComparison
    }

    public class Casts
    {
        [Test] public void Size() => Assert.AreEqual(new Rectangle(0, 0, 3, 7), (Rectangle) new Size(3, 7));
    }

    public class Equatable
    {
        [Test] public void Equals() => Assert.True(Test.Equals(new Rectangle(3, 7, 11, 13)));
        [Test] public void EqualsObject() => Assert.True(Test.Equals((object) new Rectangle(3, 7, 11, 13)));
        [Test] public void HashCode() => Assert.AreEqual(new Rectangle(3, 7, 11, 13).GetHashCode(), Test.GetHashCode());
    }

    [Test] public void String() => Assert.AreEqual("Rectangle<(3,7)(11x13)>", Test.ToString());
        
    }
}