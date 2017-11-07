using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	interface INeuron
	{
		float Output { get; }

		void ProcessInput();
	}
}
