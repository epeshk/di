using System.Collections.Generic;

namespace TagsCloudVisualization.WordsSources
{
    public interface IWordsSource
    {
        IEnumerable<string> GetWords();
    }
}