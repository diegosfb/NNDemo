using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace NeuralNetworkPOC
{
	public class Layer
	{

		public string Name { get; private set; }
		Matrix<double> bias, synapse, neuronValues;
		List<String> neuronNames = new List<string>();
		int neuronCount,nextLayerNeuronCount;

		const int randomSeed = 1;
		private static int samplesSize = 0;
		static Random rGen = new Random(randomSeed);

		public delegate Matrix<double> ActivationFunction(Matrix<double> input);
		public ActivationFunction activationFunction { get; set; }
		public ActivationFunction activationFunctionDerived { get; set; }

		public int NeuronCount
		{
			get
			{
				return neuronCount;
			}
		}
		public int NextLayerNeuronCount
		{
			get
			{
				return nextLayerNeuronCount;
			}
		}
		public Matrix<double> Synapse
		{
			get
			{
				return synapse;
			}

			set
			{
				synapse = value;
			}
		}
		public Matrix<double> Bias
		{
			get
			{
				return bias;
			}

			set
			{
				bias = value;
			}
		}
		public static int SamplesSize
		{
			get
			{
				return samplesSize;
			}

			set
			{
				samplesSize = value;
			}
		}
		public Matrix<double> PreviousBiasUpdate { get; internal set; }
		public Matrix<double> PreviousSynapseUpdate { get; internal set; }

		public Matrix<double> NeuronValues
		{
			get
			{
				if(neuronValues == null)
					neuronValues = Matrix<double>.Build.Random(SamplesSize, NeuronCount, randomSeed);

				return neuronValues;
			}

			set
			{
				neuronValues = value;
			}
		}
		public List<string> NeuronNames
		{
			get
			{
				return neuronNames;
			}

			set
			{
				neuronNames = value;
			}
		}

		public Layer(int numOfNeurons, int outputs, string name = null)
		{                     
			activationFunction = ActivationFunctions.Sigmoid;
			activationFunctionDerived = ActivationFunctions.SigmoidDerived;

			neuronCount = numOfNeurons;
			nextLayerNeuronCount = outputs;
			
			bias = Matrix<double>.Build.Random(1, NeuronCount, randomSeed);

			if(NextLayerNeuronCount > 0)
				synapse = Matrix< double >.Build.Random(NeuronCount, NextLayerNeuronCount, randomSeed);
			
			Name = name;
		}

		
		public Matrix<double> ProcessInputs(Matrix<double> neuronValues, Matrix<double> nextLayerNeuronBias = null) //If nextLayerNeuonBias = null then it is not using bias!!
		{
			//Batch Processing
			if (Synapse == null)
				throw new Exception("No synapse matrix to process layer. Do not process the output layer!");

			if (neuronValues.ColumnCount != NeuronCount)
				throw new Exception("Incorrect number of neuron values as input for layer " + Name);

			//Matrix representation calculations for improved efficiency
			Matrix<double> weights;
			Matrix<double> input;

			if (nextLayerNeuronBias != null)
			{               
				weights = Synapse.Stack(nextLayerNeuronBias);//Add Row of biases
				input = neuronValues.Append(Matrix<double>.Build.Dense(neuronValues.RowCount, 1, 1));//Add Column of 1s
			}
			else
			{
				weights = Synapse;
				input = neuronValues;
			}

			var layerOutput = input * weights;
			Matrix<double> nextLayerNeuronValues = activationFunction(layerOutput);
			return nextLayerNeuronValues;
		}

		public void SetLayerNeuronNames(List<string> neuronNames)
		{
			if(neuronNames.Count == 0)
				return;

			if (NeuronCount != neuronNames.Count)
				throw new Exception("Miss match number of Neurons!");

			for (int i = 0; i < NeuronCount; i++)
				NeuronNames.Add(neuronNames[i]);
		}
	}
}
