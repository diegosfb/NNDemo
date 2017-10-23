using MathNet.Numerics.LinearAlgebra;
using NeuralNetworkPOC;
using System;
using System.Collections.Generic;
using System.IO;


namespace SimplifiedNeuralNetwork
{

	//Very simple example to easily understand how it works!
	public static class ExecuteSimplifiedNeuralNetwork
	{
		public static void Execute()
		{

			if (File.Exists("output.txt"))
				File.Delete("output.txt");
		
			Console.WriteLine("Supervised learning using a simple neural network");
			Console.WriteLine("GOAL: Determine the type of Iris flower");

			var dataSet = new IrisFlowerDataSets();
			dataSet.LoadHardcodedData();

			Matrix<double> trainingSetInput = dataSet.CreatePropertiesMatrixFromSet(dataSet.TrainingSet.ToArray());
			Matrix<double> trainingSetOutput = dataSet.CreateOutputMatrixFromSet(dataSet.TrainingSet.ToArray());

			SimplifiedNeuralNetwork netwrok = new SimplifiedNeuralNetwork();
			List<int> hiddenLayersWidth = new List<int>();
			hiddenLayersWidth.Add(3);

			Console.WriteLine("\r\nTraining Network... \r\n");
			netwrok.TrainNetwork(trainingSetInput, trainingSetOutput, hiddenLayersWidth);

			Console.WriteLine(netwrok.ToString());
			File.AppendAllText("output.txt", netwrok.ToString());

			Console.WriteLine("\r\n\r\nTesting Trained Network: \r\n");
			Matrix<double> testSetInput = dataSet.CreatePropertiesMatrixFromSet(dataSet.TestingSet.ToArray());
			Matrix<double> testSetOutput = dataSet.CreateOutputMatrixFromSet(dataSet.TestingSet.ToArray());

			Matrix<double> testSetPredictedOutcome = netwrok.Forward(testSetInput);

			//Example Application Results Processing 
			Console.WriteLine(dataSet.ProcessTestSetResults(testSetOutput, testSetPredictedOutcome));
			File.AppendAllText("output.txt",dataSet.ProcessTestSetResults(testSetOutput, testSetPredictedOutcome));

		}
		
	}

	class SimplifiedNeuralNetwork
	{
		public int EpochsIterations { get; set; }
		const int randomSeed = 1;
		public double Alpha{get; set;}
		Matrix<double> synapse0;    //NetworksWeights (W1).
		Matrix<double> synapse1;    //NetworksWeights (W2).

		public SimplifiedNeuralNetwork()
		{
			Alpha = 0.005;               //default Alpha
			EpochsIterations = 1000000;  //default Epochs
		}

		static Matrix<double> CreateWeigthsMatrix(int inputLayerWidth, int outputLayerWidth)
		{
			Matrix<double> w = Matrix<double>.Build.Random(inputLayerWidth, outputLayerWidth,randomSeed);
			return w;
		}

		//Activation Function
		static Matrix<double> Sigmoid(Matrix<double> matrix)
		{
			//Output: 1/(1 + e^-x) for every element of the input matrix.
			//Is run when data reaches a neuron. It maps values to probabilities! (0 to 1)
			Matrix<double> outputMatrix = Matrix<double>.Build.Dense(matrix.RowCount, matrix.ColumnCount);
			foreach (var tuple in matrix.EnumerateIndexed())
				outputMatrix.At(tuple.Item1, tuple.Item2, (double)(1 / (1 + Math.Exp(-tuple.Item3))));
			return outputMatrix;
		}

		static Matrix<double> SigmoidDerived(Matrix<double> matrix)
		{
			//Returns the value of the sigmoid function derivative f'(x) = f(x)(1 - f(x)), 
			//This gives us the slope!
			//Output: x(1 - x) for every element of the input matrix.

			Matrix<double> outputMatrix = Matrix<double>.Build.Dense(matrix.RowCount, matrix.ColumnCount);
			foreach (var tuple in matrix.EnumerateIndexed())
				outputMatrix.At(tuple.Item1, tuple.Item2, tuple.Item3 * (1 - tuple.Item3));

			return outputMatrix;
		}

		public Matrix<double> Forward(Matrix<double> input)
		{
			Matrix<double> layer1Output = Sigmoid(input * synapse0);
			Matrix<double> layer2Output = Sigmoid(layer1Output * synapse1);
			return layer2Output;
		}

		public Matrix<double> Forward(Vector<double> input)
		{
			return Forward(input.ToRowMatrix());
		}

		private Matrix<double> BackpropagateLayerError(Matrix<double> outputCalculated, Matrix<double> error)
		{
			Matrix<double> der = SigmoidDerived(outputCalculated);
			return error.PointwiseMultiply(der);
		}

		public Matrix<double> TrainNetwork(Matrix<double> trainingSetInput, Matrix<double> trainingSetOutput, List<int>hiddenLayersWidth)
		{
			var watch = System.Diagnostics.Stopwatch.StartNew();
			
			
			int inputLayerWidth = trainingSetInput.ColumnCount;   //Number of variables of the input
			int hiddenLayerWidth = hiddenLayersWidth[0];          //Number of neurons on the hidden layer
			int outputLayerWidth = trainingSetOutput.ColumnCount; //Number of variables of the output

			synapse0 = CreateWeigthsMatrix(inputLayerWidth, hiddenLayerWidth);
			synapse1 = CreateWeigthsMatrix(hiddenLayerWidth, outputLayerWidth);

			Matrix<double> layer2Output = null;

			for (int i = 0; i < EpochsIterations; i++)
			{
				//Process inputs
				Matrix<double> layer1Output = Sigmoid(trainingSetInput * synapse0);
				layer2Output = Sigmoid(layer1Output * synapse1);

				//Calculate Layers Error
				Matrix<double> layer2Error = layer2Output - trainingSetOutput; //It is different for the last layer
				Matrix<double> layer2Delta = BackpropagateLayerError(layer2Output, layer2Error);

				Matrix<double> layer1Error = layer2Delta * synapse1.Transpose();
				Matrix<double> layer1Delta = BackpropagateLayerError(layer1Output, layer1Error);

				//Update Synapses
				var correctS1 = Alpha * (layer1Output.Transpose() * layer2Delta);
				var correctS0 = Alpha * (trainingSetInput.Transpose() * layer1Delta);

				synapse1 -= correctS1;
				synapse0 -= correctS0;
			}

			
			watch.Stop();
			var elapsedMs = watch.ElapsedMilliseconds;
			
			File.AppendAllText("output.txt", "Training Time (ms): " + elapsedMs);
			return layer2Output;

		}

		public override string ToString()
		{
			string network = "Alpha:" + Alpha + "\r\n";
			network += "Epochs:" + EpochsIterations + "\r\n";
			network += "Layers: 3 \r\n";

			network += "\r\nLayer INPUT has 4 neurons\r\n";
			network += "Activation Function: Sigmoid\r\n";
			network += synapse0.ToString();


			network += "\r\n\r\nHidden Layer has 3 neurons\r\n";
			network += "Activation Function: Sigmoid\r\n";
			network += synapse1.ToString();


			network += "\r\n\r\nLayer OUTPUT has 3 neurons\r\n";
			network += "Activation Function: Sigmoid\r\n";

			return network;
		}

	}
}
