using System;

namespace Micromind.Common
{
	[Serializable]
	public class CustomReportControl
	{
		public string ControlName = "";

		public string DisplayText = "";

		public CRCTypes ControlType;

		public byte ValueType = 1;

		public string FieldName = "";

		public string Key = "";

		public string FieldName1 = "";

		public string Key1 = "";

		public string Query = "";

		public int OrderIndex;

		public override string ToString()
		{
			return ControlName + " - " + ControlType.ToString();
		}
	}
}
