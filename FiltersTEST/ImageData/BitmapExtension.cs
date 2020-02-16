using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersTEST.ImageData
{
    public static class BitmapExtension
    {
        public static Pixel[,] BitmapToPixelsArray(this Bitmap bitmap)
        {
            Pixel[,] pixelsArray = new Pixel[bitmap.Width, bitmap.Height];
            for (int i = 0; i < bitmap.Width; i++)
                for (int j = 0; j < bitmap.Height; j++)
                {
                    var temp = bitmap.GetPixel(i, j);
                    pixelsArray[i, j] = new Pixel(temp.R, temp.G, temp.B, temp.A, new Point(i, j));
                }

            return pixelsArray;
        }
    }
}
