using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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
        private List<Tuple<int, int>> ToDrawValues { get; set; }

        public GlobalForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var width = int.Parse(WidthInput.Text);
            var height = int.Parse(HeightInput.Text);
            var randomElementsNumber = 0;
            if (InitialSetting.SelectedItem.ToString() == "Random")
            {
                randomElementsNumber = int.Parse(RandomElementsNumberInput.Text);
            }
            
            var interval = int.Parse(IntervalInput.Text);
            var gridValidator = new GridValidator();
            if (!gridValidator.Validate(width, height, randomElementsNumber, interval)) return;
            PauseButton.Enabled = true;
            Grid = null;
            TimesList.Items.Clear();
            Timer?.Dispose();
            GlobalWidth = width;
            GlobalHeight = height;
            var selectedStartType = InitialSetting.SelectedItem.ToString();
            var selectedNeighborhood = NeighborhoodType.SelectedItem.ToString();
            var periodicValues = checkBoxPeriodical.Checked;
            Grid = new Grid(width, height, randomElementsNumber, periodicValues, selectedStartType,
                selectedNeighborhood, ToDrawValues);
            _bitmap = new Bitmap(_maxWidth, _maxWidth / GlobalWidth * GlobalHeight);
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
            boardPictureBox.Width = _bitmap.Width;
            boardPictureBox.Height = _bitmap.Height;
            ActualView = Grid.Grained.ToList();
            Draw(ActualView, GlobalWidth, GlobalHeight);
            Grid.MakeNewGeneration(TimesList);
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
                boardPictureBox.BorderStyle = BorderStyle.FixedSingle;
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
            var row = new ListViewItem(new[] {"Draw", stopWatch.ElapsedMilliseconds.ToString()});
            TimesList.Items.Insert(0, row);
            if (_grainedElementsNumber != Grid.HeightElementsNumber * Grid.WidthElementsNumber) return;
            Timer.Stop();
            MessageBox.Show("All grains are ready", "Ready!", MessageBoxButtons.OK);
        }

        private void boardPictureBox_Click(object sender, EventArgs e)
        {
            var me = (MouseEventArgs) e;
            var x = me.X;
            var y = me.Y;
            var xx = x / _elementSize;
            var yy = y / _elementSize;

            if (Timer == null || Timer.Enabled == false)
            {
                ToDrawValues.Add(new Tuple<int, int>(xx, yy));
            }
            else
            {
                var added = Grid.AddValue(xx, yy);
                _grainedElementsNumber = _grainedElementsNumber + added;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (InitialSetting.SelectedItem.ToString() != "Clicks" || WidthInput.Text == "" ||
                HeightInput.Text == "") return;
            ToDrawValues = new List<Tuple<int, int>>();
            var width = int.Parse(WidthInput.Text);
            var height = int.Parse(HeightInput.Text);
            Draw(null, width, height);
        }
    }
}
