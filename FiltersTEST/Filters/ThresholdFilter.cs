using FiltersTEST.ImageData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersTEST.Filters
{
    public class ThresholdFilter : Filter
    {
        public override string FilterName => "Threshold filter";

        public double WhitePixelsFraction = 0.3d;/////////

        public override Bitmap ApplyFilter(Pixel[,] pixels)
        {
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
    }

    public class PixelComparer : IComparer<Pixel>
    {
        public int Compare(Pixel pixel1, Pixel pixel2)
        {
            var temp1 = (int)(255 * Math.Round(0.299 * pixel1.R + 0.587 * pixel1.G + 0.114 * pixel1.B, 10) / 255);
            temp1 = Math.Min(temp1, 255);
            temp1 = Math.Max(temp1, 0);

            var temp2 = (int)(255 * Math.Round(0.299 * pixel2.R + 0.587 * pixel2.G + 0.114 * pixel2.B, 10) / 255);
            temp2 = Math.Min(temp2, 255);
            temp2 = Math.Max(temp2, 0);

            if (temp1 > temp2)return 1;
            else if (temp1 < temp2) return -1;
            else return 0;
        }
    }
}
