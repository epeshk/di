using System;
using System.Linq;
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
            var words = wordsSources.Select(s => s.GetWords()).FirstOrDefault(w => w != null);
            if (words == null)
            {
                Console.WriteLine("Can't load input file");
                Environment.Exit(0);
            }
            foreach (var wordsPreprocessing in preprocessings)
            {
                words = wordsPreprocessing.Process(words);
            }
            var statistics = statisticsProvider.GetStatistics(words);
            var tagCloud = tagsCloudCreator.Create(statistics);
            if(!imageSavers.Any(saver => saver.TrySave(tagCloud)))
            {
                Console.WriteLine("Can't save result");
            }
        }
    }
}