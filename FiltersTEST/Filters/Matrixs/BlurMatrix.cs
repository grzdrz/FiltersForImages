using FiltersTEST.Filters.Matrixs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersTEST.Filters.Matrixs
{
    public class BlurMatrix : IMatrix
    {
        public string MatrixName => "Blur";

        public double[,] Matrix => new double[,]
        {
            { 0, 0, 1, 0, 0 },
            { 0, 1, 1, 1, 0 },
            { 1, 1, 1, 1, 1 },
            { 0, 1, 1, 1, 0 },
            { 0, 0, 1, 0, 0 }
        };
    }
}
