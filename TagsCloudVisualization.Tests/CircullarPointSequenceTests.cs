using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.RectangleLayouter;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class CircularPointsSequence_Tests
    {
        private IEnumerable<Vector> CreateLoop(Vector center)
        {
            return center.GetSequentialPoints(1, 1);
        }

        [Test]
        public void AllPoints_Should_be_distinct()
        {
            var points = CreateLoop(new Vector(1, 1)).Take(500).ToList();

            points.AllPairsShould((a, b) => a.ShouldNotBeApproximately(b));
        }

        [Test]
        public void CenterVector_Should_be_added_to_all_points()
        {
            var loopWithZeroCenter = CreateLoop(new Vector(0, 0)).Take(100).ToList();

            var center = new Vector(1, 2);
            var loopWithNonZeroCenter = CreateLoop(center).Take(100).ToList();
            var loopWithNonZeroCenterMinusCenter = loopWithNonZeroCenter.Select(vector => vector - center).ToList();

            loopWithNonZeroCenterMinusCenter.ShouldBeEquivalent(loopWithZeroCenter, (a, b) => a.ShouldBeApproximately(b));
        }

        [Test]
        public void FirstPoint_Should_be_center()
        {
            var center = new Vector(1, 2);
            var firstPoint = CreateLoop(center).First();

            firstPoint.ShouldBeApproximately(center);
        }

        [Test]
        public void Radius_Should_be_equal_for_points_on_same_circle()
        {
            var points = CreateLoop(new Vector(0, 0)).Take(3).ToList();

            points[1].Length.Should().BeApproximately(points[2].Length, 1e-6);
        }

        [Test]
        public void Radius_Should_increase_after_full_rotation()
        {
            var points = new Vector(0, 0).GetSequentialPoints(1, 3.2).Take(4).ToList();
            points[3].Length.Should().BeGreaterThan(points[1].Length);
        }
    }
}