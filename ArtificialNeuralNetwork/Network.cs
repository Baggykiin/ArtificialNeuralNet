using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialNeuralNetwork
{
	public class Network
	{
		public ILayer[] Layers { get; }
		public InputLayer InputLayer => (InputLayer)Layers.First();
		public Layer OutputLayer => (Layer)Layers.Last();

		public Network(int inputSize, int outputSize, params int[] hiddenLayerSizes)
		{
			Layers = 
				((ILayer)new InputLayer(inputSize)).AsEnumerableOfOne()
				.Concat(hiddenLayerSizes.Select(size => new Layer(size)))
				.Concat(new Layer(outputSize).AsEnumerableOfOne()).ToArray();
		}

		public void InitialiseRandom()
		{
			for (var i = 1; i < Layers.Length; i++)
			{
				var layer = (Layer) Layers[i];
				layer.InitialiseRandom(Layers[i-1]);
			}
		}

		public void Run()
		{
			foreach (var layer in Layers)
			{
				layer.Run();
			}
		}

		public void Run(IEnumerable<float> input)
		{
			InputLayer.Load(input);
			Run();
		}

		public void StochasticGradientDescent(IEnumerable<ITraining> trainingData, int epochCount, int miniBatchSize, int learningRate)
		{
			StochasticGradientDescent(trainingData.ToArray(), epochCount, miniBatchSize, learningRate);
		}

		public void StochasticGradientDescent(ITraining[] trainingData, int epochCount, int miniBatchSize, int learningRate)
		{
			for (var i = 0; i < epochCount; i++)
			{
				// Create a new mini-batch by randomly selecting some training data.
				var miniBatch = new List<ITraining>();
				for (var j = 0; j < miniBatchSize; j++)
				{
					miniBatch.Add(trainingData[RngProvider.Current.Next(trainingData.Length)]);
				}
				;
				Train(miniBatch);
			}
		}

		private void Train(List<ITraining> miniBatch)
		{
			foreach (var training in miniBatch)
			{
				Run(training.Inputs);

				var outcome = OutputLayer.Outputs();

				var nablaBias = new float[Layers.Length][][];
				var nablaWeight = new float[Layers.Length][];

				// Start at the last layer, progressively moving forward, calculating ∇b and ∇w.
				for (var i = Layers.Length - 1; i >= 0; i--)
				{
					var result = BackPropagate();


				}
			}
		}

		private void BackPropagate()
		{
			
		}
	}

	public interface ITraining
	{
		List<float> Inputs { get; }
		List<float> DesiredOutput { get; }
	}
}
