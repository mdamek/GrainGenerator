using System;
using System.Drawing;

namespace GameOfLife
{
    public class GamePixel
    {
        private bool Value { get; set; }
        public int X { get; }
        public int Y { get; }
        public int Id { get; set; }
        public Color Color { get; set; }

        public GamePixel(int x, int y)
        {
            X = x;
            Y = y;
        }

        public GamePixel(int x, int y, int id, bool value, Color color)
        {
            Value = value;
            X = x;
            Y = y;
            Id = id;
            Color = color;

        }

        public bool IsAlive()
        {
            return Value;
        }

        public void Revive()
        {
            if (Value) throw new ArgumentException("Pixel already live");
            Value = true;
        }

        public void Kill()
        {
            if (!Value) throw new ArgumentException("Pixel is already dead");
            Value = false;
        }
    }
}
