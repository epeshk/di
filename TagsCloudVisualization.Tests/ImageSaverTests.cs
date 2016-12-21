using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.ImageSaver;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class ImageSaverTests
    {
        [Test]
        public void PngImageSaver_should_return_true_on_png_file()
        {
            var settings = new FileSettings
            {
                OutputFile = "file.png"
            };
            var saver = new PngImageSaver(settings);
            saver.TrySave(new Bitmap(1, 1)).Should().BeTrue();
        }

        [Test]
        public void PngImageSaver_should_return_false_on_non_png_file()
        {
            var settings = new FileSettings
            {
                OutputFile = "file.wtf"
            };
            var saver = new PngImageSaver(settings);
            saver.TrySave(new Bitmap(1, 1)).Should().BeFalse();
        }
    }
}