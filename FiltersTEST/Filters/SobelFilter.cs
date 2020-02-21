using FiltersTEST.ImageData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersTEST.Filters
{
    public class SobelFilter : Filter
    {
        public override string FilterName { get { return "SobelFilter"; } }

        public override Dictionary<string, object> FilterOptions { get; set; }

        public override Bitmap ApplyFilter(Pixel[,] pixels)
        {
            //матрица-оператор
            double[,] sx = new double[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

            var width = pixels.GetLength(0);
            var height = pixels.GetLength(1);
            Bitmap result = new Bitmap(width, height);
            double[,] sy = new double[sx.GetLength(1), sx.GetLength(0)];

            //транспонирование матрицы-оператора
            for (int i = 0; i < sx.GetLength(0); i++)
                for (int j = 0; j < sx.GetLength(1); j++)
                    sy[j, i] = sx[i, j];

            if (pixels.Length == 1)
            {
                var gray = (int)(255 * Math.Round(0.299 * pixels[0, 0].R + 0.587 * pixels[0, 0].G + 0.114 * pixels[0, 0].B, 10) / 255);
                gray = Math.Min(gray, 255);
                gray = Math.Max(gray, 0);

                int temp = (int)Math.Sqrt(
                    (double)gray * sx[0, 0] * (double)gray * sx[0, 0] +
                    (double)gray * sx[0, 0] * (double)gray * sx[0, 0]);
                result.SetPixel(0, 0, Color.FromArgb(pixels[0, 0].A, temp, temp, temp)); 
                return result;
            }

            for (int x = sx.GetLength(0) / 2; x < width - sx.GetLength(0) / 2; x++)
                for (int y = sx.GetLength(1) / 2; y < height - sx.GetLength(1) / 2; y++)
                {
                    //кусок изображения вокруг текущего(x, y) пикселя, размером операторной матрицы
                    double[,] pixelsInOperatorXWindow = new double[sx.GetLength(0), sx.GetLength(1)];
                    double[,] pixelsInOperatorYWindow = new double[sy.GetLength(0), sy.GetLength(1)];
                    double resultNumber = 0;
                    double resultNumber1 = 0;
                    double resultNumber2 = 0;
                    for (int i = 0; i < sx.GetLength(0); i++)
                        for (int j = 0; j < sx.GetLength(1); j++)
                        {
                            pixelsInOperatorXWindow[i, j] = GetGrayColorTone(pixels[x - sx.GetLength(0) / 2 + i, y - sx.GetLength(1) / 2 + j]);
                            resultNumber1 += pixelsInOperatorXWindow[i, j] * sx[i, j];
                        }
                    for (int i = 0; i < sy.GetLength(0); i++)
                        for (int j = 0; j < sy.GetLength(1); j++)
                        {
                            pixelsInOperatorYWindow[i, j] = GetGrayColorTone(pixels[x - sy.GetLength(0) / 2 + i, y - sy.GetLength(1) / 2 + j]);
                            resultNumber2 += pixelsInOperatorYWindow[i, j] * sy[i, j];
                        }
                    resultNumber = FilterExtremeValues(Math.Sqrt(resultNumber1 * resultNumber1 + resultNumber2 * resultNumber2));
                    result.SetPixel(x, y, Color.FromArgb(pixels[x, y].A, (int)resultNumber, (int)resultNumber, (int)resultNumber));
                }
            return result;
        }

        public double GetGrayColorTone(Pixel pixel)
        {
            var gray = (int)(255d * Math.Round(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B, 10) / 255d);
            gray = Math.Min(gray, 255);
            gray = Math.Max(gray, 0);
            return gray;
        }

        public double FilterExtremeValues(double pixel)
        {
            var gray = Math.Min(pixel, 255);
            gray = Math.Max(gray, 0);
            return gray;
        }
    }
}
