using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Xml;

namespace Micromind.Common.Libraries
{
	public class ApplicationConfiguration : IConfigurationSectionHandler
	{
		private const string TRACING_ENABLED = "SystemFramework.Tracing.Enabled";

		private const string TRACING_TRACEFILE = "SystemFramework.Tracing.TraceFile";

		private const string TRACING_TRACELEVEL = "SystemFramework.Tracing.TraceLevel";

		private const string TRACING_SWITCHNAME = "SystemFramework.Tracing.SwitchName";

		private const string TRACING_SWITCHDESCRIPTION = "SystemFramework.Tracing.SwitchDescription";

		private const string EVENTLOG_ENABLED = "SystemFramework.EventLog.Enabled";

		private const string EVENTLOG_MACHINENAME = "SystemFramework.EventLog.Machine";

		private const string EVENTLOG_SOURCENAME = "SystemFramework.EventLog.SourceName";

		private const string EVENTLOG_TRACELEVEL = "SystemFramework.EventLog.LogLevel";

		private static bool tracingEnabled;

		private static string tracingTraceFile;

		private static TraceLevel tracingTraceLevel;

		private static string tracingSwitchName;

		private static string tracingSwitchDescription;

		private static bool eventLogEnabled;

		private static string eventLogMachineName;

		private static string eventLogSourceName;

		private static TraceLevel eventLogTraceLevel;

		private static string appRoot;

		private const bool TRACING_ENABLED_DEFAULT = true;

		private const string TRACING_TRACEFILE_DEFAULT = "ApplicationTrace.txt";

		private const TraceLevel TRACING_TRACELEVEL_DEFAULT = TraceLevel.Verbose;

		private const string TRACING_SWITCHNAME_DEFAULT = "ApplicationTraceSwitch";

		private const string TRACING_SWITCHDESCRIPTION_DEFAULT = "Application error and tracing information";

		private const bool EVENTLOG_ENABLED_DEFAULT = true;

		private const string EVENTLOG_MACHINENAME_DEFAULT = ".";

		private const string EVENTLOG_SOURCENAME_DEFAULT = "WebApplication";

		private const TraceLevel EVENTLOG_TRACELEVEL_DEFAULT = TraceLevel.Error;

		public static string AppRoot => appRoot;

		public static bool TracingEnabled => tracingEnabled;

		public static string TracingTraceFile => appRoot + "\\" + tracingTraceFile;

		public static TraceLevel TracingTraceLevel => tracingTraceLevel;

		public static string TracingSwitchName => tracingSwitchName;

		public static string TracingSwitchDescription => tracingSwitchDescription;

		public static bool EventLogEnabled => eventLogEnabled;

		public static string EventLogMachineName => eventLogMachineName;

		public static string EventLogSourceName => eventLogSourceName;

		public static TraceLevel EventLogTraceLevel => eventLogTraceLevel;

		public object Create(object parent, object configContext, XmlNode section)
		{
			NameValueCollection nameValueCollection;
			try
			{
				nameValueCollection = (NameValueCollection)new NameValueSectionHandler().Create(parent, configContext, section);
			}
			catch
			{
				nameValueCollection = null;
			}
			if (nameValueCollection == null)
			{
				tracingEnabled = true;
				tracingTraceFile = "ApplicationTrace.txt";
				tracingTraceLevel = TraceLevel.Verbose;
				tracingSwitchName = "ApplicationTraceSwitch";
				tracingSwitchDescription = "Application error and tracing information";
				eventLogEnabled = true;
				eventLogMachineName = ".";
				eventLogSourceName = "WebApplication";
				eventLogTraceLevel = TraceLevel.Error;
			}
			else
			{
				tracingEnabled = ReadSetting(nameValueCollection, "SystemFramework.Tracing.Enabled", defaultValue: true);
				tracingTraceFile = ReadSetting(nameValueCollection, "SystemFramework.Tracing.TraceFile", "ApplicationTrace.txt");
				tracingTraceLevel = ReadSetting(nameValueCollection, "SystemFramework.Tracing.TraceLevel", TraceLevel.Verbose);
				tracingSwitchName = ReadSetting(nameValueCollection, "SystemFramework.Tracing.SwitchName", "ApplicationTraceSwitch");
				tracingSwitchDescription = ReadSetting(nameValueCollection, "SystemFramework.Tracing.SwitchDescription", "Application error and tracing information");
				eventLogEnabled = ReadSetting(nameValueCollection, "SystemFramework.EventLog.Enabled", defaultValue: true);
				eventLogMachineName = ReadSetting(nameValueCollection, "SystemFramework.EventLog.Machine", ".");
				eventLogSourceName = ReadSetting(nameValueCollection, "SystemFramework.EventLog.SourceName", "WebApplication");
				eventLogTraceLevel = ReadSetting(nameValueCollection, "SystemFramework.EventLog.LogLevel", TraceLevel.Error);
			}
			return null;
		}

		public static string ReadSetting(NameValueCollection settings, string key, string defaultValue)
		{
			try
			{
				object obj = settings[key];
				return (obj == null) ? defaultValue : ((string)obj);
			}
			catch
			{
				return defaultValue;
			}
		}

		public static bool ReadSetting(NameValueCollection settings, string key, bool defaultValue)
		{
			try
			{
				object obj = settings[key];
				return (obj == null) ? defaultValue : Convert.ToBoolean((string)obj);
			}
			catch
			{
				return defaultValue;
			}
		}

		public static int ReadSetting(NameValueCollection settings, string key, int defaultValue)
		{
			try
			{
				object obj = settings[key];
				return (obj == null) ? defaultValue : Convert.ToInt32((string)obj);
			}
			catch
			{
				return defaultValue;
			}
		}

		public static TraceLevel ReadSetting(NameValueCollection settings, string key, TraceLevel defaultValue)
		{
			try
			{
				object obj = settings[key];
				return (TraceLevel)((obj == null) ? ((int)defaultValue) : Convert.ToInt32((string)obj));
			}
			catch
			{
				return defaultValue;
			}
		}

		public static void OnApplicationStart(string myAppPath)
		{
			appRoot = myAppPath;
		}
	}
}
