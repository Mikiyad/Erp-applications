using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class RequisitionData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REQUISITIONTYPEID_FIELD = "RequisitionTypeID";

		public const string JOBID_FIELD = "JobID";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string STATUS_FIELD = "Status";

		public const string EQUIPMENTCATEGORYID_FIELD = "EquipmentCategoryID";

		public const string EQUIPMENTID_FIELD = "EquipmentID";

		public const string REMARKS_FIELD = "Remarks";

		public const string REQUIREDON_FIELD = "RequiredOn";

		public const string REQUIREDTILL_FIELD = "RequiredTill";

		public const string REQUISITION_TABLE = "EA_Requisition";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable RequisitionTable => base.Tables["EA_Requisition"];

		public RequisitionData()
		{
			BuildDataTables();
		}

		public RequisitionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("EA_Requisition");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("RequisitionTypeID", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("EquipmentCategoryID", typeof(string));
			columns.Add("EquipmentID", typeof(string));
			columns.Add("RequiredOn", typeof(DateTime));
			columns.Add("RequiredTill", typeof(DateTime));
			columns.Add("Remarks", typeof(string));
			columns.Add("Status", typeof(byte));
			base.Tables.Add(dataTable);
		}
	}
}
