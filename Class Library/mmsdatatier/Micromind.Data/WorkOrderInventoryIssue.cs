using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class WorkOrderInventoryIssue : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE_PARM2 = "@Reference2";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REQUESTEDBY_PARM = "@RequestedBy";

		private const string SOURCESYSDOCTYPE_PARM = "@SourceSysDocType";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string WORKORDERINVENTORYISSUE_TABLE = "EA_WorkOrder_Inventory_Issue";

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

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string WORKORDERINVENTORYISSUEDETAIL_TABLE = "EA_WorkOrder_Inventory_Issue_Detail";

		public WorkOrderInventoryIssue(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateWorkOrderInventoryIssueText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_WorkOrder_Inventory_Issue", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("RequestedBy", "@RequestedBy"), new FieldValue("SourceSysDocType", "@SourceSysDocType"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("EA_WorkOrder_Inventory_Issue", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateWorkOrderInventoryIssueCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateWorkOrderInventoryIssueText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateWorkOrderInventoryIssueText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocType", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@RequestedBy"].SourceColumn = "RequestedBy";
			parameters["@SourceSysDocType"].SourceColumn = "SourceSysDocType";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
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

		private string GetInsertUpdateWorkOrderInventoryIssueDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_WorkOrder_Inventory_Issue_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("UnitID", "@UnitID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("Cost", "@Cost"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("Quantity", "@Quantity"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateWorkOrderInventoryIssueDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateWorkOrderInventoryIssueDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateWorkOrderInventoryIssueDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
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
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(WorkOrderInventoryIssueData journalData)
		{
			return true;
		}

		public bool InsertUpdateWorkOrderInventoryIssue(WorkOrderInventoryIssueData WorkOrderInventoryIssueData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateWorkOrderInventoryIssueCommand = GetInsertUpdateWorkOrderInventoryIssueCommand(isUpdate);
			try
			{
				DataRow dataRow = WorkOrderInventoryIssueData.WorkOrderInventoryIssueTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("EA_WorkOrder_Inventory_Issue", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in WorkOrderInventoryIssueData.WorkOrderInventoryIssueDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteWorkOrderInventoryIssueDetailsRows(sysDocID, text, isDeletingTransaction: false, sqlTransaction);
				}
				string text2 = "";
				string text3 = "";
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row2 in WorkOrderInventoryIssueData.WorkOrderInventoryIssueDetailTable.Rows)
				{
					DataRow dataRow3 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["SysDocID"] = row2["SysDocID"];
					dataRow3["VoucherID"] = row2["VoucherID"];
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["LocationID"] = row2["LocationID"];
					dataRow3["EqWorkOrderID"] = row2["SourceVoucherID"];
					dataRow3["ProductID"] = row2["ProductID"];
					dataRow3["Quantity"] = -1m * decimal.Parse(row2["Quantity"].ToString());
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["RowIndex"] = row2["RowIndex"];
					dataRow3["PayeeType"] = "A";
					dataRow3["PayeeID"] = dataRow["AccountID"];
					dataRow3["SysDocType"] = (byte)229;
					dataRow3["UnitID"] = row2["UnitID"];
					dataRow3["UnitPrice"] = row2["Cost"];
					dataRow3["Cost"] = row2["Cost"];
					dataRow3["TransactionDate"] = dataRow["TransactionDate"];
					dataRow3["TransactionType"] = (byte)24;
					dataRow3.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow3);
					text2 = row2["SourceVoucherID"].ToString();
					text3 = row2["SourceSysDocID"].ToString();
					int result = 0;
					if (!(text2 == "") && !(text3 == ""))
					{
						int.TryParse(row2["SourceRowIndex"].ToString(), out result);
						float result2 = 0f;
						if (row2["UnitQuantity"] != DBNull.Value)
						{
							float.TryParse(row2["UnitQuantity"].ToString(), out result2);
						}
						else
						{
							float.TryParse(row2["Quantity"].ToString(), out result2);
						}
						if (flag)
						{
							flag &= new EquipmentWorkOrder(base.DBConfig).UpdateRowIssuedQuantity(text3, text2, result, result2, sqlTransaction);
						}
						if (text2 != "")
						{
							flag &= new EquipmentWorkOrder(base.DBConfig).CloseIssuedDoc(text3, text2, sqlTransaction);
						}
					}
				}
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				insertUpdateWorkOrderInventoryIssueCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(WorkOrderInventoryIssueData, "EA_WorkOrder_Inventory_Issue", insertUpdateWorkOrderInventoryIssueCommand)) : (flag & Insert(WorkOrderInventoryIssueData, "EA_WorkOrder_Inventory_Issue", insertUpdateWorkOrderInventoryIssueCommand)));
				insertUpdateWorkOrderInventoryIssueCommand = GetInsertUpdateWorkOrderInventoryIssueDetailsCommand(isUpdate: false);
				insertUpdateWorkOrderInventoryIssueCommand.Transaction = sqlTransaction;
				if (WorkOrderInventoryIssueData.Tables["EA_WorkOrder_Inventory_Issue_Detail"].Rows.Count > 0)
				{
					flag &= Insert(WorkOrderInventoryIssueData, "EA_WorkOrder_Inventory_Issue_Detail", insertUpdateWorkOrderInventoryIssueCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("EA_WorkOrder_Inventory_Issue", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "WorkOrder Inventory Issue";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "EA_WorkOrder_Inventory_Issue", "VoucherID", sqlTransaction);
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

		private GLData CreateWorkOrderInventoryIssueGLData(WorkOrderInventoryIssueData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.WorkOrderInventoryIssueTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.WorkOrderInventoryIssue;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Project Inventory Issue";
			dataRow2["Note"] = dataRow["Description"];
			string text = dataRow["SysDocID"].ToString();
			string text2 = new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", text, sqlTransaction).ToString();
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			new Hashtable();
			new ArrayList();
			new Hashtable();
			new ArrayList();
			string text3 = "";
			DataRow dataRow3 = null;
			foreach (DataRow row in transactionData.WorkOrderInventoryIssueDetailTable.Rows)
			{
				decimal num = default(decimal);
				decimal.Parse(row["Quantity"].ToString());
				string productID = row["ProductID"].ToString();
				string warehouseLocationID = row["LocationID"].ToString();
				string text4 = row["JobID"].ToString();
				string value = row["CostCategoryID"].ToString();
				string voucherID = row["VoucherID"].ToString();
				row["SysDocID"].ToString();
				int rowIndex = int.Parse(row["RowIndex"].ToString());
				DataSet productTransactionAccounts = new Products(base.DBConfig).GetProductTransactionAccounts(productID, text2, warehouseLocationID, text, sqlTransaction);
				if (productTransactionAccounts == null || productTransactionAccounts.Tables.Count == 0 || productTransactionAccounts.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("Product accounts information not found for product or location.");
				}
				text3 = productTransactionAccounts.Tables[0].Rows[0]["InventoryAssetAccountID"].ToString();
				string value2 = "";
				object jobAccountIDByLocation = new Job(base.DBConfig).GetJobAccountIDByLocation(text4, text2, JobAccounts.WIPAccount, sqlTransaction);
				if (jobAccountIDByLocation != null)
				{
					value2 = jobAccountIDByLocation.ToString();
				}
				if (string.IsNullOrEmpty(value2))
				{
					throw new CompanyException("Work In Progress (WIP) account not set for location:" + text2);
				}
				num = -1m * Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(productID, text, voucherID, rowIndex, sqlTransaction));
				num = Math.Abs(Math.Round(num, currencyDecimalPoints));
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["Debit"] = Math.Abs(num);
				dataRow3["Credit"] = DBNull.Value;
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = value2;
				dataRow3["JobID"] = text4;
				dataRow3["CostCategoryID"] = value;
				dataRow3["Description"] = dataRow["Description"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["JVEntryType"] = (byte)2;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["Debit"] = DBNull.Value;
				dataRow3["Credit"] = Math.Abs(num);
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text3;
				dataRow3["JobID"] = text4;
				dataRow3["CostCategoryID"] = value;
				dataRow3["Description"] = dataRow["Description"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["JVEntryType"] = (byte)1;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
			}
			return gLData;
		}

		public WorkOrderInventoryIssueData GetWorkOrderInventoryIssueByID(string sysDocID, string voucherID)
		{
			try
			{
				WorkOrderInventoryIssueData workOrderInventoryIssueData = new WorkOrderInventoryIssueData();
				string textCommand = "SELECT * FROM EA_WorkOrder_Inventory_Issue WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(workOrderInventoryIssueData, "EA_WorkOrder_Inventory_Issue", textCommand);
				if (workOrderInventoryIssueData == null || workOrderInventoryIssueData.Tables.Count == 0 || workOrderInventoryIssueData.Tables["EA_WorkOrder_Inventory_Issue"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,P.ItemType\r\n                        FROM EA_WorkOrder_Inventory_Issue_Detail TD INNER JOIN Product P ON P.ProductID = TD.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' Order By RowIndex";
				FillDataSet(workOrderInventoryIssueData, "EA_WorkOrder_Inventory_Issue_Detail", textCommand);
				return workOrderInventoryIssueData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteWorkOrderInventoryIssueDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				WorkOrderInventoryIssueData workOrderInventoryIssueData = new WorkOrderInventoryIssueData();
				string textCommand = "SELECT SOD.* FROM EA_WorkOrder_Inventory_Issue_Detail SOD INNER JOIN EA_WorkOrder_Inventory_Issue SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(workOrderInventoryIssueData, "EA_WorkOrder_Inventory_Issue_Detail", textCommand, sqlTransaction);
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				string text = "";
				string text2 = "";
				string text3 = "";
				foreach (DataRow row in workOrderInventoryIssueData.WorkOrderInventoryIssueDetailTable.Rows)
				{
					text3 = row["ProductID"].ToString();
					text = row["SourceVoucherID"].ToString();
					text2 = row["SourceSysDocID"].ToString();
					int result = 0;
					if (!(text == "") && !(text2 == ""))
					{
						int.TryParse(row["SourceRowIndex"].ToString(), out result);
						float result2 = 0f;
						if (row["UnitQuantity"] != DBNull.Value)
						{
							float.TryParse(row["UnitQuantity"].ToString(), out result2);
						}
						else
						{
							float.TryParse(row["Quantity"].ToString(), out result2);
						}
						_ = new Products(base.DBConfig).GetOrderedQuantity(text3, sqlTransaction) + result2;
						_ = 0f;
						if (flag)
						{
							flag &= new EquipmentWorkOrder(base.DBConfig).UpdateRowIssuedQuantity(text2, text, result, -1f * result2, sqlTransaction);
						}
						if (flag)
						{
							exp = "UPDATE EA_Work_Order SET Status=1 WHERE SysDocID='" + text2 + "' AND VoucherID='" + text + "' ";
							flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
						}
					}
				}
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(229, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
				textCommand = "DELETE FROM EA_WorkOrder_Inventory_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidWorkOrderInventoryIssue(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteWorkOrderInventoryIssue(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteWorkOrderInventoryIssueDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				text = "DELETE FROM EA_WorkOrder_Inventory_Issue WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("WorkOrder Inventory Issue", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetWorkOrderInventoryIssueToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT IA.*\r\n                                FROM EA_WorkOrder_Inventory_Issue IA\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "EA_WorkOrder_Inventory_Issue", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["EA_WorkOrder_Inventory_Issue"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT *, (SELECT SUM(Quantity * COST)  FROM EA_WorkOrder_Inventory_Issue_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")) AS TotalAmount,(SELECT  SUM(ISNULL(UnitQuantity,0)* Cost)  FROM EA_WorkOrder_Inventory_Issue_Detail \r\n                        WHERE  SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") ) AS UnitAmount FROM EA_WorkOrder_Inventory_Issue_Detail\r\n                       WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")                      \r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "EA_WorkOrder_Inventory_Issue_Detail", cmdText);
				dataSet.Relations.Add("WorkOrderIssueDetail", new DataColumn[2]
				{
					dataSet.Tables["EA_WorkOrder_Inventory_Issue"].Columns["SysDocID"],
					dataSet.Tables["EA_WorkOrder_Inventory_Issue"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["EA_WorkOrder_Inventory_Issue_Detail"].Columns["SysDocID"],
					dataSet.Tables["EA_WorkOrder_Inventory_Issue_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetWorkOrderInventoryIssueList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT DISTINCT JI.VoucherID [VoucherID],JI.SysDocID [Doc ID],JI.TransactionDate AS [Date]  , JI.Description , JI.Reference AS WorkOrder\r\n                            FROM EA_WorkOrder_Inventory_Issue JI \r\n                            WHERE  JI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY JI.TransactionDate, JI.VoucherID ";
			FillDataSet(dataSet, "EA_WorkOrder_Inventory_Issue", str);
			return dataSet;
		}
	}
}
