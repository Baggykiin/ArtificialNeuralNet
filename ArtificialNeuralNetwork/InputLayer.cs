using System.Collections.Generic;
using System.Linq;

namespace ArtificialNeuralNetwork
{
	public class InputLayer : ILayer
	{
		private readonly List<InputNeuron> _neurons = new List<InputNeuron>();
		public List<INeuron> Neurons => _neurons.Cast<INeuron>().ToList();

		public InputLayer(int size)
		{
			for (var i = 0; i < size; i++)
			{
				_neurons.Add(new InputNeuron("i" + i));
			}
		}

		public InputLayer(IEnumerable<InputNeuron> neurons)
		{
			_neurons = neurons.ToList();
		}

		public void Run()
		{
			;
		}

		public void Load(params float[] values)
		{
			Load((IEnumerable<float>)values);
		}

		public void Load(IEnumerable<float> values)
		{
			using (var en = values.GetEnumerator())
			{
				foreach (var neuron in _neurons)
				{
					en.MoveNext();
					neuron.Value = en.Current;
				}
			}
		}
	}
}