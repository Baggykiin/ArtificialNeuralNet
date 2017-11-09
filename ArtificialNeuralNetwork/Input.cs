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

		public float Value => Source.Value;
		public float WeightedValue => Value * Weight;

		public Input(INeuron source, INeuron destination, float weight)
		{
			Source = source;
			Destination = destination;
			Weight = weight;
		}

		public override string ToString()
		{
			return $"Input(from {Source.Name}, weight: {Weight}, value: {Value})";
		}
	}
}
