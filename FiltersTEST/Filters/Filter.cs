using FiltersTEST.ImageData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersTEST.Filters
{
    public abstract class Filter
    {
        public abstract Bitmap ApplyFilter(Pixel[,] pixels);

        public abstract string FilterName { get; }
    }
}
