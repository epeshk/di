using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using ResultOf;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.WordsSources;

namespace TagsCloudVisualization.WordsPreprocessings
{
    public class ExcludeWords : IWordsPreprocessing
    {
        private readonly IWordsSource[] wordsSource;
        private readonly FileSettings settings;

        public ExcludeWords(IWordsSource[] wordsSource, FileSettings settings)
        {
            this.wordsSource = wordsSource;
            this.settings = settings;
        }

        public Result<IEnumerable<string>> Process(IEnumerable<string> wordsSequence)
        {
            return wordsSource.FirstSuccess(settings.ExcludeWordsFile)
                .Then(exclude => wordsSequence.Where(word => !exclude.Contains(word)));
        }
    }
}