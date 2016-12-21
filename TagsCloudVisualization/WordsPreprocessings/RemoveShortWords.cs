using System.Collections.Generic;
using System.Linq;
using ResultOf;

namespace TagsCloudVisualization.WordsPreprocessings
{
    public class RemoveShortWords : IWordsPreprocessing
    {
        public Result<IEnumerable<string>> Process(IEnumerable<string> wordsSequence)
        {
            return wordsSequence.Where(word => word.Length > 3);
        }
    }
}