using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArtificialNeuralNetwork
{
	public class NetworkSerialiser
	{
		public string Save(Network network)
		{
			var json = JsonConvert.SerializeObject(network, Formatting.Indented);
			return json;
		}

		public Network Load(string json)
		{
			dynamic obj = JsonConvert.DeserializeObject(json);
			var layers = (JArray)obj.Layers;

			var inputLayer = LoadInputLayer(layers[0]);
			var processingLayers = LoadLayers(layers.Skip(1), inputLayer.Neurons);
			
			return new Network(inputLayer, processingLayers);
		}

		private IEnumerable<InputNeuron> LoadInputNeurons(dynamic inputNeurons)
		{
			foreach (var neuron in inputNeurons)
			{
				yield return new InputNeuron((string)neuron.Name)
				{
					Value = neuron.Value
				};
			}
		}

		private InputLayer LoadInputLayer(dynamic layer)
		{
			IEnumerable<InputNeuron> neurons = LoadInputNeurons(layer.Neurons);
			return new InputLayer(neurons);
		}

		private IEnumerable<Layer> LoadLayers(dynamic layers, List<INeuron> inputLayer)
		{
			var previousLayer = inputLayer;
			foreach (var layer in layers)
			{
				var loaded = (Layer)LoadLayer(layer, previousLayer);
				previousLayer = loaded.Neurons;
				yield return loaded;
			}
		}

		private Layer LoadLayer(dynamic layer, List<INeuron> previousLayer)
		{
			IEnumerable<SigmoidNeuron> neurons = LoadSigmoidNeurons(layer.Neurons, previousLayer);
			return new Layer(neurons);
		}

		private IEnumerable<SigmoidNeuron> LoadSigmoidNeurons(dynamic neurons, List<INeuron> previousLayer)
		{
			foreach (var neuron in neurons)
			{
				var sn = new SigmoidNeuron((string)neuron.Name);
				sn.Inputs = ((IEnumerable<Input>)LoadInputs(neuron.Inputs, previousLayer, sn)).ToList();
				yield return sn;
			}
		}

		private IEnumerable<Input> LoadInputs(dynamic inputs, List<INeuron> sourceNeurons, INeuron destinationNeuron)
		{
			for (int i = 0; i < inputs.Count; i++)
			{
				dynamic input = inputs[i];
				INeuron source = sourceNeurons[i];
				yield return new Input(source, destinationNeuron, (float)input.Weight);
			}
		}
	}
}
