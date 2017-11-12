using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandwritingRecognition
{
	class Logger
	{
		public static List<string> Messages { get; } = new List<string>();

		public static event Action<string> OnLog;

		public static void Log(string message)
		{
			Messages.Add(message);
			OnLog?.Invoke(message);
		}
	}
}
