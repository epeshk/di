using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.Tests
{
    internal static class AssertionExensions
    {
        public static void ShouldBeApproximately(this Vector actual, Vector expected, double precision = 1e-6)
        {
            if (!(actual.X.Equal(expected.X, precision) && actual.Y.Equal(expected.Y, precision)))
                Assert.Fail($"Expected: {expected}\nBut was: {actual}\nPrecision: {precision}");
        }

        public static void ShouldNotBeApproximately(this Vector actual, Vector expected, double precision = 1e-6)
        {
            if (actual.X.Equal(expected.X, precision) && actual.Y.Equal(expected.Y, precision))
                Assert.Fail($"Expected: {expected}\nBut was: {actual}\nPrecision: {precision}");
        }

        public static void ShouldBeEquivalent<T>(this List<T> actual, List<T> expected, Action<T, T> pairAssertion)
        {
            actual.Should().HaveSameCount(expected);
            for (var i = 0; i < actual.Count; ++i)
                pairAssertion(actual[i], expected[i]);
        }

        public static void AllPairsShould<T>(this IEnumerable<T> actual, Action<T, T> condition)
        {
            var list = actual.ToList();
            for (var i = 0; i < list.Count; ++i)
                for (var j = i + 1; j < list.Count; ++j)
                    condition(list[i], list[j]);
        }
    }
}