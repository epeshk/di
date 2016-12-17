using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.ImageDrawer
{
    public interface IImageDrawer
    {
        Image Draw(IEnumerable<Word> words);
    }
}