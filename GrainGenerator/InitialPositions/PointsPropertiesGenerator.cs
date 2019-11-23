using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public static class PointsPropertiesGenerator
    {
        private static readonly Random Random = new Random();
        public static List<int> GenerateRandom(int count, int min, int max)
        {
            if (max <= min || count < 0 || count > max - min && max - min > 0)
            {
                throw new ArgumentOutOfRangeException("Range " + min + " to " + max +
                        " (" + (max - (long)min) + " values), or count " + count + " is illegal");
            }
            var candidates = new HashSet<int>();      
            for (var top = max - count; top < max; top++)
            {
                if (!candidates.Add(Random.Next(min, top + 1)))
                {
                    candidates.Add(top);
                }
            }
            var result = candidates.ToList();
            for (var i = result.Count - 1; i > 0; i--)
            {
                var k = Random.Next(i + 1);
                var tmp = result[k];
                result[k] = result[i];
                result[i] = tmp;
            }
            return result;
        }

        public static List<Tuple<int,int>> GenerateRandomCoordinates(int randomNumbers, int maxWidth, int maxHeight)
        {
            var random = new Random();
            var coordinates = new HashSet<Tuple<int, int>>();
            for (var i = 0; i < randomNumbers; i++)
            {
                Tuple<int, int> newCoordinates;
                while (true)
                {
                    newCoordinates = new Tuple<int, int>(random.Next(maxWidth), random.Next(maxHeight));
                    if (!coordinates.Contains(newCoordinates)) break;
                }

                coordinates.Add(newCoordinates);
            }

            return coordinates.ToList();
        }
    }
}
