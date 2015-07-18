using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NeuralNetwork.Classes;

namespace NeuralNetwork
{
	internal class Program
	{
		#region -- Constants --
		private const int MaxEpochs = 5000;
		#endregion

		#region -- Variables --
		private static int _numInputParameters;
		private static int _numHiddenLayerNeurons;
		private static int _numOutputParameters;
		private static Network _network;
		private static List<DataSet> _dataSets; 
		#endregion

		#region -- Main --
		private static void Main()
		{
			Greet();
			SetupNetwork();
			TrainNetwork();
			VerifyTraining();
		}
		#endregion

		#region -- Network Training --
		private static void TrainNetwork()
		{
			PrintNewLine();
			PrintUnderline(50);
			Console.WriteLine("Training...");

			_network.Train(_dataSets);

			PrintNewLine();
			Console.WriteLine("Training Complete!");
			PrintNewLine();
		}

		private static void VerifyTraining()
		{
			Console.WriteLine("Let's test it!");
			PrintNewLine();

			while (true)
			{
				PrintUnderline(50);
				var values = GetInputData(String.Format("Type {0} inputs: ", _numInputParameters));
				var results = _network.GetResult(values);
				PrintNewLine();

				foreach (var result in results)
				{
					Console.WriteLine("Output: {0}", result);
				}

				PrintNewLine();

				var convertedResults = new int[results.Length];
				for (var i = 0; i < results.Length; i++) { convertedResults[i] = results[i] > 0.5 ? 1 : 0; }

				var message = String.Format("Was the result supposed to be {0}? (y/n/exit)", String.Join(" ", convertedResults));
				if (!GetBool(message))
				{
					var offendingDataSet = _dataSets.FirstOrDefault(x => x.Values.SequenceEqual(values) && x.Results.SequenceEqual(convertedResults));
					_dataSets.Remove(offendingDataSet);

					var expectedResults = GetExpectedResult("What were the expected results?");
					if(!_dataSets.Exists(x => x.Values.SequenceEqual(values) && x.Results.SequenceEqual(expectedResults)))
						_dataSets.Add(new DataSet(values, expectedResults));

					PrintNewLine();
					Console.WriteLine("Retraining Network...");
					PrintNewLine();

					_network.Train(_dataSets);
				}
				else
				{
					PrintNewLine();
					Console.WriteLine("Neat!");
					Console.WriteLine("Encouraging Network...");
					PrintNewLine();

					_network.Train(_dataSets);
				}
			}
		}
		#endregion

		#region -- Network Setup --
		private static void Greet()
		{
			Console.WriteLine("We're going to create an artificial Neural Network!");
			Console.WriteLine("The network will use back propagation to train itself.");
			PrintUnderline(50);
			PrintNewLine();
		}

		private static void SetupNetwork()
		{
			if (GetBool("Do you want to read from the space delimited data.txt file? (yes/no/exit)"))
			{
				SetupFromFile();
			}
			else
			{
				SetNumInputParameters();
				SetNumNeuronsInHiddenLayer();
				SetNumOutputParameters();
				GetTrainingData();
			}

			Console.WriteLine("Creating Network...");
			_network = new Network(_numInputParameters, _numHiddenLayerNeurons, _numOutputParameters, MaxEpochs);
			PrintNewLine();
		}

		private static void SetNumInputParameters()
		{
			PrintNewLine();
			Console.WriteLine("How many input parameters will there be? (2 or more)");
			_numInputParameters = GetInput("Input Parameters: ", 2);
			PrintNewLine(2);
		}

		private static void SetNumNeuronsInHiddenLayer()
		{
			Console.WriteLine("How many neurons in the hidden layer? (2 or more)");
			_numHiddenLayerNeurons = GetInput("Neurons: ", 2);
			PrintNewLine(2);
		}

		private static void SetNumOutputParameters()
		{
			Console.WriteLine("How many output parameters will there be? (1 or more)");
			_numOutputParameters = GetInput("Output Parameters: ", 1);
			PrintNewLine(2);
		}

		private static void GetTrainingData()
		{
			PrintUnderline(50);
			Console.WriteLine("Now, we need some input data.");
			PrintNewLine();

			_dataSets = new List<DataSet>();
			for (var i = 0; i < 4; i++)
			{
				var values = GetInputData(String.Format("Data Set {0}", i + 1));
				var expectedResult = GetExpectedResult(String.Format("Expected Result for Data Set {0}:", i + 1));
				_dataSets.Add(new DataSet(values, expectedResult));
			}
		}

		private static double[] GetInputData(string message)
		{
			Console.WriteLine(message);
			var line = Console.ReadLine();

			while (line == null || line.Split(' ').Count() != _numInputParameters)
			{
				Console.WriteLine("{0} inputs are required.", _numInputParameters);
				PrintNewLine();
				Console.WriteLine(message);
				line = Console.ReadLine();
			}

			var values = new double[_numInputParameters];
			var lineNums = line.Split(' ');
			for(var i = 0; i < lineNums.Length; i++)
			{
				double num;
				if (Double.TryParse(lineNums[i], out num))
				{
					values[i] = num;
				}
				else
				{
					Console.WriteLine("You entered an invalid number.  Try again");
					PrintNewLine(2);
					return GetInputData(message);
				}
			}

			return values;
		}

		private static int[] GetExpectedResult(string message)
		{
			Console.WriteLine(message);
			var line = Console.ReadLine();

			while (line == null || line.Split(' ').Count() != _numOutputParameters)
			{
				Console.WriteLine("{0} outputs are required.", _numOutputParameters);
				PrintNewLine();
				Console.WriteLine(message);
				line = Console.ReadLine();
			}

			var values = new int[_numOutputParameters];
			var lineNums = line.Split(' ');
			for (var i = 0; i < lineNums.Length; i++)
			{
				int num;
				if (int.TryParse(lineNums[i], out num) && (num == 0 || num == 1))
				{
					values[i] = num;
				}
				else
				{
					Console.WriteLine("You must enter 1s and 0s!");
					PrintNewLine(2);
					return GetExpectedResult(message);
				}
			}

			return values;
		}
		#endregion

		#region -- I/O Help --
		private static void SetupFromFile()
		{
			_dataSets = new List<DataSet>();
			var fileContent = File.ReadAllText("data.txt");
			var lines = fileContent.Split(new []{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

			if (lines.Length < 2)
			{
				WriteError("There aren't enough lines in the file.  The first line should have 3 integers representing the number of inputs, the number of hidden neurons and the number of outputs." +
				           "\r\nThere should also be at least one line of data.");
			}
			else
			{
				var setupParameters = lines[0].Split(' ');
				if (setupParameters.Length != 3)
					WriteError("There aren't enough setup parameters.");

				if (!int.TryParse(setupParameters[0], out _numInputParameters) || !int.TryParse(setupParameters[1], out _numHiddenLayerNeurons) || !int.TryParse(setupParameters[2], out _numOutputParameters))
					WriteError("The setup parameters are malformed.  There must be 3 integers.");

				if (_numInputParameters < 2)
					WriteError("The number of input parameters must be greater than or equal to 2.");

				if (_numHiddenLayerNeurons < 2)
					WriteError("The number of hidden neurons must be greater than or equal to 2.");

				if (_numOutputParameters < 1)
					WriteError("The number of hidden neurons must be greater than or equal to 1.");
			}

			for (var lineIndex = 1; lineIndex < lines.Length; lineIndex++)
			{
				var items = lines[lineIndex].Split(' ');
				if (items.Length != _numInputParameters + _numOutputParameters)
					WriteError(String.Format("The data file is malformed.  There were {0} elements on line {1} instead of {2}", items.Length, lineIndex + 1, _numInputParameters + _numOutputParameters));

				var values = new double[_numInputParameters];
				for (var i = 0; i < _numInputParameters; i++)
				{
					double num;
					if (!double.TryParse(items[i], out num))
						WriteError(String.Format("The data file is malformed.  On line {0}, input parameter {1} is not a valid number.", lineIndex + 1, items[i]));
					else
						values[i] = num;
				}

				var expectedResults = new int[_numOutputParameters];
				for (var i = 0; i < _numOutputParameters; i++)
				{
					int num;
					if (!int.TryParse(items[_numInputParameters + i], out num))
						Console.WriteLine(String.Format("The data file is malformed.  On line {0}, output paramater {1} is not a valid number.", lineIndex, items[i]));
					else
						expectedResults[i] = num;
				}
				_dataSets.Add(new DataSet(values, expectedResults));
			}
		}
		#endregion

		#region -- Console Helpers --
		private static int GetInput(string message, int min)
		{
			Console.Write(message);
			var num = GetNumber();

			while (num < min)
			{
				Console.Write(message);
				num = GetNumber();
			}

			return num;
		}

		private static int GetNumber()
		{
			int num;
			return int.TryParse(Console.ReadLine(), out num) ? num : 0;
		}

		private static bool GetBool(string message)
		{
			Console.WriteLine(message);
			Console.Write("Answer: ");
			var line = Console.ReadLine();

			while (line == null || (line.ToLower() != "y" && line.ToLower() != "n"))
			{
				if (line == "exit")
					Environment.Exit(0);

				Console.WriteLine(message);
				Console.Write("Answer: ");
				line = Console.ReadLine();
			}

			PrintNewLine();
			return line.ToLower() == "y";
		}

		private static void PrintNewLine(int numNewLines = 1)
		{
			for (var i = 0; i < numNewLines; i++)
				Console.WriteLine();
		}

		private static void PrintUnderline(int numUnderlines)
		{
			for (var i = 0; i < numUnderlines; i++)
				Console.Write('-');
			PrintNewLine(2);
		}

		private static void WriteError(string error)
		{
			Console.WriteLine(error);
			Console.ReadLine();
			Environment.Exit(0);
		}
		#endregion
	}
}