using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ArtificialNeuralNetwork
{
	public class Input
	{
		[JsonIgnore]
		public INeuron Source { get; set; }
		[JsonIgnore]
		public INeuron Destination { get; set; }

		public float Weight { get; set; }
		[JsonIgnore]
		public float RelativeError { get; set; }

		[JsonIgnore]
		public List<float> NewWeights { get; set; } = new List<float>();

		[JsonIgnore]
		public float Value => Source.Value;
		[JsonIgnore]
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
