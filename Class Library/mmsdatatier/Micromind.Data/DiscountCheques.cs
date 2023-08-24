using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class DiscountCheques : StoreObject
	{
		private const string CHEQUEDISCOUNT_TABLE = "Cheque_Discount";

		private const string CHEQUEDISCOUNTDETAIL_TABLE = "Cheque_Discount_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

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

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string CHEQUEID_PARM = "@ChequeID";

		private const string DISCOUNTAMOUNT_PARM = "@DiscountAmount";

		public DiscountCheques(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateDiscountChequeText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Cheque_Discount", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Reference", "@Reference"), new FieldValue("BankAccountID", "@BankAccountID"), new FieldValue("BankFacilityID", "@BankFacilityID"), new FieldValue("BankChargeAmount", "@BankChargeAmount"), new FieldValue("LiabilityAccountID", "@LiabilityAccountID"), new FieldValue("BankChargePercent", "@BankChargePercent"), new FieldValue("BankCommission", "@BankCommission"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Cheque_Discount", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDiscountChequeCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDiscountChequeText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDiscountChequeText(isUpdate: false), base.DBConfig.Connection);
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

		private string GetInsertUpdateDiscountChequeDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Cheque_Discount_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("DiscountAmount", "@DiscountAmount"), new FieldValue("BankChargeAmount", "@BankChargeAmount"), new FieldValue("ChequeID", "@ChequeID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDiscountChequeDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDiscountChequeDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDiscountChequeDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ChequeID", SqlDbType.NVarChar);
			parameters.Add("@BankChargeAmount", SqlDbType.Money);
			parameters.Add("@DiscountAmount", SqlDbType.Money);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ChequeID"].SourceColumn = "ChequeID";
			parameters["@BankChargeAmount"].SourceColumn = "BankChargeAmount";
			parameters["@DiscountAmount"].SourceColumn = "DiscountAmount";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(DiscountChequeData journalData)
		{
			return true;
		}

		public bool InsertUpdateDiscountCheque(DiscountChequeData discountChequeData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateDiscountChequeCommand = GetInsertUpdateDiscountChequeCommand(isUpdate);
			try
			{
				DataRow dataRow = discountChequeData.ChequeDiscountTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				int[] array = new int[discountChequeData.ChequeDiscountDetailTable.Rows.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = int.Parse(discountChequeData.ChequeDiscountDetailTable.Rows[i]["ChequeID"].ToString());
				}
				string text3 = "";
				for (int j = 0; j < array.Length; j++)
				{
					if (text3 != "")
					{
						text3 += ",";
					}
					text3 = text3 + "'" + array[j].ToString() + "'";
				}
				string text4 = "";
				if (!isUpdate)
				{
					text4 = "SELECT COUNT(ChequeID) FROM Cheque_Received WHERE ChequeID IN (" + text3 + ") AND Status NOT IN (1,8,4)";
					object obj = ExecuteScalar(text4);
					if (obj != null && int.Parse(obj.ToString()) > 0)
					{
						throw new CompanyException("Some cheques are in a status that cannot be discounted. Make sure that cheques are not already matured or cancelled.", 1008);
					}
				}
				else
				{
					text4 = "SELECT COUNT(ChequeID) FROM Cheque_Received WHERE ChequeID IN (" + text3 + ") AND Status IN (2,8,9)";
					object obj2 = ExecuteScalar(text4);
					if (obj2 != null && int.Parse(obj2.ToString()) > 0)
					{
						throw new CompanyException("There are cheques that are bounced, matured or cancelled. Transaction cannot be modifed", 1008);
					}
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Cheque_Discount", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				insertUpdateDiscountChequeCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(discountChequeData, "Cheque_Discount", insertUpdateDiscountChequeCommand)) : (flag & Insert(discountChequeData, "Cheque_Discount", insertUpdateDiscountChequeCommand)));
				insertUpdateDiscountChequeCommand = GetInsertUpdateDiscountChequeDetailsCommand(isUpdate: false);
				insertUpdateDiscountChequeCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteDiscountChequeDetailsRows(text2, text, sqlTransaction);
				}
				if (discountChequeData.Tables["Cheque_Discount_Detail"].Rows.Count > 0)
				{
					flag &= Insert(discountChequeData, "Cheque_Discount_Detail", insertUpdateDiscountChequeCommand);
				}
				GLData journalData = CreateChequeDiscountGLData(discountChequeData);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				text4 = "UPDATE Cheque_Received SET DiscountDate='" + CommonLib.ToSqlDateTimeString(DateTime.Parse(dataRow["TransactionDate"].ToString())) + "',DiscountSysDocID='" + dataRow["SysDocID"].ToString() + "',DiscountVoucherID='" + dataRow["VoucherID"].ToString() + "',DiscountAccountID='" + dataRow["LiabilityAccountID"].ToString() + "', DiscountBankAccountID='" + dataRow["BankAccountID"].ToString() + "', Status = 3  WHERE ChequeID IN (" + text3 + ")";
				flag &= (ExecuteNonQuery(text4, sqlTransaction) > 0);
				text4 = "UPDATE  CR SET DiscountAmount = \r\n                            (Select DiscountAmount FROM Cheque_Discount_Detail CDD WHERE CR.ChequeID = CDD.ChequeID AND SYsDocID = '" + text2 + "' AND VoucherID = '" + text + "')\r\n                            FROM  Cheque_Received CR WHERE ChequeID IN (" + text3 + ")";
				flag &= (ExecuteNonQuery(text4, sqlTransaction) > 0);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Cheque_Discount", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Cheque_Discount", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ChequeDiscount, text2, text, "Cheque_Discount", sqlTransaction);
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

		private GLData CreateChequeDiscountGLData(DiscountChequeData discountChequeData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = discountChequeData.ChequeDiscountTable.Rows[0];
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)84;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Note"] = dataRow["Note"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			foreach (DataRow row in discountChequeData.ChequeDiscountDetailTable.Rows)
			{
				int num = int.Parse(row["ChequeID"].ToString());
				DataRow dataRow4 = new ReceivedCheques(base.DBConfig).GetChequeByID(num.ToString()).Tables[0].Rows[0];
				DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["AccountID"] = dataRow["BankAccountID"];
				dataRow5["Debit"] = row["DiscountAmount"];
				dataRow5["Credit"] = DBNull.Value;
				dataRow5["CheckID"] = num;
				dataRow5["CheckNumber"] = dataRow4["ChequeNumber"];
				dataRow5["CheckDate"] = dataRow4["ChequeDate"];
				dataRow5["Description"] = "Discounted Cheque:" + dataRow4["ChequeNumber"].ToString();
				dataRow5["Reference"] = dataRow["Reference"];
				dataRow5["RowIndex"] = -1;
				dataRow5["CompanyID"] = value;
				dataRow5["DivisionID"] = value2;
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
				dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["AccountID"] = dataRow["LiabilityAccountID"];
				dataRow5["Debit"] = DBNull.Value;
				dataRow5["Credit"] = row["DiscountAmount"];
				dataRow5["CheckID"] = num;
				dataRow5["CheckNumber"] = dataRow4["ChequeNumber"];
				dataRow5["CheckDate"] = dataRow4["ChequeDate"];
				dataRow5["Description"] = "Discounted Cheque:" + dataRow4["ChequeNumber"].ToString();
				dataRow5["Reference"] = dataRow["Reference"];
				dataRow5["RowIndex"] = 0;
				dataRow5["CompanyID"] = value;
				dataRow5["DivisionID"] = value2;
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
			}
			decimal result = default(decimal);
			if (dataRow["BankChargeAmount"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["BankChargeAmount"].ToString(), out result);
			}
			if (result > 0m)
			{
				DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["AccountID"] = dataRow["BankAccountID"];
				dataRow5["Debit"] = DBNull.Value;
				dataRow5["Credit"] = dataRow["BankChargeAmount"];
				dataRow5["Description"] = "Discounted Cheques Charges";
				dataRow5["Reference"] = dataRow["Reference"];
				dataRow5["RowIndex"] = 0;
				dataRow5["CompanyID"] = value;
				dataRow5["DivisionID"] = value2;
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
				dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["AccountID"] = dataRow["BankChargeAccountID"];
				dataRow5["Debit"] = dataRow["BankChargeAmount"];
				dataRow5["Credit"] = DBNull.Value;
				dataRow5["Description"] = "Discounted Cheques Charges";
				dataRow5["Reference"] = dataRow["Reference"];
				dataRow5["RowIndex"] = 0;
				dataRow5["CompanyID"] = value;
				dataRow5["DivisionID"] = value2;
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
			}
			decimal result2 = default(decimal);
			if (dataRow["BankChargeAmount"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["BankChargeAmount"].ToString(), out result2);
			}
			if (result2 > 0m)
			{
				DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["AccountID"] = dataRow["BankAccountID"];
				dataRow5["Debit"] = DBNull.Value;
				dataRow5["Credit"] = dataRow["BankCommission"];
				dataRow5["Description"] = "Discounted Cheques Interest";
				dataRow5["Reference"] = dataRow["Reference"];
				dataRow5["RowIndex"] = 0;
				dataRow5["CompanyID"] = value;
				dataRow5["DivisionID"] = value2;
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
				dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["AccountID"] = dataRow["BankInterestAccountID"];
				dataRow5["Debit"] = dataRow["BankCommission"];
				dataRow5["Credit"] = DBNull.Value;
				dataRow5["Description"] = "Discounted Cheques Interest";
				dataRow5["Reference"] = dataRow["Reference"];
				dataRow5["RowIndex"] = 0;
				dataRow5["CompanyID"] = value;
				dataRow5["DivisionID"] = value2;
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
			}
			return gLData;
		}

		public DiscountChequeData GetDiscountChequeByID(string sysDocID, string voucherID)
		{
			try
			{
				DiscountChequeData discountChequeData = new DiscountChequeData();
				string textCommand = "SELECT CD.*,BF.CurrentAccountID,AC.AccountName,BF.LimitAmount FROM Cheque_Discount CD\r\n                                INNER JOIN Bank_Facility BF ON CD.BankFacilityID = BF.FacilityID\r\n                                INNER JOIN Account AC ON AC.AccountID = BF.CurrentAccountID WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(discountChequeData, "Cheque_Discount", textCommand);
				if (discountChequeData == null || discountChequeData.Tables.Count == 0 || discountChequeData.Tables["Cheque_Discount"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT  'True' AS 'D', TD.ChequeID,TD.SysDocID,TD.VoucherID,CQR.ChequeDate AS [Chq Date],CQR.ChequeNumber [Chq #],CQR.BankID,\r\n                            CQR.PayeeID,CQR.PayeeType,CQR.PayeeAccountID,CQR.PDCAccountID,Bank.BankName [Bank Name],TD.BankChargeAmount,TD.DiscountAmount,\r\n                            SendDate, CSD.VoucherID AS [SendVoucherID],\r\n                            CASE CQR.PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,\r\n                            ISNULL(CQR.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,ISNULL(CQR.AmountFC,CQR.Amount) AS Amount\r\n                            FROM   Cheque_Discount_Detail TD \r\n                            INNER JOIN Cheque_Received CQR ON CQR.ChequeID = TD.ChequeID\r\n\t\t\t\t\t\t\tINNER JOIN Bank ON CQR.BankID=Bank.BankID\r\n                            LEFT OUTER JOIN Cheque_Discount TR ON TD.VoucherID=TR.VoucherID AND TD.SysDocID=TR.SysDocID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=CQR.PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=CQR.PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=CQR.PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=CQR.PayeeID\r\n                            LEFT OUTER JOIN Cheque_Send_Detail CSD ON CSD.ChequeID = TD.ChequeID \r\n                            WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(discountChequeData, "Cheque_Discount_Detail", textCommand);
				return discountChequeData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteDiscountChequeDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				bool num = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCDirectMaturity, true).ToString());
				int num2 = 1;
				num2 = (num ? 1 : 4);
				string exp = "UPDATE Cheque_Received SET DiscountSysDocID=NULL,DiscountVoucherID=NULL,DiscountAccountID=NULL,DiscountAmount= NULL, DiscountDate= NULL , DiscountBankAccountID=NULL,Status = " + num2 + "\r\n                             WHERE ChequeID IN (Select ChequeID FROM  Cheque_Discount_Detail CSD WHERE CSD.SysDocID= '" + sysDocID + "' AND CSD.VoucherID = '" + voucherID + "')";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				string commandText = "DELETE FROM Cheque_Discount_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidDiscountCheque(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE Cheque_Discount SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Cheque Discount", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteDiscountCheque(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteDiscountChequeDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Cheque_Discount WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Cheque Discount", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetDiscountChequeToPrint(string sysDocID, string voucherID)
		{
			return GetDiscountChequeToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetDiscountChequeToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,TransactionDate,\r\n                                SI.SalesPersonID,RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,IsVoid,Reference,Discount AS Discount,ISNULL(Total,0) - ISNULL(Discount,0) AS GrandTotal,\r\n                                ISNULL(TaxAmount ,0) AS Tax,Total AS Total,PONumber,SI.Note\r\n                                FROM  Cheque_Discount SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Cheque_Discount", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Cheque_Discount"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,SQD.ProductID,SQD.Description,ISNULL(UnitQuantity,SQD.Quantity) AS Quantity,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,\r\n                        SQD.UnitPrice AS UnitPrice,\r\n                        ISNULL(UnitQuantity,SQD.Quantity) * SQD.UnitPrice AS Total,SQD.UnitID\r\n                        FROM   Cheque_Discount_Detail SQD\r\n                        INNER JOIN Product P ON P.ProductID = SQD.ProductID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Cheque_Discount_Detail", cmdText);
				dataSet.Relations.Add("CustomerQuote", new DataColumn[2]
				{
					dataSet.Tables["Cheque_Discount"].Columns["SysDocID"],
					dataSet.Tables["Cheque_Discount"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Cheque_Discount_Detail"].Columns["SysDocID"],
					dataSet.Tables["Cheque_Discount_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Cheque_Discount"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Cheque_Discount"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Discount"].ToString(), out result2);
					row["TotalInWords"] = NumToWord.GetNumInWords(result - result2);
				}
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
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],Note,TransactionDate [Quote Date]\r\n\r\n                            FROM         Cheque_Discount INV";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Cheque_Discount", sqlCommand);
			return dataSet;
		}

		public DataSet GetDiscountChequesReport(string CustomerID, string BankID, DateTime from, DateTime to, bool Status)
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
			FillDataSet(dataSet, "Cheque_Discount", sqlCommand);
			return dataSet;
		}
	}
}
