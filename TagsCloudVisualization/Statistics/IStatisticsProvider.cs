using System.Collections.Generic;

namespace TagsCloudVisualization.Statistics
{
    public interface IStatisticsProvider
    {
        IEnumerable<KeyValuePair<string, int>> GetStatistics(IEnumerable<string> words);
    }
}