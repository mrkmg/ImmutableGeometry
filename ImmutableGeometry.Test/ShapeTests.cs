using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace ImmutableGeometry.Test.ShapeTests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    [Parallelizable(ParallelScope.All)]
    public class ShapeTests
    { 
        
    private static readonly Shape TestShape = new (
        (0,0), (0, 5), (5, 5), (5, 10), (0, 10), (0, 15), (10, 10), (10, 3));
    
    private static readonly Shape TestWithNodesOutOfOrder = new (
        (0, 5), (5, 5), (5, 10), (0, 10), (0, 15), (10, 10), (10, 3), (0,0));

    private static readonly Shape TestL = new(new Point[]
        {new(-2, -4), new(-2, 4), new(2, 4), new(2, 2), new(0, 2), new(0, -4)});
    
    private static readonly Shape TestSquare = new (
        (0, 0), (0, 5), (5, 5), (5, 0));
    
    private static readonly Shape TestDiamond = new (
        (0, -5), (-5, 0), (5, 5), (5, 0));
    
    private static readonly Shape TestTranslated = new (
        (25,50), (25,55), (30,55), (30,60), (25,60), (25,65), (35,60), (35,53));
    
    private static readonly Shape TestRotated = new (
        (4,-1), (2,4), (6,6), (11,6), (0,8), (5,9), (8,11), (-2,12));
    
    public static readonly Shape TestScaled = new (
        (-5,-16), (15,-7), (-5,-1), (5,-1), (-5,14), (5,14), (15,14), (-5,29));
    
    public static readonly Shape TestScaledDoubled = new(
        (-5,-8), (15,-2), (-5,2), (5,2), (-5,12), (5,12), (15,12), (-5,22));

    public static readonly Shape TestMirroredX = new(
        (0,0), (-10,3), (-5,5), (0,5), (-10,10), (-5,10), (0,10), (0,15));
    
    public static readonly Shape TestMirroredY = new(
        (0,-15), (0,-10), (5,-10), (10,-10), (0,-5), (5,-5), (10,-3), (0,0));
    
    private static readonly Point[] TestEdgePoints = new []
    {
        (0,0), (0,1), (0,2), (0,3), (0,4), (0,5), (1,5), (2,5), (3,5), (4,5), (5,5), (5,6), (5,7), (5,8), (5,9), (5,10),
        (4,10), (3,10), (2,10), (1,10), (0,10), (0,11), (0,12), (0,13), (0,14), (0,15), (1,15), (2,14), (3,14), (4,13),
        (5,13), (6,12), (7,12), (8,11), (9,11), (10,10), (10,9), (10,8), (10,7), (10,6), (10,5), (10,4), (10,3), (9,3),
        (8,2), (7,2), (6,2), (5,2), (4,1), (3,1), (2,1), (1,0)
    }.AsPoints().ToArray();

    private static readonly Point[] TestAllPoints = new []
    {
        (0,0), (1,0), (0,1), (1,1), (2,1), (3,1), (4,1), (0,2), (1,2), (2,2), (3,2), (4,2), (5,2), (6,2), (7,2), (8,2),
        (0,3), (1,3), (2,3), (3,3), (4,3), (5,3), (6,3), (7,3), (8,3), (9,3), (10,3), (0,4), (1,4), (2,4), (3,4), (4,4),
        (5,4), (6,4), (7,4), (8,4), (9,4), (10,4), (0,5), (1,5), (2,5), (3,5), (4,5), (5,5), (6,5), (7,5), (8,5), (9,5),
        (10,5), (5,6), (6,6), (7,6), (8,6), (9,6), (10,6), (5,7), (6,7), (7,7), (8,7), (9,7), (10,7), (5,8), (6,8),
        (7,8), (8,8), (9,8), (10,8), (5,9), (6,9), (7,9), (8,9), (9,9), (10,9), (0,10), (1,10), (2,10), (3,10), (4,10),
        (5,10), (6,10), (7,10), (8,10), (9,10), (10,10), (0,11), (1,11), (2,11), (3,11), (4,11), (5,11), (6,11), (7,11),
        (8,11), (9,11), (0,12), (1,12), (2,12), (3,12), (4,12), (5,12), (6,12), (7,12), (0,13), (1,13), (2,13), (3,13),
        (4,13), (5,13), (0,14), (1,14), (2,14), (3,14), (0,15), (1,15)

    }.AsPoints().ToArray();
    
    [Test] public void Debug() => TestHelpers.DebugShape(TestShape);
    
    public class Fields 
    {
        
        [Test] public void EdgePoints() => Assert.That(TestShape.EdgePoints, Is.EquivalentTo(TestEdgePoints));
        [Test] public void Points() => Assert.That(TestShape.Points, Is.EquivalentTo(TestAllPoints));
    }

    public class Methods
    {
        [Test] public void MirrorX() 
            => Assert.That(TestShape.Mirror(true, false).Corners, Is.EquivalentTo(TestMirroredX.Corners));

        [Test] public void MirrorY() 
            => Assert.That(TestShape.Mirror(false, true).Corners, Is.EquivalentTo(TestMirroredY.Corners));

        [Test] public void Rotate() 
            => Assert.That(TestShape.Rotate(Math.PI / 8).Corners, Is.EquivalentTo(TestRotated.Corners));

        [Test] public void ScaleDouble() 
            => Assert.That(TestShape.Scale(2).Corners, Is.EquivalentTo(TestScaledDoubled.Corners));

        [Test] public void ScaleDoubleDouble() 
            => Assert.That(TestShape.Scale(2, 3).Corners, Is.EquivalentTo(TestScaled.Corners));

        [Test] public void ScaleVector() 
            => Assert.That(TestShape.Scale(new Vector(2, 3)).Corners, Is.EquivalentTo(TestScaled.Corners));

        [Test] public void TranslateIntInt() 
            => Assert.That(TestShape.Translate(25, 50).Corners, Is.EquivalentTo(TestTranslated.Corners));

        [Test] public void TranslateVector() 
            => Assert.That(TestShape.Translate(new Vector(25, 50)).Corners, Is.EquivalentTo(TestTranslated.Corners));
    }
    
    public class Casts 
    {
    }
    
    public class Operators 
    {
    }

    public class Equatable
    {
        [Test] public void Equals() => Assert.True(TestShape.Equals(TestWithNodesOutOfOrder));
        [Test] public void EqualsRef() => Assert.True(TestShape.Equals(TestShape));
        [Test] public void EqualsNull() => Assert.False(TestShape.Equals(null));
        [Test] public void NotEqualsDifferentLength() => Assert.False(TestShape.Equals(TestSquare));
        [Test] public void NotEqualsSameLength() => Assert.False(TestSquare.Equals(TestDiamond));
        
        [Test] public void EqualsObject() => Assert.True(TestShape.Equals((object) TestWithNodesOutOfOrder));
        [Test] public void EqualsObjectRef() => Assert.True(TestShape.Equals((object) TestShape));
        [Test] public void EqualsObjectNull() => Assert.False(TestShape.Equals((object) null));
        // ReSharper disable once SuspiciousTypeConversion.Global
        [Test] public void EqualsObjectDifferentType() => Assert.False(TestShape.Equals(new Point()));
        
        [Test] public void HashCode() 
            => Assert.AreEqual(TestShape.GetHashCode(), TestWithNodesOutOfOrder.GetHashCode());
    }

    [Test] public void String() => Assert.AreEqual("Shape", TestShape.ToString());
    }
}