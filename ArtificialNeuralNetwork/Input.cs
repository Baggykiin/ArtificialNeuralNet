using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	class Input
	{
		public INeuron InputNeuron { get; set; }
		public float Weight { get; set; }

		public float Value => InputNeuron.Output;
		public float WeightedValue => Value * Weight;

		public Input(INeuron inputNeuron, float weight)
		{
			InputNeuron = inputNeuron;
			Weight = weight;
		}
	}
}
