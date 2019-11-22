using System;
using System.Drawing;

namespace GameOfLife
{
    public class GamePixel
    {
        public bool GrainValue { get; set; }
        public int Id { get; set; }
        public Color Color { get; set; }

        public bool IsGrain()
        {
            return GrainValue;
        }

        public void MakeGrain()
        {
            if (GrainValue) throw new ArgumentException("Pixel already grain");
            GrainValue = true;
        }
    }
}
