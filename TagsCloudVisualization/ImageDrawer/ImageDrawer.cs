using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.ImageDrawer
{
    public class ImageDrawer : IImageDrawer
    {
        private readonly DrawingSettings settings;

        public ImageDrawer(DrawingSettings settings)
        {
            this.settings = settings;
        }

        public Image Draw(IEnumerable<Word> words)
        {
            var bitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(new SolidBrush(settings.BackgroundColor), 0, 0, settings.ImageSize.Width, settings.ImageSize.Height);
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                var brushes = settings.ForegroundColors.Select(color => new SolidBrush(color)).ToList();
                foreach (var word in words)
                {
                    graphics.DrawString(word.Label, word.Font, brushes.Choice(), word.Rectangle);
                }
            }

            return bitmap;
        }
    }
}