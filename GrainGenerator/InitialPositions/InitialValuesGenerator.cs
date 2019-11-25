using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife.InitialPositions
{
    public static class InitialValuesGenerator
    {
        public static GamePixel[,] RandomValues(int randomElementsNumber, int widthElementsNumber, int heightElementsNumber)
        {
            var boardValues = new GamePixel[widthElementsNumber, heightElementsNumber];
            var randomValues = PointsPropertiesGenerator.GenerateRandom(randomElementsNumber, 0, boardValues.Length);
            var randomColorsToUse =
                PointsPropertiesGenerator.GenerateRandom(randomElementsNumber, 0, 16777216)
                    .ConvertAll(Color.FromArgb);
            Parallel.For(0, randomColorsToUse.Capacity,
                i => { randomColorsToUse[i] = Color.FromArgb(255, randomColorsToUse[i]); });
            var randomizeValue = 0;
            for (var i = 0; i < widthElementsNumber; i++)
            {
                for (var j = 0; j < heightElementsNumber; j++)
                {
                    boardValues[i,j] = new GamePixel{Id = randomizeValue, GrainValue = false};
                    randomizeValue++;
                }
            }           
            var flattened = Enumerable.Range(0, boardValues.GetLength(0)).SelectMany(x => Enumerable.Range(0, boardValues.GetLength(1)).Select(y => boardValues[x, y]));
            var distinct = flattened.Distinct().ToList();
            for (var i = 0; i < randomValues.Capacity; i++)
            {
                distinct[randomValues[i]].MakeGrain();
                distinct[randomValues[i]].Color = randomColorsToUse[i];
            }
            var iterator = 0;
            for (var i = 0; i < widthElementsNumber; i++)
            {
                for (var j = 0; j < heightElementsNumber; j++)
                {
                    boardValues[i, j] = distinct[iterator];
                    iterator++;
                }
            }
            return boardValues;
        }

        public static Color RandomColor()
        {

            var random = new Random(Guid.NewGuid().GetHashCode());
            var randomNumber = random.Next(0, 16777216);
            var color = Color.FromArgb(randomNumber);
            return Color.FromArgb(255, color);
        }

        public static GamePixel[,] ValuesFromClicks(IEnumerable<Tuple<int, int, Color>> clicks, int widthElementsNumber, int heightElementsNumber)
        {
            if (clicks == null) MessageBox.Show("You should click", "Error", MessageBoxButtons.OK);
            var boardValues = new GamePixel[widthElementsNumber, heightElementsNumber];
            var randomizeValue = 0;
            for (var i = 0; i < widthElementsNumber; i++)
            {
                for (var j = 0; j < heightElementsNumber; j++)
                {
                    boardValues[i, j] = new GamePixel { Id = randomizeValue, GrainValue = false };
                    randomizeValue++;
                }
            }

            foreach (var click in clicks)
            {
                boardValues[click.Item1, click.Item2].GrainValue = true;
                boardValues[click.Item1, click.Item2].Color = click.Item3;
            }

            return boardValues;
        }

        public static GamePixel[,] EvenlyValues(int evenValuesNumber, int widthElementsNumber, int heightElementsNumber)
        {
            var boardValues = new GamePixel[widthElementsNumber, heightElementsNumber];
            return boardValues;
        }

        public static GamePixel[,] CircleValues(int rValue, int widthElementsNumber, int heightElementsNumber)
        {
            var boardValues = new GamePixel[widthElementsNumber, heightElementsNumber];
            var randomizeValue = 0;
            for (var i = 0; i < widthElementsNumber; i++)
            {
                for (var j = 0; j < heightElementsNumber; j++)
                {
                    boardValues[i, j] = new GamePixel { Id = randomizeValue, GrainValue = false };
                    randomizeValue++;
                }
            }
            var readyPoints = new List<Tuple<int, int>>();
            var random = new Random(Guid.NewGuid().GetHashCode());
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            while (stopWatch.Elapsed.Seconds < 3)
            {
                var randX = random.Next(widthElementsNumber);
                var randY = random.Next(heightElementsNumber);
                var approves = 0;
                if (readyPoints.Any(e => e.Item1 == randX && e.Item2 == randY)) continue;
                foreach (var readyPoint in readyPoints)
                {
                    if (CanBeAddedByDistance(readyPoint.Item1, readyPoint.Item2, randX, randY, rValue))
                    {
                        approves++;
                    }
                }

                if (approves == readyPoints.Count)
                {
                    readyPoints.Add(new Tuple<int, int>(randX, randY));
                }

                approves = 0;
            }
            stopWatch.Stop();
            foreach (var readyPoint in readyPoints)
            {
                boardValues[readyPoint.Item1, readyPoint.Item2].GrainValue = true;
                boardValues[readyPoint.Item1, readyPoint.Item2].Color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            }
            return boardValues;
        }

        private static bool CanBeAddedByDistance(int oldX, int oldY, int newX, int newY, int r)
        {
            var distance = Math.Sqrt(Math.Pow(newX - oldX, 2) + Math.Pow(newY - oldY, 2));
            return r < distance;
        }
    }
}
