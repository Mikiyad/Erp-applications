using System;
using System.Globalization;
using System.Text;

namespace Micromind.Utilities.FTP.Ftp
{
	public class UnixFileParser : FTPFileParser
	{
		private const string SYMLINK_ARROW = "->";

		private const char SYMLINK_CHAR = 'l';

		private const char ORDINARY_FILE_CHAR = '-';

		private const char DIRECTORY_CHAR = 'd';

		private const string format1a = "MMM'-'d'-'yyyy";

		private const string format1b = "MMM'-'dd'-'yyyy";

		private string[] format1 = new string[2]
		{
			"MMM'-'d'-'yyyy",
			"MMM'-'dd'-'yyyy"
		};

		private const string format2a = "MMM'-'d'-'yyyy'-'HH':'mm";

		private const string format2b = "MMM'-'dd'-'yyyy'-'HH':'mm";

		private string[] format2 = new string[2]
		{
			"MMM'-'d'-'yyyy'-'HH':'mm",
			"MMM'-'dd'-'yyyy'-'HH':'mm"
		};

		private const int MIN_FIELD_COUNT = 8;

		public override FTPFile Parse(string raw)
		{
			char c = raw[0];
			if (c != '-' && c != 'd' && c != 'l')
			{
				return null;
			}
			string[] array = Split(raw);
			if (array.Length < 8)
			{
				StringBuilder stringBuilder = new StringBuilder("Unexpected number of fields in listing '");
				stringBuilder.Append(raw).Append("' - expected minimum ").Append(8)
					.Append(" fields but found ")
					.Append(array.Length)
					.Append(" fields");
				throw new FormatException(stringBuilder.ToString());
			}
			int num = 0;
			string text = array[num++];
			c = text[0];
			bool isDir = false;
			bool flag = false;
			switch (c)
			{
			case 'd':
				isDir = true;
				break;
			case 'l':
				flag = true;
				break;
			}
			int linkCount = 0;
			if (array.Length > 8)
			{
				try
				{
					linkCount = int.Parse(array[num++]);
				}
				catch (FormatException)
				{
				}
			}
			string owner = array[num++];
			string group = array[num++];
			long num2 = 0L;
			string text2 = array[num++];
			try
			{
				num2 = long.Parse(text2);
			}
			catch (FormatException)
			{
				throw new FormatException("Failed to parse size: " + text2);
			}
			int num3 = num;
			StringBuilder stringBuilder2 = new StringBuilder(array[num++]);
			stringBuilder2.Append('-').Append(array[num++]).Append('-');
			string text3 = array[num++];
			DateTime lastModified;
			if (text3.IndexOf(':') < 0)
			{
				stringBuilder2.Append(text3);
				lastModified = DateTime.ParseExact(stringBuilder2.ToString(), format1, CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.None);
			}
			else
			{
				int year = CultureInfo.CurrentCulture.Calendar.GetYear(DateTime.Now);
				stringBuilder2.Append(year).Append('-').Append(text3);
				lastModified = DateTime.ParseExact(stringBuilder2.ToString(), format2, CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.None);
				if (lastModified > DateTime.Now)
				{
					lastModified.AddYears(-1);
				}
			}
			string text4 = null;
			string linkedName = null;
			int num4 = 0;
			bool flag2 = true;
			for (int i = num3; i < num3 + 3; i++)
			{
				num4 = raw.IndexOf(array[i], num4);
				if (num4 < 0)
				{
					flag2 = false;
					break;
				}
				num4 += array[i].Length;
			}
			if (flag2)
			{
				string text5 = raw.Substring(num4).Trim();
				if (!flag)
				{
					text4 = text5;
				}
				else
				{
					num4 = text5.IndexOf("->");
					if (num4 <= 0)
					{
						text4 = text5;
					}
					else
					{
						int length = "->".Length;
						text4 = text5.Substring(0, num4).Trim();
						if (num4 + length < text5.Length)
						{
							linkedName = text5.Substring(num4 + length);
						}
					}
				}
				return new FTPFile(1, raw, text4, num2, isDir, ref lastModified)
				{
					Group = group,
					Owner = owner,
					Link = flag,
					LinkCount = linkCount,
					LinkedName = linkedName,
					Permissions = text
				};
			}
			throw new FormatException("Failed to retrieve name: " + raw);
		}
	}
}
