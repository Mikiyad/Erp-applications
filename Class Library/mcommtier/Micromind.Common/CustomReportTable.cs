using System;

namespace Micromind.Common
{
	[Serializable]
	public class CustomReportTable
	{
		public string query = "";

		public string tableName = "";

		public string TableName
		{
			get
			{
				return tableName;
			}
			set
			{
				tableName = value;
			}
		}

		public override string ToString()
		{
			return tableName;
		}
	}
}
