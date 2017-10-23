using MathNet.Numerics.LinearAlgebra;
using System;

namespace NeuralNetworkPOC
{
    public class ActivationFunctions
    {
        //Refer to https://en.wikipedia.org/wiki/Activation_function for various activation functions

        public static Matrix<double> Sigmoid(Matrix<double> matrix)
        {
            //Output: 1/(1 + e^-x) for every element of the input matrix.
            //Is run when data reaches a neuron. It maps values to probabilities! (0 to 1)

            foreach (var tuple in matrix.EnumerateIndexed())
                matrix.At(tuple.Item1, tuple.Item2, (double)(1.0 / (1.0 + Math.Exp(-tuple.Item3))));

            return matrix;
        }

        public static Matrix<double> SigmoidDerived(Matrix<double> matrix)
        {
            //Returns the value of the sigmoid function derivative f'(x) = f(x)(1 - f(x)), 
            //This gives us the slope!
            //Output: x(1 - x) for every element of the input matrix.

            foreach (var tuple in matrix.EnumerateIndexed())
                matrix.At(tuple.Item1, tuple.Item2, tuple.Item3 * (1.0 - tuple.Item3));

            return matrix;
        }

        public static Matrix<double> Identity(Matrix<double> matrix)
        {
            return matrix;
        }

        public static Matrix<double> IdentityDerived(Matrix<double> matrix)
        {
            return Matrix<double>.Build.Dense(matrix.RowCount, matrix.ColumnCount, 1.0);
        }

        public static Matrix<double> BentIdentity(Matrix<double> matrix)
        {
            //bent identity function
            //x / (2.0 * (Math.Sqrt(x * x + 1.0))) + 1.0;
            foreach (var tuple in matrix.EnumerateIndexed())
                matrix.At(tuple.Item1, tuple.Item2, (Math.Sqrt(tuple.Item3 * tuple.Item3 + 1) - 1.0) / 2.0 + tuple.Item3);

            return matrix;
        }

        public static Matrix<double> BentIdentityDerived(Matrix<double> matrix)
        {
            //derivative of the bent identity function
            //(Math.Sqrt(x * x + 1) - 1.0) / 2.0 + x;

            foreach (var tuple in matrix.EnumerateIndexed())
                matrix.At(tuple.Item1, tuple.Item2, tuple.Item3 / (2.0 * (Math.Sqrt(tuple.Item3 * tuple.Item3 + 1.0))) + 1.0);

            return matrix;
        }

        public static Matrix<double> ReLU(Matrix<double> matrix)
        {
            //ReLU function

            foreach (var tuple in matrix.EnumerateIndexed())
            {
                if (tuple.Item3 < 0)
                    matrix.At(tuple.Item1, tuple.Item2, 0.0);
                else
                    matrix.At(tuple.Item1, tuple.Item2, tuple.Item3);
            }

            return matrix;
        }

        public static Matrix<double> ReLUDerived(Matrix<double> matrix)
        {
            //derivative of ReLU function

            foreach (var tuple in matrix.EnumerateIndexed())
            {
                if (tuple.Item3 < 0)
                    matrix.At(tuple.Item1, tuple.Item2, 0.0);
                else
                    matrix.At(tuple.Item1, tuple.Item2, 1.0);
            }

            return matrix;
        }
    }
}
