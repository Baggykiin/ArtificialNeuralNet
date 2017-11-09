using System.Collections.Generic;
using System.Linq;

namespace ArtificialNeuralNetwork
{
	public interface ILayer
	{
		List<INeuron> Neurons { get; }

		void Run();
	}
}