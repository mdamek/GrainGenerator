using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameOfLife.InitialPositions;
using GameOfLife.Neighborhoods;

namespace GameOfLife
{
    public class Grid
    {
        public int WidthElementsNumber { get; }
        public int HeightElementsNumber { get; }
        public GamePixel[,] BoardValues { get; set; }
        public ConcurrentBag<GamePixelWithCoordinates> Grained { get; set; }
        private bool PeriodicValues { get; }
        private readonly List<bool> _neighborhoodsPositions;

        public Grid(int widthElementsNumber, int heightElementsNumber, int randomElementsNumber, bool periodicValues,
            string initialSetting, string neighborhoodType, List<Tuple<int, int, Color>> toDrawValues)
        {
            WidthElementsNumber = widthElementsNumber;
            HeightElementsNumber = heightElementsNumber;
            PeriodicValues = periodicValues;
            BoardValues = InitializeBoardValues(widthElementsNumber, heightElementsNumber, randomElementsNumber,
                initialSetting, toDrawValues);
            GetGrainedAndNotPixels();
            _neighborhoodsPositions = NeighborhoodsCreator.Generate(neighborhoodType);
        }

        private void GetGrainedAndNotPixels()
        {
            Grained = new ConcurrentBag<GamePixelWithCoordinates>();
            for (var i = 0; i < WidthElementsNumber; i++)
            {
                for (var j = 0; j < HeightElementsNumber; j++)
                {
                    if (BoardValues[i, j].IsGrain())
                    {
                        Grained.Add(new GamePixelWithCoordinates
                        {
                            X = i,
                            Y = j,
                            Color = BoardValues[i, j].Color,
                            GrainValue = BoardValues[i, j].IsGrain(),
                            Id = BoardValues[i, j].Id
                        });
                    }
                }
            }
        }

        private GamePixel[,] InitializeBoardValues(int widthElementsNumber, int heightElementsNumber,
            int randomElementsNumber, string initialSetting, List<Tuple<int, int, Color>> toDrawValues)
        {
            GamePixel[,] readyBoard = null;
            switch (initialSetting)
            {
                case "Random":
                    readyBoard = InitialValuesGenerator.RandomValues(randomElementsNumber, widthElementsNumber,
                        heightElementsNumber);
                    break;
                case "Clicks":
                    readyBoard = InitialValuesGenerator.ValuesFromClicks(toDrawValues, widthElementsNumber,
                        heightElementsNumber);
                    break;
                case "Circle area":
                    break;
                case "Evenly":
                    readyBoard = InitialValuesGenerator.EvenlyValues(randomElementsNumber, widthElementsNumber,
                        heightElementsNumber);
                    break;
                default:
                    throw new ArgumentException("Impossible value");
            }

            return readyBoard;
        }

        public void MakeNewGeneration(ListView listView)
        {
            Grained = new ConcurrentBag<GamePixelWithCoordinates>();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var temporaryBoard = new GamePixel[WidthElementsNumber, HeightElementsNumber];
            for (var i = 0; i < WidthElementsNumber; i++)
            {
                for (var j = 0; j < HeightElementsNumber; j++)
                {
                    var actual = BoardValues[i, j];
                    temporaryBoard[i, j] = new GamePixel
                        {Color = actual.Color, GrainValue = actual.GrainValue, Id = actual.Id};
                }
            }

            var toGrain = GetPixelsToGrain(temporaryBoard);
            Parallel.For(0, toGrain.Count, new ParallelOptions {MaxDegreeOfParallelism = 8},
                i => { NeighborhoodsAction(temporaryBoard, toGrain[i]); });
            stopWatch.Stop();
            var row = new ListViewItem(new[] {"Calculation", stopWatch.ElapsedMilliseconds.ToString()});
            listView.Items.Insert(0, row);
        }

        public int AddValue(int x, int y)
        {
            if (x > BoardValues.GetLength(0) || y > BoardValues.GetLength(1)) return 0;
            if (BoardValues[x, y].IsGrain())
            {
                MessageBox.Show("This pixel is already grained", "Error", MessageBoxButtons.OK);
                return 0;
            }

            var randomColor = InitialValuesGenerator.RandomColor();
            BoardValues[x, y].GrainValue = true;
            BoardValues[x, y].Color = randomColor;
            Grained.Add(new GamePixelWithCoordinates
            {
                Color = randomColor,
                GrainValue = true,
                Id = BoardValues[x, y].Id,
                X = x,
                Y = y
            });

            return 1;
        }

        public List<GamePixelWithCoordinates> GetPixelsToGrain(GamePixel[,] boardValues)
        {
            var pixels = new List<GamePixelWithCoordinates>();
            for (var i = 0; i < WidthElementsNumber; i++)
            {
                for (var j = 0; j < HeightElementsNumber; j++)
                {
                    if (boardValues[i, j].IsGrain()) continue;
                    var actual = BoardValues[i, j];
                    pixels.Add(new GamePixelWithCoordinates
                        {Color = actual.Color, GrainValue = actual.GrainValue, Id = actual.Id, X = i, Y = j});
                }
            }

            return pixels;
        }

        //public GamePixel ChangeOnePixelColor(int x, int y)
        //{
        //    var value = BoardValues.SingleOrDefault(e => e.X == x && e.Y == y);
        //    if(value == null) throw new ArgumentException("There is no that value");
        //    var index = BoardValues.IndexOf(value);
        //    if (BoardValues[index].IsGrain())
        //    {

        //    }
        //    else
        //    {
        //        BoardValues[index].MakeGrain();
        //    }
        //    return BoardValues[index];
        //}
        private void NeighborhoodsAction(GamePixel[,] temporaryBoard, GamePixelWithCoordinates actualPixel)
        {
            var x = actualPixel.X;
            var y = actualPixel.Y;

            var neighborhoods = new GamePixel[8];

            if (x - 1 >= 0 && y - 1 >= 0)
            {
                if (_neighborhoodsPositions[0]) neighborhoods[0] = temporaryBoard[x - 1, y - 1];
            }
            else
            {
                if (PeriodicValues)
                {
                    if (x == 0 && y == 0)
                    {
                        neighborhoods[0] = temporaryBoard[WidthElementsNumber - 1, HeightElementsNumber - 1];
                    }
                    else
                    {
                        if (x == 0 && y != 0)
                        {
                            neighborhoods[0] = temporaryBoard[x - 1 + WidthElementsNumber, y - 1];
                        }

                        if (y == 0 && x != 0)
                        {
                            neighborhoods[0] = temporaryBoard[x - 1, y - 1 + HeightElementsNumber];
                        }
                    }
                }
                else
                {
                    neighborhoods[0] = null;
                }
            }

            if (y - 1 >= 0)
            {
                if (_neighborhoodsPositions[1]) neighborhoods[1] = temporaryBoard[x, y - 1];
            }
            else
            {
                if (PeriodicValues)
                {
                    neighborhoods[1] = temporaryBoard[x, y + HeightElementsNumber - 1];
                }
                else
                {
                    neighborhoods[1] = null;
                }
            }

            if (x + 1 < WidthElementsNumber && y - 1 >= 0)
            {
                if (_neighborhoodsPositions[2]) neighborhoods[2] = temporaryBoard[x + 1, y - 1];
            }
            else
            {
                if (PeriodicValues)
                {
                    if (x == WidthElementsNumber - 1 && y == 0)
                    {
                        neighborhoods[2] = temporaryBoard[0, HeightElementsNumber - 1];
                    }
                    else
                    {
                        if (x == WidthElementsNumber - 1 && y != 0)
                        {
                            neighborhoods[2] = temporaryBoard[x + 1 - WidthElementsNumber, y - 1];
                        }

                        if (y == 0 && x != 0)
                        {
                            neighborhoods[2] = temporaryBoard[x + 1, y - 1 + HeightElementsNumber];
                        }
                    }
                }
                else
                {
                    neighborhoods[2] = null;
                }
            }

            if (x - 1 >= 0)
            {
                if (_neighborhoodsPositions[3]) neighborhoods[3] = temporaryBoard[x - 1, y];
            }
            else
            {
                if (PeriodicValues)
                {
                    neighborhoods[3] = temporaryBoard[x + WidthElementsNumber - 1, y];
                }
                else
                {
                    neighborhoods[3] = null;
                }
            }

            if (x + 1 < WidthElementsNumber)
            {
                if (_neighborhoodsPositions[4]) neighborhoods[4] = temporaryBoard[x + 1, y];
            }
            else
            {
                if (PeriodicValues)
                {
                    neighborhoods[4] = temporaryBoard[x - WidthElementsNumber + 1, y];
                }
                else
                {
                    neighborhoods[4] = null;
                }
            }

            if (x - 1 >= 0 && y + 1 < HeightElementsNumber)
            {
                if (_neighborhoodsPositions[5]) neighborhoods[5] = temporaryBoard[x - 1, y + 1];
            }
            else
            {
                if (PeriodicValues)
                {
                    if (x == 0 && y == HeightElementsNumber - 1)
                    {
                        neighborhoods[5] = temporaryBoard[WidthElementsNumber - 1, 0];
                    }
                    else
                    {
                        if (x == 0 && y != 0)
                        {
                            neighborhoods[5] = temporaryBoard[x - 1 + WidthElementsNumber, y - 1];
                        }

                        if (y == 0 && x != 0)
                        {
                            neighborhoods[5] = temporaryBoard[x - 1, y + 1 + HeightElementsNumber];
                        }
                    }
                }
                else
                {
                    neighborhoods[5] = null;
                }
            }

            if (y + 1 < HeightElementsNumber)
            {
                if (_neighborhoodsPositions[6]) neighborhoods[6] = temporaryBoard[x, y + 1];
            }
            else
            {
                if (PeriodicValues)
                {
                    neighborhoods[6] = temporaryBoard[x, y - HeightElementsNumber + 1];
                }
                else
                {
                    neighborhoods[6] = null;
                }
            }

            if (x + 1 < WidthElementsNumber && y + 1 < HeightElementsNumber)
            {
                if (_neighborhoodsPositions[7]) neighborhoods[7] = temporaryBoard[x + 1, y + 1];
            }
            else
            {
                if (PeriodicValues)
                {
                    if (x == WidthElementsNumber - 1 && y == HeightElementsNumber - 1)
                    {
                        neighborhoods[7] = temporaryBoard[0, 0];
                    }
                    else
                    {
                        if (x == WidthElementsNumber - 1 && y != 0)
                        {
                            neighborhoods[7] = temporaryBoard[x + 1 - WidthElementsNumber, y + 1];
                        }

                        if (y == HeightElementsNumber - 1 && x != WidthElementsNumber - 1)
                        {
                            neighborhoods[5] = temporaryBoard[x + 1, y + 1 - HeightElementsNumber];
                        }
                    }
                }
                else
                {
                    neighborhoods[7] = null;
                }
            }

            var groupedByColors = neighborhoods
                .Where(e => e != null)
                .Where(e => e.GrainValue)
                .GroupBy(e => e.Color)
                .Select(e => new ColorCounter {Color = e.Key, Count = e.Count()})
                .OrderBy(e => e.Count)
                .ToList();
            if (groupedByColors.Count == 0) return;
            var max = groupedByColors.First();
            var toRandom = groupedByColors.Where(e => e.Count == max.Count).ToList();
            if (toRandom.Count == 1)
            {
                BoardValues[x, y].Color = toRandom.First().Color;
                BoardValues[x, y].MakeGrain();
                actualPixel.GrainValue = true;
                actualPixel.Color = toRandom.First().Color;
                Grained.Add(actualPixel);
                return;
            }

            var random = new Random();
            BoardValues[x, y].Color = toRandom[random.Next(0, toRandom.Count - 1)].Color;
            BoardValues[x, y].MakeGrain();
            actualPixel.GrainValue = true;
            actualPixel.Color = toRandom.First().Color;
            Grained.Add(actualPixel);
        }
    }

    public class ColorCounter
    {
        public Color Color { get; set; }
        public int Count { get; set; }
    }
}

