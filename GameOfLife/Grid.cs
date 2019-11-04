using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class Grid
    {
        public int WidthElementsNumber { get; set; }
        public int HeightElementsNumber { get; set; }
        public int RandomElementsNumber { get; set; }
        private GamePixel[,] BoardValues { get; set; }
        public Grid(int widthElementsNumber, int heightElementsNumber, int randomElementsNumber)
        {
            WidthElementsNumber = widthElementsNumber;
            HeightElementsNumber = heightElementsNumber;
            RandomElementsNumber = randomElementsNumber;
            BoardValues = InitializeBoardValues(widthElementsNumber, heightElementsNumber, randomElementsNumber);
        }

        private GamePixel[,] InitializeBoardValues(int widthElementsNumber, int heightElementsNumber, int randomElementsNumber)
        {
            var boardValues =  new GamePixel[widthElementsNumber, heightElementsNumber];
            var random = new Random();
            for (var i = 0; i < widthElementsNumber; i++)
            {
                for (var j = 0; j < heightElementsNumber; j++)
                {
                    boardValues[i, j] = new GamePixel();
                }
            }
            for (var i = 0; i < randomElementsNumber; i++)
            {
                int randomX;
                int randomY;
                while (true)
                {
                    randomX = random.Next(0, widthElementsNumber);
                    randomY = random.Next(0, heightElementsNumber);
                    if (!boardValues[randomX, randomY].IsAlive()) break;
                }
                boardValues[randomX, randomY].Revive();
            }
            return boardValues;
        }

        public GamePixel GetGamePixel(int x, int y)
        {
            return BoardValues[x, y];
        }

        public GamePixel[,] MakeNewGeneration(Rules rules)
        {
            return this.BoardValues;
        }
    }
}
