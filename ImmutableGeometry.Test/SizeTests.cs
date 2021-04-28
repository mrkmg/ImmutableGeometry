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
        [Test] public void X() 
            => Assert.That(Test.Width, Is.EqualTo(3));
        
        [Test] public void Y() 
            => Assert.That(Test.Height, Is.EqualTo(7));
    }

    public class Properties
    {
        [Test] public void Area() 
            => Assert.That(Test.Area, Is.EqualTo(21));
    }

    public class Methods
    {
        [Test] public void ScaleInt() 
            => Assert.That(Test.Scale(4), Is.EqualTo(new Size(12, 28)));
            
        [Test] public void ScaleIntInt() 
            => Assert.That(Test.Scale(4, 5), Is.EqualTo(new Size(12, 35)));
            
        [Test] public void ScaleDouble() 
            => Assert.That(Test.Scale(2.5d), Is.EqualTo(new Size(7, 17)));
            
        [Test] public void ScaleDoubleDouble() 
            => Assert.That(Test.Scale(2.5d, 3.5d), Is.EqualTo(new Size(7, 24)));
    }

    public class Casts
    {
        [Test] public void Vector() 
            => Assert.That((Size) new Vector(5, 10), Is.EqualTo(new Size(5, 10)));
            
        [Test] public void Point() 
            => Assert.That((Size) new Point(5, 10), Is.EqualTo(new Size(5, 10)));
            
        [Test] public void Rectangle() 
            => Assert.That((Size) new Rectangle(3, 7, 11, 13), Is.EqualTo(new Size(11, 13)));
        
        [Test] public void Tuple()
            => Assert.That((Size) (5, 10), Is.EqualTo(new Size(5, 10)));
    }

    public class Operators
    {
        [Test] public void Plus() 
            => Assert.That(new Size(5, 10) + new Size(26, 17), Is.EqualTo(new Size(31, 27)));
            
        [Test] public void Minus() 
            => Assert.That(new Size(11, 20) - new Size(7, 9), Is.EqualTo(new Size(4, 11)));
            
        [Test] public void Multiply() 
            => Assert.That(new Size(2, 3) * new Size(4, 5), Is.EqualTo(new Size(8, 15)));
            
        [Test] public void MultiplyInt() 
            => Assert.That(new Size(2, 3) * 4, Is.EqualTo(new Size(8, 12)));
            
        [Test] public void Divide() 
            => Assert.That(new Size(12, 18) / new Size(3, 2), Is.EqualTo(new Size(4, 9)));
            
        [Test] public void DivideInt() 
            => Assert.That(new Size(12, 18) / 3, Is.EqualTo(new Size(4, 6)));
            
        [Test] public void MultiplyDouble() 
            => Assert.That(new Size(2, 3) * 5.5d, Is.EqualTo(new Size(11, 16)));
            
        [Test] public void DivideDouble() 
            => Assert.That(new Size(12, 18) / 5.5d, Is.EqualTo(new Size(2, 3)));
            
        // ReSharper disable EqualExpressionComparison
        [Test] public void Equals() 
            => Assert.True(new Size(5, 10) == new Size(5, 10));
        
        [Test] public void NotEquals() 
            => Assert.True(new Size(5, 10) != new Size(10, 5));
        // ReSharper enable EqualExpressionComparison
    }

    public class Equatable
    {
        [Test] public void Equals() 
            => Assert.True(Test.Equals(new Size(3, 7)));
        
        [Test] public void EqualsObject() 
            => Assert.True(Test.Equals((object) new Size(3, 7)));
        
        [Test] public void HashCode() 
            => Assert.That(Test.GetHashCode(), Is.EqualTo(new Size(3, 7).GetHashCode()));
            
    }

    public class Comparable
    {
        [Test] public void Less() 
            => Assert.That(Test.CompareTo(new Size(4, 8)), Is.EqualTo(-1));
            
        [Test] public void Equal() 
            => Assert.That(Test.CompareTo(new Size(3, 7)), Is.EqualTo(0));
            
        [Test] public void More() 
            => Assert.That(Test.CompareTo(new Size(2, 6)), Is.EqualTo(1));
            
    }

    [Test] public void String() 
        => Assert.That(Test.ToString(), Is.EqualTo("Size<3,7>"));
        
    
    }
}