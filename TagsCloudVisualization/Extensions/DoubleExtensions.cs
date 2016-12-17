using System;

namespace TagsCloudVisualization.Extensions
{
    internal static class DoubleExtensions
    {
        public static bool Equal(this double a, double b, double precision) => Math.Abs(a - b) <= precision;
    }
}