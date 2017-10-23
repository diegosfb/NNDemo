using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetworkPOC
{
    public class IrisFlower:DataSet
    {
        public enum IrisSpecies {Setosa, Virginica, Versicolor, Unknown };
        const double defaultCertaintyThreshold = 0.8;

        public IrisFlower (double sepalLength,double sepalWidth, double petalLength, double petalWidth, IrisSpecies species)
        {
            Inputs.Add(sepalLength);
            Inputs.Add(sepalWidth);
            Inputs.Add(petalLength);
            Inputs.Add(petalWidth);

            double[] speciesMappedValues = new double[3];//Setosa, Versicolor & Virginica
            speciesMappedValues[0] = Convert.ToDouble(species == IrisSpecies.Setosa);
            speciesMappedValues[1] = Convert.ToDouble(species == IrisSpecies.Versicolor);
            speciesMappedValues[2] = Convert.ToDouble(species == IrisSpecies.Virginica);

            Outputs.AddRange(speciesMappedValues);
        }

        public IrisSpecies MappedOutputToSpecie(double certaintyThreshold = defaultCertaintyThreshold)
        {
            return EstimateSpecie(Outputs.ToArray(), certaintyThreshold);
        }

        public static IrisSpecies EstimateSpecie(double[] typeProbabilities, double certaintyThreshold = defaultCertaintyThreshold)
        {
            IrisFlower.IrisSpecies result = IrisFlower.IrisSpecies.Unknown;

            if (typeProbabilities[0] > certaintyThreshold) //Assumed to be this type
            {
                if (result == IrisSpecies.Unknown)
                    result = IrisSpecies.Setosa;
                else
                    return IrisSpecies.Unknown; //It was classified already as another type. It can't define between types!
            }

            if (typeProbabilities[1] > certaintyThreshold) //Assumed to be this type
            {
                if (result == IrisSpecies.Unknown)
                    result = IrisSpecies.Versicolor;
                else
                    return IrisSpecies.Unknown; //It was classified already as another type. It can't define between types!
            }

            if (typeProbabilities[2] > certaintyThreshold) //Assumed to be this type
            {
                if (result == IrisSpecies.Unknown)
                    result = IrisSpecies.Virginica;
                else
                    return IrisSpecies.Unknown; //It was classified already as another type. It can't define between types!
            }

            return result;
        }

        public override string ToString()
        {
            return MappedOutputToSpecie().ToString() + "[SeptalLength:" + Inputs[0].ToString() + ", SeptalWidth:" + Inputs[1].ToString() + ", PetalLenght:" + Inputs[2].ToString() + ", PetalWidth:" + Inputs[3].ToString() + "]";  
        }

        public static bool IsUnknownType(IrisFlower.IrisSpecies type)
        {
            return (type == IrisFlower.IrisSpecies.Unknown);
        }

        public static List<string> GetInputVariablesList()
        {
            List<string>  inputNeuronNames = new List<string>();
            inputNeuronNames.Add("SL");
            inputNeuronNames.Add("SW");
            inputNeuronNames.Add("PL");
            inputNeuronNames.Add("PW");

            return inputNeuronNames;
        }

        public static List<string> GetOutputVariablesList()
        {
            List<string> outputNeuronNames = new List<string>();
            outputNeuronNames.Add("IsSetosa");
            outputNeuronNames.Add("IsVersicolor");
            outputNeuronNames.Add("IsVirginica");
            return outputNeuronNames;
        }

    }
}
