using System.Drawing;

namespace GameOfLife
{
    public class GamePixelWithCoordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool GrainValue { get; set; }
        public int Id { get; set; }
        public Color Color { get; set; }
    }
}
