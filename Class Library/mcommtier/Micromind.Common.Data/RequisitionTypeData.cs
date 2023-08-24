using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class RequisitionTypeData : DataSet
	{
		public const string TYPEID_FIELD = "RequisitionTypeID";

		public const string TYPENAME_FIELD = "RequisitionTypeName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string REQUISITIONTYPE_TABLE = "EA_Requisition_Type";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable RequisitionTypeTable => base.Tables["EA_Requisition_Type"];

		public RequisitionTypeData()
		{
			BuildDataTables();
		}

		public RequisitionTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("EA_Requisition_Type");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("RequisitionTypeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("RequisitionTypeName", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
