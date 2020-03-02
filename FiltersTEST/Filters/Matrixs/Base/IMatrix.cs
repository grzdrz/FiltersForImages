using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersTEST.Filters.Matrixs.Base
{
    public interface IMatrix
    {
        string MatrixName { get; }

        double DefaultDiv { get; }

        double Offset { get; }

        double[,] Matrix { get; }
    }
}
