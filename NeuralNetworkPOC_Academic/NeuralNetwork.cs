using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace NeuralNetworkPOC
{

	public class NeuralNetwork
	{
		public int EpochsIterations { get; set; }
		public double Alpha{get; set;}
		public bool UseNeuronBias { get; set; }
		public bool IsTrained { get; private set; }
		public List<Layer> Layers { get; private set; }
		public bool ShuffleSamples { get; set; }
		public double Momentum { get; set; }

		const int defaultEpochs = 100000; //Iterations
		const double defaultAlpha = 0.01; //Learning Rate
		const double defaultMomentum = 0; //NN Momentum
   



		private int shuffleSeed = 1;    //Random Seeds

		public NeuralNetwork()
		{
			Alpha = defaultAlpha;               //default Alpha
			EpochsIterations = defaultEpochs;   //default Epochs
			UseNeuronBias = true;
			ShuffleSamples = false;
			Momentum = defaultMomentum;
			Layers = new List<Layer>();
			IsTrained = false;
		}

		public Matrix<double> Forward(Matrix<double> input)
		{
			Layer.SamplesSize = input.RowCount; //First Time we assign the number of samples!
			Layers[0].NeuronValues = input;
			
			Matrix<double> temp = null;

			for (int i = 0; i < Layers.Count - 1; i++)
			{
				if (UseNeuronBias)
					temp = Layers[i].ProcessInputs(Layers[i].NeuronValues,Layers[i+1].Bias);
				else
					temp = Layers[i].ProcessInputs(Layers[i].NeuronValues);

				Layers[i + 1].NeuronValues = temp;

			}

			return temp;
		}

		public Matrix<double> Forward(Vector<double> input)
		{
			return Forward(input.ToRowMatrix());
		}

		public int GetWidestLayerNeuronCount()
		{
			int maxNeurons = 0;

			foreach (var l in Layers)
			{
				if (l.NeuronCount > maxNeurons)
					maxNeurons = l.NeuronCount;
			}

			return maxNeurons;
		}

		private Matrix<double> GradientDescent(Matrix<double> outputCalculated, Matrix<double> error)
		{
			Matrix<double> der = ActivationFunctions.SigmoidDerived(outputCalculated);
			return error.PointwiseMultiply(der);
		}

		private Matrix<double> GradientDescent(double outputCalculated, double error)
		{
			Matrix<double> outputCalculatedMatrix = Matrix<double>.Build.Dense(1, 1,outputCalculated);
			Matrix<double> der = ActivationFunctions.SigmoidDerived(outputCalculatedMatrix);
			return error * der;
		}

		public Matrix<double> TrainNetwork(Matrix<double> trainingSetInput, Matrix<double> trainingSetOutput, List<int> hiddenLayersWidth)
		{
			BuildNetwork(trainingSetInput, trainingSetOutput, hiddenLayersWidth);
			
			Matrix<double> layerDelta = null;
			Matrix<double> layerError = null;

			if (UseNeuronBias)
				trainingSetInput = trainingSetInput.Append(Matrix<double>.Build.Dense(trainingSetInput.RowCount, 1, 1));//Add Column of 1s

			for (int i = 0; i < EpochsIterations; i++)
			{
				if (ShuffleSamples)
					ShuffleInputSamples(trainingSetInput, trainingSetOutput);

				Forward(trainingSetInput);

				for (int j = Layers.Count - 1; j > 0; j--)
				{
					var synapse = Layers[j].Synapse;
					var layerNeuronValues = Layers[j].NeuronValues;
					//layerNeuronValues is ÿ (output estimation) if it is the output layer

					if (j == Layers.Count - 1)//It is different for the last layer
					{
						layerError = layerNeuronValues - trainingSetOutput; //Error = ÿ - y
					}
					else
					{
						if (UseNeuronBias)
						{
							layerNeuronValues = layerNeuronValues.Append(Matrix<double>.Build.Dense(layerNeuronValues.RowCount, 1, 1));//Add Column of 1s
							synapse = Layers[j].Synapse.Stack(Layers[j + 1].Bias); // Add a row of biasese of the output neurons to the synapse matrix
						}

						layerError = layerDelta * synapse.Transpose();// previous layer deltas * synapse

					}

					//layer[j].SetLayerError(layerError); //SUM(Error^2) - To be implemented if needed. Only for stats
					layerDelta = GradientDescent(layerNeuronValues, layerError); //delta = GradientDescent(ÿ,Error)
					var previousLayerNeuronValues = Layers[j - 1].NeuronValues; //output of the previous layer (a)

					if (UseNeuronBias)
					{
						previousLayerNeuronValues = previousLayerNeuronValues.Append(Matrix<double>.Build.Dense(previousLayerNeuronValues.RowCount, 1, 1));//Add Column of 1s

						if (Layers[j].NextLayerNeuronCount > 0) //Is not last layer.
						{
							//Remove layer delta of the bias. The bias is not backpropagated
							layerDelta = layerDelta.RemoveColumn(layerDelta.ColumnCount - 1);
						}
					}

					var synapseUpdate = Alpha * (previousLayerNeuronValues.Transpose() * layerDelta);//Synapses Update (dJ/dW)

					if (UseNeuronBias)
					{
						var bUpdate = synapseUpdate.Row(synapseUpdate.RowCount - 1).ToRowMatrix();
						Layers[j].Bias -= bUpdate;

						if (Layers[j].PreviousBiasUpdate != null && Momentum != 0)
							Layers[j].Bias -= Layers[j].PreviousBiasUpdate.Multiply(Momentum);

						Layers[j].PreviousBiasUpdate = bUpdate;
						synapseUpdate = synapseUpdate.RemoveRow(synapseUpdate.RowCount - 1); //Remove the bias row!
					}

					Layers[j - 1].Synapse -= synapseUpdate;

					if (Layers[j - 1].PreviousSynapseUpdate != null && Momentum != 0)
						Layers[j - 1].Synapse -= Layers[j - 1].PreviousSynapseUpdate.Multiply(Momentum);

					Layers[j - 1].PreviousSynapseUpdate = synapseUpdate;
				}

			}
			
			IsTrained = true;
			return Layers[Layers.Count - 1].NeuronValues;

		}

		private void ShuffleInputSamples(Matrix<double> trainingSetInput, Matrix<double> trainingSetOutput)
		{
			Random rnd = new Random(shuffleSeed);

			for (int i = 0; i < trainingSetInput.RowCount; i++)
			{

				int from = rnd.Next(0, trainingSetInput.RowCount - 1);
				int to = rnd.Next(0, trainingSetInput.RowCount - 1);

				trainingSetInput.ExchangeRows(from, to);
				trainingSetOutput.ExchangeRows(from, to);
			}
		}

		private void BuildNetwork(Matrix<double> trainingSetInput, Matrix<double> trainingSetOutput, List<int> hiddenLayersWidth)
		{
			int inputLayerWidth = trainingSetInput.ColumnCount;   //Number of variables of the input
			int outputLayerWidth = trainingSetOutput.ColumnCount; //Number of variables of the output
			Layers.Clear();

			Layers.Add(new Layer(inputLayerWidth, hiddenLayersWidth[0], "INPUT"));
			Layers[0].SetLayerNeuronNames(NetworkGraphics.InputNames);

			for (int i = 0; i < hiddenLayersWidth.Count; i++)
			{
				if (i == hiddenLayersWidth.Count - 1)//Last Hidden Layer
					Layers.Add(new Layer(hiddenLayersWidth[i], outputLayerWidth, "HIDDEN" + i));
				else
					Layers.Add(new Layer(hiddenLayersWidth[i], hiddenLayersWidth[i + 1], "HIDDEN" + i));
			}

			Layers.Add(new Layer(outputLayerWidth, 0, "OUTPUT"));//LastLayer
			Layers[Layers.Count-1].SetLayerNeuronNames(NetworkGraphics.OutputNames);
		}
	}
}
