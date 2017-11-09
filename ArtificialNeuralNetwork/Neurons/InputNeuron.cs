using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	class InputNeuron : INeuron
	{
		public float Value { get; set; }
		public string Name { get; set; }

		public InputNeuron(string name = "")
		{
			Name = name;
		}

		public void Update()
		{

		}

		public void InitialiseRandom(IEnumerable<INeuron> inputNeurons)
		{
			Value = (float)RngProvider.Current.NextDouble();
		}

		public void Initialise(IEnumerable<INeuron> inputNeurons)
		{
			
		}

		public void InitialiseRandom()
		{
			throw new NotImplementedException();
		}

		public override string ToString() => $"InputNeuron({Value})";
	}
}
