using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class WorkOrder : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string DESCRIPTION_PARM = "@Description";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string NOTE_PARM = "@Note";

		private const string STATUS_PARM = "@Status";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string BOMPRODUCTID_PARM = "@BOMProductID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string ROUTEGROUPID_PARM = "@RouteGroupID";

		private const string BOMID_PARM = "@BOMID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string REMARKS_PARM = "@Remarks";

		private const string UNITID_PARM = "@UnitID";

		public const string EXPENSEID_PARM = "@ExpenseID";

		public const string AMOUNT_PARM = "@Amount";

		public WorkOrder(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateWorkOrderText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Mfg_Work_Order", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("Description", "@Description"), new FieldValue("Status", "@Status"), new FieldValue("Note", "@Note"), new FieldValue("LocationID", "@LocationID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Mfg_Work_Order", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateWorkOrderCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateWorkOrderText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateWorkOrderText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Note"].SourceColumn = "Note";
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

		private string GetInsertUpdateWorkOrderDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Mfg_Work_Order_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@BOMProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitID", "@UnitID"), new FieldValue("RouteGroupID", "@RouteGroupID"), new FieldValue("BOMID", "@BOMID"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateWorkOrderExpenseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Mfg_Work_Order_Expense", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("Reference", "@Reference"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateWorkOrderDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateWorkOrderDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateWorkOrderDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@BOMProductID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@RouteGroupID", SqlDbType.NVarChar);
			parameters.Add("@BOMID", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@BOMProductID"].SourceColumn = "ProductID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@RouteGroupID"].SourceColumn = "RouteGroupID";
			parameters["@BOMID"].SourceColumn = "BOMID";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdateWorkOrderExpenseCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateWorkOrderExpenseText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateWorkOrderExpenseText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ExpenseID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ExpenseID"].SourceColumn = "ExpenseID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@Reference"].SourceColumn = "Reference";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(WorkOrderData journalData)
		{
			return true;
		}

		public bool InsertUpdateWorkOrder(WorkOrderData workOrderData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateWorkOrderCommand = GetInsertUpdateWorkOrderCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = workOrderData.WorkOrderTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Mfg_Work_Order", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in workOrderData.WorkOrderDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				foreach (DataRow row2 in workOrderData.WorkOrderExpenseTable.Rows)
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
					flag &= DeleteWorkOrderDetailsRows(sysDocID, text, isDeletingTransaction: false, sqlTransaction);
					flag &= DeleteWorkOrderExpenseRows(sysDocID, text, sqlTransaction);
					flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, text, sqlTransaction);
				}
				insertUpdateWorkOrderCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(workOrderData, "Mfg_Work_Order", insertUpdateWorkOrderCommand)) : (flag & Insert(workOrderData, "Mfg_Work_Order", insertUpdateWorkOrderCommand)));
				insertUpdateWorkOrderCommand = GetInsertUpdateWorkOrderDetailsCommand(isUpdate: false);
				insertUpdateWorkOrderCommand.Transaction = sqlTransaction;
				if (workOrderData.Tables["Mfg_Work_Order_Detail"].Rows.Count > 0)
				{
					flag &= Insert(workOrderData, "Mfg_Work_Order_Detail", insertUpdateWorkOrderCommand);
				}
				if (workOrderData.Tables["Mfg_Work_Order_Expense"].Rows.Count > 0)
				{
					insertUpdateWorkOrderCommand = GetInsertUpdateWorkOrderExpenseCommand(isUpdate: false);
					insertUpdateWorkOrderCommand.Transaction = sqlTransaction;
					flag &= Insert(workOrderData, "Mfg_Work_Order_Expense", insertUpdateWorkOrderCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Mfg_Work_Order", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Work Order";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Mfg_Work_Order", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.WorkOrder, sysDocID, text, "Mfg_Work_Order", sqlTransaction);
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

		private GLData CreateWorkOrderGLData(WorkOrderData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.WorkOrderTable.Rows[0];
			string text = "";
			string text2 = "";
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string text3 = dataRow["LocationID"].ToString();
			string text4 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text3, sqlTransaction).ToString();
			string text5 = "";
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.WorkOrder;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Work Order";
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			decimal num = default(decimal);
			decimal d = default(decimal);
			foreach (DataRow row in transactionData.WorkOrderDetailTable.Rows)
			{
				decimal num2 = default(decimal);
				decimal num3 = decimal.Parse(row["Quantity"].ToString());
				string productID = row["ProductID"].ToString();
				text2 = row["VoucherID"].ToString();
				text = row["SysDocID"].ToString();
				int rowIndex = int.Parse(row["RowIndex"].ToString());
				text4 = new Products(base.DBConfig).GetProductAccountIDByLocation(productID, text3, Products.ProductAccounts.AssetAccount, sqlTransaction);
				if (num3 > 0m)
				{
					num2 += num3;
				}
				else
				{
					num2 += -1m * Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(productID, text, text2, rowIndex, sqlTransaction));
				}
				d += num2;
				if (hashtable.ContainsKey(text4))
				{
					num = decimal.Parse(hashtable[text4].ToString());
					num += Math.Round(num2, currencyDecimalPoints);
					hashtable[text4] = num;
				}
				else
				{
					hashtable.Add(text4, Math.Round(num2, currencyDecimalPoints));
					arrayList.Add(text4);
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
					text4 = arrayList[i].ToString();
					num = decimal.Parse(hashtable[text4].ToString());
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
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = text4;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			decimal d2 = default(decimal);
			foreach (DataRow row2 in transactionData.WorkOrderExpenseTable.Rows)
			{
				string text6 = row2["ExpenseID"].ToString();
				num = decimal.Parse(row2["Amount"].ToString());
				text5 = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text6, sqlTransaction);
				DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["IsBaseOnly"] = true;
				dataRow4["AccountID"] = text5;
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
				dataRow4["Reference"] = text6;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
				d2 += num;
			}
			_ = d + d2;
			return gLData;
		}

		public WorkOrderData GetWorkOrderByID(string sysDocID, string voucherID)
		{
			try
			{
				WorkOrderData workOrderData = new WorkOrderData();
				string textCommand = "SELECT * FROM Mfg_Work_Order WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(workOrderData, "Mfg_Work_Order", textCommand);
				if (workOrderData == null || workOrderData.Tables.Count == 0 || workOrderData.Tables["Mfg_Work_Order"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM Mfg_Work_Order_Detail TD WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(workOrderData, "Mfg_Work_Order_Detail", textCommand);
				textCommand = "SELECT TE.*\r\n                        FROM Mfg_Work_Order_Expense TE\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(workOrderData, "Mfg_Work_Order_Expense", textCommand);
				return workOrderData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteWorkOrderDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(78, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
				string commandText = "DELETE FROM Mfg_Work_Order_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteWorkOrderExpenseRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				string commandText = "DELETE FROM Mfg_Work_Order_Expense WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidWorkOrder(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteWorkOrder(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteWorkOrderDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				text = "DELETE FROM Mfg_Work_Order WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Work Order", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetWorkOrderToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT IA.SysDocID,IA.VoucherID,IA.TransactionDate [Date],IA.RequiredDate,IA.Reference,\r\n                                Case  When Status=0 Then 'Estimate'  When Status=1 Then 'Open'  When Status=2 Then 'In Progress'  When Status=3 Then 'Hold' \r\n                                       When Status=4 Then 'Cancelled'  When Status=5 Then 'Completed' else '' END As [Status],IA.Note,IA.Description,IA.DateCreated,IA.DateUpdated,IA.CreatedBy,IA.UpdatedBy,LOC.LocationName [Location] FROM Mfg_Work_Order IA\r\n                                      INNER JOIN Location LOC ON Loc.LocationID = IA.LocationID\r\n                                      WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Mfg_Work_Order", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Mfg_Work_Order"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT WOD.SysDocID,WOD.VoucherID,ProductID [ProductID],Description,Quantity,UnitID,BOMName [BOM],RG.RouteGroupName [Route Group],Remarks FROM Mfg_Work_Order_Detail WOD\r\n                        LEFT JOIN BOM ON BOM.BOMID=WOD.BOMID\r\n                        LEFT JOIN Route_Group RG ON RG.RouteGroupID=WOD.RouteGroupID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Mfg_Work_Order_Detail", cmdText);
				cmdText = "SELECT E.* \r\n                        FROM Mfg_Work_Order_Expense E\r\n                            \r\n                       WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                       ORDER BY RowIndex";
				FillDataSet(dataSet, "Mfg_Work_Order_Expense", cmdText);
				dataSet.Relations.Add("WorkOrder", new DataColumn[2]
				{
					dataSet.Tables["Mfg_Work_Order"].Columns["SysDocID"],
					dataSet.Tables["Mfg_Work_Order"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Mfg_Work_Order_Detail"].Columns["SysDocID"],
					dataSet.Tables["Mfg_Work_Order_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("WorkOrderExpense", new DataColumn[2]
				{
					dataSet.Tables["Mfg_Work_Order"].Columns["SysDocID"],
					dataSet.Tables["Mfg_Work_Order"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Mfg_Work_Order_Expense"].Columns["SysDocID"],
					dataSet.Tables["Mfg_Work_Order_Expense"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetWorkOrderAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "Select DISTINCT JWO.SysDocID [Doc ID], JWO.VoucherID [Number], JWO.TransactionDate AS [Date], JWO.AssemblyProductID AS [AssemblyProduct],JWO.QuantityBuilt,JWO.RequiredQuantity FROM Job_Work_Order JWO";
				FillDataSet(dataSet, "Mfg_Work_Order", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet getDetails(string tableName, string DocID, string Value)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = null;
				text = "SELECT * FROM " + tableName + " WHERE VOUCHERID='" + Value + "'";
				FillDataSet(dataSet, "Reference", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet getReference(string Table, string ColumnName, string SysdocId, string VoucherNo)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = null;
				text = "SELECT " + ColumnName + " FROM " + Table + " WHERE VOUCHERID='" + VoucherNo + "'";
				FillDataSet(dataSet, "Reference", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetWorkOrderList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID,VoucherID,TransactionDate AS [Date],RequiredDate [Required Date],Reference,Note\r\n                            FROM   Mfg_Work_Order WO\r\n                            WHERE  WO.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY WO.TransactionDate, WO.VoucherID ";
			FillDataSet(dataSet, "Mfg_Work_Order", str);
			return dataSet;
		}
	}
}
