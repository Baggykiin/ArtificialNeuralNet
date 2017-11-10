using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	public class SigmoidNeuron : INeuron
	{
		public float Value { get; set; }
		public float ValueDerivative { get; set; }
		public float Bias { get; set; }
		public float DeltaNetError { get; set; }

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
			ValueDerivative = SigmoidDerivativeFunction(Value);
		}

		/// <summary>
		/// Calculates the error values for this neuron based on the error values of its output neurons.
		/// </summary>
		public void CalculateError(IEnumerable<SigmoidNeuron> outputNeurons, int inputIndex)
		{
			DeltaNetError = outputNeurons.Sum(n => n.DeltaNetError * n.ValueDerivative * n.Inputs[inputIndex].Weight);

			foreach (var input in Inputs)
			{
				CalculateError(input);
			}
			;
		}

		/// <summary>
		/// Calculates the error values for this neuron with respect to a single target output value.
		/// </summary>
		public void CalculateError(float targetValue)
		{
			DeltaNetError = -(targetValue - Value);
			foreach (var input in Inputs)
			{
				CalculateError(input);
			}
		}

		/// <summary>
		/// Calculates the error value for a single input with respect to a single target output value.
		/// </summary>
		public void CalculateError(Input input)
		{
			var deltaError = DeltaNetError * ValueDerivative * input.Value;
			input.RelativeError = deltaError;
		}

		public void CalculateNewWeights(float eta)
		{
			foreach (var input in Inputs)
			{
				input.NewWeights.Add(input.Weight - eta * input.RelativeError);
			}
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

		public void ApplyNewWeights()
		{
			foreach (var input in Inputs)
			{
				input.ApplyNewWeights();
			}
		}
	}
}
