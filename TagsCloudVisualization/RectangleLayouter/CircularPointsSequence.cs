using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.RectangleLayouter
{
    internal static class CircularPointsSequence
    {
        public static IEnumerable<Vector> GetSequentialPoints(this Vector center, double radiusStep, double segmentStep)
        {
            return GetSequentialPoints(radiusStep, segmentStep).Select(p => p + center);
        }

        private static IEnumerable<Vector> GetSequentialPoints(double radiusStep, double segmentStep)
        {
            return new[] {Vector.Zero}
                .Concat(GetSequence(radiusStep, radiusStep)
                    .SelectMany(r => GetCircle(r, segmentStep)));
        }

        private static IEnumerable<Vector> GetCircle(double radius, double segmentStep)
        {
            var radiusVector = new Vector(0, radius);
            var step = segmentStep/radius;
            return GetSequence(0, step)
                .TakeWhile(x => x < 2*Math.PI)
                .Select(x => radiusVector.Rotate(x));
        }

        private static IEnumerable<double> GetSequence(double first, double step)
        {
            while (true)
            {
                yield return first;
                first += step;
            }
        }
    }
}