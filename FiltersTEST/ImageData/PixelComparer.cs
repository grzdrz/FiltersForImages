using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersTEST.ImageData
{
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

            if (temp1 > temp2) return 1;
            else if (temp1 < temp2) return -1;
            else return 0;
        }
    }
}
