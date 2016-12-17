using NUnit.Framework;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.Tests
{
    public class DoubleExtensions_Tests
    {
        [TestCase(1, 1, 0, ExpectedResult = true)]
        [TestCase(1, 2, 2, ExpectedResult = true)]
        [TestCase(0, 1e-7, 1e-6, ExpectedResult = true)]
        [TestCase(double.PositiveInfinity, 1e24, 1, ExpectedResult = false)]
        public bool Equal_Should_compare_doubles_correct(double x, double y, double precision)
        {
            return x.Equal(y, precision);
        }
    }
}