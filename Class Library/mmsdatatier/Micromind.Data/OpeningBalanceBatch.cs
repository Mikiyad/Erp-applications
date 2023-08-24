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
	public sealed class OpeningBalanceBatch : StoreObject
	{
		private const string BATCHID_PARM = "@BatchID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string BATCHDATE_PARM = "@BatchDate";

		private const string BATCHTYPE_PARM = "@BatchType";

		private const string REFERENCE_PARM = "@Reference";

		private const string DESCRIPTION_PARM = "@Description";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string OPENINGBALANCEBATCH_TABLE = "Opening_Balance_Batch";

		private const string TRANSACTIONSYSDOCID_PARM = "@TransactionSysDocID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public const string PRODUCTID_PARM = "@ProductID";

		public const string UNITID_PARM = "@UnitID";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string PURCHASEDATE_PARM = "@PurchaseDate";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string DUEDATE_PARM = "@DueDate";

		private const string INVOICEAMOUNT_PARM = "@InvoiceAmount";

		private const string BALANCEAMOUNT_PARM = "@BalanceAmount";

		private const string COST_PARM = "@Cost";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string OPENINGBALANCEBATCHDETAIL_TABLE = "Opening_Balance_Batch_Detail";

		public OpeningBalanceBatch(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateOpeningBalanceBatchText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Opening_Balance_Batch", new FieldValue("BatchID", "@BatchID", isUpdateConditionField: true), new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("BatchDate", "@BatchDate"), new FieldValue("BatchType", "@BatchType"), new FieldValue("Reference", "@Reference"), new FieldValue("AccountID", "@AccountID"), new FieldValue("TransactionSysDocID", "@TransactionSysDocID"), new FieldValue("Description", "@Description"), new FieldValue("LocationID", "@LocationID"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Opening_Balance_Batch", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateOpeningBalanceBatchCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateOpeningBalanceBatchText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateOpeningBalanceBatchText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@BatchID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@BatchDate", SqlDbType.DateTime);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@BatchType", SqlDbType.TinyInt);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@TransactionSysDocID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Money);
			parameters["@BatchID"].SourceColumn = "BatchID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@BatchDate"].SourceColumn = "BatchDate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@BatchType"].SourceColumn = "BatchType";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@TransactionSysDocID"].SourceColumn = "TransactionSysDocID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
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

		private string GetInsertUpdateOpeningBalanceBatchDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Opening_Balance_Batch_Detail", new FieldValue("BatchID", "@BatchID"), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("TransactionSysDocID", "@TransactionSysDocID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("AccountID", "@AccountID"), new FieldValue("Description", "@Description"), new FieldValue("Reference", "@Reference"), new FieldValue("ProductID", "@ProductID"), new FieldValue("Cost", "@Cost"), new FieldValue("LocationID", "@LocationID"), new FieldValue("Quantity", "@Quantity"), new FieldValue("PurchaseDate", "@PurchaseDate"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("DueDate", "@DueDate"), new FieldValue("InvoiceAmount", "@InvoiceAmount"), new FieldValue("BalanceAmount", "@BalanceAmount"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateOpeningBalanceBatchDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateOpeningBalanceBatchDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateOpeningBalanceBatchDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@BatchID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionSysDocID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@Cost", SqlDbType.Money);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@PurchaseDate", SqlDbType.DateTime);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@InvoiceAmount", SqlDbType.Money);
			parameters.Add("@BalanceAmount", SqlDbType.Money);
			parameters["@BatchID"].SourceColumn = "BatchID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionSysDocID"].SourceColumn = "TransactionSysDocID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@PurchaseDate"].SourceColumn = "PurchaseDate";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@InvoiceAmount"].SourceColumn = "InvoiceAmount";
			parameters["@BalanceAmount"].SourceColumn = "BalanceAmount";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(OpeningBalanceBatchData journalData)
		{
			return true;
		}

		public bool InsertUpdateOpeningBalanceBatch(OpeningBalanceBatchData openingBalanceBatchData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateOpeningBalanceBatchCommand = GetInsertUpdateOpeningBalanceBatchCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = openingBalanceBatchData.OpeningBalanceBatchTable.Rows[0];
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text2 = dataRow["BatchID"].ToString();
				string text3 = dataRow["SysDocID"].ToString();
				string text4 = dataRow["LocationID"].ToString();
				if (!isUpdate)
				{
					string exp = "SELECT COUNT(BatchID)  FROM Opening_Balance_Batch WHERE BatchID = '" + text2 + "' AND SysDocID ='" + text3 + "'";
					object obj = ExecuteScalar(exp, sqlTransaction);
					if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
					{
						throw new CompanyException("Document number already exist.", 1046);
					}
				}
				if (isUpdate)
				{
					flag &= DeleteOpeningBalanceBatchDetailsRows(text3, text2, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(88, text3, text2, isDeletingTransaction: false, sqlTransaction);
				}
				OpeningBalanceBatchTypes openingBalanceBatchTypes = (OpeningBalanceBatchTypes)int.Parse(dataRow["BatchType"].ToString());
				string value = (openingBalanceBatchTypes == OpeningBalanceBatchTypes.Inventory) ? new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text4, sqlTransaction).ToString() : string.Empty;
				insertUpdateOpeningBalanceBatchCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(openingBalanceBatchData, "Opening_Balance_Batch", insertUpdateOpeningBalanceBatchCommand)) : (flag & Insert(openingBalanceBatchData, "Opening_Balance_Batch", insertUpdateOpeningBalanceBatchCommand)));
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				decimal num = default(decimal);
				switch (openingBalanceBatchTypes)
				{
				case OpeningBalanceBatchTypes.Customer:
				{
					decimal num4 = default(decimal);
					Dictionary<string, decimal> dictionary2 = new Dictionary<string, decimal>();
					ArrayList arrayList3 = new ArrayList();
					foreach (DataRow row in openingBalanceBatchData.OpeningBalanceBatchDetailsTable.Rows)
					{
						string text14 = row["AccountID"].ToString();
						decimal d2 = decimal.Parse(row["BalanceAmount"].ToString());
						if (dictionary2.ContainsKey(text14))
						{
							num = decimal.Parse(dictionary2[text14].ToString());
							num = (dictionary2[text14] = num + Math.Round(d2, currencyDecimalPoints));
						}
						else
						{
							dictionary2.Add(text14, Math.Round(d2, currencyDecimalPoints));
							arrayList3.Add(text14);
						}
					}
					string text15 = "";
					foreach (string item in arrayList3)
					{
						if (text15 != "")
						{
							text15 += ",";
						}
						text15 = text15 + "('" + item + "')";
					}
					text = "SELECT * FROM\r\n                                      (values " + text15 + ") as T(ID)\r\n                                    EXCEPT\r\n                                    SELECT CustomerID FROM Customer";
					DataSet dataSet3 = new DataSet();
					FillDataSet(dataSet3, "Customer", text, sqlTransaction);
					if (dataSet3 != null && dataSet3.Tables["Customer"].Rows.Count > 0)
					{
						string text16 = "";
						foreach (DataRow row2 in dataSet3.Tables[0].Rows)
						{
							if (text16 != "")
							{
								text16 += ",";
							}
							text16 = text16 + "'" + row2[0].ToString() + "'";
						}
						throw new CompanyException("One or more customer codes does not exist in customer list.\nCustomers:" + text16, 1055);
					}
					GLData gLData2 = new GLData();
					DataRow dataRow9 = gLData2.JournalTable.NewRow();
					dataRow9["JournalID"] = 0;
					dataRow9["JournalDate"] = dataRow["BatchDate"];
					dataRow9["SysDocID"] = dataRow["SysDocID"];
					dataRow9["VoucherID"] = dataRow["BatchID"];
					dataRow9["Reference"] = dataRow["Reference"];
					dataRow9["Reference2"] = "";
					dataRow9["SysDocType"] = (byte)202;
					if (dataRow["CurrencyID"] != null && dataRow["CurrencyID"].ToString() != baseCurrencyID)
					{
						dataRow9["CurrencyID"] = dataRow["CurrencyID"].ToString();
						dataRow9["CurrencyRate"] = dataRow["CurrencyRate"].ToString();
					}
					else
					{
						dataRow9["CurrencyID"] = DBNull.Value;
						dataRow9["CurrencyRate"] = 1;
					}
					dataRow9["Note"] = dataRow["Description"].ToString();
					dataRow9.EndEdit();
					gLData2.JournalTable.Rows.Add(dataRow9);
					num4 = default(decimal);
					DataRow dataRow10;
					for (int j = 0; j < arrayList3.Count; j++)
					{
						dataRow10 = gLData2.JournalDetailsTable.NewRow();
						dataRow10.BeginEdit();
						dataRow10["JournalID"] = 0;
						string text17 = arrayList3[j].ToString();
						num = dictionary2[text17];
						num4 += num;
						string text18 = (string)(dataRow10["PayeeType"] = "C");
						dataRow10["PayeeID"] = text17;
						text = "  SELECT  ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID FROM  Customer CUS \r\n                                             LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                          LEFT OUTER JOIN Location LOC ON Loc.LocationID  = '" + text4 + "' WHERE CUstomerID = '" + text17 + "'";
						string text19 = (string)(dataRow10["AccountID"] = (ExecuteScalar(text) as string));
						if (dataRow["CurrencyID"] != null && dataRow["CurrencyID"].ToString() != "" && dataRow["CurrencyID"].ToString() != baseCurrencyID)
						{
							if (num > 0m)
							{
								dataRow10["DebitFC"] = num;
								dataRow10["CreditFC"] = DBNull.Value;
							}
							else
							{
								dataRow10["CreditFC"] = Math.Abs(num);
								dataRow10["DebitFC"] = DBNull.Value;
							}
						}
						else
						{
							if (num > 0m)
							{
								dataRow10["Debit"] = num;
								dataRow10["Credit"] = DBNull.Value;
							}
							else
							{
								dataRow10["Credit"] = Math.Abs(num);
								dataRow10["Debit"] = DBNull.Value;
							}
							dataRow10["DebitFC"] = DBNull.Value;
							dataRow10["CreditFC"] = DBNull.Value;
						}
						dataRow10["Description"] = dataRow["Description"];
						dataRow10["Reference"] = dataRow["Reference"];
						dataRow10["CompanyID"] = dataRow["CompanyID"];
						dataRow10["DivisionID"] = dataRow["DivisionID"];
						dataRow10["RowIndex"] = 0;
						dataRow10.EndEdit();
						gLData2.JournalDetailsTable.Rows.Add(dataRow10);
					}
					dataRow10 = gLData2.JournalDetailsTable.NewRow();
					dataRow10.BeginEdit();
					dataRow10["JournalID"] = 0;
					dataRow10["PayeeType"] = "A";
					dataRow10["AccountID"] = dataRow["AccountID"];
					if (dataRow["CurrencyID"] != null && dataRow["CurrencyID"].ToString() != "" && dataRow["CurrencyID"].ToString() != baseCurrencyID)
					{
						if (num4 > 0m)
						{
							dataRow10["CreditFC"] = num4;
							dataRow10["DebitFC"] = DBNull.Value;
						}
						else
						{
							dataRow10["DebitFC"] = Math.Abs(num4);
							dataRow10["CreditFC"] = DBNull.Value;
						}
					}
					else
					{
						if (num4 > 0m)
						{
							dataRow10["Credit"] = num4;
							dataRow10["Debit"] = DBNull.Value;
						}
						else
						{
							dataRow10["Debit"] = Math.Abs(num4);
							dataRow10["Credit"] = DBNull.Value;
						}
						dataRow10["DebitFC"] = DBNull.Value;
						dataRow10["CreditFC"] = DBNull.Value;
					}
					dataRow10["Description"] = dataRow["Description"];
					dataRow10["Reference"] = dataRow["Reference"];
					dataRow10["CompanyID"] = dataRow["CompanyID"];
					dataRow10["DivisionID"] = dataRow["DivisionID"];
					dataRow10["RowIndex"] = 0;
					dataRow10.EndEdit();
					gLData2.JournalDetailsTable.Rows.Add(dataRow10);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(gLData2, isUpdate, sqlTransaction);
					flag &= InsertARJournal(openingBalanceBatchData, sqlTransaction);
					break;
				}
				case OpeningBalanceBatchTypes.Vendor:
				{
					decimal num6 = default(decimal);
					Dictionary<string, decimal> dictionary3 = new Dictionary<string, decimal>();
					ArrayList arrayList4 = new ArrayList();
					foreach (DataRow row3 in openingBalanceBatchData.OpeningBalanceBatchDetailsTable.Rows)
					{
						string text20 = row3["AccountID"].ToString();
						decimal d3 = decimal.Parse(row3["BalanceAmount"].ToString());
						if (dictionary3.ContainsKey(text20))
						{
							num = decimal.Parse(dictionary3[text20].ToString());
							num = (dictionary3[text20] = num + Math.Round(d3, currencyDecimalPoints));
						}
						else
						{
							dictionary3.Add(text20, Math.Round(d3, currencyDecimalPoints));
							arrayList4.Add(text20);
						}
					}
					string text21 = "";
					foreach (string item2 in arrayList4)
					{
						if (text21 != "")
						{
							text21 += ",";
						}
						text21 = text21 + "('" + item2 + "')";
					}
					text = "SELECT * FROM\r\n                                      (values " + text21 + ") as T(ID)\r\n                                    EXCEPT\r\n                                    SELECT VendorID FROM Vendor";
					DataSet dataSet4 = new DataSet();
					FillDataSet(dataSet4, "Vendor", text, sqlTransaction);
					if (dataSet4 != null && dataSet4.Tables["Vendor"].Rows.Count > 0)
					{
						string text22 = "";
						foreach (DataRow row4 in dataSet4.Tables[0].Rows)
						{
							if (text22 != "")
							{
								text22 += ",";
							}
							text22 = text22 + "'" + row4[0].ToString() + "'";
						}
						throw new CompanyException("One or more vendor codes does not exist in vendor list.\nVendors:" + text22, 1055);
					}
					GLData gLData3 = new GLData();
					DataRow dataRow12 = gLData3.JournalTable.NewRow();
					dataRow12["JournalID"] = 0;
					dataRow12["JournalDate"] = dataRow["BatchDate"];
					dataRow12["SysDocID"] = dataRow["SysDocID"];
					dataRow12["SysDocType"] = (byte)203;
					dataRow12["VoucherID"] = dataRow["BatchID"];
					dataRow12["Reference"] = dataRow["Reference"];
					dataRow12["Reference2"] = "";
					if (dataRow["CurrencyID"] != null && dataRow["CurrencyID"].ToString() != baseCurrencyID)
					{
						dataRow12["CurrencyID"] = dataRow["CurrencyID"].ToString();
						dataRow12["CurrencyRate"] = dataRow["CurrencyRate"].ToString();
					}
					else
					{
						dataRow12["CurrencyID"] = DBNull.Value;
						dataRow12["CurrencyRate"] = 1;
					}
					dataRow12["Note"] = dataRow["Description"].ToString();
					dataRow12.EndEdit();
					gLData3.JournalTable.Rows.Add(dataRow12);
					num6 = default(decimal);
					DataRow dataRow13;
					for (int k = 0; k < arrayList4.Count; k++)
					{
						try
						{
							dataRow13 = gLData3.JournalDetailsTable.NewRow();
							dataRow13.BeginEdit();
							dataRow13["JournalID"] = 0;
							string text23 = arrayList4[k].ToString();
							num = dictionary3[text23];
							num6 += num;
							string text24 = (string)(dataRow13["PayeeType"] = "V");
							dataRow13["PayeeID"] = text23;
							text = "  SELECT  ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID FROM  Vendor VEN \r\n                                           LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                          LEFT OUTER JOIN Location LOC ON Loc.LocationID  = '" + text4 + "' WHERE VendorID = '" + text23 + "'";
							string text25 = (string)(dataRow13["AccountID"] = (ExecuteScalar(text) as string));
							if (dataRow["CurrencyID"] != null && dataRow["CurrencyID"].ToString() != "" && dataRow["CurrencyID"].ToString() != baseCurrencyID)
							{
								if (num > 0m)
								{
									dataRow13["CreditFC"] = num;
									dataRow13["DebitFC"] = DBNull.Value;
								}
								else
								{
									dataRow13["DebitFC"] = Math.Abs(num);
									dataRow13["CreditFC"] = DBNull.Value;
								}
							}
							else
							{
								if (num > 0m)
								{
									dataRow13["Credit"] = num;
									dataRow13["Debit"] = DBNull.Value;
								}
								else
								{
									dataRow13["Debit"] = Math.Abs(num);
									dataRow13["Credit"] = DBNull.Value;
								}
								dataRow13["DebitFC"] = DBNull.Value;
								dataRow13["CreditFC"] = DBNull.Value;
							}
							dataRow13["Description"] = dataRow["Description"];
							dataRow13["Reference"] = dataRow["Reference"];
							dataRow13["CompanyID"] = dataRow["CompanyID"];
							dataRow13["DivisionID"] = dataRow["DivisionID"];
							dataRow13["RowIndex"] = 0;
							dataRow13.EndEdit();
							gLData3.JournalDetailsTable.Rows.Add(dataRow13);
						}
						catch (Exception)
						{
							throw;
						}
					}
					dataRow13 = gLData3.JournalDetailsTable.NewRow();
					dataRow13.BeginEdit();
					dataRow13["JournalID"] = 0;
					dataRow13["PayeeType"] = "A";
					dataRow13["AccountID"] = dataRow["AccountID"];
					if (dataRow["CurrencyID"] != null && dataRow["CurrencyID"].ToString() != "" && dataRow["CurrencyID"].ToString() != baseCurrencyID)
					{
						if (num6 > 0m)
						{
							dataRow13["DebitFC"] = num6;
							dataRow13["CreditFC"] = DBNull.Value;
						}
						else
						{
							dataRow13["CreditFC"] = Math.Abs(num6);
							dataRow13["DebitFC"] = DBNull.Value;
						}
					}
					else
					{
						if (num6 > 0m)
						{
							dataRow13["Debit"] = num6;
							dataRow13["Credit"] = DBNull.Value;
						}
						else
						{
							dataRow13["Credit"] = Math.Abs(num6);
							dataRow13["Debit"] = DBNull.Value;
						}
						dataRow13["DebitFC"] = DBNull.Value;
						dataRow13["CreditFC"] = DBNull.Value;
					}
					dataRow13["Description"] = dataRow["Description"];
					dataRow13["Reference"] = dataRow["Reference"];
					dataRow13["CompanyID"] = dataRow["CompanyID"];
					dataRow13["DivisionID"] = dataRow["DivisionID"];
					dataRow13["RowIndex"] = 0;
					dataRow13.EndEdit();
					gLData3.JournalDetailsTable.Rows.Add(dataRow13);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(gLData3, isUpdate, sqlTransaction);
					flag &= InsertAPJournal(openingBalanceBatchData, sqlTransaction);
					break;
				}
				case OpeningBalanceBatchTypes.Employee:
				{
					decimal num2 = default(decimal);
					Dictionary<string, decimal> dictionary = new Dictionary<string, decimal>();
					ArrayList arrayList2 = new ArrayList();
					foreach (DataRow row5 in openingBalanceBatchData.OpeningBalanceBatchDetailsTable.Rows)
					{
						string text8 = row5["AccountID"].ToString();
						decimal d = decimal.Parse(row5["BalanceAmount"].ToString());
						if (dictionary.ContainsKey(text8))
						{
							num = decimal.Parse(dictionary[text8].ToString());
							num = (dictionary[text8] = num + Math.Round(d, currencyDecimalPoints));
						}
						else
						{
							dictionary.Add(text8, Math.Round(d, currencyDecimalPoints));
							arrayList2.Add(text8);
						}
					}
					string text9 = "";
					foreach (string item3 in arrayList2)
					{
						if (text9 != "")
						{
							text9 += ",";
						}
						text9 = text9 + "('" + item3 + "')";
					}
					text = "SELECT * FROM\r\n                                      (values " + text9 + ") as T(ID)\r\n                                    EXCEPT\r\n                                    SELECT EmployeeID FROM Employee";
					DataSet dataSet2 = new DataSet();
					FillDataSet(dataSet2, "Employee", text, sqlTransaction);
					if (dataSet2 != null && dataSet2.Tables["Employee"].Rows.Count > 0)
					{
						string text10 = "";
						foreach (DataRow row6 in dataSet2.Tables[0].Rows)
						{
							if (text10 != "")
							{
								text10 += ",";
							}
							text10 = text10 + "'" + row6[0].ToString() + "'";
						}
						throw new CompanyException("One or more employee codes does not exist in employee list.\nEmployees:" + text10, 1055);
					}
					GLData gLData = new GLData();
					DataRow dataRow6 = gLData.JournalTable.NewRow();
					dataRow6["JournalID"] = 0;
					dataRow6["JournalDate"] = dataRow["BatchDate"];
					dataRow6["SysDocID"] = dataRow["SysDocID"];
					dataRow6["VoucherID"] = dataRow["BatchID"];
					dataRow6["Reference"] = dataRow["Reference"];
					dataRow6["Reference2"] = "";
					dataRow6["SysDocType"] = (byte)204;
					if (dataRow["CurrencyID"] != null && dataRow["CurrencyID"].ToString() != baseCurrencyID)
					{
						dataRow6["CurrencyID"] = dataRow["CurrencyID"].ToString();
						dataRow6["CurrencyRate"] = dataRow["CurrencyRate"].ToString();
					}
					else
					{
						dataRow6["CurrencyID"] = DBNull.Value;
						dataRow6["CurrencyRate"] = 1;
					}
					dataRow6["Note"] = dataRow["Description"].ToString();
					dataRow6.EndEdit();
					gLData.JournalTable.Rows.Add(dataRow6);
					num2 = default(decimal);
					DataRow dataRow7;
					for (int i = 0; i < arrayList2.Count; i++)
					{
						dataRow7 = gLData.JournalDetailsTable.NewRow();
						dataRow7.BeginEdit();
						dataRow7["JournalID"] = 0;
						string text11 = arrayList2[i].ToString();
						num = dictionary[text11];
						num2 += num;
						string text12 = (string)(dataRow7["PayeeType"] = "E");
						dataRow7["PayeeID"] = text11;
						text = "  SELECT ISNULL(EMP.AccountID,ISNULL(ET.AccountID, LOC.EmployeeAccountID)) AS EmpAccountID FROM  Employee EMP \r\n                                             LEFT OUTER JOIN Employee_Type ET ON EMP.ContractType = ET.TypeID\r\n                                          LEFT OUTER JOIN Location LOC ON Loc.LocationID  = '" + text4 + "' WHERE EmployeeID = '" + text11 + "'";
						string text13 = (string)(dataRow7["AccountID"] = (ExecuteScalar(text) as string));
						if (dataRow["CurrencyID"] != null && dataRow["CurrencyID"].ToString() != "" && dataRow["CurrencyID"].ToString() != baseCurrencyID)
						{
							if (num > 0m)
							{
								dataRow7["CreditFC"] = num;
								dataRow7["DebitFC"] = DBNull.Value;
							}
							else
							{
								dataRow7["DebitFC"] = Math.Abs(num);
								dataRow7["CreditFC"] = DBNull.Value;
							}
						}
						else
						{
							if (num > 0m)
							{
								dataRow7["Credit"] = num;
								dataRow7["Debit"] = DBNull.Value;
							}
							else
							{
								dataRow7["Debit"] = Math.Abs(num);
								dataRow7["Credit"] = DBNull.Value;
							}
							dataRow7["DebitFC"] = DBNull.Value;
							dataRow7["CreditFC"] = DBNull.Value;
						}
						dataRow7["Description"] = dataRow["Description"];
						dataRow7["Reference"] = dataRow["Reference"];
						dataRow7["CompanyID"] = dataRow["CompanyID"];
						dataRow7["DivisionID"] = dataRow["DivisionID"];
						dataRow7["RowIndex"] = 0;
						dataRow7.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow7);
					}
					dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					dataRow7["PayeeType"] = "A";
					dataRow7["AccountID"] = dataRow["AccountID"];
					if (dataRow["CurrencyID"] != null && dataRow["CurrencyID"].ToString() != "" && dataRow["CurrencyID"].ToString() != baseCurrencyID)
					{
						if (num2 > 0m)
						{
							dataRow7["DebitFC"] = num2;
							dataRow7["CreditFC"] = DBNull.Value;
						}
						else
						{
							dataRow7["CreditFC"] = Math.Abs(num2);
							dataRow7["DebitFC"] = DBNull.Value;
						}
					}
					else
					{
						if (num2 > 0m)
						{
							dataRow7["Debit"] = num2;
							dataRow7["Credit"] = DBNull.Value;
						}
						else
						{
							dataRow7["Credit"] = Math.Abs(num2);
							dataRow7["Debit"] = DBNull.Value;
						}
						dataRow7["DebitFC"] = DBNull.Value;
						dataRow7["CreditFC"] = DBNull.Value;
					}
					dataRow7["Description"] = dataRow["Description"];
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7["CompanyID"] = dataRow["CompanyID"];
					dataRow7["DivisionID"] = dataRow["DivisionID"];
					dataRow7["RowIndex"] = 0;
					dataRow7.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow7);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(gLData, isUpdate, sqlTransaction);
					flag &= InsertEmployeeJournal(openingBalanceBatchData, sqlTransaction);
					break;
				}
				case OpeningBalanceBatchTypes.Inventory:
				{
					ArrayList arrayList = new ArrayList();
					foreach (DataRow row7 in openingBalanceBatchData.OpeningBalanceBatchDetailsTable.Rows)
					{
						string text5 = row7["ProductID"].ToString();
						if (!arrayList.Contains(text5))
						{
							arrayList.Add(text5);
						}
					}
					string text6 = "";
					foreach (string item4 in arrayList)
					{
						if (text6 != "")
						{
							text6 += ",";
						}
						text6 = text6 + "('" + item4 + "')";
					}
					text = "SELECT * FROM\r\n                                      (values " + text6 + ") as T(ID)\r\n                                    EXCEPT\r\n                                    SELECT ProductID FROM Product";
					DataSet dataSet = new DataSet();
					FillDataSet(dataSet, "Product", text, sqlTransaction);
					if (dataSet != null && dataSet.Tables["Product"].Rows.Count > 0)
					{
						string text7 = "";
						foreach (DataRow row8 in dataSet.Tables[0].Rows)
						{
							if (text7 != "")
							{
								text7 += ",";
							}
							text7 = text7 + "'" + row8[0].ToString() + "'";
						}
						throw new CompanyException("One or more item codes does not exist in item list.\nItems:" + text7, 1055);
					}
					GLData journalData = CreateInventoryOpeningBalanceBatchGLData(openingBalanceBatchData, sqlTransaction);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
					InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
					foreach (DataRow row9 in openingBalanceBatchData.OpeningBalanceBatchDetailsTable.Rows)
					{
						DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
						dataRow4.BeginEdit();
						dataRow4["SysDocID"] = dataRow["SysDocID"];
						dataRow4["VoucherID"] = dataRow["BatchID"];
						dataRow4["Description"] = dataRow["Description"];
						dataRow4["LocationID"] = dataRow["LocationID"];
						dataRow4["ProductID"] = row9["ProductID"];
						dataRow4["Quantity"] = row9["Quantity"];
						dataRow4["Reference"] = dataRow["Reference"];
						dataRow4["RowIndex"] = row9["RowIndex"];
						dataRow4["PayeeType"] = "A";
						dataRow4["PayeeID"] = dataRow["AccountID"];
						dataRow4["SysDocType"] = (byte)88;
						dataRow4["UnitPrice"] = row9["Cost"];
						dataRow4["Cost"] = row9["Cost"];
						dataRow4["TransactionDate"] = row9["PurchaseDate"];
						dataRow4["TransactionType"] = (byte)18;
						dataRow4["CompanyID"] = dataRow["CompanyID"];
						dataRow4["DivisionID"] = dataRow["DivisionID"];
						dataRow4.EndEdit();
						inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
						row9["AccountID"] = value;
					}
					inventoryTransactionData.Merge(openingBalanceBatchData.Tables["Product_Lot_Receiving_Detail"]);
					flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(inventoryTransactionData, isUpdate: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
					break;
				}
				}
				if (openingBalanceBatchData.Tables["Opening_Balance_Batch_Detail"].Rows.Count > 0)
				{
					insertUpdateOpeningBalanceBatchCommand = GetInsertUpdateOpeningBalanceBatchDetailsCommand(isUpdate: false);
					insertUpdateOpeningBalanceBatchCommand.Transaction = sqlTransaction;
					flag &= Insert(openingBalanceBatchData, "Opening_Balance_Batch_Detail", insertUpdateOpeningBalanceBatchCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Opening_Balance_Batch", "SysDocID", dataRow["SysDocID"].ToString(), "BatchID", dataRow["BatchID"].ToString(), sqlTransaction, !isUpdate);
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

		private bool InsertARJournal(OpeningBalanceBatchData openingBalanceBatchData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			ARJournalData aRJournalData = new ARJournalData();
			DataTable dataTable = aRJournalData.Tables["ARJournal"];
			string text = openingBalanceBatchData.OpeningBalanceBatchTable.Rows[0]["LocationID"].ToString();
			string text2 = openingBalanceBatchData.OpeningBalanceBatchTable.Rows[0]["CurrencyID"].ToString();
			decimal d = 1m;
			string a = "M";
			bool flag2 = false;
			if (text2 != new Currencies(base.DBConfig).GetBaseCurrencyID())
			{
				flag2 = true;
				d = decimal.Parse(openingBalanceBatchData.OpeningBalanceBatchTable.Rows[0]["CurrencyRate"].ToString());
				a = new Currencies(base.DBConfig).GetCurrencyRateType(text2);
			}
			DataRow dataRow = openingBalanceBatchData.OpeningBalanceBatchTable.Rows[0];
			Hashtable hashtable = new Hashtable();
			foreach (DataRow row in openingBalanceBatchData.OpeningBalanceBatchDetailsTable.Rows)
			{
				row["SysDocID"] = dataRow["SysDocID"];
				row["VoucherID"] = dataRow["BatchID"];
				string text3 = row["AccountID"].ToString();
				DateTime.Parse(row["TransactionDate"].ToString());
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["VoucherID"] = dataRow["BatchID"];
				dataRow3["CustomerID"] = row["AccountID"];
				dataRow3["Reference"] = row["Reference"];
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
				decimal num = decimal.Parse(row["BalanceAmount"].ToString());
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
				dataRow3["ARDate"] = row["TransactionDate"];
				dataRow3["ARDueDate"] = row["DueDate"];
				dataRow3.EndEdit();
				dataTable.Rows.Add(dataRow3);
			}
			return flag & new ARJournal(base.DBConfig).InsertJournal(aRJournalData, sqlTransaction);
		}

		private bool InsertAPJournal(OpeningBalanceBatchData openingBalanceBatchData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			APJournalData aPJournalData = new APJournalData();
			DataTable dataTable = aPJournalData.Tables["APJournal"];
			string text = openingBalanceBatchData.OpeningBalanceBatchTable.Rows[0]["LocationID"].ToString();
			string text2 = openingBalanceBatchData.OpeningBalanceBatchTable.Rows[0]["CurrencyID"].ToString();
			decimal d = 1m;
			string a = "M";
			bool flag2 = false;
			if (text2 != new Currencies(base.DBConfig).GetBaseCurrencyID())
			{
				flag2 = true;
				d = decimal.Parse(openingBalanceBatchData.OpeningBalanceBatchTable.Rows[0]["CurrencyRate"].ToString());
				a = new Currencies(base.DBConfig).GetCurrencyRateType(text2);
			}
			DataRow dataRow = openingBalanceBatchData.OpeningBalanceBatchTable.Rows[0];
			Hashtable hashtable = new Hashtable();
			foreach (DataRow row in openingBalanceBatchData.OpeningBalanceBatchDetailsTable.Rows)
			{
				row["SysDocID"] = dataRow["SysDocID"];
				row["VoucherID"] = dataRow["BatchID"];
				string text3 = row["AccountID"].ToString();
				DateTime.Parse(row["TransactionDate"].ToString());
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["VoucherID"] = dataRow["BatchID"];
				dataRow3["VendorID"] = row["AccountID"];
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
				decimal num = decimal.Parse(row["BalanceAmount"].ToString());
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
				dataRow3["APDueDate"] = row["DueDate"];
				dataRow3.EndEdit();
				dataTable.Rows.Add(dataRow3);
			}
			return flag & new APJournal(base.DBConfig).InsertJournal(aPJournalData, sqlTransaction);
		}

		private bool InsertEmployeeJournal(OpeningBalanceBatchData openingBalanceBatchData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			EmployeeJournalData employeeJournalData = new EmployeeJournalData();
			DataTable dataTable = employeeJournalData.Tables["Employee_Journal"];
			string text = openingBalanceBatchData.OpeningBalanceBatchTable.Rows[0]["LocationID"].ToString();
			string text2 = openingBalanceBatchData.OpeningBalanceBatchTable.Rows[0]["CurrencyID"].ToString();
			decimal d = 1m;
			string a = "M";
			bool flag2 = false;
			if (text2 != new Currencies(base.DBConfig).GetBaseCurrencyID())
			{
				flag2 = true;
				d = decimal.Parse(openingBalanceBatchData.OpeningBalanceBatchTable.Rows[0]["CurrencyRate"].ToString());
				a = new Currencies(base.DBConfig).GetCurrencyRateType(text2);
			}
			DataRow dataRow = openingBalanceBatchData.OpeningBalanceBatchTable.Rows[0];
			Hashtable hashtable = new Hashtable();
			foreach (DataRow row in openingBalanceBatchData.OpeningBalanceBatchDetailsTable.Rows)
			{
				row["SysDocID"] = dataRow["SysDocID"];
				row["VoucherID"] = dataRow["BatchID"];
				string text3 = row["AccountID"].ToString();
				DateTime.Parse(row["TransactionDate"].ToString());
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["VoucherID"] = dataRow["BatchID"];
				dataRow3["EmployeeID"] = row["AccountID"];
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
				decimal num = decimal.Parse(row["BalanceAmount"].ToString());
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
				dataRow3["JournalDate"] = row["TransactionDate"];
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
			string textCommand = "SELECT MAX(BatchID) AS LastNumber FROM Opening_Balance_Batch WHERE SysDocID='" + sysDocID + "'";
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

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string SysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "";
			text3 = ((!(SysDocID == "SYS_013")) ? ("select distinct t.[BatchID] as [Batch NUmber],t.SysDocID,t.BatchDate,t.Description as Note,\r\n                          STUFF((SELECT distinct ', ' + t1.AccountID\r\n                                 from Opening_Balance_Batch_Detail t1\r\n                                 where t.BatchID = t1.BatchID and t1.SysDocID=t.SysDocID\r\n                                    FOR XML PATH(''), TYPE\r\n                                    ).value('.', 'NVARCHAR(MAX)') \r\n                                ,1,2,'') Account, t.Reference, t.LocationID AS Location\r\n\t\t\t\t\t\t\t\t\r\n                        from Opening_Balance_Batch t  where t.SysDocID='" + SysDocID + "' and t.BatchDate Between '" + text + "' AND '" + text2 + "'") : ("select distinct t.[BatchID] as [Batch NUmber],t.SysDocID,t.BatchDate,t.AccountID,t.Description AS Note,\r\n                          STUFF((SELECT distinct ', ' + t1.ProductID\r\n                                 from Opening_Balance_Batch_Detail t1\r\n                                 where t.BatchID = t1.BatchID and t1.SysDocID=t.SysDocID\r\n                                    FOR XML PATH(''), TYPE\r\n                                    ).value('.', 'NVARCHAR(MAX)') \r\n                                ,1,2,'') Product, t.Reference, t.LocationID AS Location\r\n\t\t\t\t\t\t\t\t\r\n                        from Opening_Balance_Batch t  where t.SysDocID='" + SysDocID + "' and t.BatchDate Between '" + text + "' AND '" + text2 + "'"));
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Opening_Balance_Batch", sqlCommand);
			return dataSet;
		}

		private GLData CreateInventoryOpeningBalanceBatchGLData(OpeningBalanceBatchData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.OpeningBalanceBatchTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string value = dataRow["AccountID"].ToString();
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
			foreach (DataRow row in transactionData.OpeningBalanceBatchDetailsTable.Rows)
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

		public OpeningBalanceBatchData GetOpeningBalanceBatchByID(string sysDocID, string voucherID)
		{
			try
			{
				OpeningBalanceBatchData openingBalanceBatchData = new OpeningBalanceBatchData();
				string textCommand = "SELECT * FROM Opening_Balance_Batch WHERE BatchID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(openingBalanceBatchData, "Opening_Balance_Batch", textCommand);
				if (openingBalanceBatchData == null || openingBalanceBatchData.Tables.Count == 0 || openingBalanceBatchData.Tables["Opening_Balance_Batch"].Rows.Count == 0)
				{
					return null;
				}
				OpeningBalanceBatchTypes openingBalanceBatchTypes = (OpeningBalanceBatchTypes)int.Parse(openingBalanceBatchData.Tables[0].Rows[0]["BatchType"].ToString());
				switch (openingBalanceBatchTypes)
				{
				case OpeningBalanceBatchTypes.Customer:
					textCommand = "SELECT TD.*,CUS.CustomerName AS CustomerName\r\n                        FROM Opening_Balance_Batch_Detail TD INNER JOIN Customer CUS ON Cus.CustomerID = TD.AccountID\r\n                        WHERE BatchID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
					break;
				case OpeningBalanceBatchTypes.Vendor:
					textCommand = "SELECT TD.*,VEN.VendorName AS VendorName\r\n                        FROM Opening_Balance_Batch_Detail TD INNER JOIN Vendor VEN ON VEN.VendorID = TD.AccountID\r\n                        WHERE BatchID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
					break;
				case OpeningBalanceBatchTypes.Inventory:
					textCommand = "SELECT TD.*, Cost * TD.Quantity AS Total,IsTrackLot,IsTrackSerial\r\n                        FROM Opening_Balance_Batch_Detail TD INNER JOIN Product P ON P.ProductID = TD.ProductID\r\n                        WHERE BatchID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
					break;
				case OpeningBalanceBatchTypes.Employee:
					textCommand = "SELECT TD.*,EMP.FirstName + ' ' + EMP.LastName AS Name\r\n                        FROM Opening_Balance_Batch_Detail TD INNER JOIN Employee EMP ON EMP.EmployeeID = TD.AccountID\r\n                        WHERE BatchID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
					break;
				}
				FillDataSet(openingBalanceBatchData, "Opening_Balance_Batch_Detail", textCommand);
				if (openingBalanceBatchTypes == OpeningBalanceBatchTypes.Inventory)
				{
					textCommand = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='SYS_013'";
					FillDataSet(openingBalanceBatchData, "Product_Lot_Receiving_Detail", textCommand);
				}
				return openingBalanceBatchData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteOpeningBalanceBatchDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Opening_Balance_Batch_Detail WHERE SysDocID = '" + sysDocID + "' AND BatchID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidOpeningBalanceBatch(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteOpeningBalanceBatch(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = " SELECT BatchType FROM Opening_Balance_Batch WHERE SysDocID = '" + sysDocID + "' AND BatchID = '" + voucherID + "' ";
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
				flag &= DeleteOpeningBalanceBatchDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Opening_Balance_Batch WHERE SysDocID = '" + sysDocID + "' AND BatchID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Opening Balance Batch", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetOpeningBalanceBatchToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT OP.*, CASE OP.BatchType WHEN 1 THEN 'Customer' WHEN 2 THEN 'Vendor' WHEN 3 THEN 'Employee' WHEN 4 THEN 'Inventory' END AS BatchTypeName, \r\n                                LOC.LocationName FROM Opening_Balance_Batch OP\r\n                                INNER JOIN Location LOC ON Loc.LocationID = OP.LocationID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND BatchID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Opening_Balance_Batch", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Opening_Balance_Batch"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT OPD.*,\r\n                                  CASE OP.BatchType WHEN 1 THEN (SELECT CustomerName FROM Customer WHERE CustomerID = OPD.AccountID)  \r\n                                    WHEN 2 THEN (SELECT VendorName FROM Vendor WHERE VendorID = OPD.AccountID) END AS Name \r\n                                    FROM Opening_Balance_Batch_Detail OPD\r\n\t\t\t\t\t\t\t\t\t  INNER JOIN Opening_Balance_Batch OP ON OPD.BatchID = OP.BatchID AND OPD.SysDocID = OP.SysDocID\r\n                        WHERE OPD.SysDocID='" + sysDocID + "' AND OPD.BatchID IN (" + text + ")\r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Opening_Balance_Batch_Detail", cmdText);
				dataSet.Relations.Add("OpeningBalanceDetail", new DataColumn[2]
				{
					dataSet.Tables["Opening_Balance_Batch"].Columns["SysDocID"],
					dataSet.Tables["Opening_Balance_Batch"].Columns["BatchID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Opening_Balance_Batch_Detail"].Columns["SysDocID"],
					dataSet.Tables["Opening_Balance_Batch_Detail"].Columns["BatchID"]
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
