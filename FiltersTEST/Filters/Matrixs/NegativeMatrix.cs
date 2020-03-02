using FiltersTEST.Filters.Matrixs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersTEST.Filters.Matrixs
{
    public class NegativeMatrix : IMatrix
    {
        public string MatrixName => "Negative";

        public double DefaultDiv => 10d;

        public double Offset => 256d;

        public double[,] Matrix => new double[,]
      {
            { 0, 0, 0 },
            { 0, -1, 0 },
            { 0, 0, 0 }
      };
    }
}
