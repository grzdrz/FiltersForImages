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
    public class ThresholdFilter : Filter, IFilterOptionControls
    {
        public override string FilterName 
        {
            get { return "Threshold filter"; } 
        }

        public override Dictionary<string, object> FilterOptions { get; set; }

        public ThresholdFilter()
        {
            //дефолтные параметры фильтра
            FilterOptions = new Dictionary<string, object>();
            FilterOptions["WhitePixelsFraction"] = 30d;
        }

        public override Bitmap ApplyFilter(Pixel[,] pixels)
        {
            //итоговая доля белых пикселей
            double WhitePixelsFraction = ((double)FilterOptions["WhitePixelsFraction"]) / 100d;

            //линеаризируем двум. массив пикселей в одномерный
            List<Pixel> pixelsLineArray = new List<Pixel>();
            for (int i = 0; i < pixels.GetLength(0); i++)
                for (int j = 0; j < pixels.GetLength(1); j++)
                    pixelsLineArray.Add(pixels[i, j]);

            //сортируем линеаризованный список пикселей
            pixelsLineArray.Sort(new PixelComparer());

            Bitmap resultImage = new Bitmap(pixels.GetLength(0), pixels.GetLength(1));

            //численность белых пикселей = общая численность пикселей * доля белых пикселей
            int CountOfWhitePixels = (int)(WhitePixelsFraction * pixels.Length);

            int counter = 0;
            foreach (var pixel in pixelsLineArray)
            {
                if (counter < CountOfWhitePixels)
                    resultImage.SetPixel(pixel.Position.X, pixel.Position.Y, Color.FromArgb(pixel.A, 0, 0, 0));
                else
                    resultImage.SetPixel(pixel.Position.X, pixel.Position.Y, Color.FromArgb(pixel.A, 255, 255, 255));
                counter++;
            }

            return resultImage;
        }

        public void CreateOptionControls(Form2 formOfOptions)
        {
            NumericUpDown numericUpDown1_WhitePixelsFraction = new NumericUpDown();
            numericUpDown1_WhitePixelsFraction.Value = Convert.ToDecimal(FilterOptions["WhitePixelsFraction"]);
            numericUpDown1_WhitePixelsFraction.Maximum = 100;
            numericUpDown1_WhitePixelsFraction.Minimum = 0;
            numericUpDown1_WhitePixelsFraction.Name = "WhitePixelsFraction";
            numericUpDown1_WhitePixelsFraction.Size = new Size(171, 20);////
            numericUpDown1_WhitePixelsFraction.Location = new System.Drawing.Point(123, 10);////

            Label label_WhitePixelsFraction = new Label();
            label_WhitePixelsFraction.Font = new Font("Microsoft Sans Serif", 8F);
            label_WhitePixelsFraction.Text = "White pixels fraction";
            label_WhitePixelsFraction.Size = new Size(100, 20);/////
            label_WhitePixelsFraction.Location = new System.Drawing.Point(5, 10);//////

            formOfOptions.Controls.Add(numericUpDown1_WhitePixelsFraction);
            formOfOptions.Controls.Add(label_WhitePixelsFraction);
        }
    }
}
