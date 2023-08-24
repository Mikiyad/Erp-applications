using System;

namespace Micromind.Common.Data
{
	[Serializable]
	public class DoubleString
	{
		public string FirstString = "";

		public string SecondString = "";

		public DoubleString(string firstString, string secondString)
		{
			FirstString = firstString;
			SecondString = secondString;
		}
	}
}
