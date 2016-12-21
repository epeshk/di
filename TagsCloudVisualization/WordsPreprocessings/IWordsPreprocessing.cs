using System.Collections.Generic;
using ResultOf;

namespace TagsCloudVisualization.WordsPreprocessings
{
    public interface IWordsPreprocessing
    {
        Result<IEnumerable<string>> Process(IEnumerable<string> wordsSequence);
    }
}