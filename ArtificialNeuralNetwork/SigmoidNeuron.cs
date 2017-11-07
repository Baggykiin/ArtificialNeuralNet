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
		public float Delta { get; set; }

		public List<Input> Inputs { get; set; }

		public SigmoidNeuron()
		{
			
		}

		/// <summary>
		/// Initialises the neuron with random weights and bias.
		/// </summary>
		public void InitialiseRandom(IEnumerable<INeuron> inputNeurons)
		{
			Inputs = inputNeurons.Select(neuron => new Input(neuron, (float)RngProvider.Current.RandomNormal())).ToList();
			Bias = (float)RngProvider.Current.RandomNormal();
		}

		/// <summary>
		/// Initialises the neuron with the given weights and bias.
		/// </summary>
		public void Initialise(IEnumerable<Input> inputs, float bias)
		{
			Inputs = inputs.ToList();
			Bias = bias;
		}

		/// <summary>
		/// Calculates the output of this neuron according to the given inputs.
		/// </summary>
		public void ProcessInput()
		{
			var sum = Inputs.Sum(i => i.WeightedValue);
			var biased = sum + Bias;
			Output = SigmoidFunction(biased);
		}

		public void BackPropagate(float targetValue)
		{
			var error = SigmoidDerivativeFunction(Output) * (Output - targetValue);
		}

		public void BackPropagate(IEnumerable<SigmoidNeuron> previous)
		{
			var error = SigmoidDerivativeFunction(Output) * 
		}

		private static float SigmoidFunction(float value)
		{
			return 1.0f / (1.0f + (float)Math.Exp(-value));
		}

		private static float SigmoidDerivativeFunction(float value)
		{
			return value * (1.0f - value);
		}

		public override string ToString()
		{
			return $"SigmoidNeuron(out: {Output:F} - bias: {Bias:F} - in: {Inputs.Count}x)";
		}
	}
}
