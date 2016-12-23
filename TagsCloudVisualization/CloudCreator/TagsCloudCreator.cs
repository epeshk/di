using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ResultOf;
using TagsCloudVisualization.ImageDrawer;
using TagsCloudVisualization.RectangleLayouter;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.CloudCreator
{
    public class TagsCloudCreator : ITagsCloudCreator
    {
        private readonly Func<IRectangleLayouter> createRectangleLayouter;
        private readonly IImageDrawer imageDrawer;
        private readonly StringMeasurer measurer;
        private readonly DrawingSettings settings;

        public TagsCloudCreator(
            DrawingSettings settings,
            StringMeasurer measurer,
            Func<IRectangleLayouter> createRectangleLayouter,
            IImageDrawer imageDrawer)
        {
            this.settings = settings;
            this.measurer = measurer;
            this.createRectangleLayouter = createRectangleLayouter;
            this.imageDrawer = imageDrawer;
        }

        public Result<Image> Create(IEnumerable<KeyValuePair<string, int>> wordStatistics)
        {
            var wordsArray = wordStatistics.ToArray();
            var maxCountResult = Result.Of(() => wordsArray.Select(w => w.Value).Max());
            if (!maxCountResult.IsSuccess)
                return maxCountResult.ChangeType<int, Image>();
            var maxCount = maxCountResult.GetValueOrThrow();

            return Result.Of(() => createRectangleLayouter())
                .Then(layouter => wordsArray.Select(w =>
                {
                    var font = new Font(settings.FontFamily, GetFontSize(w.Value, maxCount));
                    var size = measurer.BoundRectangleSize(w.Key, font);
                    var rectangle = layouter.PutNextRectangle(size);
                    return new Word(rectangle, font, w.Key);
                }))
                .Then(words => imageDrawer.Draw(words));
        }

        private float GetFontSize(int wordCount, int maxCount)
        {
            return (float) (settings.MaximumFontSize*Math.Pow((float) wordCount/maxCount, settings.FontScale));
        }
    }
}