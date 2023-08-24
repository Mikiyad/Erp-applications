using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class OpeningBalanceLeave : StoreObject
	{
		private const string BATCHID_PARM = "@BatchID";

		private const string BATCHDATE_PARM = "@BatchDate";

		private const string BATCHTYPE_PARM = "@BatchType";

		private const string REFERENCE_PARM = "@Reference";

		private const string DESCRIPTION_PARM = "@Description";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string OPENINGBALANCELEAVE_TABLE = "Opening_Balance_Leave";

		private const string TRANSACTIONSYSDOCID_PARM = "@TransactionSysDocID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string UNITID_PARM = "@UnitID";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string LEAVESTARTDATE_PARM = "@LeaveStartDate";

		private const string LEAVEENDDATE_PARM = "@LeaveEndDate";

		private const string LEAVETAKEN_PARM = "@LeaveTaken";

		private const string LEAVETYPEID_PARM = "@LeaveTypeID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string PAIDDAYS_PARM = "@PaidDays";

		private const string OPENINGBALANCELEAVEDETAIL_TABLE = "Opening_Balance_Leave_Detail";

		public OpeningBalanceLeave(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateOpeningBalanceLeaveText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Opening_Balance_Leave", new FieldValue("BatchID", "@BatchID", isUpdateConditionField: true), new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("BatchDate", "@BatchDate"), new FieldValue("BatchType", "@BatchType"), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionSysDocID", "@TransactionSysDocID"), new FieldValue("Description", "@Description"), new FieldValue("LocationID", "@LocationID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Opening_Balance_Leave", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateOpeningBalanceLeaveCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateOpeningBalanceLeaveText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateOpeningBalanceLeaveText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@BatchID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@BatchDate", SqlDbType.DateTime);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@BatchType", SqlDbType.TinyInt);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@TransactionSysDocID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters["@BatchID"].SourceColumn = "BatchID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@BatchDate"].SourceColumn = "BatchDate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@BatchType"].SourceColumn = "BatchType";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@TransactionSysDocID"].SourceColumn = "TransactionSysDocID";
			parameters["@LocationID"].SourceColumn = "LocationID";
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

		private string GetInsertUpdateOpeningBalanceLeaveDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Opening_Balance_Leave_Detail", new FieldValue("BatchID", "@BatchID"), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("TransactionSysDocID", "@TransactionSysDocID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("BatchType", "@BatchType"), new FieldValue("Description", "@Description"), new FieldValue("Reference", "@Reference"), new FieldValue("LeaveTypeID", "@LeaveTypeID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("LeaveStartDate", "@LeaveStartDate"), new FieldValue("LeaveEndDate", "@LeaveEndDate"), new FieldValue("LeaveTaken", "@LeaveTaken"), new FieldValue("PaidDays", "@PaidDays"), new FieldValue("LocationID", "@LocationID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateOpeningBalanceLeaveDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateOpeningBalanceLeaveDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateOpeningBalanceLeaveDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@BatchID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionSysDocID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@BatchType", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@LeaveTypeID", SqlDbType.NVarChar);
			parameters.Add("@LeaveTaken", SqlDbType.Int);
			parameters.Add("@PaidDays", SqlDbType.Int);
			parameters.Add("@LeaveStartDate", SqlDbType.DateTime);
			parameters.Add("@LeaveEndDate", SqlDbType.DateTime);
			parameters["@BatchID"].SourceColumn = "BatchID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionSysDocID"].SourceColumn = "TransactionSysDocID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@LeaveTypeID"].SourceColumn = "LeaveTypeID";
			parameters["@LeaveTaken"].SourceColumn = "LeaveTaken";
			parameters["@PaidDays"].SourceColumn = "PaidDays";
			parameters["@BatchType"].SourceColumn = "BatchType";
			parameters["@LeaveStartDate"].SourceColumn = "LeaveStartDate";
			parameters["@LeaveEndDate"].SourceColumn = "LeaveEndDate";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(OpeningBalanceLeaveData journalData)
		{
			return true;
		}

		public bool InsertUpdateOpeningBalanceLeave(OpeningBalanceLeaveData openingBalanceBatchData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateOpeningBalanceLeaveCommand = GetInsertUpdateOpeningBalanceLeaveCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = openingBalanceBatchData.OpeningBalanceLeaveTable.Rows[0];
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text2 = dataRow["BatchID"].ToString();
				string text3 = dataRow["SysDocID"].ToString();
				string text4 = dataRow["LocationID"].ToString();
				if (!isUpdate)
				{
					string exp = "SELECT COUNT(BatchID)  FROM Opening_Balance_Leave WHERE BatchID = '" + text2 + "' AND SysDocID ='" + text3 + "'";
					object obj = ExecuteScalar(exp, sqlTransaction);
					if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
					{
						throw new CompanyException("Document number already exist.", 1046);
					}
				}
				if (isUpdate)
				{
					flag &= DeleteOpeningBalanceLeaveDetailsRows(text3, text2, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(88, text3, text2, isDeletingTransaction: false, sqlTransaction);
				}
				OpeningBalanceBatchTypes openingBalanceBatchTypes = (OpeningBalanceBatchTypes)int.Parse(dataRow["BatchType"].ToString());
				if (openingBalanceBatchTypes != OpeningBalanceBatchTypes.Leave)
				{
					_ = string.Empty;
				}
				else
				{
					new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text4, sqlTransaction).ToString();
				}
				insertUpdateOpeningBalanceLeaveCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(openingBalanceBatchData, "Opening_Balance_Leave", insertUpdateOpeningBalanceLeaveCommand)) : (flag & Insert(openingBalanceBatchData, "Opening_Balance_Leave", insertUpdateOpeningBalanceLeaveCommand)));
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				decimal num = default(decimal);
				switch (openingBalanceBatchTypes)
				{
				case OpeningBalanceBatchTypes.Customer:
				{
					decimal num4 = default(decimal);
					Dictionary<string, decimal> dictionary2 = new Dictionary<string, decimal>();
					ArrayList arrayList2 = new ArrayList();
					foreach (DataRow row in openingBalanceBatchData.OpeningBalanceLeaveDetailsTable.Rows)
					{
						_ = row;
						string text11 = "";
						decimal d2 = default(decimal);
						if (dictionary2.ContainsKey(text11))
						{
							num = decimal.Parse(dictionary2[text11].ToString());
							num = (dictionary2[text11] = num + Math.Round(d2, currencyDecimalPoints));
						}
						else
						{
							dictionary2.Add(text11, Math.Round(d2, currencyDecimalPoints));
							arrayList2.Add(text11);
						}
					}
					string text12 = "";
					foreach (string item in arrayList2)
					{
						if (text12 != "")
						{
							text12 += ",";
						}
						text12 = text12 + "('" + item + "')";
					}
					text = "SELECT * FROM\r\n                                      (values " + text12 + ") as T(ID)\r\n                                    EXCEPT\r\n                                    SELECT CustomerID FROM Customer";
					DataSet dataSet2 = new DataSet();
					FillDataSet(dataSet2, "Customer", text, sqlTransaction);
					if (dataSet2 != null && dataSet2.Tables["Customer"].Rows.Count > 0)
					{
						string text13 = "";
						foreach (DataRow row2 in dataSet2.Tables[0].Rows)
						{
							if (text13 != "")
							{
								text13 += ",";
							}
							text13 = text13 + "'" + row2[0].ToString() + "'";
						}
						throw new CompanyException("One or more customer codes does not exist in customer list.\nCustomers:" + text13, 1055);
					}
					GLData gLData2 = new GLData();
					DataRow dataRow6 = gLData2.JournalTable.NewRow();
					dataRow6["JournalID"] = 0;
					dataRow6["JournalDate"] = dataRow["BatchDate"];
					dataRow6["SysDocID"] = dataRow["SysDocID"];
					dataRow6["VoucherID"] = dataRow["BatchID"];
					dataRow6["Reference"] = dataRow["Reference"];
					dataRow6["Reference2"] = "";
					dataRow6["SysDocType"] = (byte)202;
					if (dataRow["SysDocID"] != null && dataRow["SysDocID"].ToString() != baseCurrencyID)
					{
						dataRow6["CurrencyID"] = dataRow["SysDocID"].ToString();
					}
					else
					{
						dataRow6["CurrencyID"] = DBNull.Value;
						dataRow6["CurrencyRate"] = 1;
					}
					dataRow6["Note"] = dataRow["Description"].ToString();
					dataRow6.EndEdit();
					gLData2.JournalTable.Rows.Add(dataRow6);
					num4 = default(decimal);
					DataRow dataRow7;
					for (int j = 0; j < arrayList2.Count; j++)
					{
						dataRow7 = gLData2.JournalDetailsTable.NewRow();
						dataRow7.BeginEdit();
						dataRow7["JournalID"] = 0;
						string text14 = arrayList2[j].ToString();
						num = dictionary2[text14];
						num4 += num;
						string text15 = (string)(dataRow7["PayeeType"] = "C");
						dataRow7["PayeeID"] = text14;
						text = "  SELECT  ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID FROM  Customer CUS \r\n                                             LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                          LEFT OUTER JOIN Location LOC ON Loc.LocationID  = '" + text4 + "' WHERE CUstomerID = '" + text14 + "'";
						string text16 = (string)(dataRow7["AccountID"] = (ExecuteScalar(text) as string));
						if (dataRow["SysDocID"] != null && dataRow["SysDocID"].ToString() != "" && dataRow["SysDocID"].ToString() != baseCurrencyID)
						{
							if (num > 0m)
							{
								dataRow7["DebitFC"] = num;
								dataRow7["CreditFC"] = DBNull.Value;
							}
							else
							{
								dataRow7["CreditFC"] = Math.Abs(num);
								dataRow7["DebitFC"] = DBNull.Value;
							}
						}
						else
						{
							if (num > 0m)
							{
								dataRow7["Debit"] = num;
								dataRow7["Credit"] = DBNull.Value;
							}
							else
							{
								dataRow7["Credit"] = Math.Abs(num);
								dataRow7["Debit"] = DBNull.Value;
							}
							dataRow7["DebitFC"] = DBNull.Value;
							dataRow7["CreditFC"] = DBNull.Value;
						}
						dataRow7["Description"] = dataRow["Description"];
						dataRow7["Reference"] = dataRow["Reference"];
						dataRow7["RowIndex"] = 0;
						dataRow7.EndEdit();
						gLData2.JournalDetailsTable.Rows.Add(dataRow7);
					}
					dataRow7 = gLData2.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					dataRow7["PayeeType"] = "A";
					if (dataRow["SysDocID"] != null && dataRow["SysDocID"].ToString() != "" && dataRow["SysDocID"].ToString() != baseCurrencyID)
					{
						if (num4 > 0m)
						{
							dataRow7["CreditFC"] = num4;
							dataRow7["DebitFC"] = DBNull.Value;
						}
						else
						{
							dataRow7["DebitFC"] = Math.Abs(num4);
							dataRow7["CreditFC"] = DBNull.Value;
						}
					}
					else
					{
						if (num4 > 0m)
						{
							dataRow7["Credit"] = num4;
							dataRow7["Debit"] = DBNull.Value;
						}
						else
						{
							dataRow7["Debit"] = Math.Abs(num4);
							dataRow7["Credit"] = DBNull.Value;
						}
						dataRow7["DebitFC"] = DBNull.Value;
						dataRow7["CreditFC"] = DBNull.Value;
					}
					dataRow7["Description"] = dataRow["Description"];
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7["RowIndex"] = 0;
					dataRow7.EndEdit();
					gLData2.JournalDetailsTable.Rows.Add(dataRow7);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(gLData2, isUpdate, sqlTransaction);
					flag &= InsertARJournal(openingBalanceBatchData, sqlTransaction);
					break;
				}
				case OpeningBalanceBatchTypes.Vendor:
				{
					decimal num2 = default(decimal);
					Dictionary<string, decimal> dictionary = new Dictionary<string, decimal>();
					ArrayList arrayList = new ArrayList();
					foreach (DataRow row3 in openingBalanceBatchData.OpeningBalanceLeaveDetailsTable.Rows)
					{
						string text5 = "";
						decimal d = decimal.Parse(row3["LeaveTaken"].ToString());
						if (dictionary.ContainsKey(text5))
						{
							num = decimal.Parse(dictionary[text5].ToString());
							num = (dictionary[text5] = num + Math.Round(d, currencyDecimalPoints));
						}
						else
						{
							dictionary.Add(text5, Math.Round(d, currencyDecimalPoints));
							arrayList.Add(text5);
						}
					}
					string text6 = "";
					foreach (string item2 in arrayList)
					{
						if (text6 != "")
						{
							text6 += ",";
						}
						text6 = text6 + "('" + item2 + "')";
					}
					text = "SELECT * FROM\r\n                                      (values " + text6 + ") as T(ID)\r\n                                    EXCEPT\r\n                                    SELECT VendorID FROM Vendor";
					DataSet dataSet = new DataSet();
					FillDataSet(dataSet, "Vendor", text, sqlTransaction);
					if (dataSet != null && dataSet.Tables["Vendor"].Rows.Count > 0)
					{
						string text7 = "";
						foreach (DataRow row4 in dataSet.Tables[0].Rows)
						{
							if (text7 != "")
							{
								text7 += ",";
							}
							text7 = text7 + "'" + row4[0].ToString() + "'";
						}
						throw new CompanyException("One or more vendor codes does not exist in vendor list.\nVendors:" + text7, 1055);
					}
					GLData gLData = new GLData();
					DataRow dataRow3 = gLData.JournalTable.NewRow();
					dataRow3["JournalID"] = 0;
					dataRow3["JournalDate"] = dataRow["BatchDate"];
					dataRow3["SysDocID"] = dataRow["SysDocID"];
					dataRow3["SysDocType"] = (byte)203;
					dataRow3["VoucherID"] = dataRow["BatchID"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["Reference2"] = "";
					if (dataRow["SysDocID"] != null && dataRow["SysDocID"].ToString() != baseCurrencyID)
					{
						dataRow3["CurrencyID"] = dataRow["SysDocID"].ToString();
					}
					else
					{
						dataRow3["CurrencyID"] = DBNull.Value;
						dataRow3["CurrencyRate"] = 1;
					}
					dataRow3["Note"] = dataRow["Description"].ToString();
					dataRow3.EndEdit();
					gLData.JournalTable.Rows.Add(dataRow3);
					num2 = default(decimal);
					DataRow dataRow4;
					for (int i = 0; i < arrayList.Count; i++)
					{
						try
						{
							dataRow4 = gLData.JournalDetailsTable.NewRow();
							dataRow4.BeginEdit();
							dataRow4["JournalID"] = 0;
							string text8 = arrayList[i].ToString();
							num = dictionary[text8];
							num2 += num;
							string text9 = (string)(dataRow4["PayeeType"] = "V");
							dataRow4["PayeeID"] = text8;
							text = "  SELECT  ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID FROM  Vendor VEN \r\n                                           LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                          LEFT OUTER JOIN Location LOC ON Loc.LocationID  = '" + text4 + "' WHERE VendorID = '" + text8 + "'";
							string text10 = (string)(dataRow4["AccountID"] = (ExecuteScalar(text) as string));
							if (dataRow["SysDocID"] != null && dataRow["SysDocID"].ToString() != "" && dataRow["SysDocID"].ToString() != baseCurrencyID)
							{
								if (num > 0m)
								{
									dataRow4["CreditFC"] = num;
									dataRow4["DebitFC"] = DBNull.Value;
								}
								else
								{
									dataRow4["DebitFC"] = Math.Abs(num);
									dataRow4["CreditFC"] = DBNull.Value;
								}
							}
							else
							{
								if (num > 0m)
								{
									dataRow4["Credit"] = num;
									dataRow4["Debit"] = DBNull.Value;
								}
								else
								{
									dataRow4["Debit"] = Math.Abs(num);
									dataRow4["Credit"] = DBNull.Value;
								}
								dataRow4["DebitFC"] = DBNull.Value;
								dataRow4["CreditFC"] = DBNull.Value;
							}
							dataRow4["Description"] = dataRow["Description"];
							dataRow4["Reference"] = dataRow["Reference"];
							dataRow4["RowIndex"] = 0;
							dataRow4.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow4);
						}
						catch (Exception)
						{
							throw;
						}
					}
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["PayeeType"] = "A";
					if (dataRow["SysDocID"] != null && dataRow["SysDocID"].ToString() != "" && dataRow["SysDocID"].ToString() != baseCurrencyID)
					{
						if (num2 > 0m)
						{
							dataRow4["DebitFC"] = num2;
							dataRow4["CreditFC"] = DBNull.Value;
						}
						else
						{
							dataRow4["CreditFC"] = Math.Abs(num2);
							dataRow4["DebitFC"] = DBNull.Value;
						}
					}
					else
					{
						if (num2 > 0m)
						{
							dataRow4["Debit"] = num2;
							dataRow4["Credit"] = DBNull.Value;
						}
						else
						{
							dataRow4["Credit"] = Math.Abs(num2);
							dataRow4["Debit"] = DBNull.Value;
						}
						dataRow4["DebitFC"] = DBNull.Value;
						dataRow4["CreditFC"] = DBNull.Value;
					}
					dataRow4["Description"] = dataRow["Description"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["RowIndex"] = 0;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(gLData, isUpdate, sqlTransaction);
					flag &= InsertAPJournal(openingBalanceBatchData, sqlTransaction);
					break;
				}
				}
				if (openingBalanceBatchData.Tables["Opening_Balance_Leave_Detail"].Rows.Count > 0)
				{
					insertUpdateOpeningBalanceLeaveCommand = GetInsertUpdateOpeningBalanceLeaveDetailsCommand(isUpdate: false);
					insertUpdateOpeningBalanceLeaveCommand.Transaction = sqlTransaction;
					flag &= Insert(openingBalanceBatchData, "Opening_Balance_Leave_Detail", insertUpdateOpeningBalanceLeaveCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Opening_Balance_Leave", "SysDocID", dataRow["SysDocID"].ToString(), "BatchID", dataRow["BatchID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Opening Balance Batch";
				if (isUpdate)
				{
					flag &= AddActivityLog(entityName, text2, text3, ActivityTypes.Update, sqlTransaction);
					return flag;
				}
				flag &= AddActivityLog(entityName, text2, text3, ActivityTypes.Add, sqlTransaction);
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

		private bool InsertARJournal(OpeningBalanceLeaveData openingBalanceBatchData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			ARJournalData aRJournalData = new ARJournalData();
			DataTable dataTable = aRJournalData.Tables["ARJournal"];
			string text = openingBalanceBatchData.OpeningBalanceLeaveTable.Rows[0]["LocationID"].ToString();
			string text2 = openingBalanceBatchData.OpeningBalanceLeaveTable.Rows[0]["CurrencyID"].ToString();
			decimal d = 1m;
			string a = "M";
			bool flag2 = false;
			if (text2 != new Currencies(base.DBConfig).GetBaseCurrencyID())
			{
				flag2 = true;
				d = decimal.Parse(openingBalanceBatchData.OpeningBalanceLeaveTable.Rows[0]["CurrencyRate"].ToString());
				a = new Currencies(base.DBConfig).GetCurrencyRateType(text2);
			}
			DataRow dataRow = openingBalanceBatchData.OpeningBalanceLeaveTable.Rows[0];
			Hashtable hashtable = new Hashtable();
			foreach (DataRow row in openingBalanceBatchData.OpeningBalanceLeaveDetailsTable.Rows)
			{
				row["SysDocID"] = dataRow["SysDocID"];
				row["VoucherID"] = dataRow["BatchID"];
				string text3 = "";
				DateTime.Parse(row["TransactionDate"].ToString());
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["VoucherID"] = dataRow["BatchID"];
				dataRow3["Reference"] = row["VoucherID"];
				string text4 = "";
				if (hashtable.ContainsKey(text3))
				{
					text4 = hashtable[text3].ToString();
				}
				else
				{
					string exp = "  SELECT  ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID FROM  Customer CUS \r\n                                             LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                          LEFT OUTER JOIN Location LOC ON Loc.LocationID  = '" + text + "' WHERE CUstomerID = '" + text3 + "'";
					text4 = ExecuteScalar(exp).ToString();
					hashtable.Add(text3, text4);
				}
				if (text4 == "")
				{
					throw new Exception("Account receivable not found for customer: " + text3);
				}
				dataRow3["ARAccountID"] = text4;
				decimal num = decimal.Parse(row["LeaveTaken"].ToString());
				decimal num2 = default(decimal);
				if (flag2)
				{
					num2 = ((!(a == "M")) ? Math.Round(num / d, 4) : Math.Round(num * d, 4));
					if (num2 > 0m)
					{
						dataRow3["Debit"] = num2;
						dataRow3["DebitFC"] = num;
					}
					else
					{
						dataRow3["Credit"] = Math.Abs(num2);
						dataRow3["CreditFC"] = Math.Abs(num);
					}
				}
				else if (num > 0m)
				{
					dataRow3["Debit"] = num;
				}
				else
				{
					dataRow3["Credit"] = Math.Abs(num);
				}
				dataRow3["CurrencyID"] = dataRow["CurrencyID"];
				dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
				dataRow3["Description"] = row["Description"];
				dataRow3.EndEdit();
				dataTable.Rows.Add(dataRow3);
			}
			return flag & new ARJournal(base.DBConfig).InsertJournal(aRJournalData, sqlTransaction);
		}

		private bool InsertAPJournal(OpeningBalanceLeaveData openingBalanceBatchData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			APJournalData aPJournalData = new APJournalData();
			DataTable dataTable = aPJournalData.Tables["APJournal"];
			string text = openingBalanceBatchData.OpeningBalanceLeaveTable.Rows[0]["LocationID"].ToString();
			string text2 = openingBalanceBatchData.OpeningBalanceLeaveTable.Rows[0]["CurrencyID"].ToString();
			decimal d = 1m;
			string a = "M";
			bool flag2 = false;
			if (text2 != new Currencies(base.DBConfig).GetBaseCurrencyID())
			{
				flag2 = true;
				d = decimal.Parse(openingBalanceBatchData.OpeningBalanceLeaveTable.Rows[0]["CurrencyRate"].ToString());
				a = new Currencies(base.DBConfig).GetCurrencyRateType(text2);
			}
			DataRow dataRow = openingBalanceBatchData.OpeningBalanceLeaveTable.Rows[0];
			Hashtable hashtable = new Hashtable();
			foreach (DataRow row in openingBalanceBatchData.OpeningBalanceLeaveDetailsTable.Rows)
			{
				row["SysDocID"] = dataRow["SysDocID"];
				row["VoucherID"] = dataRow["BatchID"];
				string text3 = "";
				DateTime.Parse(row["TransactionDate"].ToString());
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["VoucherID"] = dataRow["BatchID"];
				dataRow3["Reference"] = row["Reference"];
				string text4 = "";
				if (hashtable.ContainsKey(text3))
				{
					text4 = hashtable[text3].ToString();
				}
				else
				{
					string exp = "  SELECT  ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID FROM  Vendor VEN \r\n                                         LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                          LEFT OUTER JOIN Location LOC ON Loc.LocationID  = '" + text + "' WHERE VendorID = '" + text3 + "'";
					text4 = ExecuteScalar(exp).ToString();
					hashtable.Add(text3, text4);
				}
				if (text4 == "")
				{
					throw new Exception("Account payable not found for vendor: " + text3);
				}
				dataRow3["APAccountID"] = text4;
				decimal num = decimal.Parse(row["LeaveTaken"].ToString());
				decimal num2 = default(decimal);
				if (flag2)
				{
					num2 = ((!(a == "M")) ? Math.Round(num / d, 4) : Math.Round(num * d, 4));
					if (num2 > 0m)
					{
						dataRow3["Credit"] = num2;
						dataRow3["CreditFC"] = num;
					}
					else
					{
						dataRow3["Debit"] = Math.Abs(num2);
						dataRow3["DebitFC"] = Math.Abs(num);
					}
				}
				else if (num > 0m)
				{
					dataRow3["Credit"] = num;
				}
				else
				{
					dataRow3["Debit"] = Math.Abs(num);
				}
				dataRow3["CurrencyID"] = dataRow["CurrencyID"];
				dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
				dataRow3["Description"] = row["Description"];
				dataRow3["APDate"] = row["TransactionDate"];
				dataRow3.EndEdit();
				dataTable.Rows.Add(dataRow3);
			}
			return flag & new APJournal(base.DBConfig).InsertJournal(aPJournalData, sqlTransaction);
		}

		private bool InsertEmployeeJournal(OpeningBalanceLeaveData openingBalanceBatchData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			EmployeeJournalData employeeJournalData = new EmployeeJournalData();
			DataTable dataTable = employeeJournalData.Tables["Employee_Journal"];
			string text = openingBalanceBatchData.OpeningBalanceLeaveTable.Rows[0]["LocationID"].ToString();
			string text2 = openingBalanceBatchData.OpeningBalanceLeaveTable.Rows[0]["CurrencyID"].ToString();
			decimal d = 1m;
			string a = "M";
			bool flag2 = false;
			if (text2 != new Currencies(base.DBConfig).GetBaseCurrencyID())
			{
				flag2 = true;
				d = decimal.Parse(openingBalanceBatchData.OpeningBalanceLeaveTable.Rows[0]["CurrencyRate"].ToString());
				a = new Currencies(base.DBConfig).GetCurrencyRateType(text2);
			}
			DataRow dataRow = openingBalanceBatchData.OpeningBalanceLeaveTable.Rows[0];
			Hashtable hashtable = new Hashtable();
			foreach (DataRow row in openingBalanceBatchData.OpeningBalanceLeaveDetailsTable.Rows)
			{
				row["SysDocID"] = dataRow["SysDocID"];
				row["VoucherID"] = dataRow["BatchID"];
				string text3 = "";
				DateTime.Parse(row["TransactionDate"].ToString());
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["VoucherID"] = dataRow["BatchID"];
				dataRow3["Reference"] = row["VoucherID"];
				string text4 = "";
				if (hashtable.ContainsKey(text3))
				{
					text4 = hashtable[text3].ToString();
				}
				else
				{
					string exp = "  SELECT ISNULL(EMP.AccountID,ISNULL(ET.AccountID, LOC.EmployeeAccountID)) AS EmpAccountID FROM  Employee EMP \r\n                                             LEFT OUTER JOIN Employee_Type ET ON EMP.ContractType = ET.TypeID\r\n                                          LEFT OUTER JOIN Location LOC ON Loc.LocationID  = '" + text + "' WHERE EmployeeID = '" + text3 + "'";
					text4 = ExecuteScalar(exp).ToString();
					hashtable.Add(text3, text4);
				}
				if (text4 == "")
				{
					throw new Exception("Account not found for employee: " + text3);
				}
				dataRow3["AccountID"] = text4;
				decimal num = decimal.Parse(row["LeaveTaken"].ToString());
				decimal num2 = default(decimal);
				if (flag2)
				{
					num2 = ((!(a == "M")) ? Math.Round(num / d, 4) : Math.Round(num * d, 4));
					if (num2 > 0m)
					{
						dataRow3["Credit"] = num2;
						dataRow3["CreditFC"] = num;
					}
					else
					{
						dataRow3["Debit"] = Math.Abs(num2);
						dataRow3["DebitFC"] = Math.Abs(num);
					}
				}
				else if (num > 0m)
				{
					dataRow3["Credit"] = num;
				}
				else
				{
					dataRow3["Debit"] = Math.Abs(num);
				}
				dataRow3["CurrencyID"] = dataRow["CurrencyID"];
				dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
				dataRow3["Description"] = row["Description"];
				dataRow3.EndEdit();
				dataTable.Rows.Add(dataRow3);
			}
			return flag & new EmployeeJournal(base.DBConfig).InsertJournal(employeeJournalData, sqlTransaction);
		}

		public string GetNextBatchNumber(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			int num = 1;
			string text2 = "";
			string textCommand = "SELECT MAX(BatchID) AS LastNumber FROM Opening_Balance_Leave WHERE SysDocID='" + sysDocID + "'";
			FillDataSet(dataSet, "System_Document", textCommand);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				text2 = dataSet.Tables[0].Rows[0]["LastNumber"].ToString();
			}
			for (int i = 0; i < text2.Length && !char.IsNumber(text2[i]); i++)
			{
				text += text2[i].ToString();
			}
			if (text2 != "")
			{
				num = int.Parse(text2.Substring(text.Length)) + 1;
				int num2 = text2.Length - text.Length;
				string text3 = "";
				for (int j = 0; j < num2; j++)
				{
					text3 += "0";
				}
				return text + num.ToString(text3);
			}
			return text + num.ToString("00000000");
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			StoreConfiguration.ToSqlDateTimeString(from);
			StoreConfiguration.ToSqlDateTimeString(to);
			SqlCommand sqlCommand = new SqlCommand("select distinct t.[BatchID] as [Batch NUmber],t.BatchDate, \r\n                          STUFF((SELECT distinct ', ' + t1.EmployeeID\r\n                                 from Opening_Balance_Leave_Detail t1\r\n                                 where t.BatchID = t1.BatchID\r\n                                    FOR XML PATH(''), TYPE\r\n                                    ).value('.', 'NVARCHAR(MAX)') \r\n                                ,1,2,'') Employee, t.Reference, t.LocationID AS Location\r\n\t\t\t\t\t\t\t\t\r\n                        from Opening_Balance_Leave t\r\n                            ");
			FillDataSet(dataSet, "Opening_Balance_Leave", sqlCommand);
			return dataSet;
		}

		private GLData CreateInventoryOpeningBalanceLeaveGLData(OpeningBalanceLeaveData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.OpeningBalanceLeaveTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string value = "";
			string text = dataRow["LocationID"].ToString();
			string sysDocID = dataRow["SysDocID"].ToString();
			string text2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text, sqlTransaction).ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.OpeningBalanceItem;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["BatchDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["BatchID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Opening Inventory";
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			decimal num = default(decimal);
			decimal d = default(decimal);
			foreach (DataRow row in transactionData.OpeningBalanceLeaveDetailsTable.Rows)
			{
				decimal d2 = default(decimal);
				decimal d3 = decimal.Parse(row["Quantity"].ToString());
				decimal d4 = decimal.Parse(row["Cost"].ToString());
				string productID = row["ProductID"].ToString();
				row["VoucherID"].ToString();
				int.Parse(row["RowIndex"].ToString());
				DataSet productTransactionAccounts = new Products(base.DBConfig).GetProductTransactionAccounts(productID, text, text, sysDocID, sqlTransaction);
				if (productTransactionAccounts == null || productTransactionAccounts.Tables.Count == 0 || productTransactionAccounts.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("Product accounts information not found for product or location.");
				}
				text2 = productTransactionAccounts.Tables[0].Rows[0]["InventoryAssetAccountID"].ToString();
				d2 += d3 * d4;
				d += Math.Round(d2, currencyDecimalPoints);
				if (hashtable.ContainsKey(text2))
				{
					num = decimal.Parse(hashtable[text2].ToString());
					num += Math.Round(d2, currencyDecimalPoints);
					hashtable[text2] = num;
				}
				else
				{
					hashtable.Add(text2, Math.Round(d2, currencyDecimalPoints));
					arrayList.Add(text2);
				}
			}
			d = Math.Round(d, currencyDecimalPoints);
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			if (d != 0m)
			{
				for (int i = 0; i < hashtable.Count; i++)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					text2 = arrayList[i].ToString();
					num = decimal.Parse(hashtable[text2].ToString());
					if (num > 0m)
					{
						dataRow3["Debit"] = num;
						dataRow3["Credit"] = DBNull.Value;
					}
					else
					{
						dataRow3["Debit"] = DBNull.Value;
						dataRow3["Credit"] = Math.Abs(num);
					}
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = text2;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = value;
			if (d > 0m)
			{
				dataRow3["Debit"] = DBNull.Value;
				dataRow3["Credit"] = d;
			}
			else
			{
				dataRow3["Debit"] = Math.Abs(d);
				dataRow3["Credit"] = DBNull.Value;
			}
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		public OpeningBalanceLeaveData GetOpeningBalanceLeaveByID(string sysDocID, string voucherID)
		{
			try
			{
				OpeningBalanceLeaveData openingBalanceLeaveData = new OpeningBalanceLeaveData();
				string textCommand = "SELECT * FROM Opening_Balance_Leave WHERE BatchID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(openingBalanceLeaveData, "Opening_Balance_Leave", textCommand);
				if (openingBalanceLeaveData == null || openingBalanceLeaveData.Tables.Count == 0 || openingBalanceLeaveData.Tables["Opening_Balance_Leave"].Rows.Count == 0)
				{
					return null;
				}
				if (int.Parse(openingBalanceLeaveData.Tables[0].Rows[0]["BatchType"].ToString()) == 5)
				{
					textCommand = "SELECT TD.*,EMP.FirstName + ' ' + EMP.LastName AS Name\r\n                        FROM Opening_Balance_Leave_Detail TD INNER JOIN Employee EMP ON EMP.EmployeeID = TD.EmployeeID\r\n                        WHERE BatchID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				}
				FillDataSet(openingBalanceLeaveData, "Opening_Balance_Leave_Detail", textCommand);
				return openingBalanceLeaveData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteOpeningBalanceLeaveDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Opening_Balance_Leave_Detail WHERE SysDocID = '" + sysDocID + "' AND BatchID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidOpeningBalanceLeave(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteOpeningBalanceLeave(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = " SELECT BatchType FROM Opening_Balance_Leave WHERE SysDocID = '" + sysDocID + "' AND BatchID = '" + voucherID + "' ";
				object obj = ExecuteScalar(text, sqlTransaction);
				if (obj == null || obj.ToString() == "")
				{
					throw new CompanyException("Batch information not found.");
				}
				if (byte.Parse(obj.ToString()) == 4)
				{
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(88, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
					text = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					flag &= Delete(text, sqlTransaction);
				}
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteOpeningBalanceLeaveDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Opening_Balance_Leave WHERE SysDocID = '" + sysDocID + "' AND BatchID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Opening Balance Leave", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetOpeningBalanceLeaveToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT OP.*, CASE OP.BatchType WHEN 1 THEN 'Customer' WHEN 2 THEN 'Vendor' WHEN 3 THEN 'Employee' WHEN 4 THEN 'Inventory' END AS BatchTypeName, \r\n                                LOC.LocationName FROM Opening_Balance_Leave OP\r\n                                INNER JOIN Location LOC ON Loc.LocationID = OP.LocationID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND BatchID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Opening_Balance_Leave", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Opening_Balance_Leave"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT OPD.*,\r\n                                  CASE OP.BatchType WHEN 1 THEN (SELECT CustomerName FROM Customer WHERE CustomerID = OPD.AccountID)  \r\n                                    WHEN 2 THEN (SELECT VendorName FROM Vendor WHERE VendorID = OPD.AccountID) END AS Name \r\n                                    FROM Opening_Balance_Leave_Detail OPD\r\n\t\t\t\t\t\t\t\t\t  INNER JOIN Opening_Balance_Leave OP ON OPD.BatchID = OP.BatchID AND OPD.SysDocID = OP.SysDocID\r\n                        WHERE OPD.SysDocID='" + sysDocID + "' AND OPD.BatchID IN (" + text + ")\r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Opening_Balance_Leave_Detail", cmdText);
				dataSet.Relations.Add("OpeningBalanceDetail", new DataColumn[2]
				{
					dataSet.Tables["Opening_Balance_Leave"].Columns["SysDocID"],
					dataSet.Tables["Opening_Balance_Leave"].Columns["BatchID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Opening_Balance_Leave_Detail"].Columns["SysDocID"],
					dataSet.Tables["Opening_Balance_Leave_Detail"].Columns["BatchID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
