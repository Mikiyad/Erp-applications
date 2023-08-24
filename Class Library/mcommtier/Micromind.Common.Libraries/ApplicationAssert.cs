using System;
using System.Diagnostics;
using System.Text;

namespace Micromind.Common.Libraries
{
	public class ApplicationAssert
	{
		public const int LineNumber = 0;

		[Conditional("DEBUG")]
		public static void Check(bool condition, string errorText, int lineNumber)
		{
			if (!condition)
			{
				string currentTrace = string.Empty;
				GenerateStackTrace(lineNumber, out currentTrace);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Assert: ").Append("\r\n").Append(errorText)
					.Append("\r\n")
					.Append(currentTrace);
				ApplicationLog.WriteWarning(stringBuilder.ToString());
			}
		}

		public static void CheckCondition(bool condition, string errorText, int lineNumber)
		{
			if (!condition)
			{
				GenerateStackTrace(lineNumber, out string _);
				throw new ApplicationException(errorText);
			}
		}

		private static void GenerateStackTrace(int lineNumber, out string currentTrace)
		{
			currentTrace = string.Empty;
		}
	}
}
