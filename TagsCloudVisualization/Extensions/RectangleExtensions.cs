using System.Drawing;

namespace TagsCloudVisualization.Extensions
{
    internal static class RectangleExtensions
    {
        public static int Area(this Rectangle rectangle) => rectangle.Width*rectangle.Height;
    }
}