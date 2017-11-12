using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtificialNeuralNetwork;

namespace TestProject
{
	class Program
	{
		static void Main(string[] args)
		{
			var network = new Network(2, 2, 2);
			network.Initialise();

			var mid = network.Layers[1].Neurons.Cast<SigmoidNeuron>().ToList();
			mid[0].Inputs[0].Weight = 0.15f;
			mid[0].Inputs[1].Weight = 0.20f;
			mid[0].Bias = 0.35f;

			mid[1].Inputs[0].Weight = 0.25f;
			mid[1].Inputs[1].Weight = 0.30f;
			mid[1].Bias = 0.35f;

			var end = network.OutputLayer.Neurons.Cast<SigmoidNeuron>().ToList();
			end[0].Inputs[0].Weight = 0.40f;
			end[0].Inputs[1].Weight = 0.45f;
			end[0].Bias = 0.60f;

			end[1].Inputs[0].Weight = 0.50f;
			end[1].Inputs[1].Weight = 0.55f;
			end[1].Bias = 0.60f;

			network.StochasticGradientDescent(1, 1, 0.5f, null, new Training1());
			;
		}
	}

	internal class Training1 : ITraining
	{
		public List<float> Inputs => new List<float>{0.05f, 0.10f};
		public List<float> DesiredOutput => new List<float>{0.01f, 0.99f};
}
}
