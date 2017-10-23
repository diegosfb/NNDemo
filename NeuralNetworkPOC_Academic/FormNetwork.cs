using MathNet.Numerics.LinearAlgebra;
using NeuralNetworkPOC.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetworkPOC
{
	public partial class FormNetwork : Form
	{
		//Select the DEMO to be run!
		IrisFlowerDataSets dataSet = new IrisFlowerDataSets();
		//MathFuncDataSets dataSet = new MathFuncDataSets();
		//WheatSeedDataSets dataSet = new WheatSeedDataSets();
		
		static string datasetFile = Settings.Default.DefaultDataSetFile;
		NeuralNetwork network;
		Matrix<double> testSetPredictedOutcome;

		public FormNetwork()
		{
			InitializeComponent();

			network = new NeuralNetwork();
			dataSet.UseNormalizedOutput = true;
			
			//We can either load the data hardcoded in as the Dataset defaults
			//the default examples or we can load the data from a file
			dataSet.LoadHardcodedData(); //hardcoded examples
			//dataSet.LoadFileInputData(datasetFile,true); //Load values from file
			
			buttonRun.Enabled = network.IsTrained;
			buttonRunCase.Enabled = network.IsTrained && (dataSet.GetType() == typeof(IrisFlowerDataSets));
			buttonGenerate.Enabled = network.IsTrained && (dataSet.GetType() == typeof(IrisFlowerDataSets));
			buttonExport.Enabled = network.IsTrained;
		}

		private void buttonRun_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			textBoxResult.Clear();
			Matrix<double> testSetInput, testSetOutput;
			CreateTestingSet(out testSetInput, out testSetOutput);
			testSetPredictedOutcome = network.Forward(testSetInput);

			NetworkGraphics.ToTreeView(treeView1, network);
			NetworkGraphics.ToPictureBox(pictureBox1, network, 400, 0);

			textBoxResult.AppendText(dataSet.ProcessTestSetResults(testSetInput,testSetOutput, testSetPredictedOutcome));
			Cursor.Current = Cursors.Default;

		}

		private void buttonTrain_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Matrix<double> trainingSetInput, trainingSetOutput;
			CreateTrainingSet(out trainingSetInput, out trainingSetOutput);
			textBoxResult.Clear();
						
			//////////////////////Configure Network///////////////////////////
			List<int> hiddenLayersWidth = new List<int>();
			hiddenLayersWidth.Add(3);
			network.EpochsIterations = 1000000;
			network.Alpha = 0.005;
			//network.Momentum = 0.1;
			network.UseNeuronBias = false;

			//////////////////////////////////////////////////////////////////

			var watch = System.Diagnostics.Stopwatch.StartNew();
			network.TrainNetwork(trainingSetInput, trainingSetOutput, hiddenLayersWidth);

			watch.Stop();
			var elapsedMs = watch.ElapsedMilliseconds;

			//Console.WriteLine("Elapsed Training Time (ms): " + elapsedMs); //For Debug purposes

			NetworkGraphics.ToTreeView(treeView1, network);
			NetworkGraphics.ToPictureBox(pictureBox1, network, 400, 0);

			buttonRun.Enabled = network.IsTrained;
			buttonRunCase.Enabled = network.IsTrained && (dataSet.GetType() == typeof(IrisFlowerDataSets));
			buttonGenerate.Enabled = network.IsTrained && (dataSet.GetType() == typeof(IrisFlowerDataSets));
			buttonExport.Enabled = network.IsTrained;
			Cursor.Current = Cursors.Default;
		}

		private void CreateTrainingSet(out Matrix<double> trainingSetInput, out Matrix<double> trainingSetOutput)
		{
			NetworkGraphics.InputNames = dataSet.InputNeuronNames;
			NetworkGraphics.OutputNames = dataSet.OutputNeuronNames;

			trainingSetInput = dataSet.CreatePropertiesMatrixFromSet(dataSet.TrainingSet.ToArray());
			trainingSetOutput = dataSet.CreateOutputMatrixFromSet(dataSet.TrainingSet.ToArray());
		}

		private void CreateTestingSet(out Matrix<double> testingSetInput, out Matrix<double> testingSetOutput)
		{
			NetworkGraphics.OutputNames = dataSet.InputNeuronNames;
			NetworkGraphics.InputNames = dataSet.OutputNeuronNames;
			
			testingSetInput = dataSet.CreatePropertiesMatrixFromSet(dataSet.TestingSet.ToArray());
			testingSetOutput = dataSet.CreateOutputMatrixFromSet(dataSet.TestingSet.ToArray());
		}



		private void buttonRunCase_Click(object sender, EventArgs e)
		{
			if (network.IsTrained && dataSet.GetType() == typeof(IrisFlowerDataSets))
			{
				Cursor.Current = Cursors.WaitCursor;
				var caseSet = new DataSet[1];
				caseSet[0] = new IrisFlower(Convert.ToDouble(textBoxSL.Text), Convert.ToDouble(textBoxSW.Text), Convert.ToDouble(textBoxPL.Text), Convert.ToDouble(textBoxPW.Text), IrisFlower.IrisSpecies.Unknown);
				textBoxResult.Clear();

				Matrix<double> runCaseInput = dataSet.CreatePropertiesMatrixFromSet(caseSet);
				Matrix<double> predicted = network.Forward(runCaseInput);

				textBoxResult.AppendText(DataSet.ClassifyOutput(predicted.Column(0), NetworkGraphics.OutputNames, caseSet[0]));

				NetworkGraphics.ToTreeView(treeView1, network);
				NetworkGraphics.ToPictureBox(pictureBox1, network, 400, 0);
				Cursor.Current = Cursors.Default;

			}
		}

		private void buttonGenerate_Click(object sender, EventArgs e)
		{
			if (network.IsTrained && dataSet.GetType() == typeof(IrisFlowerDataSets))
			{
				Random rnd = new Random();
				int index = rnd.Next(0, dataSet.TestingSet.Count - 1);
				var item = dataSet.TestingSet[index];

				textBoxPL.Text = item.Inputs[0].ToString();
				textBoxPW.Text = item.Inputs[1].ToString();
				textBoxSL.Text = item.Inputs[2].ToString();
				textBoxSW.Text = item.Inputs[3].ToString();
				textBoxGeneratedSpecies.Text = item.Outputs[0].ToString();
			}
			
		}

		private void buttonExport_Click(object sender, EventArgs e)
		{
			String path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

			string fileDetails = path + "\\NeuralNetworkConfig" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".txt";
			string xmlFile = path + "\\NeuralNetworkConfig" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".xml";
			string sets = path + "\\DataSet" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".csv";
			string testResult = path + "\\TestRun" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".txt";

			File.AppendAllText(fileDetails, "NEURAL NETWORK CONFIG");
			File.AppendAllText(fileDetails, "\r\nLearning Rate: " + network.Alpha);
			File.AppendAllText(fileDetails, "\r\nMomentum: " + network.Momentum);
			File.AppendAllText(fileDetails, "\r\nEpochs used on the training: " + network.EpochsIterations);

			File.AppendAllText(fileDetails, "\r\n\r\nLAYERS STRUCTURE");

			foreach (var layer in network.Layers)
			{
				File.AppendAllText(fileDetails, "\r\n\r\nLayer " + layer.Name + " has " + layer.NeuronCount + " neurons");
				File.AppendAllText(fileDetails, "\r\nActivation Function: " + layer.activationFunction.Method.Name);

				if (network.UseNeuronBias)
				{
					for (int i = 0; i < layer.NeuronCount; i++)
						File.AppendAllText(fileDetails, "\r\nNeuron " + i + " - Bias: " + layer.Bias[1,i]);
				}

				if (layer.Synapse != null)
					File.AppendAllText(fileDetails, "\r\nWeights Matrix - " + layer.Synapse);

			}

			ExportDataSets(sets);
			NetworkGraphics.ExportTreeViewToXml(treeView1, xmlFile);
			MessageBox.Show("Files " + fileDetails + " & " + xmlFile + " SUCCESSFULY CREATED");
		}

		private void ExportDataSets(string fileName)
		{
			String lineIO = string.Empty;
			String lineLabels = string.Empty;

			foreach (var name in network.Layers[0].NeuronNames)
			{
				lineIO += "INPUT,";
				lineLabels += name + ",";
			}

			foreach (var name in network.Layers[network.Layers.Count - 1].NeuronNames)
			{
				lineIO += "OUTPUT,";
				lineLabels += name + ",";
			}

			lineIO += "SETTYPE";
			lineLabels += "Used in Set";

			String content = lineIO + "\r\n" + lineLabels + "\r\n";

			foreach (var sample in dataSet.TrainingSet)
			{
				foreach (var inputVal in sample.Inputs)
					content += inputVal.ToString() + ",";

				foreach (var outputVal in sample.Outputs)
					content += outputVal.ToString() + ",";

				content += "Training\r\n";

			}

			foreach (var sample in dataSet.TestingSet)
			{
				foreach (var inputVal in sample.Inputs)
					content += inputVal.ToString() + ",";

				foreach (var outputVal in sample.Outputs)
					content += outputVal.ToString() + ",";

				content += "Testing\r\n";

			}

			File.AppendAllText(fileName, content);
		}
	}
}
