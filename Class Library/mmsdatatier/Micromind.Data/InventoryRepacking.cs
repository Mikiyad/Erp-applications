using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class InventoryRepacking : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string DESCRIPTION_PARM = "@Description";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string REPACKEDPRODUCTID_PARM = "@RepackedProductID";

		private const string NOTE_PARM = "@Note";

		private const string QUANTITYREPACKED_PARM = "@QuantityRepacked";

		private const string UNITCOST_PARM = "@UnitCost";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string COST_PARM = "@Cost";

		private const string SUBUNITCOST_PARM = "@SubUnitCost";

		private const string COGS_PARM = "@COGS";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITID_PARM = "@UnitID";

		private const string FACTOR_PARM = "@Factor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string REMARKS_PARM = "@Remarks";

		public const string EXPENSEID_PARM = "@ExpenseID";

		public const string AMOUNT_PARM = "@Amount";

		public InventoryRepacking(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateInventoryRepackingText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Repacking", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("Reference", "@Reference"), new FieldValue("RepackedProductID", "@RepackedProductID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("LocationID", "@LocationID"), new FieldValue("Note", "@Note"), new FieldValue("QuantityRepacked", "@QuantityRepacked"), new FieldValue("UnitCost", "@UnitCost"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Inventory_Repacking", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInventoryRepackingCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInventoryRepackingText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInventoryRepackingText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@RepackedProductID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@QuantityRepacked", SqlDbType.Decimal);
			parameters.Add("@UnitCost", SqlDbType.Money);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@RepackedProductID"].SourceColumn = "RepackedProductID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@QuantityRepacked"].SourceColumn = "QuantityRepacked";
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

		private string GetInsertUpdateInventoryRepackingDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Repacking_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("ProductID", "@ProductID"), new FieldValue("Description", "@Description"), new FieldValue("Cost", "@Cost"), new FieldValue("SubUnitCost", "@SubUnitCost"), new FieldValue("Quantity", "@Quantity"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitFactor", "@Factor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("Remarks", "@Remarks"), new FieldValue("COGS", "@COGS"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateInventoryRepackingExpenseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Inventory_Repacking_Expense", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("Reference", "@Reference"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInventoryRepackingDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInventoryRepackingDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInventoryRepackingDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Cost", SqlDbType.Money);
			parameters.Add("@SubUnitCost", SqlDbType.Money);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@Factor", SqlDbType.Real);
			parameters.Add("@FactorType", SqlDbType.Char);
			parameters.Add("@COGS", SqlDbType.Money);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@SubUnitCost"].SourceColumn = "SubUnitCost";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Factor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@COGS"].SourceColumn = "COGS";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdateInventoryRepackingExpenseCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInventoryRepackingExpenseText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInventoryRepackingExpenseText(isUpdate: false), base.DBConfig.Connection);
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

		private bool ValidateData(InventoryRepackingData journalData)
		{
			return true;
		}

		public bool InsertUpdateInventoryRepacking(InventoryRepackingData packedItemData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateInventoryRepackingCommand = GetInsertUpdateInventoryRepackingCommand(isUpdate);
			try
			{
				DataRow dataRow = packedItemData.InventoryRepackingTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Inventory_Repacking", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in packedItemData.InventoryRepackingDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text2 = row["ProductID"].ToString();
					string text3 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text2, sqlTransaction);
					if (fieldValue != null)
					{
						text3 = fieldValue.ToString();
					}
					if (text3 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text3)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text2, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text2 + "\nUnit:" + row["UnitID"].ToString());
						float num = float.Parse(obj["Factor"].ToString());
						string text4 = obj["FactorType"].ToString();
						float num2 = float.Parse(row["Quantity"].ToString());
						row["UnitFactor"] = num;
						row["FactorType"] = text4;
						row["UnitQuantity"] = row["Quantity"];
						num2 = ((!(text4 == "M")) ? float.Parse(Math.Round(num2 * num, 5).ToString()) : float.Parse(Math.Round(num2 / num, 5).ToString()));
						row["Quantity"] = num2;
					}
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				decimal d = default(decimal);
				foreach (DataRow row2 in packedItemData.InventoryRepackingExpenseTable.Rows)
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
					flag &= DeleteInventoryRepackingDetailsRows(sysDocID, text, isDeletingTransaction: false, sqlTransaction);
					flag &= DeleteInventoryRepackingExpenseRows(sysDocID, text, sqlTransaction);
				}
				insertUpdateInventoryRepackingCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(packedItemData, "Inventory_Repacking", insertUpdateInventoryRepackingCommand)) : (flag & Insert(packedItemData, "Inventory_Repacking", insertUpdateInventoryRepackingCommand)));
				insertUpdateInventoryRepackingCommand = GetInsertUpdateInventoryRepackingDetailsCommand(isUpdate: false);
				insertUpdateInventoryRepackingCommand.Transaction = sqlTransaction;
				if (packedItemData.Tables["Inventory_Repacking_Detail"].Rows.Count > 0)
				{
					flag &= Insert(packedItemData, "Inventory_Repacking_Detail", insertUpdateInventoryRepackingCommand);
				}
				insertUpdateInventoryRepackingCommand = GetInsertUpdateInventoryRepackingExpenseCommand(isUpdate: false);
				insertUpdateInventoryRepackingCommand.Transaction = sqlTransaction;
				if (packedItemData.Tables["Inventory_Repacking_Expense"].Rows.Count > 0)
				{
					flag &= Insert(packedItemData, "Inventory_Repacking_Expense", insertUpdateInventoryRepackingCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row3 in packedItemData.InventoryRepackingDetailTable.Rows)
				{
					DataRow dataRow5 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["SysDocID"] = row3["SysDocID"];
					dataRow5["VoucherID"] = row3["VoucherID"];
					dataRow5["Description"] = row3["Description"];
					dataRow5["LocationID"] = row3["LocationID"];
					dataRow5["ProductID"] = row3["ProductID"];
					dataRow5["Quantity"] = -1m * decimal.Parse(row3["Quantity"].ToString());
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["RowIndex"] = row3["RowIndex"];
					dataRow5["PayeeType"] = "A";
					dataRow5["SysDocType"] = (byte)89;
					dataRow5["UnitPrice"] = row3["Cost"];
					dataRow5["Cost"] = 0;
					dataRow5["TransactionDate"] = dataRow["TransactionDate"];
					dataRow5["TransactionType"] = (byte)8;
					dataRow5["DivisionID"] = dataRow["DivisionID"];
					dataRow5["CompanyID"] = dataRow["CompanyID"];
					dataRow5.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow5);
				}
				inventoryTransactionData.Merge(packedItemData.Tables["Product_Lot_Issue_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(packedItemData, isUpdate: false, sqlTransaction);
				inventoryTransactionData.Merge(packedItemData.Tables["Product_Lot_Receiving_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(packedItemData, isUpdate: false, sqlTransaction);
				DataRow dataRow6 = inventoryTransactionData.InventoryTransactionTable.NewRow();
				dataRow6.BeginEdit();
				dataRow6["SysDocID"] = dataRow["SysDocID"];
				dataRow6["VoucherID"] = dataRow["VoucherID"];
				dataRow6["Description"] = dataRow["Description"];
				dataRow6["LocationID"] = dataRow["LocationID"];
				dataRow6["ProductID"] = dataRow["RepackedProductID"];
				dataRow6["Quantity"] = decimal.Parse(dataRow["QuantityRepacked"].ToString());
				dataRow6["Reference"] = dataRow["Reference"];
				dataRow6["RowIndex"] = -1;
				dataRow6["PayeeType"] = "A";
				dataRow6["SysDocType"] = (byte)89;
				dataRow6["UnitPrice"] = 0;
				decimal num3 = decimal.Parse(dataRow["QuantityRepacked"].ToString());
				if (num3 > 0m)
				{
					dataRow6["Cost"] = Math.Round(d / num3, 4);
				}
				else
				{
					dataRow6["Cost"] = 0;
				}
				dataRow6["TransactionDate"] = dataRow["TransactionDate"];
				dataRow6["TransactionType"] = (byte)8;
				dataRow6["DivisionID"] = dataRow["DivisionID"];
				dataRow6["CompanyID"] = dataRow["CompanyID"];
				dataRow6.EndEdit();
				inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow6);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate: true, sqlTransaction);
				GLData journalData = CreateInventoryRepackingGLData(packedItemData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateInventoryTransactionRowID(sysDocID, text, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Inventory_Repacking", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, isInsert: true);
				string entityName = "Inventory Repacked";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Inventory_Repacking", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.InventoryRepacking, sysDocID, text, "Inventory_Repacking", sqlTransaction);
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
				string exp = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Inventory_Repacking_Detail SID  \r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateInventoryRepackingGLData(InventoryRepackingData transactionData, SqlTransaction sqlTransaction)
		{
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.InventoryRepackingTable.Rows[0];
			string text = dataRow["LocationID"].ToString();
			string text2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text, sqlTransaction).ToString();
			string text3 = "";
			string sysDocID = dataRow["SysDocID"].ToString();
			string text4 = dataRow["VoucherID"].ToString();
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.InventoryRepacking;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Inventory Repacked";
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			decimal num = default(decimal);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			Hashtable hashtable2 = new Hashtable();
			ArrayList arrayList2 = new ArrayList();
			decimal d = default(decimal);
			DataSet dataSet = new DataSet();
			foreach (DataRow row in transactionData.InventoryRepackingDetailTable.Rows)
			{
				decimal.Parse(row["Quantity"].ToString());
				string warehouseLocationID = row["LocationID"].ToString();
				decimal.Parse(row["Cost"].ToString());
				string productID = row["ProductID"].ToString();
				text4 = row["VoucherID"].ToString();
				row["SysDocID"].ToString();
				int rowIndex = int.Parse(row["RowIndex"].ToString());
				dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(productID, text, warehouseLocationID, sysDocID, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("Product accounts information not found for product or location.");
				}
				DataRow dataRow3 = dataSet.Tables[0].Rows[0];
				dataRow3["IncomeAccountID"].ToString();
				dataRow3["ConsignInAccountID"].ToString();
				string text5 = dataRow3["COGSAccountID"].ToString();
				string text6 = dataRow3["InventoryAssetAccountID"].ToString();
				decimal d2 = Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(productID, sysDocID, text4, rowIndex, sqlTransaction));
				if (text5 == "")
				{
					throw new CompanyException("COGS account is not set for the location.");
				}
				string text7 = text5;
				if (hashtable.ContainsKey(text7))
				{
					num = decimal.Parse(hashtable[text7].ToString());
					num += Math.Round(d2, currencyDecimalPoints);
					hashtable[text7] = num;
				}
				else
				{
					hashtable.Add(text7, Math.Round(d2, currencyDecimalPoints));
					arrayList.Add(text7);
				}
				text7 = text6;
				if (text6 == "")
				{
					throw new CompanyException("Asset account is not set for the location.");
				}
				if (hashtable2.ContainsKey(text7))
				{
					num = decimal.Parse(hashtable2[text7].ToString());
					num += Math.Round(d2, currencyDecimalPoints);
					hashtable2[text7] = num;
				}
				else
				{
					hashtable2.Add(text7, Math.Round(d2, currencyDecimalPoints));
					arrayList2.Add(text7);
				}
				d += Math.Round(d2, currencyDecimalPoints);
			}
			d = Math.Round(d, currencyDecimalPoints);
			DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
			if (d != 0m)
			{
				for (int i = 0; i < hashtable2.Count; i++)
				{
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					text2 = arrayList2[i].ToString();
					num = decimal.Parse(hashtable2[text2].ToString());
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
			decimal d3 = default(decimal);
			foreach (DataRow row2 in transactionData.InventoryRepackingExpenseTable.Rows)
			{
				string text8 = row2["ExpenseID"].ToString();
				num = decimal.Parse(row2["Amount"].ToString());
				text3 = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text8, sqlTransaction);
				DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["IsBaseOnly"] = true;
				dataRow5["AccountID"] = text3;
				if (num > 0m)
				{
					dataRow5["Debit"] = DBNull.Value;
					dataRow5["Credit"] = Math.Abs(num);
				}
				else
				{
					dataRow5["Debit"] = num;
					dataRow5["Credit"] = DBNull.Value;
				}
				dataRow5["Reference"] = text8;
				dataRow4["JVEntryType"] = (byte)4;
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
				d3 += num;
			}
			decimal num2 = d + d3;
			if (num2 > 0m)
			{
				string productID2 = dataRow["RepackedProductID"].ToString();
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				string text9 = (string)(dataRow4["AccountID"] = new Products(base.DBConfig).GetProductAccountIDByLocation(productID2, text, Products.ProductAccounts.AssetAccount, sqlTransaction));
				dataRow4["Debit"] = Math.Abs(num2);
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["Description"] = dataRow["Description"];
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4["JVEntryType"] = (byte)1;
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		public InventoryRepackingData GetInventoryRepackingByID(string sysDocID, string voucherID)
		{
			try
			{
				InventoryRepackingData inventoryRepackingData = new InventoryRepackingData();
				string textCommand = "SELECT * FROM Inventory_Repacking WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(inventoryRepackingData, "Inventory_Repacking", textCommand);
				if (inventoryRepackingData == null || inventoryRepackingData.Tables.Count == 0 || inventoryRepackingData.Tables["Inventory_Repacking"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,CASE WHEN ItemType = 5 THEN 'True' ELSE ISNULL(IsTrackLot,'False') END AS IsTrackLot,ItemType, ISNULL(IsTrackSerial,'False') AS IsTrackSerial\r\n                        FROM Inventory_Repacking_Detail TD INNER JOIN Product P ON P.ProductID = TD.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(inventoryRepackingData, "Inventory_Repacking_Detail", textCommand);
				textCommand = "SELECT TE.*\r\n                        FROM Inventory_Repacking_Expense TE\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(inventoryRepackingData, "Inventory_Repacking_Expense", textCommand);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (inventoryRepackingData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					inventoryRepackingData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				inventoryRepackingData.Merge(transactionIssuesProductLots, preserveChanges: false);
				textCommand = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(inventoryRepackingData, "Product_Lot_Receiving_Detail", textCommand);
				return inventoryRepackingData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteInventoryRepackingDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(89, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
				string commandText = "DELETE FROM Inventory_Repacking_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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

		internal bool DeleteInventoryRepackingExpenseRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				string commandText = "DELETE FROM Inventory_Repacking_Expense WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidInventoryRepacking(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteInventoryRepacking(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				new InventoryRepackingData();
				InventoryRepackingData inventoryRepackingByID = GetInventoryRepackingByID(sysDocID, voucherID);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteInventoryRepackingDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				DateTime.Parse(inventoryRepackingByID.InventoryRepackingTable.Rows[0]["TransactionDate"].ToString());
				flag &= DeleteInventoryRepackingExpenseRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Inventory_Repacking WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Inventory Repacking", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public DataSet GetInventoryRepackingToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT IA.*,LOC.LocationName,P.Description,P.UnitID FROM Inventory_Repacking IA       \r\n                                INNER JOIN Location LOC ON Loc.LocationID = IA.LocationID\r\n                                INNER JOIN Product P ON P.ProductID = IA.RepackedProductID\r\n                                WHERE SysDocID = '" + sysDocID + "'  AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Inventory_Repacking", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Inventory_Repacking"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT *, SUM(Quantity * COST) AS Amount, SUM(ISNULL(UnitQuantity,0)* Cost) AS UnitAmount FROM Inventory_Repacking_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                        GROUP BY SysDocID,VoucherID, ProductID,Description,UnitID,Quantity,Cost, COGS, SubunitCost, LocationID, UnitFactor, UnitQuantity,FactorType,RowIndex, Remarks,ITRowID\r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Inventory_Repacking_Detail", cmdText);
				cmdText = "SELECT * FROM Inventory_Repacking_Expense\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Inventory_Repacking_Expense", cmdText);
				dataSet.Relations.Add("InventoryRepackingDetail", new DataColumn[2]
				{
					dataSet.Tables["Inventory_Repacking"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Repacking"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Inventory_Repacking_Detail"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Repacking_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("InvenotryRepackingExpense", new DataColumn[2]
				{
					dataSet.Tables["Inventory_Repacking"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Repacking"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Inventory_Repacking_Expense"].Columns["SysDocID"],
					dataSet.Tables["Inventory_Repacking_Expense"].Columns["VoucherID"]
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
			string text3 = "SELECT  SysDocID,VoucherID,RepackedProductID,QuantityRepacked,TransactionDate,LocationID,Description,Reference FROM Inventory_Repacking ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Inventory_Repacking", sqlCommand);
			return dataSet;
		}
	}
}
