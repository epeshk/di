using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Settings
{
    public class DrawingSettings
    {
        public Size ImageSize { get; set; } = new Size(1400, 1400);
        public Color BackgroundColor { get; set; } = Color.CornflowerBlue;
        public List<Color> ForegroundColors { get; set; } = new List<Color>
        {
            Color.DarkBlue,
            Color.AliceBlue,
            Color.DeepSkyBlue,
            Color.Coral
        };

        public FontFamily FontFamily { get; set; } = FontFamily.GenericSerif;
        public int MaximumFontSize { get; set; } = 36;
        public double FontScale { get; set; } = 0.7;
    }
}