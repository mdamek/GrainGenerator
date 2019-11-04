using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class GlobalForm : Form
    {
        public GlobalForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var width = int.Parse(WidthInput.Text);
            var height = int.Parse(HeightInput.Text);
            var randomElementsNumber = int.Parse(RandomElementsNumberInput.Text);
            var gridValidator = new GridValidator();
            if (!gridValidator.Validate(width, height, randomElementsNumber)) return;
            StartButton.Enabled = false;
            PauseButton.Enabled = true;
            var grid = new Grid(width, height, randomElementsNumber);
            Draw(grid.GetUsedPixels(), width, height);
            grid.MakeNewGeneration(new ConwayRules());
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            StartButton.Enabled = true;
            PauseButton.Enabled = false;
        }

        private void Draw(List<GamePixel> elementsToDraw, int width, int height)
        {
            var screen = Screen.FromControl(this);
            var workingArea = screen.WorkingArea;

            var workingWidth = workingArea.Height - 40;
            var workingHeight = workingWidth;

            var elementWidth = workingWidth / width;
            var elementHeight = workingHeight / height;

            var bitmap = new Bitmap(workingWidth, workingHeight);
            boardPictureBox.Width = workingWidth;
            boardPictureBox.Height = workingHeight;

            foreach (var gamePixel in elementsToDraw)
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    if (gamePixel.IsAlive())
                    {
                        graphics.FillRectangle(Brushes.Black, new Rectangle(gamePixel.X * elementWidth, gamePixel.Y * elementHeight, elementWidth, elementHeight));
                    }
                }
            }          
            boardPictureBox.Image = bitmap;
        }
    }
}
