namespace NeuralNetwork.Network
{
	public class Synapse
	{
		#region -- Properties --
		public Neuron InputNeuron { get; set; }
		public Neuron OutputNeuron { get; set; }
		public double Weight { get; set; }
		public double WeightDelta { get; set; }
		#endregion

		#region -- Constructor --
		public Synapse(Neuron inputNeuron, Neuron outputNeuron)
		{
			InputNeuron = inputNeuron;
			OutputNeuron = outputNeuron;
			Weight = Network.GetRandom();
		}
		#endregion
	}
}