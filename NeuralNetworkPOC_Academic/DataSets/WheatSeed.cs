using System;
using System.Collections.Generic;

namespace NeuralNetworkPOC
{
    public class WheatSeed:DataSet
    {
        const double defaultCertaintyThreshold = 0.75;
        public enum WheatSeedSpecies { Unknown, Kama, Rosa, Canadian };
        
        public WheatSeed(double area, double perimeter, double compactness, double lengthOfKernel, double widthOfKernel, double asymetryCoefficient, double lengthOfKernelGroove, WheatSeedSpecies species)
        {
            Inputs.Add(area);
            Inputs.Add(perimeter);
            Inputs.Add(compactness);
            Inputs.Add(lengthOfKernel);
            Inputs.Add(widthOfKernel);
            Inputs.Add(asymetryCoefficient);
            Inputs.Add(lengthOfKernelGroove);

            double[] speciesMappedValues = new double[3];//Kama,Rosa & Canadian
            speciesMappedValues[0] = Convert.ToDouble(species == WheatSeedSpecies.Kama);
            speciesMappedValues[1] = Convert.ToDouble(species == WheatSeedSpecies.Rosa);
            speciesMappedValues[2] = Convert.ToDouble(species == WheatSeedSpecies.Canadian);

            Outputs.AddRange(speciesMappedValues);
        }

        public WheatSeedSpecies MappedOutputToSpecie(double certaintyThreshold = defaultCertaintyThreshold)
        {
            return EstimateSpecie(Outputs.ToArray(), certaintyThreshold);
        }

        public static WheatSeedSpecies EstimateSpecie(double[] typeProbabilities, double certaintyThreshold = defaultCertaintyThreshold)
        {
            WheatSeedSpecies result = WheatSeedSpecies.Unknown;

            if (typeProbabilities[0] > certaintyThreshold) //Assumed to be this type
            {
                if (result == WheatSeedSpecies.Unknown)
                    result = WheatSeedSpecies.Kama;
                else
                    return WheatSeedSpecies.Unknown; //It was classified already as another type. It can't define between types!
            }

            if (typeProbabilities[1] > certaintyThreshold) //Assumed to be this type
            {
                if (result == WheatSeedSpecies.Unknown)
                    result = WheatSeedSpecies.Rosa;
                else
                    return WheatSeedSpecies.Unknown; //It was classified already as another type. It can't define between types!
            }

            if (typeProbabilities[2] > certaintyThreshold) //Assumed to be this type
            {
                if (result == WheatSeedSpecies.Unknown)
                    result = WheatSeedSpecies.Canadian;
                else
                    return WheatSeedSpecies.Unknown; //It was classified already as another type. It can't define between types!
            }

            return result;
        }

        public override string ToString()
        {
            return MappedOutputToSpecie().ToString() + "[Area:" + Inputs[0] + ", Perimeter:" + Inputs[1] + ", Compactness:" + Inputs[2] + ", LengthOfKernel:" + Inputs[3] + ", WidthOfKernel:" + Inputs[4] + ", AsymetryCoefficient:" + Inputs[5] + ", LengthOfKernelGroove:" + Inputs[6] + "]";
        }

        public static List<string> GetOutputVariablesList()
        {
            List<string> outputNeuronNames = new List<string>();
            outputNeuronNames.Add("IsKama");
            outputNeuronNames.Add("IsRosa");
            outputNeuronNames.Add("IsCanadian");
            return outputNeuronNames;
        }

        public static List<string> GetInputVariablesList()
        {
            List<string> inputNeuronNames = new List<string>();
            inputNeuronNames.Add("Area");
            inputNeuronNames.Add("Per");
            inputNeuronNames.Add("Comp");
            inputNeuronNames.Add("LoK");
            inputNeuronNames.Add("WoK");
            inputNeuronNames.Add("AC");
            inputNeuronNames.Add("LoKG");
            return inputNeuronNames;
        }

        internal static bool IsUnknownType(WheatSeedSpecies type)
        {
            return (type == WheatSeed.WheatSeedSpecies.Unknown);
        }
    }
}
