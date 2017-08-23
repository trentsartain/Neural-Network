using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using NeuralNetwork.NetworkModels;
using Newtonsoft.Json;

namespace NeuralNetwork.Helpers
{
	public static class ImportHelper
	{
		public static Network ImportNetwork()
		{
			var dn = GetHelperNetwork();
			if (dn == null) return null;

			var network = new Network();
			var allNeurons = new List<Neuron>();

			network.LearnRate = dn.LearnRate;
			network.Momentum = dn.Momentum;

			//Input Layer
			foreach (var n in dn.InputLayer)
			{
				var neuron = new Neuron
				{
					Id = n.Id,
					Bias = n.Bias,
					BiasDelta = n.BiasDelta,
					Gradient = n.Gradient,
					Value = n.Value
				};

				network.InputLayer.Add(neuron);
				allNeurons.Add(neuron);
			}

			//Hidden Layers
			foreach (var layer in dn.HiddenLayers)
			{
				var neurons = new List<Neuron>();
				foreach (var n in layer)
				{
					var neuron = new Neuron
					{
						Id = n.Id,
						Bias = n.Bias,
						BiasDelta = n.BiasDelta,
						Gradient = n.Gradient,
						Value = n.Value
					};

					neurons.Add(neuron);
					allNeurons.Add(neuron);
				}

				network.HiddenLayers.Add(neurons);
			}

			//Export Layer
			foreach (var n in dn.OutputLayer)
			{
				var neuron = new Neuron
				{
					Id = n.Id,
					Bias = n.Bias,
					BiasDelta = n.BiasDelta,
					Gradient = n.Gradient,
					Value = n.Value
				};

				network.OutputLayer.Add(neuron);
				allNeurons.Add(neuron);
			}

			//Synapses
			foreach (var syn in dn.Synapses)
			{
				var synapse = new Synapse { Id = syn.Id };
				var inputNeuron = allNeurons.First(x => x.Id == syn.InputNeuronId);
				var outputNeuron = allNeurons.First(x => x.Id == syn.OutputNeuronId);
				synapse.InputNeuron = inputNeuron;
				synapse.OutputNeuron = outputNeuron;
				synapse.Weight = syn.Weight;
				synapse.WeightDelta = syn.WeightDelta;

				inputNeuron.OutputSynapses.Add(synapse);
				outputNeuron.InputSynapses.Add(synapse);
			}

			return network;
		}

		public static List<DataSet> ImportDatasets()
		{
			try
			{
				var dialog = new OpenFileDialog
				{
					Multiselect = false,
					Title = "Open Dataset File",
					Filter = "Text File|*.txt;"
				};

				using (dialog)
				{
					if (dialog.ShowDialog() != DialogResult.OK) return null;
					using (var file = File.OpenText(dialog.FileName))
					{
						return JsonConvert.DeserializeObject<List<DataSet>>(file.ReadToEnd());
					}
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		private static HelperNetwork GetHelperNetwork()
		{
			try
			{
				var dialog = new OpenFileDialog
				{
					Multiselect = false,
					Title = "Open Network File",
					Filter = "Text File|*.txt;"
				};

				using (dialog)
				{
					if (dialog.ShowDialog() != DialogResult.OK) return null;

					using (var file = File.OpenText(dialog.FileName))
					{
						return JsonConvert.DeserializeObject<HelperNetwork>(file.ReadToEnd());
					}
				}
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
