using System.Collections.Generic;
using System.Linq;
using ResultOf;

namespace TagsCloudVisualization.WordsPreprocessings
{
    public class LowercaseWords : IWordsPreprocessing
    {
        public Result<IEnumerable<string>> Process(IEnumerable<string> wordsSequence)
        {
            return Result.Of(() => wordsSequence.Select(word => word.ToLower()));
        }
    }
}