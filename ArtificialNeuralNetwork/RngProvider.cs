using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	class RngProvider : Random
	{
		private static RngProvider _current;
		public static RngProvider Current => _current ?? (_current = new RngProvider());

		public RngProvider()
		{
			
		}

		public double RandomNormal()
		{
			var u1 = 1.0 - NextDouble(); //uniform(0,1] random doubles
			var u2 = 1.0 - NextDouble();
			return  Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
		}
	}
}
