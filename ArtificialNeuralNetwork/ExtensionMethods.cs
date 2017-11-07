using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	internal static class ExtensionMethods
	{
		public static IEnumerable<T> AsEnumerableOfOne<T>(this T obj)
		{
			yield return obj;
		}

		public static IEnumerable<float> Outputs(this ILayer layer) => layer.Neurons.Select(n => n.Output);
	}
}
