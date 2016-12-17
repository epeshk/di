using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Statistics
{
    public class StatisticsProvider : IStatisticsProvider
    {
        public IEnumerable<KeyValuePair<string, int>> GetStatistics(IEnumerable<string> words)
        {
            return words.GroupBy(w => w).ToDictionary(g => g.Key, g => g.Count());
        }
    }
}