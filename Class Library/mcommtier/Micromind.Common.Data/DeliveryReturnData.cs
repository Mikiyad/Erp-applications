using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class DeliveryReturnData : DataSet
	{
		public const string DELIVERYRETURN_TABLE = "Delivery_Return";

		public const string DELIVERYRETURNDETAIL_TABLE = "Delivery_Return_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string SALESFLOW_FIELD = "SalesFlow";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SALESPERSONID_FIELD = "SalespersonID";

		public const string REQUIREDDATE_FIELD = "RequiredDate";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string CUSTOMERADDRESS_FIELD = "CustomerAddress";

		public const string STATUS_FIELD = "Status";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string TERMID_FIELD = "TermID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string REFERENCE2_FIELD = "Reference2";

		public const string NOTE_FIELD = "Note";

		public const string DNOTESYSDOCID_FIELD = "DNoteSysDocID";

		public const string DNOTEVOCHERID_FIELD = "DNoteVoucherID";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string ISEXPORT_FIELD = "IsExport";

		public const string REASONID_FIELD = "ReasonID";

		public const string DISCOUNT_FIELD = "Discount";

		public const string TOTAL_FIELD = "Total";

		public const string DRIVERID_FIELD = "DriverID";

		public const string VEHICLEID_FIELD = "VehicleID";

		public const string ISCOMPLETERETURN_FIELD = "IsCompleteReturn";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string SPECIFICATIONID_FIELD = "SpecificationID";

		public const string STYLEID_FIELD = "StyleID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REMARKS_FIELD = "Remarks";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string DNROWINDEX_FIELD = "DNRowIndex";

		public DataTable DeliveryReturnTable => base.Tables["Delivery_Return"];

		public DataTable DeliveryReturnDetailTable => base.Tables["Delivery_Return_Detail"];

		public DeliveryReturnData()
		{
			BuildDataTables();
		}

		public DeliveryReturnData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Delivery_Return");
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
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("CustomerID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("SalespersonID", typeof(string));
			columns.Add("SalesFlow", typeof(byte));
			columns.Add("RequiredDate", typeof(DateTime));
			columns.Add("ShippingAddressID", typeof(string));
			columns.Add("CustomerAddress", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("TermID", typeof(string));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("DNoteSysDocID", typeof(string));
			columns.Add("DNoteVoucherID", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("IsExport", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("DriverID", typeof(string));
			columns.Add("ReasonID", typeof(string));
			columns.Add("VehicleID", typeof(string));
			columns.Add("IsCompleteReturn", typeof(bool));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Delivery_Return_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("SpecificationID", typeof(string));
			columns.Add("StyleID", typeof(string));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("DNRowIndex", typeof(short));
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotReceivingDetailTable(this);
		}
	}
}
