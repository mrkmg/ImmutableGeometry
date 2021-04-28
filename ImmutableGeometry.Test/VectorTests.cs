using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace ImmutableGeometry.Test.VectorTests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    [Parallelizable(ParallelScope.All)]
    public class VectorTests
    {
    
    private static readonly Vector Test = new (4, -3);
    public static readonly List<Point> Line = new [] {(0, 0), (1, -1), (2, -1), (3, -2)}.AsPoints().ToList();

    public class Construct
    {
        [Test] public void FromPolarPositive() => Assert.AreEqual(new Vector(2, 1), Vector.FromPolar(Math.PI / 8, 3));
        [Test] public void FromPolarNegative() => Assert.AreEqual(new Vector(-1, -2), Vector.FromPolar(-5 * Math.PI / 8, 3));
    }

    public class Fields {
        [Test] public void X() => Assert.AreEqual(4, Test.X);
        [Test] public void Y() => Assert.AreEqual(-3, Test.Y);
    }
    
    public class Properties {
        [Test] public void UnitX() => Assert.AreEqual(0.80d, Test.UnitX, 0.01d);
        [Test] public void UnitY() => Assert.AreEqual(-0.60d, Test.UnitY, 0.01d);
        [Test] public void Angle() => Assert.AreEqual(-.64d, Test.Angle, 0.01d);
    }

    public class Methods
    {
        [Test] public void RotatePositive() => Assert.AreEqual(new Vector(3, 4), Test.Rotate(Math.PI / 2));
        [Test] public void RotateNegative() => Assert.AreEqual(new Vector(-3, -4), Test.Rotate(-Math.PI / 2));
        [Test] public void Extend() => Assert.AreEqual(new Vector(5, -4), Test.Extend(2));
        [Test] public void Shrink() => Assert.AreEqual(new Vector(1, -1), Test.Shrink(3));
        [Test] public void Cross() => Assert.AreEqual(85, Test.Cross(new Vector(11, 13)));
        [Test] public void Scale() => Assert.AreEqual(new Vector(8, -6), Test.Scale(2));
    }
    
    public class Casts {
        [Test] public void Size() => Assert.AreEqual(new Vector(5, 10), (Vector) new Size(5, 10));
        [Test] public void ToPoint() => Assert.AreEqual(new Vector(5, 10), (Vector) new Point(5, 10));
        [Test] public void ToLine() 
            => Assert.AreEqual(new Vector(5, 10), (Vector) new Line(new Point(3, 7), new Point(8, 17))); 
    }
    
    public class Operators {
        [Test] public void Plus() => Assert.AreEqual(new Vector(31, 27), new Vector(5, 10) + new Vector(26, 17));
        [Test] public void Minus() => Assert.AreEqual(new Vector(-4, 11), new Vector(7, 20) - new Vector(11, 9));
        [Test] public void Multiply() => Assert.AreEqual(23d, new Vector(2, 3) * new Vector(4, 5));
        [Test] public void MultiplyInt() => Assert.AreEqual(new Vector(8, 12), new Vector(2, 3) * 4);
        [Test] public void DivideInt() => Assert.AreEqual(new Vector(4, 6), new Vector(12, 18) / 3);
        [Test] public void Negate() => Assert.AreEqual(new Vector(4, -5), -new Vector(-4, 5));
        // ReSharper disable EqualExpressionComparison
        [Test] public void Equals() => Assert.True(new Vector(5, 10) == new Vector(5, 10));
        [Test] public void NotEquals() => Assert.True(new Vector(5, 10) != new Vector(10, 5));
        // ReSharper enable EqualExpressionComparison
    }

    public class Equatable
    {
        [Test] public void Equals() => Assert.True(Test.Equals(new Vector(4, -3)));
        [Test] public void EqualsObject() => Assert.True(Test.Equals((object) new Vector(4, -3)));
        [Test] public void HashCode() => Assert.AreEqual(new Vector(4, -3).GetHashCode(), Test.GetHashCode());
    }

    [Test] public void String() => Assert.AreEqual("Vector<4,-3>", Test.ToString());
    
    }
}