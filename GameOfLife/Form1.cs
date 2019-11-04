using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        public Form1()
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
            Draw(grid);
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            StartButton.Enabled = true;
            PauseButton.Enabled = false;
        }

        private void Draw(Grid grid)
        {
            var screen = Screen.FromControl(this);
            var workingArea = screen.WorkingArea;


            var workingWidth = workingArea.Height;
            var workingHeight = workingWidth;


            var elementWidth = workingWidth / grid.WidthElementsNumber;
            var elementHeight = workingHeight / grid.HeightElementsNumber;

            var bitmap = new Bitmap(workingWidth, workingHeight);
            boardPictureBox.Width = workingWidth;
            boardPictureBox.Height = workingHeight;
            for (var i = 0; i < grid.WidthElementsNumber; i++)
            {
                for (var j = 0; j < grid.HeightElementsNumber; j++)
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        var actualPixel = grid.GetGamePixel(i, j);
                        if (actualPixel.IsAlive())
                        {
                            graphics.FillRectangle(Brushes.Black, new Rectangle(i * elementWidth, j * elementHeight,
                                elementWidth, elementHeight));
                        }
                    }
                }
            }
            boardPictureBox.Image = bitmap;
        }
    }
}
