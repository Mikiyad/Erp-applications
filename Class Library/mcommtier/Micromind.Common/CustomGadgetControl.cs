using System;

namespace Micromind.Common
{
	[Serializable]
	public class CustomGadgetControl
	{
		public string ControlName = "";

		public string DisplayText = "";

		public CRCTypes ControlType;

		public byte ValueType = 1;

		public string FieldName = "";

		public string Key = "";

		public int OrderIndex;

		public override string ToString()
		{
			return ControlName + " - " + ControlType.ToString();
		}
	}
}
