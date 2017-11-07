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
		public float Output { get; set; }

		public void ProcessInput()
		{

		}

		public void InitialiseRandom()
		{
			throw new NotImplementedException();
		}

		public override string ToString() => $"InputNeuron({Output})";
	}
}
