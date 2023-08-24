using System;

namespace Micromind.Common
{
	[Serializable]
	public class CustomGadgetTable
	{
		public string query = "";

		public string tableName = "";

		public string TableName => tableName;

		public override string ToString()
		{
			return tableName;
		}
	}
}
