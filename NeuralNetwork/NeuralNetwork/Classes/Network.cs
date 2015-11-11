using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Classes
{
    public class Network
    {
		#region -- Properties --
		public double LearnRate { get; set; }
		public double Momentum { get; set; }
		public List<Neuron> InputLayer { get; set; }
		public List<Neuron> HiddenLayer { get; set; }
		public List<Neuron> OutputLayer { get; set; }
		#endregion

		#region -- Globals --
		private static readonly Random Random = new Random();
		#endregion

        #region -- Constructor --
        public Network(int inputSize, int hiddenSize, int outputSize, double? learnRate = null, double? momentum = null)
        {
            LearnRate = learnRate ?? .4;
            Momentum = momentum ?? .9;
            InputLayer = new List<Neuron>();
            HiddenLayer = new List<Neuron>();
            OutputLayer = new List<Neuron>();

            for (var i = 0; i < inputSize; i++)
                InputLayer.Add(new Neuron());

            for (var i = 0; i < hiddenSize; i++)
                HiddenLayer.Add(new Neuron(InputLayer));

            for (var i = 0; i < outputSize; i++)
                OutputLayer.Add(new Neuron(HiddenLayer));
        }
        #endregion

        #region -- Training --
        public void Train(List<DataSet> dataSets, int numEpochs)
        {
            for (var i = 0; i < numEpochs; i++)
            {
                foreach (var dataSet in dataSets)
                {
                    ForwardPropagate(dataSet.Values);
                    BackPropagate(dataSet.Targets);
                }
            }
        }

		public void Train(List<DataSet> dataSets, double minimumError)
		{
			var error = 1.0;
			var numEpochs = 0;

			while (error > minimumError && numEpochs < int.MaxValue)
			{
				var errors = new List<double>();
				foreach (var dataSet in dataSets)
				{
					ForwardPropagate(dataSet.Values);
					BackPropagate(dataSet.Targets);
					errors.Add(CalculateError(dataSet.Targets));
				}
				error = errors.Average();
				numEpochs++;
			}
		}

        public void ForwardPropagate(params double[] inputs)
        {
            var i = 0;
            InputLayer.ForEach(a => a.Value = inputs[i++]);
            HiddenLayer.ForEach(a => a.CalculateValue());
            OutputLayer.ForEach(a => a.CalculateValue());
        }

        public void BackPropagate(params double[] targets)
        {
            var i = 0;
            OutputLayer.ForEach(a => a.CalculateGradient(targets[i++]));
            HiddenLayer.ForEach(a => a.CalculateGradient());
            HiddenLayer.ForEach(a => a.UpdateWeights(LearnRate, Momentum));
            OutputLayer.ForEach(a => a.UpdateWeights(LearnRate, Momentum));
        }

        public double[] Compute(params double[] inputs)
        {
            ForwardPropagate(inputs);
            return OutputLayer.Select(a => a.Value).ToArray();
        }

        public double CalculateError(params double[] targets)
        {
            var i = 0;
            return OutputLayer.Sum(a => Math.Abs(a.CalculateError(targets[i++])));
        }
        #endregion

        #region -- Helpers --
        public static double GetRandom()
        {
            return 2 * Random.NextDouble() - 1;
        }
        #endregion
    }

	#region -- Enum --
	public enum TrainingType
	{
		Epoch,
		MinimumError
	}
	#endregion
}