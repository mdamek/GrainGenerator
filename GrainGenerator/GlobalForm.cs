using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
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
        private Bitmap bitmap;

        public GlobalForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var width = int.Parse(WidthInput.Text);
            var height = int.Parse(HeightInput.Text);
            var randomElementsNumber = int.Parse(RandomElementsNumberInput.Text);
            var interval = int.Parse(IntervalInput.Text);
            var gridValidator = new GridValidator();
            if (!gridValidator.Validate(width, height, randomElementsNumber, interval)) return;
            PauseButton.Enabled = true;
            Grid = null;
            TimesList.Items.Clear();
            Timer?.Dispose();
            GlobalWidth = width;
            GlobalHeight = height;
            Grid = new Grid(width, height, randomElementsNumber, false);
            PauseButton.Text = "Pause";
            Timer = new Timer {Interval = interval };
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
            bitmap = new Bitmap(_maxWidth, _maxWidth / GlobalWidth * GlobalHeight);
            boardPictureBox.Width = bitmap.Width;
            boardPictureBox.Height = bitmap.Height;
            ActualView = Grid.Grained.ToList();
            Draw(ActualView, GlobalWidth, GlobalHeight);
            Grid.MakeNewGeneration(TimesList);
        }

        private void Draw(List<GamePixelWithCoordinates> elementsToDraw, int width, int height)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            if (width >= height)
            {
                var elementSize = _maxWidth / width;
                for (var i = 0; i < elementsToDraw.Count; i++)
                {
                    var actual = elementsToDraw[i];
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        using (var brush = new SolidBrush(actual.Color))
                        {
                            graphics.FillRectangle(brush, new Rectangle(actual.X * elementSize, actual.Y * elementSize, elementSize, elementSize));
                        }
                    }
                }
                boardPictureBox.Image = bitmap;
            }
            else
            {
                var elementSize = _maxHeight / height;
                var bitmap = new Bitmap(_maxHeight, elementSize * width);
                boardPictureBox.Width = bitmap.Width;
                boardPictureBox.Height = bitmap.Height;
                foreach (var gamePixel in elementsToDraw)
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        using (var brush = new SolidBrush(gamePixel.Color))
                        {
                            graphics.FillRectangle(brush, new Rectangle(gamePixel.X, gamePixel.Y, elementSize, elementSize));
                        }
                    }
                }
                boardPictureBox.Image = null;
                boardPictureBox.Image = bitmap;
            }
            stopWatch.Stop();
            var row = new ListViewItem(new[]{ "Draw", stopWatch.ElapsedMilliseconds.ToString() } );
            TimesList.Items.Insert(0, row);
            if (ActualView.Count != Grid.HeightElementsNumber * Grid.WidthElementsNumber) return;
            Timer.Stop();
            MessageBox.Show("All grains are ready", "Information", MessageBoxButtons.OK);
        }

       

        private void boardPictureBox_Click(object sender, EventArgs e)
        {
            //var me = (MouseEventArgs)e;
            //var x = me.X;
            //var y = me.Y;
            //var screen = Screen.FromControl(this);
            //var workingArea = screen.WorkingArea;

            //var workingWidth = workingArea.Height - 40;
            //var workingHeight = workingWidth;

            //var elementWidth = workingWidth / GlobalWidth;
            //var elementHeight = workingHeight / GlobalHeight;

            //var finalX = x / elementWidth;
            //var finalY = y / elementHeight;

            //var pixel = Grid.ChangeOnePixelColor(finalX, finalY);
            //if (pixel.IsGrain())
            //{
            //    ActualView.Add(new GamePixel(finalX, finalY, 1, true, Color.AliceBlue));
            //}
            //else
            //{
            //    var element = ActualView.First(u => u.X == pixel.X && u.Y == pixel.Y);
            //    ActualView.Remove(element);
            //}

            //Draw(ActualView, GlobalWidth, GlobalHeight);
        }
    }
}
