using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Location : StoreObject
	{
		private const string LOCATIONID_PARM = "@LocationID";

		private const string LOCATIONNAME_PARM = "@LocationName";

		private const string INACTIVE_PARM = "@Inactive";

		private const string NOTE_PARM = "@Note";

		private const string SALESACCOUNTID_PARM = "@SalesAccountID";

		private const string COGSACCOUNTID_PARM = "@COGSAccountID";

		private const string UNINVOICEDINVENTORYACCOUNTID_PARM = "@UnInvoicedInventoryAccountID";

		private const string INVENTORYACCOUNTID_PARM = "@InventoryAccountID";

		private const string SALESTAXACCOUNTID_PARM = "@SalesTaxAccountID";

		private const string ISPOSLOCATION_PARM = "@IsPOSLocation";

		private const string DISCOUNTGIVENACCOUNTID_PARM = "@DiscountGivenAccountID";

		private const string DISCOUNTRECEIVEDACCOUNTID_PARM = "@DiscountReceivedAccountID";

		private const string ARACCOUNTID_PARM = "@ARAccountID";

		private const string APACCOUNTID_PARM = "@APAccountID";

		private const string EMPLOYEEACCOUNTID_PARM = "@EmployeeAccountID";

		private const string PROJECTWIPACCOUNTID_PARM = "@ProjectWIPAccountID";

		private const string PROJECTINCOMEACCOUNTID_PARM = "@ProjectIncomeAccountID";

		private const string PROJECTCOSTACCOUNTID_PARM = "@ProjectTimesheetContraAccountID";

		private const string PROJECTTIMESHEETCONTRAACCOUNTID_PARM = "@ProjectCostAccountID";

		private const string PROJECTRETENTIONACCOUNTID_PARM = "@ProjectRetentionAccountID";

		private const string PROJECTADVANCEACCOUNTID_PARM = "@ProjectAdvanceAccountID";

		private const string MANUWIPACCOUNTID_PARM = "@ManuWIPAccountID";

		private const string MANUTIMESHEETCONTRAACCOUNTID_PARM = "@ManuTimesheetContraAccountID";

		private const string CONSIGNINACCOUNTID_PARM = "@ConsignInAccountID";

		private const string CONSIGNINCOMMISSIONACCOUNTID_PARM = "@ConsignInCommissionAccountID";

		private const string CONSIGNINDIFFACCOUNTID_PARM = "@ConsignInDiffAccountID";

		private const string CONSIGNOUTSALESACCOUNTID_PARM = "@ConsignOutSalesAccountID";

		private const string CONSIGNOUTCOGSACCOUNTID_PARM = "@ConsignOutCOGSAccountID";

		private const string ALLOCATIONDISCOUNTACCOUNTID_PARM = "@AllocationDiscountAccountID";

		private const string ROUNDOFFACCOUNTID_PARM = "@RoundOffAccountID";

		private const string PURCHASEPREPAYMENTACCOUNTID_PARM = "@PurchasePrePaymentAccountID";

		private const string PREPAYMENTAPACCOUNTID_PARM = "@PrepaymentAPAccountID";

		private const string POSCASHACCOUNTID_PARM = "@POSCashAccountID";

		private const string POSCARDACCOUNTID_PARM = "@POSCardAccountID";

		private const string EXCHANGEGAINLOSSACCOUNTID_PARM = "@ExchangeGainLossAccountID";

		private const string ISCONSIGNOUTLOCATION_PARM = "@IsConsignOutLocation";

		private const string ISCONSIGNINLOCATION_PARM = "@IsConsignInLocation";

		private const string ISWAREHOUSE_PARM = "@IsWarehouse";

		private const string LOCATIONCURRENCYID_PARM = "@LocationCurrencyID";

		private const string AREAID_PARM = "@AreaID";

		private const string COUNTRYID_PARM = "@CountryID";

		private const string TAXLOCATIONDETAIL_TABLE = "LocationAccounts_Tax_Detail";

		private const string TAXSALESACCOUNTID_PARM = "@SalesAccountID";

		private const string TAXPURCHASEACCOUNTID_PARM = "@PurchaseAccountID";

		private const string TAXID_PARM = "@TaxID";

		private const string TAXPERCENT_PARM = "@TaxPercent";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string LEAVEEXPENSEACCOUNTID_PARM = "@LeaveExpenseAccountID";

		private const string EOSBENEFITACCOUNTID_PARM = "@EOSBenefitAccountID";

		private const string PROVISIONACCOUNTID_PARM = "@ProvisionAccountID";

		private const string TICKETACCOUNTID_PARM = "@TicketAccountID";

		private const string LOCATION_TABLE = "Location";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string USERID_PARM = "@UserID";

		public Location(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Location", new FieldValue("LocationID", "@LocationID", isUpdateConditionField: true), new FieldValue("LocationName", "@LocationName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("SalesAccountID", "@SalesAccountID"), new FieldValue("COGSAccountID", "@COGSAccountID"), new FieldValue("UnInvoicedInventoryAccountID", "@UnInvoicedInventoryAccountID"), new FieldValue("IsPOSLocation", "@IsPOSLocation"), new FieldValue("InventoryAccountID", "@InventoryAccountID"), new FieldValue("SalesTaxAccountID", "@SalesTaxAccountID"), new FieldValue("ARAccountID", "@ARAccountID"), new FieldValue("APAccountID", "@APAccountID"), new FieldValue("EmployeeAccountID", "@EmployeeAccountID"), new FieldValue("ProjectWIPAccountID", "@ProjectWIPAccountID"), new FieldValue("ProjectIncomeAccountID", "@ProjectIncomeAccountID"), new FieldValue("ProjectCostAccountID", "@ProjectTimesheetContraAccountID"), new FieldValue("ProjectTimesheetContraAccountID", "@ProjectCostAccountID"), new FieldValue("ProjectRetentionAccountID", "@ProjectRetentionAccountID"), new FieldValue("ProjectAdvanceAccountID", "@ProjectAdvanceAccountID"), new FieldValue("ManuWIPAccountID", "@ManuWIPAccountID"), new FieldValue("ManuTimesheetContraAccountID", "@ManuTimesheetContraAccountID"), new FieldValue("POSCashAccountID", "@POSCashAccountID"), new FieldValue("POSCardAccountID", "@POSCardAccountID"), new FieldValue("ExchangeGainLossAccountID", "@ExchangeGainLossAccountID"), new FieldValue("DiscountGivenAccountID", "@DiscountGivenAccountID"), new FieldValue("DiscountReceivedAccountID", "@DiscountReceivedAccountID"), new FieldValue("IsConsignOutLocation", "@IsConsignOutLocation"), new FieldValue("IsConsignInLocation", "@IsConsignInLocation"), new FieldValue("ConsignInAccountID", "@ConsignInAccountID"), new FieldValue("ConsignInCommissionAccountID", "@ConsignInCommissionAccountID"), new FieldValue("ConsignInDiffAccountID", "@ConsignInDiffAccountID"), new FieldValue("ConsignOutSalesAccountID", "@ConsignOutSalesAccountID"), new FieldValue("ConsignOutCOGSAccountID", "@ConsignOutCOGSAccountID"), new FieldValue("AllocationDiscountAccountID", "@AllocationDiscountAccountID"), new FieldValue("RoundOffAccountID", "@RoundOffAccountID"), new FieldValue("PurchasePrePaymentAccountID", "@PurchasePrePaymentAccountID"), new FieldValue("PrepaymentAPAccountID", "@PrepaymentAPAccountID"), new FieldValue("IsWarehouse", "@IsWarehouse"), new FieldValue("LocationCurrencyID", "@LocationCurrencyID"), new FieldValue("AreaID", "@AreaID"), new FieldValue("CountryID", "@CountryID"), new FieldValue("LeaveExpenseAccountID", "@LeaveExpenseAccountID"), new FieldValue("EOSBenefitAccountID", "@EOSBenefitAccountID"), new FieldValue("ProvisionAccountID", "@ProvisionAccountID"), new FieldValue("TicketAccountID", "@TicketAccountID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Location", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@LocationName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@IsPOSLocation", SqlDbType.Bit);
			parameters.Add("@SalesAccountID", SqlDbType.NVarChar);
			parameters.Add("@COGSAccountID", SqlDbType.NVarChar);
			parameters.Add("@UnInvoicedInventoryAccountID", SqlDbType.NVarChar);
			parameters.Add("@InventoryAccountID", SqlDbType.NVarChar);
			parameters.Add("@SalesTaxAccountID", SqlDbType.NVarChar);
			parameters.Add("@ARAccountID", SqlDbType.NVarChar);
			parameters.Add("@APAccountID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeAccountID", SqlDbType.NVarChar);
			parameters.Add("@ProjectWIPAccountID", SqlDbType.NVarChar);
			parameters.Add("@ProjectIncomeAccountID", SqlDbType.NVarChar);
			parameters.Add("@ProjectTimesheetContraAccountID", SqlDbType.NVarChar);
			parameters.Add("@ProjectCostAccountID", SqlDbType.NVarChar);
			parameters.Add("@ProjectRetentionAccountID", SqlDbType.NVarChar);
			parameters.Add("@ProjectAdvanceAccountID", SqlDbType.NVarChar);
			parameters.Add("@ManuWIPAccountID", SqlDbType.NVarChar);
			parameters.Add("@ManuTimesheetContraAccountID", SqlDbType.NVarChar);
			parameters.Add("@POSCashAccountID", SqlDbType.NVarChar);
			parameters.Add("@POSCardAccountID", SqlDbType.NVarChar);
			parameters.Add("@DiscountGivenAccountID", SqlDbType.NVarChar);
			parameters.Add("@DiscountReceivedAccountID", SqlDbType.NVarChar);
			parameters.Add("@ExchangeGainLossAccountID", SqlDbType.NVarChar);
			parameters.Add("@ConsignInAccountID", SqlDbType.NVarChar);
			parameters.Add("@ConsignInCommissionAccountID", SqlDbType.NVarChar);
			parameters.Add("@ConsignInDiffAccountID", SqlDbType.NVarChar);
			parameters.Add("@ConsignOutSalesAccountID", SqlDbType.NVarChar);
			parameters.Add("@ConsignOutCOGSAccountID", SqlDbType.NVarChar);
			parameters.Add("@AllocationDiscountAccountID", SqlDbType.NVarChar);
			parameters.Add("@RoundOffAccountID", SqlDbType.NVarChar);
			parameters.Add("@PurchasePrePaymentAccountID", SqlDbType.NVarChar);
			parameters.Add("@PrepaymentAPAccountID", SqlDbType.NVarChar);
			parameters.Add("@IsConsignOutLocation", SqlDbType.Bit);
			parameters.Add("@IsConsignInLocation", SqlDbType.Bit);
			parameters.Add("@LocationCurrencyID", SqlDbType.NVarChar);
			parameters.Add("@AreaID", SqlDbType.NVarChar);
			parameters.Add("@CountryID", SqlDbType.NVarChar);
			parameters.Add("@IsWarehouse", SqlDbType.Bit);
			parameters.Add("@LeaveExpenseAccountID", SqlDbType.NVarChar);
			parameters.Add("@EOSBenefitAccountID", SqlDbType.NVarChar);
			parameters.Add("@ProvisionAccountID", SqlDbType.NVarChar);
			parameters.Add("@TicketAccountID", SqlDbType.NVarChar);
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@LocationName"].SourceColumn = "LocationName";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@IsPOSLocation"].SourceColumn = "IsPOSLocation";
			parameters["@SalesAccountID"].SourceColumn = "SalesAccountID";
			parameters["@COGSAccountID"].SourceColumn = "COGSAccountID";
			parameters["@UnInvoicedInventoryAccountID"].SourceColumn = "UnInvoicedInventoryAccountID";
			parameters["@InventoryAccountID"].SourceColumn = "InventoryAccountID";
			parameters["@SalesTaxAccountID"].SourceColumn = "SalesTaxAccountID";
			parameters["@ARAccountID"].SourceColumn = "ARAccountID";
			parameters["@APAccountID"].SourceColumn = "APAccountID";
			parameters["@EmployeeAccountID"].SourceColumn = "EmployeeAccountID";
			parameters["@ProjectWIPAccountID"].SourceColumn = "ProjectWIPAccountID";
			parameters["@ProjectIncomeAccountID"].SourceColumn = "ProjectIncomeAccountID";
			parameters["@ProjectTimesheetContraAccountID"].SourceColumn = "ProjectCostAccountID";
			parameters["@ProjectCostAccountID"].SourceColumn = "ProjectTimesheetContraAccountID";
			parameters["@ProjectRetentionAccountID"].SourceColumn = "ProjectRetentionAccountID";
			parameters["@ProjectAdvanceAccountID"].SourceColumn = "ProjectAdvanceAccountID";
			parameters["@ManuWIPAccountID"].SourceColumn = "ManuWIPAccountID";
			parameters["@ManuTimesheetContraAccountID"].SourceColumn = "ManuTimesheetContraAccountID";
			parameters["@POSCashAccountID"].SourceColumn = "POSCashAccountID";
			parameters["@POSCardAccountID"].SourceColumn = "POSCardAccountID";
			parameters["@DiscountGivenAccountID"].SourceColumn = "DiscountGivenAccountID";
			parameters["@DiscountReceivedAccountID"].SourceColumn = "DiscountReceivedAccountID";
			parameters["@ExchangeGainLossAccountID"].SourceColumn = "ExchangeGainLossAccountID";
			parameters["@ConsignInAccountID"].SourceColumn = "ConsignInAccountID";
			parameters["@ConsignInCommissionAccountID"].SourceColumn = "ConsignInCommissionAccountID";
			parameters["@ConsignInDiffAccountID"].SourceColumn = "ConsignInDiffAccountID";
			parameters["@ConsignOutSalesAccountID"].SourceColumn = "ConsignOutSalesAccountID";
			parameters["@AllocationDiscountAccountID"].SourceColumn = "AllocationDiscountAccountID";
			parameters["@RoundOffAccountID"].SourceColumn = "RoundOffAccountID";
			parameters["@PurchasePrePaymentAccountID"].SourceColumn = "PurchasePrePaymentAccountID";
			parameters["@PrepaymentAPAccountID"].SourceColumn = "PrepaymentAPAccountID";
			parameters["@ConsignOutCOGSAccountID"].SourceColumn = "ConsignOutCOGSAccountID";
			parameters["@IsConsignOutLocation"].SourceColumn = "IsConsignOutLocation";
			parameters["@IsConsignInLocation"].SourceColumn = "IsConsignInLocation";
			parameters["@LocationCurrencyID"].SourceColumn = "LocationCurrencyID";
			parameters["@AreaID"].SourceColumn = "AreaID";
			parameters["@CountryID"].SourceColumn = "CountryID";
			parameters["@IsWarehouse"].SourceColumn = "IsWarehouse";
			parameters["@LeaveExpenseAccountID"].SourceColumn = "LeaveExpenseAccountID";
			parameters["@EOSBenefitAccountID"].SourceColumn = "EOSBenefitAccountID";
			parameters["@ProvisionAccountID"].SourceColumn = "ProvisionAccountID";
			parameters["@TicketAccountID"].SourceColumn = "TicketAccountID";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateTaxDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("LocationAccounts_Tax_Detail", new FieldValue("LocationID", "@LocationID", isUpdateConditionField: true), new FieldValue("SalesAccountID", "@SalesAccountID"), new FieldValue("PurchaseAccountID", "@PurchaseAccountID"), new FieldValue("TaxID", "@TaxID"), new FieldValue("TaxPercent", "@TaxPercent"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("LocationAccounts_Tax_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateTaxDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateTaxDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateTaxDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@PurchaseAccountID", SqlDbType.NVarChar);
			parameters.Add("@SalesAccountID", SqlDbType.NVarChar);
			parameters.Add("@TaxPercent", SqlDbType.Decimal);
			parameters.Add("@TaxID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@PurchaseAccountID"].SourceColumn = "PurchaseAccountID";
			parameters["@SalesAccountID"].SourceColumn = "SalesAccountID";
			parameters["@TaxPercent"].SourceColumn = "TaxPercent";
			parameters["@TaxID"].SourceColumn = "TaxID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertLocation(LocationData accountLocationData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(accountLocationData, "Location", insertUpdateCommand);
				object value = accountLocationData.LocationTable.Rows[0]["LocationID"];
				insertUpdateCommand = GetInsertUpdateTaxDetailsCommand(isUpdate: false);
				insertUpdateCommand.Transaction = sqlTransaction;
				if (accountLocationData.Tables["LocationAccounts_Tax_Detail"].Rows.Count > 0)
				{
					foreach (DataRow row in accountLocationData.TaxLocationDetailTable.Rows)
					{
						row["LocationID"] = value;
					}
					flag &= Insert(accountLocationData, "LocationAccounts_Tax_Detail", insertUpdateCommand);
				}
				string text = accountLocationData.LocationTable.Rows[0]["LocationID"].ToString();
				AddActivityLog("Location", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Location", "LocationID", text, sqlTransaction, isInsert: true);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool UpdateLocation(LocationData accountLocationData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountLocationData, "Location", insertUpdateCommand);
				if (accountLocationData.Tables["LocationAccounts_Tax_Detail"].Rows.Count > 0)
				{
					accountLocationData.TaxLocationDetailTable.Rows[0]["TaxID"].ToString();
					string text = accountLocationData.LocationTable.Rows[0]["LocationID"].ToString();
					flag &= DeleteTaxDetailsRows(text, sqlTransaction);
					foreach (DataRow row in accountLocationData.TaxLocationDetailTable.Rows)
					{
						row["LocationID"] = text;
					}
				}
				if (!flag)
				{
					return flag;
				}
				object obj = accountLocationData.LocationTable.Rows[0]["LocationID"];
				UpdateTableRowByID("Location", "LocationID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountLocationData.LocationTable.Rows[0]["LocationName"].ToString();
				AddActivityLog("Location", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Location", "LocationID", obj, sqlTransaction, isInsert: false);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		internal bool DeleteTaxDetailsRows(string locationID, SqlTransaction sqlTransaction)
		{
			bool result = true;
			try
			{
				string exp = "select count(*) from  LocationAccounts_Tax_Detail WHERE LocationID = '" + locationID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null)
				{
					return result;
				}
				if (obj.ToString() != "")
				{
					exp = "DELETE FROM LocationAccounts_Tax_Detail WHERE LocationID = '" + locationID + "'";
					return Delete(exp, sqlTransaction);
				}
				return result;
			}
			catch
			{
				throw;
			}
		}

		public LocationData GetLocation()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Location");
			LocationData locationData = new LocationData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(locationData, "Location", sqlBuilder);
			return locationData;
		}

		public bool DeleteLocation(string locationID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Location WHERE LocationID = '" + locationID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Location", locationID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public LocationData GetLocationByID(string id, bool isPOS)
		{
			LocationData locationData = new LocationData();
			string text = "SELECT * FROM Location WHERE LocationID='" + id + "' ";
			if (isPOS)
			{
				text += " AND ISPOSLocation = 'True' ";
			}
			FillDataSet(locationData, "Location", text);
			text = "SELECT LocationID,\r\n                        TaxID,\r\n                        SalesAccountID,\r\n                        PurchaseAccountID,TaxPercent, RowIndex\r\n                         FROM LocationAccounts_Tax_Detail WHERE LocationID='" + id + "'  UNION select '',TaxID, '','', NUll, NULL from Tax where TaxID NOT IN (select TaxID from LocationAccounts_Tax_Detail WHERE LocationID='" + id + "') ";
			FillDataSet(locationData, "LocationAccounts_Tax_Detail", text);
			return locationData;
		}

		public DataSet GetLocationByFields(params string[] columns)
		{
			return GetLocationByFields(null, isInactive: true, columns);
		}

		public DataSet GetLocationByFields(string[] locationID, params string[] columns)
		{
			return GetLocationByFields(locationID, isInactive: true, columns);
		}

		public DataSet GetLocationByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Location");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "LocationID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Location";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Location", sqlBuilder);
			return dataSet;
		}

		public DataSet GetLocationList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LocationID [Code],LocationName [Name]\r\n                           FROM Location ";
			FillDataSet(dataSet, "Location", textCommand);
			return dataSet;
		}

		public DataSet GetLocationComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LocationID [Code],LocationName [Name],IsConsignOutLocation,IsConsignInLocation,ISPOSLocation,IsWarehouse,  \r\n                                CASE(SELECT COUNT(LocationID) FROM User_Location_Detail ULD2  WHERE\r\n                                ULD2.LocationID = L.LocationID) WHEN  0 THEN 'False' ELSE 'True' END AS IsUserLocation\r\n                           FROM Location L ORDER BY LocationID,LocationName";
			FillDataSet(dataSet, "Location", textCommand);
			return dataSet;
		}

		public DataSet GetUserLocationComboList(string UserID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT L.LocationID [Code],LocationName [Name],IsConsignOutLocation,IsConsignInLocation,ISPOSLocation,IsWarehouse \r\n                               \r\n                           FROM Location L LEFT JOIN User_Location_Detail ULD ON L.LocationID=ULD.LocationID WHERE ULD.UserID='" + UserID + "'  ORDER BY L.LocationID,LocationName";
			FillDataSet(dataSet, "Location", textCommand);
			return dataSet;
		}

		public DataSet GetSalesByLocationSummaryReport(DateTime from, DateTime to, string fromLocation, string toLocation)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT ISNULL(D.LocationID, 'No Location') AS [LocationID], SP.LocationName,\r\n                            SUM(ISNULL(SI.Discount, 0))  AS Discount, SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN SI.Total-SI.Discount+SI.RoundOff ELSE 0 END) \r\n                            AS [CashSale], SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN SI.Total-SI.Discount+SI.RoundOff ELSE 0 END) AS [CreditSale]  \r\n\t\t\t\t            FROM Sales_Invoice SI \r\n                             INNER JOIN System_Document D ON D.SysDocID=SI.SysDocID\r\n                            LEFT OUTER JOIN Location SP ON D.LocationID = SP.LocationID\r\n                           \r\n                            WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				str = str + " AND D.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			str += " GROUP BY D.LocationID,SP.LocationName ORDER BY D.LocationID";
			FillDataSet(dataSet, "Sales", str);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables[0].Columns["LocationID"]
			};
			str = "SELECT ISNULL(D.LocationID,'No Location') AS [LocationID], SP.LocationName,\r\n                            -1 * SUM(ISNULL(SI.Discount, 0)) AS DiscountReturn,  SUM(SI.Total-SI.Discount+SI.RoundOff)  AS  [SalesReturn]\r\n\t\t\t\t\t\t\tFROM Sales_Return SI \r\n                            INNER JOIN System_Document D ON D.SysDocID=SI.SysDocID\r\n                            LEFT OUTER JOIN Location SP ON D.LocationID=SP.LocationID\r\n                           \r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				str = str + " AND D.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			str += " GROUP BY D.LocationID, SP.LocationName Order BY D.LocationID";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Sales", str);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables[0].Columns["LocationID"]
			};
			dataSet.Merge(dataSet2.Tables[0]);
			return dataSet;
		}

		public DataSet GetDailySalesAnalysisReport(DateTime from, DateTime to, string fromLocation, string toLocation)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string text3 = "";
			text3 = "SELECT    CONVERT (date,ASD.Date,103) as asd_date ,SUM(COGS) COGS into #tmp1             \r\n                        FROM Axo_Sales_Detail ASD\r\n                        INNER JOIN System_Document SD with(nolock)ON SD.SysDocID = ASD.[Doc ID]\r\n                        LEFT OUTER JOIN Location L with(nolock)  ON L.LocationID = SD.LocationID\r\n                        WHERE    asd.Date  BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromLocation != "")
			{
				text3 = text3 + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			text3 += "group by    CONVERT(date, ASD.Date, 103)";
			text3 = text3 + "SELECT      CONVERT (date,TransactionDate,103) AS [Date],\r\n                        SUM(Discount) Discount,SUM(RoundOff) RoundOff,SUM(TaxAmount) Tax,\r\n                        SUM(CASE ISNULL(IsCash, 'False') WHEN  'True' THEN TaxAmount ELSE 0 END) AS[CashSaleTax], \r\n                        SUM(CASE ISNULL(IsCash, 'False') WHEN  'False' THEN TaxAmount ELSE 0 END) AS[CreditSaleTax],\r\n                        SUM(CASE ISNULL(IsCash, 'False') WHEN  'True' THEN Total + RoundOff - Discount ELSE 0 END) AS[CashSale], \r\n                        SUM(CASE ISNULL(IsCash, 'False') WHEN  'False' THEN Total + RoundOff - Discount ELSE 0 END) AS[CreditSale]\r\n                        into #tmp2 \r\n                        FROM        Sales_Invoice SI\r\n                        INNER JOIN    System_Document D with(nolock)  ON D.SysDocID = SI.SysDocID\r\n                        LEFT OUTER JOIN Location SP with(nolock)ON D.LocationID = SP.LocationID\r\n                        WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'AND ISNULL(IsVoid, 'False') = 'False' ";
			if (fromLocation != "")
			{
				text3 = text3 + " AND D.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			text3 += "GROUP BY  CONVERT(date, TransactionDate, 103)";
			text3 += "select b.Date AS [Date], b.Discount , b.RoundOff , b.Tax, a.COGS AS Cost , b.CashSaleTax, b.CreditSaleTax, b.CashSale, b.CreditSale from #tmp2 b inner join #tmp1 a on b.[date] = a.asd_date\r\n                     ORDER BY CONVERT(date, b.Date)";
			text3 += " DROP Table #tmp1 ";
			text3 += " DROP Table #tmp2 ";
			FillDataSet(dataSet, "Sales", text3);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables[0].Columns["Date"]
			};
			text3 = "SELECT CONVERT (date,TransactionDate,103) AS [Date],\r\n                    -1*SUM(Discount) AS DiscountReturn,-1*SUM(RoundOff) RoundOffReturn,-1*SUM(TaxAmount) TaxReturn,\r\n                    0.00 AS CostReturn,SUM(Total) AS [SalesReturn]\r\n                    FROM Sales_Return SI\r\n                    INNER JOIN System_Document D with(nolock)   ON D.SysDocID=SI.SysDocID\r\n                    LEFT OUTER JOIN Location SP with(nolock)   ON D.LocationID=SP.LocationID\r\n                           \r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				text3 = text3 + " AND D.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			text3 += " GROUP BY CONVERT (date,TransactionDate,103),  CONVERT(date, TransactionDate) ORDER BY CONVERT(date, TransactionDate) ";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Sales", text3);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables[0].Columns["Date"]
			};
			dataSet.Merge(dataSet2.Tables[0]);
			return dataSet;
		}

		public DataSet GetDailySalesAnalysisReportOld(DateTime from, DateTime to, string fromLocation, string toLocation)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT CONVERT (VARCHAR(11),TransactionDate,103) AS [Date],\r\n                            SUM(Discount) Discount,SUM(RoundOff) RoundOff,SUM(TaxAmount) Tax,\r\n                            (SELECT SUM (COGS) FROM Axo_Sales_Detail ASD ";
			if (fromLocation != "")
			{
				str += "INNER JOIN System_Document SD with(nolock)  ON SD.SysDocID=ASD.[Doc ID] LEFT OUTER JOIN Location L with(nolock)  ON L.LocationID = SD.LocationID  ";
			}
			str += "  WHERE CONVERT (VARCHAR(11),ASD.Date,103)=CONVERT (VARCHAR(11),TransactionDate,103)";
			if (fromLocation != "")
			{
				str = str + " AND SD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "'  ";
			}
			str = str + " ) AS Cost,\r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN TaxAmount ELSE 0 END) AS [CashSaleTax], \r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN TaxAmount ELSE 0 END) AS [CreditSaleTax],\r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN Total+RoundOff-Discount ELSE 0 END) AS [CashSale], \r\n                            SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN Total+RoundOff-Discount ELSE 0 END) AS [CreditSale]\r\n                            FROM Sales_Invoice  SI\r\n                            INNER JOIN System_Document D with(nolock)  ON D.SysDocID=SI.SysDocID\r\n                            LEFT OUTER JOIN Location SP with(nolock)  ON D.LocationID = SP.LocationID  \r\n                            WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				str = str + " AND D.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			str += " GROUP BY CONVERT (VARCHAR(11),TransactionDate,103),  CONVERT(date, TransactionDate)  ORDER BY CONVERT(date, TransactionDate)";
			FillDataSet(dataSet, "Sales", str);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables[0].Columns["Date"]
			};
			str = "SELECT CONVERT (VARCHAR(11),TransactionDate,103) AS [Date],\r\n                    -1*SUM(Discount) AS DiscountReturn,-1*SUM(RoundOff) RoundOffReturn,-1*SUM(TaxAmount) TaxReturn,\r\n                    0.00 AS CostReturn,SUM(Total) AS [SalesReturn]\r\n                    FROM Sales_Return SI\r\n                    INNER JOIN System_Document D with(nolock)   ON D.SysDocID=SI.SysDocID\r\n                    LEFT OUTER JOIN Location SP with(nolock)   ON D.LocationID=SP.LocationID\r\n                           \r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				str = str + " AND D.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			str += " GROUP BY CONVERT (VARCHAR(11),TransactionDate,103),  CONVERT(date, TransactionDate) ORDER BY CONVERT(date, TransactionDate) ";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Sales", str);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables[0].Columns["Date"]
			};
			dataSet.Merge(dataSet2.Tables[0]);
			return dataSet;
		}

		public DataSet GetSalesByLocationDetailReport(DateTime from, DateTime to, string fromLocation, string toLocation)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT DISTINCT ISNULL(D.LocationID,'No Location') AS [LocationID],LocationName\r\n                             FROM Sales_Invoice_Detail SD \r\n                            INNER JOIN Sales_Invoice SI ON SI.SysDocID=SD.SysDocID  AND SI.VoucherID = SD.VoucherID  \r\n                            INNER JOIN System_Document D ON D.SysDocID=SI.SysDocID\r\n                            LEFT OUTER JOIN Location ON D.LocationID=Location.LocationID ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				text3 = text3 + " AND D.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			text3 += " UNION ";
			text3 += "SELECT DISTINCT ISNULL(D.LocationID,'No Location') AS [LocationID],LocationName\r\n                    FROM Sales_Return_Detail SD \r\n                    INNER JOIN Sales_Return SI ON SI.SysDocID=SD.SysDocID  AND SI.VoucherID = SD.VoucherID\r\n                    INNER JOIN System_Document D ON D.SysDocID=SI.SysDocID\r\n                    LEFT OUTER JOIN Location ON D.LocationID=Location.LocationID ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				text3 = text3 + " AND D.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Location", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "SELECT DISTINCT SI.SysDocID, SI.VoucherID, ISNULL(D.LocationID,'No Location') AS [LocationID],TransactionDate,Note,  \r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Sale' ELSE 'Cash Sale' END AS [Type], \r\n                    CurrencyID,CurrencyRate, SI.Discount, SI.DiscountFC,  (SI.Total-SI.Discount+SI.RoundOff) AS Total, (SI.TotalFC-SI.DiscountFC+SI.RoundOff) AS TotalFC \r\n                    FROM Sales_Invoice SI \r\n                    INNER JOIN System_Document D ON D.SysDocID=SI.SysDocID\r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				text3 = text3 + " AND D.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			text3 += " UNION ";
			text3 = text3 + "SELECT DISTINCT SI.SysDocID, SI.VoucherID, ISNULL(D.LocationID,'No Location') AS [LocationID],TransactionDate,Note,  \r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Return' ELSE 'Cash Return' END AS [Type], \r\n                    CurrencyID, CurrencyRate, -1 * Discount, -1 * DiscountFC,  -1 * (SI.Total-SI.Discount+SI.RoundOff) AS Total, -1 * (SI.TotalFC-SI.DiscountFC+SI.RoundOff) AS TotalFC \r\n                    FROM Sales_Return SI \r\n                    INNER JOIN System_Document D ON D.SysDocID=SI.SysDocID\r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				text3 = text3 + " AND D.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			text3 += " ORDER BY TransactionDate";
			FillDataSet(dataSet2, "Sales", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Sales Detail", dataSet.Tables["Location"].Columns["LocationID"], dataSet.Tables["Sales"].Columns["LocationID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetPurchaseByLocationSummaryReport(DateTime from, DateTime to, string fromLocation, string toLocation)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT T.LocationID,T.LocationName,SUM(T.Discount) AS Discount,SUM(T.CashSale) AS [CashSale],SUM(T.CreditSale) AS [CreditSale]  FROM (\r\n                            SELECT ISNULL(PID.LocationID,'No Location')AS [LocationID],L.LocationName,\r\n                            Discount  AS Discount, SUM(CASE ISNULL(IsCash,'False') WHEN  'True' THEN PID.Amount ELSE 0 END) \r\n                            AS [CashSale], SUM(CASE ISNULL(IsCash,'False') WHEN  'False' THEN PID.Amount ELSE 0 END) \r\n                            AS [CreditSale]\r\n                            FROM Purchase_Invoice_Detail PID\r\n                            LEFT JOIN Purchase_Invoice PN ON PID.SysDocID=PN.SysDocID AND PID.VoucherID=PN.VoucherID\r\n                            LEFT JOIN Location L ON PID.LocationID=L.LocationID\r\n                            WHERE PN.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(PN.IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				str = str + " AND PID.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			str += " GROUP BY PID.LocationID,L.LocationName,Discount)T GROUP BY T.LocationID,T.LocationName ";
			FillDataSet(dataSet, "Purchase", str);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables[0].Columns["LocationID"]
			};
			str = "SELECT ISNULL(PRD.LocationID,'No Location')AS [LocationID],L.LocationName, \r\n                    SUM(PRD.Amount) AS  [PurchaseReturn], '' AS DiscountReturn \r\n                    FROM Purchase_Return_Detail PRD LEFT JOIN Purchase_Return PR ON PRD.SysDocID=PR.SysDocID AND PRD.VoucherID=PR.VoucherID\r\n                    LEFT JOIN LOCATION L ON L.LocationID=PRD.LocationID\r\n\r\n                    WHERE PR.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND ISNULL(PR.IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				str = str + " AND PRD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			str += " GROUP BY PRD.LocationID,L.LocationName Order BY PRD.LocationID";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Purchase", str);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables[0].Columns["LocationID"]
			};
			dataSet.Merge(dataSet2.Tables[0]);
			return dataSet;
		}

		public DataSet GetPurchaseByLocationDetailReport(DateTime from, DateTime to, string fromLocation, string toLocation)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "Select DISTINCT ISNULL(PID.LocationID,'No Location') AS [LocationID],LocationName\r\n                                FROM Purchase_Invoice_Detail PID \r\n                                INNER JOIN Purchase_Invoice SI ON SI.SysDocID=PID.SysDocID AND SI.VoucherID=PID.VoucherID\r\n                                LEFT OUTER JOIN Location ON PID.LocationID=Location.LocationID ";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				text3 = text3 + " AND PID.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			text3 += " UNION ";
			text3 += "Select DISTINCT ISNULL(PRD.LocationID,'No Location') AS [LocationID],LocationName\r\n                    FROM Purchase_Return_Detail PRD \r\n                    INNER JOIN Purchase_Return SI ON SI.SysDocID=PRD.SysDocID  AND SI.VoucherID=PRD.VoucherID\r\n                    LEFT OUTER JOIN Location ON PRD.LocationID=Location.LocationID";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				text3 = text3 + " AND PRD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			text3 += " ORDER BY LocationID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Location", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "Select DISTINCT SI.SysDocID,SI.VoucherID,ISNULL(LocationID,'No Location') AS [LocationID],TransactionDate,Note,  \r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Purchase' ELSE 'Cash Purchase' END AS [Type], \r\n                    CurrencyID,CurrencyRate, SUM( PID.Amount) AS Total ,SUM(PID.AmountFC) AS TotalFC,Discount,DiscountFC\r\n                    FROM Purchase_Invoice SI LEFT JOIN Purchase_Invoice_Detail PID ON SI.SysDocID=PID.SysDocID AND SI.VoucherID=PID.VoucherID\r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				text3 = text3 + " AND LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			text3 += "GROUP BY SI.SysDocID,SI.VoucherID,LocationID,TransactionDate,Note,IsCash,CurrencyID,CurrencyRate,Discount,DiscountFC UNION ";
			text3 = text3 + "Select SI.SysDocID,SI.VoucherID,ISNULL(LocationID,'No Location') AS [LocationID],TransactionDate,Note,  \r\n                    Case ISNULL(IsCash,'False') WHEN 'False' THEN 'Credit Return' ELSE 'Cash Return' END AS [Type], \r\n                    CurrencyID,CurrencyRate,-1*Discount,-1*DiscountFC,  -1 * SUM(PRD.Amount) AS Total, -1 * SUM(PRD.AmountFC) TotalFC \r\n                    FROM Purchase_Return SI INNER JOIN Purchase_Return_Detail PRD ON SI.SysDocID=PRD.SysDocID  AND SI.VoucherID=PRD.VoucherID\r\n                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(IsVoid,'False')='False' ";
			if (fromLocation != "")
			{
				text3 = text3 + " AND LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			text3 += "GROUP BY SI.SysDocID,SI.VoucherID,LocationID,TransactionDate,Note,IsCash,CurrencyID,CurrencyRate,Discount,DiscountFC ORDER BY TransactionDate";
			FillDataSet(dataSet2, "Purchase", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Purchase Detail", dataSet.Tables["Location"].Columns["LocationID"], dataSet.Tables["Purchase"].Columns["LocationID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetLocationsByUser(string userID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT L.LocationID,LocationName, \r\n                                CASE (SELECT COUNT(LocationID) FROM User_Location_Detail ULD2 WHERE UserID='" + userID + "' AND \r\n                                ULD2.LocationID=ULD.LocationID) WHEN  0 THEN 'False' ELSE 'True' END  AS IsMember\r\n                                FROM Location L LEFT OUTER JOIN User_Location_Detail ULD \r\n                                ON L.LocationID=ULD.LocationID AND ULD.UserID='" + userID + "'";
			FillDataSet(dataSet, "User_Location_Detail", textCommand);
			return dataSet;
		}

		public bool InsertUserLocationDetails(string entityID, LocationData userLocationData, bool byUser)
		{
			bool flag = true;
			SqlCommand detailsInsertUpdateCommand = GetDetailsInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				if (entityID == "")
				{
					throw new CompanyException("User or Location is not provided.");
				}
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = "";
				if (byUser)
				{
					text = "DELETE FROM User_Location_Detail WHERE UserID = '" + entityID + "'";
					flag &= Delete(text, sqlTransaction);
				}
				else
				{
					text = "DELETE FROM User_Location_Detail WHERE LocationID = '" + entityID + "'";
					flag &= Delete(text, sqlTransaction);
				}
				if (userLocationData.UserLocationDetailTable.Rows.Count <= 0)
				{
					return flag;
				}
				detailsInsertUpdateCommand.Transaction = sqlTransaction;
				flag &= Insert(userLocationData, "User_Location_Detail", detailsInsertUpdateCommand);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		private string GetDetailsInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("User_Location_Detail", new FieldValue("LocationID", "@LocationID"), new FieldValue("UserID", "@UserID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetDetailsInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetDetailsInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetDetailsInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@UserID"].SourceColumn = "UserID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}
	}
}
