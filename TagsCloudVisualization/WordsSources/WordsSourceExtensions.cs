using System.Collections.Generic;
using ResultOf;

namespace TagsCloudVisualization.WordsSources
{
    public static class WordsSourceExtensions
    {
        public static Result<IEnumerable<string>> FirstSuccess(this IWordsSource[] sources, string fileName)
        {
            foreach (var wordsSource in sources)
            {
                var result = wordsSource.GetWords(fileName);
                if (result.IsSuccess)
                    return result;
            }

            return Result.Fail<IEnumerable<string>>("Can't load file: " + fileName);
        }
    }
}