using System;
using System.Drawing;
using System.Drawing.Imaging;
using ResultOf;
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

        public Result<None> TrySave(Image image)
        {
            if (!settings.OutputFile.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                return Result.Fail<None>("Can't save image. Not supported format");
            return Result.OfAction(() => image.Save(settings.OutputFile, ImageFormat.Png), "Can't save image");
        }
    }
}