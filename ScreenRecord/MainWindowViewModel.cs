using Instances;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScreenRecord
{
	public class MainWindowViewModel : ViewModelProperty
	{
		private ConsoleCommand _consoleCommand = new ConsoleCommand();

		private void startRecord()
		{
			string binaryPath = @"ffmpeg.exe";
			if (File.Exists(binaryPath) == false)
			{
				MessageBox.Show("ffmpeg.exe not found!");
				return;
			}
			
			string args = "-y -rtbufsize 100M -f gdigrab -framerate 30 -probesize 10M -draw_mouse 1 -i desktop -c:v libx264 -r 30 -preset ultrafast -tune zerolatency -crf 25 -pix_fmt yuv420p \"output.mp4\"";
			_consoleCommand?.Start(binaryPath, args, true, consoleOutputData);
		}

		private void stopRecord()
		{
			_consoleCommand?.SendInput("q");
		}

		private void consoleOutputData(object sender, (DataType type, string data) msg)
		{
			Console.WriteLine(msg.data);
		}

		#region record start command
		private RelayCommand _recordStartCommand;
		public RelayCommand RecordStartCommand => _recordStartCommand ??= new RelayCommand(onRecordStartCommand, canRecordStartCommand);
		private bool canRecordStartCommand(object obj)
		{
			return true;
		}

		private void onRecordStartCommand(object obj)
		{
			startRecord();
		}
		#endregion


		#region record stop command
		private RelayCommand _recordStopCommand;
		public RelayCommand RecordStopCommand => _recordStopCommand ??= new RelayCommand(onRecordStopCommand, canRecordStopCommand);
		private bool canRecordStopCommand(object obj)
		{
			return true;
		}

		private void onRecordStopCommand(object obj)
		{
			stopRecord();
		}
		#endregion
	}
}
