using FiltersTEST.Filters.Matrixs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersTEST.Filters.Matrixs
{
    public class TESTMatrix : IMatrix
    {
        public string MatrixName => "TEST";

        //public double[,] Matrix => new double[,]//размытие
        //{
        //    { 0.5, 0.75, 0.5},
        //    { 0.75, 1, 0.75 },
        //    { 0.5, 0.75, 0.5}
        //};
        public double[,] Matrix => new double[,]//негатив
      {
            { 0, 0, 0 },
            { 0, -1, 0 },
            { 0, 0, 0 }
      };
    }
}
