namespace Micromind.DataControls
{
	public class ConditionColumn
	{
		public string ColumnName
		{
			get;
			set;
		}

		public string DisplayText
		{
			get;
			set;
		}

		public ConditionColumn()
		{
		}

		public ConditionColumn(string columnName, string displayText)
		{
			ColumnName = columnName;
			DisplayText = displayText;
		}

		public override string ToString()
		{
			return DisplayText;
		}
	}
}
