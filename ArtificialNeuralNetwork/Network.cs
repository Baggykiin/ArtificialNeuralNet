using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArtificialNeuralNetwork
{
	public class Network
	{
		public ILayer[] Layers { get; }
		[JsonIgnore]
		public InputLayer InputLayer => (InputLayer)Layers.First();
		[JsonIgnore]
		public Layer OutputLayer => (Layer)Layers.Last();

		public Network(int inputSize, int outputSize, params int[] hiddenLayerSizes)
		{
			Layers = 
				((ILayer)new InputLayer(inputSize)).AsEnumerableOfOne()
				.Concat(hiddenLayerSizes.Select(size => new Layer(size)))
				.Concat(new Layer(outputSize).AsEnumerableOfOne()).ToArray();
		}

		public Network(InputLayer inputLayer, IEnumerable<Layer> processingLayers)
		{
			Layers = ((ILayer)inputLayer).AsEnumerableOfOne()
				.Concat(processingLayers).ToArray();
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

		public float[] GetOutput(IEnumerable<float> input)
		{
			Run(input);
			return OutputLayer.Neurons.Select(n => n.Value).ToArray();
		}

		public MiniBatchResult[] StochasticGradientDescent(int epochCount, int miniBatchSize, float eta, ITraining[] validationSet = null, params ITraining[] trainingData)
		{
			return StochasticGradientDescent(trainingData.ToArray(), epochCount, miniBatchSize, eta, validationSet);
		}

		public MiniBatchResult[] StochasticGradientDescent(IEnumerable<ITraining> trainingData, int epochCount, int miniBatchSize, float eta, ITraining[] validationSet = null)
		{
			return StochasticGradientDescent(trainingData.ToArray(), epochCount, miniBatchSize, eta, validationSet);
		}

		public MiniBatchResult[] StochasticGradientDescent(ITraining[] trainingData, int epochCount, int miniBatchSize, float eta, ITraining[] validationSet = null)
		{
			Console.WriteLine("Starting stochastic gradient descent training.");
			Console.WriteLine("---------------------------------------------");
			Console.WriteLine($"Training set size : {trainingData.Length}");
			Console.WriteLine($"Number of epochs  : {epochCount}");
			Console.WriteLine($"Mini-batch size   : {miniBatchSize}");
			Console.WriteLine($"Learning rate     : η = {eta:F}");
			Console.WriteLine($"Validation set    : {(validationSet == null ? "no" : $"yes: ({validationSet.Length} items)")}");
			Console.WriteLine("---------------------------------------------");

			var mbResults = new List<MiniBatchResult>();
			for (var i = 1; i <= epochCount; i++)
			{
				// Create a new mini-batch by randomly selecting some training data.
				var miniBatch = new List<ITraining>();
				for (var j = 0; j < miniBatchSize; j++)
				{
					miniBatch.Add(trainingData[RngProvider.Current.Next(trainingData.Length)]);
				}
				;
				var results = Train(miniBatch, eta);

				Console.WriteLine($"Epoch #{i:D2}/{epochCount:D2} complete.");
				Console.WriteLine($" > Avg. error      : {results.Average(r => r.TotalError):F}");
				Console.WriteLine($" > Success rate    : {(results.Count(r => r.Success) / (float)results.Count * 100):F}%");
				if (i % 500 == 0 && validationSet != null)
				{
					var validationResult = Validate(validationSet);
					Console.WriteLine($" > Validation rate : {validationResult * 100:F}%");
					Thread.Sleep(1000);
				}
				mbResults.Add(new MiniBatchResult
				{
					TrainingResults = results.ToArray(),

				});
			}
			return mbResults.ToArray();
		}

		private bool Validate(ITraining validation)
		{
			var output = GetOutput(validation.Inputs);

			for (int i = 0; i < output.Length; i++)
			{
				var diff = validation.DesiredOutput[i] - output[i];
				if (diff >= 0.5 || diff <= -0.5f) return false;
			}
			return true;
		}

		private float Validate(ITraining[] validationSet)
		{
			var successCount = validationSet.Select(Validate).Count(s => s);
			return (float)successCount / validationSet.Length;
		}

		private List<TrainingResult> Train(List<ITraining> miniBatch, float eta)
		{
			var results = new List<TrainingResult>();

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
					neuron.CalculateNewBias(eta);
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
						neuron.CalculateNewBias(eta);
					}
				}

				// Let's see how well we did. Save the total output error so we can report it later.
				var totalError = OutputLayer.Neurons.Cast<SigmoidNeuron>().Sum(n => Math.Abs(n.DeltaNetError));
				// Now let's see if we managed to actually produce the right output
				bool success = OutputLayer.Neurons.Cast<SigmoidNeuron>().All(n => Math.Abs(n.DeltaNetError) < 0.5f);

				results.Add(new TrainingResult
				{
					TotalError = totalError,
					Success = success
				});
			}

			// Mini batch training complete, now we can apply the newly calculated weights to our neurons.
			foreach (var layer in Layers.Skip(1).Cast<Layer>())
			{
				layer.ApplyNewWeightsAndBiases();
			}

			return results;
		}
	}

	public class MiniBatchResult
	{
		public TrainingResult[] TrainingResults { get; set; }
	}

	public class TrainingResult
	{
		public float TotalError { get; set; }
		public bool Success { get; set; }

		public override string ToString()
		{
			return $"TrainingResult(success: {Success}, error: {TotalError:F})";
		}
	}

	public interface ITraining
	{
		List<float> Inputs { get; }
		List<float> DesiredOutput { get; }
	}
}
