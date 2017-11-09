using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	public class SigmoidNeuron : INeuron
	{
		public float Value { get; set; }
		public float Bias { get; set; }
		public float Delta { get; set; }
		public string Name { get; set; }

		public List<Input> Inputs { get; set; }

		public SigmoidNeuron(string name = "")
		{
			Name = name;
		}

		/// <summary>
		/// Initialises the neuron with random weights and bias.
		/// </summary>
		public void InitialiseRandom(IEnumerable<INeuron> inputNeurons)
		{
			Inputs = inputNeurons.Select(neuron => new Input(neuron, this, (float)RngProvider.Current.RandomNormal())).ToList();
			Bias = (float)RngProvider.Current.RandomNormal();
		}

		public void Initialise(IEnumerable<INeuron> inputNeurons)
		{
			Inputs = inputNeurons.Select(neuron => new Input(neuron, this, 0)).ToList();
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
		public void Update()
		{
			var sum = Inputs.Sum(i => i.WeightedValue);
			var biased = sum + Bias;
			Value = SigmoidFunction(biased);
		}

		public void BackPropagate(float targetValue)
		{
			Delta = SigmoidDerivativeFunction(Value) * (Value - targetValue);
			AdjustParameters();
		}

		public void BackPropagate(IEnumerable<SigmoidNeuron> previous)
		{
			Delta = SigmoidDerivativeFunction(Value) * previous.Sum(n => n.Delta * n.Inputs.Find(i => i.Source == this).Weight);
			AdjustParameters();
		}

		private void AdjustParameters()
		{
			var deltaWeight = 0;
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
			return $"SigmoidNeuron {Name} (out: {Value:F} - bias: {Bias:F} - in: {Inputs.Count}x)";
		}
	}
}
