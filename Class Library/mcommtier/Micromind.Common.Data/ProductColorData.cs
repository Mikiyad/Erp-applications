using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductColorData : DataSet
	{
		public const string COLORID_FIELD = "ColorID";

		public const string NAME_FIELD = "Name";

		public const string DESCRIPTION_FIELD = "Description";

		public const string NOTE_FIELD = "Note";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string PRODUCTCOLORS_TABLE = "[Product Colors]";

		public DataTable ProductColorTable => base.Tables["[Product Colors]"];

		public ProductColorData()
		{
			BuildDataTables();
		}

		public ProductColorData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Product Colors]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ColorID", typeof(short));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Name", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
