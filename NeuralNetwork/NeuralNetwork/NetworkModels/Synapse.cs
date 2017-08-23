using System;

namespace NeuralNetwork.NetworkModels
{
	public class Synapse
	{
		#region -- Properties --
		public Guid Id { get; set; }
		public Neuron InputNeuron { get; set; }
		public Neuron OutputNeuron { get; set; }
		public double Weight { get; set; }
		public double WeightDelta { get; set; }
		#endregion

		#region -- Constructor --
		public Synapse() { }

		public Synapse(Neuron inputNeuron, Neuron outputNeuron)
		{
			Id = Guid.NewGuid();
			InputNeuron = inputNeuron;
			OutputNeuron = outputNeuron;
			Weight = Network.GetRandom();
		}
		#endregion
	}
}