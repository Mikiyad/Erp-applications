using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ContainerSizeData : DataSet
	{
		public const string CONTAINERSIZE_TABLE = "ContainerSize";

		public const string CONTAINERSIZEID_FIELD = "ContainerSizeID";

		public const string CONTAINERSIZENAME_FIELD = "ContainerSizeName";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ContainerSizeTable => base.Tables["ContainerSize"];

		public ContainerSizeData()
		{
			BuildDataTables();
		}

		public ContainerSizeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("ContainerSize");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ContainerSizeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ContainerSizeName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
