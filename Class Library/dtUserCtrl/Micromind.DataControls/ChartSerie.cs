using System;

namespace Micromind.DataControls
{
	[Serializable]
	public class ChartSerie
	{
		public string SerieName = "";

		public string ValueColumn = "";

		public int ChartType = 1;

		public string DisplayName = "";

		public int Color = -1;

		public bool LabelVisible;

		public int LabelPosition = -1;

		public string LabelTextPattern = "";

		public ChartSerie(string name, string valueColumn, int type, string title)
		{
			SerieName = name;
			ValueColumn = valueColumn;
			ChartType = type;
			DisplayName = title;
		}
	}
}
