using System.Windows.Forms;

namespace GameOfLife
{
    public class GridValidator
    {
        public bool Validate(int width, int height, int randomElementsNumber)
        {
            if (width < 0)
            {
                MessageBox.Show("Width must be > 0", "Error");
                return false;
            }
            if (height < 0)
            {
                MessageBox.Show("Height must be > 0", "Error");
                return false;
            }
            if (randomElementsNumber < 0)
            {
                MessageBox.Show("Random elements number must be > 0", "Error");
                return false;
            }
            if (width * height < randomElementsNumber)
            {
                MessageBox.Show($"Random elements number must be < than total elements number: {width * height}", "Error");
                return false;
            }
            return true;
        }
    }
}
