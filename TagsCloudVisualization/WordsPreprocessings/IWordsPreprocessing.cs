using System.Collections.Generic;

namespace TagsCloudVisualization.WordsPreprocessings
{
    public interface IWordsPreprocessing
    {
        IEnumerable<string> Process(IEnumerable<string> wordsSequence);
    }
}