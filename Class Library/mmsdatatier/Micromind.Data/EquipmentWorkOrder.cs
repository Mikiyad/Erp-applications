using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EquipmentWorkOrder : StoreObject
	{
		private const string WORKORDER_TABLE = "EA_Work_Order";

		private const string WORKORDEREXPENSEDETAIL_TABLE = "EA_WorkOrder_Expense_Detail";

		private const string WORKORDERRESOURCESDETAIL_TABLE = "EA_WorkOrder_Resources_Detail";

		private const string WORKORDERMANPOWERDETAIL_TABLE = "EA_WorkOrder_ManPower_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string STATUS_PARM = "@Status";

		private const string NOTE_PARM = "@Note";

		private const string REFERENCE_PARM = "@Reference";

		private const string DESCRIPTION_PARM = "@Description";

		private const string EQUIPMENTID_PARM = "@EquipmentID";

		private const string WORKORDERTYPE_PARM = "@WorkOrderTypeID";

		private const string CURRENTMETERREADING_PARM = "@CurrentMeterReading";

		private const string ISVOID_PARM = "@IsVoid";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string SITE_PARM = "@LocationID";

		private const string EXPENSEID_PARM = "@ExpenseID";

		private const string RATETYPE_PARM = "@RateType";

		private const string PCVOUCHERID_PARM = "@PCVoucherID";

		private const string PCSYSDOCID_PARM = "@PCSysDocID";

		private const string PCROWINDEX_PARM = "@PCRowIndex";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITPRICEFC_PARM = "@UnitPriceFC";

		private const string LCOST_PARM = "@LCost";

		private const string LCOSTAMOUNT_PARM = "@LCostAmount";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string QUANTITYSHIPPED_PARM = "@QuantityShipped";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string PORVOUCHERID_PARM = "@PORVoucherID";

		private const string PORSYSDOCID_PARM = "@PORSysDocID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string ISPORROW_PARM = "@IsPORRow";

		private const string LOTNUMBER_PARM = "@LotNumber";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string ROWSOURCE_PARM = "@RowSource";

		private const string JOBID_PARM = "@JobID";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string EMPLOYEENAME_PARM = "@EmployeeName";

		private const string POSITIONID_PARM = "@PositionID";

		private const string NO_PARM = "@Hrs";

		private const string REMARKS_PARM = "@Remarks";

		public EquipmentWorkOrder(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateEquipmentWorkOrderDataText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_Work_Order", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("EquipmentID", "@EquipmentID"), new FieldValue("CurrentMeterReading", "@CurrentMeterReading"), new FieldValue("Reference", "@Reference"), new FieldValue("WorkOrderTypeID", "@WorkOrderTypeID"), new FieldValue("Status", "@Status"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("EA_Work_Order", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEquipmentWorkOrderDataCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEquipmentWorkOrderDataText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEquipmentWorkOrderDataText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@WorkOrderTypeID", SqlDbType.NVarChar);
			parameters.Add("@EquipmentID", SqlDbType.NVarChar);
			parameters.Add("@CurrentMeterReading", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EquipmentID"].SourceColumn = "EquipmentID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@CurrentMeterReading"].SourceColumn = "CurrentMeterReading";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@WorkOrderTypeID"].SourceColumn = "WorkOrderTypeID";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Status"].SourceColumn = "Status";
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

		private string GetInsertUpdateResourcesDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_WorkOrder_Resources_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("LCost", "@LCost"), new FieldValue("LCostAmount", "@LCostAmount"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("JobID", "@JobID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("PORSysDocID", "@PORSysDocID"), new FieldValue("PORVoucherID", "@PORVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("LotNumber", "@LotNumber"), new FieldValue("RowSource", "@RowSource"), new FieldValue("IsPORRow", "@IsPORRow"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateResourcesDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateResourcesDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateResourcesDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@UnitPriceFC", SqlDbType.Decimal);
			parameters.Add("@LCost", SqlDbType.Decimal);
			parameters.Add("@LCostAmount", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@AmountFC", SqlDbType.Decimal);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@PORSysDocID", SqlDbType.NVarChar);
			parameters.Add("@PORVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@IsPORRow", SqlDbType.Bit);
			parameters.Add("@LotNumber", SqlDbType.Int);
			parameters.Add("@RowSource", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@UnitPriceFC"].SourceColumn = "UnitPriceFC";
			parameters["@LCost"].SourceColumn = "LCost";
			parameters["@LCostAmount"].SourceColumn = "LCostAmount";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@PORSysDocID"].SourceColumn = "PORSysDocID";
			parameters["@PORVoucherID"].SourceColumn = "PORVoucherID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@IsPORRow"].SourceColumn = "IsPORRow";
			parameters["@LotNumber"].SourceColumn = "LotNumber";
			parameters["@RowSource"].SourceColumn = "RowSource";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateManpowerDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_WorkOrder_ManPower_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("PositionID", "@PositionID"), new FieldValue("Remarks", "@Remarks"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("Hrs", "@Hrs"), new FieldValue("EmployeeName", "@EmployeeName"));
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateManpowerDetailCommand(bool isUpdate)
		{
			if (insertCommand != null)
			{
				insertCommand = null;
			}
			insertCommand = new SqlCommand(GetInsertUpdateManpowerDetailText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeName", SqlDbType.NVarChar);
			parameters.Add("@PositionID", SqlDbType.NVarChar);
			parameters.Add("@Hrs", SqlDbType.Int);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@PositionID"].SourceColumn = "PositionID";
			parameters["@EmployeeName"].SourceColumn = "EmployeeName";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@Hrs"].SourceColumn = "Hrs";
			return insertCommand;
		}

		private string GetInsertUpdateExpenseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_WorkOrder_Expense_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("RateType", "@RateType"), new FieldValue("Reference", "@Reference"), new FieldValue("PCVoucherID", "@PCVoucherID"), new FieldValue("PCSysDocID", "@PCSysDocID"), new FieldValue("PCRowIndex", "@PCRowIndex"), new FieldValue("CurrencyRate", "@CurrencyRate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateExpenseCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateExpenseText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateExpenseText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ExpenseID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@RateType", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters.Add("@PCRowIndex", SqlDbType.Int);
			parameters.Add("@PCSysDocID", SqlDbType.NVarChar);
			parameters.Add("@PCVoucherID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ExpenseID"].SourceColumn = "ExpenseID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@RateType"].SourceColumn = "RateType";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@PCRowIndex"].SourceColumn = "PCRowIndex";
			parameters["@PCSysDocID"].SourceColumn = "PCSysDocID";
			parameters["@PCVoucherID"].SourceColumn = "PCVoucherID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateEquipmentWorkOrder(EquipmentWorkOrderData EquipmentWorkOrderData, bool isUpdate)
		{
			bool flag = true;
			string text = "";
			SqlCommand insertUpdateEquipmentWorkOrderDataCommand = GetInsertUpdateEquipmentWorkOrderDataCommand(isUpdate);
			string text2 = "";
			string sysDocID = "";
			string text3 = "";
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = EquipmentWorkOrderData.WorkOrderTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text4 = dataRow["VoucherID"].ToString();
				string sysDocID2 = dataRow["SysDocID"].ToString();
				string eqpmntID = dataRow["EquipmentID"].ToString();
				decimal num = default(decimal);
				foreach (DataRow row in EquipmentWorkOrderData.Resources_Detail.Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result2);
					decimal.TryParse(row["UnitPrice"].ToString(), out result3);
					decimal.TryParse(row["Amount"].ToString(), out result);
					num += result;
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("EA_Work_Order", "VoucherID", dataRow["SysDocID"].ToString(), text4, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				bool flag2 = false;
				decimal d = 1m;
				string a = "M";
				if (EquipmentWorkOrderData.Resources_Detail.Rows.Count > 0)
				{
					string commaSeperatedIDs = GetCommaSeperatedIDs(EquipmentWorkOrderData, "EA_WorkOrder_Resources_Detail", "ProductID");
					DataSet dataSet = new DataSet();
					text = "SELECT P.ProductID,P.ItemType,P.LastCost, P.Quantity AS TotalQuantity,AverageCost FROM Product P  \r\n        \t\t\t\t\tWHERE P.ProductID IN  (" + commaSeperatedIDs + ")";
					FillDataSet(dataSet, "Product", text, sqlTransaction);
					text = "SELECT PL.ProductID, PL.LocationID, PL.Quantity AS LocationQuantity FROM Product_Location  PL  \r\n        \t\t\t\t\tWHERE PL.ProductID IN  (" + commaSeperatedIDs + ")";
					FillDataSet(dataSet, "Product_Location", text, sqlTransaction);
					foreach (DataRow row2 in EquipmentWorkOrderData.Resources_Detail.Rows)
					{
						row2["SysDocID"] = dataRow["SysDocID"];
						row2["VoucherID"] = dataRow["VoucherID"];
						text2 = row2["ProductID"].ToString();
						string text5 = row2["LocationID"].ToString();
						_ = dataSet.Tables["Product"].Select("ProductID = '" + text2 + "'")[0];
						float result4 = 0f;
						DataRow[] array = dataSet.Tables["Product_Location"].Select("ProductID = '" + text2 + "' AND LocationID IS NOT NULL AND LocationID = '" + text5 + "'");
						if (array.Length != 0)
						{
							float.TryParse(array[0]["LocationQuantity"].ToString(), out result4);
							_ = array[0];
						}
						float num2 = 0f;
						string text6 = "";
						object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text2, sqlTransaction);
						if (fieldValue != null)
						{
							text6 = fieldValue.ToString();
						}
						if (text6 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text6)
						{
							DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text2, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text2 + "\nUnit:" + row2["UnitID"].ToString());
							float num3 = float.Parse(obj2["Factor"].ToString());
							string text7 = obj2["FactorType"].ToString();
							num2 = float.Parse(row2["Quantity"].ToString());
							row2["UnitFactor"] = num3;
							row2["FactorType"] = text7;
							row2["UnitQuantity"] = row2["Quantity"];
							num2 = ((!(text7 == "M")) ? float.Parse(Math.Round(num2 * num3, 5).ToString()) : float.Parse(Math.Round(num2 / num3, 5).ToString()));
							row2["Quantity"] = num2;
						}
						if (flag2)
						{
							decimal result5 = default(decimal);
							decimal result6 = default(decimal);
							row2["UnitPriceFC"] = row2["UnitPrice"];
							row2["AmountFC"] = row2["Amount"];
							decimal.TryParse(row2["UnitPrice"].ToString(), out result5);
							decimal.TryParse(row2["Amount"].ToString(), out result6);
							result5 = ((!(a == "M")) ? Math.Round(result5 / d, 4) : Math.Round(result5 * d, 4));
							row2["UnitPrice"] = result5;
							result6 = ((!(a == "M")) ? Math.Round(result6 / d, currencyDecimalPoints) : Math.Round(result6 * d, currencyDecimalPoints));
							row2["Amount"] = result6;
						}
					}
				}
				foreach (DataRow row3 in EquipmentWorkOrderData.Manpower_Detail.Rows)
				{
					row3["SysDocID"] = dataRow["SysDocID"];
					row3["VoucherID"] = dataRow["VoucherID"];
				}
				foreach (DataRow row4 in EquipmentWorkOrderData.Expense_Detail.Rows)
				{
					row4["SysDocID"] = dataRow["SysDocID"];
					row4["VoucherID"] = dataRow["VoucherID"];
					string a2 = row4["CurrencyID"].ToString();
					if (a2 != "" && a2 != baseCurrencyID)
					{
						decimal d2 = decimal.Parse(row4["Amount"].ToString());
						row4["AmountFC"] = row4["Amount"];
						decimal result7 = 1m;
						decimal.TryParse(row4["CurrencyRate"].ToString(), out result7);
						d2 = ((!(row4["RateType"].ToString() == "M")) ? Math.Round(d2 / result7, currencyDecimalPoints) : Math.Round(d2 * result7, currencyDecimalPoints));
						row4["Amount"] = d2;
					}
					else
					{
						row4["CurrencyRate"] = 1;
					}
				}
				if (isUpdate)
				{
					flag &= DeleteResourceDetailRows(sysDocID2, text4, isDeletingTransaction: false, sqlTransaction);
					flag &= DeleteManpowerDetails(sysDocID2, text4, sqlTransaction);
					flag &= DeleteExpenseRows(sysDocID2, text4, sqlTransaction);
				}
				insertUpdateEquipmentWorkOrderDataCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(EquipmentWorkOrderData, "EA_Work_Order", insertUpdateEquipmentWorkOrderDataCommand)) : (flag & Insert(EquipmentWorkOrderData, "EA_Work_Order", insertUpdateEquipmentWorkOrderDataCommand)));
				if (!flag)
				{
					throw new CompanyException("Faild to save the transaction. Please reload the transaction and try again.");
				}
				if (EquipmentWorkOrderData.Tables["EA_WorkOrder_Resources_Detail"].Rows.Count > 0)
				{
					insertUpdateEquipmentWorkOrderDataCommand = GetInsertUpdateResourcesDetailsCommand(isUpdate: false);
					insertUpdateEquipmentWorkOrderDataCommand.Transaction = sqlTransaction;
					flag &= Insert(EquipmentWorkOrderData, "EA_WorkOrder_Resources_Detail", insertUpdateEquipmentWorkOrderDataCommand);
				}
				if (EquipmentWorkOrderData.Tables["EA_WorkOrder_ManPower_Detail"].Rows.Count > 0)
				{
					insertUpdateEquipmentWorkOrderDataCommand = GetInsertUpdateManpowerDetailCommand(isUpdate: false);
					insertUpdateEquipmentWorkOrderDataCommand.Transaction = sqlTransaction;
					flag &= Insert(EquipmentWorkOrderData, "EA_WorkOrder_ManPower_Detail", insertUpdateEquipmentWorkOrderDataCommand);
				}
				if (EquipmentWorkOrderData.Tables["EA_WorkOrder_Expense_Detail"].Rows.Count > 0)
				{
					insertUpdateEquipmentWorkOrderDataCommand = GetInsertUpdateExpenseCommand(isUpdate: false);
					insertUpdateEquipmentWorkOrderDataCommand.Transaction = sqlTransaction;
					flag &= Insert(EquipmentWorkOrderData, "EA_WorkOrder_Expense_Detail", insertUpdateEquipmentWorkOrderDataCommand);
				}
				if (EquipmentWorkOrderData.Tables["EA_WorkOrder_Resources_Detail"].Rows.Count > 0)
				{
					foreach (DataRow row5 in EquipmentWorkOrderData.Resources_Detail.Rows)
					{
						sysDocID = row5["SourceSysDocID"].ToString();
						text3 = row5["SourceVoucherID"].ToString();
						int.Parse(row5["SourceRowIndex"].ToString());
					}
					if (text3 != "")
					{
						flag &= new Mobilization(base.DBConfig).UpdateMobilizationStatus(sysDocID, text3, eqpmntID, sqlTransaction);
					}
				}
				flag &= new InventoryTransaction(base.DBConfig).AllocateUnallocatedItemsToLot(sysDocID2, text4, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("EA_Work_Order", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "EquipmentWorkOrderData";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text4, sysDocID2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text4, sysDocID2, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "EA_Work_Order", "VoucherID", sqlTransaction);
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

		public EquipmentWorkOrderData GetEquipmentWorkOrderByID(string sysDocID, string voucherID)
		{
			try
			{
				EquipmentWorkOrderData equipmentWorkOrderData = new EquipmentWorkOrderData();
				string textCommand = "SELECT * FROM EA_Work_Order WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(equipmentWorkOrderData, "EA_Work_Order", textCommand);
				if (equipmentWorkOrderData == null || equipmentWorkOrderData.Tables.Count == 0 || equipmentWorkOrderData.Tables["EA_Work_Order"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,CAST(ISNULL(Issued,0)AS real) as IssuedQty,Product.Description,Product.ItemType,Product.IsTrackLot,Product.IsTrackSerial,Product.Weight,\r\n                         Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID\r\n                         ,(SELECT CAST(ISNULL(SUM(Quantity),0)AS real) from Inventory_Transactions IT WHERE IT.ProductID=TD.ProductID AND IT.locationid=SD.LocationID ) AS Stock,PL.Quantity AS 'OnHand'\r\n\t\t\t\t\t\tFROM EA_WorkOrder_Resources_Detail TD \r\n\t\t\t\t\t\tLEFT JOIN EA_Work_Order WO ON WO.SysDocID=TD.SysDocID AND WO.VoucherID=TD.VoucherID\r\n\t\t\t\t\t\tINNER JOIN Product ON TD.ProductID=Product.ProductID\r\n\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=WO.SysDocID  \r\n\t\t\t\t\t\tLEFT JOIN Product_Location PL ON Product.ProductID = PL.ProductID AND SD.LocationID = PL.LocationID \r\n\t\t\t\t\t\tWHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(equipmentWorkOrderData, "EA_WorkOrder_Resources_Detail", textCommand);
				textCommand = "SELECT * from EA_WorkOrder_Expense_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(equipmentWorkOrderData, "EA_WorkOrder_Expense_Detail", textCommand);
				textCommand = "select *, E.FirstName+'-'+E.LastName  from EA_WorkOrder_ManPower_Detail MD \r\n                        LEFT JOIN Employee E ON MD.EmployeeID=E.EmployeeID\r\n\t\t\t\t\t\tWHERE MD.VoucherID='" + voucherID + "' AND MD.SysDocID='" + sysDocID + "'";
				FillDataSet(equipmentWorkOrderData, "EA_WorkOrder_ManPower_Detail", textCommand);
				return equipmentWorkOrderData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteExpenseRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM EA_WorkOrder_Expense_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteResourceDetailRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				EquipmentWorkOrderData equipmentWorkOrderData = new EquipmentWorkOrderData();
				string textCommand = " SELECT SOD.*, WO.EquipmentID FROM EA_WorkOrder_Resources_Detail SOD LEFT JOIN EA_Work_Order WO ON SOD.SysDocID=WO.SysDocID and SOD.VoucherID=WO.VoucherID\r\n\t\t\t\t\t\t\t  WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(equipmentWorkOrderData, "EA_WorkOrder_Resources_Detail", textCommand, sqlTransaction);
				if (equipmentWorkOrderData.Resources_Detail.Rows.Count == 0)
				{
					return true;
				}
				foreach (DataRow row in equipmentWorkOrderData.Resources_Detail.Rows)
				{
					string text = row["SourceSysDocID"].ToString();
					string text2 = row["SourceVoucherID"].ToString();
					string text3 = row["EquipmentID"].ToString();
					if (text2 != "")
					{
						textCommand = "UPDATE EA_Mobilization_Equipment__Detail SET Status=1 WHERE SysDocID='" + text + "' AND VoucherID='" + text2 + "'AND EquipmentID='" + text3 + "'";
						flag = (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
					}
				}
				textCommand = "DELETE FROM EA_WorkOrder_Resources_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteManpowerDetails(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool result = true;
			try
			{
				EquipmentWorkOrderData equipmentWorkOrderData = new EquipmentWorkOrderData();
				string textCommand = "  SELECT * FROM EA_WorkOrder_ManPower_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(equipmentWorkOrderData, "EA_WorkOrder_ManPower_Detail", textCommand);
				if (equipmentWorkOrderData.Tables["EA_WorkOrder_ManPower_Detail"].Rows.Count > 0)
				{
					string commandText = "DELETE FROM EA_WorkOrder_ManPower_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					return Delete(commandText, sqlTransaction);
				}
				return result;
			}
			catch
			{
				throw;
			}
		}

		public bool VoidEquipmentWorkOrder(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidEquipmentWorkOrder(sysDocID, voucherID, isVoid, null);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		private bool VoidEquipmentWorkOrder(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				EquipmentWorkOrderData equipmentWorkOrderData = new EquipmentWorkOrderData();
				string textCommand = " SELECT SOD.*, WO.EquipmentID FROM EA_WorkOrder_Resources_Detail SOD LEFT JOIN EA_Work_Order WO ON SOD.SysDocID=WO.SysDocID and SOD.VoucherID=WO.VoucherID\r\n\t\t\t\t\t\t\t  WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(equipmentWorkOrderData, "EA_WorkOrder_Resources_Detail", textCommand, sqlTransaction);
				if (equipmentWorkOrderData.Resources_Detail.Rows.Count == 0)
				{
					return true;
				}
				foreach (DataRow row in equipmentWorkOrderData.Resources_Detail.Rows)
				{
					string text = row["SourceSysDocID"].ToString();
					string text2 = row["SourceVoucherID"].ToString();
					string text3 = row["EquipmentID"].ToString();
					if (text2 != "")
					{
						textCommand = "UPDATE EA_Mobilization_Equipment__Detail SET Status=1 WHERE SysDocID='" + text + "' AND VoucherID='" + text2 + "'AND EquipmentID='" + text3 + "'";
						flag = (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
					}
				}
				textCommand = "UPDATE EA_Work_Order SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Equipment Work Order", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteEquipmentWorkOrder(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteResourceDetailRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= DeleteExpenseRows(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteManpowerDetails(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM EA_Work_Order WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("EquipmentWorkOrderData", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetEquipmentWorkOrderToPrint(string sysDocID, string voucherID)
		{
			return GetEquipmentWorkOrderToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetEquipmentWorkOrderToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  SI.*,\r\n                             E.Capacity, E.CapacityType, E.Description, E.Model, E.Model, E.Year, E.RegistrationNumber, E.VendorID , VendorName, Employee.FirstName+''+Employee.LastName\r\n                             from EA_Work_Order SI \r\n                             LEFT JOIN EA_Equipment E ON SI.EquipmentID=E.EquipmentID\r\n                             LEFT JOIN Vendor V ON E.VendorID=V.VendorID\r\n                             LEFT JOIN Employee ON E.OwnedBy=Employee.EmployeeID\r\n                            WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "EA_Work_Order", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["EA_Work_Order"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,PID.ProductID,PID.Description,ISNULL(UnitQuantity,PID.Quantity) AS Quantity,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,\r\n\t\t\t\t\t\tISNULL(UnitPriceFC,PID.UnitPrice) AS UnitPrice,UnitPrice as UnitPriceLC,\r\n\t\t\t\t\t\tISNULL(UnitQuantity,PID.Quantity)*ISNULL(UnitPriceFC,PID.UnitPrice) AS Total,\r\n                        ISNULL(UnitQuantity,PID.Quantity)*ISNULL(PID.UnitPrice,0) AS TotalLC,\r\n                        PID.UnitID,LocationID, P.BrandID\r\n\t\t\t\t\t\tFROM   EA_WorkOrder_Resources_Detail PID \r\n\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID=PID.ProductID\r\n\t\t\t\t\t\tWHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "EA_WorkOrder_Resources_Detail", cmdText);
				dataSet.Relations.Add("EquipmentWorkOrderDataResources", new DataColumn[2]
				{
					dataSet.Tables["EA_Work_Order"].Columns["SysDocID"],
					dataSet.Tables["EA_Work_Order"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["EA_WorkOrder_Resources_Detail"].Columns["SysDocID"],
					dataSet.Tables["EA_WorkOrder_Resources_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				cmdText = "SELECT  * FROM EA_WorkOrder_ManPower_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "EA_WorkOrder_ManPower_Detail", cmdText);
				dataSet.Relations.Add("EquipmentWorkOrderDataManpower", new DataColumn[2]
				{
					dataSet.Tables["EA_Work_Order"].Columns["SysDocID"],
					dataSet.Tables["EA_Work_Order"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["EA_WorkOrder_ManPower_Detail"].Columns["SysDocID"],
					dataSet.Tables["EA_WorkOrder_ManPower_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				cmdText = "SELECT  * FROM EA_WorkOrder_Expense_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "EA_WorkOrder_Expense_Detail", cmdText);
				dataSet.Relations.Add("EquipmentWorkOrderDataExpenses", new DataColumn[2]
				{
					dataSet.Tables["EA_Work_Order"].Columns["SysDocID"],
					dataSet.Tables["EA_Work_Order"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["EA_WorkOrder_Expense_Detail"].Columns["SysDocID"],
					dataSet.Tables["EA_WorkOrder_Expense_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseExpenseAllocationReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string sysDocID, string voucherID)
		{
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(fromDate);
				string text2 = StoreConfiguration.ToSqlDateTimeString(toDate);
				string empty = string.Empty;
				DataSet dataSet = new DataSet();
				empty = "SELECT PID.SysDocID, PID.VoucherID, PID.ProductID, PID.Description, PID.Quantity [Qty Purchased], P.Weight, PID.UnitID, UnitPrice [Factory Cost], UnitPriceFC [Factory Cost FC], PID.Amount [Purchased Amount], PID.AmountFC [Purchased Amount FC], \r\n\t\t\t\t        LCost * PID.Quantity [Direct Cost Weightwise], LCost [Direct Cost per Unit], UnitPrice + LCost [Per Unit Cost], V.VendorID + ' - ' + VendorName [Vendor] \r\n\t                  FROM Purchase_Invoice_Detail PID\r\n\t                  INNER JOIN Purchase_Invoice PI ON PID.SysDocID = PI.SysDocID AND PID.VoucherID = PI.VoucherID\r\n\t                  INNER JOIN Product P ON PID.ProductID = P.ProductID \r\n                      INNER JOIN Vendor V ON PI.VendorID = V.VendorID \r\n                        WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND ISNULL(PI.IsVoid, 'False') = 'False'";
				if (sysDocID != "")
				{
					empty = empty + " AND PI.SysDocID = '" + sysDocID + "' ";
				}
				if (voucherID != "")
				{
					empty = empty + " AND PI.VoucherID  = '" + voucherID + "' ";
				}
				if (fromItem != "")
				{
					empty = empty + " AND P.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					empty = empty + " AND P.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					empty = empty + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					empty = empty + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					empty = empty + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					empty = empty + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				FillDataSet(dataSet, "EA_WorkOrder_Resources_Detail", empty);
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
			string text3 = "select SysDocID [DOC ID], VoucherID [Doc Number], CurrentMeterReading , TransactionDate from EA_Work_Order ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "EA_Work_Order", sqlCommand);
			return dataSet;
		}

		public DataSet GetEquipmentWorkOrderList(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date]    \r\n                            FROM EA_Work_Order \r\n                            WHERE ISNULL(IsVoid,'False')='False'" + " ORDER BY TransactionDate, VoucherID ");
			FillDataSet(dataSet, "EA_Work_Order", sqlCommand);
			return dataSet;
		}

		public DataSet GetPurchaseList(string sysDocID, DateTime from, DateTime to)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date],Ven.VendorName AS [Vendor],PI.ContainerNumber AS [Container#]\r\n                            FROM Purchase_Invoice  PI INNER JOIN Vendor VEN ON PI.VendorID = Ven.VendorID\r\n                            WHERE ISNULL(IsVoid,'False')='False'  AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + " AND SysDocID='" + sysDocID + "'";
			}
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "EA_Work_Order", str);
			return dataSet;
		}

		public DataSet GetWorkorderByEquipmentLocationProjectReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				string text3 = " select EA.SysDocID, EA.VoucherID,EA.TransactionDate,EA.EquipmentID, EQ.Description,\r\n                                EA.CurrentMeterReading,CASE EA.WorkOrderTypeID when 0 THEN 'SCHEDULED' when 1 then 'BREAK DOWN' WHEN 2 THEN 'OTHERS' END AS WorkOrder, EA.Note, EA.TransactionDate ,\r\n                                 EQ.Capacity, EQ.Model, EQ.Year, EQ.Description,Case  EQ.CapacityType WHEN 1 THEN 'SEAT' WHEN 2 THEN 'TON' END AS [CAPACITY TYPE], EQ.RegistrationNumber, EQ.VendorID, VendorName\r\n                                 from EA_Work_Order EA LEFT JOIN EA_Equipment EQ ON EA.EquipmentID=EQ.EquipmentID\r\n                                 LEFT JOIN Vendor V ON EQ.VendorID=v.VendorID\r\n\t\t\t\t\t\t         WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromEquipment != "")
				{
					text3 = text3 + " AND EA.EquipmentID BETWEEN '" + fromEquipment + "' AND '" + toEquipment + "' ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND EQ.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				if (fromJob != "")
				{
					text3 = text3 + " AND EQ.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "'";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "EA_Work_Order", text3);
				text3 = "SELECT     PID.SysDocID,PID.VoucherID,PID.ProductID,PID.Description,ISNULL(UnitQuantity,PID.Quantity) AS Quantity,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,\r\n\t\t\t\t\t\tISNULL(UnitPriceFC,PID.UnitPrice) AS UnitPrice,UnitPrice as UnitPriceLC,\r\n\t\t\t\t\t\tISNULL(UnitQuantity,PID.Quantity)*ISNULL(UnitPriceFC,PID.UnitPrice) AS Total,\r\n                        ISNULL(UnitQuantity,PID.Quantity)*ISNULL(PID.UnitPrice,0) AS TotalLC,\r\n                        PID.UnitID,EQ.LocationID, P.BrandID, EA.TransactionDate\r\n\t\t\t\t\t\tFROM   EA_WorkOrder_Resources_Detail PID \r\n\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID=PID.ProductID\r\n\t\t\t\t\t\tLEFT JOIN EA_Work_Order EA ON EA.SysDocID=PID.SysDocID and EA.VoucherID=PID.VoucherID\r\n                        LEFT JOIN EA_Equipment EQ ON EA.EquipmentID=EQ.EquipmentID                         \r\n\t\t\t\t\t\t WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
				if (fromEquipment != "")
				{
					text3 = text3 + " AND EA.EquipmentID BETWEEN '" + fromEquipment + "' AND '" + toEquipment + "' ";
				}
				if (fromItem != "")
				{
					text3 = text3 + " AND PID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					text3 = text3 + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					text3 = text3 + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND EQ.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				if (fromJob != "")
				{
					text3 = text3 + " AND EQ.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "'";
				}
				FillDataSet(dataSet, "EA_WorkOrder_Resources_Detail", text3);
				dataSet.Relations.Add("WorkorderResources", new DataColumn[2]
				{
					dataSet.Tables["EA_Work_Order"].Columns["SysDocID"],
					dataSet.Tables["EA_Work_Order"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["EA_WorkOrder_Resources_Detail"].Columns["SysDocID"],
					dataSet.Tables["EA_WorkOrder_Resources_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				text3 = "select WED.* from EA_WorkOrder_Expense_Detail WED \r\n                        LEFT JOIN EA_Work_Order WO ON WED.SysDocID =WO.SysDocID AND WED.VoucherID=WO.VoucherID \r\n                        LEFT JOIN EA_Equipment EQ ON WO.EquipmentID=EQ.EquipmentID\r\n                        WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromEquipment != "")
				{
					text3 = text3 + " AND EA.EquipmentID BETWEEN '" + fromEquipment + "' AND '" + toEquipment + "' ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND EQ.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				if (fromJob != "")
				{
					text3 = text3 + " AND EQ.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				FillDataSet(dataSet, "EA_WorkOrder_Expense_Detail", text3);
				dataSet.Relations.Add("WorkorderExpenses", new DataColumn[2]
				{
					dataSet.Tables["EA_Work_Order"].Columns["SysDocID"],
					dataSet.Tables["EA_Work_Order"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["EA_WorkOrder_Expense_Detail"].Columns["SysDocID"],
					dataSet.Tables["EA_WorkOrder_Expense_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				text3 = "SELECT  MMD.*, E.FirstName+''+E.LastName AS Employee, PositionName, EAM.TransactionDate FROM EA_WorkOrder_ManPower_Detail MMD \r\n                        LEFT JOIN EA_Work_Order EAM ON MMD.SysDocID=EAM.SysDocID and MMD.VoucherID=EAM.VoucherID\r\n                        LEFT JOIN Employee E ON MMD.EmployeeID=E.EmployeeID\r\n                        LEFT JOIN EA_Equipment EQ ON EAM.EquipmentID=EQ.EquipmentID\r\n                        LEFT JOIN Position P ON MMD.PositionID =p.PositionID WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromEquipment != "")
				{
					text3 = text3 + " AND EAM.EquipmentID BETWEEN '" + fromEquipment + "' AND '" + toEquipment + "' ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND EQ.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				if (fromJob != "")
				{
					text3 = text3 + " AND EQ.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				FillDataSet(dataSet, "EA_WorkOrder_ManPower_Detail", text3);
				dataSet.Relations.Add("WorkorderManpower", new DataColumn[2]
				{
					dataSet.Tables["EA_Work_Order"].Columns["SysDocID"],
					dataSet.Tables["EA_Work_Order"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["EA_WorkOrder_ManPower_Detail"].Columns["SysDocID"],
					dataSet.Tables["EA_WorkOrder_ManPower_Detail"].Columns["VoucherID"]
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
				string textCommand = "SELECT DISTINCT JMR.SysDocID [Doc ID],JMR.VoucherID [Number], TransactionDate AS [Date]\r\n\t\t\t\t\t\t\t\t FROM EA_Work_Order JMR LEFT JOIN EA_WorkOrder_Resources_Detail JMRD ON JMR.SysDocID=JMRD.SysDocID and JMR.VoucherID=JMRD.VoucherID \r\n\t\t\t\t\t\t\t\t LEFT JOIN Job J ON JMRD.JobID=J.JobID WHERE JMR.Status=1";
				FillDataSet(dataSet, "EA_Work_Order", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowIssuedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			bool flag = true;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,Issued FROM EA_WorkOrder_Resources_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Product", textCommand);
				if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables[0].Rows[0];
					if (dataRow["UnitQuantity"] != DBNull.Value)
					{
						float.TryParse(dataRow["UnitQuantity"].ToString(), out result);
					}
					else
					{
						float.TryParse(dataRow["Quantity"].ToString(), out result);
					}
					float.TryParse(dataRow["Issued"].ToString(), out result2);
				}
				result2 += quantity;
				textCommand = "UPDATE EA_WorkOrder_Resources_Detail SET Issued=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return flag & (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
			}
			catch
			{
				return false;
			}
		}

		internal bool CloseIssuedDoc(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM EA_WorkOrder_Resources_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM EA_WorkOrder_Resources_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(Issued,0))  FROM EA_WorkOrder_Resources_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE EA_Work_Order SET Status= 2 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public bool UpdateWorkOrderStatus(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "Select Count(*) FROM EA_Work_Order WO\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' ";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE EA_Work_Order SET Status=0 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string sysDocID, string VoucherID, bool showitemwithTansactions, bool showinactiveitems, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "";
				if (fromWarehouse != "" || toWarehouse != "")
				{
					text3 = " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				string str = "SELECT DISTINCT IT.ProductID [Item Code] ,Product.Description AS [Item Description],\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID " + text3 + " AND IT2.TransactionDate<'" + text + "'),0) AS OpeningQuantity,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity*IT2.AverageCost) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  " + text3 + " AND IT2.TransactionDate<'" + text + "'),0) AS OpeningValue,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  " + text3 + " AND IT2.TransactionDate<='" + text2 + "'),0) AS ClosingQuantity,\r\n\t\t\t        ISNULL((SELECT SUM(IT2.Quantity * IT2.AverageCost) FROM Inventory_Transactions IT2 WHERE IT.ProductID=IT2.ProductID  " + text3 + " AND IT2.TransactionDate<='" + text2 + "'),0) AS ClosingValue\r\n                         \r\n\t\t\t        FROM Inventory_Transactions IT INNER JOIN Product ON IT.ProductID=Product.ProductID \r\n                          \r\n                   WHERE ";
				str = str + " Product.ItemType NOT IN ('3') AND TransactionDate < '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					str = str + " AND IT.LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (VoucherID != "")
				{
					str = str + " AND IT.EqWorkOrderID='" + VoucherID + "'";
				}
				if (showitemwithTansactions)
				{
					str = str + "AND  Product.ProductID in ( select distinct productid from Inventory_Transactions  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "')";
					if (fromWarehouse != "")
					{
						str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "') ";
					}
				}
				if (showinactiveitems)
				{
					str += "AND isnull( Product.isinactive ,0) = 0 ";
				}
				str += " GROUP BY IT.ProductID,Product.Description ORDER BY IT.ProductID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", str);
				DataSet dataSet2 = new DataSet();
				str = "SELECT 'OPENING STOCK ' SysDocID,'0000000' VoucherID,'0' SysDocType, '" + text + "' AS TransactionDate,IT.ProductID,Product.Description,'' AS LocationID, 0 AS UnitPrice, 0 AS AverageCost,'' AS Reference ,IT.EqWorkOrderID,\r\n                                  CASE  WHEN  SUM(ISNULL(IT.Quantity, 0) )> 0 THEN SUM(ISNULL(IT.Quantity, 0) ) ELSE 0 END AS [Qty In],\r\n                                  CASE  WHEN SUM(ISNULL(IT.Quantity, 0) )< 0 THEN -1 * SUM(ISNULL(IT.Quantity, 0) ) ELSE 0 END AS [Qty Out], 0 AssetValue,\r\n                                  'OPENING STOCK'  AS PayeeName\r\n                                  From Inventory_Transactions IT INNER JOIN Product ON Product.ProductID=IT.ProductID \r\n                                  LEFt OUTER JOIN Customer ON IT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                                  Vendor ON IT.PayeeID=Vendor.VendorID \r\n                                   \r\n                                \r\n                                  WHERE  convert(date, transactionDate) < '" + text + "'";
				if (fromWarehouse != "")
				{
					str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (VoucherID != "")
				{
					str = str + " AND IT.EqWorkOrderID='" + VoucherID + "'";
				}
				if (showitemwithTansactions)
				{
					str = str + "AND  Product.ProductID in ( select distinct productid from Inventory_Transactions  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "')";
					if (fromWarehouse != "")
					{
						str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "') ";
					}
				}
				if (showinactiveitems)
				{
					str += "AND isnull( Product.isinactive ,0) = 0 ";
				}
				str += "GROUP BY IT.ProductID,Product.Description, IT.EqWorkOrderID  UNION ALL ";
				str = str + "SELECT SysDocID,VoucherID,SysDocType,CAST(TransactionDate AS DATE) as TransactionDate,IT.ProductID,Product.Description,LocationID,UnitPrice,IT.AverageCost,IT.Reference ,IT.EqWorkOrderID,\r\n\t\t\t\t\tCASE  WHEN IT.Quantity > 0 THEN ISNULL(IT.Quantity, 0) ELSE 0 END AS [Qty In],\r\n\t\t\t\t\tCASE  WHEN IT.Quantity < 0 THEN -1 * ISNULL(IT.Quantity, 0) ELSE 0 END AS [Qty Out],AssetValue,\r\n\t\t\t\t\tPayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName END) AS PayeeName                  \r\n\t\t\t\t\tFrom Inventory_Transactions IT INNER JOIN Product ON Product.ProductID=IT.ProductID \r\n\t\t\t\t\tLEFt OUTER JOIN Customer ON IT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\tVendor ON IT.PayeeID=Vendor.VendorID                 \r\n\t\t\t\t\tWHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromWarehouse != "")
				{
					str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				if (fromItem != "")
				{
					str = str + " AND IT.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					str = str + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (VoucherID != "")
				{
					str = str + " AND IT.EqWorkOrderID='" + VoucherID + "'";
				}
				if (showitemwithTansactions)
				{
					str = str + "AND  Product.ProductID in ( select distinct productid from Inventory_Transactions  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "')";
					if (fromWarehouse != "")
					{
						str = str + " AND LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "') ";
					}
				}
				if (showinactiveitems)
				{
					str += "AND isnull( Product.isinactive ,0) = 0 ";
				}
				str += " ORDER BY TransactionDate, VoucherID";
				FillDataSet(dataSet2, "Inventory_Transactions", str);
				dataSet.Merge(dataSet2);
				dataSet.Tables["Inventory_Transactions"].Columns.Add("Balance");
				foreach (DataRow row in dataSet.Tables["Product"].Rows)
				{
					int num = Convert.ToInt32(decimal.Parse(row["OpeningQuantity"].ToString()));
					string str2 = row["Item Code"].ToString();
					string filterExpression = "ProductID='" + str2 + "' ";
					DataRow[] array = dataSet.Tables["Inventory_Transactions"].Select(filterExpression);
					foreach (DataRow obj2 in array)
					{
						int.TryParse(obj2["Qty Out"].ToString(), out int result);
						int.TryParse(obj2["Qty In"].ToString(), out int result2);
						obj2["Balance"] = (num = num + result2 - result);
					}
				}
				dataSet.Relations.Add("Balance Detail", dataSet.Tables["Product"].Columns["Item Code"], dataSet.Tables["Inventory_Transactions"].Columns["ProductID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
