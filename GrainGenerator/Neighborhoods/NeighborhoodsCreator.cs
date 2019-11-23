using System.Collections.Generic;

namespace GameOfLife.Neighborhoods
{
    public static class NeighborhoodsCreator
    {
        public static List<bool> Generate(NeighborhoodType neighborhoodType)
        {
            switch (neighborhoodType)
            {
                case NeighborhoodType.Moore:
                    return GenerateNeighborhoodContainer("1,1,1,1,1,1,1,1");
            }
            return  new List<bool>();
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
