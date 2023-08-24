using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PropertyRent : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string PROPERTYID_PARM = "@PropertyID";

		private const string UNITID_PARM = "@UnitID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string CONTRACTSTARTDATE_PARM = "@ContractStartDate";

		private const string NOTE_PARM = "@Note";

		private const string CONTRACTENDDATE_PARM = "@ContractEndDate";

		private const string TOTALDAYS_PARM = "@TotalDays";

		private const string NOOFINSTALLMENTS_PARM = "@NoofInstallments";

		private const string STATUS_PARM = "@Status";

		private const string AGREEMENTTYPE_PARM = "@AgreementType";

		private const string AGREEMENTSTATUS_PARM = "@AgreementStatus";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string PARENTSYSDOCID_PARM = "@ParentSysDocID";

		private const string PARENTVOUCHERID_PARM = "@ParentVoucherID";

		private const string TOTAL_PARAM = "@Total";

		private const string PROPERTYAGENT_PARAM = "@PropertyAgentID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public const string INCOMEID_PARM = "@IncomeID";

		public const string AMOUNT_PARM = "@Amount";

		public const string RATETYPE_PARM = "@RateType";

		public const string DESCRIPTION_PARM = "@Description";

		public const string ROWINDEX_PARM = "@RowIndex";

		public const string REFERENCE_PARM = "@Reference";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string DISCOUNT_PARM = "@Discount";

		private const string ISPERIODICINVOICE_PARM = "@IsPeriodicInvoice";

		private const string INVOICESTARTDATE_PARM = "@InvoiceStartDate";

		private const string FREQUENCY_PARM = "@Frequency";

		private const string FREQUENCYCOUNT_PARM = "@FrequencyCount";

		public PropertyRent(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePropertyRentText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Rent", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("ContractStartDate", "@ContractStartDate"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("PropertyID", "@PropertyID"), new FieldValue("UnitID", "@UnitID"), new FieldValue("Status", "@Status"), new FieldValue("AgreementType", "@AgreementType"), new FieldValue("AgreementStatus", "@AgreementStatus"), new FieldValue("Note", "@Note"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("ContractEndDate", "@ContractEndDate"), new FieldValue("TotalDays", "@TotalDays"), new FieldValue("NoofInstallments", "@NoofInstallments"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("ParentSysDocID", "@ParentSysDocID"), new FieldValue("ParentVoucherID", "@ParentVoucherID"), new FieldValue("PropertyAgentID", "@PropertyAgentID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("IsPeriodicInvoice", "@IsPeriodicInvoice"), new FieldValue("InvoiceStartDate", "@InvoiceStartDate"), new FieldValue("Frequency", "@Frequency"), new FieldValue("FrequencyCount", "@FrequencyCount"), new FieldValue("Total", "@Total"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_Rent", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePropertyRentCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePropertyRentText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePropertyRentText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ContractStartDate", SqlDbType.DateTime);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@PropertyID", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@AgreementType", SqlDbType.NVarChar);
			parameters.Add("@AgreementStatus", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@ContractEndDate", SqlDbType.DateTime);
			parameters.Add("@TotalDays", SqlDbType.Int);
			parameters.Add("@NoofInstallments", SqlDbType.Int);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ParentSysDocID", SqlDbType.NVarChar);
			parameters.Add("@ParentVoucherID", SqlDbType.NVarChar);
			parameters.Add("@PropertyAgentID", SqlDbType.NVarChar);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@IsPeriodicInvoice", SqlDbType.Bit);
			parameters.Add("@InvoiceStartDate", SqlDbType.DateTime);
			parameters.Add("@FrequencyCount", SqlDbType.TinyInt);
			parameters.Add("@Frequency", SqlDbType.Char);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ContractStartDate"].SourceColumn = "ContractStartDate";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@PropertyID"].SourceColumn = "PropertyID";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@AgreementType"].SourceColumn = "AgreementType";
			parameters["@AgreementStatus"].SourceColumn = "AgreementStatus";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@ContractEndDate"].SourceColumn = "ContractEndDate";
			parameters["@TotalDays"].SourceColumn = "TotalDays";
			parameters["@NoofInstallments"].SourceColumn = "NoofInstallments";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@ParentSysDocID"].SourceColumn = "ParentSysDocID";
			parameters["@ParentVoucherID"].SourceColumn = "ParentVoucherID";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@PropertyAgentID"].SourceColumn = "PropertyAgentID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@IsPeriodicInvoice"].SourceColumn = "IsPeriodicInvoice";
			parameters["@InvoiceStartDate"].SourceColumn = "InvoiceStartDate";
			parameters["@Frequency"].SourceColumn = "Frequency";
			parameters["@FrequencyCount"].SourceColumn = "FrequencyCount";
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

		private string GetInsertUpdatePropertyRentDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Rent_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("IncomeID", "@IncomeID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Discount", "@Discount"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Reference", "@Reference"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePropertyRentDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePropertyRentDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePropertyRentDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@IncomeID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@IncomeID"].SourceColumn = "IncomeID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(PropertyRentData journalData)
		{
			return true;
		}

		public bool InsertUpdatePropertyRent(PropertyRentData propertyRentData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePropertyRentCommand = GetInsertUpdatePropertyRentCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = propertyRentData.PropertyRentTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Property_Rent", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in propertyRentData.PropertyRentDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				foreach (DataRow row2 in propertyRentData.PropertyRentDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					string a = row2["CurrencyID"].ToString();
					if (a != "" && a != baseCurrencyID)
					{
						decimal d = decimal.Parse(row2["Amount"].ToString());
						decimal result = 1m;
						decimal.TryParse(row2["CurrencyRate"].ToString(), out result);
						d = ((!(row2["RateType"].ToString() == "M")) ? Math.Round(d / result, currencyDecimalPoints) : Math.Round(d * result, currencyDecimalPoints));
						row2["Amount"] = d;
					}
					else
					{
						row2["CurrencyRate"] = 1;
					}
				}
				if (isUpdate)
				{
					flag &= DeletePropertyRentDetailsRows(text2, text, isDeletingTransaction: false, sqlTransaction);
				}
				insertUpdatePropertyRentCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(propertyRentData, "Property_Rent", insertUpdatePropertyRentCommand)) : (flag & Insert(propertyRentData, "Property_Rent", insertUpdatePropertyRentCommand)));
				insertUpdatePropertyRentCommand = GetInsertUpdatePropertyRentDetailsCommand(isUpdate: false);
				insertUpdatePropertyRentCommand.Transaction = sqlTransaction;
				if (propertyRentData.Tables["Property_Rent_Detail"].Rows.Count > 0)
				{
					flag &= Insert(propertyRentData, "Property_Rent_Detail", insertUpdatePropertyRentCommand);
				}
				string textCommand = "SELECT * FROM Property_Rent  WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Property_Rent", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException(" no data.");
				}
				DataRow dataRow3 = dataSet.Tables["Property_Rent"].Rows[0];
				string text3 = "";
				string text4 = dataRow3["ParentSysDocID"].ToString();
				string text5 = dataRow3["ParentVoucherID"].ToString();
				string text6 = dataRow3["SourceSysDocID"].ToString();
				text3 = dataRow3["SourceVoucherID"].ToString();
				string text7 = "";
				text7 = dataRow3["AgreementType"].ToString();
				if (text6 != "" && text3 != "")
				{
					textCommand = "UPDATE Property_Rent SET ParentSysDocID='" + text6 + "',ParentVoucherID='" + text3 + "' WHERE SysDocID='" + text2 + "' AND VoucherID='" + text + "'";
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				}
				textCommand = "UPDATE Property_Rent SET AgreementStatus=2 WHERE SysDocID='" + text6 + "' AND VoucherID='" + text3 + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				if (text7 == "2")
				{
					textCommand = "SELECT * FROM Property_Rent  WHERE SysDocID = '" + text6 + "' AND VoucherID = '" + text3 + "'";
					dataSet = new DataSet();
					FillDataSet(dataSet, "Property_Rent", textCommand, sqlTransaction);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException(" no data.");
					}
					DataRow dataRow4 = dataSet.Tables["Property_Rent"].Rows[0];
					text4 = dataRow4["ParentSysDocID"].ToString();
					text5 = dataRow4["ParentVoucherID"].ToString();
					if (text4 != "" && text5 != "" && text7 == "2")
					{
						textCommand = "UPDATE Property_Rent SET ParentSysDocID='" + text4 + "',ParentVoucherID='" + text5 + "' WHERE SysDocID='" + text2 + "' AND VoucherID='" + text + "'";
						flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
					}
				}
				if (propertyRentData.Tables.Contains("Tax_Detail") && propertyRentData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(propertyRentData, text2, text, isUpdate, sqlTransaction);
				}
				if (flag)
				{
					flag &= new PropertyUnit(base.DBConfig).UpdatePropertyUnitStatus(dataRow["UnitID"].ToString(), 2, sqlTransaction);
				}
				if (flag)
				{
					bool flag2 = false;
					if (!string.IsNullOrEmpty(dataRow["IsPeriodicInvoice"].ToString()))
					{
						flag2 = bool.Parse(dataRow["IsPeriodicInvoice"].ToString());
					}
					if (flag2 && !isUpdate)
					{
						flag &= new SalesInvoiceNI(base.DBConfig).InsertBasePropertyInvoiceData(propertyRentData, isUpdate);
						flag &= new RecurringInvoice(base.DBConfig).InsertBasePropertyInvoiceData(propertyRentData, isUpdate, isPosting: false);
					}
				}
				bool result2 = false;
				bool.TryParse(dataRow["IsPeriodicInvoice"].ToString(), out result2);
				if (!result2)
				{
					GLData journalData = CreatePropertyRentGLData(propertyRentData, sqlTransaction);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Property_Rent", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Property Rent";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Property_Rent", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PropertyRental, text2, text, "Property_Rent", sqlTransaction);
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

		private GLData CreatePropertyRentGLData1(PropertyRentData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.PropertyRentTable.Rows[0];
			_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string text = dataRow["CustomerID"].ToString();
			string text2 = dataRow["SysDocID"].ToString();
			dataRow["VoucherID"].ToString();
			string a = dataRow["AgreementType"].ToString();
			string text3 = "";
			string text4 = "";
			string textCommand = "SELECT SD.LocationID,ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID,ISNULL(SD.COGSAccountID,LOC.COGSAccountID) AS COGSAccountID,\r\n                                ISNULL(SD.DiscountGivenAccountID,LOC.DiscountGivenAccountID) AS DiscountGivenAccountID,LOC.InventoryAccountID,ISNULL(SD.SalesAccountID,LOC.SalesAccountID) AS SalesAccountID,\r\n                                 LOC.UnInvoicedInventoryAccountID, ISNULL(SD.SalesTaxAccountID,LOC.SalesTaxAccountID) AS SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID,Loc.ConsignInAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
			if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
			{
				throw new CompanyException("There is no location assigned to this system document or location record is missing.");
			}
			text3 = dataSet.Tables["Accounts"].Rows[0]["ARAccountID"].ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = (!(a == "1")) ? SysDocTypes.PropertyRenew : SysDocTypes.PropertyRental;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = "";
			dataRow2["Narration"] = "Rental Registraion" + dataRow["VoucherID"];
			dataRow2["Note"] = dataRow["Note"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			new Hashtable();
			new ArrayList();
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			foreach (DataRow row in transactionData.PropertyRentDetailTable.Rows)
			{
				string text5 = row["IncomeID"].ToString();
				num = decimal.Parse(row["Amount"].ToString());
				text4 = new PropertyIncomeCode(base.DBConfig).GetIncomeAccountID(text5, sqlTransaction);
				DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["IsBaseOnly"] = true;
				dataRow3["AccountID"] = text4;
				if (num > 0m)
				{
					dataRow3["Debit"] = DBNull.Value;
					dataRow3["Credit"] = Math.Abs(num);
				}
				else
				{
					dataRow3["Debit"] = num;
					dataRow3["Credit"] = DBNull.Value;
				}
				dataRow3["Reference"] = text5;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				num2 += num;
			}
			DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			dataRow4["JournalID"] = 0;
			dataRow4["AccountID"] = text3;
			dataRow4["PayeeID"] = text;
			dataRow4["Debit"] = num2;
			dataRow4["Credit"] = DBNull.Value;
			dataRow4["IsBaseOnly"] = true;
			dataRow4["PayeeType"] = "C";
			dataRow4["Reference"] = "";
			dataRow4["IsARAP"] = true;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			return gLData;
		}

		private GLData CreatePropertyRentGLData(PropertyRentData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.PropertyRentTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			decimal result = default(decimal);
			decimal.TryParse(dataRow["TaxAmount"].ToString(), out result);
			string text = dataRow["CustomerID"].ToString();
			string text2 = dataRow["SysDocID"].ToString();
			dataRow["VoucherID"].ToString();
			string a = dataRow["AgreementType"].ToString();
			string text3 = "";
			string text4 = "";
			string textCommand = "SELECT SD.LocationID,ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID,ISNULL(SD.COGSAccountID,LOC.COGSAccountID) AS COGSAccountID,\r\n                                ISNULL(SD.DiscountGivenAccountID,LOC.DiscountGivenAccountID) AS DiscountGivenAccountID,LOC.InventoryAccountID,ISNULL(SD.SalesAccountID,LOC.SalesAccountID) AS SalesAccountID,\r\n                                 LOC.UnInvoicedInventoryAccountID, ISNULL(SD.SalesTaxAccountID,LOC.SalesTaxAccountID) AS SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID,Loc.ConsignInAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
			if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
			{
				throw new CompanyException("There is no location assigned to this system document or location record is missing.");
			}
			DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
			dataRow2["BaseCurrencyID"].ToString();
			text3 = dataRow2["ARAccountID"].ToString();
			DataRow dataRow3 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = (!(a == "1")) ? SysDocTypes.PropertyRenew : SysDocTypes.PropertyRental;
			dataRow3["JournalID"] = 0;
			dataRow3["JournalDate"] = dataRow["TransactionDate"];
			dataRow3["SysDocID"] = dataRow["SysDocID"];
			dataRow3["SysDocType"] = (byte)sysDocTypes;
			dataRow3["VoucherID"] = dataRow["VoucherID"];
			dataRow3["Reference"] = "";
			dataRow3["Narration"] = "Rental Registraion" + dataRow["VoucherID"];
			dataRow3["Note"] = dataRow["Note"];
			dataRow3.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow3);
			new Hashtable();
			new ArrayList();
			decimal num = default(decimal);
			decimal d = default(decimal);
			foreach (DataRow row in transactionData.PropertyRentDetailTable.Rows)
			{
				string text5 = row["IncomeID"].ToString();
				num = decimal.Parse(row["Amount"].ToString());
				text4 = new PropertyIncomeCode(base.DBConfig).GetIncomeAccountID(text5, sqlTransaction);
				DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				decimal result2 = default(decimal);
				decimal.TryParse(row["TaxAmount"].ToString(), out result2);
				dataRow4["AttributeID1"] = transactionData.Tables["Property_Rent"].Rows[0]["PropertyId"];
				dataRow4["AttributeID2"] = transactionData.Tables["Property_Rent"].Rows[0]["UnitId"];
				dataRow4["JournalID"] = 0;
				dataRow4["IsBaseOnly"] = true;
				dataRow4["AccountID"] = text4;
				if (num > 0m)
				{
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["Credit"] = Math.Abs(num);
				}
				else
				{
					dataRow4["Debit"] = num;
					dataRow4["Credit"] = DBNull.Value;
				}
				dataRow4["Reference"] = text5;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
				d += Math.Round(num, 4);
			}
			DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
			dataRow5.BeginEdit();
			dataRow5["AttributeID1"] = transactionData.Tables["Property_Rent"].Rows[0]["PropertyId"];
			dataRow5["AttributeID2"] = transactionData.Tables["Property_Rent"].Rows[0]["UnitId"];
			dataRow5["JournalID"] = 0;
			dataRow5["AccountID"] = text3;
			dataRow5["PayeeID"] = text;
			dataRow5["Debit"] = d + result;
			dataRow5["Credit"] = DBNull.Value;
			dataRow5["IsBaseOnly"] = true;
			dataRow5["PayeeType"] = "C";
			dataRow5["Reference"] = "";
			dataRow5["IsARAP"] = true;
			dataRow5.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow5);
			if (result > 0m)
			{
				if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
				{
					throw new CompanyException("Tax details not found for the transaction.");
				}
				DataRow[] array = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
				decimal num2 = default(decimal);
				for (int i = 0; i < array.Length; i++)
				{
					num2 = default(decimal);
					DataRow obj2 = array[i];
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					string text6 = "";
					text6 = obj2["TaxItemID"].ToString();
					string text7 = "";
					textCommand = "SELECT SalesTaxAccountID FROM Tax WHERE  TaxCode = '" + text6.Trim() + "'";
					object obj3 = ExecuteScalar(textCommand);
					if (obj3 != null)
					{
						text7 = obj3.ToString();
					}
					if (text7 == "")
					{
						throw new CompanyException("AccountID is not set for tax item: " + text6 + ".");
					}
					decimal.TryParse(obj2["TaxAmount"].ToString(), out num2);
					dataRow5["AccountID"] = text7;
					dataRow5["PayeeID"] = text;
					dataRow5["PayeeType"] = "A";
					dataRow5["Debit"] = DBNull.Value;
					dataRow5["Credit"] = Math.Round(num2, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					dataRow5["AttributeID1"] = transactionData.Tables["Property_Rent"].Rows[0]["PropertyId"];
					dataRow5["AttributeID2"] = transactionData.Tables["Property_Rent"].Rows[0]["UnitId"];
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
			}
			return gLData;
		}

		public PropertyRentData GetPropertyRentByID(string sysDocID, string voucherID)
		{
			try
			{
				PropertyRentData propertyRentData = new PropertyRentData();
				string textCommand = "SELECT PR.*,RT.SysDocID AS InvSysDocID,RT.VoucherID AS InvVoucherID,TransactionID FROM Property_Rent PR LEFT JOIN Recurring_Transaction RT ON PR.SysDocID=RT.SourceSysDocID AND PR.VoucherID=RT.SourceVoucherID WHERE PR.VoucherID='" + voucherID + "' AND PR.SysDocID='" + sysDocID + "'  AND RT.Status=0";
				FillDataSet(propertyRentData, "Property_Rent", textCommand);
				if (propertyRentData == null || propertyRentData.Tables.Count == 0 || propertyRentData.Tables["Property_Rent"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM Property_Rent_Detail TD WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(propertyRentData, "Property_Rent_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(propertyRentData, "Tax_Detail", textCommand);
				return propertyRentData;
			}
			catch
			{
				throw;
			}
		}

		public PropertyRentData GetPropertyRentDetail(string sysDocID, string voucherID)
		{
			try
			{
				PropertyRentData propertyRentData = new PropertyRentData();
				string textCommand = "SELECT * FROM Property_Rent WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(propertyRentData, "Property_Rent", textCommand);
				if (propertyRentData == null || propertyRentData.Tables.Count == 0 || propertyRentData.Tables["Property_Rent"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,PID.IncomeName,PID.IncomeType\r\n                        FROM Property_Rent_Detail TD  INNER JOIN PropertyIncome_Code PID ON TD.IncomeID=PID.IncomeID WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(propertyRentData, "Property_Rent_Detail", textCommand);
				return propertyRentData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPropertyRentActiveDetail(DateTime from, DateTime to)
		{
			CommonLib.ToSqlDateTimeString(from);
			CommonLib.ToSqlDateTimeString(to);
			try
			{
				PropertyRentData propertyRentData = new PropertyRentData();
				string textCommand = "SELECT PT.*,P.PropertyName Property,PU.PropertyUnitName as Unit,DATEDIFF(day,PT.ContractStartDate,PT.ContractEndDate) +1 as Calc_TotalDays,Cast(CASE when CONVERT(VARCHAR(10),'" + from.ToShortDateString() + "',111) > PT.ContractEndDate \r\n                        THEN DATEDIFF(day,PT.ContractStartDate,PT.ContractEndDate)-(Select Sum(isnull(RentedDays,0)) From Rental_Posting_Detail Where SourceSysDocID=PT.SysDocID And SourceVoucherID=PT.VoucherID)+1 ELSE\r\n                        DATEDIFF(day,CONVERT(VARCHAR(10), PT.ContractStartDate, 111),CONVERT(VARCHAR(10),'" + from.ToShortDateString() + "',111)) +1-(Select isnull(Sum(RentedDays),0) From Rental_Posting_Detail Where SourceSysDocID=PT.SysDocID And SourceVoucherID=PT.VoucherID) END as Int) as RentalDays,CS.CustomerName,PID.Total as OriginalAmount FROM Property_Rent PT INNER JOIN Customer CS ON PT.CustomerID=CS.CustomerID\r\n                        Left Join Property P ON P.PropertyID=PT.PropertyID\r\n                        Left Join Property_Unit PU ON PU.PropertyUnitID=PT.UnitID\r\n\t                    Left Join (Select Isnull(Sum(Amount),0) as Total,SysDocID,VoucherID From Property_Rent_Detail PRD INNER JOIN PropertyIncome_Code PID ON PRD.IncomeID=PID.IncomeID  Where PID.IncomeType=1 Group By SysDocID,VoucherID) AS PID ON PID.SysDocID=PT.SysDocID And PID.VoucherID=PT.VoucherID\r\n                        WHERE AgreementStatus!=3 and CONVERT(VARCHAR(10),'" + from.ToShortDateString() + "',111) BETWEEN ContractStartDate AND ContractEndDate Order By PT.SysDocID,PT.VoucherID Asc";
				FillDataSet(propertyRentData, "Property_Rent", textCommand);
				if (propertyRentData == null || propertyRentData.Tables.Count == 0 || propertyRentData.Tables["Property_Rent"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,PID.IncomeName,PID.IncomeType,CS.CustomerName,PR.ContractStartDate,PR.ContractEndDate,DATEDIFF(day,PR.ContractStartDate,PR.ContractEndDate) +1 as Calc_TotalDays ,PR.CustomerID,PR.PropertyID, CASE when CONVERT(VARCHAR(10),'" + from.ToShortDateString() + "',111) > PR.ContractEndDate THEN DATEDIFF(day,PR.ContractStartDate,PR.ContractEndDate) +1 ELSE\r\n                        DATEDIFF(day,CONVERT(VARCHAR(10), PR.ContractStartDate, 111),CONVERT(VARCHAR(10),'" + from.ToShortDateString() + "',111)) +1-(Select isnull(Sum(RentedDays),0) From Rental_Posting_Detail Where SourceSysDocID=TD.SysDocID And SourceVoucherID=TD.VoucherID) END as RentalDays   \r\n                        FROM Property_Rent_Detail TD  INNER JOIN PropertyIncome_Code PID ON TD.IncomeID=PID.IncomeID  INNER JOIN Property_Rent PR ON TD.SysDocID=PR.SysDocID AND TD.VoucherID=PR.VoucherID INNER JOIN Customer CS ON PR.CustomerID=CS.CustomerID WHERE PR.AgreementStatus!=3 And CONVERT(VARCHAR(10),'" + from.ToShortDateString() + "',111)  BETWEEN PR.ContractStartDate AND PR.ContractEndDate And PID.IncomeType=1 ";
				FillDataSet(propertyRentData, "Property_Rent_Detail", textCommand);
				if (propertyRentData.Tables["Property_Rent"].Rows.Count > 0)
				{
					propertyRentData.Relations.Add("PropertyRentActiveDetail", new DataColumn[2]
					{
						propertyRentData.Tables["Property_Rent"].Columns["SysDocID"],
						propertyRentData.Tables["Property_Rent"].Columns["VoucherID"]
					}, new DataColumn[2]
					{
						propertyRentData.Tables["Property_Rent_Detail"].Columns["SysDocID"],
						propertyRentData.Tables["Property_Rent_Detail"].Columns["VoucherID"]
					}, createConstraints: false);
				}
				return propertyRentData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePropertyRentDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string textCommand = "SELECT * FROM Property_Rent  WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Property_Rent", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException(" no data.");
				}
				textCommand = "UPDATE Property_Rent SET AgreementStatus=1 WHERE SysDocID='" + dataSet.Tables["Property_Rent"].Rows[0]["SourceSysDocID"].ToString() + "' AND VoucherID='" + dataSet.Tables["Property_Rent"].Rows[0]["SourceVoucherID"].ToString() + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				flag &= new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				string commandText = "DELETE FROM Property_Rent_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidPropertyRent(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeletePropertyRent(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeletePropertyRentDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				text = "DELETE FROM Property_Rent WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				text = "Update Recurring_Transaction SET Status=1  WHERE SourceSysDocID='" + sysDocID + "' AND SourceVoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Property Rent", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetPropertyRentToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT C.CustomerName,CA.AddressPrintFormat,CA.Email,CA.Phone1,CA.Mobile,PR.*,P.*,PU.*,C.TaxIDNumber as CTaxIDNo, PA.PropertyAgentName\r\n                                FROM Property_Rent PR\r\n                                LEFT OUTER JOIN Customer C ON C.CustomerID = PR.CustomerID\r\n                                LEFT OUTER JOIN Property P ON PR.[PropertyID]=P.PropertyID\r\n                                LEFT OUTER JOIN  Customer_Address CA ON C.CustomerID=CA.CustomerID\r\n                                LEFT OUTER JOIN  Property_Unit PU ON PR.UnitID=PU.PropertyUnitID \r\n                                LEFT OUTER JOIN Property_Agent PA ON PR.PropertyAgentID=PA.PropertyAgentID \r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Property_Rent", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Property_Rent"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT * FROM Property_Rent_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Property_Rent_Detail", cmdText);
				DataSet dataSet2 = new DataSet();
				cmdText = "SELECT GPD.*,SD.DocName  FROM General_Payment_Detail GPD LEFT JOIN System_Document SD ON GPD.SysDocID=SD.SysDocID\r\n                        WHERE ISNULL(IsVoid, 'False') = 'False' AND  SourceSysDocID='" + sysDocID + "' AND SourceVoucherID IN (" + text + ")";
				FillDataSet(dataSet2, "General_Payment_Detail", cmdText);
				dataSet.Merge(dataSet2);
				dataSet.Tables["Property_Rent"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Property_Rent"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["TaxAmount"].ToString(), out result2);
					row["TotalInWords"] = NumToWord.GetNumInWords(decimalPoints: new CompanyInformations(base.DBConfig).CurrencyDecimalPoints, amount: result + result2);
				}
				dataSet.Relations.Add("Property_Rent_Fees", new DataColumn[2]
				{
					dataSet.Tables["Property_Rent"].Columns["SysDocID"],
					dataSet.Tables["Property_Rent"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Property_Rent_Detail"].Columns["SysDocID"],
					dataSet.Tables["Property_Rent_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				if (dataSet2.Tables[0].Rows.Count > 0)
				{
					dataSet.Relations.Add("Property_Rent_payment", new DataColumn[2]
					{
						dataSet.Tables["Property_Rent"].Columns["SysDocID"],
						dataSet.Tables["Property_Rent"].Columns["VoucherID"]
					}, new DataColumn[2]
					{
						dataSet.Tables["General_Payment_Detail"].Columns["SourceSysDocID"],
						dataSet.Tables["General_Payment_Detail"].Columns["SourceVoucherID"]
					}, createConstraints: false);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPropertyRentList(string sysDocID, string CustomerID, DateTime from, DateTime to)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT PR.SysDocID [Doc ID], PR.VoucherID [Number], PR.TransactionDate AS [Date],PR.CustomerID,Cus.CustomerName AS [Tenant],PR.PropertyID,PR.UnitID,PR.PayeeTaxGroupID, PR.PropertyAgentID\r\n                             FROM Property_Rent PR LEFT JOIN Customer CUS ON PR.CustomerID = Cus.CustomerID\t\t\t\t\t\t\t\r\n                             WHERE  PR.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND PR.AgreementStatus=1 ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + " AND PR.SysDocID='" + sysDocID + "'";
			}
			if (!string.IsNullOrEmpty(CustomerID))
			{
				str = str + " AND PR.CustomerID='" + CustomerID + "'";
			}
			str += " ORDER BY  PR.VoucherID,PR.TransactionDate ";
			FillDataSet(dataSet, "Property_Rent", str);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool val1, bool val2, int AgreeType)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT PR.SysDocID , PR.VoucherID [VoucherID], PR.TransactionDate AS [Date],PR.CustomerID,Cus.CustomerName AS [Tenant],\r\n                            P.PropertyName AS Property,PU.PropertyUnitName AS Unit,PA.PropertyAgentName AS Agent ,PR.Total\r\n                            FROM Property_Rent PR LEFT JOIN Customer CUS ON PR.CustomerID = Cus.CustomerID\t\r\n                            LEFT JOIN Property P ON PR.PropertyID=P.PropertyID\t\t\r\n                            LEFT JOIN Property_Unit PU ON PR.UnitID=PU.PropertyUnitID\t\r\n                            LEFT OUTER JOIN Property_Agent PA ON PR.PropertyAgentID=PA.PropertyAgentID\t\t\t\t\t\t\t\t\r\n                             WHERE  PR.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND 1=1 ";
			str = ((AgreeType != 1) ? (str + " AND PR.AgreementType=2") : (str + " AND PR.AgreementType=1"));
			str += " ORDER BY PR.VoucherID,PR.TransactionDate ";
			FillDataSet(dataSet, "Property_Rent", str);
			return dataSet;
		}

		public DataSet GetList()
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT PR.SysDocID [SysDocID], PR.VoucherID [VoucherID], PR.TransactionDate AS [Date],PR.CustomerID,Cus.CustomerName AS [Tenant],\r\n                            P.PropertyName AS Property,PU.PropertyUnitName AS Unit,PA.PropertyAgentName AS Agent ,PR.Total\r\n                            FROM Property_Rent PR LEFT JOIN Customer CUS ON PR.CustomerID = Cus.CustomerID\t\r\n                            LEFT JOIN Property P ON PR.PropertyID=P.PropertyID\t\t\r\n                            LEFT JOIN Property_Unit PU ON PR.UnitID=PU.PropertyUnitID\t\r\n                            LEFT OUTER JOIN Property_Agent PA ON PR.PropertyAgentID=PA.PropertyAgentID\t\t";
			str += " ORDER BY PR.VoucherID,PR.TransactionDate ";
			FillDataSet(dataSet, "Property_Rent", str);
			return dataSet;
		}

		public DataSet GetPropertyRentDetailList(string sysDocID, string CustomerID, DateTime from, DateTime to)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT PR.SysDocID [Doc ID], PR.VoucherID [Number], PR.TransactionDate AS [Date],PR.CustomerID,Cus.CustomerName AS [Tenant],PR.PropertyID,P.PropertyName [Property],PR.UnitID,PU.PropertyUnitName [Unit],PR.ContractStartDate,PR.ContractEndDate, PR.PayeeTaxGroupID, PR.PropertyAgentID\r\n                             FROM Property_Rent PR LEFT JOIN Customer CUS ON PR.CustomerID = Cus.CustomerID\t\t\r\n                             LEFT JOIN Property P ON P.PropertyID=PR.PropertyID\t\t\r\n                             LEFT JOIN Property_Unit PU ON PU.PropertyUnitID=PR.UnitID\t\t\t\r\n                             WHERE  PR.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND PR.AgreementStatus=1 ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + " AND PR.SysDocID='" + sysDocID + "'";
			}
			if (!string.IsNullOrEmpty(CustomerID))
			{
				str = str + " AND PR.CustomerID='" + CustomerID + "'";
			}
			str += " ORDER BY  PR.VoucherID,PR.TransactionDate ";
			FillDataSet(dataSet, "Property_Rent", str);
			return dataSet;
		}
	}
}
