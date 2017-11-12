using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArtificialNeuralNetwork;

namespace HandwritingRecognition
{
	public partial class Form1 : Form
	{
		private NetworkSerialiser ns = new NetworkSerialiser();
		private Network net;
		private List<HandwritingImage> trainingImages;
		private List<HandwritingImage> validationImages;

		public Form1()
		{
			InitializeComponent();
			Logger.OnLog += AppendMessage;
		}

		private void AppendMessage(string message)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() => AppendMessage(message)));
				return;
			}
			textBox1.Text += message + "\r\n";
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Task.Run((Action)StartNet);
		}

		private void StartNet()
		{
			Logger.Log("Creating parser");
			var parser = new ImageParser();
			Logger.Log("Loading training images");
			trainingImages = parser.LoadImages("Data/train-images.idx3-ubyte").ToList();
			validationImages = parser.LoadImages("Data/t10k-images.idx3-ubyte").ToList();
			Logger.Log("Attaching labels");
			parser.AttachLabels(trainingImages, "Data/train-labels.idx1-ubyte");
			parser.AttachLabels(validationImages, "Data/t10k-labels.idx1-ubyte");

			Logger.Log("Preparing images for training");
			foreach (var i in trainingImages)
			{
				i.PrepareForTraining();
			}
			foreach (var i in validationImages)
			{
				i.PrepareForTraining();
			}

			var first = trainingImages.First();

			Logger.Log("Building neural network");
			net = new Network(first.Width * first.Height, 10, 15);
			Logger.Log("Initialising network with random weights");
			net.InitialiseRandom();
			Logger.Log("Ready");
			Logger.Log("------------------------");
			Invoke(new Action(() =>
			{
				btnTrain.Enabled = true;
			}));
		}



		private async void btnTrain_Click(object sender, EventArgs e)
		{

			Logger.Log("Starting training...");
			btnTrain.Enabled = false;
			await Task.Run(() => net.StochasticGradientDescent(
				trainingImages,
				epochCount: (int)epochCount.Value,
				miniBatchSize: (int)miniBatchSize.Value,
				eta: (float)learningRate.Value,
				validationSet: validationImages.Cast<ITraining>().ToArray()));
			Logger.Log("Training finished");
			btnTrain.Enabled = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog
			{
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
				CheckFileExists = true
			};
			dlg.ShowDialog();

			var image = (Bitmap)Image.FromFile(dlg.FileName);

			if (image.Width * image.Height != net.InputLayer.Neurons.Count)
			{
				MessageBox.Show("Invalid image", "Image dimensions are incorrect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			pictureBox1.Image = image;

			var intensities = new List<float>();
			for (int y = 0; y < image.Height; y++)
			{
				for (int x = 0; x < image.Width; x++)
				{
					var pixel = image.GetPixel(x, y);
					var intensity = 255 - new[] { (float)pixel.R, pixel.G, pixel.B }.Average() / 255;
					intensities.Add(intensity);
				}
			}
			var img = new HandwritingImage(28, 28, intensities.Select(i => (byte) (i * 255)).ToArray());
			img.PrepareForTraining();

			net.Run(img.Inputs);

			var values = net.OutputLayer.Neurons.Select((n, i) => (neuron: n, number: i)).OrderByDescending(n => n.neuron.Value).ToList();

			var results = values.Where(v => v.neuron.Value > 0.5);
			if (results.Any())
			{
				resultLabel.Text = "I have some guesses about this number:\r\n"
					+ string.Join("\r\n", results.Select(r => $"{r.number}: {r.neuron.Value * 100:F}%"));
			}
			else
			{
				resultLabel.Text = "I am not confident enough to make a guess about this number.";
			}
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			var json = ns.Save(net);

			var dlg = new SaveFileDialog
			{
				AddExtension = true,
				DefaultExt = "json"
			};
			var result = dlg.ShowDialog();
			if (result == DialogResult.Cancel) return;

			File.WriteAllText(dlg.FileName, json);
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog
			{
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
				CheckFileExists = true
			};
			var result = dlg.ShowDialog();
			if (result == DialogResult.Cancel) return;
			var json = File.ReadAllText(dlg.FileName);
			net = ns.Load(json);
		}
	}
}
