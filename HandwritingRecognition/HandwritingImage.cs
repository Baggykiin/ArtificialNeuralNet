using System.Collections.Generic;
using System.Linq;
using ArtificialNeuralNetwork;

namespace HandwritingRecognition
{
	class HandwritingImage : ITraining
	{
		public int Width { get; set; }
		public int Height { get; set; }
		public byte[] Data { get; set; }

		public int WrittenNumber { get; set; }

		public List<float> Inputs { get; private set; }
		public List<float> DesiredOutput { get; private set; }

		public string Example => string.Concat(Data.Select((pix, i) => DrawPixel(pix).ToString() + (i % Width == Width - 1 ? "\n" : "")));

		public HandwritingImage(int width, int height, byte[] data)
		{
			Width = width;
			Height = height;
			Data = data;
		}

		public void PrepareForTraining()
		{
			Inputs = Data.Select(b => b / 255.0f).ToList();
			DesiredOutput = new float[10].ToList();
			DesiredOutput[WrittenNumber] = 1.0f;
		}

		private char DrawPixel(byte pixel)
		{
			if(pixel <= 64) return '░';
			if (pixel <= 128) return '▒';
			if (pixel < 192) return '▓';
			return '█';
		}

		public override string ToString()
		{
			return $"HandwritingImage({WrittenNumber})";
		}
	}
}