using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Micromind.Common.Libraries
{
	public class ApplicationLog
	{
		private static TraceSwitch debugSwitch;

		private static StreamWriter debugWriter;

		private static TraceLevel eventLogTraceLevel;

		public static void WriteError(string message)
		{
			WriteLog(TraceLevel.Error, message);
		}

		public static void WriteWarning(string message)
		{
			WriteLog(TraceLevel.Warning, message);
		}

		public static void WriteInfo(string message)
		{
			WriteLog(TraceLevel.Info, message);
		}

		public static void WriteTrace(string message)
		{
			WriteLog(TraceLevel.Verbose, message);
		}

		public static string FormatException(Exception ex, string catchInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (catchInfo != string.Empty)
			{
				stringBuilder.Append(catchInfo).Append("\r\n");
			}
			stringBuilder.Append(ex.Message).Append("\r\n").Append(ex.StackTrace);
			return stringBuilder.ToString();
		}

		private static void WriteLog(TraceLevel level, string messageText)
		{
			try
			{
				if (debugWriter != null && level <= debugSwitch.Level)
				{
					lock (debugWriter)
					{
						debugWriter.Flush();
					}
				}
				if (level <= eventLogTraceLevel)
				{
					EventLogEntryType type;
					switch (level)
					{
					case TraceLevel.Error:
						type = EventLogEntryType.Error;
						break;
					case TraceLevel.Warning:
						type = EventLogEntryType.Warning;
						break;
					case TraceLevel.Info:
						type = EventLogEntryType.Information;
						break;
					case TraceLevel.Verbose:
						type = EventLogEntryType.SuccessAudit;
						break;
					default:
						type = EventLogEntryType.SuccessAudit;
						break;
					}
					new EventLog("Application", ApplicationConfiguration.EventLogMachineName, ApplicationConfiguration.EventLogSourceName).WriteEntry(messageText, type);
				}
			}
			catch
			{
			}
		}

		static ApplicationLog()
		{
			Type typeFromHandle = typeof(ApplicationLog);
			try
			{
				if (!Monitor.TryEnter(typeFromHandle))
				{
					Monitor.Enter(typeFromHandle);
				}
				else
				{
					bool flag = true;
					try
					{
						if (ApplicationConfiguration.TracingEnabled)
						{
							string tracingTraceFile = ApplicationConfiguration.TracingTraceFile;
							if (tracingTraceFile != string.Empty)
							{
								string tracingSwitchName = ApplicationConfiguration.TracingSwitchName;
								if (tracingSwitchName != string.Empty)
								{
									debugWriter = new StreamWriter(new FileInfo(tracingTraceFile).Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite));
									Debug.Listeners.Add(new TextWriterTraceListener(debugWriter));
									debugSwitch = new TraceSwitch(tracingSwitchName, ApplicationConfiguration.TracingSwitchDescription);
									debugSwitch.Level = ApplicationConfiguration.TracingTraceLevel;
								}
								flag = false;
							}
						}
					}
					catch
					{
					}
					if (flag)
					{
						debugSwitch = null;
						debugWriter = null;
					}
					if (ApplicationConfiguration.EventLogEnabled)
					{
						eventLogTraceLevel = ApplicationConfiguration.EventLogTraceLevel;
					}
					else
					{
						eventLogTraceLevel = TraceLevel.Off;
					}
				}
			}
			finally
			{
				Monitor.Exit(typeFromHandle);
			}
		}

		public static void WriteImportLogError(StringBuilder errorBuilder)
		{
			if (errorBuilder.Length > 0)
			{
				WriteLogError(errorBuilder, "ImportList.log");
				EventLog.WriteEntry("Axolon Import List", "Error occured when importing list. Please see ImportList.log.", EventLogEntryType.Error);
			}
		}

		public static void WriteLogError(StringBuilder errorBuilder, string logFileName)
		{
			if (errorBuilder != null && errorBuilder.Length > 0)
			{
				if (logFileName == null || logFileName.Trim() == string.Empty)
				{
					logFileName = "Axolon.log";
				}
				logFileName = Path.GetFileName(logFileName);
				StreamWriter streamWriter = new StreamWriter(StoreConfiguration.OriginalDiectory + Path.DirectorySeparatorChar.ToString() + logFileName, append: false);
				streamWriter.Write(errorBuilder.ToString());
				streamWriter.Flush();
				streamWriter.Close();
			}
		}
	}
}
