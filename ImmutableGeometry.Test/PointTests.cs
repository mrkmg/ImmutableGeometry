using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace ImmutableGeometry.Test.PointTests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    [Parallelizable(ParallelScope.All)]
    public class PointTests
    { 
        
    private static readonly Point Test = new (4, -3);
    
    public class Fields {
        [Test] public void X() => Assert.AreEqual(4, Test.X);
        [Test] public void Y() => Assert.AreEqual(-3, Test.Y);
    }

    public class Methods
    {
        [Test] public void Translate() => Assert.AreEqual(new Point(10, -6), Test.Translate(6, -3));
    }
    
    public class Casts {
        [Test] public void Size() => Assert.AreEqual(new Point(5, 10), (Point) new Size(5, 10));
        [Test] public void Vector() => Assert.AreEqual(new Point(5, 10), (Point) new Vector(5, 10));
        [Test] public void Rectangle() => Assert.AreEqual(new Point(3, 7), (Point) new Rectangle(3, 7, 11, 13));
    }
    
    public class Operators {
        [Test] public void Plus() => Assert.AreEqual(new Vector(21, 7), new Point(5, 10) + new Point(26, 17));
        [Test] public void Minus() => Assert.AreEqual(new Vector(-4, 11), new Point(7, 20) - new Point(11, 9));
        [Test] public void BitwiseAndSize() => Assert.AreEqual(new Rectangle(5, 10, 26, 17), new Point(5, 10) & new Size(26, 17));
        [Test] public void AddVector() => Assert.AreEqual(new Point(12, 18), new Point(5, 10) + new Vector(7, 8));
        [Test] public void SubtractVector() => Assert.AreEqual(new Point(7, 19), new Point(14, 30) - new Vector(7, 11));
        [Test] public void Negate() => Assert.AreEqual(new Point(-3, 7), -new Point(3, -7));
        // ReSharper disable EqualExpressionComparison
        [Test] public void Equals() => Assert.True(new Point(5, 10) == new Point(5, 10));
        [Test] public void NotEquals() => Assert.True(new Point(5, 10) != new Point(10, 5));
        // ReSharper enable EqualExpressionComparison
    }

    public class Equatable
    {
        [Test] public void Equals() => Assert.True(Test.Equals(new Point(4, -3)));
        [Test] public void EqualsObject() => Assert.True(Test.Equals((object) new Point(4, -3)));
        [Test] public void HashCode() => Assert.AreEqual(new Point(4, -3).GetHashCode(), Test.GetHashCode());
    }

    [Test] public void String() => Assert.AreEqual("Point<4,-3>", Test.ToString());

    }
}