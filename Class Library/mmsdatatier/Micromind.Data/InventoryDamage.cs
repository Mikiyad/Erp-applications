using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class InventoryDamage : StoreObject
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

		private const string ISVOID_PARM = "@IsVoid";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string INVENTORYDAMAGE_TABLE = "Inventory_Damage";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string COST_PARM = "@Cost";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string FACTOR_PARM = "@Factor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string REMARKS_PARM = "@Remarks";

		private const string INVENTORYDAMAGEDETAIL_TABLE = "Inventory_Damage_Detail";

		public InventoryDamage(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateInventoryDamageText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Damage", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("Reference", "@Reference"), new FieldValue("AccountID", "@AccountID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("AdjustmentTypeID", "@AdjustmentTypeID"), new FieldValue("IsVoid", "@IsVoid"), new FieldValue("LocationID", "@LocationID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Inventory_Damage", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInventoryDamageCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInventoryDamageText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInventoryDamageText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@IsVoid", SqlDbType.Bit);
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
			parameters["@IsVoid"].SourceColumn = "IsVoid";
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

		private string GetInsertUpdateInventoryDamageDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Damage_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("Cost", "@Cost"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitID", "@UnitID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInventoryDamageDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInventoryDamageDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInventoryDamageDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@Factor", SqlDbType.Real);
			parameters.Add("@FactorType", SqlDbType.Char);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@Factor"].SourceColumn = "Factor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(InventoryDamageData journalData)
		{
			return true;
		}

		public bool InsertUpdateInventoryDamage(InventoryDamageData inventoryAdjustmentData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateInventoryDamageCommand = GetInsertUpdateInventoryDamageCommand(isUpdate);
			try
			{
				DataRow dataRow = inventoryAdjustmentData.InventoryDamageTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				string checkFieldValue = dataRow["LocationID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Inventory_Damage", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string idFieldValue = dataRow["AdjustmentTypeID"].ToString();
				string text3 = (string)(dataRow["AccountID"] = new Databases(base.DBConfig).GetFieldValue("Adjustment_Type", "AccountID", "TypeID", idFieldValue, sqlTransaction).ToString());
				foreach (DataRow row in inventoryAdjustmentData.InventoryDamageDetailsTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text4 = row["ProductID"].ToString();
					decimal result = default(decimal);
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product_Location", "Quantity", "ProductID", text4, "LocationID", checkFieldValue, sqlTransaction);
					if (fieldValue != null)
					{
						decimal.TryParse(fieldValue.ToString(), out result);
					}
					float num = 0f;
					string text5 = "";
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text4, sqlTransaction);
					if (fieldValue != null)
					{
						text5 = fieldValue.ToString();
					}
					if (text5 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text5)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text4, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text4 + "\nUnit:" + row["UnitID"].ToString(), 1051);
						float num2 = float.Parse(obj["Factor"].ToString());
						string text6 = obj["FactorType"].ToString();
						num = float.Parse(row["Quantity"].ToString());
						row["Factor"] = num2;
						row["FactorType"] = text6;
						row["UnitQuantity"] = row["Quantity"];
						num = ((!(text6 == "M")) ? float.Parse(Math.Round(num * num2, 5).ToString()) : float.Parse(Math.Round(num / num2, 5).ToString()));
						row["Quantity"] = num;
					}
				}
				if (isUpdate)
				{
					flag &= DeleteInventoryDamageDetailsRows(sysDocID, text, isDeletingTransaction: false, sqlTransaction);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row2 in inventoryAdjustmentData.InventoryDamageDetailsTable.Rows)
				{
					DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = row2["SysDocID"];
					dataRow4["VoucherID"] = row2["VoucherID"];
					dataRow4["Description"] = dataRow["Description"];
					dataRow4["LocationID"] = dataRow["LocationID"];
					dataRow4["ProductID"] = row2["ProductID"];
					object obj4 = dataRow4["Quantity"] = (row2["Quantity"] = -1m * decimal.Parse(row2["Quantity"].ToString()));
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["RowIndex"] = row2["RowIndex"];
					dataRow4["PayeeType"] = "A";
					dataRow4["PayeeID"] = dataRow["AccountID"];
					dataRow4["SysDocType"] = (byte)87;
					dataRow4["UnitID"] = row2["UnitID"];
					dataRow4["TransactionDate"] = dataRow["TransactionDate"];
					dataRow4["TransactionType"] = (byte)6;
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
				}
				inventoryTransactionData.Merge(inventoryAdjustmentData.Tables["Product_Lot_Issue_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(inventoryAdjustmentData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				insertUpdateInventoryDamageCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(inventoryAdjustmentData, "Inventory_Damage", insertUpdateInventoryDamageCommand)) : (flag & Insert(inventoryAdjustmentData, "Inventory_Damage", insertUpdateInventoryDamageCommand)));
				insertUpdateInventoryDamageCommand = GetInsertUpdateInventoryDamageDetailsCommand(isUpdate: false);
				insertUpdateInventoryDamageCommand.Transaction = sqlTransaction;
				if (inventoryAdjustmentData.Tables["Inventory_Damage_Detail"].Rows.Count > 0)
				{
					flag &= Insert(inventoryAdjustmentData, "Inventory_Damage_Detail", insertUpdateInventoryDamageCommand);
				}
				GLData journalData = CreateInventoryDamageGLData(inventoryAdjustmentData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateInventoryTransactionRowID(sysDocID, text, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Inventory_Damage", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Inventory Damage";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Inventory_Damage", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.InventoryNoneSale, sysDocID, text, "Inventory_Damage", sqlTransaction);
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
				string exp = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Inventory_Damage_Detail SID  \r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateInventoryDamageGLData(InventoryDamageData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.InventoryDamageTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string idFieldValue = dataRow["AdjustmentTypeID"].ToString();
			string str = new Databases(base.DBConfig).GetFieldValue("Adjustment_Type", "TypeName", "TypeID", idFieldValue, sqlTransaction).ToString();
			string value = new Databases(base.DBConfig).GetFieldValue("Adjustment_Type", "AccountID", "TypeID", idFieldValue, sqlTransaction).ToString();
			string text = dataRow["LocationID"].ToString();
			string text2 = dataRow["SysDocID"].ToString();
			string value2 = dataRow["CompanyID"].ToString();
			string value3 = dataRow["DivisionID"].ToString();
			string text3 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text, sqlTransaction).ToString();
			string docLocationID = new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", text2, sqlTransaction).ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.InventoryNoneSale;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Inventory None Sale - " + str;
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			decimal num = default(decimal);
			decimal d = default(decimal);
			foreach (DataRow row in transactionData.InventoryDamageDetailsTable.Rows)
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
					d2 += -1m * Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(productID, text2, voucherID, rowIndex, sqlTransaction));
				}
				d2 = Math.Round(d2, currencyDecimalPoints);
				d += d2;
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
			dataRow3["JVEntryType"] = (byte)4;
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["CompanyID"] = value2;
			dataRow3["DivisionID"] = value3;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		public InventoryDamageData GetInventoryDamageByID(string sysDocID, string voucherID)
		{
			try
			{
				InventoryDamageData inventoryDamageData = new InventoryDamageData();
				string textCommand = "SELECT * FROM Inventory_Damage WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(inventoryDamageData, "Inventory_Damage", textCommand);
				if (inventoryDamageData == null || inventoryDamageData.Tables.Count == 0 || inventoryDamageData.Tables["Inventory_Damage"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*, CASE WHEN ItemType = 5 THEN 'True' ELSE ISNULL(IsTrackLot,'False') END AS IsTrackLot,ItemType, ISNULL(IsTrackSerial,'False') AS IsTrackSerial\r\n                        FROM Inventory_Damage_Detail TD INNER JOIN Product P ON P.ProductID = TD.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex";
				FillDataSet(inventoryDamageData, "Inventory_Damage_Detail", textCommand);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (inventoryDamageData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					inventoryDamageData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				inventoryDamageData.Merge(transactionIssuesProductLots, preserveChanges: false);
				return inventoryDamageData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteInventoryDamageDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(87, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
				string commandText = "DELETE FROM Inventory_Damage_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidInventoryDamage(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteInventoryDamage(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteInventoryDamageDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				text = "DELETE FROM Inventory_Damage WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Inventory Damage", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetInventoryDamageToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT IA.*,LOC.LocationName,AT.TypeName FROM Inventory_Damage IA\r\n                                INNER JOIN  Adjustment_Type AT ON IA.AdjustmentTypeID = AT.TypeID\r\n                                INNER JOIN Location LOC ON Loc.LocationID = IA.LocationID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Inventory_Damage", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Inventory_Damage"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT ID.*,IT.AverageCost, -1 * SUM(ID.Quantity * IT.AverageCost) AS Amount, -1 * SUM(ISNULL(ID.UnitQuantity, 0) * IT.AverageCost) AS UnitAmount\r\n                        FROM Inventory_Damage_Detail ID\r\n                        INNER JOIN Inventory_Transactions IT ON IT.SysDocID = ID.SysDocID AND IT.VoucherID = ID.VoucherID AND IT.ProductID = ID.ProductID\r\n                        WHERE ID.SysDocID = '" + sysDocID + "' AND ID.VoucherID IN (" + text + ")\r\n                        GROUP BY IT.AverageCost, ID.SysDocID, ID.VoucherID, ID.ProductID, ID.Description, ID.UnitID, ID.Quantity,\r\n                        ID.Cost, ID.Factor, ID.UnitQuantity, ID.FactorType, ID.RowIndex, ID.Remarks,ID.ITRowID\r\n                        ORDER BY ID.RowIndex\r\n                          ";
				FillDataSet(dataSet, "Inventory_Damage_Detail", cmdText);
				dataSet.Relations.Add("NoneSaleDetail", new DataColumn[2]
				{
					dataSet.Tables["Inventory_Damage"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Damage"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Inventory_Damage_Detail"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Damage_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT SysDocID [Doc ID], VoucherID [Doc Number],TransactionDate, Description, LocationID from Inventory_Damage ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE DateCreated Between '" + text + "' AND '" + text2 + "'";
			}
			if (sysDocID != "")
			{
				text3 = text3 + " AND SysDocID = '" + sysDocID + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Inventory_Damage", sqlCommand);
			return dataSet;
		}

		public bool VoidInventoryDamage(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				string exp = "UPDATE Inventory_Damage SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(87, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Inventory Damage", voucherID, sysDocID, activityType, sqlTransaction);
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
	}
}
