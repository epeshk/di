using System;
using System.Collections.Generic;
using System.Linq;
using ResultOf;
using TagsCloudVisualization.CloudCreator;
using TagsCloudVisualization.ImageSaver;
using TagsCloudVisualization.Statistics;
using TagsCloudVisualization.WordsPreprocessings;
using TagsCloudVisualization.WordsSources;

namespace TagsCloudVisualization.App.Application
{
    public class TagsApp : IApp
    {
        private readonly IWordsSource[] wordsSources;
        private readonly IWordsPreprocessing[] preprocessings;
        private readonly IStatisticsProvider statisticsProvider;
        private readonly ITagsCloudCreator tagsCloudCreator;
        private readonly IImageSaver[] imageSavers;

        public TagsApp(
            IWordsSource[] wordsSources,
            IWordsPreprocessing[] preprocessings,
            IStatisticsProvider statisticsProvider,
            ITagsCloudCreator tagsCloudCreator,
            IImageSaver[] imageSavers)
        {
            this.wordsSources = wordsSources;
            this.preprocessings = preprocessings;
            this.statisticsProvider = statisticsProvider;
            this.tagsCloudCreator = tagsCloudCreator;
            this.imageSavers = imageSavers;
        }

        public void Start()
        {
            var words = wordsSources
                .Select(s => s.GetWords())
                .FirstOrDefault(w => w.IsSuccess)
                .FailOnNull("Can't load input file");

            foreach (var wordsPreprocessing in preprocessings)
            {
                words = words.Then(w => wordsPreprocessing.Process(w));
            }

            var tagCloud = words
                .Then(w => statisticsProvider.GetStatistics(w))
                .Then(s => tagsCloudCreator.Create(s));

            if (!imageSavers.Any(saver => saver.TrySave(tagCloud)))
            {
                Console.WriteLine("Can't save result");
            }
        }
    }
    public static class EnumerableExtensions
        {
            public static T FirstOrDefined<T>(this IEnumerable<T> enumerable, Func<T, bool> condition, T defaultValue)
            {
                foreach (var element in enumerable)
                {
                    if (condition(element))
                        return element;
                }
                return defaultValue;
            }
        }
}