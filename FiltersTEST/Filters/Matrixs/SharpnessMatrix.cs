using FiltersTEST.Filters.Matrixs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersTEST.Filters.Matrixs
{
    public class SharpnessMatrix : IMatrix
    {
        public string MatrixName => "Sharpness";

        public double[,] Matrix => new double[,]
        {
            { -1, -1, -1 },
            { -1, 9, -1 },
            { -1, -1, -1 }
        };
    }
}
