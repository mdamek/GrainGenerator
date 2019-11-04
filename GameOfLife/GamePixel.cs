using System;

namespace GameOfLife
{
    public class GamePixel
    {
        private bool Value { get; set; }

        public GamePixel()
        {
            Value = false;
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
