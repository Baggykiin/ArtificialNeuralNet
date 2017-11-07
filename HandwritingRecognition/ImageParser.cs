using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingRecognition
{
	class ImageParser
	{
		private const int LabelFileMagicNumber = 2049;
		private const int ImageFileMagicNumber = 2051;

		public IEnumerable<HandwritingImage> LoadImages(string imageFilePath)
		{
			var bytes = File.ReadAllBytes(imageFilePath);

			var offset = 0;
			var magicNumber = ReadInt(bytes, ref offset);
			var imgCount = ReadInt(bytes, ref offset);
			var rowCount = ReadInt(bytes, ref offset);
			var colCount = ReadInt(bytes, ref offset);
			
			if(magicNumber != ImageFileMagicNumber) throw new InvalidOperationException("Data is not in expected format");

			for (var i = 0; i < imgCount; i++)
			{
				var imageData = new byte[rowCount * colCount];
				Console.WriteLine("");
				Array.Copy(bytes, offset + (i * imageData.Length), imageData, 0, imageData.Length);
				
				yield return new HandwritingImage(colCount, rowCount, imageData);
			}
		}

		private int ReadInt(byte[] array, ref int offset)
		{
			return array[offset++] << 24 | array[offset++] << 16 | array[offset++] << 8 | array[offset++];
		}

		public void AttachLabels(List<HandwritingImage> images, string labelFilePath)
		{
			var bytes = File.ReadAllBytes(labelFilePath);

			var offset = 0;
			var magicNumber = ReadInt(bytes, ref offset);
			var itemCount = ReadInt(bytes, ref offset);

			if (magicNumber != LabelFileMagicNumber) throw new InvalidOperationException("Data is not in expected format");

			if(images.Count != itemCount) throw new InvalidOperationException("Number of images and number of labels does not match");

			for (var i = 0; i < itemCount; i++)
			{
				int label = bytes[offset + i];
				images[i].WrittenNumber = label;
				;
			}
		}
	}
}
