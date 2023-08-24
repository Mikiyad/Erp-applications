using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class InventoryAdjustment : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string ADJUSTMENTTYPEID_PARM = "@AdjustmentTypeID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string INVENTORYADJUSTMENT_TABLE = "Inventory_Adjustment";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string COST_PARM = "@Cost";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string FACTOR_PARM = "@Factor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string LISTVOUCHERID_PARM = "@ListVoucherID";

		private const string LISTSYSDOCID_PARM = "@ListSysDocID";

		private const string LISTROWINDEX_PARM = "@ListRowIndex";

		private const string REMARKS_PARM = "@Remarks";

		private const string INVENTORYADJUSTMENTDETAIL_TABLE = "Inventory_Adjustment_Detail";

		public InventoryAdjustment(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateInventoryAdjustmentText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Adjustment", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("Reference", "@Reference"), new FieldValue("AccountID", "@AccountID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("AdjustmentTypeID", "@AdjustmentTypeID"), new FieldValue("LocationID", "@LocationID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Inventory_Adjustment", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInventoryAdjustmentCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInventoryAdjustmentText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInventoryAdjustmentText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@AdjustmentTypeID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@AdjustmentTypeID"].SourceColumn = "AdjustmentTypeID";
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

		private string GetInsertUpdateInventoryAdjustmentDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Adjustment_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("Cost", "@Cost"), new FieldValue("UnitID", "@UnitID"), new FieldValue("ListVoucherID", "@ListVoucherID"), new FieldValue("ListSysDocID", "@ListSysDocID"), new FieldValue("ListRowIndex", "@ListRowIndex"), new FieldValue("Quantity", "@Quantity"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInventoryAdjustmentDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInventoryAdjustmentDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInventoryAdjustmentDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Cost", SqlDbType.Money);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@Factor", SqlDbType.Real);
			parameters.Add("@FactorType", SqlDbType.Char);
			parameters.Add("@ListSysDocID", SqlDbType.NVarChar);
			parameters.Add("@ListVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ListRowIndex", SqlDbType.Int);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@Factor"].SourceColumn = "Factor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@ListSysDocID"].SourceColumn = "ListSysDocID";
			parameters["@ListVoucherID"].SourceColumn = "ListVoucherID";
			parameters["@ListRowIndex"].SourceColumn = "ListRowIndex";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(InventoryAdjustmentData journalData)
		{
			return true;
		}

		public bool InsertUpdateInventoryAdjustment(InventoryAdjustmentData inventoryAdjustmentData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateInventoryAdjustmentCommand = GetInsertUpdateInventoryAdjustmentCommand(isUpdate);
			try
			{
				DataRow dataRow = inventoryAdjustmentData.InventoryAdjustmentTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Inventory_Adjustment", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string idFieldValue = dataRow["AdjustmentTypeID"].ToString();
				string text3 = (string)(dataRow["AccountID"] = new Databases(base.DBConfig).GetFieldValue("Adjustment_Type", "AccountID", "TypeID", idFieldValue, sqlTransaction).ToString());
				foreach (DataRow row in inventoryAdjustmentData.InventoryAdjustmentDetailsTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteInventoryAdjustmentDetailsRows(sysDocID, text, isDeletingTransaction: false, sqlTransaction);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row2 in inventoryAdjustmentData.InventoryAdjustmentDetailsTable.Rows)
				{
					DataRow dataRow3 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["SysDocID"] = row2["SysDocID"];
					dataRow3["VoucherID"] = row2["VoucherID"];
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["LocationID"] = dataRow["LocationID"];
					dataRow3["ProductID"] = row2["ProductID"];
					dataRow3["Quantity"] = row2["Quantity"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["RowIndex"] = row2["RowIndex"];
					dataRow3["PayeeType"] = "A";
					dataRow3["PayeeID"] = dataRow["AccountID"];
					dataRow3["SysDocType"] = (byte)18;
					dataRow3["UnitPrice"] = row2["Cost"];
					dataRow3["Cost"] = row2["Cost"];
					dataRow3["TransactionDate"] = dataRow["TransactionDate"];
					dataRow3["TransactionType"] = (byte)6;
					dataRow3["DivisionID"] = dataRow["DivisionID"];
					dataRow3["CompanyID"] = dataRow["CompanyID"];
					dataRow3.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow3);
				}
				inventoryTransactionData.Merge(inventoryAdjustmentData.Tables["Product_Lot_Issue_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(inventoryAdjustmentData, isUpdate: false, sqlTransaction);
				inventoryTransactionData.Merge(inventoryAdjustmentData.Tables["Product_Lot_Receiving_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(inventoryAdjustmentData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				insertUpdateInventoryAdjustmentCommand.Transaction = sqlTransaction;
				if (!isUpdate)
				{
					if (inventoryAdjustmentData.Tables["Inventory_Adjustment"].Rows.Count > 0)
					{
						flag &= Insert(inventoryAdjustmentData, "Inventory_Adjustment", insertUpdateInventoryAdjustmentCommand);
					}
				}
				else
				{
					flag &= Update(inventoryAdjustmentData, "Inventory_Adjustment", insertUpdateInventoryAdjustmentCommand);
				}
				insertUpdateInventoryAdjustmentCommand = GetInsertUpdateInventoryAdjustmentDetailsCommand(isUpdate: false);
				insertUpdateInventoryAdjustmentCommand.Transaction = sqlTransaction;
				if (inventoryAdjustmentData.Tables["Inventory_Adjustment_Detail"].Rows.Count > 0)
				{
					flag &= Insert(inventoryAdjustmentData, "Inventory_Adjustment_Detail", insertUpdateInventoryAdjustmentCommand);
				}
				flag &= new InventoryTransaction(base.DBConfig).AllocateUnallocatedItemsToLot(sysDocID, text, sqlTransaction);
				GLData journalData = CreateInventoryAdjustmentGLData(inventoryAdjustmentData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateInventoryTransactionRowID(sysDocID, text, sqlTransaction);
				DateTime.Parse(inventoryAdjustmentData.InventoryAdjustmentTable.Rows[0]["TransactionDate"].ToString());
				foreach (DataRow row3 in inventoryAdjustmentData.InventoryAdjustmentDetailsTable.Rows)
				{
					decimal d = decimal.Parse(row3["Quantity"].ToString());
					row3["ProductID"].ToString();
					_ = (d <= 0m);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Inventory_Adjustment", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Inventory Adjustment";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Inventory_Adjustment", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.InventoryAdjustment, sysDocID, text, "Inventory_Adjustment", sqlTransaction);
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

		private bool UpdateInventoryTransactionRowID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Inventory_Adjustment_Detail SID  \r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateInventoryAdjustmentGLData(InventoryAdjustmentData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.InventoryAdjustmentTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string idFieldValue = dataRow["AdjustmentTypeID"].ToString();
			string str = new Databases(base.DBConfig).GetFieldValue("Adjustment_Type", "TypeName", "TypeID", idFieldValue, sqlTransaction).ToString();
			string value = new Databases(base.DBConfig).GetFieldValue("Adjustment_Type", "AccountID", "TypeID", idFieldValue, sqlTransaction).ToString();
			string text = dataRow["LocationID"].ToString();
			string text2 = dataRow["SysDocID"].ToString();
			string text3 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text, sqlTransaction).ToString();
			string docLocationID = new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", text2, sqlTransaction).ToString();
			string value2 = dataRow["CompanyID"].ToString();
			string value3 = dataRow["DivisionID"].ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.InventoryAdjustment;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Inventory Adjustment - " + str;
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			decimal num = default(decimal);
			decimal d = default(decimal);
			foreach (DataRow row in transactionData.InventoryAdjustmentDetailsTable.Rows)
			{
				decimal d2 = default(decimal);
				decimal d3 = decimal.Parse(row["Quantity"].ToString());
				decimal d4 = decimal.Parse(row["Cost"].ToString());
				string productID = row["ProductID"].ToString();
				string voucherID = row["VoucherID"].ToString();
				int rowIndex = int.Parse(row["RowIndex"].ToString());
				DataSet productTransactionAccounts = new Products(base.DBConfig).GetProductTransactionAccounts(productID, docLocationID, text, text2, sqlTransaction);
				if (productTransactionAccounts == null || productTransactionAccounts.Tables.Count == 0 || productTransactionAccounts.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("Product accounts information not found for product or location.");
				}
				text3 = productTransactionAccounts.Tables[0].Rows[0]["InventoryAssetAccountID"].ToString();
				if (d3 > 0m)
				{
					d2 += d3 * d4;
				}
				else
				{
					d2 = -1m * Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(productID, text2, voucherID, rowIndex, sqlTransaction));
				}
				d += Math.Round(d2, currencyDecimalPoints);
				if (hashtable.ContainsKey(text3))
				{
					num = decimal.Parse(hashtable[text3].ToString());
					num += Math.Round(d2, currencyDecimalPoints);
					hashtable[text3] = num;
				}
				else
				{
					hashtable.Add(text3, Math.Round(d2, currencyDecimalPoints));
					arrayList.Add(text3);
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
					text3 = arrayList[i].ToString();
					num = decimal.Parse(hashtable[text3].ToString());
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
					dataRow3["AccountID"] = text3;
					dataRow3["JVEntryType"] = (byte)1;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["CompanyID"] = value2;
					dataRow3["DivisionID"] = value3;
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
			dataRow3["JVEntryType"] = (byte)1;
			dataRow3["CompanyID"] = value2;
			dataRow3["DivisionID"] = value3;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		public InventoryAdjustmentData GetInventoryAdjustmentByID(string sysDocID, string voucherID)
		{
			try
			{
				InventoryAdjustmentData inventoryAdjustmentData = new InventoryAdjustmentData();
				string textCommand = "SELECT * FROM Inventory_Adjustment WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(inventoryAdjustmentData, "Inventory_Adjustment", textCommand);
				if (inventoryAdjustmentData == null || inventoryAdjustmentData.Tables.Count == 0 || inventoryAdjustmentData.Tables["Inventory_Adjustment"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,CASE WHEN ItemType = 5 THEN 'True' ELSE IsTrackLot END  AS IsTrackLot,IsTrackSerial\r\n                        FROM Inventory_Adjustment_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(inventoryAdjustmentData, "Inventory_Adjustment_Detail", textCommand);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (inventoryAdjustmentData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					inventoryAdjustmentData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				inventoryAdjustmentData.Merge(transactionIssuesProductLots, preserveChanges: false);
				textCommand = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(inventoryAdjustmentData, "Product_Lot_Receiving_Detail", textCommand);
				return inventoryAdjustmentData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteInventoryAdjustmentDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(18, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
				string commandText = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Inventory_Adjustment_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidInventoryAdjustment(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteInventoryAdjustment(string sysDocID, string voucherID, bool isupdatecosting)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (new Products(base.DBConfig).HasInUseLots(sysDocID, voucherID))
				{
					throw new CompanyException("This transaction cannot be modifed because some of items are refered by other transactions.");
				}
				new InventoryAdjustmentData();
				GetInventoryAdjustmentByID(sysDocID, voucherID);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteInventoryAdjustmentDetailsRows(sysDocID, voucherID, isupdatecosting, sqlTransaction);
				text = "DELETE FROM Inventory_Adjustment WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Inventory Adjustment", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetInventoryAdjustmentToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT IA.*,LOC.LocationName,AT.TypeName FROM Inventory_Adjustment IA\r\n                                INNER JOIN  Adjustment_Type AT ON IA.AdjustmentTypeID = AT.TypeID\r\n                                INNER JOIN Location LOC ON Loc.LocationID = IA.LocationID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Inventory_Adjustment", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Inventory_Adjustment"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT *, SUM(Quantity * COST) AS Amount, SUM(ISNULL(UnitQuantity,0)* Cost) AS UnitAmount FROM Inventory_Adjustment_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                         GROUP BY SysDocID,VoucherID,ProductID,Description,UnitID,Quantity,Cost,Factor,UnitQuantity,FactorType,RowIndex, ListVoucherID, ListSysDocID, ListRowIndex, Remarks,ITRowID\r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Inventory_Adjustment_Detail", cmdText);
				dataSet.Relations.Add("AdjustmentDetail", new DataColumn[2]
				{
					dataSet.Tables["Inventory_Adjustment"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Adjustment"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Inventory_Adjustment_Detail"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Adjustment_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInventoryAdjustmentReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = " SELECT IA.SysDocID, IA.VoucherID, IA.TransactionDate, IA.LocationID, \r\n                                IA.Reference, IA.AdjustmentTypeID, IAD.ProductID, IAD.Description, \r\n                                IAD.Quantity, IAD.UnitID, IAD.RowIndex\r\n                                FROM Inventory_Adjustment IA INNER JOIN Inventory_Adjustment_Detail IAD ON IA.SysDocID = IAD.SysDocID AND                             \r\n                                IA.VoucherID = IAD.VoucherID \r\n                                INNER JOIN PRODUCT P ON IAD.ProductID=P.ProductID\r\n                                WHERE 1=1 ";
				text3 = text3 + " AND P.ItemType NOT IN ('3') AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromItem != "")
				{
					text3 = text3 + " AND IAD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND IAD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND IAD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND IAD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromWarehouse != "" || toWarehouse != "")
				{
					text3 = text3 + " AND IA.LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				text3 += " ORDER BY IAD.RowIndex";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromJob, string toJob, string fromCostCategory, string toCostCategory, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				if (fromWarehouse != "" || toWarehouse != "")
				{
					_ = " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				string text3 = "SELECT JIID.SysDocID, JIID.VoucherID, JIID.ProductID, JIID.JobID, JIID.LocationID, JIID.CostCategoryID, JIID.Description, JIID.Quantity, JIID.Cost, JIID.Quantity * JIID.Cost AS Amount, \r\n                        Product.CategoryID, JobName, CostCategoryName, TransactionDate, (SELECT DISTINCT SysDocType FROM Inventory_Transactions IT WHERE IT.SysDocID = JIID.SysDocID AND IT.VoucherID = JIID.VoucherID) AS SysDocType,\r\n                        PM.ManufacturerName,PS.StyleName,C.CountryName\r\n\t\t\t\t\t\tFROM Job_Inventory_Issue_Detail AS JIID \r\n\t\t\t\t\t\tINNER JOIN Job_Inventory_Issue JII ON JIID.SysDocID = JII.SysDocID AND JIID.VoucherID = JII.VoucherID \r\n\t\t\t\t\t\tINNER JOIN Product ON JIID.ProductID = Product.ProductID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Job J ON JIID.JobID = J.JobID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Job_Cost_Category JCC ON JIID.CostCategoryID = JCC.CostCategoryID\r\n                                    LEFT OUTER JOIN Product_Manufacturer PM On PM.ManufacturerId=Product.ManufacturerId\r\n\t\t\t\t\t\t            LEFT OUTER JOIN Product_Style PS On PS.StyleId=Product.StyleId\r\n\t\t\t\t\t\t            LEFT OUTER JOIN Country C On C.CountryId=Product.Origin\r\n                        WHERE JIID.JobID <> NULL OR JIID.JobID <> '' ";
				text3 = text3 + " AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					text3 = text3 + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					text3 = text3 + " AND JIID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND JIID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND JIID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND JIID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND JIID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND JIID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromJob != "")
				{
					text3 = text3 + " AND JIID.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "'";
				}
				if (fromCostCategory != "")
				{
					text3 = text3 + " AND JIID.CostCategoryID BETWEEN '" + fromCostCategory + "' AND '" + toCostCategory + "'";
				}
				text3 += " UNION ";
				text3 += "SELECT  JIRD.SysDocID, JIRD.VoucherID, JIRD.ProductID, JIRD.JobID, JIRD.LocationID, JIRD.CostCategoryID, JIRD.Description, (JIRD.Quantity ) * -1 , \r\n\t\t\t\t\t\t\t\tJIRD.Cost, (JIRD.Quantity * JIRD.Cost )*-1 AS Amount\r\n\t\t\t\t\t\t\t\t, Product.CategoryID, JobName, CostCategoryName, TransactionDate, (SELECT DISTINCT SysDocType FROM Inventory_Transactions IT WHERE IT.SysDocID = JIRD.SysDocID AND IT.VoucherID = JIRD.VoucherID) AS SysDocType ,\r\n                                    PM.ManufacturerName,PS.StyleName,C.CountryName   \r\n                        FROM Job_Inventory_Return_Detail AS JIRD \r\n\t\t\t\t\t\tINNER JOIN Job_Inventory_Return JIR ON JIRD.SysDocID = JIR.SysDocID AND JIRD.VoucherID = JIR.VoucherID   \r\n\t\t\t\t\t\tINNER JOIN Product ON JIRD.ProductID = Product.ProductID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Job J ON JIRD.JobID = J.JobID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Job_Cost_Category JCC ON JIRD.CostCategoryID = JCC.CostCategoryID\r\n                        LEFT OUTER JOIN Product_Manufacturer PM On PM.ManufacturerId=Product.ManufacturerId\r\n\t\t\t\t\t\t            LEFT OUTER JOIN Product_Style PS On PS.StyleId=Product.StyleId\r\n\t\t\t\t\t\t            LEFT OUTER JOIN Country C On C.CountryId=Product.Origin\r\n                        WHERE JIRD.JobID <> NULL OR JIRD.JobID <> '' ";
				text3 = text3 + " AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					text3 = text3 + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					text3 = text3 + " AND JIRD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND JIRD.ProductID IN (SELECT DISTINCT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND JIRD.ProductID IN (SELECT DISTINCT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND JIRD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND JIRD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND JIRD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromJob != "")
				{
					text3 = text3 + " AND JIRD.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "'";
				}
				if (fromCostCategory != "")
				{
					text3 = text3 + " AND JIRD.CostCategoryID BETWEEN '" + fromCostCategory + "' AND '" + toCostCategory + "'";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Inventory_Transactions", text3);
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
			string text3 = "SELECT 'False' AS V, SysDocID,VoucherID,TransactionDate,LocationID FROM Inventory_Adjustment ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Inventory_Adjustment", sqlCommand);
			return dataSet;
		}
	}
}
