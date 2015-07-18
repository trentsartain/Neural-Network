using System;
using System.Linq;

namespace NeuralNetwork.Classes
{
	public class Neuron
	{
		#region -- Properties --
		private double BiasWeight { get; set; }
		private bool IsInput { get; set; }
		public double[] Inputs { get; set; }
		public double[] Weights { get; set; }
		public double Error { get; set; }

		public double Output
		{
			get
			{
				if (IsInput) return Inputs[0];

				var num = Weights.Select((t, i) => t * Inputs[i]).Sum();
				return Sigmoid.Output(num + BiasWeight);
			}
		}
		#endregion

		#region -- Variables --
		private readonly Random _r = new Random();
		#endregion

		#region -- Constructor --
		public Neuron(int numInputs, bool isInput = false)
		{
			Inputs = new double[numInputs];
			Weights = new double[numInputs];

			if (isInput)
			{
				IsInput = true;
				BiasWeight = 0;
				Weights[0] = 1;
			}
		}
		#endregion

		#region -- Adjustments --
		public void RandomizeWeights()
		{
			for (var i = 0; i < Weights.Length; i++) { Weights[i] = _r.NextDouble(); }
			BiasWeight = _r.NextDouble();
		}

		public void AdjustWeights()
		{
			for (var i = 0; i < Weights.Length; i++) { Weights[i] += Error*Inputs[i]; }
			BiasWeight += Error;
		}
		#endregion
	}
}