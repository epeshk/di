using System;
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
            Result.Of(() => wordsSources
                .Select(s => s.GetWords())
                .First(w => w.IsSuccess))
                .Then(words => preprocessings.Aggregate(
                    words, (current, wordsPreprocessing) => current.Then(wordsPreprocessing.Process)))
                .Then(w => statisticsProvider.GetStatistics(w))
                .Then(s => tagsCloudCreator.Create(s))
                .Then(image => imageSavers.First(saver => saver.TrySave(image).IsSuccess))
                .OnFail(e =>
                {
                    Console.WriteLine(e);
                    Environment.Exit(1);
                });
        }
    }
}