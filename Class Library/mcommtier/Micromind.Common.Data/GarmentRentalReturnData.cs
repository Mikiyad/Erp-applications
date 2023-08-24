using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class GarmentRentalReturnData : DataSet
	{
		public const string GARMENTRENTALRETURN_TABLE = "Garment_Rental_Return";

		public const string GARMENTRENTALRETURNDETAIL_TABLE = "Garment_Rental_Return_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCEROWINDEX_FIELD = "SourceRowIndex";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string CUSTOMERADDRESS_FIELD = "CustomerAddress";

		public const string STATUS_FIELD = "Status";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DISCOUNT_FIELD = "Discount";

		public const string CHARGES_FIELD = "Charges";

		public const string TOTAL_FIELD = "Total";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string REGISTERID_FIELD = "RegisterID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string CASHAMOUNT_FIELD = "CashAmount";

		public const string CARDAMOUNT_FIELD = "CardAmount";

		public const string AMOUNTPAID_FIELD = "AmountPaid";

		public const string BALANCE_FIELD = "Balance";

		public const string CASHACCOUNTID_FIELD = "CashAccountID";

		public const string CARDACCOUNTID_FIELD = "CardAccountID";

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

		public DataTable GarmentRentalReturnTable => base.Tables["Garment_Rental_Return"];

		public DataTable GarmentRentalReturnDetailTable => base.Tables["Garment_Rental_Return_Detail"];

		public GarmentRentalReturnData()
		{
			BuildDataTables();
		}

		public GarmentRentalReturnData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Garment_Rental_Return");
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
			columns.Add("CustomerID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("ShippingAddressID", typeof(string));
			columns.Add("CustomerAddress", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceRowIndex", typeof(short));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("RegisterID", typeof(string));
			columns.Add("Discount", typeof(decimal));
			columns.Add("Charges", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("CashAmount", typeof(decimal));
			columns.Add("CardAmount", typeof(decimal));
			columns.Add("AmountPaid", typeof(decimal));
			columns.Add("Balance", typeof(decimal));
			columns.Add("CashAccountID", typeof(string));
			columns.Add("CardAccountID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Garment_Rental_Return_Detail");
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
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceRowIndex", typeof(int));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotReceivingDetailTable(this);
			InventoryTransactionData.AddProductLotIssueDetailTable(this);
		}
	}
}
