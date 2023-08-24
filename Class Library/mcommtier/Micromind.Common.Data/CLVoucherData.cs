using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CLVoucherData : DataSet
	{
		public const string CLVOUCHER_TABLE = "CL_Voucher";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string VOUCHERDATE_FIELD = "VoucherDate";

		public const string NOTE_FIELD = "Note";

		public const string REFERENCE_FIELD = "Reference";

		public const string VALIDFROM_FIELD = "ValidFrom";

		public const string VALIDTO_FIELD = "ValidTo";

		public const string REASON_FIELD = "Reason";

		public const string ISVOID_FIELD = "IsVoid";

		public const string AMOUNT_FIELD = "Amount";

		public const string APPROVEDBY_FIELD = "ApprovedBy";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CLVOUCHERTable => base.Tables["CL_Voucher"];

		public DataTable CLVOUCHERDetailTable => base.Tables["CL_Voucher"];

		public CLVoucherData()
		{
			BuildDataTables();
		}

		public CLVoucherData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("CL_Voucher");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("CustomerID", typeof(string));
			columns.Add("VoucherDate", typeof(DateTime));
			columns.Add("ValidFrom", typeof(DateTime));
			columns.Add("ValidTo", typeof(DateTime));
			columns.Add("Reason", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("Note", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("ApprovedBy", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
