using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArtificialNeuralNetwork;

namespace HandwritingRecognition
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{

			Console.WriteLine("Creating parser");
			var parser = new ImageParser();
			Console.WriteLine("Loading training images");
			var trainingImages = parser.LoadImages("Data/train-images.idx3-ubyte").ToList();
			var validationImages = parser.LoadImages("Data/t10k-images.idx3-ubyte").ToList();
			Console.WriteLine("Attaching labels");
			parser.AttachLabels(trainingImages, "Data/train-labels.idx1-ubyte");
			parser.AttachLabels(validationImages, "Data/t10k-labels.idx1-ubyte");

			Console.WriteLine("Preparing images for training");
			foreach (var i in trainingImages)
			{
				i.PrepareForTraining();
			}
			foreach (var i in validationImages)
			{
				i.PrepareForTraining();
			}

			var first = trainingImages.First();

			Console.WriteLine("Building neural network");
			var net = new Network(first.Width * first.Height, 10, 15);
			Console.WriteLine("Initialising network with random weights");
			net.InitialiseRandom();

			Console.WriteLine("Starting training...");
			net.StochasticGradientDescent(trainingImages, epochCount: 30000, miniBatchSize: 10, eta: 3f, validationSet: validationImages.Cast<ITraining>().ToArray());
			;

//			Application.EnableVisualStyles();
//			Application.SetCompatibleTextRenderingDefault(false);
//			Application.Run(new Form1());
		}
	}
}
