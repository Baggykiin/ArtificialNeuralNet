using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArtificialNeuralNetwork;

namespace HandwritingRecognition
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			
			var parser = new ImageParser();
			var images = parser.LoadImages("Data/train-images.idx3-ubyte").ToList();

			parser.AttachLabels(images, "Data/train-labels.idx1-ubyte");

			foreach (var i in images)
			{
				i.PrepareForTraining();
			}

			var first = images.First();

			var net = new Network(first.Width * first.Height, 10, 15);
			net.InitialiseRandom();

			net.StochasticGradientDescent(images, 10, 10, 1);
		}
	}
}
