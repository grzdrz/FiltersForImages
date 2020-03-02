using FiltersTEST.Filters.Matrixs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersTEST.Filters.Matrixs
{
    public class EmbossMatrix : IMatrix
    {
        string IMatrix.MatrixName => "Emboss";

        double IMatrix.DefaultDiv => 10d;

        double IMatrix.Offset => 0d;

        double[,] IMatrix.Matrix => new double[,]
        {
            { -2, -1, 0 },
            { -1, 1, 1 },
            { 0, 1, 2 }
        };
    }
}
