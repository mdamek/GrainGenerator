using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using GameOfLife.InitialPositions;
using GameOfLife.Neighborhoods;

namespace GameOfLife
{
    public class Grid
    {
        public int WidthElementsNumber { get; }
        public int HeightElementsNumber { get; }
        private GamePixel[,] BoardValues { get; set; }
        public ConcurrentBag<GamePixelWithCoordinates> Grained { get; set; }
        private bool PeriodicValues { get; }

        public Grid(int widthElementsNumber, int heightElementsNumber, int randomElementsNumber, bool periodicValues)
        {
            WidthElementsNumber = widthElementsNumber;
            HeightElementsNumber = heightElementsNumber;
            PeriodicValues = periodicValues;
            BoardValues = InitializeBoardValues(widthElementsNumber, heightElementsNumber, randomElementsNumber);
            GetGrainedAndNotPixels();
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

        private GamePixel[,] InitializeBoardValues(int widthElementsNumber, int heightElementsNumber, int randomElementsNumber)
        {
            var readyBoard = InitialValuesGenerator.RandomValues(randomElementsNumber, widthElementsNumber, heightElementsNumber);
            return readyBoard;
        }

        public void MakeNewGeneration(ListView listView)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var temporaryBoard = new GamePixel[WidthElementsNumber, HeightElementsNumber];
            for (var i = 0; i < WidthElementsNumber; i++)
            {
                for (var j = 0; j < HeightElementsNumber; j++)
                {
                    var actual = BoardValues[i, j];
                    temporaryBoard[i, j] = new GamePixel{Color = actual.Color, GrainValue = actual.GrainValue, Id = actual.Id};
                }
            }
            var toGrain = GetPixelsToGrain(temporaryBoard);
            Parallel.For(0, toGrain.Count,new ParallelOptions(){MaxDegreeOfParallelism = 8}, i =>
            {
                NeighborhoodsAction(temporaryBoard, toGrain[i]);
            });
            stopWatch.Stop();
            var row = new ListViewItem(new []{"Calculation", stopWatch.ElapsedMilliseconds.ToString() });
            listView.Items.Insert(0,row);
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
                    pixels.Add(new GamePixelWithCoordinates { Color = actual.Color, GrainValue = actual.GrainValue, Id = actual.Id, X = i, Y = j });
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
            var neighborhoodsPositions = NeighborhoodsCreator.Generate(NeighborhoodType.Moore);
            var neighborhoods = new GamePixel[8];
            if (PeriodicValues == false)
            {
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    if(neighborhoodsPositions[0]) neighborhoods[0] = temporaryBoard[x - 1, y - 1];
                }
                else
                {
                    neighborhoods[0] = null;
                }
                if (y - 1 >= 0)
                {
                    if (neighborhoodsPositions[1]) neighborhoods[1] = temporaryBoard[x, y - 1];
                }
                else
                {
                    neighborhoods[1] = null;
                }

                if (x + 1 < WidthElementsNumber && y - 1 >= 0)
                {
                    if (neighborhoodsPositions[2]) neighborhoods[2] = temporaryBoard[x + 1, y - 1];
                }
                else
                {
                    neighborhoods[2] = null;
                }

                if (x - 1 >= 0)
                {
                    if (neighborhoodsPositions[3]) neighborhoods[3] = temporaryBoard[x - 1, y];
                }
                else
                {
                    neighborhoods[3] = null;
                }

                if (x + 1 < WidthElementsNumber)
                {
                    if (neighborhoodsPositions[4]) neighborhoods[4] = temporaryBoard[x + 1, y];
                }
                else
                {
                    neighborhoods[4] = null;
                }

                if (x - 1 >= 0 && y + 1 < HeightElementsNumber)
                {
                    if (neighborhoodsPositions[5]) neighborhoods[5] = temporaryBoard[x - 1, y + 1];
                }
                else
                {
                    neighborhoods[5] = null;
                }

                if (y + 1 < HeightElementsNumber)
                {
                    if (neighborhoodsPositions[6]) neighborhoods[6] = temporaryBoard[x, y + 1];
                }
                else
                {
                    neighborhoods[6] = null;
                }

                if (x + 1 < WidthElementsNumber && y + 1 < HeightElementsNumber)
                {
                    if (neighborhoodsPositions[7]) neighborhoods[7] = temporaryBoard[x + 1, y + 1];
                }
                else
                {
                    neighborhoods[7] = null;
                }
                var groupedByColors = neighborhoods
                    .Where(e => e != null)
                    .Where(e => e.GrainValue)
                    .GroupBy(e => e.Color)
                    .Select(e => new ColorCounter { Color = e.Key, Count = e.Count()})
                    .OrderBy(e => e.Count)
                    .ToList();
                if(groupedByColors.Count == 0) return;
                var max = groupedByColors.First();
                var toRandom = groupedByColors.Where(e => e.Count == max.Count).ToList();
                if (toRandom.Count == 1)
                {
                    BoardValues[x, y].Color = toRandom.First().Color;
                    BoardValues[x,y].MakeGrain();
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
}
