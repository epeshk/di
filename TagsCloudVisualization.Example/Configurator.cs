using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.App
{
    public class Configurator : IConfigurator
    {
        private readonly CircularCloudLayouterSettings layouterSettings;
        private readonly DrawingSettings drawingSettings;
        private readonly FileSettings fileSettings;

        public Configurator(
            CircularCloudLayouterSettings layouterSettings,
            DrawingSettings drawingSettings,
            FileSettings fileSettings)
        {
            this.layouterSettings = layouterSettings;
            this.drawingSettings = drawingSettings;
            this.fileSettings = fileSettings;
        }

        public void Configure()
        {
            SettingsForm.For(layouterSettings).ShowDialog();
            SettingsForm.For(drawingSettings).ShowDialog();
            SettingsForm.For(fileSettings).ShowDialog();
        }
    }
}