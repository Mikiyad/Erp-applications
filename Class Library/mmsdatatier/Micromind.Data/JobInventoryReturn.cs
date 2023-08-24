using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class JobInventoryReturn : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REQUESTEDBY_PARM = "@RequestedBy";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string JOBINVENTORYRETURN_TABLE = "Job_Inventory_Return";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string COST_PARM = "@Cost";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string FACTOR_PARM = "@Factor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string JOBINVENTORYRETURNDETAIL_TABLE = "Job_Inventory_Return_Detail";

		public JobInventoryReturn(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateJobInventoryReturnText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Inventory_Return", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("RequestedBy", "@RequestedBy"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Inventory_Return", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobInventoryReturnCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobInventoryReturnText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobInventoryReturnText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@RequestedBy"].SourceColumn = "RequestedBy";
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

		private string GetInsertUpdateJobInventoryReturnDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Inventory_Return_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("Cost", "@Cost"), new FieldValue("Quantity", "@Quantity"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobInventoryReturnDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobInventoryReturnDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobInventoryReturnDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Cost", SqlDbType.Money);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@Factor", SqlDbType.Real);
			parameters.Add("@FactorType", SqlDbType.Char);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@Factor"].SourceColumn = "Factor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(JobInventoryReturnData journalData)
		{
			return true;
		}

		public bool InsertUpdateJobInventoryReturn(JobInventoryReturnData jobInventoryReturnData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateJobInventoryReturnCommand = GetInsertUpdateJobInventoryReturnCommand(isUpdate);
			try
			{
				DataRow dataRow = jobInventoryReturnData.JobInventoryReturnTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Job_Inventory_Return", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in jobInventoryReturnData.JobInventoryReturnDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteJobInventoryReturnDetailsRows(sysDocID, text, isDeletingTransaction: false, sqlTransaction);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row2 in jobInventoryReturnData.JobInventoryReturnDetailTable.Rows)
				{
					DataRow dataRow3 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["SysDocID"] = row2["SysDocID"];
					dataRow3["VoucherID"] = row2["VoucherID"];
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["LocationID"] = row2["LocationID"];
					dataRow3["JobID"] = row2["JobID"];
					dataRow3["CostCategoryID"] = row2["CostCategoryID"];
					dataRow3["ProductID"] = row2["ProductID"];
					dataRow3["Quantity"] = decimal.Parse(row2["Quantity"].ToString());
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["RowIndex"] = row2["RowIndex"];
					dataRow3["PayeeType"] = "A";
					dataRow3["PayeeID"] = dataRow["AccountID"];
					dataRow3["SysDocType"] = (byte)72;
					dataRow3["UnitPrice"] = row2["Cost"];
					dataRow3["Cost"] = row2["Cost"];
					dataRow3["TransactionDate"] = dataRow["TransactionDate"];
					dataRow3["TransactionType"] = (byte)16;
					dataRow3.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow3);
				}
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				insertUpdateJobInventoryReturnCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(jobInventoryReturnData, "Job_Inventory_Return", insertUpdateJobInventoryReturnCommand)) : (flag & Insert(jobInventoryReturnData, "Job_Inventory_Return", insertUpdateJobInventoryReturnCommand)));
				insertUpdateJobInventoryReturnCommand = GetInsertUpdateJobInventoryReturnDetailsCommand(isUpdate: false);
				insertUpdateJobInventoryReturnCommand.Transaction = sqlTransaction;
				if (jobInventoryReturnData.Tables["Job_Inventory_Return_Detail"].Rows.Count > 0)
				{
					flag &= Insert(jobInventoryReturnData, "Job_Inventory_Return_Detail", insertUpdateJobInventoryReturnCommand);
				}
				flag &= new InventoryTransaction(base.DBConfig).AllocateUnallocatedItemsToLot(sysDocID, text, sqlTransaction);
				GLData journalData = CreateJobInventoryReturnGLData(jobInventoryReturnData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Job_Inventory_Return", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Project Inventory Return";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Job_Inventory_Return", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.JobInventoryReturn, sysDocID, text, "Job_Inventory_Return", sqlTransaction);
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

		private GLData CreateJobInventoryReturnGLData(JobInventoryReturnData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.JobInventoryReturnTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.JobInventoryReturn;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Project Inventory Return";
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			new Hashtable();
			new ArrayList();
			decimal num = default(decimal);
			string text = "";
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			decimal num2 = default(decimal);
			foreach (DataRow row in transactionData.JobInventoryReturnDetailTable.Rows)
			{
				decimal num3 = default(decimal);
				decimal d = decimal.Parse(row["Quantity"].ToString());
				decimal d2 = decimal.Parse(row["Cost"].ToString());
				string productID = row["ProductID"].ToString();
				string warehouseLocationID = row["LocationID"].ToString();
				string text2 = row["JobID"].ToString();
				string value = row["CostCategoryID"].ToString();
				row["VoucherID"].ToString();
				row["SysDocID"].ToString();
				int.Parse(row["RowIndex"].ToString());
				string text3 = dataRow["SysDocID"].ToString();
				string text4 = new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", text3, sqlTransaction).ToString();
				DataSet productTransactionAccounts = new Products(base.DBConfig).GetProductTransactionAccounts(productID, text4, warehouseLocationID, text3, sqlTransaction);
				if (productTransactionAccounts == null || productTransactionAccounts.Tables.Count == 0 || productTransactionAccounts.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("Product accounts information not found for product or location.");
				}
				text = productTransactionAccounts.Tables[0].Rows[0]["InventoryAssetAccountID"].ToString();
				string value2 = "";
				object jobAccountIDByLocation = new Job(base.DBConfig).GetJobAccountIDByLocation(text2, text4, JobAccounts.WIPAccount, sqlTransaction);
				if (jobAccountIDByLocation != null)
				{
					value2 = jobAccountIDByLocation.ToString();
				}
				num3 += Math.Round(d * d2, currencyDecimalPoints);
				num2 += num3;
				if (hashtable.ContainsKey(text))
				{
					num = decimal.Parse(hashtable[text].ToString());
					num += Math.Round(num3, currencyDecimalPoints);
					hashtable[text] = num;
				}
				else
				{
					hashtable.Add(text, Math.Round(num3, currencyDecimalPoints));
					arrayList.Add(text);
				}
				if (num3 != 0m)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["Debit"] = DBNull.Value;
					dataRow3["Credit"] = Math.Abs(num3);
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = value2;
					dataRow3["JobID"] = text2;
					dataRow3["CostCategoryID"] = value;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["JVEntryType"] = (byte)2;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
				if (num3 != 0m)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["Debit"] = Math.Abs(num3);
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = text;
					dataRow3["JobID"] = text2;
					dataRow3["CostCategoryID"] = value;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["JVEntryType"] = (byte)1;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			return gLData;
		}

		public JobInventoryReturnData GetJobInventoryReturnByID(string sysDocID, string voucherID)
		{
			try
			{
				JobInventoryReturnData jobInventoryReturnData = new JobInventoryReturnData();
				string textCommand = "SELECT * FROM Job_Inventory_Return WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobInventoryReturnData, "Job_Inventory_Return", textCommand);
				if (jobInventoryReturnData == null || jobInventoryReturnData.Tables.Count == 0 || jobInventoryReturnData.Tables["Job_Inventory_Return"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,P.ItemType\r\n                        FROM Job_Inventory_Return_Detail TD INNER JOIN Product P ON P.ProductID = TD.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobInventoryReturnData, "Job_Inventory_Return_Detail", textCommand);
				return jobInventoryReturnData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobInventoryReturnDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(72, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
				string commandText = "DELETE FROM Job_Inventory_Return_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidJobInventoryReturn(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteJobInventoryReturn(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteJobInventoryReturnDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				text = "DELETE FROM Job_Inventory_Return WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Job Inventory Return", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetJobInventoryReturnToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT IA.* FROM Job_Inventory_Return IA\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Job_Inventory_Return", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Job_Inventory_Return"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT *, SUM(Quantity * COST) AS Amount, SUM(ISNULL(UnitQuantity,0)* Cost) AS UnitAmount FROM Job_Inventory_Return_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                        GROUP BY SysDocID,VoucherID,JobID, LocationID, CostCategoryID, BillType, IsBilled, BilledAmount, ProductID,Description,UnitID,Quantity,Cost,Factor,UnitQuantity,FactorType,RowIndex\r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Job_Inventory_Return_Detail", cmdText);
				dataSet.Relations.Add("JobReturnDetail", new DataColumn[2]
				{
					dataSet.Tables["Job_Inventory_Return"].Columns["SysDocID"],
					dataSet.Tables["Job_Inventory_Return"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Job_Inventory_Return_Detail"].Columns["SysDocID"],
					dataSet.Tables["Job_Inventory_Return_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobInventoryReturnList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT DISTINCT JI.SysDocID [Doc ID], JI.VoucherID [VoucherID], JID.JobID,J.JobName,JI.TransactionDate AS [Date]   \r\n                            FROM Job_Inventory_Return JI INNER JOIN Job_Inventory_Return_Detail JID ON JI.SysDocID=JID.SysDocID and JI.VoucherID=JID.VoucherID  LEFT JOIN Job J ON JID.JobID=J.JobID\r\n                            WHERE  JI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY JI.TransactionDate, JI.VoucherID ";
			FillDataSet(dataSet, "Job_Inventory_Return", str);
			return dataSet;
		}
	}
}
