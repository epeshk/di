using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class StringMeasurer : IDisposable
    {
        private readonly Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));

        public void Dispose()
        {
            graphics.Dispose();
        }

        public Size BoundRectangleSize(string word, Font font)
        {
            var sizeF = MeasureString(word, font);
            return new Size((int) (sizeF.Width + 1), (int) (sizeF.Height + 1));
        }

        private SizeF MeasureString(string word, Font font)
        {
            return graphics.MeasureString(word, font);
        }
    }
}