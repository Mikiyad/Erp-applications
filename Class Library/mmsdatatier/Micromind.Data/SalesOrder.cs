using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class SalesOrder : StoreObject
	{
		private const string SALESORDER_TABLE = "Sales_Order";

		private const string SALESORDERDETAIL_TABLE = "Sales_Order_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string SALESFLOW_PARM = "@SalesFlow";

		private const string ISEXPORT_PARM = "@IsExport";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string BILLINGADDRESSID_PARM = "@BillingAddressID";

		private const string SHIPTOADDRESS_PARM = "@ShipToAddress";

		private const string PRICEINCLUDETAX_PARM = "@PriceIncludeTax";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TOTAL_PARM = "@Total";

		private const string DUEDATE_PARM = "@DueDate";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string ROUNDOFF_PARM = "@RoundOff";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REMARKS_PARM = "@Remarks";

		private const string UNITID_PARM = "@UnitID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string TAXPERCENTAGE_PARM = "@TaxPercentage";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string SPECIFICATIONID_PARM = "@SpecificationID";

		private const string STYLEID_PARM = "@StyleID";

		private const string REFSLNO_PARM = "@RefSlNo";

		private const string REFTEXT1_PARM = "@RefText1";

		private const string REFTEXT2_PARM = "@RefText2";

		private const string REFNUM1_PARM = "@RefNum1";

		private const string REFNUM2_PARM = "@RefNum2";

		private const string REFDATE1_PARM = "@RefDate1";

		private const string REFDATE2_PARM = "@RefDate2";

		private const string LOTNUMBER_PARM = "@LotNumber";

		private const string SOURCELOTNUMBER_PARM = "@SourceLotNumber";

		private const string BINID_PARM = "@BinID";

		private const string RACKID_PARM = "@RackID";

		private const string LOTQTY_PARM = "@LotQty";

		private const string SOLDQTY_PARM = "@SoldQty";

		private const string PRODUCTIONDATE_PARM = "@ProductionDate";

		private const string EXPIRYDATE_PARM = "@ExpiryDate";

		private const string COST_PARM = "@Cost";

		private const string REFERENCE2_PARM = "@Reference2";

		public SalesOrder(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSalesOrderText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Order", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalesFlow", "@SalesFlow"), new FieldValue("IsExport", "@IsExport"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("BillingAddressID", "@BillingAddressID"), new FieldValue("ShipToAddress", "@ShipToAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Discount", "@Discount"), new FieldValue("DueDate", "@DueDate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Total", "@Total"), new FieldValue("PONumber", "@PONumber"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("Note", "@Note"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("JobID", "@JobID"), new FieldValue("RoundOff", "@RoundOff"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Sales_Order", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesOrderCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesOrderText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesOrderText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@SalesFlow", SqlDbType.TinyInt);
			parameters.Add("@IsExport", SqlDbType.Bit);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@BillingAddressID", SqlDbType.NVarChar);
			parameters.Add("@ShipToAddress", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@RoundOff", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@SalesFlow"].SourceColumn = "SalesFlow";
			parameters["@IsExport"].SourceColumn = "IsExport";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@BillingAddressID"].SourceColumn = "BillingAddressID";
			parameters["@ShipToAddress"].SourceColumn = "ShipToAddress";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@PriceIncludeTax"].SourceColumn = "PriceIncludeTax";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@RoundOff"].SourceColumn = "RoundOff";
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

		private string GetInsertUpdateSalesOrderDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Order_Detail", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex", isUpdateConditionField: true), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Cost", "@Cost"), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("LocationID", "@LocationID"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxPercentage", "@TaxPercentage"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Discount", "@Discount"), new FieldValue("SpecificationID", "@SpecificationID"), new FieldValue("StyleID", "@StyleID"), new FieldValue("RefSlNo", "@RefSlNo"), new FieldValue("RefText1", "@RefText1"), new FieldValue("RefText2", "@RefText2"), new FieldValue("RefNum1", "@RefNum1"), new FieldValue("RefNum2", "@RefNum2"), new FieldValue("RefDate1", "@RefDate1"), new FieldValue("RefDate2", "@RefDate2"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesOrderDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesOrderDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesOrderDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Cost", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@TaxPercentage", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@SpecificationID", SqlDbType.NVarChar);
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters.Add("@RefSlNo", SqlDbType.NVarChar);
			parameters.Add("@RefText1", SqlDbType.NVarChar);
			parameters.Add("@RefText2", SqlDbType.NVarChar);
			parameters.Add("@RefNum1", SqlDbType.Decimal);
			parameters.Add("@RefNum2", SqlDbType.Decimal);
			parameters.Add("@RefDate1", SqlDbType.DateTime);
			parameters.Add("@RefDate2", SqlDbType.DateTime);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxPercentage"].SourceColumn = "TaxPercentage";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@SpecificationID"].SourceColumn = "SpecificationID";
			parameters["@StyleID"].SourceColumn = "StyleID";
			parameters["@RefSlNo"].SourceColumn = "RefSlNo";
			parameters["@RefText1"].SourceColumn = "RefText1";
			parameters["@RefText2"].SourceColumn = "RefText2";
			parameters["@RefNum1"].SourceColumn = "RefNum1";
			parameters["@RefNum2"].SourceColumn = "RefNum2";
			parameters["@RefDate1"].SourceColumn = "RefDate1";
			parameters["@RefDate2"].SourceColumn = "RefDate2";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(SalesOrderData journalData)
		{
			return true;
		}

		public bool InsertUpdateSalesOrder(SalesOrderData salesOrderData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateSalesOrderCommand = GetInsertUpdateSalesOrderCommand(isUpdate);
			string text = "";
			string text2 = "";
			try
			{
				DataRow dataRow = salesOrderData.SalesOrderTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text3 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				bool flag2 = bool.Parse(dataRow["AllowSOEdit"].ToString());
				bool flag3 = false;
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (!dataRow["IsExport"].IsDBNullOrEmpty())
				{
					flag3 = bool.Parse(dataRow["IsExport"].ToString());
				}
				if (isUpdate && !flag2 && OrderHasShippedQuantity(sysDocID, text3, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items in this order has been already shipped.");
				}
				decimal num = default(decimal);
				foreach (DataRow row in salesOrderData.SalesOrderDetailTable.Rows)
				{
					decimal num2 = default(decimal);
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result);
					decimal.TryParse(row["UnitPrice"].ToString(), out result2);
					decimal.TryParse(row["Discount"].ToString(), out result3);
					num2 = Math.Round(result * (result2 - result3), currencyDecimalPoints);
					num += num2;
				}
				dataRow["Total"] = num;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Sales_Order", "VoucherID", dataRow["SysDocID"].ToString(), text3, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row2 in salesOrderData.SalesOrderDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					string text4 = row2["ProductID"].ToString();
					string text5 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text4, sqlTransaction);
					if (fieldValue != null)
					{
						text5 = fieldValue.ToString();
					}
					if (text5 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text5)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text4, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text4 + "\nUnit:" + row2["UnitID"].ToString());
						float num3 = float.Parse(obj2["Factor"].ToString());
						string text6 = obj2["FactorType"].ToString();
						float num4 = float.Parse(row2["Quantity"].ToString());
						row2["UnitFactor"] = num3;
						row2["FactorType"] = text6;
						row2["UnitQuantity"] = row2["Quantity"];
						num4 = ((!(text6 == "M")) ? float.Parse(Math.Round(num4 * num3, 5).ToString()) : float.Parse(Math.Round(num4 / num3, 5).ToString()));
						row2["Quantity"] = num4;
					}
				}
				insertUpdateSalesOrderCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(salesOrderData, "Sales_Order", insertUpdateSalesOrderCommand)) : (flag & Insert(salesOrderData, "Sales_Order", insertUpdateSalesOrderCommand)));
				if (flag2)
				{
					if (isUpdate)
					{
						flag &= UpdateSODetails(salesOrderData, sqlTransaction);
					}
					flag &= InsertSODetails(salesOrderData, sqlTransaction);
				}
				insertUpdateSalesOrderCommand = GetInsertUpdateSalesOrderDetailsCommand(isUpdate: false);
				insertUpdateSalesOrderCommand.Transaction = sqlTransaction;
				if (isUpdate && !flag2)
				{
					flag &= DeleteSalesOrderDetailsRows(sysDocID, text3, sqlTransaction);
				}
				if (salesOrderData.Tables["Sales_Order_Detail"].Rows.Count > 0 && !flag2)
				{
					flag &= Insert(salesOrderData, "Sales_Order_Detail", insertUpdateSalesOrderCommand);
				}
				if (!flag)
				{
					return flag;
				}
				if (itemSourceTypes == ItemSourceTypes.None)
				{
					text2 = salesOrderData.SalesOrderDetailTable.Rows[0]["SourceVoucherID"].ToString();
					text = salesOrderData.SalesOrderDetailTable.Rows[0]["SourceSysDocID"].ToString();
					if (text2 != "")
					{
						flag &= new SalesQuote(base.DBConfig).UpdateSalesQuoteStatus(text, text2, sqlTransaction);
					}
				}
				foreach (DataRow row3 in salesOrderData.SalesOrderDetailTable.Rows)
				{
					string productID = row3["ProductID"].ToString();
					float num5 = float.Parse(row3["Quantity"].ToString());
					float quantity = new Products(base.DBConfig).GetReservedQuantity(productID, sqlTransaction) + num5;
					flag &= new Products(base.DBConfig).UpdateReservedQuantity(productID, quantity, sqlTransaction);
				}
				flag &= InsertUpdateProductLotReserveDetail(salesOrderData, isUpdate: false, sqlTransaction);
				if (!isUpdate && salesOrderData.Tables.Contains("Job"))
				{
					JobData jobData = new JobData();
					jobData.Merge(salesOrderData.Tables["Job"]);
					flag &= new Job(base.DBConfig).InsertJob(jobData);
				}
				if (salesOrderData.Tables.Contains("Tax_Detail") && salesOrderData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(salesOrderData, sysDocID, text3, isUpdate, sqlTransaction);
				}
				if (bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.MaterialReservationOnSo, false).ToString()))
				{
					flag &= new Reservation(base.DBConfig).InsertUpdateReservation(salesOrderData, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Sales_Order", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Sales Order";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text3, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text3, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Sales_Order", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				if (!flag3)
				{
					new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SalesOrder, sysDocID, text3, "Sales_Order", sqlTransaction);
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ExportSalesOrder, sysDocID, text3, "Sales_Order", sqlTransaction);
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

		public bool UpdateSODetails(DataSet salesOrderData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				GetInsertUpdateSalesOrderDetailsCommand(isUpdate: true).Transaction = sqlTransaction;
				string text = "IsNonEdit = 'True'";
				string sort = "RowIndex ASC";
				salesOrderData.Tables["Sales_Order_Detail"].Select(text, sort).CopyToDataTable();
				DataTable dataTable = new DataView(salesOrderData.Tables["Sales_Order_Detail"])
				{
					RowFilter = text
				}.ToTable();
				if (dataTable.Rows.Count > 0)
				{
					new DataSet();
					new DataTable("Sales_Order_Detail".ToString());
					{
						foreach (DataRow row in dataTable.Rows)
						{
							string text2 = "";
							string text3 = "";
							string text4 = "";
							string text5 = "";
							string text6 = "";
							string text7 = "";
							string text8 = "";
							string text9 = "";
							string text10 = "";
							string text11 = "";
							string text12 = "";
							string text13 = "";
							string text14 = "";
							string text15 = "";
							string text16 = "";
							string text17 = "";
							decimal num = default(decimal);
							decimal result = default(decimal);
							decimal result2 = default(decimal);
							decimal result3 = default(decimal);
							decimal result4 = default(decimal);
							decimal result5 = default(decimal);
							decimal result6 = default(decimal);
							decimal result7 = default(decimal);
							decimal result8 = default(decimal);
							decimal result9 = default(decimal);
							int result10 = 0;
							text2 = row["ProductID"].ToString();
							num = decimal.Parse(row["Quantity"].ToString());
							text3 = row["Description"].ToString();
							text4 = row["Remarks"].ToString();
							text5 = row["UnitID"].ToString();
							decimal.TryParse(row["UnitFactor"].ToString(), out result9);
							text6 = row["FactorType"].ToString();
							text7 = row["LocationID"].ToString();
							text8 = row["SourceSysDocID"].ToString();
							text9 = row["SourceVoucherID"].ToString();
							decimal.TryParse(row["SourceRowIndex"].ToString(), out result8);
							int.TryParse(row["TaxOption"].ToString(), out result10);
							text14 = row["TaxGroupID"].ToString();
							text15 = row["SpecificationID"].ToString();
							text16 = row["StyleID"].ToString();
							text17 = row["JobID"].ToString();
							text10 = row["CostCategoryID"].ToString();
							decimal.TryParse(row["UnitPrice"].ToString(), out result);
							decimal.TryParse(row["Cost"].ToString(), out result2);
							decimal.TryParse(row["UnitQuantity"].ToString(), out result3);
							decimal.TryParse(row["SubunitPrice"].ToString(), out result4);
							decimal.TryParse(row["TaxPercentage"].ToString(), out result5);
							decimal.TryParse(row["TaxAmount"].ToString(), out result6);
							decimal.TryParse(row["Discount"].ToString(), out result7);
							decimal? num2 = null;
							object value = null;
							if (result3 == 0m)
							{
								result3 = Convert.ToDecimal(value);
							}
							if (result7 == 0m)
							{
								result7 = num2.GetValueOrDefault(0m);
							}
							if (result4 == 0m)
							{
								result7 = num2.GetValueOrDefault(0m);
							}
							if (result2 == 0m)
							{
								result2 = num2.GetValueOrDefault(0m);
							}
							text11 = row["SysDocID"].ToString();
							text12 = row["VoucherID"].ToString();
							text13 = row["RowIndex"].ToString();
							string commandText = ("UPDATE Sales_Order_Detail SET ProductID = '" + text2 + "',Quantity =" + num + ",UnitPrice = " + result + ",Description = '" + text3 + "',Remarks = '" + text4 + "',UnitID = '" + text5 + "',FactorType = '" + text6 + "',LocationID = '" + text7 + "',SourceSysDocID ='" + text8 + "',SourceVoucherID ='" + text9 + "',SourceRowIndex = " + result8 + ",TaxOption = " + result10 + ",TaxGroupID ='" + text14 + "',TaxPercentage = " + result5 + ",TaxAmount = " + result6 + ",SpecificationID = '" + text15 + "',StyleID = '" + text16 + "',JobID = '" + text17 + "',CostCategoryID = '" + text10 + "' WHERE SysDocID = '" + text11 + "' AND VoucherID = '" + text12 + "' AND RowIndex = " + text13) ?? "";
							flag &= Update(commandText, sqlTransaction);
						}
						return flag;
					}
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertSODetails(DataSet salesOrderData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				SqlCommand insertUpdateSalesOrderDetailsCommand = GetInsertUpdateSalesOrderDetailsCommand(isUpdate: false);
				insertUpdateSalesOrderDetailsCommand.Transaction = sqlTransaction;
				string text = "IsNonEdit = 'False'";
				string sort = "RowIndex ASC";
				DataTable dataTable = new DataTable();
				DataTable dataTable2 = new DataView(salesOrderData.Tables["Sales_Order_Detail"])
				{
					RowFilter = text
				}.ToTable();
				if (dataTable2.Rows.Count <= 0)
				{
					return true;
				}
				dataTable = salesOrderData.Tables["Sales_Order_Detail"].Select(text, sort).CopyToDataTable();
				if (dataTable.Rows.Count > 0)
				{
					DataSet dataSet = new DataSet();
					dataTable.TableName = "Sales_Order_Detail";
					dataTable.Copy();
					dataSet.Tables.Add(dataTable2);
					return flag & Insert(dataSet, "Sales_Order_Detail", insertUpdateSalesOrderDetailsCommand);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertUpdateProductLotReserveDetail(DataSet data, bool isUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				bool flag = true;
				SqlCommand insertUpdateProductLotReserveDetailCommand = GetInsertUpdateProductLotReserveDetailCommand(isUpdate: false);
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				insertUpdateProductLotReserveDetailCommand.Transaction = sqlTransaction;
				string text = "";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				if (data.Tables.Contains("Sales_Order_ProductLot_Detail") && data.Tables["Sales_Order_ProductLot_Detail"].Rows.Count > 0)
				{
					foreach (DataRow row in data.Tables["Sales_Order_ProductLot_Detail"].Rows)
					{
						text4 = row["ProductID"].ToString();
						text3 = row["LotNumber"].ToString();
						DataSet productAvailableLotsAndBins = new Products(base.DBConfig).GetProductAvailableLotsAndBins(text4, text3);
						if (productAvailableLotsAndBins == null || productAvailableLotsAndBins.Tables.Count == 0 || productAvailableLotsAndBins.Tables[0].Rows.Count == 0)
						{
							text = ((!string.IsNullOrEmpty(text)) ? (text + ",'" + text3 + "'") : (text + "'" + text3 + "'"));
							text2 = ((!string.IsNullOrEmpty(text2)) ? (text2 + ",'" + text4 + "'") : (text2 + "'" + text4 + "'"));
						}
					}
					_ = (text != "");
					flag &= Insert(data, "Sales_Order_ProductLot_Detail", insertUpdateProductLotReserveDetailCommand);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		private string GetInsertUpdateProductLotReserveDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Order_ProductLot_Detail", new FieldValue("LotNumber", "@LotNumber"), new FieldValue("SourceLotNumber", "@SourceLotNumber"), new FieldValue("Reference", "@Reference"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("BinID", "@BinID"), new FieldValue("RackID", "@RackID"), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("SoldQty", "@SoldQty"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Cost", "@Cost"), new FieldValue("Reference2", "@Reference2"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProductLotReserveDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateProductLotReserveDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateProductLotReserveDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@LotNumber", SqlDbType.NVarChar);
			parameters.Add("@SourceLotNumber", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@BinID", SqlDbType.NVarChar);
			parameters.Add("@RackID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@SoldQty", SqlDbType.Decimal);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Cost", SqlDbType.Decimal);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters["@LotNumber"].SourceColumn = "LotNumber";
			parameters["@SourceLotNumber"].SourceColumn = "SourceLotNumber";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@BinID"].SourceColumn = "BinID";
			parameters["@RackID"].SourceColumn = "RackID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@SoldQty"].SourceColumn = "SoldQty";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Reference2"].SourceColumn = "Reference2";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public SalesOrderData GetSalesOrderByID(string sysDocID, string voucherID)
		{
			try
			{
				SalesOrderData salesOrderData = new SalesOrderData();
				string textCommand = "SELECT * FROM Sales_Order WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesOrderData, "Sales_Order", textCommand);
				if (salesOrderData == null || salesOrderData.Tables.Count == 0 || salesOrderData.Tables["Sales_Order"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID,Product.IsTrackSerial,Product.IsTrackLot,ISNull(Product.TaxGroupID,PC.TaxGroupID) AS TaxGroupID,\r\n                    T.SysDocID AS PKSysDocID,T.VoucherID AS PKVoucherID,'True' AS IsNonEdit,Isnull(TD.QuantityShipped,0) as Shipped \r\n                        FROM Sales_Order_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        LEFT OUTER JOIN Product_Class PC ON PC.ClassID = Product.ClassID\r\n                            LEFT OUTER JOIN \r\n\t\t\t\t\t\t\t\t(SELECT TOP 1 DND.SysDocID,DND.VoucherID,DND.SourceSysDocID,DND.SourceVoucherID FROM Delivery_Note_Detail DND\r\n                                WHERE  DND.SourceSysDocID = '" + sysDocID + "' AND   DND.SourceVoucherID = '" + voucherID + "') T  \r\n\t\t\t\t\t\t\t\tON T.SourceSysDocID = TD.SysDocID AND T.SourceVoucherID = TD.VoucherID \r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(salesOrderData, "Sales_Order_Detail", textCommand);
				DataSet transactionReserveProductLots = GetTransactionReserveProductLots(sysDocID, voucherID);
				if (salesOrderData.Tables.Contains("Sales_Order_ProductLot_Detail"))
				{
					salesOrderData.Tables.Remove("Sales_Order_ProductLot_Detail");
				}
				salesOrderData.Merge(transactionReserveProductLots, preserveChanges: false);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesOrderData, "Tax_Detail", textCommand);
				textCommand = "SELECT DISTINCT SQD.VoucherID,SQD.SysDocID FROM Sales_Quote_Detail SQD INNER JOIN Sales_Order_Detail SOD ON SQD.VoucherID=SOD.SourceVoucherID AND SQD.SysDocID=SOD.SourceSysDocID WHERE SOD.VoucherID='" + voucherID + "' AND SOD.SysDocID='" + sysDocID + "'";
				FillDataSet(salesOrderData, "Sales_Quote_Detail", textCommand);
				textCommand = "SELECT DISTINCT  dnd.VoucherID,dnd.SysDocID from Sales_order SO INNER JOIN Delivery_Note_Detail dnd ON SO.SysDocID=dnd.SourceSysDocID AND SO.VoucherID=dnd.SourceVoucherID WHERE SO.VoucherID='" + voucherID + "' AND SO.SysDocID='" + sysDocID + "'";
				FillDataSet(salesOrderData, "Delivery_Note_Detail", textCommand);
				textCommand = "SELECT DISTINCT  EPD.VoucherID,EPD.SysDocID from Sales_order SO INNER JOIN Export_PickList_Detail EPD ON SO.SysDocID=EPD.SourceSysDocID AND SO.VoucherID=EPD.SourceVoucherID WHERE SO.VoucherID='" + voucherID + "' AND SO.SysDocID='" + sysDocID + "'";
				FillDataSet(salesOrderData, "Export_PickList_Detail", textCommand);
				textCommand = "SELECT DISTINCT SI.VoucherID,SI.SysDocID from Sales_Invoice_Detail SI INNER JOIN Sales_order SO ON SO.SysDocID=SI.OrderSysDocID AND SO.VoucherID=SI.OrderVoucherID WHERE SO.VoucherID='" + voucherID + "' AND SO.SysDocID='" + sysDocID + "'";
				FillDataSet(salesOrderData, "Sales_Invoice_Detail", textCommand);
				return salesOrderData;
			}
			catch
			{
				throw;
			}
		}

		public SalesOrderData GetPurchaseFromSalesOrderByID(string sysDocID, string voucherID)
		{
			try
			{
				SalesOrderData salesOrderData = new SalesOrderData();
				string textCommand = "SELECT * FROM Sales_Order WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesOrderData, "Sales_Order", textCommand);
				if (salesOrderData == null || salesOrderData.Tables.Count == 0 || salesOrderData.Tables["Sales_Order"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT Isnull(TD.Quantity,0)-Isnull(POD.QuantityShipped1,0) as Quantity,TD.*,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID,Product.IsTrackSerial,Product.IsTrackLot,ISNull(Product.TaxGroupID,PC.TaxGroupID) AS TaxGroupID,\r\n                    T.SysDocID AS PKSysDocID,T.VoucherID AS PKVoucherID,'True' AS IsNonEdit,Isnull(TD.QuantityShipped,0) as Shipped \r\n                        FROM Sales_Order_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        LEFT OUTER JOIN Product_Class PC ON PC.ClassID = Product.ClassID\r\n                            LEFT OUTER JOIN \r\n\t\t\t\t\t\t\t\t(SELECT TOP 1 DND.SysDocID,DND.VoucherID,DND.SourceSysDocID,DND.SourceVoucherID FROM Delivery_Note_Detail DND\r\n                                WHERE  DND.SourceSysDocID = '" + sysDocID + "' AND   DND.SourceVoucherID = '" + voucherID + "') T  \r\n\t\t\t\t\t\t\t\tON T.SourceSysDocID = TD.SysDocID AND T.SourceVoucherID = TD.VoucherID \r\n                        LEFT JOIN (SELECT SourceSysDocID,SourceVoucherID,Isnull(Sum(Quantity),0) as QuantityShipped1,ProductID,SourceRowIndex from  Purchase_Order_Detail POD Group by SourceSysDocID,SourceVoucherID,ProductID,SourceRowIndex) \r\n                        POD ON POD.SourceVoucherID=TD.VoucherID and POD.SourceSysDocID=TD.SysDocID AND POD.ProductID=TD.ProductID AND POD.SourceRowIndex=TD.RowIndex\r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "' AND (Isnull(TD.Quantity,0)-Isnull(POD.QuantityShipped1,0))>0 ORDER BY TD.RowIndex ";
				FillDataSet(salesOrderData, "Sales_Order_Detail", textCommand);
				return salesOrderData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTransactionReserveProductLots(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT PL.Reference AS LotReference,PL.LotNumber,PL.LocationID,PLD.*,PL.ProductionDate,PL.ExpiryDate,PL.ReceiptDate,\r\n                        CASE WHEN PL.SourceLotNumber IS NULL THEN PL.ReceiptNumber ELSE (SELECT ReceiptNumber FROM Product_Lot PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END  AS Consign#\r\n                       FROM Sales_Order_ProductLot_Detail PLD INNER JOIN Product_Lot PL ON PL.LotNumber = PLD.LotNumber  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Sales_Order_ProductLot_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetReservationDetails(string sysDocID, string voucherID, string productID, int rowIndex)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "select  sum (Quantity) AS Qty,LotNumber from Reservation_Detail where SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND ProductID='" + productID + "' AND OrderRowIndex=" + rowIndex + " Group by LotNumber, Quantity ";
			FillDataSet(dataSet, "Reservation", textCommand);
			if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
			{
				return null;
			}
			return dataSet;
		}

		public DataSet GetOpenOrdersSummary(string customerID, bool isExport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer],SO.PONumber AS [Customer PO#], J.JobName FROM Sales_Order SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID \r\n\t\t\t\t\t\t\t LEFT JOIN JOB J ON SO.JOBID=j.JobID\r\n\t\t\t\t\t\t\t  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsExport,'False')= '" + isExport.ToString() + "' AND SO.Status=1 ";
				if (customerID != "")
				{
					text = text + " AND SO.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "Sales_Order", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenOrdersForPurchase(bool isExport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "\r\n\t\t\t\t\t\t\tSELECT  SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer], J.JobName FROM Sales_Order SO\r\n\t\t\t\t\t\t\tLEFT JOIN(SELECT DISTINCT SOD.SysDocID,SOD.VoucherID FROM Sales_Order_Detail SOD INNER JOIN Purchase_Order_Detail POD ON SOD.VoucherID=POD.SourceVoucherID AND SOD.SysDocID=POD.SourceSysDocID  GROUP BY SOD.SysDocID,SOD.VoucherID HAVING SUM(SOD.Quantity)=SUM(POD.Quantity))as OD on SO.VoucherID=OD.VoucherID AND SO.SysDocID=OD.SysDocID\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID \r\n\t\t\t\t\t\t\t LEFT JOIN JOB J ON SO.JOBID=j.JobID Where OD.SysDocID IS NULL AND ISNULL(SO.IsVoid,'False')='False' AND ISNULL(SO.IsExport,'False')= '" + isExport.ToString() + "' AND SO.Status=1 ";
				FillDataSet(dataSet, "Sales_Order", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowShippedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityShipped FROM Sales_Order_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
					float.TryParse(dataRow["QuantityShipped"].ToString(), out result2);
				}
				result2 += quantity;
				textCommand = "UPDATE Sales_Order_Detail SET QuantityShipped=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool CloseShippedOrder(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM Sales_Order_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                                AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Sales_Order_Detail SOD2 WHERE SOD.SysDocID=SOD2.SysDocID AND SOD.VoucherID=SOD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityShipped,0) ) FROM Sales_Order_Detail SOD3 WHERE SOD.SysDocID=SOD3.SysDocID AND SOD.VoucherID=SOD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Sales_Order SET Status= 2 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool ReOpenOrder(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM Sales_Order_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                                AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Sales_Order_Detail SOD2 WHERE SOD.SysDocID=SOD2.SysDocID AND SOD.VoucherID=SOD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityShipped,0) ) FROM Sales_Order_Detail SOD3 WHERE SOD.SysDocID=SOD3.SysDocID AND SOD.VoucherID=SOD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) > 0)
				{
					return true;
				}
				exp = "UPDATE Sales_Order SET Status= 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool DeleteSalesOrderDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				SalesOrderData salesOrderData = new SalesOrderData();
				string textCommand = "SELECT SOD.*,ISVOID,IsExport FROM Sales_Order_Detail SOD INNER JOIN Sales_Order SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(salesOrderData, "Sales_Order_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(salesOrderData.SalesOrderDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(salesOrderData.SalesOrderDetailTable.Rows[0]["IsExport"].ToString(), out result2);
				if (result2)
				{
					textCommand = "SELECT *  FROM Export_PickList_Detail SOD \r\n                              WHERE SOD.SourceSysDocID = '" + sysDocID + "' AND SOD.SourceVoucherID = '" + voucherID + "'";
					FillDataSet(salesOrderData, "Export_PickList_Detail", textCommand, sqlTransaction);
					if (salesOrderData != null && salesOrderData.Tables.Count > 0 && salesOrderData.Tables["Export_PickList_Detail"].Rows.Count > 0)
					{
						throw new CompanyException("Some items in this transaction has been already shipped. You are not able to modify.", 1047);
					}
				}
				if (!result)
				{
					foreach (DataRow row in salesOrderData.SalesOrderDetailTable.Rows)
					{
						string productID = row["ProductID"].ToString();
						float num = float.Parse(row["Quantity"].ToString());
						float num2 = new Products(base.DBConfig).GetReservedQuantity(productID, sqlTransaction) - num;
						if (num2 < 0f)
						{
							num2 = 0f;
						}
						flag &= new Products(base.DBConfig).UpdateReservedQuantity(productID, num2, sqlTransaction);
					}
				}
				textCommand = "DELETE FROM Sales_Order_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Sales_Order_ProductLot_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				flag &= new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				flag &= ReOpenOrder(sysDocID, voucherID, sqlTransaction);
				if (bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.MaterialReservationOnSo, false).ToString()))
				{
					return flag & new Reservation(base.DBConfig).DeleteReservationDetailsRows(sysDocID, voucherID, isDeletingTransaction: false, sqlTransaction);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool VoidSalesOrder(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				SalesOrderData salesOrderData = new SalesOrderData();
				string textCommand = "SELECT * FROM Sales_Order_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(salesOrderData, "Sales_Order_Detail", textCommand, sqlTransaction);
				foreach (DataRow row in salesOrderData.SalesOrderDetailTable.Rows)
				{
					string productID = row["ProductID"].ToString();
					float num = float.Parse(row["Quantity"].ToString());
					float num2 = new Products(base.DBConfig).GetReservedQuantity(productID, sqlTransaction) - num;
					if (num2 < 0f)
					{
						num2 = 0f;
					}
					flag &= new Products(base.DBConfig).UpdateReservedQuantity(productID, num2, sqlTransaction);
				}
				textCommand = "UPDATE Sales_Order SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Sales Order", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteSalesOrder(string sysDocID, string voucherID, bool IsjobID)
		{
			bool flag = true;
			try
			{
				string text = "";
				string sysDocID2 = "";
				string text2 = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				SalesOrderData salesOrderData = new SalesOrderData();
				string textCommand = "SELECT SourceSysDocID, SourceVoucherID FROM Sales_Order_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(salesOrderData, "Sales_Order_Detail", textCommand, sqlTransaction);
				if (salesOrderData != null || salesOrderData.Tables.Count != 0 || salesOrderData.Tables["Sales_Order"].Rows.Count != 0)
				{
					foreach (DataRow row in salesOrderData.SalesOrderDetailTable.Rows)
					{
						sysDocID2 = row["SourceSysDocID"].ToString();
						text2 = row["SourceVoucherID"].ToString();
					}
				}
				flag &= DeleteSalesOrderDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Sales_Order WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (flag && text2 != "")
				{
					flag &= new SalesQuote(base.DBConfig).ReactivateSalesQuote(sysDocID2, text2, sqlTransaction);
				}
				if (IsjobID)
				{
					text = "DELETE FROM Job WHERE JobID='" + sysDocID.Trim() + "-" + voucherID.Trim() + "'";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				}
				if (bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.MaterialReservationOnSo, false).ToString()))
				{
					flag &= new Reservation(base.DBConfig).DeleteReservation(sysDocID, voucherID);
				}
				flag &= new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Sales Order", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetOpenOrderListReport()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID,VoucherID,SO.CustomerID,CustomerName,TransactionDate,SO.SalespersonID,Total\r\n                            FROM Sales_Order SO INNER JOIN Customer ON SO.CustomerID=Customer.CustomerID\r\n                            WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
			FillDataSet(dataSet, "Orders", textCommand);
			return dataSet;
		}

		public DataSet GetSalesOrderToPrint(string sysDocID, string voucherID)
		{
			return GetSalesOrderToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetSalesOrderToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.CustomerID,CustomerName,Customer.TaxIDNumber as CTaxIDNo,CustomerAddress,TransactionDate,CA.ContactName,\r\n                                CASE SI.ApprovalStatus when 1 THEN 'PENDING' WHEN 2 THEN 'WAITING' WHEN 3 THEN 'REJECTED' WHEN 10 THEN 'APPROVED' ELSE 'NA' END AS [APPROVAL STATUS] ,\r\n                                (SELECT TOP 1 ApproverID FROM Approval_Task AT WHERE AT.DocumentSysDocID=SysDocID AND AT.DocumentCode=VoucherID ORDER BY AT.DateApproved DESC) AS [Approved By],\r\n                                SI.SalesPersonID,SP.FullName,SP.Phone1 AS [SP Mob],SP.Note AS [SP Des],RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,IsVoid,SI.Reference,Discount AS Discount,ISNULL(Total,0) - ISNULL(Discount,0) AS GrandTotal,\r\n                                ISNULL(TaxAmount ,0) AS Tax,ISNULL(RoundOff,0) as RoundOff, Total AS Total,SI.PONumber,SI.Note,J.JobName,JC.CostCategoryName,CA.Phone1,CA.Fax,SI.ShipToAddress,\r\n                                (select DISTINCT SQ.Remarks1 from Sales_Quote SQ LEFT OUTER JOIN Sales_Order_Detail SOD ON SQ.SysDocID=SOD.SourceSysDocID and SQ.VoucherID=SOD.SourceVoucherID where SOD.SysDocID=SI.SysDocID AND SOD.VoucherID=SI.VoucherID) AS QuoteRemarks1,\r\n                                (select DISTINCT SQ.Remarks2 from Sales_Quote SQ LEFT OUTER JOIN Sales_Order_Detail SOD ON SQ.SysDocID=SOD.SourceSysDocID and SQ.VoucherID=SOD.SourceVoucherID where SOD.SysDocID=SI.SysDocID AND SOD.VoucherID=SI.VoucherID) AS QuoteRemarks2\r\n                                , SI.PayeeTaxGroupID, TG.TaxGroupName,Customer.ShortName, SI.CreatedBy,J.Note [Job Note] \r\n                                FROM  Sales_Order SI LEFT JOIN Approval_Task AT ON SI.SysDocID=AT.DocumentCode AND SI.VoucherID=AT.DocumentCode \r\n                                INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT JOIN Job J ON J.JobID=SI.JobID\r\n                                LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=SI.CostCategoryID\r\n                                LEFT JOIN Salesperson SP ON SP.SalespersonID=SI.SalespersonID\r\n                                LEFT OUTER JOIN Tax_Group TG ON SI.PayeeTaxGroupID=TG.TaxGroupID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Sales_Order", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Sales_Order"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,SOD.ProductID,SOD.Description, ISNULL(UnitQuantity,SOD.Quantity) AS Quantity,SOD.TaxAmount, SOD.TaxGroupID,TG.TaxGroupName,\r\n                        P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,P.Photo,P.Description2,SOD.Remarks,\r\n                        SOD.UnitPrice AS UnitPrice,\r\n                        ISNULL(UnitQuantity,SOD.Quantity)*(UnitPrice- ISNULL(SOD.Discount,0))  AS Total,SOD.UnitID,J.JobName,JC.CostCategoryName ,ISNULL(SOD.Discount,0) AS Discount, PB.BrandName, SOD.SpecificationID, SpecificationName,C.CountryName as Origin\r\n                        ,PST.StyleName [Style],SOD.RefSlNo,SOD.RefText1,SOD.RefText2,SOD.RefNum1,SOD.RefNum2,SOD.RefDate1,SOD.RefDate2 ,\r\n                        P.ClassID, PC.ClassName, P.CategoryID, PG.CategoryName, P.ManufacturerID, PM.ManufacturerName, P.UPC,PU.UnitName\r\n                        FROM   Sales_Order_Detail SOD\r\n                        INNER JOIN Product P ON SOD.ProductID = P.ProductID\r\n                        LEFT OUTER JOIN Product_Brand PB ON P.BrandID=PB.BrandID \r\n                        LEFT JOIN Job J ON J.JobID=SOD.JobID\r\n                        LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=SOD.CostCategoryID\r\n                        LEFT JOIN Tax_Group TG ON SOD.TaxGroupID=TG.TaxGroupID\r\n                        LEFT OUTER JOIN Product_Specification PS ON PS.SpecificationID=SOD.SpecificationID\r\n                        LEFT JOIN Country C ON C.CountryID=P.Origin\r\n                        LEFT JOIN Product_Style PST ON PST.StyleID=SOD.StyleID\r\n                        LEFT OUTER JOIN Product_Class PC On P.ClassID=PC.ClassID\r\n                        LEFT OUTER JOIN Product_Category PG On P.CategoryID=PG.CategoryID\r\n                        LEFT OUTER JOIN Product_Manufacturer PM ON P.ManufacturerID=PM.ManufacturerID\r\n                        LEFT OUTER JOIN Unit PU ON PU.UnitID=SOD.UnitID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") ORDER BY RowIndex";
				FillDataSet(dataSet, "Sales_Order_Detail", cmdText);
				DataSet transactionReserveProductLots = GetTransactionReserveProductLots(sysDocID, voucherID.ToString());
				if (dataSet.Tables.Contains("Sales_Order_ProductLot_Detail"))
				{
					dataSet.Tables.Remove("Sales_Order_ProductLot_Detail");
				}
				dataSet.Merge(transactionReserveProductLots, preserveChanges: false);
				dataSet.Relations.Add("CustomerOrder", new DataColumn[2]
				{
					dataSet.Tables["Sales_Order"].Columns["SysDocID"],
					dataSet.Tables["Sales_Order"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Sales_Order_Detail"].Columns["SysDocID"],
					dataSet.Tables["Sales_Order_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Sales_Order"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Sales_Order"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Discount"].ToString(), out result2);
					decimal.TryParse(row["Tax"].ToString(), out result3);
					row["TotalInWords"] = NumToWord.GetNumInWords(decimalPoints: new CompanyInformations(base.DBConfig).CurrencyDecimalPoints, amount: result - result2 + result3);
				}
				if (bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.ShowLotdetailinPrintout, false).ToString()))
				{
					cmdText = "SELECT PL.*,PLS.SoldQty,PLS.LotNumber FROM Product_Lot PL LEFT JOIN Sales_Order_ProductLot_Detail PLS ON PL.LotNumber=PLS.LotNumber WHERE PLS.SysDocID=\r\n                            '" + sysDocID + "' AND PLS.VoucherID IN (" + text + ")";
					FillDataSet(dataSet, "ProductLot", cmdText);
					dataSet.Relations.Add("ProductLotRel", new DataColumn[1]
					{
						dataSet.Tables["Sales_Order_Detail"].Columns["ProductID"]
					}, new DataColumn[1]
					{
						dataSet.Tables["ProductLot"].Columns["ItemCode"]
					}, createConstraints: false);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool isExport, bool showVoid, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Order Date],INV.CurrencyID [Currency],\r\n                            INV.SalespersonID [Salesperson],J.JobID,J.JobName,Total - ISNULL(Discount,0) AS [Amount],ISNULL((CASE INV.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' END) ,(CASE Customer.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' END))AS TAXOPTION, INV.TaxAmount\r\n                            FROM     Sales_Order INV\r\n                            LEFT JOIN Job J ON INV.JobID=J.JobID\r\n                           LEFT JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "' AND ISNULL(IsExport,'False')= '" + isExport.ToString() + "'";
			}
			if (sysDocID != "")
			{
				text3 = text3 + " AND INV.SysDocID = '" + sysDocID + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Sales_Order", sqlCommand);
			return dataSet;
		}

		public bool OrderHasShippedQuantity(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Sales_Order_Detail SOD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityShipped,0))>0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}

		public bool IsPOOrder(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Sales_Order_Detail \r\n                                WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(Quantity,0))>0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}

		public DataSet GetSalesOrderDetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool openOrdersonly, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "SELECT DISTINCT SO.TransactionDate,SO.VoucherID,C.CustomerName,J.JobName,SOD.UnitPrice,SOD.ProductID,SOD.UnitID,SOD.Description,ISNULL(SOD.UnitQuantity,SOD.Quantity),\r\n                                SOD.QuantityShipped,SOD.UnitPrice\r\n                                FROM Sales_Order SO LEFT JOIN Sales_Order_Detail SOD ON SO.SysDocID=SOD.SysDocID AND SO.VoucherID=SOD.VoucherID\r\n                                LEFT JOIN JOB J ON J.JobID=SO.JobID LEFT JOIN Customer C ON C.CustomerID=SO.CustomerID LEFT JOIN Product P ON SOD.ProductID=P.ProductID\r\n                               \r\n                                LEFT JOIN Product ON SOD.ProductID=Product.ProductID \r\n                                     ";
				text3 = text3 + "WHERE SO.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND ISNULL(SO.IsVoid,'False')='False' ";
				if (jobID != "")
				{
					text3 = text3 + " AND SO.JobID='" + jobID + "'";
				}
				if (vendorID != "")
				{
					text3 = text3 + " AND SO.CustomerID= '" + vendorID + "'";
				}
				if (fromItem != "")
				{
					text3 = text3 + " AND SOD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND SOD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND SOD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND SOD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND SOD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND SOD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND SOD.ProductID IN (SELECT ProductID FROM Product WHERE BrandId BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (openOrdersonly)
				{
					text3 += " AND SO.Status=1";
				}
				FillDataSet(dataSet, "Sales_Order", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, int status)
		{
			try
			{
				string exp = "UPDATE Sales_Order SET Status= " + status + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}
	}
}
