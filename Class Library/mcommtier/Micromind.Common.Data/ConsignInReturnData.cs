using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ConsignInReturnData : DataSet
	{
		public const string CONSIGNINRETURN_TABLE = "ConsignIn_Return";

		public const string CONSIGNINRETURNDETAIL_TABLE = "ConsignIn_Return_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string VENDORID_FIELD = "VendorID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CONSIGNSYSDOCID_FIELD = "ConsignSysDocID";

		public const string CONSIGNVOUCHERID_FIELD = "ConsignVoucherID";

		public const string CONSIGNROWINDEX_FIELD = "ConsignRowIndex";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string VENDORADDRESS_FIELD = "VendorAddress";

		public const string STATUS_FIELD = "Status";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string QUANTITYRETURNED_FIELD = "QuantityReturned";

		public const string DESCRIPTION_FIELD = "Description";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string CONSIGNLOCATIONID_FIELD = "ConsignLocationID";

		public const string ORDERVOUCHERID_FIELD = "ConsignSysDocID";

		public const string ORDERSYSDOCID_FIELD = "ConsignVoucherID";

		public const string ORDERROWINDEX_FIELD = "ConsignRowIndex";

		public DataTable ConsignInReturnTable => base.Tables["ConsignIn_Return"];

		public DataTable ConsignInReturnDetailTable => base.Tables["ConsignIn_Return_Detail"];

		public ConsignInReturnData()
		{
			BuildDataTables();
		}

		public ConsignInReturnData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("ConsignIn_Return");
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
			columns.Add("VendorID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("ShippingAddressID", typeof(string));
			columns.Add("VendorAddress", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("ConsignVoucherID", typeof(string));
			columns.Add("ConsignSysDocID", typeof(string));
			columns.Add("ConsignRowIndex", typeof(short));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("ConsignIn_Return_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("ConsignLocationID", typeof(string));
			columns.Add("ConsignVoucherID", typeof(string));
			columns.Add("ConsignSysDocID", typeof(string));
			columns.Add("ConsignRowIndex", typeof(int));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotIssueDetailTable(this);
		}
	}
}
