using System;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace ImmutableGeometry.Test.SizeTests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    [Parallelizable(ParallelScope.All)]
    public class SizeTests
    {

    private static readonly Size Test = new (3, 7);

    public class InvalidConstructors
    {
        [Test]
        public void ZeroSize() => Assert.Throws<ArgumentOutOfRangeException>(() => new Size(0, 0));

        [Test]
        public void Width() => Assert.Throws<ArgumentOutOfRangeException>(() => new Size(-1, 4));

        [Test]
        public void Height() => Assert.Throws<ArgumentOutOfRangeException>(() => new Size(4, -1));
    }

    public class Fields
    {
        [Test] public void X() => Assert.AreEqual(3, Test.Width);
        [Test] public void Y() => Assert.AreEqual(7, Test.Height);
    }

    public class Properties
    {
        [Test] public void Area() => Assert.AreEqual(21, Test.Area);
    }

    public class Methods
    {
        [Test] public void ScaleInt() => Assert.AreEqual(new Size(12, 28), Test.Scale(4));
        [Test] public void ScaleIntInt() => Assert.AreEqual(new Size(12, 35), Test.Scale(4, 5));
        [Test] public void ScaleDouble() => Assert.AreEqual(new Size(7, 17), Test.Scale(2.5d));
        [Test] public void ScaleDoubleDouble() => Assert.AreEqual(new Size(7, 24), Test.Scale(2.5d, 3.5d));
    }

    public class Casts
    {
        [Test] public void Vector() => Assert.AreEqual(new Size(5, 10), (Size) new Vector(5, 10));
        [Test] public void Point() => Assert.AreEqual(new Size(5, 10), (Size) new Point(5, 10));
        [Test] public void Rectangle() => Assert.AreEqual(new Size(11, 13), (Size) new Rectangle(3, 7, 11, 13));
    }

    public class Operators
    {
        [Test] public void Plus() => Assert.AreEqual(new Size(31, 27), new Size(5, 10) + new Size(26, 17));
        [Test] public void Minus() => Assert.AreEqual(new Size(4, 11), new Size(11, 20) - new Size(7, 9));
        [Test] public void Multiply() => Assert.AreEqual(new Size(8, 15), new Size(2, 3) * new Size(4, 5));
        [Test] public void Divide() => Assert.AreEqual(new Size(4, 9), new Size(12, 18) / new Size(3, 2));
        [Test] public void MultiplyInt() => Assert.AreEqual(new Size(8, 12), new Size(2, 3) * 4);
        [Test] public void DivideInt() => Assert.AreEqual(new Size(4, 6), new Size(12, 18) / 3);
        [Test] public void MultiplyDouble() => Assert.AreEqual(new Size(11, 16), new Size(2, 3) * 5.5d);
        [Test] public void DivideDouble() => Assert.AreEqual(new Size(2, 3), new Size(12, 18) / 5.5d);
        // ReSharper disable EqualExpressionComparison
        [Test] public void Equals() => Assert.True(new Size(5, 10) == new Size(5, 10));
        [Test] public void NotEquals() => Assert.True(new Size(5, 10) != new Size(10, 5));
        // ReSharper enable EqualExpressionComparison
    }

    public class Equatable
    {
        [Test] public void Equals() => Assert.True(Test.Equals(new Size(3, 7)));
        [Test] public void EqualsObject() => Assert.True(Test.Equals((object) new Size(3, 7)));
        [Test] public void HashCode() => Assert.AreEqual(new Size(3, 7).GetHashCode(), Test.GetHashCode());
    }

    public class Comparable
    {
        [Test] public void Less() => Assert.AreEqual(-1, Test.CompareTo(new Size(4, 8)));
        [Test] public void Equal() => Assert.AreEqual(0, Test.CompareTo(new Size(3, 7)));
        [Test] public void More() => Assert.AreEqual(1, Test.CompareTo(new Size(2, 6)));
    }

    [Test] public void String() => Assert.AreEqual("Size<3,7>", Test.ToString());
    
    }
}