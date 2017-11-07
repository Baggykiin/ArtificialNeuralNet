using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	class Layer : ILayer
	{
		private readonly List<SigmoidNeuron> _neurons = new List<SigmoidNeuron>();
		public IEnumerable<INeuron> Neurons => _neurons;

		public Layer(int size)
		{
			for (var i = 0; i < size; i++)
			{
				_neurons.Add(new SigmoidNeuron());
			}
		}

		public void Run()
		{
			foreach (var neuron in _neurons)
			{
				neuron.ProcessInput();
			}
		}

		public void InitialiseRandom(ILayer previousLayer)
		{
			foreach (var neuron in Neurons.Cast<SigmoidNeuron>())
			{
				neuron.InitialiseRandom(previousLayer.Neurons);
			}
		}
	}
}
