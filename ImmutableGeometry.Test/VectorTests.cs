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

    public class Construct
    {
        [Test] public void FromPolarPositive() 
            => Assert.That(Vector.FromPolar(Math.PI / 8, 3), Is.EqualTo(new Vector(2, 1)));
            
        [Test] public void FromPolarNegative() 
            => Assert.That(Vector.FromPolar(-5 * Math.PI / 8, 3), Is.EqualTo(new Vector(-1, -2)));
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
        [Test] public void RotatePositive() 
            => Assert.That(Test.Rotate(Math.PI / 2), Is.EqualTo(new Vector(3, 4)));
            
        [Test] public void RotateNegative() 
            => Assert.That(Test.Rotate(-Math.PI / 2), Is.EqualTo(new Vector(-3, -4)));
            
        [Test] public void Extend() 
            => Assert.That(Test.Extend(2), Is.EqualTo(new Vector(5, -4)));
            
        [Test] public void Shrink() 
            => Assert.That(Test.Shrink(3), Is.EqualTo(new Vector(1, -1)));
            
        [Test] public void Cross() 
            => Assert.That(Test.Cross(new Vector(11, 13)), Is.EqualTo(85));
        
        [Test] public void Scale() 
            => Assert.That(Test.Scale(2), Is.EqualTo(new Vector(8, -6)));
            
    }
    
    public class Casts {
        [Test] public void Size() 
            => Assert.That((Vector) new Size(5, 10), Is.EqualTo(new Vector(5, 10)));
            
        [Test] public void Point() 
            => Assert.That((Vector) new Point(5, 10), Is.EqualTo(new Vector(5, 10)));
            
        [Test] public void Line() 
            => Assert.That((Vector) new Line(new Point(3, 7), new Point(8, 17)), Is.EqualTo(new Vector(5, 10)));

        [Test] public void Tuple()
            => Assert.That((Vector) (5, 10), Is.EqualTo(new Vector(5, 10)));
    }
    
    public class Operators {
        [Test] public void Plus() 
            => Assert.That(new Vector(5, 10) + new Vector(26, 17), Is.EqualTo(new Vector(31, 27)));
            
        [Test] public void Minus() 
            => Assert.That(new Vector(7, 20) - new Vector(11, 9), Is.EqualTo(new Vector(-4, 11)));
            
        [Test] public void Multiply() 
            => Assert.That(new Vector(2, 3) * new Vector(4, 5), Is.EqualTo(23));
        
        [Test] public void MultiplyInt() 
            => Assert.That(new Vector(2, 3) * 4, Is.EqualTo(new Vector(8, 12)));
            
        [Test] public void DivideInt() 
            => Assert.That(new Vector(12, 18) / 3, Is.EqualTo(new Vector(4, 6)));
            
        [Test] public void Negate() 
            => Assert.That(-new Vector(-4, 5), Is.EqualTo(new Vector(4, -5)));
            
        // ReSharper disable EqualExpressionComparison
        [Test] public void Equals() 
            => Assert.True(new Vector(5, 10) == new Vector(5, 10));

        [Test] public void NotEquals() 
            => Assert.True(new Vector(5, 10) != new Vector(10, 5));

        // ReSharper enable EqualExpressionComparison
    }

    public class Equatable
    {
        [Test] public void Equals() 
            => Assert.True(Test.Equals(new Vector(4, -3)));

        [Test] public void EqualsObject() 
            => Assert.True(Test.Equals((object) new Vector(4, -3)));

        [Test] public void HashCode() 
            => Assert.That(Test.GetHashCode(), Is.EqualTo(new Vector(4, -3).GetHashCode()));
    }

    [Test] public void String() 
        => Assert.That(Test.ToString(), Is.EqualTo("Vector<4,-3>"));
    
    }
}