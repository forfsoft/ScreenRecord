using Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenRecord
{
	public class ConsoleCommand
	{
		private Instance _instance = null;

		public bool Start(string binaryPath, string args, bool async = true, EventHandler<(DataType type, string data)> outputData = null)
		{
			if (_instance?.Started == true)
			{
				return false;
			}
			_instance = new Instance(binaryPath, args);
			_instance.DataReceived += outputData;

			if (true == async)
			{
				Task.WhenAll(_instance.FinishedRunning().ContinueWith(o =>
				{
				}));
			}
			else
			{
				Task.WaitAll(_instance.FinishedRunning().ContinueWith(o =>
				{
				}));
			}
			return true;
		}

		public void SendInput(string text)
		{
			if (_instance?.Started == false)
			{
				return;
			}
			_instance.SendInput(text);
		}
	}
}
