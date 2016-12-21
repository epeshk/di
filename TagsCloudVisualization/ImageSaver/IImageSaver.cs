using System.Drawing;
using ResultOf;

namespace TagsCloudVisualization.ImageSaver
{
    public interface IImageSaver
    {
        Result<None> TrySave(Image image);
    }
}