using System;
using System.Linq;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace ImmutableGeometry.Test.LineTests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    [Parallelizable(ParallelScope.All)]
    public class LineTests
    { 
        
    private static readonly Line TestLine = new (3, 5, 7, 11);
    private static readonly Line TestAlt1 = new (-3, -5, -7, -11);
    private static readonly Line TestAlt2 = new (3, -5, 7, -11);
    private static readonly Line TestAlt3 = new (-3, 5, -7, 11);
    private static readonly Line TestFromVector = new(new Point(3, 5), new Vector(4, 6));
    private static readonly Line TestVertical = new(3, 3, 3, 10);
    private static readonly Line TestHorizontal = new(3, 3, 10, 3);
    private static readonly Line TestNegativeVertical = new(3, 10, 3, 3);
    private static readonly Line TestNegativeHorizontal = new(10, 3, 3, 3);

    private static readonly Line ExpectedTranslated = new (8, 15, 12, 21);
    private static readonly Line ExpectedTransposed = new (5, 3, 11, 7);
    private static readonly Line ExpectedRotated = new (3, 5, -1, -1);
    
    private static readonly Point[] ExpectedLinePoints 
        = TestHelpers.Points((3, 5), (4, 6), (4, 7), (5, 8), (6, 9), (6, 10), (7, 11));
    
    private static readonly Point[] ExpectedAlt1Points 
        = TestHelpers.Points((-3,-5), (-4,-6), (-4,-7), (-5,-8), (-6,-9), (-6,-10), (-7,-11));
    
    private static readonly Point[] ExpectedAlt2Points 
        = TestHelpers.Points((3,-5), (4,-6), (4,-7), (5,-8), (6,-9), (6,-10), (7,-11));
    
    private static readonly Point[] ExpectedAlt3Points 
        = TestHelpers.Points((-3,5), (-4,6), (-4,7), (-5,8), (-6,9), (-6,10), (-7,11));
    
    private static readonly Point[] Expected5Points 
        = TestHelpers.Points((3,5), (4,6), (4,7), (5,8), (6,9), (6,10), (7,11));
    
    private static readonly Point[] ExpectedVerticalPoints 
        = TestHelpers.Points((3,3), (3,4), (3,5), (3,6), (3,7), (3,8), (3,9), (3,10));
    
    private static readonly Point[] ExpectedHorizontalPoints 
        = TestHelpers.Points((3,3), (4,3), (5,3), (6,3), (7,3), (8,3), (9,3), (10,3));
    
    private static readonly Point[] ExpectedNegativeVerticalPoints 
        = TestHelpers.Points((3,10), (3,9), (3,8), (3,7), (3,6), (3,5), (3,4), (3,3));
    
    private static readonly Point[] ExpectedNegativeHorizontalPoints 
        = TestHelpers.Points((10,3), (9,3), (8,3), (7,3), (6,3), (5,3), (4,3), (3,3));


    public class Fields
    {
        [Test] public void Start() 
            => Assert.That(TestLine.Start, Is.EqualTo(new Point(3, 5)));
            
        [Test] public void End() 
            => Assert.That(TestLine.End, Is.EqualTo(new Point(7, 11)));
            
        [Test] public void Delta() 
            => Assert.That(TestLine.Delta, Is.EqualTo(new Vector(4, 6)));
    }

    public class Properties
    {
        [Test]
        public void Points()
            => Assert.Multiple(() =>
            {
                Assert.That(TestLine.Points, Is.EquivalentTo(ExpectedLinePoints));
                Assert.That(TestAlt1.Points, Is.EquivalentTo(ExpectedAlt1Points));
                Assert.That(TestAlt2.Points, Is.EquivalentTo(ExpectedAlt2Points));
                Assert.That(TestAlt3.Points, Is.EquivalentTo(ExpectedAlt3Points));
                Assert.That(TestFromVector.Points, Is.EquivalentTo(Expected5Points));
                Assert.That(TestVertical.Points, Is.EquivalentTo(ExpectedVerticalPoints));
                Assert.That(TestHorizontal.Points, Is.EquivalentTo(ExpectedHorizontalPoints));
                Assert.That(TestNegativeVertical.Points, Is.EquivalentTo(ExpectedNegativeVerticalPoints));
                Assert.That(TestNegativeHorizontal.Points, Is.EquivalentTo(ExpectedNegativeHorizontalPoints));
            });
    }

    public class Methods
    {
        [Test] public void Translate()
            => Assert.That(TestLine.Translate(5, 10), Is.EqualTo(ExpectedTranslated));

        [Test] public void TranslateVector()
            => Assert.That(TestLine.Translate(new Vector(5, 10)), Is.EqualTo(ExpectedTranslated));

        [Test] public void Transpose()
            => Assert.That(TestLine.Transpose(), Is.EqualTo(ExpectedTransposed));

        [Test] public void Rotate()
            => Assert.That(TestLine.Rotate(Math.PI), Is.EqualTo(ExpectedRotated));

        [Test] public void IntersectsWith()
            => Assert.Multiple(() =>
            {
                // Crosses
                Assert.True(TestLine.IntersectsWith(new Line(3, 11, 7, 5)));
                Assert.True(TestLine.IntersectsWith(new Line(7, 5, 3, 11)));
                
                // Cross but not intersects
                Assert.False(TestLine.IntersectsWith(new Line(3, 11, 7, 5).Translate(10, 0)));
                Assert.False(TestLine.IntersectsWith(new Line(7, 5, 3, 11).Translate(0, 10)));
                
                // End points
                Assert.True(TestLine.IntersectsWith(new Line(7, 11, 20, 20)));
                Assert.True(TestLine.IntersectsWith(new Line(0, 0, 3, 5)));
                
                // Near
                Assert.False(TestLine.IntersectsWith(new Line(3, 6, 7, 12)));
                Assert.False(TestLine.IntersectsWith(new Line(4, 5, 8, 11)));
                Assert.False(TestLine.IntersectsWith(new Line(7, 12, 3, 6)));
                Assert.False(TestLine.IntersectsWith(new Line(8, 11, 4, 5)));
            });
    }
    
    public class Casts
    {
        [Test] public void ToVector()
            => Assert.That((Line) new Vector(5, 10), Is.EqualTo(new Line(0, 0, 5, 10)));
    }
    
    public class Operators {
        [Test] public void Equals() => Assert.True(TestLine == new Line(3, 5, 7, 11));
        [Test] public void NotEquals() => Assert.True(TestLine != new Line(-3, -5, -7, -11));
        [Test] public void NotEqualsDifferentStart() => Assert.True(TestLine != new Line(-3, -5, 7, 11));
        [Test] public void NotEqualsDifferentEnd() => Assert.True(TestLine != new Line(3, 5, -7, -11));
    }

    public class Equatable
    {
        [Test] public void Equals() => Assert.True(TestLine.Equals(new Line(3, 5, 7, 11)));
        [Test] public void EqualsObject() => Assert.True(TestLine.Equals((object) new Line(3, 5, 7, 11)));
        [Test] public void HashCode() => Assert.AreEqual(new Line(3, 5, 7, 11).GetHashCode(), TestLine.GetHashCode());
        [Test] public void EqualsObjectRef() => Assert.True(TestLine.Equals((object) TestLine));
        [Test] public void EqualsObjectNull() => Assert.False(TestLine.Equals((object) null));
        // ReSharper disable once SuspiciousTypeConversion.Global
        [Test] public void EqualsObjectDifferentType() => Assert.False(TestLine.Equals(new Point()));
    }

    [Test] public void String() => Assert.AreEqual("Line<(3,5)->(7,11)>", TestLine.ToString());
    
    }
}