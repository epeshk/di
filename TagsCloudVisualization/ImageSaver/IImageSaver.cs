using System.Drawing;

namespace TagsCloudVisualization.ImageSaver
{
    public interface IImageSaver
    {
        bool TrySave(Image image);
    }
}