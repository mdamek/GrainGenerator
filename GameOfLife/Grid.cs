using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Grid
    {
        private int WidthElementsNumber { get; }
        private int HeightElementsNumber { get; }
        private List<GamePixel> BoardValues { get; }
        public Grid(int widthElementsNumber, int heightElementsNumber, int randomElementsNumber)
        {
            WidthElementsNumber = widthElementsNumber;
            HeightElementsNumber = heightElementsNumber;
            BoardValues = InitializeBoardValues(widthElementsNumber, heightElementsNumber, randomElementsNumber);
        }

        private List<GamePixel> InitializeBoardValues(int widthElementsNumber, int heightElementsNumber, int randomElementsNumber)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var boardValues =  new List<GamePixel>(widthElementsNumber * heightElementsNumber);
            var randomValues =
                PointsPropertiesGenerator.GenerateRandom(randomElementsNumber, 0, randomElementsNumber);
            var ran = new List<int>(randomValues);
            var randomColorsToUse = 
                PointsPropertiesGenerator.GenerateRandom(randomElementsNumber, 0, 16777216)
                .ConvertAll(Color.FromArgb);
            for (var i = 0; i < widthElementsNumber; i++)
            {
                for (var j = 0; j < heightElementsNumber; j++)
                {
                    boardValues.Add(new GamePixel(i, j));
                }
            }
            for (var i = 0; i < randomElementsNumber; i++)
            {
                boardValues[i].Color = randomColorsToUse[i];
                boardValues[randomValues[i]].Revive();
                boardValues[i].Id = i;
            }
            stopwatch.Stop();
            var stod = stopwatch.Elapsed.Milliseconds;
            return boardValues;
        }

        public void MakeNewGeneration(IRules rules)
        {
            var actualBoard = BoardValues.ConvertAll(e => new GamePixel(e.X, e.Y, e.Id, e.IsAlive(), e.Color));
            Parallel.For(0, actualBoard.Count, i =>
            {
                var actualPixel = actualBoard[i];
                var neighborhoods = GetNeighborhoodsNumber(i, WidthElementsNumber, HeightElementsNumber, actualBoard);
                var decision = rules.ChangeState(neighborhoods, actualPixel.IsAlive());
                switch (decision)
                {
                    case ToState.ToDead:
                        BoardValues[i].Kill();
                        break;
                    case ToState.ToLive:
                        BoardValues[i].Revive();
                        break;
                    case ToState.DoNotChange:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        }

        public List<GamePixel> GetUsedPixels()
        {
            return BoardValues.Where(e => e.IsAlive()).ToList();
        }

        public GamePixel ChangeOnePixelColor(int x, int y)
        {
            var value = BoardValues.SingleOrDefault(e => e.X == x && e.Y == y);
            if(value == null) throw new ArgumentException("There is no that value");
            var index = BoardValues.IndexOf(value);
            if (BoardValues[index].IsAlive())
            {
                BoardValues[index].Kill();
            }
            else
            {
                BoardValues[index].Revive();
            }
            return BoardValues[index];
        }

        private int GetNeighborhoodsNumber(int index, int width, int height, List<GamePixel> gamePixels)
        {
            bool GetValueOfNeighborhood(int x, int y, int i)
            {
                if (x == -1 || y == -1 || x == width || y == height || i < 0 || i >= gamePixels.Count)
                {
                    return false;
                }
                return gamePixels[i].IsAlive();
            }

            var actualPixel = gamePixels[index];
            var neighborhoods = new List<bool>
            {
                GetValueOfNeighborhood(actualPixel.X - 1, actualPixel.Y - 1, index - width - 1),
                GetValueOfNeighborhood(actualPixel.X, actualPixel.Y - 1, index - width),
                GetValueOfNeighborhood(actualPixel.X + 1, actualPixel.Y - 1, index - width + 1),

                GetValueOfNeighborhood(actualPixel.X - 1, actualPixel.Y + 1, index + width - 1),
                GetValueOfNeighborhood(actualPixel.X, actualPixel.Y + 1, index + width),
                GetValueOfNeighborhood(actualPixel.X + 1, actualPixel.Y + 1, index + width + 1),

                GetValueOfNeighborhood(actualPixel.X - 1, actualPixel.Y, index - 1),
                GetValueOfNeighborhood(actualPixel.X + 1, actualPixel.Y, index + 1)
            };
            return neighborhoods.Count(e => e);
        }
    }
}
