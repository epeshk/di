using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsPreprocessings
{
    public class LowercaseWords : IWordsPreprocessing
    {
        public IEnumerable<string> Process(IEnumerable<string> wordsSequence)
        {
            return wordsSequence.Select(word => word.ToLower());
        }
    }
}