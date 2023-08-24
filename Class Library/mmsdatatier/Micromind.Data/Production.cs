using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Production : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE1_PARM = "@Reference1";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string JOBORDERID_PARM = "@JobOrderID";

		private const string NOTE_PARM = "@Note";

		private const string ROUTEID_PARM = "@RouteID";

		private const string BOMID_PARM = "@BOMID";

		private const string WORKCOMPDATE_PARM = "@WorkCompDate";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@BOMProductID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string QUANTITYBUILD_PARM = "@QuantityBuild";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string COST_PARM = "@Cost";

		private const string COSTALLOCATION_PARM = "@CostAllocation";

		private const string UNITID_PARM = "@UnitID";

		private const string TOTAL_PARM = "@Total";

		private const string NEXTROUTE_PARM = "@NextRoute";

		private const string EXPENSEID_PARM = "@ExpenseID";

		private const string AMOUNT_PARM = "@Amount";

		private const string QUANTITY_PARM = "@Quantity";

		private const string PRICE_PARM = "@UnitPrice";

		public const string UNITQUANTITY_PARM = "@UnitQuantity";

		public const string UNITFACTOR_PARM = "@UnitFactor";

		public const string FACTORTYPE_PARM = "@FactorType";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string EMPLOYEENAME_PARM = "@EmployeeName";

		private const string POSITIONID_PARM = "@PositionID";

		private const string REMARKS_PARM = "@Remarks";

		private const string HOUR_PARM = "@Hour";

		public Production(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateProductionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Mfg_Production", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("Reference1", "@Reference1"), new FieldValue("JobOrderID", "@JobOrderID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Note", "@Note"), new FieldValue("WorkCompDate", "@WorkCompDate"), new FieldValue("BOMID", "@BOMID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("RouteID", "@RouteID"), new FieldValue("Total", "@Total"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Mfg_Production", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProductionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateProductionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateProductionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference1", SqlDbType.NVarChar);
			parameters.Add("@JobOrderID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@WorkCompDate", SqlDbType.DateTime);
			parameters.Add("@RouteID", SqlDbType.NVarChar);
			parameters.Add("@BOMID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Total", SqlDbType.Money);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference1"].SourceColumn = "Reference1";
			parameters["@JobOrderID"].SourceColumn = "JobOrderID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@WorkCompDate"].SourceColumn = "WorkCompDate";
			parameters["@RouteID"].SourceColumn = "RouteID";
			parameters["@BOMID"].SourceColumn = "BOMID";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Total"].SourceColumn = "Total";
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

		private string GetInsertUpdateProductionDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Mfg_Production_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@BOMProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("Cost", "@Cost"), new FieldValue("CostAllocation", "@CostAllocation"), new FieldValue("NextRoute", "@NextRoute"), new FieldValue("QuantityBuild", "@QuantityBuild"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("Total", "@Total"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateProductionExpenseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Mfg_Production_Expense", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Reference", "@Reference"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateProductionRawMaterialText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Mfg_Production_Raw_Material", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@BOMProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("Description", "@Description"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("Total", "@Total"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Reference", "@Reference"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateProductionResourceText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Mfg_Production_Resource", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("PositionID", "@PositionID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("EmployeeName", "@EmployeeName"), new FieldValue("Hour", "@Hour"), new FieldValue("Remarks", "@Remarks"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProductionDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateProductionDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateProductionDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@BOMProductID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Cost", SqlDbType.Real);
			parameters.Add("@CostAllocation", SqlDbType.Real);
			parameters.Add("@QuantityBuild", SqlDbType.Real);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@NextRoute", SqlDbType.NVarChar);
			parameters.Add("@Total", SqlDbType.Money);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@BOMProductID"].SourceColumn = "ProductID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@CostAllocation"].SourceColumn = "CostAllocation";
			parameters["@QuantityBuild"].SourceColumn = "QuantityBuild";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@NextRoute"].SourceColumn = "NextRoute";
			parameters["@Total"].SourceColumn = "Total";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdateProductionExpenseCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateProductionExpenseText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateProductionExpenseText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ExpenseID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ExpenseID"].SourceColumn = "ExpenseID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Reference"].SourceColumn = "Reference";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdateProductionRawMaterialsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateProductionRawMaterialText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateProductionRawMaterialText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@BOMProductID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Quantity", SqlDbType.Decimal);
			parameters.Add("@UnitPrice", SqlDbType.Money);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@Total", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@BOMProductID"].SourceColumn = "ProductID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Reference"].SourceColumn = "Reference";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdateProductionResourceCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateProductionResourceText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateProductionResourceText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@PositionID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeName", SqlDbType.NVarChar);
			parameters.Add("@Hour", SqlDbType.Decimal);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@PositionID"].SourceColumn = "PositionID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@EmployeeName"].SourceColumn = "EmployeeName";
			parameters["@Hour"].SourceColumn = "Hour";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(ProductionData journalData)
		{
			return true;
		}

		public bool InsertUpdateProduction(ProductionData productionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateProductionCommand = GetInsertUpdateProductionCommand(isUpdate);
			_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			try
			{
				DataRow dataRow = productionData.ProductionTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Mfg_Production", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in productionData.ProductionDetailTable.Rows)
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
						float num2 = float.Parse(row["QuantityBuild"].ToString());
						row["UnitFactor"] = num;
						row["FactorType"] = text4;
						row["UnitQuantity"] = row["QuantityBuild"];
						num2 = ((!(text4 == "M")) ? float.Parse(Math.Round(num2 * num, 5).ToString()) : float.Parse(Math.Round(num2 / num, 5).ToString()));
						row["QuantityBuild"] = num2;
					}
				}
				foreach (DataRow row2 in productionData.ProductionRawMaterialTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					string text5 = row2["ProductID"].ToString();
					string text6 = "";
					object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text5, sqlTransaction);
					if (fieldValue2 != null)
					{
						text6 = fieldValue2.ToString();
					}
					if (text6 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text6)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text5, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text5 + "\nUnit:" + row2["UnitID"].ToString());
						float num3 = float.Parse(obj2["Factor"].ToString());
						string text7 = obj2["FactorType"].ToString();
						float num4 = float.Parse(row2["Quantity"].ToString());
						row2["UnitFactor"] = num3;
						row2["FactorType"] = text7;
						row2["UnitQuantity"] = row2["Quantity"];
						num4 = ((!(text7 == "M")) ? float.Parse(Math.Round(num4 * num3, 5).ToString()) : float.Parse(Math.Round(num4 / num3, 5).ToString()));
						row2["Quantity"] = num4;
					}
				}
				new Currencies(base.DBConfig).GetBaseCurrencyID();
				if (isUpdate)
				{
					flag &= DeleteProductionDetailsRows(sysDocID, text, isUpdate, sqlTransaction);
					flag &= DeleteProductionExpenseRows(sysDocID, text, sqlTransaction);
					flag &= DeleteProductionRawMaterialsRows(sysDocID, text, sqlTransaction);
					flag &= DeleteProductionResourceRows(sysDocID, text, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(264, sysDocID, text, isDeletingTransaction: true, sqlTransaction);
				}
				insertUpdateProductionCommand.Transaction = sqlTransaction;
				if (!isUpdate)
				{
					if (productionData.Tables["Mfg_Production"].Rows.Count > 0)
					{
						flag &= Insert(productionData, "Mfg_Production", insertUpdateProductionCommand);
					}
				}
				else
				{
					flag &= Update(productionData, "Mfg_Production", insertUpdateProductionCommand);
				}
				insertUpdateProductionCommand = GetInsertUpdateProductionDetailsCommand(isUpdate: false);
				insertUpdateProductionCommand.Transaction = sqlTransaction;
				if (productionData.Tables["Mfg_Production_Detail"].Rows.Count > 0)
				{
					flag &= Insert(productionData, "Mfg_Production_Detail", insertUpdateProductionCommand);
				}
				insertUpdateProductionCommand = GetInsertUpdateProductionExpenseCommand(isUpdate: false);
				insertUpdateProductionCommand.Transaction = sqlTransaction;
				if (productionData.Tables["Mfg_Production_Expense"].Rows.Count > 0)
				{
					flag &= Insert(productionData, "Mfg_Production_Expense", insertUpdateProductionCommand);
				}
				insertUpdateProductionCommand = GetInsertUpdateProductionRawMaterialsCommand(isUpdate: false);
				insertUpdateProductionCommand.Transaction = sqlTransaction;
				if (productionData.Tables["Mfg_Production_Raw_Material"].Rows.Count > 0)
				{
					flag &= Insert(productionData, "Mfg_Production_Raw_Material", insertUpdateProductionCommand);
				}
				insertUpdateProductionCommand = GetInsertUpdateProductionResourceCommand(isUpdate: false);
				insertUpdateProductionCommand.Transaction = sqlTransaction;
				if (productionData.Tables["Mfg_Production_Resource"].Rows.Count > 0)
				{
					flag &= Insert(productionData, "Mfg_Production_Resource", insertUpdateProductionCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row3 in productionData.ProductionRawMaterialTable.Rows)
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
					dataRow5["SysDocType"] = 264;
					dataRow5["UnitPrice"] = row3["UnitPrice"];
					dataRow5["Cost"] = row3["UnitPrice"];
					dataRow5["TransactionDate"] = dataRow["TransactionDate"];
					dataRow5["TransactionType"] = (byte)27;
					dataRow5["DivisionID"] = dataRow["DivisionID"];
					dataRow5["CompanyID"] = dataRow["CompanyID"];
					dataRow5.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow5);
				}
				foreach (DataRow row4 in productionData.ProductionDetailTable.Rows)
				{
					DataRow dataRow7 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["SysDocID"] = row4["SysDocID"];
					dataRow7["VoucherID"] = row4["VoucherID"];
					dataRow7["Description"] = row4["Description"];
					dataRow7["LocationID"] = row4["LocationID"];
					dataRow7["ProductID"] = row4["ProductID"];
					dataRow7["Quantity"] = 1m * decimal.Parse(row4["QuantityBuild"].ToString());
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7["RowIndex"] = row4["RowIndex"];
					dataRow7["PayeeType"] = "A";
					dataRow7["SysDocType"] = 264;
					dataRow7["UnitPrice"] = row4["Cost"];
					dataRow7["Cost"] = row4["Cost"];
					dataRow7["TransactionDate"] = dataRow["TransactionDate"];
					dataRow7["TransactionType"] = (byte)27;
					dataRow7["DivisionID"] = dataRow["DivisionID"];
					dataRow7["CompanyID"] = dataRow["CompanyID"];
					dataRow7.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow7);
				}
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate: true, sqlTransaction);
				GLData journalData = CreateProductionGLData(productionData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Mfg_Production", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, isInsert: true);
				string entityName = "Production";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Mfg_Production", "VoucherID", sqlTransaction);
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

		private GLData CreateProductionGLData(ProductionData transactionData, SqlTransaction sqlTransaction)
		{
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.ProductionTable.Rows[0];
			string text = dataRow["LocationID"].ToString();
			string text2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text, sqlTransaction).ToString();
			string text3 = "";
			string sysDocID = dataRow["SysDocID"].ToString();
			dataRow["VoucherID"].ToString();
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.Production;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (int)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Production";
			dataRow2["Note"] = dataRow["Note"];
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
			foreach (DataRow row in transactionData.ProductionRawMaterialTable.Rows)
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
					num = Math.Round(num, currencyDecimalPoints);
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
					dataRow4["Description"] = dataRow["Note"];
					dataRow4["CompanyID"] = value;
					dataRow4["DivisionID"] = value2;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
			}
			decimal num3 = default(decimal);
			foreach (DataRow row2 in transactionData.ProductionExpenseTable.Rows)
			{
				string text6 = row2["ExpenseID"].ToString();
				num = decimal.Parse(row2["Amount"].ToString());
				num = Math.Round(num, currencyDecimalPoints);
				text3 = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text6, sqlTransaction);
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
				dataRow5["JVEntryType"] = (byte)4;
				dataRow5["Reference"] = text6;
				dataRow5["CompanyID"] = value;
				dataRow5["DivisionID"] = value2;
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
				num3 += num;
			}
			new Hashtable();
			new ArrayList();
			hashtable = new Hashtable();
			arrayList = new ArrayList();
			d = default(decimal);
			dataSet = new DataSet();
			num2 = default(decimal);
			foreach (DataRow row3 in transactionData.ProductionDetailTable.Rows)
			{
				decimal.Parse(row3["QuantityBuild"].ToString());
				string warehouseLocationID2 = row3["LocationID"].ToString();
				num2 = decimal.Parse(row3["Total"].ToString());
				string productID2 = row3["ProductID"].ToString();
				row3["VoucherID"].ToString();
				row3["SysDocID"].ToString();
				int.Parse(row3["RowIndex"].ToString());
				dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(productID2, text, warehouseLocationID2, sysDocID, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("Product accounts information not found for product or location.");
				}
				DataRow dataRow6 = dataSet.Tables[0].Rows[0];
				dataRow6["IncomeAccountID"].ToString();
				dataRow6["ConsignInAccountID"].ToString();
				dataRow6["COGSAccountID"].ToString();
				string text7 = dataRow6["InventoryAssetAccountID"].ToString();
				decimal d3 = num2;
				string text5 = text7;
				if (text7 == "")
				{
					throw new CompanyException("Asset account is not set for the location.");
				}
				if (hashtable.ContainsKey(text5))
				{
					num = decimal.Parse(hashtable[text5].ToString());
					num += Math.Round(d3, currencyDecimalPoints);
					hashtable[text5] = num;
				}
				else
				{
					hashtable.Add(text5, Math.Round(d3, currencyDecimalPoints));
					arrayList.Add(text5);
				}
				d += Math.Round(d3, currencyDecimalPoints);
			}
			d = Math.Round(d, currencyDecimalPoints);
			dataRow4 = gLData.JournalDetailsTable.NewRow();
			if (d != 0m)
			{
				for (int j = 0; j < hashtable.Count; j++)
				{
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					text2 = arrayList[j].ToString();
					num = decimal.Parse(hashtable[text2].ToString());
					num = Math.Round(num, currencyDecimalPoints);
					if (num > 0m)
					{
						dataRow4["Credit"] = DBNull.Value;
						dataRow4["Debit"] = num;
					}
					else
					{
						dataRow4["Credit"] = num;
						dataRow4["Debit"] = DBNull.Value;
					}
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text2;
					dataRow4["JVEntryType"] = (byte)1;
					dataRow4["Description"] = dataRow["Note"];
					dataRow4["CompanyID"] = value;
					dataRow4["DivisionID"] = value2;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
			}
			return gLData;
		}

		public ProductionData GetProductionByID(string sysDocID, string voucherID)
		{
			try
			{
				ProductionData productionData = new ProductionData();
				string textCommand = "SELECT * FROM Mfg_Production WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(productionData, "Mfg_Production", textCommand);
				if (productionData == null || productionData.Tables.Count == 0 || productionData.Tables["Mfg_Production"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT PD.*\r\n                        FROM Mfg_Production_Detail PD\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(productionData, "Mfg_Production_Detail", textCommand);
				textCommand = "SELECT E.*\r\n                        FROM Mfg_Production_Expense E\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(productionData, "Mfg_Production_Expense", textCommand);
				textCommand = "SELECT RM.*\r\n                        FROM Mfg_Production_Raw_Material RM\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(productionData, "Mfg_Production_Raw_Material", textCommand);
				textCommand = "SELECT R.*\r\n                        FROM Mfg_Production_Resource R\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(productionData, "Mfg_Production_Resource", textCommand);
				return productionData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteProductionDetailsRows(string sysDocID, string voucherID, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(264, sysDocID, voucherID, !isUpdate, sqlTransaction);
				string commandText = "DELETE FROM Mfg_Production_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteProductionExpenseRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Mfg_Production_Expense WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteProductionRawMaterialsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Mfg_Production_Raw_Material WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteProductionResourceRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Mfg_Production_Resource WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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

		public bool DeleteProduction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteProductionDetailsRows(sysDocID, voucherID, isUpdate: false, sqlTransaction);
				flag &= DeleteProductionExpenseRows(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteProductionRawMaterialsRows(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteProductionResourceRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Mfg_Production WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(264, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Production", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public DataSet GetProductionToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT DISTINCT P.SysDocID, P.VoucherID [VoucherID], P.TransactionDate AS [Date]  ,R.RouteName [Route],B.BOMName [BOM]\r\n                            ,P.LocationID,Total, P.Reference,P.Note FROM   Mfg_Production P  \r\n                            LEFT JOIN Route R ON R.RouteID=P.RouteID \r\n                             LEFT JOIN BOM B ON B.BOMID=P.BOMID \r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Mfg_Production", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Mfg_Production"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT PD.SysDocID,PD.VoucherID,PD.ProductID,PD.Description,PD.UnitID,PD.QuantityBuild [Qty],PD.Cost,PD.CostAllocation,PD.Total FROM Mfg_Production_Detail PD\r\n                         WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Mfg_Production_Detail", cmdText);
				cmdText = "SELECT PRM.SysDocID,PRM.VoucherID,PRM.ProductID,PRM.Description,PRM.UnitID,PRM.Quantity,PRM.UnitPrice,PRM.Reference,PRM.Total FROM Mfg_Production_Raw_Material PRM\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Mfg_Production_Raw_Material", cmdText);
				cmdText = "SELECT PR.SysDocID,PR.VoucherID,PR.EmployeeID,PR.EmployeeName,PR.Hour,PR.Remarks FROM Mfg_Production_Resource PR\r\n                     \r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Mfg_Production_Resource", cmdText);
				cmdText = "SELECT PE.SysDocID,PE.VoucherID,PE.ExpenseID,PE.Description,PE.Amount,PE.Reference FROM Mfg_Production_Expense PE\r\n                     \r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Mfg_Production_Expense", cmdText);
				dataSet.Relations.Add("ProductionDetail", new DataColumn[2]
				{
					dataSet.Tables["Mfg_Production"].Columns["SysDocID"],
					dataSet.Tables["Mfg_Production"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Mfg_Production_Detail"].Columns["SysDocID"],
					dataSet.Tables["Mfg_Production_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("ProductionRawMaterials", new DataColumn[2]
				{
					dataSet.Tables["Mfg_Production"].Columns["SysDocID"],
					dataSet.Tables["Mfg_Production"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Mfg_Production_Raw_Material"].Columns["SysDocID"],
					dataSet.Tables["Mfg_Production_Raw_Material"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("ProductionResource", new DataColumn[2]
				{
					dataSet.Tables["Mfg_Production"].Columns["SysDocID"],
					dataSet.Tables["Mfg_Production"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Mfg_Production_Resource"].Columns["SysDocID"],
					dataSet.Tables["Mfg_Production_Resource"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("ProductionExpense", new DataColumn[2]
				{
					dataSet.Tables["Mfg_Production"].Columns["SysDocID"],
					dataSet.Tables["Mfg_Production"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Mfg_Production_Expense"].Columns["SysDocID"],
					dataSet.Tables["Mfg_Production_Expense"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProductionList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT DISTINCT P.SysDocID, P.VoucherID [VoucherID], P.TransactionDate AS [Date]  ,R.RouteName [Route],B.BOMName [BOM]\r\n                            ,L.LocationName [Location],isnull(Total,0)as Total, Reference,P.Note FROM   Mfg_Production P  \r\n                            LEFT JOIN Route R ON R.RouteID=P.RouteID \r\n                            LEFT JOIN BOM B ON B.BOMID=P.BOMID \r\n                            LEFT JOIN Location L ON L.LocationID=P.LocationID \r\n                            WHERE  P.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY P.TransactionDate, P.VoucherID ";
			FillDataSet(dataSet, "Mfg_Production", str);
			return dataSet;
		}
	}
}
