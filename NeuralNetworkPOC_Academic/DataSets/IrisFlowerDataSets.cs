using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkPOC
{
    public class IrisFlowerDataSets:BaseDataSets
    {
        new const double defaultErrorThreshold = 0.25; //Override BaseClass DefaultErrorThreshold

        public IrisFlowerDataSets()
        {
            InputNeuronNames = IrisFlower.GetInputVariablesList();
            OutputNeuronNames = IrisFlower.GetOutputVariablesList();
        }

        public void LoadHardcodedData(double percentageOfTestingData = defaultPercentageOfTestingData)
        {
            
            var data = GetDataSet();

            if (UseNormalizedOutput)
                NormalizeDataSetOutputs(data.ToArray());

            int testingSetSize = Convert.ToInt32((data.Count * percentageOfTestingData) / 100);
            CreateTrainingAndTestingDataSets(data.ToArray(), testingSetSize);
        }

        public Matrix<double> CreatePropertiesMatrixFromSet(List<IrisFlower> set)
        {
            int rows = set.Count;
            int columns = this.InputNeuronNames.Count;
            double[,] array = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    array[i, j] = set[i].Outputs[j];
            }

            return DenseMatrix.OfArray(array);
        }

        public Matrix<double> CreateOutputMatrixFromSet(List<IrisFlower> set)
        {
            int rows = set.Count;
            int columns = this.OutputNeuronNames.Count;
            double[,] array = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    array[i, j] = set[i].Outputs[j];
            }

            return DenseMatrix.OfArray(array);
        }

        private List<IrisFlower> GetDataSet()
        {
            //Source: https://en.wikipedia.org/wiki/Iris_flower_data_set
            List<IrisFlower> dataSet = new List<IrisFlower>();
            dataSet.Add(new IrisFlower(5.1F, 3.5F, 1.4F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.9F, 3.0F, 1.4F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(6.2F, 3.4F, 5.4F, 2.3F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(5.9F, 3.0F, 5.1F, 1.8F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.5F, 2.8F, 4.6F, 1.5F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.7F, 2.8F, 4.5F, 1.3F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.7F, 3.3F, 5.7F, 2.5F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.7F, 3.0F, 5.2F, 2.3F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(4.7F, 3.2F, 1.3F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(7.3F, 2.9F, 6.3F, 1.8F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.9F, 3.1F, 4.9F, 1.5F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.8F, 3.2F, 5.9F, 2.3F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.7F, 3.3F, 5.7F, 2.5F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.7F, 3.0F, 5.2F, 2.3F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(4.6F, 3.1F, 1.5F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.0F, 3.6F, 1.4F, 0.3F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(6.7F, 2.5F, 5.8F, 1.8F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(7.2F, 3.6F, 6.1F, 2.5F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(5.5F, 2.3F, 4.0F, 1.3F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.5F, 3.2F, 5.1F, 2.0F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(4.9F, 2.4F, 3.3F, 1.0F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.4F, 3.4F, 1.5F, 0.4F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.2F, 4.1F, 1.5F, 0.1F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.5F, 4.2F, 1.4F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.9F, 3.1F, 1.5F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.0F, 3.2F, 1.2F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.5F, 3.5F, 1.3F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.9F, 3.6F, 1.4F, 0.1F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.4F, 3.0F, 1.3F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.1F, 3.4F, 1.5F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(7.0F, 3.2F, 4.7F, 1.4F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.4F, 3.2F, 4.5F, 1.5F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.6F, 2.9F, 4.6F, 1.3F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.2F, 2.7F, 3.9F, 1.4F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.3F, 2.5F, 4.9F, 1.5F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.1F, 2.8F, 4.7F, 1.2F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.4F, 2.9F, 4.3F, 1.3F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.6F, 3.0F, 4.4F, 1.4F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.8F, 2.8F, 4.8F, 1.4F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.7F, 3.0F, 5.0F, 1.7F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.0F, 2.9F, 4.5F, 1.5F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.7F, 2.6F, 3.5F, 1.0F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.2F, 2.9F, 4.3F, 1.3F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.1F, 2.5F, 3.0F, 1.1F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.7F, 2.8F, 4.1F, 1.3F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.3F, 3.3F, 6.0F, 2.5F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(5.8F, 2.7F, 5.1F, 1.9F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(7.1F, 3.0F, 5.9F, 2.1F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.3F, 2.9F, 5.6F, 1.8F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.5F, 3.0F, 5.8F, 2.2F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(7.6F, 3.0F, 6.6F, 2.1F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(4.9F, 2.5F, 4.5F, 1.7F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.4F, 2.7F, 5.3F, 1.9F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.8F, 3.0F, 5.5F, 2.1F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(5.7F, 2.5F, 5.0F, 2.0F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(5.8F, 2.8F, 5.1F, 2.4F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.4F, 3.2F, 5.3F, 2.3F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.5F, 3.0F, 5.5F, 1.8F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(7.7F, 3.8F, 6.7F, 2.2F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.5F, 3.0F, 5.2F, 2.0F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(5.4F, 3.9F, 1.7F, 0.4F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.6F, 3.4F, 1.4F, 0.3F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.0F, 3.4F, 1.5F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.4F, 2.9F, 1.4F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.9F, 3.1F, 1.5F, 0.1F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.4F, 3.7F, 1.5F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.8F, 3.4F, 1.6F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.8F, 3.0F, 1.4F, 0.1F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.3F, 3.0F, 1.1F, 0.1F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.8F, 4.0F, 1.2F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.7F, 4.4F, 1.5F, 0.4F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.4F, 3.9F, 1.3F, 0.4F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.1F, 3.5F, 1.4F, 0.3F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.7F, 3.8F, 1.7F, 0.3F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.1F, 3.8F, 1.5F, 0.3F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.4F, 3.4F, 1.7F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.1F, 3.7F, 1.5F, 0.4F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.6F, 3.6F, 1.0F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.1F, 3.3F, 1.7F, 0.5F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.8F, 3.4F, 1.9F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.0F, 3.0F, 1.6F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.0F, 3.4F, 1.6F, 0.4F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.2F, 3.5F, 1.5F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.2F, 3.4F, 1.4F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.7F, 3.2F, 1.6F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.8F, 3.1F, 1.6F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.5F, 2.4F, 3.8F, 1.1F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.5F, 2.4F, 3.7F, 1.0F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.8F, 2.7F, 3.9F, 1.2F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.0F, 2.7F, 5.1F, 1.6F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.4F, 3.0F, 4.5F, 1.5F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.0F, 3.4F, 4.5F, 1.6F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.7F, 3.1F, 4.7F, 1.5F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.3F, 2.3F, 4.4F, 1.3F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.6F, 3.0F, 4.1F, 1.3F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.5F, 2.5F, 4.0F, 1.3F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.5F, 2.6F, 4.4F, 1.2F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.1F, 3.0F, 4.6F, 1.4F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.8F, 2.6F, 4.0F, 1.2F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.0F, 2.3F, 3.3F, 1.0F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.6F, 2.7F, 4.2F, 1.3F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.7F, 3.0F, 4.2F, 1.2F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.7F, 2.9F, 4.2F, 1.3F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(7.7F, 2.6F, 6.9F, 2.3F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.0F, 2.2F, 5.0F, 1.5F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.9F, 3.2F, 5.7F, 2.3F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(5.6F, 2.8F, 4.9F, 2.0F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(7.7F, 2.8F, 6.7F, 2.0F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.3F, 2.7F, 4.9F, 1.8F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.7F, 3.3F, 5.7F, 2.1F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.4F, 3.1F, 5.5F, 1.8F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.0F, 3.0F, 4.8F, 1.8F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.9F, 3.1F, 5.4F, 2.1F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.7F, 3.1F, 5.6F, 2.4F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.9F, 3.1F, 5.1F, 2.3F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(5.8F, 2.7F, 5.1F, 1.9F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(5.0F, 3.5F, 1.3F, 0.3F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.5F, 2.3F, 1.3F, 0.3F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.4F, 3.2F, 1.3F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.0F, 3.5F, 1.6F, 0.6F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.1F, 3.8F, 1.9F, 0.4F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.8F, 3.0F, 1.4F, 0.3F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.1F, 3.8F, 1.6F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(4.6F, 3.2F, 1.4F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.3F, 3.7F, 1.5F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.0F, 3.3F, 1.4F, 0.2F, IrisFlower.IrisSpecies.Setosa));
            dataSet.Add(new IrisFlower(5.9F, 3.0F, 4.2F, 1.5F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.0F, 2.2F, 4.0F, 1.0F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.1F, 2.9F, 4.7F, 1.4F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.6F, 2.9F, 3.6F, 1.3F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.7F, 3.1F, 4.4F, 1.4F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.6F, 3.0F, 4.5F, 1.5F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.0F, 2.0F, 3.5F, 1.0F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.8F, 2.7F, 4.1F, 1.0F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.2F, 2.2F, 4.5F, 1.5F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.6F, 2.5F, 3.9F, 1.1F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(5.9F, 3.2F, 4.8F, 1.8F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(6.1F, 2.8F, 4.0F, 1.3F, IrisFlower.IrisSpecies.Versicolor));
            dataSet.Add(new IrisFlower(7.2F, 3.2F, 6.0F, 1.8F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.2F, 2.8F, 4.8F, 1.8F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.1F, 3.0F, 4.9F, 1.8F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.4F, 2.8F, 5.6F, 2.1F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(7.2F, 3.0F, 5.8F, 1.6F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(7.4F, 2.8F, 6.1F, 1.9F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(7.9F, 3.8F, 6.4F, 2.0F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.4F, 2.8F, 5.6F, 2.2F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.3F, 2.8F, 5.1F, 1.5F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.1F, 2.6F, 5.6F, 1.4F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(7.7F, 3.0F, 6.1F, 2.3F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.3F, 3.4F, 5.6F, 2.4F, IrisFlower.IrisSpecies.Virginica));
            dataSet.Add(new IrisFlower(6.3F, 2.5F, 5.0F, 1.9F, IrisFlower.IrisSpecies.Virginica));
            return dataSet;
        }
        
        private static double EstimationItemError(Vector<double> typeProbabilities)
        {
            return 1.0 - typeProbabilities.Max();
        }

        //OVERRIDES DEFAULTA RESULT PROCESSING
        public override String ProcessTestSetResults(Matrix<double> testSetInput,Matrix<double> testSetOutput, Matrix<double> testSetPredictedOutcome)
        {
            double accError = 0;
            int failures = 0;
            int canNotDefine = 0;
            String txt = "Testing Set Results\r\n\r\n";
            
            for (int i = 0; i < testSetPredictedOutcome.RowCount; i++)
            {
                var realResult = IrisFlower.EstimateSpecie(testSetOutput.Row(i).ToArray());
                var estimatedResult = IrisFlower.EstimateSpecie(testSetPredictedOutcome.Row(i).ToArray());
                var error = EstimationItemError(testSetPredictedOutcome.Row(i));

                string result = String.Empty;

                if (estimatedResult == IrisFlower.IrisSpecies.Unknown)
                {
                    canNotDefine++;
                    result = "[UNDEFINED]";
                }
                else
                {
                    if (estimatedResult != realResult)
                    {
                        failures++;
                        result = "[ERROR]";
                    }
                }

                result += "Real Output was " + realResult.ToString() + " estimation was " + estimatedResult.ToString() + "[Error:" + Math.Round(error, 5) + "]\r\n";
                txt += result;
                accError += error;
            }

            txt += "\r\n\r\nAverage Estimation Error: " + (accError / testSetPredictedOutcome.RowCount).ToString();
            txt += "\r\nTotal Errors: " + failures + " out of " + testSetPredictedOutcome.RowCount + " samples [" + Math.Round((double)(100 * failures / testSetPredictedOutcome.RowCount), 2) + "%]";
            txt += "\r\nTotal Undefined: " + canNotDefine + " out of " + testSetPredictedOutcome.RowCount + " samples [" + Math.Round((double)(100 * canNotDefine / testSetPredictedOutcome.RowCount), 2) + "%]";

            return txt;
        }

        public String ProcessTestSetResults(Matrix<double> testSetOutput, Matrix<double> testSetPredictedOutcome)
        {
            return ProcessTestSetResults(null, testSetOutput, testSetPredictedOutcome);
        }

    }
}
