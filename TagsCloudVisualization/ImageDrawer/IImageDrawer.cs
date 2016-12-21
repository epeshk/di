using System.Collections.Generic;
using System.Drawing;
using ResultOf;

namespace TagsCloudVisualization.ImageDrawer
{
    public interface IImageDrawer
    {
        Result<Image> Draw(IEnumerable<Word> words);
    }
}