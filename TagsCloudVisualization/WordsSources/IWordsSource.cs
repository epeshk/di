using System.Collections.Generic;
using ResultOf;

namespace TagsCloudVisualization.WordsSources
{
    public interface IWordsSource
    {
        Result<IEnumerable<string>> GetWords(string fileName);
    }
}