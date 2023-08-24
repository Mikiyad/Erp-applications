using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class MaintenanceEntry : StoreObject
	{
		public const string SYSDOCID_PARM = "@SysDocID";

		public const string DOCID_PARM = "@VoucherID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string MAINTENANCEENTRY_TABLE = "Vehicle_Maintenance_Entry";

		private const string MAINTENANCEENTRYDETAIL_TABLE = "Vehicle_Maintenance_Entry_Detail";

		public const string VEHICLENUMBER_PARM = "@VehicleNumber";

		public const string MAINTENANCEDATE_PARM = "@MaintenanceDate";

		public const string ODOMETER_PARM = "@Odometer";

		public const string SERVICETYPE_PARM = "@ServiceType";

		public const string SERVICEPROVIDER_PARM = "@ServiceProvider";

		public const string SOURCESYDOCID_PARM = "@SourceSysDocID";

		public const string SOURCEDOCID_PARAM = "@SourceVoucherID";

		public const string NEXTSCHEDULESTATUS_PARAM = "@NextServiceScheduleStatus";

		public const string REQUIREDTIME_PARM = "@TimeRequired";

		public const string ISVOID_PARM = "@IsVoid";

		private const string VENDORID_PARM = "@VendorID";

		private const string ISIMPORT_PARM = "@IsImport";

		private const string PURCHASEFLOW_PARM = "@PurchaseFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string BUYERID_PARM = "@BuyerID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string TERMID_PARM = "@TermID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string SOURCEDOCTYPE_PARM = "@ItemSourceTypes";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string DISCOUNT_PARM = "@Discount";

		private const string DISCOUNTFC_PARM = "@DiscountFC";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TOTAL_PARM = "@Total";

		private const string TOTALFC_PARM = "@TotalFC";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string ISCASH_PARM = "@IsCash";

		private const string DUEDATE_PARM = "@DueDate";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITPRICEFC_PARM = "@UnitPriceFC";

		private const string LCOST_PARM = "@LCost";

		private const string LCOSTAMOUNT_PARM = "@LCostAmount";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string RATETYPE_PARM = "@RateType";

		private const string JOBID_PARM = "@JobID";

		public MaintenanceEntry(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateMaintenanceEntryText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Vehicle_Maintenance_Entry", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("VehicleNumber", "@VehicleNumber"), new FieldValue("TimeRequired", "@TimeRequired"), new FieldValue("CreatedBy", "@CreatedBy"), new FieldValue("DateCreated", "@DateCreated"), new FieldValue("ServiceProvider", "@ServiceProvider"), new FieldValue("ServiceType", "@ServiceType"), new FieldValue("Status", "@Status"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("NextServiceScheduleStatus", "@NextServiceScheduleStatus"), new FieldValue("Odometer", "@Odometer"), new FieldValue("IsVoid", "@IsVoid"), new FieldValue("Amount", "@Amount"), new FieldValue("VendorID", "@VendorID"), new FieldValue("PurchaseFlow", "@PurchaseFlow"), new FieldValue("DueDate", "@DueDate"), new FieldValue("IsImport", "@IsImport"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("SourceDocType", "@ItemSourceTypes"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("IsCash", "@IsCash"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Vehicle_Maintenance_Entry", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateMaintenanceEntryCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateMaintenanceEntryText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateMaintenanceEntryText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@VehicleNumber", SqlDbType.NVarChar);
			parameters.Add("@CreatedBy", SqlDbType.NVarChar);
			parameters.Add("@DateCreated", SqlDbType.DateTime);
			parameters.Add("@Odometer", SqlDbType.NVarChar);
			parameters.Add("@ServiceType", SqlDbType.NVarChar);
			parameters.Add("@ServiceProvider", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.NVarChar);
			parameters.Add("@TimeRequired", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.NVarChar);
			parameters.Add("@NextServiceScheduleStatus", SqlDbType.Bit);
			parameters.Add("@IsVoid", SqlDbType.Bit);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@IsImport", SqlDbType.Bit);
			parameters.Add("@PurchaseFlow", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@BuyerID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@ItemSourceTypes", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@DiscountFC", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@TotalFC", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@IsCash", SqlDbType.Bit);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
			}
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@VehicleNumber"].SourceColumn = "VehicleNumber";
			parameters["@CreatedBy"].SourceColumn = "CreatedBy";
			parameters["@DateCreated"].SourceColumn = "DateCreated";
			parameters["@Odometer"].SourceColumn = "Odometer";
			parameters["@ServiceType"].SourceColumn = "ServiceType";
			parameters["@ServiceProvider"].SourceColumn = "ServiceProvider";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@TimeRequired"].SourceColumn = "TimeRequired";
			parameters["@IsVoid"].SourceColumn = "IsVoid";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@TimeRequired"].SourceColumn = "TimeRequired";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@NextServiceScheduleStatus"].SourceColumn = "NextServiceScheduleStatus";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@PurchaseFlow"].SourceColumn = "PurchaseFlow";
			parameters["@IsImport"].SourceColumn = "IsImport";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@ItemSourceTypes"].SourceColumn = "SourceDocType";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@DiscountFC"].SourceColumn = "DiscountFC";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@TotalFC"].SourceColumn = "TotalFC";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@IsCash"].SourceColumn = "IsCash";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@DueDate"].SourceColumn = "DueDate";
			if (isUpdate)
			{
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateMaintenanceEntryDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Vehicle_Maintenance_Entry_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("LCost", "@LCost"), new FieldValue("LCostAmount", "@LCostAmount"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("JobID", "@JobID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("RateType", "@RateType"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("CurrencyRate", "@CurrencyRate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateVehicleMaintenanceEntryDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateMaintenanceEntryDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateMaintenanceEntryDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@UnitPriceFC", SqlDbType.Decimal);
			parameters.Add("@LCost", SqlDbType.Decimal);
			parameters.Add("@LCostAmount", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@AmountFC", SqlDbType.Decimal);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@RateType", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@UnitPriceFC"].SourceColumn = "UnitPriceFC";
			parameters["@LCost"].SourceColumn = "LCost";
			parameters["@LCostAmount"].SourceColumn = "LCostAmount";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@RateType"].SourceColumn = "RateType";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(VehicleMaintenanceEntryData journalData)
		{
			return true;
		}

		internal bool DeleteMaintenanceEntryDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				VehicleMaintenanceEntryData dataSet = new VehicleMaintenanceEntryData();
				string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid,ISNULL(IsCash,'False') AS IsCash, ISNULL(IsImport,'False') AS IsImport FROM Vehicle_Maintenance_Entry_Detail SOD \r\n                                INNER JOIN Vehicle_Maintenance_Entry SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n\t\t\t\t\t\t\t  WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Purchase_Invoice_Detail", textCommand, sqlTransaction);
				textCommand = "DELETE FROM Vehicle_Maintenance_Entry_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool InsertUpdateMaintenanceEntry(VehicleMaintenanceEntryData VehicleMaintenanceEntryData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateMaintenanceEntryCommand = GetInsertUpdateMaintenanceEntryCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				DataRow dataRow = VehicleMaintenanceEntryData.MaintenanceEntryTable.Rows[0];
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string text3 = dataRow["SourceSysDocID"].ToString();
				string text4 = dataRow["SourceVoucherID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Vehicle_Maintenance_Entry", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in VehicleMaintenanceEntryData.MaintenanceEntryDetailTable.Rows)
				{
					row["VoucherID"] = dataRow["VoucherID"];
					row["SysDocID"] = dataRow["SysDocID"];
					string serviceItemID = row["ProductID"].ToString();
					if (new ServiceItem(base.DBConfig).GetServiceItemAccountID(serviceItemID) == "")
					{
						throw new CompanyException("Account ID not set for Service Item.");
					}
				}
				if (isUpdate)
				{
					flag &= DeleteMaintenanceEntryDetailsRows(text2, text, sqlTransaction);
				}
				insertUpdateMaintenanceEntryCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(VehicleMaintenanceEntryData, "Vehicle_Maintenance_Entry", insertUpdateMaintenanceEntryCommand)) : (flag & Insert(VehicleMaintenanceEntryData, "Vehicle_Maintenance_Entry", insertUpdateMaintenanceEntryCommand)));
				if (!flag)
				{
					throw new CompanyException("Faild to save the transaction. Please reload the transaction and try again.");
				}
				if (VehicleMaintenanceEntryData.MaintenanceEntryDetailTable.Rows.Count > 0)
				{
					insertUpdateMaintenanceEntryCommand = GetInsertUpdateVehicleMaintenanceEntryDetailsCommand(isUpdate: false);
					insertUpdateMaintenanceEntryCommand.Transaction = sqlTransaction;
					flag &= Insert(VehicleMaintenanceEntryData, "Vehicle_Maintenance_Entry_Detail", insertUpdateMaintenanceEntryCommand);
				}
				if (flag && text3 != null && text4 != null && text4 != "" && text3 != "")
				{
					flag &= new MaintenanceScheduler(base.DBConfig).UpdateMaintenanceStatus(text3, text4, 1);
				}
				if (isUpdate)
				{
					string text5 = dataRow["Reference"].ToString();
					if (text5 != "")
					{
						flag &= new MaintenanceScheduler(base.DBConfig).UpdateMaintenanceStatus("", text5, 1);
					}
				}
				if (VehicleMaintenanceEntryData.Tables.Contains("Tax_Detail") && VehicleMaintenanceEntryData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(VehicleMaintenanceEntryData, text2, text, isUpdate, sqlTransaction);
				}
				GLData journalData = CreateMaintenanceEntryGLData(VehicleMaintenanceEntryData);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Vehicle_Maintenance_Entry", "SysDocID", text2, "VoucherID", text, sqlTransaction, isInsert: false);
				string entityName = "Vehicle Maintenance Entry";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Vehicle_Maintenance_Entry", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.MaintenanceEntry, text2, text, "Vehicle_Maintenance_Entry", sqlTransaction);
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

		private GLData CreateMaintenanceEntryGLData(VehicleMaintenanceEntryData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.MaintenanceEntryTable.Rows[0];
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			string text = dataRow["VendorID"].ToString();
			string text2 = dataRow["SysDocID"].ToString();
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string textCommand = "SELECT SD.LocationID,ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID ,LOC.COGSAccountID,LOC.DiscountReceivedAccountID,\r\n                                LOC.InventoryAccountID,LOC.SalesAccountID,LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Vendor VEN ON VendorID='" + text + "'\r\n                                 LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Accounts", textCommand);
			DataRow dataRow3 = dataSet.Tables["Accounts"].Rows[0];
			dataRow3["LocationID"].ToString();
			string text3 = dataRow3["DiscountReceivedAccountID"].ToString();
			dataRow3["SalesTaxAccountID"].ToString();
			string value = dataRow3["APAccountID"].ToString();
			SysDocTypes sysDocTypes = SysDocTypes.MaintenanceEntry;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["CurrencyID"] = dataRow["CurrencyID"];
			dataRow2["CurrencyRate"] = dataRow["CurrencyRate"];
			dataRow2["Note"] = dataRow["Reference"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			decimal d = default(decimal);
			decimal d2 = default(decimal);
			DataRow dataRow4 = null;
			foreach (DataRow row in transactionData.MaintenanceEntryDetailTable.Rows)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				string serviceItemID = row["ProductID"].ToString();
				string serviceItemAccountID = new ServiceItem(base.DBConfig).GetServiceItemAccountID(serviceItemID);
				dataRow4["IsARAP"] = false;
				dataRow4["PayeeType"] = "";
				dataRow4["PayeeID"] = row["ProductID"];
				if (serviceItemAccountID == "")
				{
					throw new CompanyException("Account ID not set for Service Item.");
				}
				dataRow4["AccountID"] = serviceItemAccountID;
				dataRow4["Debit"] = row["Amount"];
				dataRow4["DebitFC"] = row["AmountFC"];
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
				d += decimal.Parse(row["Amount"].ToString());
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4["RowIndex"] = row["RowIndex"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			if (d < 0m)
			{
				throw new CompanyException("Total amount cannot be negative.");
			}
			dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			string providerID = dataRow["VendorID"].ToString();
			new Vendors(base.DBConfig).GetServiceProviderAccountID(providerID);
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			if (dataRow["DiscountFC"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["DiscountFC"].ToString(), out result);
			}
			else
			{
				decimal.TryParse(dataRow["Discount"].ToString(), out result);
			}
			if (dataRow["TaxAmountFC"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["TaxAmountFC"].ToString(), out result2);
			}
			else
			{
				decimal.TryParse(dataRow["TaxAmount"].ToString(), out result2);
			}
			dataRow4["JournalID"] = 0;
			dataRow4["PayeeType"] = "V";
			dataRow4["PayeeID"] = dataRow["VendorID"];
			dataRow4["IsARAP"] = true;
			dataRow4["AccountID"] = value;
			dataRow4["Debit"] = DBNull.Value;
			dataRow4["DebitFC"] = DBNull.Value;
			dataRow4["Credit"] = d + d2 + result2 - result;
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4["RowIndex"] = -1;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			if (result > 0m)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				if (text3 == "")
				{
					throw new CompanyException("You have entered a discount amount for this transaction but there is no discount account selected for this location.\nPlease select the 'Discount Received' account for this location.", 1040);
				}
				dataRow4["AccountID"] = text3;
				dataRow4["PayeeID"] = text;
				dataRow4["PayeeType"] = "A";
				dataRow4["JobID"] = "";
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["Credit"] = result;
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			if (result2 > 0m)
			{
				if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
				{
					throw new CompanyException("Tax details not found for the transaction.");
				}
				DataRow[] array = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
				decimal num = default(decimal);
				for (int i = 0; i < array.Length; i++)
				{
					num = default(decimal);
					DataRow obj = array[i];
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					string text4 = "";
					text4 = obj["TaxItemID"].ToString();
					string text5 = "";
					textCommand = "SELECT PurchaseTaxAccountID FROM Tax WHERE  TaxCode = '" + text4.Trim() + "'";
					object obj2 = ExecuteScalar(textCommand);
					if (obj2 != null)
					{
						text5 = obj2.ToString();
					}
					if (text5 == "")
					{
						throw new CompanyException("Purchase tax account is not set for tax item: " + text4 + ".");
					}
					decimal.TryParse(obj["TaxAmount"].ToString(), out num);
					dataRow4["AccountID"] = text5;
					dataRow4["PayeeID"] = text;
					dataRow4["PayeeType"] = "A";
					dataRow4["Debit"] = Math.Round(num, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
			}
			return gLData;
		}

		public VehicleMaintenanceEntryData GetMaintenanceEntryByID(string sysDocID, string voucherID)
		{
			try
			{
				VehicleMaintenanceEntryData vehicleMaintenanceEntryData = new VehicleMaintenanceEntryData();
				string textCommand = "SELECT * FROM Vehicle_Maintenance_Entry WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(vehicleMaintenanceEntryData, "Vehicle_Maintenance_Entry", textCommand);
				if (vehicleMaintenanceEntryData == null || vehicleMaintenanceEntryData.Tables.Count == 0 || vehicleMaintenanceEntryData.Tables["Vehicle_Maintenance_Entry"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*, SI.Description\r\n\t\t\t\t\t\tFROM Vehicle_Maintenance_Entry_Detail TD INNER JOIN Service_Item SI ON TD.ProductID=SI.ServiceItemID\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(vehicleMaintenanceEntryData, "Vehicle_Maintenance_Entry_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(vehicleMaintenanceEntryData, "Tax_Detail", textCommand);
				return vehicleMaintenanceEntryData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPreviousOdometerValue(string VehicleID)
		{
			try
			{
				VehicleMaintenanceEntryData vehicleMaintenanceEntryData = new VehicleMaintenanceEntryData();
				string textCommand = "SELECT TOP 1 * FROM Vehicle_Maintenance_Entry WHERE VehicleNumber='" + VehicleID + "' order By TransactionDate desc ";
				FillDataSet(vehicleMaintenanceEntryData, "Vehicle_Maintenance_Entry", textCommand);
				if (vehicleMaintenanceEntryData == null || vehicleMaintenanceEntryData.Tables.Count == 0 || vehicleMaintenanceEntryData.Tables["Vehicle_Maintenance_Entry"].Rows.Count == 0)
				{
					return null;
				}
				return vehicleMaintenanceEntryData;
			}
			catch
			{
				throw;
			}
		}

		public VehicleMaintenanceEntryData GetMaintenanceScheduleBySourceID(string sysDocID, string voucherID)
		{
			try
			{
				VehicleMaintenanceEntryData vehicleMaintenanceEntryData = new VehicleMaintenanceEntryData();
				string textCommand = "SELECT * FROM Vehicle_Maintenance_Entry WHERE SourceSysDocID='" + sysDocID + "' AND SourceVoucherID='" + voucherID + "'";
				FillDataSet(vehicleMaintenanceEntryData, "Vehicle_Maintenance_Entry", textCommand);
				if (vehicleMaintenanceEntryData == null || vehicleMaintenanceEntryData.Tables.Count == 0 || vehicleMaintenanceEntryData.Tables["Vehicle_Maintenance_Entry"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT *\r\n                        FROM Vehicle_Maintenance_Entry WHERE SourceSysDocID='" + sysDocID + "' AND SourceVoucherID='" + voucherID + "'";
				FillDataSet(vehicleMaintenanceEntryData, "Vehicle_Maintenance_Entry", textCommand);
				return vehicleMaintenanceEntryData;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteMaintenanceEntry(string sysdocid, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				DataSet dataSet = new DataSet();
				text = "select * from  Vehicle_Maintenance_Entry where SysDocID = '" + sysdocid + "' AND VoucherID ='" + voucherID + "'";
				SqlCommand sqlCommand = new SqlCommand(text);
				FillDataSet(dataSet, "Vehicle_Maintenance_Entry", sqlCommand);
				if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables["Vehicle_Maintenance_Entry"].Rows[0];
					text2 = dataRow["SourceSysDocID"].ToString();
					text3 = dataRow["SourceVoucherID"].ToString();
					text4 = dataRow["Reference"].ToString();
				}
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteMaintenanceEntryDetailsRows(sysdocid, voucherID, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysdocid, voucherID, sqlTransaction);
				text = "DELETE FROM Vehicle_Maintenance_Entry WHERE VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				if (text2 != "" && text3 != "")
				{
					flag = new MaintenanceScheduler(base.DBConfig).UpdateMaintenanceStatus(text2, text3, 0);
				}
				else if (text4 != "")
				{
					flag = new MaintenanceScheduler(base.DBConfig).UpdateMaintenanceStatus("", text4, 0);
				}
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("VEHICLE MAINTENANCE ENTRY", voucherID, activityType, sqlTransaction);
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

		public DataSet GetMaintenanceEntryToPrint(string sysDocID, string[] voucherID)
		{
			try
			{
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				DataSet dataSet = new DataSet();
				string cmdText = "SELECT MA.SysDocID, MA.VoucherID,MA.Amount,MA.Odometer,MA.ServiceProvider,VR.VendorName,v.VehicleName,\r\n                               MA.Status,ma.TransactionDate, ma.VehicleNumber,ma.CreatedBy,ma.DateUpdated\r\n                                FROM    Vehicle_Maintenance_Entry MA LEFT JOIN Service_Item SA ON SA.ServiceItemID=MA.ServiceType LEFT JOIN \r\n                                Vehicle V ON MA.VehicleNumber = V.VehicleID INNER JOIN VENDOR VR ON VR.VendorID=MA.VendorID  \r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Vehicle_Maintenance_Entry", sqlCommand);
				cmdText = "SELECT     SysDocID,VoucherID,PID.ProductID as ServiceItemID,PID.Description,ISNULL(UnitQuantity,PID.Quantity) AS Quantity,SI.Description,\r\n                        CASE  SI.ServiceType WHEN 0 THEN 'NONE' WHEN 1 THEN  'REPAIR' WHEN 2 THEN 'MAINTENANCE' WHEN 3 THEN 'INSPECTION' \r\n                                WHEN 4 THEN 'RENEWALS' END AS ServiceType,SI.RepeatCountDays, SI.RepeatCountKM,\r\n\t\t\t\t\t\tISNULL(UnitPriceFC,PID.UnitPrice) AS UnitPrice,UnitPrice as UnitPriceLC,\r\n\t\t\t\t\t\tISNULL(UnitQuantity,PID.Quantity)*ISNULL(UnitPriceFC,PID.UnitPrice) AS Total\r\n                        \r\n\t\t\t\t\t\tFROM   Vehicle_Maintenance_Entry_Detail PID LEFT JOIN Service_Item SI ON SI.ServiceItemID=PID.ProductID \r\n\t\t\t\t\t\t\r\n\t\t\t\t\t\tWHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Vehicle_Maintenance_Entry_Detail", cmdText);
				dataSet.Relations.Add("MaintenanceEntry", new DataColumn[2]
				{
					dataSet.Tables["Vehicle_Maintenance_Entry"].Columns["SysDocID"],
					dataSet.Tables["Vehicle_Maintenance_Entry"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Vehicle_Maintenance_Entry_Detail"].Columns["SysDocID"],
					dataSet.Tables["Vehicle_Maintenance_Entry_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetMaintenanceEntryReport(DateTime From, DateTime To, string fromVehicle, string toVehicle, string fromServiceItem, string toServiceItem)
		{
			string text = CommonLib.ToSqlDateTimeString(From);
			string text2 = CommonLib.ToSqlDateTimeString(To);
			string text3 = "SELECT MA.SysDocID, MA.VoucherID,MA.Amount,MA.Odometer,MA.VendorID,VR.VendorName,v.VehicleName,\r\n                               MA.Status,ma.TransactionDate, ma.VehicleNumber,ma.CreatedBy,ma.DateUpdated\r\n                                FROM    Vehicle_Maintenance_Entry MA LEFT JOIN Service_Item SA ON SA.ServiceItemID=MA.ServiceType LEFT JOIN \r\n                                Vehicle V ON MA.VehicleNumber = V.VehicleID INNER JOIN VENDOR VR ON VR.VendorID=MA.VendorID ";
			text3 = text3 + " where  MA.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  ";
			if (fromVehicle != "")
			{
				text3 = text3 + " AND MA.VehicleNumber='" + fromVehicle + "'";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vehicle_Maintenance_Entry", text3);
			text3 = "SELECT     PID.SysDocID,PID.VoucherID,PID.ProductID as ServiceItemID,VM.VehicleNumber, PID.Description,ISNULL(UnitQuantity,PID.Quantity) AS Quantity,SI.Description, \r\n                        CASE  SI.ServiceType WHEN 0 THEN 'NONE' WHEN 1 THEN  'REPAIR' WHEN 2 THEN 'MAINTENANCE' WHEN 3 THEN 'INSPECTION' \r\n                                WHEN 4 THEN 'RENEWALS' END AS ServiceType,SI.RepeatCountDays, SI.RepeatCountKM,\r\n\t\t\t\t\t\tISNULL(UnitPriceFC,PID.UnitPrice) AS UnitPrice,UnitPrice as UnitPriceLC,\r\n\t\t\t\t\t\tISNULL(UnitQuantity,PID.Quantity)*ISNULL(UnitPriceFC,PID.UnitPrice) AS Total\r\n                        \r\n\t\t\t\t\t\tFROM   Vehicle_Maintenance_Entry_Detail PID LEFT JOIN Service_Item SI ON SI.ServiceItemID=PID.ProductID\r\n\r\n\t\t\t\t\t\tLEFT JOIN Vehicle_Maintenance_Entry VM ON PID.SysDocID=VM.SysDocID and PID.VoucherID=VM.VoucherID ";
			text3 = text3 + " where  VM.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  ";
			if (fromVehicle != "")
			{
				text3 = text3 + " AND VM.VehicleNumber='" + fromVehicle + "'";
			}
			FillDataSet(dataSet, "Vehicle_Maintenance_Entry_Detail", text3);
			return dataSet;
		}

		public DataSet GetMaintenanceEntrySummary(string vehicle)
		{
			string text = "SELECT MA.SysDocID [Doc ID], MA.VoucherID [Number],MA.Amount,MA.Odometer,MA.ServiceProvider,CASE  MA.ServiceType WHEN 0 THEN 'NONE' WHEN 1 THEN  'REPAIR' WHEN 2 THEN 'MAINTENANCE' WHEN 3 THEN 'INSPECTION' WHEN 4 THEN 'RENEWALS' END AS 'Service Type',MA.Status,MA.MaintenanceDate, ma.VehicleNumber,ma.CreatedBy,ma.DateUpdated\r\n                               FROM    Vehicle_Maintenance_Entry MA LEFT JOIN\r\n                         Vehicle V ON MA.VehicleNumber = V.VehicleID INNER JOIN VENDOR VR ON VR.VendorID=MA.ServiceProvider where ISNULL(IsVoid,'False')='False' ";
			if (vehicle != "")
			{
				text = text + " AND MA.VehicleNumber='" + vehicle + "'";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Vehicle_Maintenance_Entry", text);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT SysDocID,VoucherID,VendorName AS Supplier,VH.RegistrationNumber AS [Vehicle], Total AS Amount,TransactionDate\r\n\r\n\r\n                             FROM Vehicle_Maintenance_Entry VME LEFT JOIN  Vendor V On VME.VendorID=V.VendorID\r\n\r\n                             LEFT JOIN Vehicle VH On VME.VehicleNumber=VH.VehicleID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Vehicle_Maintenance_Entry", sqlCommand);
			return dataSet;
		}

		public bool VoidMaintenance(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				VehicleMaintenanceEntryData dataSet = new VehicleMaintenanceEntryData();
				string textCommand = "SELECT * FROM Vehicle_Maintenance_Entry\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Vehicle_Maintenance_Entry", textCommand);
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				textCommand = "UPDATE Vehicle_Maintenance_Entry SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Vehicle Maintenance  Entry", voucherID, sysDocID, activityType, sqlTransaction);
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
	}
}
