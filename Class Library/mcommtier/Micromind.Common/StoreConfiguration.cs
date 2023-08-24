using Micromind.Common.Libraries;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;

using System.Threading;
using System.Xml;

namespace Micromind.Common
{
	
	public class StoreConfiguration : IConfigurationSectionHandler
	{
		private static CultureInfo currentCulture = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);

		private static DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();

		private const string WEB_ENABLEPAGECACHE = "Duwamish.Web.EnablePageCache";

		private const string WEB_PAGECACHEEXPIRESINSECONDS = "Duwamish.Web.PageCacheExpiresInSeconds";

		private const string DATAACCESS_CONNECTIONSTRING = "Duwamish.DataAccess.ConnectionString";

		private const string WEB_ENABLESSL = "Duwamish.Web.EnableSsl";

		private static string dbConnectionString;

		private static bool enablePageCache;

		private static int pageCacheExpiresInSeconds;

		private static bool enableSsl;

		private const bool WEB_ENABLEPAGECACHE_DEFAULT = true;

		private const int WEB_PAGECACHEEXPIRESINSECONDS_DEFAULT = 3600;

		private const string DATAACCESS_CONNECTIONSTRING_DEFAULT = "User ID = ; Password = ;server=;Integrated Security=SSPI;database=";

		private const bool WEB_ENABLESSL_DEFAULT = false;

		private static bool isActivityLogEnabled = true;

		public static bool EnablePageCache => enablePageCache;

		public static int PageCacheExpiresInSeconds => pageCacheExpiresInSeconds;

		public static string ConnectionString => "User ID = ; Password = ;server=;Integrated Security=SSPI;database=";

		public static bool EnableSsl => enableSsl;

		public static CultureInfo CurrentCulture
		{
			get
			{
				dateFormat.LongDatePattern = "yyyy-mm-dd hh:mm:ss";
				currentCulture.DateTimeFormat = dateFormat;
				currentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
				currentCulture.NumberFormat.NumberDecimalSeparator = ".";
				return currentCulture;
			}
		}

		public static string OriginalDiectory
		{
			get
			{
				string fileName = Process.GetCurrentProcess().MainModule.FileName;
				int length = fileName.LastIndexOf("\\");
				return fileName.Substring(0, length);
			}
		}

		public static DateTime LongDBDateTimeMin => SqlDateTime.MinValue.Value;

		public static DateTime LongDBDateTimeMax => SqlDateTime.MaxValue.Value;

		public static DateTime SmallDateTimeMin => new DateTime(1900, 1, 1);

		public static DateTime SmallDateTimeMax => new DateTime(2079, 6, 6);

		public static bool IsActivityLogEnabled
		{
			get
			{
				return isActivityLogEnabled;
			}
			set
			{
				isActivityLogEnabled = value;
			}
		}

		public static int NumberRoundDigit => 5;

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
				dbConnectionString = "User ID = ; Password = ;server=;Integrated Security=SSPI;database=";
				pageCacheExpiresInSeconds = 3600;
				enablePageCache = true;
				enableSsl = false;
			}
			else
			{
				dbConnectionString = ApplicationConfiguration.ReadSetting(nameValueCollection, "Duwamish.DataAccess.ConnectionString", "User ID = ; Password = ;server=;Integrated Security=SSPI;database=");
				pageCacheExpiresInSeconds = ApplicationConfiguration.ReadSetting(nameValueCollection, "Duwamish.Web.PageCacheExpiresInSeconds", 3600);
				enablePageCache = ApplicationConfiguration.ReadSetting(nameValueCollection, "Duwamish.Web.EnablePageCache", defaultValue: true);
				enableSsl = ApplicationConfiguration.ReadSetting(nameValueCollection, "Duwamish.Web.EnableSsl", defaultValue: false);
			}
			return nameValueCollection;
		}

		public static string GetPatchFileName()
		{
			return "PXF196.xml";
		}

		public static bool IsClient()
		{
			if (File.Exists(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + Path.DirectorySeparatorChar.ToString() + "AxolonServer.exe"))
			{
				return false;
			}
			return true;
		}

		public static void GetSqlDateTime(ref DateTime minDate, ref DateTime maxDate, SqlDateTimeTypes dateTimeType)
		{
			if (dateTimeType == SqlDateTimeTypes.DateTime && minDate < LongDBDateTimeMin)
			{
				minDate = LongDBDateTimeMin;
			}
			else if (dateTimeType == SqlDateTimeTypes.SmallDateTime && minDate < SmallDateTimeMin)
			{
				minDate = SmallDateTimeMin;
			}
			if (dateTimeType == SqlDateTimeTypes.DateTime && maxDate > LongDBDateTimeMax)
			{
				maxDate = LongDBDateTimeMax;
			}
			else if (dateTimeType == SqlDateTimeTypes.SmallDateTime && maxDate > SmallDateTimeMax)
			{
				maxDate = SmallDateTimeMax;
			}
		}

		public static DateTime GetSqlDateTime(DateTime date, bool isMaxDate, SqlDateTimeTypes dateTimeType)
		{
			if (dateTimeType == SqlDateTimeTypes.DateTime && !isMaxDate && date < LongDBDateTimeMin)
			{
				return LongDBDateTimeMin;
			}
			if (dateTimeType == SqlDateTimeTypes.SmallDateTime && !isMaxDate && date < SmallDateTimeMin)
			{
				return SmallDateTimeMin;
			}
			if (dateTimeType == SqlDateTimeTypes.DateTime && isMaxDate && date > LongDBDateTimeMax)
			{
				return LongDBDateTimeMax;
			}
			if (dateTimeType == SqlDateTimeTypes.SmallDateTime && isMaxDate && date > SmallDateTimeMax)
			{
				return SmallDateTimeMax;
			}
			return date;
		}

		public static DateTime GetSqlDateTime(DateTime date, SqlDateTimeTypes dateTimeType)
		{
			if (dateTimeType == SqlDateTimeTypes.DateTime && date == DateTime.MinValue && date < LongDBDateTimeMin)
			{
				return LongDBDateTimeMin;
			}
			if (dateTimeType == SqlDateTimeTypes.SmallDateTime && date == DateTime.MinValue && date < SmallDateTimeMin)
			{
				return SmallDateTimeMin;
			}
			if (dateTimeType == SqlDateTimeTypes.DateTime && date == DateTime.MaxValue && date > LongDBDateTimeMax)
			{
				return LongDBDateTimeMax;
			}
			if (dateTimeType == SqlDateTimeTypes.SmallDateTime && date == DateTime.MaxValue && date > SmallDateTimeMax)
			{
				return SmallDateTimeMax;
			}
			return date;
		}

		public static string ToSqlDateTimeString(DateTime date)
		{
			return date.Month + "-" + date.Day + "-" + date.Year + " " + date.Hour + ":" + date.Minute + ":" + date.Second;
		}
	}
}
