using System.Linq;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace ImmutableGeometry.Test.LineTests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    [Parallelizable(ParallelScope.All)]
    public class LineTests
    { 
        
    private static readonly Line Test1 = new (new Point(3, 5), new Point(7, 11));
    private static readonly Line Test2 = new (new Point(-3, -5), new Point(-7, -11));
    private static readonly Line Test3 = new (new Point(3, -5), new Point(7, -11));
    private static readonly Line Test4 = new (new Point(-3, 5), new Point(-7, 11));
    private static readonly Line Test5 = new(new Point(3, 5), new Vector(4, 6));
    private static readonly Line TestVertical = new(new Point(3, 3), new Point(3, 10));
    private static readonly Line TestHorizontal = new(new Point(3, 3), new Point(10, 3));
    private static readonly Line TestNegativeVertical = new(new Point(3, 3), new Point(3, 10));
    private static readonly Line TestNegativeHorizontal = new(new Point(3, 3), new Point(10, 3));
    
    private static readonly Point[] Test1Points = new[]
    {
        (3, 5), (4, 6), (4, 7), (5, 8), (6, 9), (6, 10), (7, 11)
    }.AsPoints().ToArray();
    private static readonly Point[] Test2Points = new[]
    {
        (-3,-5), (-4,-6), (-4,-7), (-5,-8), (-6,-9), (-6,-10), (-7,-11)
    }.AsPoints().ToArray();
    private static readonly Point[] Test3Points = new[]
    {
        (3,-5), (4,-6), (4,-7), (5,-8), (6,-9), (6,-10), (7,-11)
    }.AsPoints().ToArray();
    private static readonly Point[] Test4Points = new[]
    {
        (-3,5), (-4,6), (-4,7), (-5,8), (-6,9), (-6,10), (-7,11)
    }.AsPoints().ToArray();

    public class Fields
    {
        [Test] public void Start() => Assert.That(Test1.Start, Is.EqualTo(new Point(3, 5)));
        [Test] public void End() => Assert.That(Test1.End, Is.EqualTo(new Point(7, 11)));
        [Test] public void Delta() => Assert.That(Test1.Delta, Is.EqualTo(new Vector(4, 6)));
    }

    public class Properties
    {
        [Test] public void Points1() => Assert.That(Test1.Points, Is.EquivalentTo(Test1Points));
        [Test] public void Points2() => Assert.That(Test2.Points, Is.EquivalentTo(Test2Points));
        [Test] public void Points3() => Assert.That(Test3.Points, Is.EquivalentTo(Test3Points));
        [Test] public void Points4() => Assert.That(Test4.Points, Is.EquivalentTo(Test4Points));
    }

    public class Methods
    {
    }
    
    public class Casts 
    {
    }
    
    public class Operators {
    }

    public class Equatable
    {
    }
    }
}