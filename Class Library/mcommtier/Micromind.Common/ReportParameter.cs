using System;

namespace Micromind.Common
{
	[Serializable]
	public class ReportParameter
	{
		public string ParameterName = "";

		public int ParameterType = 1;

		public override string ToString()
		{
			string str = ParameterName + " - ";
			if (ParameterType == 2)
			{
				return str + "(DateTime)";
			}
			if (ParameterType == 3)
			{
				return str + "(Numeric)";
			}
			return str + "(String)";
		}
	}
}
