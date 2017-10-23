using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkPOC
{
	public static class SimulationFunctions
	{
		public static double TestFunc(double[] args)
		{
			if (args.Count() != 3)
				throw new Exception("Invalid Number of arguments!");

			return ((Math.Sin(args[0])+Math.Cosh(args[1]))* args[0]) / (1+args[2]);
		}

		public static double TestFunc2(double[] args)
		{
			if (args.Count() != 3)
				throw new Exception("Invalid Number of arguments!");

			return Math.Cos((Math.Sin(args[0]) * Math.Cosh(args[1])) / args[2]);
		}
	}

	public class MathFunc:DataSet
	{
		public delegate double MathFunction(double[] args);
		public MathFunction SimulationFunction { get; private set; }
		
		public MathFunc(double x, double y, double z)
		{
			Inputs.Add(x);
			Inputs.Add(y);
			Inputs.Add(z);

			SimulationFunction = SimulationFunctions.TestFunc;
			Outputs.Add(SimulationFunction(Inputs.ConvertAll(item => (double)item).ToArray()));

		}

		public MathFunc(double x, double y, double z, MathFunction func)
		{
			Inputs.Add(x);
			Inputs.Add(y);
			Inputs.Add(z);

			SimulationFunction = func;
			Outputs.Add(SimulationFunction(Inputs.ToArray()));

		}

		public static List<string> GetOutputVariablesList()
		{
			List<string> outputNeuronNames = new List<string>();
			outputNeuronNames.Add("Result");
			return outputNeuronNames;
		}

		public static List<string> GetInputVariablesList()
		{
			List<string> inputNeuronNames = new List<string>();
			inputNeuronNames.Add("X");
			inputNeuronNames.Add("Y");
			inputNeuronNames.Add("Z");
			return inputNeuronNames;
		}

		public override string ToString()
		{
			return "Inputs (X:" + Inputs[0] + ", Y:" + Inputs[1] + ", Z:" + Inputs[3] + ") -> Result:" + Outputs[0];
		}
	}
}
