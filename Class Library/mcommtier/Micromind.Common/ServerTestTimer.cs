using System;
using System.IO;
using System.Reflection;

namespace Micromind.Common
{
	public class ServerTestTimer
	{
		private DateTime startDate;

		private DateTime endDate;

		private string description = "";

		private string fileName = "Axolon.log";

		private static bool LogInRelease;

		public ServerTestTimer()
		{
			Start();
		}

		public ServerTestTimer(string description)
		{
			this.description = description;
		}

		public ServerTestTimer(string description, string logFileName)
		{
			fileName = logFileName;
			this.description = description;
		}

		public DateTime Start()
		{
			startDate = DateTime.Now;
			return startDate;
		}

		public void LogDuration(string logName, bool append)
		{
			string text = "";
			text = ((fileName.IndexOf(Path.PathSeparator) < 0) ? fileName : fileName);
			StreamWriter streamWriter = new StreamWriter(text, append);
			streamWriter.WriteLine();
			streamWriter.Write(logName + " : ");
			endDate = DateTime.Now;
			TimeSpan timeSpan = endDate - startDate;
			string value = "Seconds: " + timeSpan.Seconds.ToString() + " MiliSeconds: " + timeSpan.Milliseconds.ToString();
			streamWriter.Write(value);
			streamWriter.Flush();
			streamWriter.Close();
		}

		public void LogDuration()
		{
			if (LogInRelease)
			{
				string text = "";
				text = ((fileName.IndexOf(Path.PathSeparator) < 0) ? (Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\" + fileName) : fileName);
				StreamWriter streamWriter = new StreamWriter(text, append: true);
				streamWriter.WriteLine("");
				endDate = DateTime.Now;
				TimeSpan timeSpan = endDate - startDate;
				string value = "Minute: " + timeSpan.Minutes.ToString() + "\nSeconds: " + timeSpan.Seconds.ToString() + "\nMiliSeconds: " + timeSpan.Milliseconds.ToString() + "\nTicks: " + timeSpan.Ticks.ToString();
				streamWriter.WriteLine("NEW LOG:");
				streamWriter.WriteLine(description);
				streamWriter.WriteLine(value);
				streamWriter.WriteLine("******************************************************");
				streamWriter.Flush();
				streamWriter.Close();
			}
		}

		public TimeSpan ShowDuration()
		{
			return TimeSpan.MinValue;
		}
	}
}
