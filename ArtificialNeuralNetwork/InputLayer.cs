using System.Collections.Generic;

namespace ArtificialNeuralNetwork
{
	public class InputLayer : ILayer
	{
		private readonly List<InputNeuron> _neurons = new List<InputNeuron>();
		public IEnumerable<INeuron> Neurons => _neurons;

		public InputLayer(int size)
		{
			for (var i = 0; i < size; i++)
			{
				_neurons.Add(new InputNeuron());
			}
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
					neuron.Output = en.Current;
				}
			}
		}
	}
}