using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsPreprocessings
{
    public class RemoveShortWords : IWordsPreprocessing
    {
        public IEnumerable<string> Process(IEnumerable<string> wordsSequence)
        {
            return wordsSequence.Where(word => word.Length > 3);
        }
    }
}