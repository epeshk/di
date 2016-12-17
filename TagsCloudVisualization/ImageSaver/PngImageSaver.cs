using System;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.ImageSaver
{
    public class PngImageSaver : IImageSaver
    {
        private readonly FileSettings settings;

        public PngImageSaver(FileSettings settings)
        {
            this.settings = settings;
        }

        public bool TrySave(Image image)
        {
            if (!settings.OutputFile.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                return false;
            image.Save(settings.OutputFile, ImageFormat.Png);
            return true;
        }
    }
}