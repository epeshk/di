using System.Drawing;

namespace TagsCloudVisualization
{
    public class Word
    {
        public Word(Rectangle rectangle, Font font, string label)
        {
            Rectangle = rectangle;
            Font = font;
            Label = label;
        }

        public Rectangle Rectangle { get; }
        public Font Font { get; }
        public string Label { get; }
    }
}