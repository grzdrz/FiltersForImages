using FiltersTEST.ImageData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiltersTEST.Filters
{
    public class MedianFilter : Filter, IFilterOptionControls
    {
        public override string FilterName => "MedianFilter";

        public override Dictionary<string, object> FilterOptions { get; set; }

        public MedianFilter()
        {
            FilterOptions = new Dictionary<string, object>();
            FilterOptions["WindowSize"] = 3d;
        }

        public override Bitmap ApplyFilter(Pixel[,] pixels)
        {
            //размер окна-оператора
            double windowSize = (double)FilterOptions["WindowSize"];

            Bitmap resultImage = new Bitmap(pixels.GetLength(0), pixels.GetLength(1));

            for (int i = 0; i < pixels.GetLength(0); i++)
                for (int j = 0; j < pixels.GetLength(1); j++)
                {
                    Pixel temp = GetMedianPixel(pixels, i, j, windowSize);
                    resultImage.SetPixel(temp.Position.X, temp.Position.Y, Color.FromArgb(temp.A, temp.R, temp.G, temp.B));
                }

            return resultImage;
        }

        //i, j - координаты пикселя в центре окна
        public Pixel GetMedianPixel(Pixel[,] pixels, int i, int j, double windowSize)
        {
            int halfWindowSize = (int)Math.Floor(windowSize / 2d);//половина окна(с округлением в меньшую сторону)
            List<Pixel> pixelWindowLined = new List<Pixel>();//линеаризованное окно пикселей

            //i, j - координаты центрального пикселя, вокруг которого выделяется окно пикселей указанного размера
            //x, y - координаты текущего пикселя в окне
            //dx, dy - приращения между центром окна и текущим пикселем в нем
            int dx = 0; int dy = 0;
            for (int x = i - halfWindowSize; x < i - halfWindowSize + windowSize; x++)
            {
                dx = i - x;
                for (int y = j - halfWindowSize; y < j - halfWindowSize + windowSize; y++)
                {
                    dy = j - y;
                    //если запрос на пиксель не выходит за границы массива пикселей изображения
                    if (!(i - dx < 0 || i - dx > pixels.GetLength(0) - 1 || j - dy < 0 || j - dy > pixels.GetLength(1) - 1))
                    {
                        pixelWindowLined.Add(pixels[x, y]);
                    }
                }
            }

            pixelWindowLined.Sort(new PixelComparer());
            if (pixelWindowLined.Count % 2 == 0)//четная ширина окна
            {
                var pixel1 = pixelWindowLined[pixelWindowLined.Count / 2 - 1];
                var pixel2 = pixelWindowLined[pixelWindowLined.Count / 2];
                byte middleR = (byte)((pixel1.R + pixel2.R) / 2);
                byte middleG = (byte)((pixel1.G + pixel2.G) / 2);
                byte middleB = (byte)((pixel1.B + pixel2.B) / 2);
                byte middleA = (byte)((pixel1.A + pixel2.A) / 2);
                return new Pixel(middleR, middleG, middleB, middleA, new FiltersTEST.ImageData.Point(i, j));
            }
            else//нечетная ширина окна
            {
                var temp = pixelWindowLined[(int)(Math.Floor((double)(pixelWindowLined.Count) / 2d))];
                return new Pixel(temp.R, temp.G, temp.B, temp.A, new FiltersTEST.ImageData.Point(i, j));
            }
        }

        public void CreateOptionControls(Form2 formOfOptions)
        {
            NumericUpDown numericUpDown1_WhitePixelsFraction = new NumericUpDown();
            numericUpDown1_WhitePixelsFraction.Value = Convert.ToDecimal(FilterOptions["WindowSize"]);
            numericUpDown1_WhitePixelsFraction.Maximum = 8;
            numericUpDown1_WhitePixelsFraction.Minimum = 1;
            numericUpDown1_WhitePixelsFraction.Name = "WindowSize";
            numericUpDown1_WhitePixelsFraction.Size = new Size(171, 20);////
            numericUpDown1_WhitePixelsFraction.Location = new System.Drawing.Point(123, 10);////

            Label label_WhitePixelsFraction = new Label();
            label_WhitePixelsFraction.Font = new Font("Microsoft Sans Serif", 8F);
            label_WhitePixelsFraction.Text = "Filter window size";
            label_WhitePixelsFraction.Size = new Size(100, 20);/////
            label_WhitePixelsFraction.Location = new System.Drawing.Point(5, 10);//////

            formOfOptions.Controls.Add(numericUpDown1_WhitePixelsFraction);
            formOfOptions.Controls.Add(label_WhitePixelsFraction);
        }
    }
}
