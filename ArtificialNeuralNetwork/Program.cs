using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	class Program
	{
		static void Main(string[] args)
		{
			var network = new Network(2, 1);
			network.InitialiseRandom();

			network.InputLayer.Load(1, 0.5f);

			network.Run();
		}
	}
}
