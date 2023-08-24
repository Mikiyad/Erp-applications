using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ControlLayoutData : DataSet
	{
		public const string CONTROLLAYOUT_TABLE = "Control_Layout";

		public const string CONTROLTYPE_FIELD = "ControlType";

		public const string LAYOUTNAME_FIELD = "LayoutName";

		public const string CONTROLNAME_FIELD = "ControlName";

		public const string LAYOUTDATA_FIELD = "LayoutData";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ControlLayoutTable => base.Tables["Control_Layout"];

		public ControlLayoutData()
		{
			BuildDataTables();
		}

		public ControlLayoutData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Control_Layout");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ControlType", typeof(byte));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("LayoutName", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataColumn2.AutoIncrement = false;
			DataColumn dataColumn3 = columns.Add("ControlName", typeof(string));
			dataColumn3.AllowDBNull = false;
			dataColumn3.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[3]
			{
				dataColumn,
				dataColumn2,
				dataColumn3
			};
			columns.Add("LayoutData", typeof(byte[])).AllowDBNull = false;
			base.Tables.Add(dataTable);
		}
	}
}
