using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GameOfLife.InitialPositions;
using Timer = System.Windows.Forms.Timer;

namespace GameOfLife
{
    public partial class GlobalForm : Form
    {
        private Timer Timer { get; set; }
        private Grid Grid { get; set; }
        private int GlobalWidth { get; set; }
        private int GlobalHeight { get; set; }
        private List<GamePixelWithCoordinates> ActualView { get; set; }
        private int _maxHeight = 1000;
        private int _maxWidth = 1000;
        private Bitmap _bitmap;
        private int _grainedElementsNumber;
        private int _elementSize;
        private List<Tuple<int, int, Color>> ToDrawValues { get; set; }

        public GlobalForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var width = int.Parse(WidthInput.Text);
            var height = int.Parse(HeightInput.Text);
            var randomElementsNumber = 0;
            if (InitialSetting.SelectedItem.ToString() == "Random" ||
                InitialSetting.SelectedItem.ToString() == "Evenly" ||
                InitialSetting.SelectedItem.ToString() == "Circle area")
            {
                randomElementsNumber = int.Parse(RandomElementsNumberInput.Text);
            }

            var interval = int.Parse(IntervalInput.Text);
            var gridValidator = new GridValidator();
            if (!gridValidator.Validate(width, height, randomElementsNumber, interval)) return;
            PauseButton.Enabled = true;
            Grid = null;
            Timer?.Dispose();
            GlobalWidth = width;
            GlobalHeight = height;
            var selectedStartType = InitialSetting.SelectedItem.ToString();
            var selectedNeighborhood = NeighborhoodType.SelectedItem.ToString();
            var periodicValues = checkBoxPeriodical.Checked;
            Grid = new Grid(width, height, randomElementsNumber, periodicValues, selectedStartType,
                selectedNeighborhood, ToDrawValues);
            _bitmap = new Bitmap(_maxWidth, _maxWidth / GlobalWidth * GlobalHeight);
            boardPictureBox.Width = _bitmap.Width;
            boardPictureBox.Height = _bitmap.Height;
            _grainedElementsNumber = Grid.Grained.Count;
            PauseButton.Text = "Pause";
            Timer = new Timer {Interval = interval};
            Timer.Tick += (localSender, locale) => Simulation();
            Timer.Start();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            var pause = PauseButton.Text;
            switch (pause)
            {
                case "Pause":
                    PauseButton.Text = "Continue";
                    Timer.Stop();
                    break;
                case "Continue":
                    PauseButton.Text = "Pause";
                    Timer.Start();
                    break;
                default:
                    throw new ArgumentException("There are only two states");
            }
        }

        private void Simulation()
        {
            ActualView = Grid.Grained.ToList();
            Draw(ActualView, GlobalWidth, GlobalHeight);
            Grid.MakeNewGeneration();
            _grainedElementsNumber = _grainedElementsNumber + Grid.Grained.Count;
        }

        private void Draw(List<GamePixelWithCoordinates> elementsToDraw, int width, int height)
        {
            if (elementsToDraw == null)
            {
                _bitmap = new Bitmap(_maxWidth, _maxWidth / width * height);
                if (width >= height)
                {
                    _elementSize = _maxWidth / width;
                }
                else
                {
                    _elementSize = _maxHeight / height;
                }

                using (var graphics = Graphics.FromImage(_bitmap))
                {
                    using (var brush = new SolidBrush(DefaultBackColor))
                    {
                        graphics.FillRectangle(brush, new Rectangle(0, 0, _maxWidth, _maxWidth / width * height));
                    }
                }

                boardPictureBox.Image = _bitmap;
                return;
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            if (width >= height)
            {
                _elementSize = _maxWidth / width;
                foreach (var actual in elementsToDraw)
                {
                    using (var graphics = Graphics.FromImage(_bitmap))
                    {
                        using (var brush = new SolidBrush(actual.Color))
                        {
                            graphics.FillRectangle(brush,
                                new Rectangle(actual.X * _elementSize, actual.Y * _elementSize, _elementSize,
                                    _elementSize));
                        }
                    }
                }

                boardPictureBox.Image = _bitmap;
            }
            else
            {
                _elementSize = _maxHeight / height;
                foreach (var gamePixel in elementsToDraw)
                {
                    using (var graphics = Graphics.FromImage(_bitmap))
                    {
                        using (var brush = new SolidBrush(gamePixel.Color))
                        {
                            graphics.FillRectangle(brush,
                                new Rectangle(gamePixel.X, gamePixel.Y, _elementSize, _elementSize));
                        }
                    }
                }

                boardPictureBox.Image = _bitmap;
            }

            stopWatch.Stop();
            if (_grainedElementsNumber != Grid.HeightElementsNumber * Grid.WidthElementsNumber) return;
            Timer.Stop();
            MessageBox.Show("All grains are ready", "Ready!", MessageBoxButtons.OK);
        }

        private void DrawOnePixel(int x, int y, Color color)
        {
            using (var graphics = Graphics.FromImage(_bitmap))
            {
                using (var brush = new SolidBrush(color))
                {
                    graphics.FillRectangle(brush,
                        new Rectangle(x * _elementSize, y * _elementSize, _elementSize, _elementSize));
                }
            }

            boardPictureBox.Image = _bitmap;
        }

        private void boardPictureBox_Click(object sender, EventArgs e)
        {
            if(_elementSize == 0) return;
            var me = (MouseEventArgs) e;
            var x = me.X;
            var y = me.Y;
            var xx = x / _elementSize;
            var yy = y / _elementSize;
            if (Timer == null)
            {
                var color = InitialValuesGenerator.RandomColor();
                ToDrawValues.Add(new Tuple<int, int, Color>(xx, yy, color));
                DrawOnePixel(xx, yy, color);
            }
            else
            {
                var added = Grid.AddValue(xx, yy);
                _grainedElementsNumber = _grainedElementsNumber + added;
                var pixel = Grid.BoardValues[xx, yy];
                DrawOnePixel(xx, yy, pixel.Color);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (InitialSetting.SelectedItem.ToString() != "Clicks")
            {
                MessageBox.Show("Works only in Clicks mode", "Warning", MessageBoxButtons.OK);
                return;
            }

            if (WidthInput.Text == "" || HeightInput.Text == "")
            {
                MessageBox.Show("You need to fill width and height", "Warning", MessageBoxButtons.OK);
                return;
            }

            ToDrawValues = new List<Tuple<int, int, Color>>();
            var width = int.Parse(WidthInput.Text);
            var height = int.Parse(HeightInput.Text);
            Draw(null, width, height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ToDrawValues == null) return;
            ToDrawValues.Clear();
            
            var width = int.Parse(WidthInput.Text);
            var height = int.Parse(HeightInput.Text);
            Grid.BoardValues = null;
            Grid.BoardValues = InitialValuesGenerator.InitializeEmpty(width, height);
            Draw(null, width, height);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (newRandomInput.Text == "")
            {
                MessageBox.Show("You should write number of random elements", "Error", MessageBoxButtons.OK);
                return;
            }
            var randomElementsNumber = int.Parse(newRandomInput.Text);
            var pixels = new List<GamePixelWithCoordinates>();
            var random = new Random(Guid.NewGuid().GetHashCode());
            for (var i = 0; i < Grid.WidthElementsNumber; i++)
            {
                for (var j = 0; j < Grid.HeightElementsNumber; j++)
                {
                    if (!Grid.BoardValues[i, j].IsGrain()) continue;
                    var actualItem = Grid.BoardValues[i, j];
                    pixels.Add(new GamePixelWithCoordinates
                    {
                        Id = actualItem.Id,
                        Color = actualItem.Color,
                        GrainValue = actualItem.GrainValue,
                        X = i,
                        Y = j
                    });
                }
            }
            for (var i = 0; i < randomElementsNumber; i++)
            {
                var ifContain = false;
                while (ifContain == false)
                {
                    var randX = random.Next(Grid.WidthElementsNumber);
                    var randY = random.Next(Grid.HeightElementsNumber);
                    var decision = pixels.Any(a => a.X == randX && a.Y == randY);
                    if (decision) continue;
                    var answer = Grid.AddValue(randX, randY, false);
                    if (answer != 1) continue;
                    DrawOnePixel(randX, randY, Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
                    ifContain = true;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
