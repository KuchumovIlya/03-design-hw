using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloudTask
{
    public static class CollectionsUtils
    {
        public static T[] RandomShuffle<T>(this IEnumerable<T> source, Random random)
        {
            var sourceArray = source.ToArray();
            for (var i = 1; i < sourceArray.Length; i++)
            {
                var swapIndex = random.Next(i - 1);
                var temp = sourceArray[swapIndex];
                sourceArray[swapIndex] = sourceArray[i];
                sourceArray[i] = temp;
            }
            return sourceArray;
        }
    }
}
