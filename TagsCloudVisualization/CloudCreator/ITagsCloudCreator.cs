using System.Collections.Generic;
using System.Drawing;
using ResultOf;

namespace TagsCloudVisualization.CloudCreator
{
    public interface ITagsCloudCreator
    {
        Result<Image> Create(IEnumerable<KeyValuePair<string, int>> wordStatistics);
    }
}