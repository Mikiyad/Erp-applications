using System.Globalization;
using System.Resources;

namespace Micromind.ClientUI
{
	internal sealed class SR
	{
		private static SR loader;

		private ResourceManager resources;

		private SR()
		{
			string baseName = "ClientUI";
			resources = new ResourceManager(baseName, GetType().Assembly);
		}

		private static SR GetLoader()
		{
			if (loader == null)
			{
				lock (typeof(SR))
				{
					if (loader == null)
					{
						loader = new SR();
					}
				}
			}
			return loader;
		}

		public static string GetString(string name, string defaultStr)
		{
			string @string = GetString((CultureInfo)null, name);
			if (@string != string.Empty)
			{
				return @string;
			}
			return defaultStr;
		}

		public static string GetString(string name)
		{
			return name;
		}

		public static string GetString(string name, params object[] args)
		{
			return GetString(null, name, args);
		}

		public static string GetString(CultureInfo culture, string name)
		{
			return GetString(culture, name, null);
		}

		public static string GetString(CultureInfo culture, string name, params object[] args)
		{
			return GetStringHelper(culture, name, args);
		}

		private static string GetStringHelper(CultureInfo culture, string name, params object[] args)
		{
			string text = null;
			if (text == null)
			{
				SR sR = GetLoader();
				if (sR == null)
				{
					return string.Empty;
				}
				sR.resources.IgnoreCase = true;
				text = sR.resources.GetString(name, culture);
				if (text == null)
				{
					text = string.Empty;
				}
			}
			if (args != null && args.Length != 0 && text.Length > 2)
			{
				try
				{
					text = string.Format(null, text, args);
					return text;
				}
				catch
				{
					return text;
				}
			}
			return text;
		}

		public static string GetUncustomizedString(string name)
		{
			return GetUncustomizedString(null, name);
		}

		public static string GetUncustomizedString(string name, params object[] args)
		{
			return GetUncustomizedString(null, name, args);
		}

		public static string GetUncustomizedString(CultureInfo culture, string name)
		{
			return GetUncustomizedString(culture, name, null);
		}

		public static string GetUncustomizedString(CultureInfo culture, string name, params object[] args)
		{
			return GetStringHelper(culture, name, args);
		}

		public static object GetObject(string name)
		{
			return GetObject(null, name);
		}

		public static object GetObject(CultureInfo culture, string name)
		{
			return GetLoader()?.resources.GetObject(name, culture);
		}
	}
}
