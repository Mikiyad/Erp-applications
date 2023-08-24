using System.Collections;
using System.Text;

namespace Micromind.Utilities.FTP.Ftp
{
	public abstract class FTPFileParser
	{
		private const int MAX_FIELDS = 20;

		public abstract FTPFile Parse(string raw);

		protected string[] Split(string str)
		{
			ArrayList arrayList = new ArrayList();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in str)
			{
				if (!char.IsWhiteSpace(c))
				{
					stringBuilder.Append(c);
				}
				else if (stringBuilder.Length > 0)
				{
					arrayList.Add(stringBuilder.ToString());
					stringBuilder.Length = 0;
				}
			}
			if (stringBuilder.Length > 0)
			{
				arrayList.Add(stringBuilder.ToString());
			}
			return (string[])arrayList.ToArray(typeof(string));
		}
	}
}
