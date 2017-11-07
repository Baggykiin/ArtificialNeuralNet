using System.Collections.Generic;
using System.Linq;

namespace ArtificialNeuralNetwork
{
	public interface ILayer
	{
		IEnumerable<INeuron> Neurons { get; }

		void Run();
	}
}