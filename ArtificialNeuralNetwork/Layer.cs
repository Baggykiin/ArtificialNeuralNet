using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	public class Layer : ILayer
	{
		public List<INeuron> Neurons { get; } = new List<INeuron>();

		public Layer(int size)
		{
			for (var i = 0; i < size; i++)
			{
				Neurons.Add(new SigmoidNeuron("x" + i));
			}
		}

		public void Run()
		{
			foreach (var neuron in Neurons)
			{
				neuron.Update();
			}
		}

		public void InitialiseRandom(ILayer previousLayer)
		{
			foreach (var neuron in Neurons)
			{
				neuron.InitialiseRandom(previousLayer.Neurons);
			}
		}

		public void Initialise(ILayer previousLayer)
		{
			foreach (var neuron in Neurons)
			{
				neuron.Initialise(previousLayer.Neurons);
			}
		}

		public void ApplyNewWeightsAndBiases()
		{
			foreach (var neuron in Neurons.Cast<SigmoidNeuron>())
			{
				neuron.ApplyNewWeights();
				neuron.ApplyNewBiases();
			}
		}
	}
}
