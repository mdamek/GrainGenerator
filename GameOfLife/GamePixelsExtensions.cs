using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public static class GamePixelsExtensions
    {
       

        public static GamePixel Get(this List<GamePixel> gamePixels, int x, int y)
        {
            return gamePixels.FirstOrDefault(e => e.X == x && e.Y == y);
        }
    }
}
