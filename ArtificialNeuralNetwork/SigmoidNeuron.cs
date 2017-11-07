using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	class SigmoidNeuron : INeuron
	{
		public float Output { get; private set; }
		public float Bias { get; set; }
		public Dictionary<INeuron, float> Weights { get; set; }

		public SigmoidNeuron()
		{
			
		}

		/// <summary>
		/// Initialises the neuron with random weights and bias.
		/// </summary>
		public void InitialiseRandom(IEnumerable<INeuron> inputs)
		{
			Weights = inputs.ToDictionary(input => input, _ => (float)RngProvider.Current.RandomNormal());
			Bias = (float)RngProvider.Current.RandomNormal();
		}

		/// <summary>
		/// Initialises the neuron with the given weights and bias.
		/// </summary>
		public void Initialise(IEnumerable<(INeuron neuron, float weight)> inputs, float bias)
		{
			Weights = inputs.ToDictionary(pair => pair.neuron, pair => pair.weight);
			Bias = bias;
		}

		public void ProcessInput()
		{
			var weightedInputs = Weights.Select(p => p.Key.Output * p.Value);
			var sum = weightedInputs.Sum();
			var biased = sum + Bias;
			Output = Sigmoid(biased);
			;
		}

		private static float Sigmoid(float value)
		{
			return 1.0f / (1.0f + (float)Math.Exp(-value));
		}

		public override string ToString()
		{
			return $"SigmoidNeuron(out: {Output:F} - bias: {Bias:F} - in: {Weights.Count}x)";
		}
	}
}
