using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using NeuralNetworkPOC.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeuralNetworkPOC
{
    public class BaseDataSets
    {
        public virtual List<DataSet> TrainingSet { get; private set; }
        public virtual List<DataSet> TestingSet { get; private set; }
        public List<string> InputNeuronNames { get; protected set; }
        public List<string> OutputNeuronNames { get; protected set; }
        
        //RANDOM SEEDS
        protected const int setSeed = 1;
        protected static Random randomGen = new Random(setSeed);


        //DEFAULT SETTINGS
        protected const double defaultErrorThreshold = 0.1;
        protected const double defaultPercentageOfTestingData = 20;
        private const int defaultDataSetSize = 600;

        //INPUT FILE LOADING
        const string IO_INPUT = "INPUT";
        const string IO_OUTPUT = "OUTPUT";
        const string IO_SETTYPE = "SETTYPE";
        const string TESTING_LABEL = "Testing";
        protected const string defaultInputFileName = "SamplesTemplate.csv";


        //NORMALIZATION 
        public bool UseNormalizedOutput { get; set; }
        protected double[] maxOutputs;
        protected double[] minOutputs;


        protected BaseDataSets(double percentageOfTestingData = defaultPercentageOfTestingData)
        {
            TestingSet = new List<DataSet>();
            TrainingSet = new List<DataSet>();
            UseNormalizedOutput = false;
        }

        public void LoadFileInputData(string inputFile, bool setsPreDefine = false, double percentageOfTestingData = defaultPercentageOfTestingData)
        {
            if(setsPreDefine)
                LoadPresetDataSet(inputFile, percentageOfTestingData);
            else
                LoadRandomDataSet(inputFile, percentageOfTestingData);

        }

        protected void LoadRandomDataSet(string inputFile, double percentageOfTestingData)
        {
            
            var lines = File.ReadAllLines(inputFile);
            string[] ioTypes = lines[0].Split(',');
            string[] ioNames = lines[1].Split(',');
            int index = 2;

            int testingSetSize = Convert.ToInt32(((lines.Count() - 2) * percentageOfTestingData) / 100);

            PopulateLabels(ioTypes, ioNames);
            TestingSet = new List<DataSet>();
            TrainingSet = new List<DataSet>();


            while (index < lines.Count())
            {
                string[] values = lines[index].Split(',');
                TrainingSet.Add(AddToSamplesSet(values, ioTypes));
                index++;
            }
            
            CreateTrainingAndTestingDataSets(TrainingSet.ToArray(), testingSetSize);

        }

        protected void LoadPresetDataSet(string inputFile, double percentageOfTestingData)
        {
            var lines = File.ReadAllLines(inputFile);
            string[] ioTypes = lines[0].Split(',');
            string[] ioNames = lines[1].Split(',');
            int index = 2;
            bool isTestingSample;

            int testingSetSize = Convert.ToInt32(((lines.Count() - 2) * percentageOfTestingData) / 100);

            PopulateLabels(ioTypes, ioNames);
            TestingSet = new List<DataSet>();
            TrainingSet = new List<DataSet>();


            while (index < lines.Count())
            {
                string[] values = lines[index].Split(',');
                var sample = AddToSamplesSet(values, ioTypes, out isTestingSample);

                if (isTestingSample)
                    TestingSet.Add(sample);
                else
                    TrainingSet.Add(sample);

                index++;
            }
        }

        public void LoadHardcodedData(double percentageOfTestingData = defaultPercentageOfTestingData, int dataSetSize = defaultDataSetSize)
        {
            var data = GetDataSet(dataSetSize);

            if (UseNormalizedOutput)
                NormalizeDataSetOutputs(data.ToArray());

            int testingSetSize = Convert.ToInt32((data.Length* percentageOfTestingData) / 100);
            CreateTrainingAndTestingDataSets(data.ToArray(), testingSetSize);
        }

        protected void CreateTrainingAndTestingDataSets(DataSet[] data, int testingSetSize)
        {
            TestingSet = new List<DataSet>();
            TrainingSet = data.ToList();

            while (testingSetSize > 0)
            {
                int item = randomGen.Next(0, TrainingSet.Count - 1);
                TestingSet.Add(data[item]);
                TrainingSet.RemoveAt(item);
                testingSetSize--;
            }
            
        }

        private DataSet AddToSamplesSet(string[] values, string[] types)
        {
            bool ignore;
            return AddToSamplesSet(values, types, out ignore);
        }

        private DataSet AddToSamplesSet(string[] values, string[] types, out bool isTestingSample)
        {
            List<string> inputs = new List<string>();
            List<string> outputs = new List<string>();
            DataSet sample = new DataSet();
            isTestingSample = false;

            for (int i = 0; i < values.Length; i++)
            {
                if (types[i] == IO_INPUT)
                    inputs.Add(values[i]);

                if (types[i] == IO_OUTPUT)
                    outputs.Add(values[i]);

                if (types[i] == IO_SETTYPE)
                    isTestingSample = (values[i] == TESTING_LABEL);

                //Non INPUTS or OUTPUTS are ignored 
            }

            sample.SetInputValues(inputs);
            sample.SetOutputValues(outputs);
            return sample;

        }

        public Matrix<double> CreatePropertiesMatrixFromSet(DataSet[] set)
        {
            int rows = set.Length;
            int columns = this.InputNeuronNames.Count;
            double[,] array = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    array[i, j] = set[i].Inputs[j];

            }

            return DenseMatrix.OfArray(array);
        }

        public Matrix<double> CreateOutputMatrixFromSet(DataSet[] set)
        {
            int rows = set.Length;
            int columns = this.OutputNeuronNames.Count;
            double[,] array = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    array[i, j] = set[i].Outputs[j];
            }

            return DenseMatrix.OfArray(array);
        }

        private void PopulateLabels(string[] ioTypes, string[] ioNames)
        {
            InputNeuronNames = new List<string>();
            OutputNeuronNames = new List<string>();

            for (int i = 0; i < ioNames.Count(); i++)
            {
                if (ioTypes[i] == IO_INPUT)
                    InputNeuronNames.Add(ioNames[i]);

                if (ioTypes[i] == IO_OUTPUT)
                    OutputNeuronNames.Add(ioNames[i]);

                //IGNORE COLS NOT MARKED AS INPUT or OUTPUT
            }
        }
  
        public void NormalizeDataSetOutputs(DataSet[] dataset, bool setSetMaxMin = true)
        {
            int outputsCount = dataset[0].Outputs.Count;
            
            if (setSetMaxMin)
            {
                maxOutputs = new double[OutputNeuronNames.Count];
                minOutputs = new double[OutputNeuronNames.Count];

                for (int i = 0; i < outputsCount; i++) //Initialize Max & Min Outputs array
                {
                    maxOutputs[i] = dataset[0].Outputs[i];
                    minOutputs[i] = dataset[0].Outputs[i];
                }

                foreach (var item in dataset)
                {
                    for (int i = 0; i < outputsCount; i++)
                    {
                        double outputVal = item.Outputs[i];

                        if (outputVal > maxOutputs[i])
                            maxOutputs[i] = outputVal;

                        if (outputVal < minOutputs[i])
                            minOutputs[i] = outputVal;

                    }
                }
            }

            foreach (var item in dataset)
            {
                for (int i = 0; i < outputsCount; i++)
                    item.Outputs[i] = DataProcessing.Normalize(item.Outputs[i], maxOutputs[i], minOutputs[i]);
            }
        }

        public void DeNormalizeDataSetOutputs(DataSet[] dataset)
        {
            int outputsCount = dataset[0].Outputs.Count;
            
            foreach (var item in dataset)
            {
                for (int i = 0; i < outputsCount; i++)
                    item.Outputs[i] = DataProcessing.Denormalize(item.Outputs[i], maxOutputs[i], minOutputs[i]);
            }
        }

        virtual public String ProcessTestSetResults(Matrix<double> testSetInput, Matrix<double> testSetOutput, Matrix<double> testSetPredictedOutcome)
        {
            return null;
        }

        virtual protected DataSet[] GetDataSet(int size)
        {
            return null;
        }

    }
}
