using System.Collections.Generic;

namespace NeuralNetwork.Classes
{
	public class Layer
	{
		public List<Neuron> Neurons { get; set; }

		public Layer()
		{
			Neurons = new List<Neuron>();
		}
	}
}
