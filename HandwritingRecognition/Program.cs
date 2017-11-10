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
			var images = parser.LoadImages("Data/train-images.idx3-ubyte").ToList();
			Console.WriteLine("Attaching labels");
			parser.AttachLabels(images, "Data/train-labels.idx1-ubyte");

			Console.WriteLine("Preparing images for training");
			foreach (var i in images)
			{
				i.PrepareForTraining();
			}

			var first = images.First();

			Console.WriteLine("Building neural network");
			var net = new Network(first.Width * first.Height, 10, 15);
			Console.WriteLine("Initialising network with random weights");
			net.InitialiseRandom();

			Console.WriteLine("Starting training...");
			net.StochasticGradientDescent(images, epochCount: 300, miniBatchSize: 10, eta: 1f);
			;

//			Application.EnableVisualStyles();
//			Application.SetCompatibleTextRenderingDefault(false);
//			Application.Run(new Form1());
		}
	}
}
