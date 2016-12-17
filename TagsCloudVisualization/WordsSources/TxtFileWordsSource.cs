using System;
using System.Collections.Generic;
using System.IO;
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

        public IEnumerable<string> GetWords()
        {
            if (!settings.InputFile.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                return null;
            return File.ReadLines(settings.InputFile);
        }
    }
}