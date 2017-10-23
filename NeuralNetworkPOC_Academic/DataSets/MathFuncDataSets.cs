using MathNet.Numerics.LinearAlgebra;
using NeuralNetworkPOC.Helper;
using System;
using System.Collections.Generic;

namespace NeuralNetworkPOC
{
    public class MathFuncDataSets:BaseDataSets
    {
        private const int defaultTrainingSetSize = 500;
        private const int defaultTestingSetSize = 100;
        
        public MathFuncDataSets()
        {
            InputNeuronNames = MathFunc.GetInputVariablesList();
            OutputNeuronNames = MathFunc.GetOutputVariablesList();
        }
        
        protected override DataSet[] GetDataSet(int samples)
        {
            //Source: Mathematical Function - Artificial Set
            List<MathFunc> dataSet = new List<MathFunc>();

            while (samples > 0)
            {
                dataSet.Add(new MathFunc(randomGen.NextDouble(), randomGen.NextDouble(), randomGen.NextDouble()));
                samples--;
            }

            return dataSet.ToArray(); ;
        }

        public override String ProcessTestSetResults(Matrix<double> testSetInput, Matrix<double> testSetOutput, Matrix<double> testSetPredictedOutcome)
        {
            double accError = 0;
            int failures = 0;
            String txt = "Testing Set Results\r\n\r\n";

            for (int i = 0; i < testSetPredictedOutcome.RowCount; i++)
            {
                var realResult = testSetOutput[i, 0];
                var estimatedResult = testSetPredictedOutcome[i, 0];

                if (UseNormalizedOutput)
                {
                    estimatedResult = DataProcessing.Denormalize(estimatedResult, maxOutputs[0], minOutputs[0]);
                    realResult = DataProcessing.Denormalize(realResult, maxOutputs[0], minOutputs[0]);
                }

                double error = realResult - estimatedResult;
                string result = String.Empty;

                if (Math.Abs(error) > defaultErrorThreshold)
                {
                    failures++;
                    result = "[ERROR]";
                }

                if(testSetInput != null)
                    result += "Input[X: " + testSetInput[i, 0] + ", Y:" + testSetInput[i, 1] + testSetInput[i, 2] + "] > ";

                result += "Real Output:" + realResult + " Estimation: " + estimatedResult + "[Error:" + Math.Round(error, 5) + "]\r\n";
                txt += result;
                accError += error;
            }

            txt += "\r\n\r\nAverage Estimation Error: " + (accError / testSetPredictedOutcome.RowCount).ToString();
            txt += "\r\nTotal Errors: " + failures + " out of " + testSetPredictedOutcome.RowCount + " samples [" + Math.Round((double)(100 * failures / testSetPredictedOutcome.RowCount), 2) + "%]";

            return txt;
        }

    }
}
