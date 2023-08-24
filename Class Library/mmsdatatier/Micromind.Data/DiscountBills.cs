using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class DiscountBills : StoreObject
	{
		private const string BILLDISCOUNT_TABLE = "Bill_Discount";

		private const string BILLDISCOUNTDETAIL_TABLE = "Bill_Discount_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string INVOICESYSDOCID_PARM = "@InvoiceSysDocID";

		private const string INVOICEVOUCHERID_PARM = "@InvoiceVoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REFERENCE_PARM = "@Reference";

		private const string BANKACCOUNTID_PARM = "@BankAccountID";

		private const string BANKFACILITYID_PARM = "@BankFacilityID";

		private const string BANKCHARGEAMOUNT_PARM = "@BankChargeAmount";

		private const string BANKCOMMISSION_PARM = "@BankCommission";

		private const string BANKCHARGEPERCENT_PARM = "@BankChargePercent";

		private const string LIABILITYACCOUNTID_PARM = "@LiabilityAccountID";

		private const string NOTE_PARM = "@Note";

		private const string FACILITYTYPE_PARM = "@FacilityType";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string DISCOUNTAMOUNT_PARM = "@DiscountAmount";

		private const string DATE_PARM = "@Date";

		private const string DUEDATE_PARM = "@DueDate";

		private const string TOTAL_PARM = "@Total";

		private const string PAYEEID_PARM = "@PayeeID";

		public DiscountBills(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateDiscountBillsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bill_Discount", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Reference", "@Reference"), new FieldValue("BankAccountID", "@BankAccountID"), new FieldValue("BankFacilityID", "@BankFacilityID"), new FieldValue("BankChargeAmount", "@BankChargeAmount"), new FieldValue("LiabilityAccountID", "@LiabilityAccountID"), new FieldValue("BankChargePercent", "@BankChargePercent"), new FieldValue("BankCommission", "@BankCommission"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("FacilityType", "@FacilityType"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("DueDate", "@DueDate"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Bill_Discount", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDiscountBillsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDiscountBillsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDiscountBillsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@BankAccountID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@BankFacilityID", SqlDbType.NVarChar);
			parameters.Add("@BankChargeAmount", SqlDbType.Money);
			parameters.Add("@LiabilityAccountID", SqlDbType.NVarChar);
			parameters.Add("@BankChargePercent", SqlDbType.Money);
			parameters.Add("@BankCommission", SqlDbType.Money);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@FacilityType", SqlDbType.Int);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@BankAccountID"].SourceColumn = "BankAccountID";
			parameters["@BankFacilityID"].SourceColumn = "BankFacilityID";
			parameters["@BankChargeAmount"].SourceColumn = "BankChargeAmount";
			parameters["@LiabilityAccountID"].SourceColumn = "LiabilityAccountID";
			parameters["@BankChargePercent"].SourceColumn = "BankChargePercent";
			parameters["@BankCommission"].SourceColumn = "BankCommission";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@FacilityType"].SourceColumn = "FacilityType";
			parameters["@DueDate"].SourceColumn = "DueDate";
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

		private string GetInsertUpdateDiscountBillsDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bill_Discount_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("InvoiceVoucherID", "@InvoiceVoucherID"), new FieldValue("InvoiceSysDocID", "@InvoiceSysDocID"), new FieldValue("Date", "@Date"), new FieldValue("DueDate", "@DueDate"), new FieldValue("Total", "@Total"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("DiscountAmount", "@DiscountAmount"), new FieldValue("BankChargeAmount", "@BankChargeAmount"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDiscountBillsDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDiscountBillsDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDiscountBillsDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@InvoiceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@InvoiceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Date", SqlDbType.DateTime);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@BankChargeAmount", SqlDbType.Money);
			parameters.Add("@DiscountAmount", SqlDbType.Money);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@InvoiceSysDocID"].SourceColumn = "InvoiceSysDocID";
			parameters["@InvoiceVoucherID"].SourceColumn = "InvoiceVoucherID";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@Date"].SourceColumn = "Date";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@BankChargeAmount"].SourceColumn = "BankChargeAmount";
			parameters["@DiscountAmount"].SourceColumn = "DiscountAmount";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(DiscountBillData journalData)
		{
			return true;
		}

		public bool InsertUpdateDiscountBills(DiscountBillData DiscountBillData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateDiscountBillsCommand = GetInsertUpdateDiscountBillsCommand(isUpdate);
			try
			{
				DataRow dataRow = DiscountBillData.BillDiscountTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Bill_Discount", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				insertUpdateDiscountBillsCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(DiscountBillData, "Bill_Discount", insertUpdateDiscountBillsCommand)) : (flag & Insert(DiscountBillData, "Bill_Discount", insertUpdateDiscountBillsCommand)));
				insertUpdateDiscountBillsCommand = GetInsertUpdateDiscountBillsDetailsCommand(isUpdate: false);
				insertUpdateDiscountBillsCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteDiscountBillsDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (DiscountBillData.Tables["Bill_Discount_Detail"].Rows.Count > 0)
				{
					flag &= Insert(DiscountBillData, "Bill_Discount_Detail", insertUpdateDiscountBillsCommand);
				}
				GLData journalData = CreateBillDiscountGLData(DiscountBillData);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Bill_Discount", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Bill_Discount", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.BillDiscount, sysDocID, text, "Bill_Discount", sqlTransaction);
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

		private GLData CreateBillDiscountGLData(DiscountBillData DiscountBillData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = DiscountBillData.BillDiscountTable.Rows[0];
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = 267;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Note"] = dataRow["Note"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			decimal result = default(decimal);
			decimal num = default(decimal);
			foreach (DataRow row in DiscountBillData.BillDiscountDetailTable.Rows)
			{
				if (!string.IsNullOrEmpty(row["DiscountAmount"].ToString()))
				{
					decimal.TryParse(row["DiscountAmount"].ToString(), out result);
					num += result;
				}
				else
				{
					num = (string.IsNullOrEmpty(dataRow["Amount"].ToString()) ? decimal.Parse(dataRow["AmountFC"].ToString()) : decimal.Parse(dataRow["Amount"].ToString()));
				}
			}
			if (num > 0m)
			{
				DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = dataRow["BankAccountID"];
				dataRow4["Debit"] = num;
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4["RowIndex"] = -1;
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = dataRow["LiabilityAccountID"];
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["Credit"] = num;
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4["RowIndex"] = 0;
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			decimal result2 = default(decimal);
			if (dataRow["BankChargeAmount"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["BankChargeAmount"].ToString(), out result2);
			}
			if (result2 > 0m)
			{
				DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = dataRow["BankAccountID"];
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["Credit"] = dataRow["BankChargeAmount"];
				dataRow4["Description"] = "Discounted Cheques Charges";
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4["RowIndex"] = 0;
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = dataRow["BankChargeAccountID"];
				dataRow4["Debit"] = dataRow["BankChargeAmount"];
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["Description"] = "Discounted Cheques Charges";
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4["RowIndex"] = 0;
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			decimal result3 = default(decimal);
			if (dataRow["BankCommission"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["BankCommission"].ToString(), out result3);
			}
			if (result3 > 0m)
			{
				DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = dataRow["BankAccountID"];
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["Credit"] = dataRow["BankCommission"];
				dataRow4["Description"] = "Discounted Cheques Interest";
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4["RowIndex"] = 0;
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = dataRow["BankInterestAccountID"];
				dataRow4["Debit"] = dataRow["BankCommission"];
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["Description"] = "Discounted Cheques Interest";
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4["RowIndex"] = 0;
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		public DiscountBillData GetDiscountBillsByID(string sysDocID, string voucherID)
		{
			try
			{
				DiscountBillData discountBillData = new DiscountBillData();
				string textCommand = "SELECT CD.*,BF.CurrentAccountID,AC.AccountName,BF.LimitAmount FROM Bill_Discount CD\r\n                                INNER JOIN Bank_Facility BF ON CD.BankFacilityID = BF.FacilityID\r\n                                INNER JOIN Account AC ON AC.AccountID = BF.CurrentAccountID WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(discountBillData, "Bill_Discount", textCommand);
				if (discountBillData == null || discountBillData.Tables.Count == 0 || discountBillData.Tables["Bill_Discount"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT  'True' AS 'D', TD.ChequeID,TD.SysDocID,TD.VoucherID,\r\n                            TD.BankChargeAmount,TD.DiscountAmount,TD.*,\r\n                            \r\n                            Customer.CustomerName\r\n                             AS [Payee Name] ,\r\n                            ISNULL(TD.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,ISNULL(TD.Total,0) AS Amount\r\n                            FROM   Bill_Discount_Detail TD \r\n                            \r\n                           \r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=TD.PayeeID \r\n                           \r\n                            WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(discountBillData, "Bill_Discount_Detail", textCommand);
				return discountBillData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteDiscountBillsDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCDirectMaturity, true).ToString());
				string commandText = "DELETE FROM Bill_Discount_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidDiscountBills(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE Bill_Discount SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Bill Discount", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteDiscountBills(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteDiscountBillsDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Bill_Discount WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Bill Discount", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetDiscountBillsToPrint(string sysDocID, string voucherID)
		{
			return GetDiscountBillsToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetDiscountBillsToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.*,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID\r\n                                ,IsVoid,\r\n                                SI.Note, BF.*\r\n                                FROM  Bill_Discount SI \r\n                                LEFT OUTER JOIN Bank_Facility BF ON BF.FacilityID=SI.BankFacilityID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Bill_Discount", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Bill_Discount"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,SQD.*, C.CustomerName,C.*\r\n                        FROM   Bill_Discount_Detail SQD\r\n\t\t\t\t\t\tLEFT OUTER JOIN Customer C ON C.CustomerID=SQD.PayeeID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") ";
				FillDataSet(dataSet, "Bill_Discount_Detail", cmdText);
				dataSet.Relations.Add("CustomerQuote", new DataColumn[2]
				{
					dataSet.Tables["Bill_Discount"].Columns["SysDocID"],
					dataSet.Tables["Bill_Discount"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Bill_Discount_Detail"].Columns["SysDocID"],
					dataSet.Tables["Bill_Discount_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Bill_Discount"].Columns.Add("TotalInWords", typeof(string));
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],Note,TransactionDate \r\n\r\n                            FROM         Bill_Discount INV";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Bill_Discount", sqlCommand);
			return dataSet;
		}

		public DataSet GetDiscountBillssReport(string CustomerID, string BankID, DateTime from, DateTime to, bool Status)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT CQR.PayeeID,CASE CQR.PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'A' THEN Account.AccountName\r\n                        WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name],CQR.BankID,Bank.BankName,Account.Alias,\r\n                        TD.SysDocID,TD.VoucherID,CQR.ChequeNumber,CQR.ChequeDate,CQR.ReceiptDate,CQR.DiscountDate,\r\n                        (SELECT AccountName FROM Account WHERE AccountID =CQR.DiscountBankAccountID) AS [DepositedAccount],CQR.DiscountBankAccountID,\r\n                        DATEDIFF(day,CQR.DiscountDate,CQR.ChequeDate) AS DiffDate,ISNULL(CQR.AmountFC,CQR.Amount) AS Amount\r\n                        FROM   Cheque_Discount_Detail TD INNER JOIN Cheque_Received CQR ON CQR.ChequeID = TD.ChequeID\r\n                        INNER JOIN Bank ON CQR.BankID=Bank.BankID LEFT OUTER JOIN Cheque_Discount TR ON TD.VoucherID=TR.VoucherID AND TD.SysDocID=TR.SysDocID\r\n                        LEFT OUTER JOIN Customer ON Customer.CustomerID=CQR.PayeeID LEFT OUTER JOIN Vendor ON Vendor.VendorID=CQR.PayeeID\r\n                        LEFT OUTER JOIN Employee ON Employee.EmployeeID=CQR.PayeeID LEFT Outer JOIN Account ON Account.AccountID=CQR.PayeeID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE CQR.ChequeDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (BankID != "")
			{
				text3 = text3 + " AND CQR.DiscountBankAccountID='" + BankID + "'";
			}
			if (CustomerID != "")
			{
				text3 = text3 + " AND CQR.PayeeID='" + CustomerID + "'";
			}
			text3 = ((!Status) ? (text3 + " AND CQR.Status  IN(3)") : (text3 + " AND CQR.Status  IN(2,3)"));
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Bill_Discount", sqlCommand);
			return dataSet;
		}
	}
}
