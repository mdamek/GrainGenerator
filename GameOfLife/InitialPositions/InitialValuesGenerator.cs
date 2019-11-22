using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace GameOfLife.InitialPositions
{
    public static class InitialValuesGenerator
    {
        public static GamePixel[,] RandomValues(int randomElementsNumber,
            int widthElementsNumber, int heightElementsNumber)
        {
            var boardValues = new GamePixel[widthElementsNumber, heightElementsNumber];
            var randomValues = PointsPropertiesGenerator.GenerateRandom(randomElementsNumber, 0, boardValues.Length);
            var randomColorsToUse =
                PointsPropertiesGenerator.GenerateRandom(randomElementsNumber, 0, 16777216)
                    .ConvertAll(Color.FromArgb);
            for (var i = 0; i < randomColorsToUse.Capacity; i++)
            {
                randomColorsToUse[i] = Color.FromArgb(255, randomColorsToUse[i]);
            }
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
    }
}
