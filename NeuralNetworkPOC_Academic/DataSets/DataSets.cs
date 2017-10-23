using MathNet.Numerics.LinearAlgebra;
using NeuralNetworkPOC.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkPOC
{
    public class DataSet
    {
        public List<double> Inputs { get; private set; }
        public List<double> Outputs { get; private set; }

        public DataSet()
        {
            Inputs = new List<double>();
            Outputs = new List<double>();

        }

        public void SetInputValues(List<string> values)
        {
            foreach (var val in values)
                Inputs.Add(Convert.ToDouble(val));
        }

        public void SetOutputValues(List<string> values)
        {
            foreach (var val in values)
                Outputs.Add(Convert.ToDouble(val));
        }

        public static string ClassifyOutput(Vector<double> outputNeurons, List<string> outputNeuronNames, DataSet caseSet)
        {
            String txt = "INPUT: " + caseSet.ToString() + "\r\n\r\n";

            for (int i = 0; i < outputNeurons.Count; i++)
                txt += Math.Round(outputNeurons[i], 2) * 100 + "% of being" + outputNeuronNames[i] + "\r\n";

            return txt;
        }

    }
}
