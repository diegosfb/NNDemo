using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace NeuralNetworkPOC
{
    public static class MathNetMatrixExtensions
    {
        public static Matrix<double> CreateMatrixFromList(List<double> values)
        {
            Matrix<double> matrix = Matrix<double>.Build.Dense(values.Count, 1);
            matrix.SetColumn(0, values.ToArray());
            return matrix;
        }

        public static void ExchangeRows(this Matrix<double> matrix,int from, int to)
        {
            if (from >= matrix.RowCount || to >= matrix.RowCount)
                throw new Exception("Exchange Rows row index out of bounds!");

            var tmp = matrix.Row(to);
            matrix.SetRow(to, matrix.Row(from));
            matrix.SetRow(from, tmp);
        }

    }
}
