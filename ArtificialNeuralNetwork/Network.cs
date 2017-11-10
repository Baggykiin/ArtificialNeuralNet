using System;
using System.Collections;
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

		public void Initialise()
		{
			for (var i = 1; i < Layers.Length; i++)
			{
				var layer = (Layer)Layers[i];
				layer.Initialise(Layers[i - 1]);
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

		public void StochasticGradientDescent(int epochCount, int miniBatchSize, float eta, params ITraining[] trainingData)
		{
			StochasticGradientDescent(trainingData.ToArray(), epochCount, miniBatchSize, eta);
		}

		public void StochasticGradientDescent(IEnumerable<ITraining> trainingData, int epochCount, int miniBatchSize, float eta)
		{
			StochasticGradientDescent(trainingData.ToArray(), epochCount, miniBatchSize, eta);
		}

		public void StochasticGradientDescent(ITraining[] trainingData, int epochCount, int miniBatchSize, float eta)
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
				Train(miniBatch, eta);
			}
		}

		private void Train(List<ITraining> miniBatch, float eta)
		{
			foreach (var training in miniBatch)
			{
				// Feed the training input into the network
				Run(training.Inputs);

				// Start at the last layer, which is trained using the desired output from our training.
				for (var i = 0; i < OutputLayer.Neurons.Count; i++)
				{
					var neuron = (SigmoidNeuron)OutputLayer.Neurons[i];
					neuron.CalculateError(training.DesiredOutput[i]);
					neuron.CalculateNewWeights(eta);
				}
				// Now run the hidden layers, which are trained using the data from the previously calculated layer.
				for (var i = Layers.Length - 2; i > 0; i--)
				{
					var layer = Layers[i];
					for (var j = 0; j < layer.Neurons.Count; j++)
					{
						var neuron = (SigmoidNeuron)layer.Neurons[j];

						neuron.CalculateError(Layers[i+1].Neurons.Cast<SigmoidNeuron>(), j);
						neuron.CalculateNewWeights(eta);
					}
				}
			}

			// Mini batch training complete, now we can apply the newly calculated weights to our neurons.
			foreach (var layer in Layers.Skip(1).Cast<Layer>())
			{
				layer.ApplyNewWeights();
			}
		}
	}

	public interface ITraining
	{
		List<float> Inputs { get; }
		List<float> DesiredOutput { get; }
	}
}
