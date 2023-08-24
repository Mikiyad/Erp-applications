using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class InventoryDismantle : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string DESCRIPTION_PARM = "@Description";

		private const string LOCATIONID_PARM = "@LocationID";

		public const string DISMANTLEDPRODUCTID_PARM = "@DimantledProductID";

		private const string NOTE_PARM = "@Note";

		public const string QUANTITYDISMANTLED_PARM = "@QuantityDismantled";

		private const string UNITCOST_PARM = "@UnitCost";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string COST_PARM = "@Cost";

		private const string PERCENT_PARM = "@Cost_Percent";

		private const string SUBUNITCOST_PARM = "@SubUnitCost";

		private const string COGS_PARM = "@COGS";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITID_PARM = "@UnitID";

		private const string FACTOR_PARM = "@Factor";

		private const string FACTORTYPE_PARM = "@FactorType";

		public const string EXPENSEID_PARM = "@ExpenseID";

		public const string AMOUNT_PARM = "@Amount";

		public InventoryDismantle(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateInventoryDismantleText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Dismantle", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("Reference", "@Reference"), new FieldValue("DimantledProductID", "@DimantledProductID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("LocationID", "@LocationID"), new FieldValue("Note", "@Note"), new FieldValue("QuantityDismantled", "@QuantityDismantled"), new FieldValue("UnitCost", "@UnitCost"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Inventory_Dismantle", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInventoryDismantleCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInventoryDismantleText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInventoryDismantleText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@DimantledProductID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@QuantityDismantled", SqlDbType.Decimal);
			parameters.Add("@UnitCost", SqlDbType.Money);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@DimantledProductID"].SourceColumn = "DimantledProductID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@QuantityDismantled"].SourceColumn = "QuantityDismantled";
			parameters["@UnitCost"].SourceColumn = "UnitCost";
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

		private string GetInsertUpdateInventoryDismantleDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Dismantle_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("ProductID", "@ProductID"), new FieldValue("Description", "@Description"), new FieldValue("Cost", "@Cost"), new FieldValue("Cost_Percent", "@Cost_Percent"), new FieldValue("SubUnitCost", "@SubUnitCost"), new FieldValue("Quantity", "@Quantity"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitFactor", "@Factor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("COGS", "@COGS"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateInventoryDismantleExpenseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Dismantle_Expense", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("Reference", "@Reference"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInventoryDismantleDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInventoryDismantleDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInventoryDismantleDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Cost", SqlDbType.Money);
			parameters.Add("@Cost_Percent", SqlDbType.Decimal);
			parameters.Add("@SubUnitCost", SqlDbType.Money);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@Factor", SqlDbType.Real);
			parameters.Add("@FactorType", SqlDbType.Char);
			parameters.Add("@COGS", SqlDbType.Money);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Cost_Percent"].SourceColumn = "Cost_Percent";
			parameters["@SubUnitCost"].SourceColumn = "SubUnitCost";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Factor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@COGS"].SourceColumn = "COGS";
			parameters["@LocationID"].SourceColumn = "LocationID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdateInventoryDismantleExpenseCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInventoryDismantleExpenseText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInventoryDismantleExpenseText(isUpdate: false), base.DBConfig.Connection);
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

		private bool ValidateData(InventoryDismantleData journalData)
		{
			return true;
		}

		public bool InsertUpdateInventoryDismantle(InventoryDismantleData packedItemData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateInventoryDismantleCommand = GetInsertUpdateInventoryDismantleCommand(isUpdate);
			try
			{
				DataRow dataRow = packedItemData.InventoryDismantleTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Inventory_Dismantle", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in packedItemData.InventoryDismantleDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				decimal d = default(decimal);
				foreach (DataRow row2 in packedItemData.InventoryDismantleExpenseTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					string a = row2["CurrencyID"].ToString();
					if (a != "" && a != baseCurrencyID)
					{
						decimal d2 = decimal.Parse(row2["Amount"].ToString());
						decimal result = 1m;
						decimal.TryParse(row2["CurrencyRate"].ToString(), out result);
						d2 = ((!(row2["RateType"].ToString() == "M")) ? Math.Round(d2 / result, currencyDecimalPoints) : Math.Round(d2 * result, currencyDecimalPoints));
						row2["Amount"] = d2;
						d += d2;
					}
					else
					{
						row2["CurrencyRate"] = 1;
						d += decimal.Parse(row2["Amount"].ToString());
					}
				}
				if (isUpdate)
				{
					flag &= DeleteInventoryDismantleDetailsRows(sysDocID, text, isDeletingTransaction: false, sqlTransaction);
					flag &= DeleteInventoryDismantleExpenseRows(sysDocID, text, sqlTransaction);
				}
				insertUpdateInventoryDismantleCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(packedItemData, "Inventory_Dismantle", insertUpdateInventoryDismantleCommand)) : (flag & Insert(packedItemData, "Inventory_Dismantle", insertUpdateInventoryDismantleCommand)));
				insertUpdateInventoryDismantleCommand = GetInsertUpdateInventoryDismantleDetailsCommand(isUpdate: false);
				insertUpdateInventoryDismantleCommand.Transaction = sqlTransaction;
				if (packedItemData.Tables["Inventory_Dismantle_Detail"].Rows.Count > 0)
				{
					flag &= Insert(packedItemData, "Inventory_Dismantle_Detail", insertUpdateInventoryDismantleCommand);
				}
				insertUpdateInventoryDismantleCommand = GetInsertUpdateInventoryDismantleExpenseCommand(isUpdate: false);
				insertUpdateInventoryDismantleCommand.Transaction = sqlTransaction;
				if (packedItemData.Tables["Inventory_Dismantle_Expense"].Rows.Count > 0)
				{
					flag &= Insert(packedItemData, "Inventory_Dismantle_Expense", insertUpdateInventoryDismantleCommand);
				}
				decimal result2 = default(decimal);
				decimal.TryParse(dataRow["UnitCost"].ToString(), out result2);
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row3 in packedItemData.InventoryDismantleDetailTable.Rows)
				{
					DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = row3["SysDocID"];
					dataRow4["VoucherID"] = row3["VoucherID"];
					dataRow4["Description"] = row3["Description"];
					dataRow4["LocationID"] = row3["LocationID"];
					dataRow4["ProductID"] = row3["ProductID"];
					dataRow4["Quantity"] = 1m * decimal.Parse(row3["Quantity"].ToString());
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["RowIndex"] = row3["RowIndex"];
					dataRow4["PayeeType"] = "A";
					dataRow4["SysDocType"] = (byte)232;
					dataRow4["UnitPrice"] = result2;
					dataRow4["Cost"] = d + result2;
					dataRow4["TransactionDate"] = dataRow["TransactionDate"];
					dataRow4["TransactionType"] = (byte)26;
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
				}
				inventoryTransactionData.Merge(packedItemData.Tables["Product_Lot_Issue_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(packedItemData, isUpdate: false, sqlTransaction);
				inventoryTransactionData.Merge(packedItemData.Tables["Product_Lot_Receiving_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(packedItemData, isUpdate: false, sqlTransaction);
				DataRow dataRow5 = inventoryTransactionData.InventoryTransactionTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["SysDocID"] = dataRow["SysDocID"];
				dataRow5["VoucherID"] = dataRow["VoucherID"];
				dataRow5["Description"] = dataRow["Description"];
				dataRow5["LocationID"] = dataRow["LocationID"];
				dataRow5["ProductID"] = dataRow["DimantledProductID"];
				dataRow5["Quantity"] = -1m * decimal.Parse(dataRow["QuantityDismantled"].ToString());
				dataRow5["Reference"] = dataRow["Reference"];
				dataRow5["RowIndex"] = -1;
				dataRow5["PayeeType"] = "A";
				dataRow5["SysDocType"] = (byte)232;
				dataRow5["UnitPrice"] = decimal.Parse(dataRow["UnitCost"].ToString());
				dataRow5["TransactionDate"] = dataRow["TransactionDate"];
				dataRow5["TransactionType"] = (byte)8;
				dataRow5["DivisionID"] = dataRow["DivisionID"];
				dataRow5["CompanyID"] = dataRow["CompanyID"];
				dataRow5.EndEdit();
				inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow5);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate: true, sqlTransaction);
				GLData journalData = CreateInventoryDismantleGLData(packedItemData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateInventoryTransactionRowID(sysDocID, text, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Inventory_Dismantle", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, isInsert: true);
				string entityName = "Inventory Dismantled";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Inventory_Dismantle", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.InventoryDismantle, sysDocID, text, "Inventory_Dismantle", sqlTransaction);
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
				string exp = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Inventory_Dismantle_Detail SID  \r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateInventoryDismantleGLData(InventoryDismantleData transactionData, SqlTransaction sqlTransaction)
		{
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.InventoryDismantleTable.Rows[0];
			string text = dataRow["LocationID"].ToString();
			string text2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text, sqlTransaction).ToString();
			string text3 = "";
			string sysDocID = dataRow["SysDocID"].ToString();
			dataRow["VoucherID"].ToString();
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.InventoryDismantle;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Inventory Dismantled";
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			decimal num = default(decimal);
			new Hashtable();
			new ArrayList();
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			decimal d = default(decimal);
			DataSet dataSet = new DataSet();
			decimal num2 = default(decimal);
			foreach (DataRow row in transactionData.InventoryDismantleDetailTable.Rows)
			{
				decimal.Parse(row["Quantity"].ToString());
				string warehouseLocationID = row["LocationID"].ToString();
				num2 = decimal.Parse(row["Total"].ToString());
				string productID = row["ProductID"].ToString();
				row["VoucherID"].ToString();
				row["SysDocID"].ToString();
				int.Parse(row["RowIndex"].ToString());
				dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(productID, text, warehouseLocationID, sysDocID, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("Product accounts information not found for product or location.");
				}
				DataRow dataRow3 = dataSet.Tables[0].Rows[0];
				dataRow3["IncomeAccountID"].ToString();
				dataRow3["ConsignInAccountID"].ToString();
				dataRow3["COGSAccountID"].ToString();
				string text4 = dataRow3["InventoryAssetAccountID"].ToString();
				decimal d2 = num2;
				string text5 = text4;
				if (text4 == "")
				{
					throw new CompanyException("Asset account is not set for the location.");
				}
				if (hashtable.ContainsKey(text5))
				{
					num = decimal.Parse(hashtable[text5].ToString());
					num += Math.Round(d2, currencyDecimalPoints);
					hashtable[text5] = num;
				}
				else
				{
					hashtable.Add(text5, Math.Round(d2, currencyDecimalPoints));
					arrayList.Add(text5);
				}
				d += Math.Round(d2, currencyDecimalPoints);
			}
			d = Math.Round(d, currencyDecimalPoints);
			DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
			if (d != 0m)
			{
				for (int i = 0; i < hashtable.Count; i++)
				{
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					text2 = arrayList[i].ToString();
					num = decimal.Parse(hashtable[text2].ToString());
					if (num > 0m)
					{
						dataRow4["Debit"] = num;
						dataRow4["Credit"] = DBNull.Value;
					}
					else
					{
						dataRow4["Debit"] = DBNull.Value;
						dataRow4["Credit"] = Math.Abs(num);
					}
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text2;
					dataRow4["JVEntryType"] = (byte)1;
					dataRow4["Description"] = dataRow["Description"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["CompanyID"] = value;
					dataRow4["DivisionID"] = value2;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
			}
			decimal num3 = default(decimal);
			foreach (DataRow row2 in transactionData.InventoryDismantleExpenseTable.Rows)
			{
				string text6 = row2["ExpenseID"].ToString();
				num = decimal.Parse(row2["Amount"].ToString());
				text3 = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text6, sqlTransaction);
				DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["IsBaseOnly"] = true;
				dataRow5["AccountID"] = text3;
				if (num > 0m)
				{
					dataRow5["Debit"] = DBNull.Value;
					dataRow5["Credit"] = Math.Round(num, currencyDecimalPoints);
				}
				else
				{
					dataRow5["Debit"] = num;
					dataRow5["Credit"] = DBNull.Value;
				}
				dataRow4["JVEntryType"] = (byte)4;
				dataRow5["Reference"] = text6;
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
				num3 += num;
			}
			decimal result = default(decimal);
			decimal.TryParse(dataRow["UnitCost"].ToString(), out result);
			if (result > 0m)
			{
				string productID2 = dataRow["DimantledProductID"].ToString();
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				string text7 = (string)(dataRow4["AccountID"] = new Products(base.DBConfig).GetProductAccountIDByLocation(productID2, text, Products.ProductAccounts.AssetAccount, sqlTransaction));
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["Credit"] = Math.Round(result, currencyDecimalPoints);
				dataRow4["Description"] = dataRow["Description"];
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		public InventoryDismantleData GetInventoryDismantleByID(string sysDocID, string voucherID)
		{
			try
			{
				InventoryDismantleData inventoryDismantleData = new InventoryDismantleData();
				string textCommand = "SELECT * FROM Inventory_Dismantle WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(inventoryDismantleData, "Inventory_Dismantle", textCommand);
				if (inventoryDismantleData == null || inventoryDismantleData.Tables.Count == 0 || inventoryDismantleData.Tables["Inventory_Dismantle"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,CASE WHEN ItemType = 5 THEN 'True' ELSE ISNULL(IsTrackLot,'False') END AS IsTrackLot,ItemType, ISNULL(IsTrackSerial,'False') AS IsTrackSerial\r\n                        FROM Inventory_Dismantle_Detail TD INNER JOIN Product P ON P.ProductID = TD.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(inventoryDismantleData, "Inventory_Dismantle_Detail", textCommand);
				textCommand = "SELECT TE.*\r\n                        FROM Inventory_Dismantle_Expense TE\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(inventoryDismantleData, "Inventory_Dismantle_Expense", textCommand);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (inventoryDismantleData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					inventoryDismantleData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				inventoryDismantleData.Merge(transactionIssuesProductLots, preserveChanges: false);
				textCommand = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(inventoryDismantleData, "Product_Lot_Receiving_Detail", textCommand);
				return inventoryDismantleData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteInventoryDismantleDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(232, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
				string commandText = "DELETE FROM Inventory_Dismantle_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Product_Lot WHERE DocID = '" + sysDocID + "' AND ReceiptNumber = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteInventoryDismantleExpenseRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				string commandText = "DELETE FROM Inventory_Dismantle_Expense WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidInventoryDismantle(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteInventoryDismantle(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				new InventoryDismantleData();
				InventoryDismantleData inventoryDismantleByID = GetInventoryDismantleByID(sysDocID, voucherID);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteInventoryDismantleDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				DateTime.Parse(inventoryDismantleByID.InventoryDismantleTable.Rows[0]["TransactionDate"].ToString());
				flag &= DeleteInventoryDismantleExpenseRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Inventory_Dismantle WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Inventory Dismantle", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public DataSet GetInventoryDismantleToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT IA.*,LOC.LocationName,P.Description,P.UnitID FROM Inventory_Dismantle IA       \r\n                                INNER JOIN Location LOC ON Loc.LocationID = IA.LocationID\r\n                                INNER JOIN Product P ON P.ProductID = IA.DimantledProductID\r\n                                WHERE SysDocID = '" + sysDocID + "'  AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Inventory_Dismantle", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Inventory_Dismantle"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT *, SUM(Quantity * COST) AS Amount, SUM(ISNULL(UnitQuantity,0)* Cost) AS UnitAmount FROM Inventory_Dismantle_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                        GROUP BY SysDocID,VoucherID, ProductID,Description,UnitID,Quantity,Cost,Cost_Percent, COGS, SubunitCost, LocationID, UnitFactor, UnitQuantity,FactorType,RowIndex\r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Inventory_Dismantle_Detail", cmdText);
				cmdText = "SELECT * FROM Inventory_Dismantle_Expense\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Inventory_Dismantle_Expense", cmdText);
				dataSet.Relations.Add("InventoryDismantleDetail", new DataColumn[2]
				{
					dataSet.Tables["Inventory_Dismantle"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Dismantle"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Inventory_Dismantle_Detail"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Dismantle_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("InvenotryRepackingExpense", new DataColumn[2]
				{
					dataSet.Tables["Inventory_Dismantle"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Dismantle"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Inventory_Dismantle_Expense"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Dismantle_Expense"].Columns["VoucherID"]
				}, createConstraints: false);
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
			string text3 = "SELECT  SysDocID,VoucherID,DimantledProductID,QuantityDismantled,TransactionDate,LocationID FROM Inventory_Dismantle ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Inventory_Dismantle", sqlCommand);
			return dataSet;
		}
	}
}
