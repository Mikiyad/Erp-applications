using System;

namespace Micromind.Common
{
	[Serializable]
	public class ReportRelation
	{
		public string RelationName = "";

		public string ParentTableName = "";

		public string ChildTableName = "";

		public string[] ParentColumns;

		public string[] ChildColumns;

		public override string ToString()
		{
			return RelationName;
		}
	}
}
