using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public struct Vector
    {
        public double X { get; }
        public double Y { get; }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector Rotate(double angle)
        {
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);

            return new Vector(
                X*cos - Y*sin,
                X*sin + Y*cos
                );
        }

        public Vector RotateDegree(double degree)
        {
            return Rotate(degree*Math.PI/180.0);
        }

        public double Length => Math.Sqrt(X*X + Y*Y);
        public double Angle => Math.Atan2(Y, X);

        public Point ToPoint() => new Point((int) X, (int) Y);
        public Vector Norm() => new Vector(X/Length, Y/Length);
        public Vector ProjectX() => new Vector(X, 0);
        public Vector ProjectY() => new Vector(0, Y);

        public static implicit operator Point(Vector vector)
            => new Point((int) Math.Round(vector.X), (int) Math.Round(vector.Y));

        public static implicit operator Size(Vector vector)
            => new Size((int) Math.Round(vector.X), (int) Math.Round(vector.Y));

        public static implicit operator Vector(Point point)
            => new Vector(point.X, point.Y);

        public static implicit operator Vector(Size size)
            => new Vector(size.Width, size.Height);

        public static Vector operator +(Vector a, Vector b) => new Vector(a.X + b.X, a.Y + b.Y);
        public static Vector operator -(Vector a, Vector b) => new Vector(a.X - b.X, a.Y - b.Y);
        public static Vector operator *(Vector a, double k) => new Vector(a.X*k, a.Y*k);
        public static Vector operator *(double k, Vector a) => new Vector(a.X*k, a.Y*k);
        public static Vector operator -(Vector a) => new Vector(-a.X, -a.Y);

        public static readonly Vector Zero = new Vector(0, 0);
        public static readonly Vector Inf = new Vector(double.MaxValue, double.MaxValue);

        public override string ToString() => $"X: {X}, Y: {Y}";
    }
}