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
        [Test] public void X() 
            => Assert.That(Test.X, Is.EqualTo(4));

        [Test] public void Y() 
            => Assert.That(Test.Y, Is.EqualTo(-3));

    }

    public class Methods
    {
        [Test] public void Translate() 
            => Assert.That(Test.Translate(6, -3), Is.EqualTo(new Point(10, -6)));

        [Test] public void Transpose()
            => Assert.That(Test.Transpose(), Is.EqualTo(new Point(-3, 4)));
    }
    
    public class Casts 
    {
        [Test] public void Size() 
            => Assert.That((Point) new Size(5, 10), Is.EqualTo(new Point(5, 10)));

        [Test] public void Vector() 
            => Assert.That((Point) new Vector(5, 10), Is.EqualTo(new Point(5, 10)));

        [Test] public void Rectangle() 
            => Assert.That((Point) new Rectangle(3, 7, 11, 13), Is.EqualTo(new Point(3, 7)));

        [Test] public void Tuple()
            => Assert.That((Point) (4, 6), Is.EqualTo(new Point(4, 6)));
    }
    
    public class Operators {
        [Test] public void Minus() 
            => Assert.That(new Point(7, 20) - new Point(11, 9), Is.EqualTo(new Vector(-4, 11)));

        [Test] public void BitwiseAndSize() 
            => Assert.That(new Point(5, 10) & new Size(26, 17), Is.EqualTo(new Rectangle(5, 10, 26, 17)));

        [Test] public void AddVector() 
            => Assert.That(new Point(5, 10) + new Vector(7, 8), Is.EqualTo(new Point(12, 18)));

        [Test] public void SubtractVector() 
            => Assert.That(new Point(14, 30) - new Vector(7, 11), Is.EqualTo(new Point(7, 19)));

        [Test] public void Negate() 
            => Assert.That(-new Point(3, -7), Is.EqualTo(new Point(-3, 7)));

        [Test] public void AndVector()
            => Assert.That(new Point(5, 10) & new Vector(3, 7), Is.EqualTo(new Line(5, 10, 8, 17)));

        [Test] public void MultiplyVector()
            => Assert.That(new Point(5, 10) * new Vector(2, 4), Is.EqualTo(new Point(10, 40)));

        // ReSharper disable EqualExpressionComparison
        [Test] public void Equals() 
            => Assert.True(new Point(5, 10) == new Point(5, 10));

        [Test] public void NotEquals() 
            => Assert.True(new Point(5, 10) != new Point(10, 5));

        // ReSharper enable EqualExpressionComparison
    }

    public class Equatable
    {
        [Test] public void Equals() 
            => Assert.True(Test.Equals(new Point(4, -3)));

        [Test] public void EqualsObject() 
            => Assert.True(Test.Equals((object) new Point(4, -3)));

        [Test] public void HashCode() 
            => Assert.That(Test.GetHashCode(), Is.EqualTo(new Point(4, -3).GetHashCode()));
    }

    [Test] public void String() 
        => Assert.That(Test.ToString(), Is.EqualTo("Point<4,-3>"));
    }
}