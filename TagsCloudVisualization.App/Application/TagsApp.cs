using System;
using System.Linq;
using System.Windows.Forms;
using ResultOf;
using TagsCloudVisualization.CloudCreator;
using TagsCloudVisualization.ImageSaver;
using TagsCloudVisualization.Settings;
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
        private readonly FileSettings settings;

        public TagsApp(
            IWordsSource[] wordsSources,
            IWordsPreprocessing[] preprocessings,
            IStatisticsProvider statisticsProvider,
            ITagsCloudCreator tagsCloudCreator,
            IImageSaver[] imageSavers,
            FileSettings settings
            )
        {
            this.wordsSources = wordsSources;
            this.preprocessings = preprocessings;
            this.statisticsProvider = statisticsProvider;
            this.tagsCloudCreator = tagsCloudCreator;
            this.imageSavers = imageSavers;
            this.settings = settings;
        }

        public void Start()
        {
            wordsSources.FirstSuccess(settings.InputFile)
                .Then(words => preprocessings.Aggregate(
                    words, (current, wordsPreprocessing) => wordsPreprocessing.Process(current).GetValueOrThrow()))
                .Then(w => statisticsProvider.GetStatistics(w))
                .Then(s => tagsCloudCreator.Create(s))
                .Then(image => imageSavers.First(saver => saver.TrySave(image).IsSuccess))
                .OnFail(e =>
                {
                    MessageBox.Show(e);
                    Environment.Exit(1);
                });
        }
    }
}