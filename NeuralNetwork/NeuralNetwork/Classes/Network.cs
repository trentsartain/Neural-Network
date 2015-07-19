using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Classes
{
	public class Network
	{
		#region -- Properties --
		private Layer InputLayer { get; set; }
		private Layer HiddenLayer { get; set; }
		private Layer OutputLayer { get; set; }
		private int MaxEpochs { get; set; }
		#endregion

		#region -- Constructor --
		public Network(int numInputParameters, int numNeuronsInHiddenLayer, int numOutputParameters, int maxEpochs)
		{
			InputLayer = new Layer();
			HiddenLayer = new Layer();
			OutputLayer = new Layer();
			MaxEpochs = maxEpochs;

			//Setup Input Layer
			for (var i = 0; i < numInputParameters; i++)
			{
				InputLayer.Neurons.Add(new Neuron(1, true));
			}

			//Setup Hidden Layer
			for (var i = 0; i < numNeuronsInHiddenLayer; i++)
			{
				var neuron = new Neuron(numInputParameters);
				neuron.RandomizeWeights();
				HiddenLayer.Neurons.Add(neuron);
			}

			//Setup Output Layer
			for (var i = 0; i < numOutputParameters; i++)
			{
				var neuron = new Neuron(HiddenLayer.Neurons.Count);
				neuron.RandomizeWeights();
				OutputLayer.Neurons.Add(neuron);
			}
		}
		#endregion

		#region -- Training --
		public void Train(List<DataSet> dataSets)
		{
			if(!ValidateDataSets(dataSets))
				throw new Exception("The data set is invalid for this network.");

			var epoch = 0;
			while (epoch < MaxEpochs)
			{
				foreach (var dataSet in dataSets)
				{
					//Input Initialization
					foreach (var neuron in InputLayer.Neurons)
					{
						neuron.Inputs[0] = dataSet.Values[InputLayer.Neurons.IndexOf(neuron)];
					}

					//Forward Propagation Through Hidden Layer
					foreach (var neuron in HiddenLayer.Neurons)
					{
						neuron.Inputs = InputLayer.Neurons.Select(x => x.Output).ToArray();
					}

					//Output Layer
					foreach (var neuron in OutputLayer.Neurons)
					{
						neuron.Inputs = HiddenLayer.Neurons.Select(x => x.Output).ToArray();
						neuron.Error = Sigmoid.Derivative(neuron.Output) * (dataSet.Results[OutputLayer.Neurons.IndexOf(neuron)] - neuron.Output);
						neuron.AdjustWeights();

						//Back Propagation
						BackPropagate(neuron);
					}
				}

				epoch++;
			}
		}

		private void BackPropagate(Neuron outputNeuron)
		{
			for (var neuronIndex = 0; neuronIndex < HiddenLayer.Neurons.Count; neuronIndex++)
			{
				var neuron = HiddenLayer.Neurons[neuronIndex];
				neuron.Error = Sigmoid.Derivative(neuron.Output) * outputNeuron.Error * outputNeuron.Weights[neuronIndex];
				neuron.AdjustWeights();
			}
		}
		#endregion

		#region -- Use --
		public double[] GetResult(double[] inputs)
		{
			if (inputs.Length != InputLayer.Neurons.Count)
				throw new Exception("This data set is invalid for this network.");

			var results = new double[OutputLayer.Neurons.Count];

			//Prime Input Neurons
			foreach (var neuron in InputLayer.Neurons)
			{
				neuron.Inputs[0] = inputs[InputLayer.Neurons.IndexOf(neuron)];
			}

			//Propagate Forward
			foreach (var neuron in HiddenLayer.Neurons)
			{
				neuron.Inputs = InputLayer.Neurons.Select(x => x.Output).ToArray();
			}

			//Get Output Neuron Outputs
			foreach (var neuron in OutputLayer.Neurons)
			{
				neuron.Inputs = HiddenLayer.Neurons.Select(x => x.Output).ToArray();
				results[OutputLayer.Neurons.IndexOf(neuron)] = neuron.Output;
			}

			return results;
		}
		#endregion

		#region -- Helpers --
		private bool ValidateDataSets(IEnumerable<DataSet> dataSets)
		{
			foreach (var dataSet in dataSets)
			{
				if (dataSet.Values.Length != InputLayer.Neurons.Count) return false;
				if (dataSet.Results.Length != OutputLayer.Neurons.Count) return false;
			}

			return true;
		}
		#endregion
	}
}