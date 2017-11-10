using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	public class Input
	{
		public INeuron Source { get; set; }
		public INeuron Destination { get; set; }

		public float Weight { get; set; }
		public float RelativeError { get; set; }
		public List<float> NewWeights { get; set; } = new List<float>();

		public float Value => Source.Value;
		public float WeightedValue => Value * Weight;

		public Input(INeuron source, INeuron destination, float weight)
		{
			Source = source;
			Destination = destination;
			Weight = weight;
		}

		public void ApplyNewWeights()
		{
			var avgWeight = NewWeights.Average();
			Weight = avgWeight;
			NewWeights.Clear();
		}

		public override string ToString()
		{
			return $"Input(from {Source.Name}, weight: {Weight}, value: {Value})";
		}
	}
}
