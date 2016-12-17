using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    internal class Vector_Tests
    {
        [TestCase(2.5, 0)]
        [TestCase(0, -2.5)]
        [TestCase(2, 2)]
        public void Norm_should_return_vector_with_length_1(double x, double y)
        {
            var vector = new Vector(x, y);
            var normalized = vector.Norm();

            normalized.Length.Should().BeApproximately(1, 1e-6);
        }

        [TestCase(2.5, 0)]
        [TestCase(0, -2.5)]
        [TestCase(2, 2)]
        public void Norm_should_return_vector_with_same_direction(double x, double y)
        {
            var vector = Vector(x, y);
            var normalized = vector.Norm();

            normalized.Angle.Should().BeApproximately(vector.Angle, 1e-6);
        }

        [TestCase(1, 1)]
        [TestCase(-10, 12)]
        public void Point_Should_be_convertible_to_vector(int x, int y)
        {
            var point = new Point(x, y);
            var expected = new Vector(x, y);

            Vector result = point;

            result.ShouldBeApproximately(expected);
        }

        [TestCase(1, 1)]
        [TestCase(-10.1, 12.1)]
        public void ProjectX_should_project_vector_to_OX(double x, double y)
        {
            var vector = new Vector(x, y);
            var expectedVector = new Vector(x, 0);

            var result = vector.ProjectX();

            result.Should().Be(expectedVector);
        }

        [TestCase(1, 1)]
        [TestCase(-10.1, 12.1)]
        public void ProjectY_should_project_vector_to_OY(double x, double y)
        {
            var vector = new Vector(x, y);
            var expectedVector = new Vector(0, y);

            var result = vector.ProjectY();

            result.Should().Be(expectedVector);
        }

        [TestCase(1, 1)]
        [TestCase(-10, 12)]
        public void Size_ShouldBeConvertibleToVector(int width, int height)
        {
            var size = new Size(width, height);
            var expectedVector = new Vector(width, height);

            Vector conversionResult = size;

            conversionResult.Should().Be(expectedVector);
        }

        [TestCase(1, 1)]
        [TestCase(-10, 12)]
        public void Vector_Should_be_convertible_to_point(int x, int y)
        {
            var vector = new Vector(x, y);
            var expectedPoint = new Point(x, y);

            Point conversionResult = vector;

            conversionResult.X.ShouldBeEquivalentTo(expectedPoint.X);
            conversionResult.Y.ShouldBeEquivalentTo(expectedPoint.Y);
        }

        [TestCase(1, 1)]
        [TestCase(-10, 12)]
        public void Vector_Should_be_convertible_to_size(int x, int y)
        {
            var vector = new Vector(x, y);
            var expectedSize = new Size(x, y);

            Size conversionResult = vector;

            conversionResult.Width.ShouldBeEquivalentTo(expectedSize.Width);
            conversionResult.Height.ShouldBeEquivalentTo(expectedSize.Height);
        }

        private readonly TestCaseData[] operatorPlusTestCases =
        {
            new TestCaseData(Vector(0, 0), Vector(0, 0), Vector(0, 0)),
            new TestCaseData(Vector(0, 0), Vector(1, 1), Vector(1, 1)),
            new TestCaseData(Vector(1, 2), Vector(3, -1), Vector(4, 1)),
            new TestCaseData(Vector(0.5, 0), Vector(0, 0), Vector(0.5, 0))
        };

        private readonly TestCaseData[] operatorMinusTestCases =
        {
            new TestCaseData(Vector(0, 0), Vector(0, 0), Vector(0, 0)),
            new TestCaseData(Vector(0, 0), Vector(1, 1), Vector(-1, -1)),
            new TestCaseData(Vector(1.5, 2), Vector(3, -1), Vector(-1.5, 3))
        };

        private readonly TestCaseData[] operatorVectorMulDoubleTestCases =
        {
            new TestCaseData(Vector(0, 0), 0, Vector(0, 0)),
            new TestCaseData(Vector(0, 1), 2.5, Vector(0, 2.5)),
            new TestCaseData(Vector(1.5, 2), 3, Vector(4.5, 6))
        };

        private readonly TestCaseData[] operatorDoubleMulVectorTestCases =
        {
            new TestCaseData(0, Vector(0, 0), Vector(0, 0)),
            new TestCaseData(2.5, Vector(0, 1), Vector(0, 2.5)),
            new TestCaseData(3, Vector(1.5, 2), Vector(4.5, 6))
        };

        private readonly TestCaseData[] unaryMinusTestCases =
        {
            new TestCaseData(Vector(0, 0), Vector(0, 0)),
            new TestCaseData(Vector(0, 1), Vector(0, -1)),
            new TestCaseData(Vector(1.5, 0.1), Vector(-1.5, -0.1))
        };

        private readonly TestCaseData[] rotateTestCases =
        {
            new TestCaseData(Vector(1, 0), Math.PI/2, Vector(0, 1)),
            new TestCaseData(Vector(1, 0), -Math.PI/2, Vector(0, -1)),
            new TestCaseData(Vector(1, 0), 2*Math.PI + Math.PI/2, Vector(0, 1)).SetName("Angle over 2pi")
        };


        private readonly TestCaseData[] rotateDegreeTestCases =
        {
            new TestCaseData(Vector(1, 0), 90, Vector(0, 1)),
            new TestCaseData(Vector(1, 0), -90, Vector(0, -1)),
            new TestCaseData(Vector(1, 0), 360 + 90, Vector(0, 1)).SetName("Angle over 2pi")
        };

        private static Vector Vector(double x, double y) => new Vector(x, y);

        [Test, TestCaseSource(nameof(operatorDoubleMulVectorTestCases))]
        public void OperatorDoubleMulVector_Should_correctly_multiply_double_to_vector(double k, Vector a,
            Vector expected)
            => (k * a).ShouldBeApproximately(expected);

        [Test, TestCaseSource(nameof(operatorMinusTestCases))]
        public void OperatorMinus_ShouldCorrectlySubstractVectors(Vector a, Vector b, Vector expected)
            => (a - b).ShouldBeApproximately(expected);

        [Test, TestCaseSource(nameof(operatorPlusTestCases))]
        public void OperatorPlus_ShouldCorrectlySumVectors(Vector a, Vector b, Vector expected)
            => (a + b).ShouldBeApproximately(expected);

        [Test, TestCaseSource(nameof(unaryMinusTestCases))]
        public void OperatorUnaryMinus_Should_correctly_invert_vector(Vector vector, Vector expected)
            => (-vector).ShouldBeApproximately(expected);

        [Test, TestCaseSource(nameof(operatorVectorMulDoubleTestCases))]
        public void OperatorVectorMulDouble_Should_correctly_multiply_vector_to_double(Vector a, double k,
            Vector expected)
            => (a * k).ShouldBeApproximately(expected);

        [Test, TestCaseSource(nameof(rotateTestCases))]
        public void Rotate_Should_be_correct(Vector vector, double angle, Vector expected)
            => vector.Rotate(angle).ShouldBeApproximately(expected);

        [Test, TestCaseSource(nameof(rotateDegreeTestCases))]
        public void RotateDegree_Should_be_correct(Vector vector, double degree, Vector expected)
            => vector.RotateDegree(degree).ShouldBeApproximately(expected);
    }
}