using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class AssemblyBuild : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string DESCRIPTION_PARM = "@Description";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string ASSEMBLYPRODUCTID_PARM = "@AssemblyProductID";

		private const string NOTE_PARM = "@Note";

		private const string QUANTITYBUILD_PARM = "@QuantityBuild";

		private const string UNITWEIGHT_PARM = "@UnitWeight";

		private const string UNITCOST_PARM = "@UnitCost";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		public const string WORKCOMPDATE_PARM = "@WorkCompDate";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string BOMPRODUCTID_PARM = "@BOMProductID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string COST_PARM = "@Cost";

		private const string SUBUNITCOST_PARM = "@SubUnitCost";

		private const string COGS_PARM = "@COGS";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITID_PARM = "@UnitID";

		private const string FACTOR_PARM = "@Factor";

		private const string FACTORTYPE_PARM = "@FactorType";

		public const string EXPENSEID_PARM = "@ExpenseID";

		public const string AMOUNT_PARM = "@Amount";

		public AssemblyBuild(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateAssemblyBuildText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Assembly_Build", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("AssemblyProductID", "@AssemblyProductID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("Note", "@Note"), new FieldValue("WorkCompDate", "@WorkCompDate"), new FieldValue("LocationID", "@LocationID"), new FieldValue("QuantityBuild", "@QuantityBuild"), new FieldValue("UnitWeight", "@UnitWeight"), new FieldValue("UnitCost", "@UnitCost"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Assembly_Build", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateAssemblyBuildCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateAssemblyBuildText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateAssemblyBuildText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@AssemblyProductID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@QuantityBuild", SqlDbType.Decimal);
			parameters.Add("@UnitWeight", SqlDbType.Decimal);
			parameters.Add("@UnitCost", SqlDbType.Money);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@WorkCompDate", SqlDbType.DateTime);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@AssemblyProductID"].SourceColumn = "AssemblyProductID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@WorkCompDate"].SourceColumn = "WorkCompDate";
			parameters["@QuantityBuild"].SourceColumn = "QuantityBuild";
			parameters["@UnitWeight"].SourceColumn = "UnitWeight";
			parameters["@UnitCost"].SourceColumn = "UnitCost";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
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

		private string GetInsertUpdateAssemblyBuildDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Assembly_Build_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("BOMProductID", "@BOMProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("Cost", "@Cost"), new FieldValue("SubUnitCost", "@SubUnitCost"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitFactor", "@Factor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("LocationID", "@LocationID"), new FieldValue("COGS", "@COGS"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateAssemblyBuildExpenseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Assembly_Build_Expense", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("Reference", "@Reference"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateAssemblyBuildDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateAssemblyBuildDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateAssemblyBuildDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@BOMProductID", SqlDbType.NVarChar);
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
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@BOMProductID"].SourceColumn = "BOMProductID";
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
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdateAssemblyBuildExpenseCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateAssemblyBuildExpenseText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateAssemblyBuildExpenseText(isUpdate: false), base.DBConfig.Connection);
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

		private bool ValidateData(AssemblyBuildData journalData)
		{
			return true;
		}

		public bool InsertUpdateAssemblyBuild(AssemblyBuildData assemblyBuildData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateAssemblyBuildCommand = GetInsertUpdateAssemblyBuildCommand(isUpdate);
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			try
			{
				DataRow dataRow = assemblyBuildData.AssemblyBuildTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Assembly_Build", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in assemblyBuildData.AssemblyBuildDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				foreach (DataRow row2 in assemblyBuildData.AssemblyBuildDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					string text2 = row2["BOMProductID"].ToString();
					string text3 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text2, sqlTransaction);
					if (fieldValue != null)
					{
						text3 = fieldValue.ToString();
					}
					if (text3 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text3)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text2, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text2 + "\nUnit:" + row2["UnitID"].ToString());
						float num = float.Parse(obj2["Factor"].ToString());
						string text4 = obj2["FactorType"].ToString();
						float num2 = float.Parse(row2["Quantity"].ToString());
						row2["UnitFactor"] = num;
						row2["FactorType"] = text4;
						row2["UnitQuantity"] = row2["Quantity"];
						num2 = ((!(text4 == "M")) ? float.Parse(Math.Round(num2 * num, 5).ToString()) : float.Parse(Math.Round(num2 / num, 5).ToString()));
						row2["Quantity"] = num2;
					}
				}
				foreach (DataRow row3 in assemblyBuildData.AssemblyBuildExpenseTable.Rows)
				{
					row3["SysDocID"] = dataRow["SysDocID"];
					row3["VoucherID"] = dataRow["VoucherID"];
					string a = row3["CurrencyID"].ToString();
					if (a != "" && a != baseCurrencyID)
					{
						decimal d = decimal.Parse(row3["Amount"].ToString());
						decimal result = 1m;
						decimal.TryParse(row3["CurrencyRate"].ToString(), out result);
						d = ((!(row3["RateType"].ToString() == "M")) ? Math.Round(d / result, currencyDecimalPoints) : Math.Round(d * result, currencyDecimalPoints));
						row3["Amount"] = d;
					}
					else
					{
						row3["CurrencyRate"] = 1;
					}
				}
				if (isUpdate)
				{
					flag &= DeleteAssemblyBuildDetailsRows(sysDocID, text, isUpdate, sqlTransaction);
					flag &= DeleteAssemblyBuildExpenseRows(sysDocID, text, sqlTransaction);
				}
				insertUpdateAssemblyBuildCommand.Transaction = sqlTransaction;
				if (!isUpdate)
				{
					if (assemblyBuildData.Tables["Assembly_Build"].Rows.Count > 0)
					{
						flag &= Insert(assemblyBuildData, "Assembly_Build", insertUpdateAssemblyBuildCommand);
					}
				}
				else
				{
					flag &= Update(assemblyBuildData, "Assembly_Build", insertUpdateAssemblyBuildCommand);
				}
				insertUpdateAssemblyBuildCommand = GetInsertUpdateAssemblyBuildDetailsCommand(isUpdate: false);
				insertUpdateAssemblyBuildCommand.Transaction = sqlTransaction;
				if (assemblyBuildData.Tables["Assembly_Build_Detail"].Rows.Count > 0)
				{
					flag &= Insert(assemblyBuildData, "Assembly_Build_Detail", insertUpdateAssemblyBuildCommand);
				}
				insertUpdateAssemblyBuildCommand = GetInsertUpdateAssemblyBuildExpenseCommand(isUpdate: false);
				insertUpdateAssemblyBuildCommand.Transaction = sqlTransaction;
				if (assemblyBuildData.Tables["Assembly_Build_Expense"].Rows.Count > 0)
				{
					flag &= Insert(assemblyBuildData, "Assembly_Build_Expense", insertUpdateAssemblyBuildCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row4 in assemblyBuildData.AssemblyBuildDetailTable.Rows)
				{
					DataRow dataRow5 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["SysDocID"] = row4["SysDocID"];
					dataRow5["VoucherID"] = row4["VoucherID"];
					dataRow5["Description"] = dataRow["Description"];
					dataRow5["LocationID"] = row4["LocationID"];
					dataRow5["ProductID"] = row4["BOMProductID"];
					dataRow5["Quantity"] = -1m * decimal.Parse(row4["Quantity"].ToString());
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["RowIndex"] = row4["RowIndex"];
					dataRow5["PayeeType"] = "A";
					dataRow5["SysDocType"] = (byte)67;
					dataRow5["UnitPrice"] = row4["Cost"];
					dataRow5["Cost"] = row4["Cost"];
					dataRow5["TransactionDate"] = dataRow["TransactionDate"];
					dataRow5["TransactionType"] = (byte)8;
					dataRow5.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow5);
				}
				DataRow dataRow6 = inventoryTransactionData.InventoryTransactionTable.NewRow();
				dataRow6.BeginEdit();
				dataRow6["SysDocID"] = dataRow["SysDocID"];
				dataRow6["VoucherID"] = dataRow["VoucherID"];
				dataRow6["Description"] = dataRow["Description"];
				dataRow6["LocationID"] = dataRow["LocationID"];
				dataRow6["ProductID"] = dataRow["AssemblyProductID"];
				dataRow6["Quantity"] = decimal.Parse(dataRow["QuantityBuild"].ToString());
				dataRow6["Reference"] = dataRow["Reference"];
				dataRow6["RowIndex"] = -1;
				dataRow6["PayeeType"] = "A";
				dataRow6["SysDocType"] = (byte)67;
				dataRow6["UnitPrice"] = 0;
				dataRow6["Cost"] = 0;
				dataRow6["TransactionDate"] = dataRow["TransactionDate"];
				dataRow6["TransactionType"] = (byte)8;
				dataRow6.EndEdit();
				inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow6);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate: true, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).AllocateUnallocatedItemsToLot(sysDocID, text, sqlTransaction);
				GLData journalData = CreateAssemblyBuildGLData(assemblyBuildData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Assembly_Build", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, isInsert: true);
				string entityName = "Assembly Build";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Assembly_Build", "VoucherID", sqlTransaction);
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

		private GLData CreateAssemblyBuildGLData(AssemblyBuildData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.AssemblyBuildTable.Rows[0];
			string text = "";
			string text2 = "";
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string text3 = dataRow["LocationID"].ToString();
			string text4 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text3, sqlTransaction).ToString();
			string text5 = "";
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.AssemblyBuild;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Assembly Build";
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			decimal num = default(decimal);
			decimal d = default(decimal);
			foreach (DataRow row in transactionData.AssemblyBuildDetailTable.Rows)
			{
				decimal d2 = default(decimal);
				decimal d3 = decimal.Parse(row["Quantity"].ToString());
				decimal d4 = decimal.Parse(row["Cost"].ToString());
				string productID = row["BOMProductID"].ToString();
				text2 = row["VoucherID"].ToString();
				text = row["SysDocID"].ToString();
				int rowIndex = int.Parse(row["RowIndex"].ToString());
				text4 = new Products(base.DBConfig).GetProductAccountIDByLocation(productID, text3, Products.ProductAccounts.AssetAccount, sqlTransaction);
				if (d3 > 0m)
				{
					d2 += d3 * d4;
				}
				else
				{
					d2 += -1m * Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(productID, text, text2, rowIndex, sqlTransaction));
				}
				d += Math.Round(d2, currencyDecimalPoints);
				if (hashtable.ContainsKey(text4))
				{
					num = decimal.Parse(hashtable[text4].ToString());
					num += Math.Round(d2, currencyDecimalPoints);
					hashtable[text4] = num;
				}
				else
				{
					hashtable.Add(text4, Math.Round(d2, currencyDecimalPoints));
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
			decimal d5 = default(decimal);
			foreach (DataRow row2 in transactionData.AssemblyBuildExpenseTable.Rows)
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
				d5 += num;
			}
			decimal num2 = d + d5;
			if (num2 > 0m)
			{
				string productID2 = dataRow["AssemblyProductID"].ToString();
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				string text7 = (string)(dataRow3["AccountID"] = new Products(base.DBConfig).GetProductAccountIDByLocation(productID2, text3, Products.ProductAccounts.AssetAccount, sqlTransaction));
				dataRow3["Debit"] = Math.Abs(num2);
				dataRow3["Credit"] = DBNull.Value;
				dataRow3["Description"] = dataRow["Description"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
			}
			return gLData;
		}

		public AssemblyBuildData GetAssemblyBuildByID(string sysDocID, string voucherID)
		{
			try
			{
				AssemblyBuildData assemblyBuildData = new AssemblyBuildData();
				string textCommand = "SELECT * FROM Assembly_Build WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(assemblyBuildData, "Assembly_Build", textCommand);
				if (assemblyBuildData == null || assemblyBuildData.Tables.Count == 0 || assemblyBuildData.Tables["Assembly_Build"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM Assembly_Build_Detail TD\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(assemblyBuildData, "Assembly_Build_Detail", textCommand);
				textCommand = "SELECT TE.*\r\n                        FROM Assembly_Build_Expense TE\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(assemblyBuildData, "Assembly_Build_Expense", textCommand);
				return assemblyBuildData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteAssemblyBuildDetailsRows(string sysDocID, string voucherID, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(67, sysDocID, voucherID, !isUpdate, sqlTransaction);
				string commandText = "DELETE FROM Assembly_Build_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteAssemblyBuildExpenseRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				string commandText = "DELETE FROM Assembly_Build_Expense WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidAssemblyBuild(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteAssemblyBuild(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				new AssemblyBuildData();
				GetAssemblyBuildByID(sysDocID, voucherID);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteAssemblyBuildDetailsRows(sysDocID, voucherID, isUpdate: false, sqlTransaction);
				flag &= DeleteAssemblyBuildExpenseRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Assembly_Build WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Assembly Build", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public DataSet GetAssemblyBuildToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT IA.*,LOC.LocationName,J.JobName,JC.CostCategoryName,C.CustomerName FROM Assembly_Build IA                          \r\n                                INNER JOIN Location LOC ON Loc.LocationID = IA.LocationID\r\n                                LEFT JOIN Job J ON J.JobID=IA.JobID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Customer C ON J.CustomerID=C.CustomerID\r\n                                LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=IA.CostCategoryID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Assembly_Build", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Assembly_Build"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT *, SUM(Quantity * COST) AS Amount, SUM(ISNULL(UnitQuantity,0)* Cost) AS UnitAmount FROM Assembly_Build_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                        GROUP BY SysDocID,VoucherID, BOMProductID,Description,UnitID,Quantity,Cost, COGS, SubunitCost, LocationID, UnitFactor, UnitQuantity,FactorType,RowIndex\r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Assembly_Build_Detail", cmdText);
				cmdText = "SELECT * FROM Assembly_Build_Expense\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Assembly_Build_Expense", cmdText);
				dataSet.Relations.Add("AssemblyDetail", new DataColumn[2]
				{
					dataSet.Tables["Assembly_Build"].Columns["SysDocID"],
					dataSet.Tables["Assembly_Build"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Assembly_Build_Detail"].Columns["SysDocID"],
					dataSet.Tables["Assembly_Build_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("AssemblyExpense", new DataColumn[2]
				{
					dataSet.Tables["Assembly_Build"].Columns["SysDocID"],
					dataSet.Tables["Assembly_Build"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Assembly_Build_Expense"].Columns["SysDocID"],
					dataSet.Tables["Assembly_Build_Expense"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetAssemblyBuildReport(string fromProduct, string toProduct, string fromClass, string toClass, string fromCategory, string toCategory, string fromJobID, string toJobID, string toCostCategory, string fromCostCategory, DateTime asOfDate, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			string str = CommonLib.ToSqlDateTimeString(asOfDate);
			DataSet dataSet = new DataSet();
			string text = "SELECT AB.*,LOC.LocationName, JB.JobName, JCC.CostCategoryName\r\n\t                            FROM Assembly_Build AB INNER JOIN Product P ON AB.AssemblyProductID = P.ProductID\r\n                                INNER JOIN Location LOC ON Loc.LocationID = AB.LocationID\r\n                                LEFT OUTER JOIN Job JB ON AB.JobID = JB.JobID                     \r\n                                LEFT OUTER JOIN Job_Cost_Category JCC ON AB.CostCategoryID = JCC.CostCategoryID                           \r\n                                WHERE AB.TransactionDate <= '" + str + "'";
			if (fromProduct != "")
			{
				text = text + " AND P.ProductID >=' " + fromProduct + "'";
			}
			if (toProduct != "")
			{
				text = text + " AND P.ProductID <= '" + toProduct + "'";
			}
			if (fromClass != "")
			{
				text = text + " AND P.ClassID >= '" + fromClass + "'";
			}
			if (toClass != "")
			{
				text = text + " AND P.ClassID <= '" + toClass + "'";
			}
			if (fromCategory != "")
			{
				text = text + " AND P.CategoryID >= '" + fromCategory + "'";
			}
			if (toCategory != "")
			{
				text = text + " AND P.CategoryID <= '" + toCategory + "'";
			}
			if (fromManufacturer != "")
			{
				text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (fromJobID != "")
			{
				text = text + " AND AB.JobID >='" + fromJobID + "'";
			}
			if (toJobID != "")
			{
				text = text + " AND AB.JobID<='" + toJobID + "'";
			}
			if (fromCostCategory != "")
			{
				text = text + " AND AB.CostCategoryID >='" + fromCostCategory + "'";
			}
			if (toCostCategory != "")
			{
				text = text + " AND AB.CostCategoryID <='" + toCostCategory + "'";
			}
			FillDataSet(dataSet, "Assembly_Build", text);
			if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Assembly_Build"].Rows.Count == 0)
			{
				return null;
			}
			text = "SELECT ABD.SysDocID, ABD.VoucherID, BOMProductID, ABD.Quantity, UnitQuantity, Cost, ABD.Description, ABD.UnitID, UnitFactor, FactorType, SubunitCost, ABD.LocationID, COGS                      \r\n                        FROM Assembly_Build_Detail ABD INNER JOIN Assembly_Build AB\r\n\t                    ON ABD.SysDocID = AB.SysDocID AND ABD.VoucherID = AB.VoucherID\r\n                        INNER JOIN Product P ON AB.AssemblyProductID = P.ProductID                       \r\n                        WHERE AB.TransactionDate <= '" + str + "'";
			if (fromProduct != "")
			{
				text = text + " AND P.ProductID >=' " + fromProduct + "'";
			}
			if (toProduct != "")
			{
				text = text + " AND P.ProductID <= '" + toProduct + "'";
			}
			if (fromClass != "")
			{
				text = text + " AND P.ClassID >= '" + fromClass + "'";
			}
			if (toClass != "")
			{
				text = text + " AND P.ClassID <= '" + toClass + "'";
			}
			if (fromCategory != "")
			{
				text = text + " AND P.CategoryID >= '" + fromCategory + "'";
			}
			if (toCategory != "")
			{
				text = text + " AND P.CategoryID <= '" + toCategory + "'";
			}
			if (fromManufacturer != "")
			{
				text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (fromJobID != "")
			{
				text = text + " AND AB.JobID >='" + fromJobID + "'";
			}
			if (toJobID != "")
			{
				text = text + " AND AB.JobID<='" + toJobID + "'";
			}
			if (fromCostCategory != "")
			{
				text = text + " AND AB.CostCategoryID >='" + fromCostCategory + "'";
			}
			if (toCostCategory != "")
			{
				text = text + " AND AB.CostCategoryID <='" + toCostCategory + "'";
			}
			FillDataSet(dataSet, "Assembly_Build_Detail", text);
			dataSet.Relations.Add("AssemblyDetail", new DataColumn[2]
			{
				dataSet.Tables["Assembly_Build"].Columns["SysDocID"],
				dataSet.Tables["Assembly_Build"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Assembly_Build_Detail"].Columns["SysDocID"],
				dataSet.Tables["Assembly_Build_Detail"].Columns["VoucherID"]
			}, createConstraints: false);
			return dataSet;
		}

		public DataSet GetAssemblyBuildList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT DISTINCT JI.SysDocID [Doc ID], JI.VoucherID [VoucherID], JI.TransactionDate AS [Date]  ,JI.JobID,J.JobName \r\n                            FROM   Assembly_Build JI  LEFT JOIN Job J ON JI.JobID=J.JobID \r\n                            WHERE  JI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY JI.TransactionDate, JI.VoucherID ";
			FillDataSet(dataSet, "Assembly_Build", str);
			return dataSet;
		}

		public DataSet GetFactoryQty(string ItemCode)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "Select * from Product_Unit Where ProductId='" + ItemCode + "'";
			FillDataSet(dataSet, "Product_Unit", textCommand);
			return dataSet;
		}
	}
}
