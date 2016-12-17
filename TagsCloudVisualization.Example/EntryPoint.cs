using System;
using SimpleInjector;
using TagsCloudVisualization.App.Application;
using TagsCloudVisualization.CloudCreator;
using TagsCloudVisualization.ImageDrawer;
using TagsCloudVisualization.ImageSaver;
using TagsCloudVisualization.RectangleLayouter;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.Statistics;
using TagsCloudVisualization.WordsPreprocessings;
using TagsCloudVisualization.WordsSources;

namespace TagsCloudVisualization.App
{
    internal static class EntryPoint
    {
        private static void Main()
        {
            var container = new Container();
            container.Register<Func<IRectangleLayouter>>(() =>
            {
                var settings = container.GetInstance<CircularCloudLayouterSettings>();
                return () => new CircularCloudLayouter(settings);
            });
            container.Register<ITagsCloudCreator, TagsCloudCreator>();
            container.Register<IStatisticsProvider, StatisticsProvider>();
            container.Register<StringMeasurer>(Lifestyle.Singleton);
            container.RegisterCollection<IWordsPreprocessing>(new[] {typeof(RemoveShortWords), typeof(LowercaseWords)});
            container.RegisterCollection<IWordsSource>(new [] {typeof(TxtFileWordsSource)});
            container.RegisterCollection<IImageSaver>(new[] {typeof(PngImageSaver)});
            container.Register<IImageDrawer, ImageDrawer.ImageDrawer>();
            container.Register<CircularCloudLayouterSettings>(Lifestyle.Singleton);
            container.Register<DrawingSettings>(Lifestyle.Singleton);
            container.Register<FileSettings>(Lifestyle.Singleton);

            container.Register<IConfigurator, Configurator>();
            container.Register<IApp, TagsApp>();

            container.Verify();

            container.GetInstance<IConfigurator>().Configure();
            container.GetInstance<IApp>().Start();
        }
    }
}