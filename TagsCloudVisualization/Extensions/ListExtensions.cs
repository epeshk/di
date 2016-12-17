using System;
using System.Collections.Generic;

namespace TagsCloudVisualization.Extensions
{
    public static class ListExtensions
    {
        private static readonly Random random = new Random();
        public static T Choice<T>(this IList<T> list)
        {
            return list[random.Next(0, list.Count)];
        }  
    }
}