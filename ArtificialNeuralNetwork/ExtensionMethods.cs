using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

		public static IEnumerable<(TSource source, TLookup lookup)> Combine<TSource, TLookup>(this IEnumerable<TSource> source, IEnumerable<TLookup> lookup)
		{
			using (var en = lookup.GetEnumerator())
			{
				foreach (var element in source)
				{
					en.MoveNext();
					yield return (element, en.Current);
				}
			}
		} 

		public static IEnumerable<float> Outputs(this ILayer layer) => layer.Neurons.Select(n => n.Value);

	}
}
