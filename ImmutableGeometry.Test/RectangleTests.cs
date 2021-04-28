using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace ImmutableGeometry.Test.RectangleTests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    [Parallelizable(ParallelScope.All)]
    public class RectangleTests
    {
        
    private static readonly Rectangle Test = new (new Point(3, 7), new Size(11, 13));

    private static readonly Point[] ExpectedPoints = TestHelpers.Points((3, 7), (3, 8), (3, 9), (3, 10), (3, 11),
        (3, 12), (3, 13), (3, 14), (3, 15), (3, 16), (3, 17), (3, 18), (3, 19), (4, 7), (4, 8), (4, 9), (4, 10),
        (4, 11), (4, 12), (4, 13), (4, 14), (4, 15), (4, 16), (4, 17), (4, 18), (4, 19), (5, 7), (5, 8), (5, 9),
        (5, 10), (5, 11), (5, 12), (5, 13), (5, 14), (5, 15), (5, 16), (5, 17), (5, 18), (5, 19), (6, 7), (6, 8),
        (6, 9), (6, 10), (6, 11), (6, 12), (6, 13), (6, 14), (6, 15), (6, 16), (6, 17), (6, 18), (6, 19), (7, 7),
        (7, 8), (7, 9), (7, 10), (7, 11), (7, 12), (7, 13), (7, 14), (7, 15), (7, 16), (7, 17), (7, 18), (7, 19),
        (8, 7), (8, 8), (8, 9), (8, 10), (8, 11), (8, 12), (8, 13), (8, 14), (8, 15), (8, 16), (8, 17), (8, 18),
        (8, 19), (9, 7), (9, 8), (9, 9), (9, 10), (9, 11), (9, 12), (9, 13), (9, 14), (9, 15), (9, 16), (9, 17),
        (9, 18), (9, 19), (10, 7), (10, 8), (10, 9), (10, 10), (10, 11), (10, 12), (10, 13), (10, 14), (10, 15),
        (10, 16), (10, 17), (10, 18), (10, 19), (11, 7), (11, 8), (11, 9), (11, 10), (11, 11), (11, 12), (11, 13),
        (11, 14), (11, 15), (11, 16), (11, 17), (11, 18), (11, 19), (12, 7), (12, 8), (12, 9), (12, 10), (12, 11),
        (12, 12), (12, 13), (12, 14), (12, 15), (12, 16), (12, 17), (12, 18), (12, 19), (13, 7), (13, 8), (13, 9),
        (13, 10), (13, 11), (13, 12), (13, 13), (13, 14), (13, 15), (13, 16), (13, 17), (13, 18), (13, 19));

    public class Constructs
    {
        [Test] public void XYWH() 
            => Assert.That(new Rectangle(3, 7, 11, 13), Is.EqualTo(Test));
            
        [Test] public void Size() 
            => Assert.That(new Rectangle(new Size(11, 13)), Is.EqualTo(new Rectangle(0, 0, 11, 13)));
            
        [Test] public void WH() 
            => Assert.That(new Rectangle(11, 13), Is.EqualTo(new Rectangle(0, 0, 11, 13)));
            
        [Test] public void PointPoint() 
            => Assert.That(new Rectangle(new Point(3, 7), new Point(14, 20)), Is.EqualTo(Test));
            
        [Test] public void FromLTRB() 
            => Assert.That(Rectangle.FromLTRB(3, 7, 13, 19), Is.EqualTo(Test));
    }

    public class Fields
    {
        [Test] public void FieldPosition() 
            => Assert.That(Test.Position, Is.EqualTo(new Point(3, 7)));
             
        [Test] public void FieldSize() 
            => Assert.That(Test.Size, Is.EqualTo(new Size(11, 13)));
    }

    public class Properties
    {
        [Test] public void PropertyArea() 
            => Assert.That(Test.Area, Is.EqualTo(143));
            
        [Test] public void Bottom() 
            => Assert.That(Test.Bottom, Is.EqualTo(19));
            
        [Test] public void Height() 
            => Assert.That(Test.Height, Is.EqualTo(13));
            
        [Test] public void Left() 
            => Assert.That(Test.Left, Is.EqualTo(3));
            
        [Test] public void Right() 
            => Assert.That(Test.Right, Is.EqualTo(13));
            
        [Test] public void Top() 
            => Assert.That(Test.Top, Is.EqualTo(7));
            
        [Test] public void Width() 
            => Assert.That(Test.Width, Is.EqualTo(11));
            
        [Test] public void TopLeft() 
            => Assert.That(Test.TopLeft, Is.EqualTo(new Point(3, 7)));
            
        [Test] public void TopRight() 
            => Assert.That(Test.TopRight, Is.EqualTo(new Point(13, 7)));
            
        [Test] public void BottomLeft() 
            => Assert.That(Test.BottomLeft, Is.EqualTo(new Point(3, 19)));
            
        [Test] public void BottomRight() 
            => Assert.That(Test.BottomRight, Is.EqualTo(new Point(13, 19)));
            
        [Test] public void X() 
            => Assert.That(Test.X, Is.EqualTo(3));
            
        [Test] public void Y() 
            => Assert.That(Test.Y, Is.EqualTo(7));

        [Test] public void Center()
            => Assert.That(Test.Center, Is.EqualTo(new Point(8, 13)));
    }

    public class Methods
    {
        [Test] public void ContainsPoint() 
            => Assert.Multiple(() =>
            
        {
            Assert.True(Test.Contains(new Point(5, 10)));
            Assert.False(Test.Contains(new Point(1, 10)));
        });
    
        [Test] public void ContainsRectangle() 
            => Assert.Multiple(() => 
                {
                    Assert.True(Test.Contains(new Rectangle(5, 10, 2, 3)));
                    Assert.False(Test.Contains(new Rectangle(0, 0, 5, 10)));
                    Assert.False(Test.Contains(new Rectangle(5, 10, 20, 20)));
                });

        [Test] public void Intersects() 
            => Assert.Multiple(() => 
                {
                    Assert.True(Test.IntersectsWith(new Rectangle(1, 9, 13, 11)));
                    Assert.False(Test.IntersectsWith(new Rectangle(20, 5, 10, 10)));
                });

        [Test] public void Inflate() 
            => Assert.That(Test.Inflate(new Size(2, 4)), Is.EqualTo(new Rectangle(2, 5, 13, 17)));
            
        [Test] public void Deflate() 
            => Assert.That(Test.Deflate(new Size(2, 4)), Is.EqualTo(new Rectangle(4, 9, 9, 9)));
        
        [Test] public void Points()
            => Assert.That(Test.Points(), Is.EquivalentTo(ExpectedPoints));
    }

    public class Operators
    {
        [Test] public void AddVector() 
            => Assert.That(Test + new Vector(17, 23), Is.EqualTo(new Rectangle(20, 30, 11, 13)));
            
        [Test] public void SubtractVector() 
            => Assert.That(Test - new Vector(17, 23), Is.EqualTo(new Rectangle(-14, -16, 11, 13)));
            
        [Test] public void AddSize() 
            => Assert.That(Test + new Size(17, 23), Is.EqualTo(new Rectangle(3, 7, 28, 36)));
            
        [Test] public void SubtractSize() 
            => Assert.That(Test - new Size(7, 3), Is.EqualTo(new Rectangle(3, 7, 4, 10)));
            
        [Test] public void MultiplySize() 
            => Assert.That(Test * new Size(4, 5), Is.EqualTo(new Rectangle(3, 7, 44, 65)));
            
        [Test] public void DivideSize() 
            => Assert.That(Test / new Size(2, 4), Is.EqualTo(new Rectangle(3, 7, 5, 3)));
            
        // ReSharper disable EqualExpressionComparison
        [Test] public void Equals() 
            => Assert.True(new Size(5, 10) == new Size(5, 10));
            
        [Test] public void NotEquals() 
            => Assert.True(new Size(5, 10) != new Size(10, 5));
            
        // ReSharper enable EqualExpressionComparison
    }

    public class Casts
    {
        [Test] public void Size() 
            => Assert.That((Rectangle) new Size(3, 7), Is.EqualTo(new Rectangle(0, 0, 3, 7)));
            
        [Test] public void Tuple() 
            => Assert.That((Rectangle) (0, 5, 10, 15), Is.EqualTo(new Rectangle(0, 5, 10, 15)));
    }

    public class Equatable
    {
        [Test] public void Equals() 
            => Assert.True(Test.Equals(new Rectangle(3, 7, 11, 13)));
            
        [Test] public void EqualsObject() 
            => Assert.True(Test.Equals((object) new Rectangle(3, 7, 11, 13)));
            
        [Test] public void HashCode() 
            => Assert.That(Test.GetHashCode(), Is.EqualTo(new Rectangle(3, 7, 11, 13).GetHashCode()));
    }

    [Test] public void String() 
        => Assert.That(Test.ToString(), Is.EqualTo("Rectangle<(3,7)(11x13)>"));
    }
}