using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.CloudCreator
{
    public interface ITagsCloudCreator
    {
        Image Create(IEnumerable<KeyValuePair<string, int>> wordStatistics);
    }
}