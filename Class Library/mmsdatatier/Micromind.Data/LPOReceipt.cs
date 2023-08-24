using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class LPOReceipt : StoreObject
	{
		private const string LPORECEIPT_TABLE = "LPO_Receipt";

		private const string LPORECEIPTDETAIL_TABLE = "LPO_Receipt_Details";

		private const string SYSDOCTYPE_PARM = "@SysDocType";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string TOTAL_PARM = "@Total";

		private const string ISVOID_PARM = "@IsVoid";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PAYEETYPE_PARM = "@PayeeType";

		private const string PAYEEID_PARM = "@PayeeID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string LPONUMBER_PARM = "@LPONumber";

		private const string LPODATE_PARM = "@LPODate";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string ROWINDEX_PARM = "@RowIndex";

		public LPOReceipt(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateLPOReceiptText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("LPO_Receipt", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Total", "@Total"), new FieldValue("SysDocType", "@SysDocType"), new FieldValue("Reference", "@Reference"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("LPO_Receipt", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateLPOReceiptCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateLPOReceiptText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateLPOReceiptText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@SysDocType", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Money);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@SysDocType"].SourceColumn = "SysDocType";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Note"].SourceColumn = "Note";
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

		private string GetInsertUpdateLPOReceiptDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("LPO_Receipt_Details", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("Description", "@Description"), new FieldValue("LPONumber", "@LPONumber"), new FieldValue("LPODate", "@LPODate"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateLPOReceiptDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateLPOReceiptDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateLPOReceiptDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@LPONumber", SqlDbType.NVarChar);
			parameters.Add("@LPODate", SqlDbType.DateTime);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@LPONumber"].SourceColumn = "LPONumber";
			parameters["@LPODate"].SourceColumn = "LPODate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(LPOReceiptData journalData)
		{
			return true;
		}

		public bool InsertUpdateLPOReceipt(LPOReceiptData lpoReceiptData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateLPOReceiptCommand = GetInsertUpdateLPOReceiptCommand(isUpdate);
			try
			{
				DataRow dataRow = lpoReceiptData.LPOReceiptTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("LPO_Receipt", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				decimal result = 1m;
				if (dataRow["CurrencyID"] != DBNull.Value && baseCurrencyID != dataRow["CurrencyID"].ToString())
				{
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result);
					new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
				}
				insertUpdateLPOReceiptCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(lpoReceiptData, "LPO_Receipt", insertUpdateLPOReceiptCommand)) : (flag & Insert(lpoReceiptData, "LPO_Receipt", insertUpdateLPOReceiptCommand)));
				foreach (DataRow row in lpoReceiptData.LPOReceiptDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text2 = row["PayeeID"].ToString();
					string a = row["PayeeType"].ToString();
					string text3 = "";
					if (a == "C")
					{
						text3 = new Customers(base.DBConfig).GetCustomerARAccountID(sysDocID, text2);
					}
					else if (a == "V")
					{
						text3 = new Vendors(base.DBConfig).GetVendorAPAccountID(sysDocID, text2);
					}
					if (text3 == "")
					{
						throw new CompanyException("AccountID should be selected for the payee if a payee is in transaction.", 1028);
					}
					row["AccountID"] = text3;
				}
				insertUpdateLPOReceiptCommand = GetInsertUpdateLPOReceiptDetailsCommand(isUpdate: false);
				insertUpdateLPOReceiptCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteLPOReceiptDetailsRows(sysDocID, text, isDeletingTransaction: false, sqlTransaction);
				}
				if (lpoReceiptData.Tables["LPO_Receipt_Details"].Rows.Count > 0)
				{
					flag &= Insert(lpoReceiptData, "LPO_Receipt_Details", insertUpdateLPOReceiptCommand);
				}
				GLData journalData = CreateLPOReceiptGLData(lpoReceiptData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("LPO_Receipt", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "LPO Receipt";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "LPO_Receipt", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.LPOReceipt, sysDocID, text, "LPO_Receipt", sqlTransaction);
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

		private GLData CreateLPOReceiptGLData(LPOReceiptData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.LPOReceiptTable.Rows[0];
			string text = dataRow["CustomerID"].ToString();
			dataRow["VoucherID"].ToString();
			string text2 = dataRow["SysDocID"].ToString();
			string textCommand = "SELECT SD.LocationID,ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID,LOC.COGSAccountID,LOC.DiscountGivenAccountID,Loc.ConsignInAccountID,\r\n                                LOC.InventoryAccountID,LOC.SalesAccountID,LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
			if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
			{
				throw new CompanyException("There is no location assigned to this system document or location record is missing.");
			}
			DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
			dataRow2["LocationID"].ToString();
			dataRow2["DiscountGivenAccountID"].ToString();
			dataRow2["SalesTaxAccountID"].ToString();
			string value = dataRow2["ARAccountID"].ToString();
			string a = dataRow2["BaseCurrencyID"].ToString();
			bool flag = false;
			decimal result = 1m;
			if (dataRow["CurrencyID"] != DBNull.Value && a != dataRow["CurrencyID"].ToString())
			{
				flag = true;
				decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result);
			}
			DataRow dataRow3 = gLData.JournalTable.NewRow();
			byte.Parse(dataRow["SysDocType"].ToString());
			dataRow3["JournalID"] = 0;
			dataRow3["JournalDate"] = dataRow["TransactionDate"];
			dataRow3["SysDocID"] = dataRow["SysDocID"];
			dataRow3["SysDocType"] = dataRow["SysDocType"];
			dataRow3["VoucherID"] = dataRow["VoucherID"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["CurrencyID"] = dataRow["CurrencyID"];
			dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
			dataRow3["Note"] = dataRow["Note"];
			dataRow3.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow3);
			DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			dataRow4["JournalID"] = 0;
			dataRow4["PayeeType"] = "C";
			dataRow4["PayeeID"] = text;
			dataRow4["AccountID"] = value;
			dataRow4["IsARAP"] = true;
			dataRow4["Debit"] = DBNull.Value;
			dataRow4["DebitFC"] = DBNull.Value;
			if (!flag)
			{
				dataRow4["Credit"] = dataRow["Total"];
			}
			else
			{
				dataRow4["CreditFC"] = dataRow["Total"];
			}
			dataRow4["Description"] = dataRow["Note"];
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4["RowIndex"] = -1;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			foreach (DataRow row in transactionData.LPOReceiptDetailTable.Rows)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["PayeeType"] = row["PayeeType"];
				dataRow4["PayeeID"] = row["PayeeID"];
				dataRow4["AccountID"] = row["AccountID"];
				dataRow4["IsARAP"] = true;
				dataRow4["Debit"] = row["Amount"];
				dataRow4["DebitFC"] = row["AmountFC"];
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
				dataRow4["Description"] = row["Description"];
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4["RowIndex"] = row["RowIndex"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		public LPOReceiptData GetLPOReceiptByID(string sysDocID, string voucherID)
		{
			return GetLPOReceiptByID(sysDocID, voucherID, null);
		}

		internal LPOReceiptData GetLPOReceiptByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				LPOReceiptData lPOReceiptData = new LPOReceiptData();
				string textCommand = "SELECT * FROM LPO_Receipt WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(lPOReceiptData, "LPO_Receipt", textCommand, sqlTransaction);
				if (lPOReceiptData == null || lPOReceiptData.Tables.Count == 0 || lPOReceiptData.Tables["LPO_Receipt"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT LRD.*,  (CASE PayeeType\r\n                            WHEN 'C' THEN C.CustomerName\r\n                            WHEN 'V' THEN V.VendorName\r\n                            ELSE NULL END) AS AccountName\r\n                        FROM LPO_Receipt_Details LRD LEFT OUTER JOIN Customer C ON LRD.PayeeID = C.CustomerID\r\n                            LEFT OUTER JOIN VENDOR V ON LRD.PayeeID = V.VendorID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(lPOReceiptData, "LPO_Receipt_Details", textCommand, sqlTransaction);
				return lPOReceiptData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteLPOReceiptDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM LPO_Receipt_Details WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool CanEdit(string sysDocID, string voucherID)
		{
			return CanEdit(sysDocID, voucherID, null);
		}

		public bool CanEdit(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			if (!new InventoryTransaction(base.DBConfig).AllowDeleteInventoryTransaction(sysDocID, voucherID, sqlTransaction))
			{
				return false;
			}
			return true;
		}

		public bool VoidLPOReceipt(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanEdit(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("This transaction cannot be modifed because some of items are refered by other transactions.");
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				string exp = "UPDATE LPO_Receipt SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("LPO Receipt", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteLPOReceipt(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteLPOReceiptDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				text = "DELETE FROM LPO_Receipt WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("LPO Receipt", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetLPOReceiptToPrint(string sysDocID, string voucherID)
		{
			return GetLPOReceiptToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetLPOReceiptToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = " SELECT LPO_Receipt.*, Customer.CustomerName FROM LPO_Receipt JOIN Customer ON LPO_Receipt.CustomerID = Customer.CustomerID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "LPO_Receipt", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["LPO_Receipt"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = " SELECT LRD.*,  (CASE PayeeType\r\n                            WHEN 'C' THEN C.CustomerName\r\n                            WHEN 'V' THEN V.VendorName\r\n                            ELSE NULL END) AS AccountName\r\n                        FROM LPO_Receipt_Details LRD LEFT OUTER JOIN Customer C ON LRD.PayeeID = C.CustomerID\r\n                            LEFT OUTER JOIN VENDOR V ON LRD.PayeeID = V.VendorID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "LPO_Receipt_Details", cmdText);
				dataSet.Relations.Add("REL", new DataColumn[2]
				{
					dataSet.Tables["LPO_Receipt"].Columns["SysDocID"],
					dataSet.Tables["LPO_Receipt"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["LPO_Receipt_Details"].Columns["SysDocID"],
					dataSet.Tables["LPO_Receipt_Details"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInvoicesToReturn(string customerID, bool cashOnly, DateTime fromDate, DateTime toDate)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(fromDate);
			string text2 = StoreConfiguration.ToSqlDateTimeString(toDate);
			string text3 = " SELECT DISTINCT 1 AS Type,SI.SysDocID [Doc ID],SI.VoucherID [Number],TransactionDate AS [Date],SI.CustomerID + '-' + CUS.CustomerName AS [Customer]\r\n                            FROM Sales_Invoice SI INNER JOIN Sales_Invoice_Detail SID ON SI.SysDocID=SID.SysDocID AND SI.VoucherID=SID.VoucherID\r\n                            INNER JOIN Customer CUS ON Cus.CustomerID = SI.CustomerID\r\n                            WHERE ISNULL(ISNULL(UnitQuantity,Quantity),0) - ISNULL(SID.QuantityReturned,0) >0 AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n                             AND ISNULL(IsVoid,'False')='False'";
			if (customerID != "")
			{
				text3 = text3 + " AND SI.CustomerID='" + customerID + "' ";
			}
			if (cashOnly)
			{
				text3 += " AND ISNULL(IsCash,'False') = 'True' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "LPO_Receipt", sqlCommand);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT    ISNULL(IsVoid,'False') AS V, SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Invoice Date],\r\n                           Total [Amount]\r\n                            FROM         LPO_Receipt INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "LPO_Receipt", sqlCommand);
			return dataSet;
		}
	}
}
