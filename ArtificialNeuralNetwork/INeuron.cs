using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	public interface INeuron
	{
		float Value { get; set; }
		string Name { get; set; }

		void Update();
		void InitialiseRandom(IEnumerable<INeuron> inputNeurons);
		void Initialise(IEnumerable<INeuron> inputNeurons);
	}
}
