using System;
using System.Collections.Generic;
using System.IO;
using ResultOf;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.WordsSources
{
    public class TxtFileWordsSource : IWordsSource
    {
        private readonly FileSettings settings;

        public TxtFileWordsSource(FileSettings settings)
        {
            this.settings = settings;
        }

        public Result<IEnumerable<string>> GetWords()
        {
            if (!settings.InputFile.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                return Result.Fail<IEnumerable<string>>("Not supported format");
            return Result.Of(() => File.ReadLines(settings.InputFile));
        }
    }
}