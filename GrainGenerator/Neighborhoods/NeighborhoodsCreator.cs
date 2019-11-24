using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife.Neighborhoods
{
    public static class NeighborhoodsCreator
    {
        public static List<bool> Generate(string neighborhoodType)
        {
            switch (neighborhoodType)
            {
                case "Moore":
                    return GenerateNeighborhoodContainer("1,1,1,1,1,1,1,1");
                case "Von Neumann":
                    return GenerateNeighborhoodContainer("0,1,0,1,1,0,1,0");
                case "Hexagonal left":
                    return GenerateNeighborhoodContainer("0,1,1,1,1,1,1,0");
                case "Hexagonal right":
                    return GenerateNeighborhoodContainer("1,1,0,1,1,0,1,1");
                case "Hexagonal random":
                {
                    const int randNumbers = 6;
                    var readyValue = GenerateRandomNeighborhood(randNumbers);
                    return GenerateNeighborhoodContainer(readyValue);
                }
                case "Pentagonal random":
                {
                    const int randNumbers = 5;
                    var readyValue = GenerateRandomNeighborhood(randNumbers);
                    return GenerateNeighborhoodContainer(readyValue);
                }

                case "Tetragonal random":
                {
                    const int randNumbers = 4;
                    var readyValue = GenerateRandomNeighborhood(randNumbers);
                    return GenerateNeighborhoodContainer(readyValue);
                    }
                case "Triagonal random":
                {
                    const int randNumbers = 3;
                    var readyValue = GenerateRandomNeighborhood(randNumbers);
                    return GenerateNeighborhoodContainer(readyValue);
                }
                case "Diagonal random":
                {
                    const int randNumbers = 2;
                    var readyValue = GenerateRandomNeighborhood(randNumbers);
                    return GenerateNeighborhoodContainer(readyValue);
                }
                case "Monogonal random":
                {
                    const int randNumbers = 1;
                    var readyValue = GenerateRandomNeighborhood(randNumbers);
                    return GenerateNeighborhoodContainer(readyValue);
                }
            }
            return  new List<bool>();
        }

        private static string GenerateRandomNeighborhood(int randNumbers)
        {
            var random = new Random();
            var randomValues = new List<int>();
            while (randomValues.Count < randNumbers)
            {
                var actualRandom = random.Next(0, 7);
                if (randomValues.Contains(actualRandom)) continue;
                randomValues.Add(actualRandom);
            }
            var ready = new List<int>(8){0,0,0,0,0,0,0,0};
            foreach (var randomValue in randomValues)
            {
                ready[randomValue] = 1;
            }
            var readyValue = string.Join(",", ready);
            return readyValue;
        }

        private static List<bool> GenerateNeighborhoodContainer(string elements)
        {
            var toAccept = elements.Split(',');
            return new List<bool>
            {
                toAccept[0] == "1",
                toAccept[1] == "1",
                toAccept[2] == "1",
                toAccept[3] == "1",
                toAccept[4] == "1",
                toAccept[5] == "1",
                toAccept[6] == "1",
                toAccept[7] == "1"
            };
        }
    }
}
