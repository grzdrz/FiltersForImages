using FiltersTEST.ImageData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersTEST.Filters
{
    public class GrayShadesFilter : Filter
    {
        public override Bitmap ApplyFilter(Pixel[,] pixels)
        {
            Bitmap newImage = new Bitmap(pixels.GetLength(0), pixels.GetLength(1));

            for (int i = 0; i < pixels.GetLength(0); i++)
                for (int j = 0; j < pixels.GetLength(1); j++)
                {
                    var gray = (int)(255 * Math.Round(0.299 * pixels[i, j].R + 0.587 * pixels[i, j].G + 0.114 * pixels[i, j].B, 10) / 255);
                    gray = Math.Min(gray, 255);
                    gray = Math.Max(gray, 0);
                    newImage.SetPixel(i, j, Color.FromArgb(pixels[i, j].A, gray, gray, gray));
                }
            return newImage;
        }

        public override string FilterName
        {
            get { return "Gray shades filter"; }
        }
    }
}
