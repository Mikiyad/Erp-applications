using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class LocationData : DataSet
	{
		public const string LOCATION_TABLE = "Location";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string LOCATIONNAME_FIELD = "LocationName";

		public const string SALESACCOUNTID_FIELD = "SalesAccountID";

		public const string COGSACCOUNTID_FIELD = "COGSAccountID";

		public const string UNINVOICEDINVENTORYACCOUNTID_FIELD = "UnInvoicedInventoryAccountID";

		public const string INVENTORYACCOUNTID_FIELD = "InventoryAccountID";

		public const string SALESTAXACCOUNTID_FIELD = "SalesTaxAccountID";

		public const string PURCHASETAXACCOUNTID_FIELD = "PurchaseTaxAccountID";

		public const string DISCOUNTGIVENACCOUNTID_FIELD = "DiscountGivenAccountID";

		public const string DISCOUNTRECEIVEDACCOUNTID_FIELD = "DiscountReceivedAccountID";

		public const string ARACCOUNTID_FIELD = "ARAccountID";

		public const string APACCOUNTID_FIELD = "APAccountID";

		public const string EMPLOYEEACCOUNTID_FIELD = "EmployeeAccountID";

		public const string POSCASHACCOUNTID_FIELD = "POSCashAccountID";

		public const string POSCARDACCOUNTID_FIELD = "POSCardAccountID";

		public const string EXCHANGEGAINLOSSACCOUNTID_FIELD = "ExchangeGainLossAccountID";

		public const string PROJECTWIPACCOUNTID_FIELD = "ProjectWIPAccountID";

		public const string PROJECTINCOMEACCOUNTID_FIELD = "ProjectIncomeAccountID";

		public const string PROJECTCOSTACCOUNTID_FIELD = "ProjectCostAccountID";

		public const string PROJECTTIMESHEETCONTRAACCOUNTID_FIELD = "ProjectTimesheetContraAccountID";

		public const string PROJECTRETENTIONACCOUNTID_FIELD = "ProjectRetentionAccountID";

		public const string PROJECTADVANCEACCOUNTID_FIELD = "ProjectAdvanceAccountID";

		public const string MANUWIPACCOUNTID_FIELD = "ManuWIPAccountID";

		public const string MANUTIMESHEETCONTRAACCOUNTID_FIELD = "ManuTimesheetContraAccountID";

		public const string CONSIGNINACCOUNTID_FIELD = "ConsignInAccountID";

		public const string CONSIGNINCOMMISSIONACCOUNTID_FIELD = "ConsignInCommissionAccountID";

		public const string CONSIGNINDIFFACCOUNTID_FIELD = "ConsignInDiffAccountID";

		public const string CONSIGNOUTSALESACCOUNTID_FIELD = "ConsignOutSalesAccountID";

		public const string CONSIGNOUTCOGSACCOUNTID_FIELD = "ConsignOutCOGSAccountID";

		public const string ALLOCATIONDISCOUNTACCOUNTID_FIELD = "AllocationDiscountAccountID";

		public const string ROUNDOFFACCOUNTID_FIELD = "RoundOffAccountID";

		public const string PURCHASEPREPAYMENTACCOUNTID_FIELD = "PurchasePrePaymentAccountID";

		public const string PREPAYMENTAPACCOUNTID_FIELD = "PrepaymentAPAccountID";

		public const string LEAVEEXPENSEACCOUNTID_FIELD = "LeaveExpenseAccountID";

		public const string EOSBENEFITACCOUNTID_FIELD = "EOSBenefitAccountID";

		public const string PROVISIONACCOUNTID_FIELD = "ProvisionAccountID";

		public const string TICKETACCOUNTID_FIELD = "TicketAccountID";

		public const string TAXLOCATIONDETAIL_TABLE = "LocationAccounts_Tax_Detail";

		public const string TAXSALESACCOUNTID_FIELD = "SalesAccountID";

		public const string TAXPURCHASEACCOUNTID_FIELD = "PurchaseAccountID";

		public const string TAXID_FIELD = "TaxID";

		public const string TAXPERCENT_FIELD = "TaxPercent";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string LOCATIONCURRENCYID_FIELD = "LocationCurrencyID";

		public const string AREAID_FIELD = "AreaID";

		public const string COUNTRYID_FIELD = "CountryID";

		public const string ISCONSIGNOUTLOCATION_FIELD = "IsConsignOutLocation";

		public const string ISPOSLOCATION_FIELD = "IsPOSLocation";

		public const string ISCONSIGNINLOCATION_FIELD = "IsConsignInLocation";

		public const string ISWAREHOUSE_FIELD = "IsWarehouse";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string USERLOCATIONDETAIL_TABLE = "User_Location_Detail";

		public const string USERID_FIELD = "UserID";

		public DataTable LocationTable => base.Tables["Location"];

		public DataTable UserLocationDetailTable => base.Tables["User_Location_Detail"];

		public DataTable TaxLocationDetailTable => base.Tables["LocationAccounts_Tax_Detail"];

		public LocationData()
		{
			BuildDataTables();
		}

		public LocationData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Location");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("LocationID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LocationName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("InventoryAccountID", typeof(string));
			columns.Add("SalesAccountID", typeof(string));
			columns.Add("COGSAccountID", typeof(string));
			columns.Add("UnInvoicedInventoryAccountID", typeof(string));
			columns.Add("SalesTaxAccountID", typeof(string));
			columns.Add("ARAccountID", typeof(string));
			columns.Add("APAccountID", typeof(string));
			columns.Add("EmployeeAccountID", typeof(string));
			columns.Add("POSCardAccountID", typeof(string));
			columns.Add("POSCashAccountID", typeof(string));
			columns.Add("ExchangeGainLossAccountID", typeof(string));
			columns.Add("ProjectWIPAccountID", typeof(string));
			columns.Add("ProjectIncomeAccountID", typeof(string));
			columns.Add("ProjectCostAccountID", typeof(string));
			columns.Add("ProjectTimesheetContraAccountID", typeof(string));
			columns.Add("ProjectRetentionAccountID", typeof(string));
			columns.Add("ProjectAdvanceAccountID", typeof(string));
			columns.Add("ManuWIPAccountID", typeof(string));
			columns.Add("ManuTimesheetContraAccountID", typeof(string));
			columns.Add("DiscountGivenAccountID", typeof(string));
			columns.Add("IsPOSLocation", typeof(bool));
			columns.Add("DiscountReceivedAccountID", typeof(string));
			columns.Add("IsConsignOutLocation", typeof(bool));
			columns.Add("IsConsignInLocation", typeof(bool));
			columns.Add("IsWarehouse", typeof(bool));
			columns.Add("ConsignInAccountID", typeof(string));
			columns.Add("ConsignInCommissionAccountID", typeof(string));
			columns.Add("ConsignInDiffAccountID", typeof(string));
			columns.Add("ConsignOutSalesAccountID", typeof(string));
			columns.Add("ConsignOutCOGSAccountID", typeof(string));
			columns.Add("AllocationDiscountAccountID", typeof(string));
			columns.Add("RoundOffAccountID", typeof(string));
			columns.Add("PurchasePrePaymentAccountID", typeof(string));
			columns.Add("PrepaymentAPAccountID", typeof(string));
			columns.Add("CountryID", typeof(string));
			columns.Add("AreaID", typeof(string));
			columns.Add("LocationCurrencyID", typeof(string));
			columns.Add("LeaveExpenseAccountID", typeof(string));
			columns.Add("ProvisionAccountID", typeof(string));
			columns.Add("EOSBenefitAccountID", typeof(string));
			columns.Add("TicketAccountID", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("User_Location_Detail");
			columns = dataTable.Columns;
			columns.Add("LocationID", typeof(string));
			columns.Add("UserID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("LocationAccounts_Tax_Detail");
			columns = dataTable.Columns;
			columns.Add("LocationID", typeof(string));
			columns.Add("TaxID", typeof(string));
			columns.Add("PurchaseAccountID", typeof(string));
			columns.Add("SalesAccountID", typeof(string));
			columns.Add("TaxPercent", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
