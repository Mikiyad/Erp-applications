using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Mobilization : StoreObject
	{
		private const string MOBILIZATION_TABLE = "EA_Mobilization";

		private const string MOBILIZATIONEQUPMENTDETAIL_TABLE = "EA_Mobilization_Equipment__Detail";

		private const string MOBILIZATIONRESOURCESDETAIL_TABLE = "EA_Mobilization_Resources__Detail";

		private const string MOBILIZATIONMANPOWERDETAIL_TABLE = "EA_Mobilization_Manpower__Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string PLANNEDDATEFROM_PARM = "@PlannedDateFrom";

		private const string PLANNEDDATETO_PARM = "@PlannedDateTo";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string STATUS_PARM = "@Status";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string DISCOUNT_PARM = "@Discount";

		private const string DISCOUNTFC_PARM = "@DiscountFC";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TOTAL_PARM = "@Total";

		private const string TOTALFC_PARM = "@TotalFC";

		private const string REQUISITIONNUMBER_PARM = "@RequisitionNumber";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string ISSOURCEDROW_PARM = "@IsSourcedRow";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string EQUIPMENTID_PARM = "@EquipmentID";

		private const string SITE_PARM = "@LocationID";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITPRICEFC_PARM = "@UnitPriceFC";

		private const string DESCRIPTION_PARM = "@Description";

		private const string LCOST_PARM = "@LCost";

		private const string LCOSTAMOUNT_PARM = "@LCostAmount";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string QUANTITYSHIPPED_PARM = "@QuantityShipped";

		private const string ORDERVOUCHERID_PARM = "@OrderVoucherID";

		private const string ORDERSYSDOCID_PARM = "@OrderSysDocID";

		private const string PORVOUCHERID_PARM = "@PORVoucherID";

		private const string PORSYSDOCID_PARM = "@PORSysDocID";

		private const string ORDERROWINDEX_PARM = "@OrderRowIndex";

		private const string ISPORROW_PARM = "@IsPORRow";

		private const string LOTNUMBER_PARM = "@LotNumber";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string ROWSOURCE_PARM = "@RowSource";

		private const string JOBID_PARM = "@JobID";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string EMPLOYEENAME_PARM = "@EmployeeName";

		private const string POSITIONID_PARM = "@PositionID";

		private const string NO_PARM = "@NoOfMembers";

		private const string REMARKS_PARM = "@Remarks";

		public Mobilization(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateMobilizationText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_Mobilization", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("RequisitionNumber", "@RequisitionNumber"), new FieldValue("PlannedDateFrom", "@PlannedDateFrom"), new FieldValue("PlannedDateTo", "@PlannedDateTo"), new FieldValue("Status", "@Status"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("EA_Mobilization", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateMobilizationCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateMobilizationText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateMobilizationText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@RequisitionNumber", SqlDbType.NVarChar);
			parameters.Add("@PlannedDateFrom", SqlDbType.DateTime);
			parameters.Add("@PlannedDateTo", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@DiscountFC", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@TotalFC", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@RequisitionNumber"].SourceColumn = "RequisitionNumber";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@PlannedDateFrom"].SourceColumn = "PlannedDateFrom";
			parameters["@PlannedDateTo"].SourceColumn = "PlannedDateTo";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@DiscountFC"].SourceColumn = "DiscountFC";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@TotalFC"].SourceColumn = "TotalFC";
			parameters["@Note"].SourceColumn = "Note";
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
			sqlBuilder.AddInsertUpdateParameters("EA_Mobilization_Resources__Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("LCost", "@LCost"), new FieldValue("LCostAmount", "@LCostAmount"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("JobID", "@JobID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("OrderSysDocID", "@OrderSysDocID"), new FieldValue("OrderVoucherID", "@OrderVoucherID"), new FieldValue("PORSysDocID", "@PORSysDocID"), new FieldValue("PORVoucherID", "@PORVoucherID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("LotNumber", "@LotNumber"), new FieldValue("RowSource", "@RowSource"), new FieldValue("IsPORRow", "@IsPORRow"));
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
			parameters.Add("@OrderSysDocID", SqlDbType.NVarChar);
			parameters.Add("@OrderVoucherID", SqlDbType.NVarChar);
			parameters.Add("@PORSysDocID", SqlDbType.NVarChar);
			parameters.Add("@PORVoucherID", SqlDbType.NVarChar);
			parameters.Add("@OrderRowIndex", SqlDbType.Int);
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
			parameters["@OrderVoucherID"].SourceColumn = "OrderVoucherID";
			parameters["@OrderSysDocID"].SourceColumn = "OrderSysDocID";
			parameters["@PORSysDocID"].SourceColumn = "PORSysDocID";
			parameters["@PORVoucherID"].SourceColumn = "PORVoucherID";
			parameters["@OrderRowIndex"].SourceColumn = "OrderRowIndex";
			parameters["@IsPORRow"].SourceColumn = "IsPORRow";
			parameters["@LotNumber"].SourceColumn = "LotNumber";
			parameters["@RowSource"].SourceColumn = "RowSource";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEquipmentDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_Mobilization_Equipment__Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("RequisitionNumber", "@RequisitionNumber"), new FieldValue("Remarks", "@Remarks"), new FieldValue("EquipmentID", "@EquipmentID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("JobID", "@JobID"), new FieldValue("Status", "@Status"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("IsSourcedRow", "@IsSourcedRow"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEquipmentDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEquipmentDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEquipmentDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@RequisitionNumber", SqlDbType.NVarChar);
			parameters.Add("@EquipmentID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@IsSourcedRow", SqlDbType.Bit);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@RequisitionNumber"].SourceColumn = "RequisitionNumber";
			parameters["@EquipmentID"].SourceColumn = "EquipmentID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@IsSourcedRow"].SourceColumn = "IsSourcedRow";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateManpowerDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_Mobilization_Manpower__Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("PositionID", "@PositionID"), new FieldValue("Remarks", "@Remarks"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("NoOfMembers", "@NoOfMembers"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("EmployeeName", "@EmployeeName"));
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
			parameters.Add("@NoOfMembers", SqlDbType.Int);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@PositionID"].SourceColumn = "PositionID";
			parameters["@EmployeeName"].SourceColumn = "EmployeeName";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@NoOfMembers"].SourceColumn = "NoOfMembers";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			return insertCommand;
		}

		public bool InsertUpdateMobilization(MobilizationData MobilizationData, bool isUpdate)
		{
			bool flag = true;
			string text = "";
			SqlCommand insertUpdateMobilizationCommand = GetInsertUpdateMobilizationCommand(isUpdate);
			string text2 = "";
			string text3 = "";
			string text4 = "";
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = MobilizationData.MobilizationTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text5 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				decimal num = default(decimal);
				foreach (DataRow row in MobilizationData.Resources_Detail.Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result2);
					decimal.TryParse(row["UnitPrice"].ToString(), out result3);
					decimal.TryParse(row["Amount"].ToString(), out result);
					num += result;
				}
				dataRow["Total"] = num;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("EA_Mobilization", "VoucherID", dataRow["SysDocID"].ToString(), text5, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				new Currencies(base.DBConfig).GetBaseCurrencyID();
				bool flag2 = false;
				decimal d = 1m;
				string a = "M";
				if (flag2)
				{
					decimal result4 = default(decimal);
					dataRow["TotalFC"] = dataRow["Total"];
					decimal.TryParse(dataRow["Total"].ToString(), out result4);
					result4 = ((!(a == "M")) ? Math.Round(result4 / d, 4) : Math.Round(result4 * d, 4));
					dataRow["Total"] = result4;
					result4 = default(decimal);
					dataRow["DiscountFC"] = dataRow["Discount"];
					decimal.TryParse(dataRow["DiscountFC"].ToString(), out result4);
					result4 = ((!(a == "M")) ? Math.Round(result4 / d, 4) : Math.Round(result4 * d, 4));
					dataRow["Discount"] = result4;
					result4 = default(decimal);
					dataRow["TaxAmountFC"] = dataRow["TaxAmount"];
					decimal.TryParse(dataRow["TaxAmount"].ToString(), out result4);
					result4 = ((!(a == "M")) ? Math.Round(result4 / d, 4) : Math.Round(result4 * d, 4));
					dataRow["TaxAmount"] = result4;
				}
				if (MobilizationData.Resources_Detail.Rows.Count > 0)
				{
					string commaSeperatedIDs = GetCommaSeperatedIDs(MobilizationData, "EA_Mobilization_Resources__Detail", "ProductID");
					DataSet dataSet = new DataSet();
					text = "SELECT P.ProductID,P.ItemType,P.LastCost, P.Quantity AS TotalQuantity,AverageCost FROM Product P  \r\n        \t\t\t\t\tWHERE P.ProductID IN  (" + commaSeperatedIDs + ")";
					FillDataSet(dataSet, "Product", text, sqlTransaction);
					text = "SELECT PL.ProductID, PL.LocationID, PL.Quantity AS LocationQuantity FROM Product_Location  PL  \r\n        \t\t\t\t\tWHERE PL.ProductID IN  (" + commaSeperatedIDs + ")";
					FillDataSet(dataSet, "Product_Location", text, sqlTransaction);
					foreach (DataRow row2 in MobilizationData.Resources_Detail.Rows)
					{
						row2["SysDocID"] = dataRow["SysDocID"];
						row2["VoucherID"] = dataRow["VoucherID"];
						text2 = row2["ProductID"].ToString();
						string text6 = row2["LocationID"].ToString();
						_ = dataSet.Tables["Product"].Select("ProductID = '" + text2 + "'")[0];
						float result5 = 0f;
						DataRow[] array = dataSet.Tables["Product_Location"].Select("ProductID = '" + text2 + "' AND LocationID IS NOT NULL AND LocationID = '" + text6 + "'");
						if (array.Length != 0)
						{
							float.TryParse(array[0]["LocationQuantity"].ToString(), out result5);
							_ = array[0];
						}
						float num2 = 0f;
						string text7 = "";
						object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text2, sqlTransaction);
						if (fieldValue != null)
						{
							text7 = fieldValue.ToString();
						}
						if (text7 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text7)
						{
							DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text2, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text2 + "\nUnit:" + row2["UnitID"].ToString());
							float num3 = float.Parse(obj2["Factor"].ToString());
							string text8 = obj2["FactorType"].ToString();
							num2 = float.Parse(row2["Quantity"].ToString());
							row2["UnitFactor"] = num3;
							row2["FactorType"] = text8;
							row2["UnitQuantity"] = row2["Quantity"];
							num2 = ((!(text8 == "M")) ? float.Parse(Math.Round(num2 * num3, 5).ToString()) : float.Parse(Math.Round(num2 / num3, 5).ToString()));
							row2["Quantity"] = num2;
						}
						if (flag2)
						{
							decimal result6 = default(decimal);
							decimal result7 = default(decimal);
							row2["UnitPriceFC"] = row2["UnitPrice"];
							row2["AmountFC"] = row2["Amount"];
							decimal.TryParse(row2["UnitPrice"].ToString(), out result6);
							decimal.TryParse(row2["Amount"].ToString(), out result7);
							result6 = ((!(a == "M")) ? Math.Round(result6 / d, 4) : Math.Round(result6 * d, 4));
							row2["UnitPrice"] = result6;
							result7 = ((!(a == "M")) ? Math.Round(result7 / d, currencyDecimalPoints) : Math.Round(result7 * d, currencyDecimalPoints));
							row2["Amount"] = result7;
						}
					}
				}
				foreach (DataRow row3 in MobilizationData.Equipment_Detail.Rows)
				{
					row3["SysDocID"] = dataRow["SysDocID"];
					row3["VoucherID"] = dataRow["VoucherID"];
				}
				foreach (DataRow row4 in MobilizationData.Manpower_Detail.Rows)
				{
					row4["SysDocID"] = dataRow["SysDocID"];
					row4["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteResourceDetailRows(sysDocID, text5, isDeletingTransaction: false, sqlTransaction);
					flag &= DeleteEquipmentDetailsRows(sysDocID, text5, sqlTransaction);
					flag &= DeleteManpowerDetails(sysDocID, text5, sqlTransaction);
				}
				insertUpdateMobilizationCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(MobilizationData, "EA_Mobilization", insertUpdateMobilizationCommand)) : (flag & Insert(MobilizationData, "EA_Mobilization", insertUpdateMobilizationCommand)));
				if (!flag)
				{
					throw new CompanyException("Faild to save the transaction. Please reload the transaction and try again.");
				}
				if (MobilizationData.Tables["EA_Mobilization_Resources__Detail"].Rows.Count > 0)
				{
					insertUpdateMobilizationCommand = GetInsertUpdateResourcesDetailsCommand(isUpdate: false);
					insertUpdateMobilizationCommand.Transaction = sqlTransaction;
					flag &= Insert(MobilizationData, "EA_Mobilization_Resources__Detail", insertUpdateMobilizationCommand);
				}
				if (MobilizationData.Tables["EA_Mobilization_Equipment__Detail"].Rows.Count > 0)
				{
					insertUpdateMobilizationCommand = GetInsertUpdateEquipmentDetailCommand(isUpdate: false);
					insertUpdateMobilizationCommand.Transaction = sqlTransaction;
					flag &= Insert(MobilizationData, "EA_Mobilization_Equipment__Detail", insertUpdateMobilizationCommand);
				}
				if (MobilizationData.Tables["EA_Mobilization_Manpower__Detail"].Rows.Count > 0)
				{
					insertUpdateMobilizationCommand = GetInsertUpdateManpowerDetailCommand(isUpdate: false);
					insertUpdateMobilizationCommand.Transaction = sqlTransaction;
					flag &= Insert(MobilizationData, "EA_Mobilization_Manpower__Detail", insertUpdateMobilizationCommand);
				}
				if (MobilizationData.Tables["EA_Mobilization_Equipment__Detail"].Rows.Count > 0)
				{
					foreach (DataRow row5 in MobilizationData.Equipment_Detail.Rows)
					{
						text3 = row5["SourceSysDocID"].ToString();
						text4 = row5["SourceVoucherID"].ToString();
						if (text4 != "")
						{
							flag &= new Requisition(base.DBConfig).UpdateRequisitionStatus(text3, text4, sqlTransaction);
						}
					}
				}
				flag &= new InventoryTransaction(base.DBConfig).AllocateUnallocatedItemsToLot(sysDocID, text5, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("EA_Mobilization", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Mobilization";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text5, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text5, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "EA_Mobilization", "VoucherID", sqlTransaction);
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

		public MobilizationData GetMobilizationByID(string sysDocID, string voucherID)
		{
			try
			{
				MobilizationData mobilizationData = new MobilizationData();
				string textCommand = "SELECT * FROM EA_Mobilization WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(mobilizationData, "EA_Mobilization", textCommand);
				if (mobilizationData == null || mobilizationData.Tables.Count == 0 || mobilizationData.Tables["EA_Mobilization"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.ItemType,Product.IsTrackLot,Product.IsTrackSerial,Product.Weight, Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID\r\n\t\t\t\t\t\tFROM EA_Mobilization_Resources__Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(mobilizationData, "EA_Mobilization_Resources__Detail", textCommand);
				textCommand = "SELECT * from EA_Mobilization_Equipment__Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(mobilizationData, "EA_Mobilization_Equipment__Detail", textCommand);
				textCommand = "select *, E.FirstName+'-'+E.LastName  from EA_Mobilization_Manpower__Detail MD \r\n                        LEFT JOIN Employee E ON MD.EmployeeID=E.EmployeeID\r\n\t\t\t\t\t\tWHERE MD.VoucherID='" + voucherID + "' AND MD.SysDocID='" + sysDocID + "'";
				FillDataSet(mobilizationData, "EA_Mobilization_Manpower__Detail", textCommand);
				return mobilizationData;
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
				MobilizationData dataSet = new MobilizationData();
				string textCommand = " SELECT * FROM EA_Mobilization_Resources__Detail SOD\r\n\t\t\t\t\t\t\t  WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Mobilization_Resources_Detail", textCommand, sqlTransaction);
				textCommand = "DELETE FROM EA_Mobilization_Resources__Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteEquipmentDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			MobilizationData mobilizationData = new MobilizationData();
			string textCommand = "SELECT SOD.* FROM EA_Mobilization_Equipment__Detail SOD\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
			FillDataSet(mobilizationData, "EA_Mobilization_Equipment__Detail", textCommand, sqlTransaction);
			if (mobilizationData.Equipment_Detail.Rows.Count == 0)
			{
				return true;
			}
			foreach (DataRow row in mobilizationData.Equipment_Detail.Rows)
			{
				string text = row["SourceSysDocID"].ToString();
				string text2 = row["SourceVoucherID"].ToString();
				if (text2 != "")
				{
					textCommand = "UPDATE EA_Requisition SET Status=1 WHERE SysDocID='" + text + "' AND VoucherID='" + text2 + "'";
					flag = (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				}
			}
			try
			{
				textCommand = "DELETE FROM EA_Mobilization_Equipment__Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(textCommand, sqlTransaction);
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
				MobilizationData mobilizationData = new MobilizationData();
				string textCommand = "  SELECT * FROM EA_Mobilization_Manpower__Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(mobilizationData, "EA_Mobilization_Manpower__Detail", textCommand);
				if (mobilizationData.Tables["EA_Mobilization_Manpower__Detail"].Rows.Count > 0)
				{
					string commandText = "DELETE FROM EA_Mobilization_Manpower__Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					return Delete(commandText, sqlTransaction);
				}
				return result;
			}
			catch
			{
				throw;
			}
		}

		public bool VoidMobilization(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidMobilization(sysDocID, voucherID, isVoid, null);
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

		private bool VoidMobilization(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return true;
		}

		public bool DeleteMobilization(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				MobilizationData mobilizationData = new MobilizationData();
				text = "SELECT SOD.* FROM EA_Mobilization_Equipment__Detail SOD\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(mobilizationData, "EA_Mobilization_Equipment__Detail", text, sqlTransaction);
				if (mobilizationData.Equipment_Detail.Rows.Count == 0)
				{
					return true;
				}
				foreach (DataRow row in mobilizationData.Equipment_Detail.Rows)
				{
					string text2 = row["SourceSysDocID"].ToString();
					string text3 = row["SourceVoucherID"].ToString();
					if (text3 != "")
					{
						text = "UPDATE EA_Requisition SET Status=1 WHERE SysDocID='" + text2 + "' AND VoucherID='" + text3 + "'";
						flag = (ExecuteNonQuery(text, sqlTransaction) > 0);
					}
				}
				flag &= VoidMobilization(sysDocID, voucherID, isVoid: true, sqlTransaction);
				text = "DELETE FROM EA_Mobilization WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= DeleteResourceDetailRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= DeleteEquipmentDetailsRows(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteManpowerDetails(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Mobilization", voucherID, sysDocID, activityType, sqlTransaction);
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

		internal bool CloseReceivedInvoice(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string str = "SELECT COUNT(RowIndex)FROM Purchase_Invoice_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				str += " AND CASE WHEN UnitQuantity IS NULL THEN Quantity ELSE UnitQuantity END - ISNULL(QuantityReceived,0)=0";
				object obj = ExecuteScalar(str, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				str = "UPDATE Purchase_Invoice SET IsDelivered = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(str, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool UpdateRowReceivedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float num = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReceived FROM Purchase_Invoice_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				}
				num += quantity;
				textCommand = "UPDATE Purchase_Invoice_Detail SET QuantityReceived=" + num.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowReturnedQuantity(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "  UPDATE  PID  SET QuantityReturned = \r\n                                     (SELECT SUM(Quantity) FROM Purchase_Return_Detail PRD  INNER JOIN Purchase_Return PR                                      \r\n                                     ON PR.SysDocID = PRD.SysDocID AND PR.VoucherID = PRD.VoucherID\r\n\t                                 WHERE ISNULL(IsVoid,'False') = 'False' AND PRD.SourceSysDocID = PID.SysDocID AND PRD.SourceVoucherID = PID.VoucherID AND PRD.SourceRowIndex = PID.RowIndex )\r\n\t\t\t\t\t\t\t\t\t    FROM Purchase_Invoice_Detail PID WHERE PID.SysDocID = '" + sysDocID + "' AND PID.VoucherID = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetInvoicesForDelivery(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor] FROM Purchase_Invoice SO\r\n\t\t\t\t\t\t\t INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsDelivered,0)=0";
				if (vendorID != "")
				{
					text = text + " AND SO.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "EA_Mobilization", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetMobilizationToPrint(string sysDocID, string voucherID)
		{
			return GetMobilizationToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetMobilizationToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  * from EA_Mobilization SI\r\n                            WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "EA_Mobilization", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["EA_Mobilization"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,PID.ProductID,PID.Description,ISNULL(UnitQuantity,PID.Quantity) AS Quantity,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,\r\n\t\t\t\t\t\tISNULL(UnitPriceFC,PID.UnitPrice) AS UnitPrice,UnitPrice as UnitPriceLC,\r\n\t\t\t\t\t\tISNULL(UnitQuantity,PID.Quantity)*ISNULL(UnitPriceFC,PID.UnitPrice) AS Total,\r\n                        ISNULL(UnitQuantity,PID.Quantity)*ISNULL(PID.UnitPrice,0) AS TotalLC,\r\n                        PID.UnitID,LocationID, P.BrandID\r\n\t\t\t\t\t\tFROM   EA_Mobilization_Resources__Detail PID \r\n\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID=PID.ProductID\r\n\t\t\t\t\t\tWHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "EA_Mobilization_Resources__Detail", cmdText);
				dataSet.Relations.Add("MobilizationResources", new DataColumn[2]
				{
					dataSet.Tables["EA_Mobilization"].Columns["SysDocID"],
					dataSet.Tables["EA_Mobilization"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["EA_Mobilization_Resources__Detail"].Columns["SysDocID"],
					dataSet.Tables["EA_Mobilization_Resources__Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				cmdText = "SELECT *, EA.Description FROM EA_Mobilization_Equipment__Detail MED LEFT JOIN EA_Equipment EA ON MED.EquipmentID = EA.EquipmentID WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "EA_Mobilization_Equipment__Detail", cmdText);
				dataSet.Relations.Add("MobilizationEquipment", new DataColumn[2]
				{
					dataSet.Tables["EA_Mobilization"].Columns["SysDocID"],
					dataSet.Tables["EA_Mobilization"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["EA_Mobilization_Equipment__Detail"].Columns["SysDocID"],
					dataSet.Tables["EA_Mobilization_Equipment__Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				cmdText = "SELECT  * FROM EA_Mobilization_Manpower__Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "EA_Mobilization_Manpower__Detail", cmdText);
				dataSet.Relations.Add("MobilizationManpower", new DataColumn[2]
				{
					dataSet.Tables["EA_Mobilization"].Columns["SysDocID"],
					dataSet.Tables["EA_Mobilization"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["EA_Mobilization_Manpower__Detail"].Columns["SysDocID"],
					dataSet.Tables["EA_Mobilization_Manpower__Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["EA_Mobilization"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["EA_Mobilization"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal amount = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Discount"].ToString(), out result2);
					row["TotalInWords"] = NumToWord.GetNumInWords(amount);
				}
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
			string text3 = "select SysDocID [DOC ID], VoucherID [Doc Number], RequisitionNumber , TransactionDate from EA_Mobilization ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "EA_Mobilization", sqlCommand);
			return dataSet;
		}

		public DataSet GetMobilizationList(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date]    \r\n                            FROM Purchase_Invoice \r\n                            WHERE ISNULL(IsVoid,'False')='False'" + " ORDER BY TransactionDate, VoucherID ");
			FillDataSet(dataSet, "EA_Mobilization", sqlCommand);
			return dataSet;
		}

		public DataSet GetMobilizationList()
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT EA.SysDocID [Doc ID], EA.VoucherID [Number],EA.RequisitionNumber, EAMD.SourceSysDocID, EAMD.SourceVoucherID,EAMD.EquipmentID,EAMD.RowIndex, TransactionDate AS [Date]    \r\n                            FROM EA_Mobilization EA\r\n\t\t\t\t\t\t\tLEFT JOIN EA_Mobilization_Equipment__Detail EAMD ON\r\n\t\t\t\t\t\t\tEA.SysDocID=EAMD.SysDocID and EA.VoucherID=EAMD.VoucherID\r\n                            WHERE ISNULL(IsVoid,'False')='False' and  EAMD.status=1" + " ORDER BY TransactionDate, EA.VoucherID ");
			FillDataSet(dataSet, "EA_Mobilization", sqlCommand);
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
			FillDataSet(dataSet, "EA_Mobilization", str);
			return dataSet;
		}

		public bool UpdateMobilizationStatus(string sysDocID, string voucherID, string eqpmntID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "Select Count(*) FROM EA_Mobilization Mob\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' ";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE EA_Mobilization_Equipment__Detail SET Status=0 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND EquipmentID = '" + eqpmntID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetMobilizationByEquipmentLocationProjectReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				string textCommand = "select EA.SysDocID, EA.VoucherID, EA.PlannedDateFrom, EA.PlannedDateTo, EA.RequisitionNumber, EA.Note, EA.TransactionDate  from EA_Mobilization EA\r\n\t\t\t\t\t\t     WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "EA_Mobilization", textCommand);
				textCommand = "SELECT     EA.SysDocID,EA.VoucherID,PID.ProductID,PID.Description,ISNULL(UnitQuantity,PID.Quantity) AS Quantity,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,\r\n\t\t\t\t\t\tISNULL(UnitPriceFC,PID.UnitPrice) AS UnitPrice,UnitPrice as UnitPriceLC,\r\n\t\t\t\t\t\tISNULL(UnitQuantity,PID.Quantity)*ISNULL(UnitPriceFC,PID.UnitPrice) AS Total,\r\n                        ISNULL(UnitQuantity,PID.Quantity)*ISNULL(PID.UnitPrice,0) AS TotalLC,\r\n                        PID.UnitID,LocationID, P.BrandID\r\n\t\t\t\t\t\tFROM   EA_Mobilization_Resources__Detail PID \r\n\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID=PID.ProductID\r\n\t\t\t\t\t\tLEFT JOIN EA_Mobilization EA ON EA.SysDocID=PID.SysDocID and EA.VoucherID=PID.VoucherID                     \r\n\t\t\t\t\t\t WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
				if (fromItem != "")
				{
					textCommand = textCommand + " AND PID.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					textCommand = textCommand + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					textCommand = textCommand + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					textCommand = textCommand + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					textCommand = textCommand + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					textCommand = textCommand + " AND PID.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromLocation != "")
				{
					textCommand = textCommand + " AND PID.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				if (fromJob != "")
				{
					textCommand = textCommand + " AND PID.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ORDER BY RowIndex ";
				}
				FillDataSet(dataSet, "EA_Mobilization_Resources__Detail", textCommand);
				dataSet.Relations.Add("MobilizationResources", new DataColumn[2]
				{
					dataSet.Tables["EA_Mobilization"].Columns["SysDocID"],
					dataSet.Tables["EA_Mobilization"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["EA_Mobilization_Resources__Detail"].Columns["SysDocID"],
					dataSet.Tables["EA_Mobilization_Resources__Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				textCommand = "SELECT MED.*, EA.Description, EA.Capacity,CASE EA.CapacityType WHEN 1 THEN 'SEAT' WHEN 2 THEN 'TON' END AS [CAPACITY TYPE], EA.Model, EA.Color, EA.Power, EA.Year\r\n                         FROM EA_Mobilization_Equipment__Detail MED INNER JOIN EA_Mobilization EAM ON MED.SysDocID=EAm.SysDocID and MED.VoucherID=EAM.VoucherID\r\n                         LEFT JOIN EA_Equipment EA ON MED.EquipmentID = EA.EquipmentID  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromEquipment != "")
				{
					textCommand = textCommand + " AND MED.EquipmentID BETWEEN '" + fromEquipment + "' AND '" + toEquipment + "' ";
				}
				if (fromLocation != "")
				{
					textCommand = textCommand + " AND EA.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				if (fromJob != "")
				{
					textCommand = textCommand + " AND EA.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				FillDataSet(dataSet, "EA_Mobilization_Equipment__Detail", textCommand);
				dataSet.Relations.Add("MobilizationEquipment", new DataColumn[2]
				{
					dataSet.Tables["EA_Mobilization"].Columns["SysDocID"],
					dataSet.Tables["EA_Mobilization"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["EA_Mobilization_Equipment__Detail"].Columns["SysDocID"],
					dataSet.Tables["EA_Mobilization_Equipment__Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				textCommand = "SELECT  MMD.*, E.FirstName+''+E.LastName AS Employee, PositionName FROM EA_Mobilization_Manpower__Detail MMD \r\n                        LEFT JOIN EA_Mobilization EAM ON MMD.SysDocID=EAM.SysDocID and MMD.VoucherID=EAM.VoucherID\r\n                        LEFT JOIN Employee E ON MMD.EmployeeID=E.EmployeeID\r\n                        LEFT JOIN Position P ON MMD.PositionID =p.PositionID  WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				FillDataSet(dataSet, "EA_Mobilization_Manpower__Detail", textCommand);
				dataSet.Relations.Add("MobilizationManpower", new DataColumn[2]
				{
					dataSet.Tables["EA_Mobilization"].Columns["SysDocID"],
					dataSet.Tables["EA_Mobilization"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["EA_Mobilization_Manpower__Detail"].Columns["SysDocID"],
					dataSet.Tables["EA_Mobilization_Manpower__Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
