using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeProvisionData : DataSet
	{
		public const string EMPLOYEEPROVISION_TABLE = "Employee_Provision";

		public const string EMPLOYEEPROVISIONDETAIL_TABLE = "Employee_Provision_Detail";

		public const string PROVISIONTYPEID_FIELD = "ProvisionTypeID";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string SERVICEPERIOD_FIELD = "ServicePeriod";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string DUEAMOUNT_FIELD = "DueAmount";

		public const string POSTEDAMOUNT_FIELD = "Posted";

		public const string CURRENTAMOUNT_FIELD = "CurrentAmount";

		public DataTable EmployeeProvisionTable => base.Tables["Employee_Provision"];

		public DataTable EmployeeProvisionDetailTable => base.Tables["Employee_Provision_Detail"];

		public EmployeeProvisionData()
		{
			BuildDataTables();
		}

		public EmployeeProvisionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Provision");
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
			columns.Add("Note", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("ProvisionTypeID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Employee_Provision_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("ServicePeriod", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("DueAmount", typeof(decimal));
			columns.Add("Posted", typeof(decimal));
			columns.Add("CurrentAmount", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
