using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.RectangleLayouter
{
    public class CircularCloudLayouter : IRectangleLayouter
    {
        private readonly CircularCloudLayouterSettings settings;
        private readonly List<Rectangle> placedRectangles;

        public CircularCloudLayouter(CircularCloudLayouterSettings settings)
        {
            this.settings = settings;
            placedRectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Vector sizeVector = rectangleSize;

            var rectangleCenter = settings.Center
                .GetSequentialPoints(sizeVector.Length*settings.RadiusScale, settings.StepOnCircle)
                .First(c => CanPlace(c, sizeVector));

            var rectangle = CreateRectangle(rectangleCenter, rectangleSize);
            placedRectangles.Add(rectangle);

            return rectangle;
        }

        private bool CanPlace(Vector rectangleCenter, Vector size)
        {
            var rectangle = CreateRectangle(rectangleCenter, size);
            return !placedRectangles.Any(r => r.IntersectsWith(rectangle));
        }

        private Rectangle CreateRectangle(Vector rectangleCenter, Vector size)
        {
            return new Rectangle(rectangleCenter - 0.5*size, new Size(size.ToPoint()));
        }
    }
}