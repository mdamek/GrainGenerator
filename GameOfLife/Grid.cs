using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class Grid
    {
        public int WidthElementsNumber { get; set; }
        public int HeightElementsNumber { get; set; }
        public int RandomElementsNumber { get; set; }
        private GamePixel[] BoardValues { get; set; }
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
                    boardValues[i, j] = new GamePixel(i,j);
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

        public void MakeNewGeneration(IRules rules)
        {
            for (var i = 0; i < WidthElementsNumber; i++)
            {
                for (var j = 0; j < HeightElementsNumber; j++)
                {
                    var neighborhoods = GetNeighborhoodsNumber(BoardValues[i, j], WidthElementsNumber, HeightElementsNumber);
                    var decision = rules.ChangeState(neighborhoods, BoardValues[i, j].IsAlive());
                    switch (decision)
                    {
                        case ToState.ToDead:
                            BoardValues[i, j].Kill();
                            break;
                        case ToState.ToLive:
                            BoardValues[i,j].Revive();
                            break;
                        case ToState.DoNotChange:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        public List<GamePixel> GetUsedPixels()
        {
            var usedPixels = new List<GamePixel>();
            for (var i = 0; i < WidthElementsNumber; i++)
            {
                for (var j = 0; j < HeightElementsNumber; j++)
                {
                    if (BoardValues[i, j].IsAlive())
                    {
                        usedPixels.Add(BoardValues[i, j]);
                    }
                }
            }
            return usedPixels;
        }

        private int GetNeighborhoodsNumber(GamePixel gamePixel, int width, int height)
        {
            bool GetValueOfNeighborhood(int x, int y)
            {
                if (x == -1 || y == -1 || x == width || y == height)
                {
                    return false;
                }
                return BoardValues[x, y].IsAlive();
            }
            var neighborhoods = new List<bool>
            {
                GetValueOfNeighborhood(gamePixel.X - 1, gamePixel.Y - 1),
                GetValueOfNeighborhood(gamePixel.X - 1, gamePixel.Y - 1),
                GetValueOfNeighborhood(gamePixel.X, gamePixel.Y - 1),
                GetValueOfNeighborhood(gamePixel.X + 1, gamePixel.Y - 1),
                GetValueOfNeighborhood(gamePixel.X - 1, gamePixel.Y - 1),
                GetValueOfNeighborhood(gamePixel.X + 1, gamePixel.Y - 1),
                GetValueOfNeighborhood(gamePixel.X - 1, gamePixel.Y - 1),
                GetValueOfNeighborhood(gamePixel.X, gamePixel.Y - 1),
                GetValueOfNeighborhood(gamePixel.X + 1, gamePixel.Y - 1)
            };
            return neighborhoods.Count(e => e);
        }

        private GamePixel Get(int x, int y)
        {
            return BoardValues.FirstOrDefault(e => e.X == x && e.Y == y);
        }
    }
}
