using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class FreightChargeData : DataSet
	{
		public const string FREIGHTCHRAGE_TABLE = "Freight_Charge";

		public const string FREIGHTCHARGEDETAIL_TABLE = "Freight_Charge_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string VALIDDATEFROM_FIELD = "ValidDateFrom";

		public const string VALIDDATETO_FIELD = "ValidDateTo";

		public const string STATUS_FIELD = "Status";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DISCOUNT_FIELD = "Discount";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TOTAL_FIELD = "Total";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string SOURCEPORTID_FIELD = "SourcePortID";

		public const string DESTINATIONPORTID_FIELD = "DestinationPortID";

		public const string SHIPPINGCOMPANYID_FIELD = "ShippingCompanyID";

		public const string AMOUNT_FIELD = "Amount";

		public const string FREEDAYS_FIELD = "FreeDays";

		public const string TRANSITDAYS_FIELD = "TransitDays";

		public const string CONTAINERSIZEID_FIELD = "ContainerSizeID";

		public const string TYPEID_FIELD = "TypeID";

		public const string REMARKS_FIELD = "Remarks";

		public DataTable FreightChargeTable => base.Tables["Freight_Charge"];

		public DataTable FreightChargeDetailTable => base.Tables["Freight_Charge_Detail"];

		public FreightChargeData()
		{
			BuildDataTables();
		}

		public FreightChargeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Freight_Charge");
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
			columns.Add("DivisionID", typeof(string));
			columns.Add("CompanyID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("Status", typeof(byte));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("ValidDateFrom", typeof(DateTime));
			columns.Add("ValidDateTo", typeof(DateTime));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("Inactive", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Freight_Charge_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("SourcePortID", typeof(string));
			columns.Add("DestinationPortID", typeof(string));
			columns.Add("ShippingCompanyID", typeof(string));
			columns.Add("FreeDays", typeof(string));
			columns.Add("TransitDays", typeof(string));
			columns.Add("TypeID", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("ContainerSizeID", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("RowIndex", typeof(int));
			base.Tables.Add(dataTable);
		}
	}
}
