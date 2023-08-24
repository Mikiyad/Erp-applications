using System;
using System.Globalization;

namespace Micromind.Utilities.FTP.Ftp
{
	public class WindowsFileParser : FTPFileParser
	{
		private static readonly string format = "MM'-'dd'-'yy hh':'mmtt";

		private const string DIR = "<DIR>";

		private char[] sep = new char[1]
		{
			' '
		};

		private const int MIN_EXPECTED_FIELD_COUNT = 4;

		public override FTPFile Parse(string raw)
		{
			string[] array = Split(raw);
			if (array.Length < 4)
			{
				throw new FormatException("Unexpected number of fields: " + array.Length);
			}
			DateTime lastModified = DateTime.ParseExact(array[0] + " " + array[1], format, CultureInfo.CurrentCulture.DateTimeFormat);
			bool isDir = false;
			long size = 0L;
			if (array[2].ToUpper().Equals("<DIR>".ToUpper()))
			{
				isDir = true;
			}
			else
			{
				try
				{
					size = long.Parse(array[2]);
				}
				catch (FormatException)
				{
					throw new FormatException("Failed to parse size: " + array[2]);
				}
			}
			int num = 0;
			bool flag = true;
			for (int i = 0; i < 3; i++)
			{
				num = raw.IndexOf(array[i], num);
				if (num < 0)
				{
					flag = false;
					break;
				}
				num += array[i].Length;
			}
			if (flag)
			{
				string name = raw.Substring(num).Trim();
				return new FTPFile(0, raw, name, size, isDir, ref lastModified);
			}
			throw new FormatException("Failed to retrieve name: " + raw);
		}
	}
}
