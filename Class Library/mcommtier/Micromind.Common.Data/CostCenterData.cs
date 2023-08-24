using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CostCenterData : DataSet
	{
		public const string COSTCENTER_TABLE = "Cost_Center";

		public const string COSTCENTERID_FIELD = "CostCenterID";

		public const string COSTCENTERNAME_FIELD = "CostCenterName";

		public const string CASHRECEIPTACCOUNTID_FIELD = "CashReceiptAccountID";

		public const string PDCRECEIPTACCOUNTID_FIELD = "PDCReceiptAccountID";

		public const string CHEQUERECEIPTSYSDOCID_FIELD = "ChequeReceiptSysDocID";

		public const string CASHRECEIPTSYSDOCID_FIELD = "CashReceiptSysDocID";

		public const string CHEQUEPAYMENTSYSDOCID_FIELD = "ChequePaymentSysDocID";

		public const string CASHPAYMENTSYSDOCID_FIELD = "CashPaymentSysDocID";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CostCenterTable => base.Tables["Cost_Center"];

		public CostCenterData()
		{
			BuildDataTables();
		}

		public CostCenterData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Cost_Center");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CostCenterID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CostCenterName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
