using System;
using System.Collections.Generic;
using System.IO;
using ResultOf;

namespace TagsCloudVisualization.WordsSources
{
    public class TxtFileWordsSource : IWordsSource
    {
        public Result<IEnumerable<string>> GetWords(string fileName)
        {
            if (!fileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                return Result.Fail<IEnumerable<string>>("Can't load file. Not supported format");

            return Result.Of(() => File.ReadLines(fileName), "Can't load file");
        }
    }
}