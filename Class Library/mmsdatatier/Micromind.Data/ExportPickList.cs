using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Micromind.Data
{
	public sealed class ExportPickList : StoreObject
	{
		private const string EXPORTPICKLIST_TABLE = "Export_PickList";

		private const string EXPORTPICKLISTDETAIL_TABLE = "Export_PickList_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string SALESFLOW_PARM = "@SalesFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ISEXPORT_PARM = "@IsExport";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string BILLINGADDRESSID_PARM = "@BillingAddressID";

		private const string SHIPTOADDRESS_PARM = "@ShipToAddress";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string TOTAL_PARM = "@Total";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

		private const string DRIVERID_PARM = "@DriverID";

		private const string VEHICLEID_PARM = "@VehicleID";

		private const string CLUSERID_PARM = "@CLUserID";

		private const string PORT_PARM = "@Port";

		private const string CONTAINERNO_PARM = "@ContainerNumber";

		private const string CONTAINERSIZEID_PARM = "@ContainerSizeID";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string INVOICESYSDOCID_PARM = "@InvoiceSysDocID";

		private const string INVOICEVOUCHERID_PARM = "@InvoiceVoucherID";

		private const string DNOTESYSDOCID_PARM = "@DNoteSysDocID";

		private const string DNOTEVOUCHERID_PARM = "@DNoteVoucherID";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string ORDERVOUCHERID_PARM = "@OrderVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string ROWSOURCE_PARM = "@RowSource";

		private const string REMARKS_PARM = "@Remarks";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ExportPickList(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateExportPickListText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Export_PickList", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("SalesFlow", "@SalesFlow"), new FieldValue("IsExport", "@IsExport"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("BillingAddressID", "@BillingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("ShipToAddress", "@ShipToAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Discount", "@Discount"), new FieldValue("Total", "@Total"), new FieldValue("PONumber", "@PONumber"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("Note", "@Note"), new FieldValue("CLUserID", "@CLUserID"), new FieldValue("Port", "@Port"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("DriverID", "@DriverID"), new FieldValue("VehicleID", "@VehicleID"), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("ContainerSizeID", "@ContainerSizeID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Export_PickList", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateExportPickListCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateExportPickListText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateExportPickListText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@BillingAddressID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@ShipToAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters.Add("@DriverID", SqlDbType.NVarChar);
			parameters.Add("@VehicleID", SqlDbType.NVarChar);
			parameters.Add("@ContainerNumber", SqlDbType.NVarChar);
			parameters.Add("@ContainerSizeID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CLUserID", SqlDbType.NVarChar);
			parameters.Add("@Port", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@IsExport"].SourceColumn = "IsExport";
			parameters["@SalesFlow"].SourceColumn = "SalesFlow";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@BillingAddressID"].SourceColumn = "BillingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@ShipToAddress"].SourceColumn = "ShipToAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@DriverID"].SourceColumn = "DriverID";
			parameters["@VehicleID"].SourceColumn = "VehicleID";
			parameters["@ContainerNumber"].SourceColumn = "ContainerNumber";
			parameters["@ContainerSizeID"].SourceColumn = "ContainerSizeID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CLUserID"].SourceColumn = "CLUserID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@Port"].SourceColumn = "Port";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
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

		private string GetInsertUpdateExportPickListDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Export_PickList_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("RowSource", "@RowSource"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("JobID", "@JobID"), new FieldValue("Remarks", "@Remarks"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateExportPickListDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateExportPickListDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateExportPickListDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@RowSource", SqlDbType.Int);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@RowSource"].SourceColumn = "RowSource";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateInvoiceDNoteText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Invoice_DNote", new FieldValue("InvoiceSysDocID", "@InvoiceSysDocID"), new FieldValue("InvoiceVoucherID", "@InvoiceVoucherID"), new FieldValue("DNoteSysDocID", "@DNoteSysDocID"), new FieldValue("DNoteVoucherID", "@DNoteVoucherID"), new FieldValue("SourceDocType", "@SourceDocType"));
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInvoiceDNoteCommand(bool isUpdate)
		{
			if (insertCommand != null)
			{
				insertCommand = null;
			}
			insertCommand = new SqlCommand(GetInsertUpdateInvoiceDNoteText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@InvoiceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@InvoiceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@DNoteSysDocID", SqlDbType.NVarChar);
			parameters.Add("@DNoteVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters["@InvoiceSysDocID"].SourceColumn = "InvoiceSysDocID";
			parameters["@InvoiceVoucherID"].SourceColumn = "InvoiceVoucherID";
			parameters["@DNoteSysDocID"].SourceColumn = "DNoteSysDocID";
			parameters["@DNoteVoucherID"].SourceColumn = "DNoteVoucherID";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			return insertCommand;
		}

		private bool ValidateData(ExportPickListData journalData)
		{
			return true;
		}

		public bool InsertUpdateExportPickList(ExportPickListData exportPickListData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateExportPickListCommand = GetInsertUpdateExportPickListCommand(isUpdate);
			string text = "";
			string text2 = "";
			string text3 = "";
			try
			{
				DataRow dataRow = exportPickListData.ExportPickListTable.Rows[0];
				string text4 = dataRow["VoucherID"].ToString();
				string text5 = dataRow["SysDocID"].ToString();
				if (isUpdate && !AllowModify(text5, text4))
				{
					throw new CompanyException("Some items in this transaction has been already invoiced. You are not able to modify.", 1047);
				}
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool flag2 = false;
				if (dataRow["IsExport"] != DBNull.Value)
				{
					flag2 = bool.Parse(dataRow["IsExport"].ToString());
				}
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				string text6 = "";
				text6 = ((!flag2) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text6 != "")
				{
					salesFlows = (SalesFlows)int.Parse(text6.ToString());
				}
				_ = 2;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Export_PickList", "VoucherID", dataRow["SysDocID"].ToString(), text4, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in exportPickListData.ExportPickListDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					text3 = row["ProductID"].ToString();
					string text7 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text3, sqlTransaction);
					if (fieldValue != null)
					{
						text7 = fieldValue.ToString();
					}
					if (text7 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text7)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text3, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text3 + "\nUnit:" + row["UnitID"].ToString());
						float num = float.Parse(obj["Factor"].ToString());
						string text8 = obj["FactorType"].ToString();
						float num2 = float.Parse(row["Quantity"].ToString());
						row["UnitFactor"] = num;
						row["FactorType"] = text8;
						row["UnitQuantity"] = row["Quantity"];
						num2 = ((!(text8 == "M")) ? float.Parse(Math.Round(num2 * num, 5).ToString()) : float.Parse(Math.Round(num2 / num, 5).ToString()));
						row["Quantity"] = num2;
					}
				}
				insertUpdateExportPickListCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(exportPickListData, "Export_PickList", insertUpdateExportPickListCommand)) : (flag & Insert(exportPickListData, "Export_PickList", insertUpdateExportPickListCommand)));
				insertUpdateExportPickListCommand = GetInsertUpdateExportPickListDetailsCommand(isUpdate: false);
				insertUpdateExportPickListCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteExportPickListDetailsRows(text5, text4, isDeletingTransaction: false, sqlTransaction);
				}
				if (exportPickListData.Tables["Export_PickList_Detail"].Rows.Count > 0)
				{
					flag &= Insert(exportPickListData, "Export_PickList_Detail", insertUpdateExportPickListCommand);
				}
				bool flag3 = true;
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
					foreach (DataRow row2 in exportPickListData.ExportPickListDetailTable.Rows)
					{
						DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
						dataRow4.BeginEdit();
						dataRow4["SysDocID"] = row2["SysDocID"];
						dataRow4["VoucherID"] = row2["VoucherID"];
						dataRow4["LocationID"] = row2["LocationID"];
						dataRow4["ProductID"] = row2["ProductID"];
						dataRow4["Quantity"] = -1m * decimal.Parse(row2["Quantity"].ToString());
						dataRow4["Reference"] = dataRow["Reference"];
						dataRow4["SysDocType"] = (byte)208;
						dataRow4["TransactionDate"] = dataRow["TransactionDate"];
						dataRow4["TransactionType"] = (byte)2;
						dataRow4["UnitPrice"] = 0;
						dataRow4["RowIndex"] = row2["RowIndex"];
						dataRow4["PayeeType"] = "C";
						dataRow4["PayeeID"] = dataRow["CustomerID"];
						dataRow4["DivisionID"] = dataRow["DivisionID"];
						dataRow4["CompanyID"] = dataRow["CompanyID"];
						if (row2["UnitQuantity"] != DBNull.Value && row2["UnitFactor"] != DBNull.Value)
						{
							dataRow4["UnitQuantity"] = row2["UnitQuantity"];
							dataRow4["Factor"] = row2["UnitFactor"];
							dataRow4["FactorType"] = row2["FactorType"];
							decimal.Parse(row2["UnitFactor"].ToString());
							row2["FactorType"].ToString();
							decimal d = decimal.Parse(row2["UnitQuantity"].ToString());
							decimal num3 = decimal.Parse(row2["Quantity"].ToString());
							decimal d2 = decimal.Parse(row2["UnitPrice"].ToString());
							decimal num4 = default(decimal);
							num4 = ((!(num3 != 0m)) ? default(decimal) : (d * d2 / num3));
							dataRow4["UnitPrice"] = num4;
						}
						dataRow4.EndEdit();
						inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
					}
					if (isUpdate && inventoryTransactionData.Tables.Contains("Product_Lot_Issue_Detail"))
					{
						inventoryTransactionData.Tables.Remove("Product_Lot_Issue_Detail");
					}
					inventoryTransactionData.Merge(exportPickListData.Tables["Product_Lot_Issue_Detail"]);
					if (!flag3)
					{
						flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(exportPickListData, isUpdate: false, sqlTransaction);
					}
					if (!flag3)
					{
						flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
					}
					if (!flag3)
					{
						GLData journalData = CreateDNGLData(exportPickListData, sqlTransaction);
						flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
					}
					if (flag3)
					{
						flag &= AllocateInvoiceToLot(inventoryTransactionData, sqlTransaction);
					}
					if (itemSourceTypes == ItemSourceTypes.SalesOrder)
					{
						foreach (DataRow row3 in exportPickListData.ExportPickListDetailTable.Rows)
						{
							text3 = row3["ProductID"].ToString();
							text = row3["SourceVoucherID"].ToString();
							text2 = row3["SourceSysDocID"].ToString();
							int result = 0;
							if (!(text == "") && !(text2 == ""))
							{
								int.TryParse(row3["SourceRowIndex"].ToString(), out result);
								float result2 = 0f;
								if (row3["UnitQuantity"] != DBNull.Value)
								{
									float.TryParse(row3["UnitQuantity"].ToString(), out result2);
								}
								else
								{
									float.TryParse(row3["Quantity"].ToString(), out result2);
								}
								float num5 = new Products(base.DBConfig).GetReservedQuantity(text3, sqlTransaction) - result2;
								if (num5 < 0f)
								{
									num5 = 0f;
								}
								flag &= new Products(base.DBConfig).UpdateReservedQuantity(text3, num5, sqlTransaction);
								flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text2, text, result, result2, sqlTransaction);
							}
						}
						text = exportPickListData.ExportPickListDetailTable.Rows[0]["SourceVoucherID"].ToString();
						text2 = exportPickListData.ExportPickListDetailTable.Rows[0]["SourceSysDocID"].ToString();
						if (text != "")
						{
							flag &= new SalesOrder(base.DBConfig).CloseShippedOrder(text2, text, sqlTransaction);
						}
					}
				}
				else
				{
					string text9 = "";
					string text10 = "";
					text3 = "";
					ArrayList arrayList = new ArrayList();
					foreach (DataRow row4 in exportPickListData.ExportPickListDetailTable.Rows)
					{
						text3 = row4["ProductID"].ToString();
						text9 = row4["SourceVoucherID"].ToString();
						text10 = row4["SourceSysDocID"].ToString();
						int result3 = 0;
						if (!(text9 == "") && !(text10 == ""))
						{
							int.TryParse(row4["SourceRowIndex"].ToString(), out result3);
							float result4 = 0f;
							if (row4["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row4["UnitQuantity"].ToString(), out result4);
							}
							else
							{
								float.TryParse(row4["Quantity"].ToString(), out result4);
							}
							if (!flag3)
							{
								flag &= new SalesInvoice(base.DBConfig).UpdateRowShippedQuantity(text10, text9, result3, result4, sqlTransaction);
							}
						}
					}
					foreach (DataRow row5 in exportPickListData.ExportPickListDetailTable.Rows)
					{
						text9 = row5["SourceVoucherID"].ToString();
						text10 = row5["SourceSysDocID"].ToString();
						if (!arrayList.Contains(text9 + text10))
						{
							arrayList.Add(text9 + text10);
							if (text9 != "" && !flag3)
							{
								flag &= new SalesInvoice(base.DBConfig).CloseShippedInvoice(text10, text9, sqlTransaction);
							}
						}
					}
				}
				if (itemSourceTypes == ItemSourceTypes.SalesInvoice)
				{
					string exp = "DELETE FROM Invoice_DNote WHERE DnoteSysDocID='" + text5 + "' AND DNoteVoucherID='" + text4 + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
					if (exportPickListData.Tables["Invoice_DNote"].Rows.Count > 0)
					{
						insertUpdateExportPickListCommand = GetInsertUpdateInvoiceDNoteCommand(isUpdate: false);
						insertUpdateExportPickListCommand.Transaction = sqlTransaction;
						flag &= Insert(exportPickListData, "Invoice_DNote", insertUpdateExportPickListCommand);
					}
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Export_PickList", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Delivery Note";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text4, text5, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text4, text5, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Export_PickList", "VoucherID", sqlTransaction);
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

		private GLData CreateDNGLData(ExportPickListData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.ExportPickListTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string value = dataRow["CustomerID"].ToString();
				string text = dataRow["SysDocID"].ToString();
				string text2 = dataRow["VoucherID"].ToString();
				string value2 = dataRow["CompanyID"].ToString();
				string value3 = dataRow["DivisionID"].ToString();
				string value4 = dataRow["JobID"].ToString();
				string textCommand = "SELECT SD.LocationID,  LOC.UnInvoicedInventoryAccountID, LOC.InventoryAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                WHERE SysDocID = '" + text + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				string docLocationID = dataRow2["LocationID"].ToString();
				string text3 = dataRow2["UnInvoicedInventoryAccountID"].ToString();
				bool result = false;
				bool.TryParse(dataRow["IsExport"].ToString(), out result);
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.ExportPickList;
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Delivery Note - " + text2;
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				decimal num = default(decimal);
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				decimal d = default(decimal);
				foreach (DataRow row in transactionData.ExportPickListDetailTable.Rows)
				{
					string text4 = row["ProductID"].ToString();
					string warehouseLocationID = row["LocationID"].ToString();
					int rowIndex = int.Parse(row["RowIndex"].ToString());
					dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(text4, docLocationID, warehouseLocationID, text, sqlTransaction);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException("Product accounts information not found for product or location.");
					}
					dataRow2 = dataSet.Tables[0].Rows[0];
					string text5 = dataRow2["InventoryAssetAccountID"].ToString();
					if (!(text5 == text3))
					{
						ItemTypes itemTypes = ItemTypes.Inventory;
						object obj2 = dataRow2["ItemType"].ToString();
						if (obj2 == null || !(obj2.ToString() != ""))
						{
							throw new CompanyException("Item type is not selected for the product:" + text4);
						}
						itemTypes = (ItemTypes)byte.Parse(obj2.ToString());
						decimal result2 = default(decimal);
						decimal.TryParse(dataRow2["AverageCost"].ToString(), out result2);
						decimal num2 = default(decimal);
						num2 = ((itemTypes == ItemTypes.ConsignmentItem) ? default(decimal) : Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text4, text, text2, rowIndex, sqlTransaction)));
						if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.Assembly)
						{
							string text6 = text5;
							if (hashtable.ContainsKey(text6))
							{
								num = decimal.Parse(hashtable[text6].ToString());
								num += Math.Round(num2, currencyDecimalPoints);
								hashtable[text6] = num;
							}
							else
							{
								hashtable.Add(text6, Math.Round(num2, currencyDecimalPoints));
								arrayList.Add(text6);
							}
							d += Math.Round(num2, currencyDecimalPoints);
						}
					}
				}
				if (d != 0m)
				{
					for (int i = 0; i < hashtable.Count; i++)
					{
						DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
						dataRow4.BeginEdit();
						string text6 = arrayList[i].ToString();
						num = decimal.Parse(hashtable[text6].ToString());
						dataRow4["JournalID"] = 0;
						dataRow4["AccountID"] = text6;
						dataRow4["PayeeID"] = value;
						dataRow4["JobID"] = value4;
						dataRow4["Debit"] = DBNull.Value;
						dataRow4["Credit"] = num;
						dataRow4["IsBaseOnly"] = true;
						dataRow4["Reference"] = dataRow["Reference"];
						dataRow4["CompanyID"] = value2;
						dataRow4["DivisionID"] = value3;
						dataRow4.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow4);
					}
				}
				if (d != 0m)
				{
					if (text3 == "")
					{
						throw new CompanyException("Inventory on delivery account is not set.");
					}
					DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text3;
					dataRow4["Debit"] = num;
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["JobID"] = value4;
					dataRow4["IsBaseOnly"] = true;
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["CompanyID"] = value2;
					dataRow4["DivisionID"] = value3;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public ExportPickListData GetExportPickListByID(string sysDocID, string voucherID)
		{
			try
			{
				ExportPickListData exportPickListData = new ExportPickListData();
				string textCommand = "SELECT DN.*, PaymentTermID  FROM Export_PickList DN INNER JOIN Customer C ON DN.CustomerID = C.CustomerID \r\n               WHERE DN.VoucherID='" + voucherID + "' AND DN.SysDocID='" + sysDocID + "'";
				FillDataSet(exportPickListData, "Export_PickList", textCommand);
				if (exportPickListData == null || exportPickListData.Tables.Count == 0 || exportPickListData.Tables["Export_PickList"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT DISTINCT TD.*,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.ItemType,BrandName AS Brand,\r\n                        Product.Attribute3,Product.MatrixParentID,IsTrackLot,IsTrackSerial, \r\n                        (SELECT TOP 1 \r\n\t\t\t\t\t\t \r\n                                             CASE WHEN PL.SourceLotNumber IS NULL THEN PL.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL2.SourceLotNumber IS NULL THEN PL2.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL3.SourceLotNumber IS NULL THEN PL3.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL4.SourceLotNumber IS NULL THEN PL4.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL5.SourceLotNumber IS NULL THEN PL5.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL6.SourceLotNumber IS NULL THEN PL6.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL7.SourceLotNumber IS NULL THEN PL7.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL8.SourceLotNumber IS NULL THEN PL8.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL6.SourceLotNumber IS NULL THEN PL9.ReceiptNumber ELSE \r\n                            PL9.ReceiptNumber END \r\n                            FROM Product_LOT PL9 WHERE PL9.LotNumber = PL8.SourceLotNumber) END \r\n                            FROM Product_LOT PL8 WHERE PL8.LotNumber = PL7.SourceLotNumber) END \r\n                            FROM Product_LOT PL7 WHERE PL7.LotNumber = PL6.SourceLotNumber) END \r\n                            FROM Product_LOT PL6 WHERE PL6.LotNumber = PL5.SourceLotNumber) END \r\n                            FROM Product_LOT PL5 WHERE PL5.LotNumber = PL4.SourceLotNumber) END \r\n                            FROM Product_LOT PL4 WHERE PL4.LotNumber = PL3.SourceLotNumber) END \r\n                            FROM Product_LOT PL3 WHERE PL3.LotNumber = PL2.SourceLotNumber) END \r\n                            FROM Product_LOT PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END\r\n\t\t\t\t\t\t\t FROM Product_Lot_Sales PLS\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Lot PL \r\n                        ON  PLS.ItemCode =  PL.ItemCode AND PLS.LotNo = PL.LotNumber\r\n\t\t\t\t\t\tWHERE  ( PLS.LotNo = pl.LotNumber  or PLS.LotNo = pl.SourceLotNumber) \r\n                        and td.ProductID = pls.ItemCode and  PLS.DocID='" + sysDocID + "' AND PLS.InvoiceNumber='" + voucherID + "' ) AS [Consign#],\r\n                        (select ISNULL(Sum(Quantity),0) as Shipped from Export_PickList_Detail EP\r\n                        WHERE EP.SourceSysdocID=TD.SourceSysDocID and EP.SourceVoucherID=TD.SourceVoucherID AND TD.ProductID=EP.ProductID AND TD.SourceRowIndex=EP.SourceRowIndex GROUP BY ProductID ) AS Shipped,Isnull(SO.Quantity,0) as Ordered,Isnull(SO.UnitQuantity,0) as OrderedUnitQuantity \r\n                        FROM Export_PickList_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        LEFT OUTER JOIN Product_Brand Brand ON Brand.BrandID = Product.BrandID\r\n                        LEFT JOIN Sales_Order_Detail SO ON SO.SysDocID=TD.SourceSysdocID AND SO.VoucherID=TD.SourceVoucherID AND SO.ProductID=TD.ProductID AND SO.RowIndex=TD.SourceRowIndex \r\n                        WHERE TD.VoucherID='" + voucherID + "'  AND TD.SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex";
				FillDataSet(exportPickListData, "Export_PickList_Detail", textCommand);
				textCommand = "SELECT * FROM Invoice_DNote\r\n                        WHERE DNoteVoucherID='" + voucherID + "' AND DNoteSysDocID='" + sysDocID + "'";
				FillDataSet(exportPickListData, "Invoice_DNote", textCommand);
				textCommand = "SELECT  DISTINCT SourceVoucherID, SourceSysDocID from Export_PickList_Detail\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(exportPickListData, "SourceTableDetails", textCommand);
				DataSet transactionIssuesProductLotsPickList = GetTransactionIssuesProductLotsPickList(sysDocID, voucherID);
				if (exportPickListData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					exportPickListData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				exportPickListData.Merge(transactionIssuesProductLotsPickList, preserveChanges: false);
				return exportPickListData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTransactionIssuesProductLotsPickList(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT LotNo AS LotReference,PL.Reference  AS Reference,LotNo AS LotNumber,LotNo as SourceLotNumber,PLS.ItemCode AS ProductID,PLS.*,PL.ProductionDate,PL.ExpiryDate,PL.ReceiptDate,PLS.DocID AS SysDocID ,PLS.InvoiceNumber AS VoucherID,\r\n                        CASE WHEN PL.SourceLotNumber IS NULL THEN PL.ReceiptNumber ELSE (SELECT ReceiptNumber FROM Product_Lot PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END  AS Consign# \r\n                        from Product_Lot_Sales_PickList PLS INNER JOIN Product_Lot PL ON PLS.LotNo=Pl.LotNumber \r\n\t\t\t\t\t\tWHERE PLS.InvoiceNumber='" + voucherID + "' AND PLS.DocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Product_Lot_Issue_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteExportPickListDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				ExportPickListData exportPickListData = new ExportPickListData();
				string textCommand = "SELECT SOD.*,ISVOID,IsExport FROM Export_PickList_Detail SOD INNER JOIN Export_PickList SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(exportPickListData, "Export_PickList_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(exportPickListData.ExportPickListDetailTable.Rows[0]["IsExport"].ToString(), out result);
				string text = "";
				text = ((!result) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text != "")
				{
					salesFlows = (SalesFlows)int.Parse(text.ToString());
				}
				bool result2 = false;
				bool.TryParse(exportPickListData.ExportPickListDetailTable.Rows[0]["IsVoid"].ToString(), out result2);
				if (!result2)
				{
					if (salesFlows == SalesFlows.SOThenDNThenInvoice)
					{
						if (result)
						{
							flag &= DeleteInventoryTransaction(208, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
						}
						string text2 = "";
						string text3 = "";
						string text4 = "";
						foreach (DataRow row in exportPickListData.ExportPickListDetailTable.Rows)
						{
							text4 = row["ProductID"].ToString();
							text2 = row["SourceVoucherID"].ToString();
							text3 = row["SourceSysDocID"].ToString();
							int result3 = 0;
							if (!(text2 == "") && !(text3 == ""))
							{
								int.TryParse(row["SourceRowIndex"].ToString(), out result3);
								float result4 = 0f;
								if (row["UnitQuantity"] != DBNull.Value)
								{
									float.TryParse(row["UnitQuantity"].ToString(), out result4);
								}
								else
								{
									float.TryParse(row["Quantity"].ToString(), out result4);
								}
								float num = new Products(base.DBConfig).GetReservedQuantity(text4, sqlTransaction) + result4;
								if (num < 0f)
								{
									num = 0f;
								}
								flag &= new Products(base.DBConfig).UpdateReservedQuantity(text4, num, sqlTransaction);
								flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text3, text2, result3, -1f * result4, sqlTransaction);
							}
						}
					}
					else
					{
						string text5 = "";
						string text6 = "";
						if (salesFlows != SalesFlows.SOThenDNThenInvoice)
						{
							foreach (DataRow row2 in exportPickListData.ExportPickListDetailTable.Rows)
							{
								row2["ProductID"].ToString();
								text5 = row2["SourceVoucherID"].ToString();
								text6 = row2["SourceSysDocID"].ToString();
								int result5 = 0;
								if (!(text5 == "") && !(text6 == ""))
								{
									int.TryParse(row2["SourceRowIndex"].ToString(), out result5);
									float result6 = 0f;
									if (row2["UnitQuantity"] != DBNull.Value)
									{
										float.TryParse(row2["UnitQuantity"].ToString(), out result6);
									}
									else
									{
										float.TryParse(row2["Quantity"].ToString(), out result6);
									}
									flag &= new SalesInvoice(base.DBConfig).UpdateRowShippedQuantity(text6, text5, result5, -1f * result6, sqlTransaction);
									flag &= new SalesInvoice(base.DBConfig).ReOpenInvoice(text6, text5, sqlTransaction);
								}
							}
						}
					}
				}
				textCommand = "DELETE FROM Product_Lot_Sales_PickList WHERE DocID  = '" + sysDocID + "' AND InvoiceNumber= '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Export_PickList_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteInventoryTransaction(byte sysDocType, string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				string textCommand = "SELECT * FROM Inventory_Transactions WHERE  SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND SysDocType = " + sysDocType;
				FillDataSet(inventoryTransactionData, "Inventory_Transactions", textCommand);
				if (inventoryTransactionData == null)
				{
					return false;
				}
				if (inventoryTransactionData.Tables[0].Rows.Count == 0)
				{
					return true;
				}
				InventoryTransactionTypes inventoryTransactionTypes = (InventoryTransactionTypes)byte.Parse(inventoryTransactionData.Tables[0].Rows[0]["TransactionType"].ToString());
				StringBuilder stringBuilder = new StringBuilder();
				List<string> list = new List<string>();
				for (int i = 0; i < inventoryTransactionData.Tables[0].Rows.Count; i++)
				{
					string item = inventoryTransactionData.Tables[0].Rows[i]["ProductID"].ToString();
					if (!list.Contains(item))
					{
						list.Add(item);
					}
					string str = "";
					if (i > 0)
					{
						str = ",";
					}
					stringBuilder.AppendLine(str + "'" + AddSingleQuote(inventoryTransactionData.Tables[0].Rows[i]["ProductID"].ToString()) + "' ");
				}
				textCommand = " SELECT ProductID,ItemType, Quantity AS TotalQuantity,AverageCost,LastCost FROM Product WHERE ProductID IN (" + stringBuilder.ToString() + ")";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", textCommand, sqlTransaction);
				textCommand = "SELECT ProductID,LocationID, Quantity AS LocationQuantity FROM Product_Location WHERE ProductID IN (" + stringBuilder.ToString() + ")";
				FillDataSet(dataSet, "Product_Location", textCommand, sqlTransaction);
				new InventoryTransactionData();
				foreach (DataRow row in inventoryTransactionData.Tables[0].Rows)
				{
					string text = row["ProductID"].ToString();
					string text2 = row["LocationID"].ToString();
					DataRow dataRow2 = dataSet.Tables["Product"].Select("ProductID = '" + text + "'")[0];
					ItemTypes itemTypes = (ItemTypes)byte.Parse(dataRow2["ItemType"].ToString());
					row["ItemType"] = (byte)itemTypes;
					if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.ConsignmentItem || itemTypes == ItemTypes.Inventory3PL || itemTypes == ItemTypes.Assembly)
					{
						float num = float.Parse(row["Quantity"].ToString());
						DataRow[] array = dataSet.Tables["Product_Location"].Select("ProductID = '" + text + "' AND LocationID = '" + text2 + "'");
						float result = 0f;
						DataRow dataRow3 = null;
						if (array.Length != 0)
						{
							float.TryParse(array[0]["LocationQuantity"].ToString(), out result);
							dataRow3 = array[0];
						}
						float result2 = 0f;
						float.TryParse(dataRow2["TotalQuantity"].ToString(), out result2);
						decimal result3 = default(decimal);
						decimal.TryParse(dataRow2["AverageCost"].ToString(), out result3);
						if ((inventoryTransactionTypes == InventoryTransactionTypes.Adjustment && num > 0f) || inventoryTransactionTypes == InventoryTransactionTypes.Purchase)
						{
							decimal.Parse(row["UnitPrice"].ToString());
						}
						float num2 = result - num;
						float num3 = result2 - num;
						if (dataRow2 != null)
						{
							dataRow2["TotalQuantity"] = num3;
						}
						if (dataRow3 != null)
						{
							dataRow3["LocationQuantity"] = num2;
						}
					}
				}
				flag &= new Products(base.DBConfig).UpdateTotalQuantity(dataSet, sqlTransaction);
				flag &= new Products(base.DBConfig).UpdateLocationQuantity(inventoryTransactionData, isReverse: true, sqlTransaction);
				if (inventoryTransactionTypes == InventoryTransactionTypes.Purchase || inventoryTransactionTypes == InventoryTransactionTypes.Adjustment || inventoryTransactionTypes == InventoryTransactionTypes.ConsignOut || inventoryTransactionTypes == InventoryTransactionTypes.ConsignIn || inventoryTransactionTypes == InventoryTransactionTypes.W3PLGRN || inventoryTransactionTypes == InventoryTransactionTypes.Transfer || inventoryTransactionTypes == InventoryTransactionTypes.CustomerCreditMemo || inventoryTransactionTypes == InventoryTransactionTypes.JobInventoryReturn || inventoryTransactionTypes == InventoryTransactionTypes.OpeningInventory || inventoryTransactionTypes == InventoryTransactionTypes.Sale)
				{
					textCommand = "UPDATE Product_Lot SET IsDeleted='True' WHERE ReceiptNumber='" + voucherID + "' AND\r\n\t\t\t\t\t\t\t DocID = '" + sysDocID + "'";
					Update(textCommand, sqlTransaction);
					flag &= RemoveOldLotAllocations(sysDocID, voucherID, sqlTransaction);
				}
				return flag & RemoveInvoiceLotAllocation(inventoryTransactionData, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool RemoveOldLotAllocations(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = "";
				bool flag = true;
				text = "SELECT COUNT(LotNumber) from Product_Lot WHERE ReceiptNumber='" + voucherID + "' AND\r\n\t\t\t\t\t\t\t DocID = '" + sysDocID + "' AND ISNULL(IsDeleted,'False')='True'";
				int num = 0;
				object obj = ExecuteScalar(text, sqlTransaction);
				if (obj != null)
				{
					num = int.Parse(obj.ToString());
				}
				if (num > 0)
				{
					text = " INSERT INTO  Unallocated_Lot_Items\r\n\t\t\t\t\t\t\t (SysDocID,VoucherID,productid,rowindex,locationid,quantity,price)  \r\n\t\t\t\t\t\t\t\t(SELECT docid,InvoiceNumber,itemcode,rowindex,locationid,soldqty,UnitPrice FROM Product_Lot_Sales WHERE LOTNO IN \r\n\t\t\t\t\t\t\t\t(SELECT LotNumber from Product_Lot WHERE DocID = '" + sysDocID + "' AND ReceiptNumber='" + voucherID + "' )) ";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
					text = "DELETE FROM Product_Lot_Sales_PickList WHERE LOTNO IN (SELECT LotNumber from Product_Lot WHERE DocID = '" + sysDocID + "' AND ReceiptNumber='" + voucherID + "' ) ";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
					text = "DELETE FROM Product_Lot WHERE ReceiptNumber='" + voucherID + "' AND\r\n\t\t\t\t\t\t\t DocID = '" + sysDocID + "' AND ISNULL(IsDeleted,'False')='True'";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool RemoveInvoiceLotAllocation(InventoryTransactionData inventoryTransactionData, SqlTransaction sqlTransaction)
		{
			string text = "";
			bool flag = true;
			DataSet dataSet = new DataSet();
			string text2 = inventoryTransactionData.InventoryTransactionTable.Rows[0]["VoucherID"].ToString();
			string text3 = inventoryTransactionData.InventoryTransactionTable.Rows[0]["SysDocID"].ToString();
			new DataSet();
			InventoryTransactionTypes inventoryTransactionTypes = (InventoryTransactionTypes)byte.Parse(inventoryTransactionData.InventoryTransactionTable.Rows[0]["TransactionType"].ToString());
			if (inventoryTransactionTypes == InventoryTransactionTypes.Purchase)
			{
				return true;
			}
			text = "Select * FROM Product_Lot_Sales_PickList WHERE DocID='" + text3 + "' AND InvoiceNumber='" + text2 + "'";
			FillDataSet(dataSet, "Product_Lot_Sales_PickList", text, sqlTransaction);
			foreach (DataRow row in dataSet.Tables[0].Rows)
			{
				decimal num = decimal.Parse(row["SoldQty"].ToString());
				int num2 = int.Parse(row["LotNo"].ToString());
				text = ((inventoryTransactionTypes != InventoryTransactionTypes.VendorCreditMemo) ? ("Update Product_Lot SET SoldQty=SoldQty - " + num + " WHERE LotNumber='" + num2.ToString() + "' AND ItemCode='" + row["ItemCode"].ToString() + "'") : ("Update Product_Lot SET ReturnedQty = ISNULL(ReturnedQty,0) - " + num + " WHERE LotNumber='" + num2.ToString() + "' AND ItemCode='" + row["ItemCode"].ToString() + "'"));
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
			}
			text = "DELETE FROM Product_Lot_Sales_PickList WHERE DocID='" + text3 + "' AND InvoiceNumber='" + text2 + "'";
			return flag & (ExecuteNonQuery(text, sqlTransaction) >= 0);
		}

		public bool IsDNoteInvoiced(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT COUNT(*) FROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI \r\n                            ON SID.SysDocID = SI.SysDOcID AND SID.VoucherID = SI.VoucherID WHERE ISNULL(IsVoid,'False')='False' AND RowSource = 5 AND OrderSysDocID = '" + sysDocID + "' AND OrderVoucherID = '" + voucherID + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}

		public bool VoidExportPickList(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Export_PickList", "IsExport", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = bool.Parse(fieldValue.ToString());
				}
				string text = "";
				text = ((!flag2) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text != "")
				{
					salesFlows = (SalesFlows)int.Parse(text.ToString());
				}
				if (!AllowModify(sysDocID, voucherID))
				{
					throw new CompanyException("Some items in this transaction has been already invoiced. You are not able to modify.", 1047);
				}
				string exp = "UPDATE Export_PickList SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				ExportPickListData exportPickListData = new ExportPickListData();
				exp = "SELECT * FROM Export_PickList_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(exportPickListData, "Export_PickList_Detail", exp, sqlTransaction);
				if (salesFlows != SalesFlows.SOThenDNThenInvoice)
				{
					string text2 = "";
					string text3 = "";
					foreach (DataRow row in exportPickListData.ExportPickListDetailTable.Rows)
					{
						row["ProductID"].ToString();
						text2 = row["SourceVoucherID"].ToString();
						text3 = row["SourceSysDocID"].ToString();
						int result = 0;
						if (!(text2 == "") && !(text3 == ""))
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
							flag &= new SalesInvoice(base.DBConfig).UpdateRowShippedQuantity(text3, text2, result, -1f * result2, sqlTransaction);
							flag &= new SalesInvoice(base.DBConfig).ReOpenInvoice(text3, text2, sqlTransaction);
						}
					}
				}
				else
				{
					string text4 = "";
					string text5 = "";
					string text6 = "";
					foreach (DataRow row2 in exportPickListData.ExportPickListDetailTable.Rows)
					{
						text6 = row2["ProductID"].ToString();
						text4 = row2["SourceVoucherID"].ToString();
						text5 = row2["SourceSysDocID"].ToString();
						int result3 = 0;
						if (!(text4 == "") && !(text5 == ""))
						{
							int.TryParse(row2["SourceRowIndex"].ToString(), out result3);
							float result4 = 0f;
							if (row2["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row2["UnitQuantity"].ToString(), out result4);
							}
							else
							{
								float.TryParse(row2["Quantity"].ToString(), out result4);
							}
							float num = new Products(base.DBConfig).GetReservedQuantity(text6, sqlTransaction) + result4;
							if (num < 0f)
							{
								num = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateReservedQuantity(text6, num, sqlTransaction);
							flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text5, text4, result3, -1f * result4, sqlTransaction);
							if (flag)
							{
								exp = "UPDATE Sales_Order SET Status=1 WHERE SysDocID='" + text5 + "' AND VoucherID='" + text4 + "' ";
								flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
							}
						}
					}
					if (flag2)
					{
						flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(208, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
					}
				}
				exp = "DELETE FROM Invoice_DNote WHERE DnoteSysDocID='" + sysDocID + "' AND DNoteVoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Export Pick List", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteExportPickList(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!AllowModify(sysDocID, voucherID))
				{
					throw new CompanyException("Some items in this transaction has been already invoiced. You are not able to modify.", 1047);
				}
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Export_PickList", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidExportPickList(sysDocID, voucherID, isVoid: true);
				}
				flag &= DeleteExportPickListDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				text = "DELETE FROM Export_PickList WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Pick List", voucherID, sysDocID, activityType, sqlTransaction);
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

		internal bool UpdateRowReturnedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReturned,SalesFlow FROM Delivery_Note_Detail DND INNER JOIN Delivery_Note DN ON DN.SysDocID=DND.SysDocID AND DN.VoucherID=DND.VoucherID\r\n                                WHERE DND.SysDocID='" + sysDocID + "' AND DND.VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
					float.TryParse(dataRow["QuantityReturned"].ToString(), out result2);
					SalesFlows salesFlows = SalesFlows.DirectInvoice;
					if (dataRow["SalesFlow"] != DBNull.Value && dataRow["SalesFlow"].ToString() != "")
					{
						salesFlows = (SalesFlows)int.Parse(dataRow["SalesFlow"].ToString());
					}
					if (salesFlows != SalesFlows.SOThenDNThenInvoice)
					{
						throw new CompanyException("Sales return is not allowed from a delivery note because selected delivery note is created in an unsupported Sales Flow.");
					}
				}
				result2 += quantity;
				textCommand = "UPDATE Delivery_Note_Detail SET QuantityReturned=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetUninvoicedExportPickLists(string customerID, bool isExport)
		{
			return GetUninvoicedExportPickLists("", customerID, isExport);
		}

		public DataSet GetUninvoicedExportPickLists(string sysDocID, string customerID, bool isExport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "";
				if (sysDocID != "")
				{
					DataSet entityLinks = new SystemDocuments(base.DBConfig).GetEntityLinks(sysDocID, SysDocEntityTypes.CustomerClass);
					if (entityLinks != null && entityLinks.Tables["System_Doc_Entity_Link"].Rows.Count > 0)
					{
						foreach (DataRow row in entityLinks.Tables["System_Doc_Entity_Link"].Rows)
						{
							if (text != "")
							{
								text += ",";
							}
							text = text + "'" + row["EntityID"].ToString() + "'";
						}
					}
				}
				string str = "SELECT DN.SysDocID [Doc ID],DN.VoucherID [Number], DN.TransactionDate AS [Date],DN.CustomerID + '-' + C.CustomerName AS [Customer]\r\n                              FROM Export_PickList DN\r\n                              INNER JOIN Export_PickList_Detail DND ON DND.SysDocID = DN.SysDocID AND DND.VoucherID = DN.VoucherID \r\n                              INNER JOIN Customer C ON DN.CustomerID=C.CustomerID                                 \r\n                              WHERE ISNULL(DN.IsVoid,'False')='False' AND ISNULL(DN.IsInvoiced,'False')='False' AND ISNULL(DN.SalesFlow,0) = 2 AND ISNULL(DN.Status,0)<>2 ";
				if (customerID != "")
				{
					str = str + " AND DN.CustomerID='" + customerID + "' ";
				}
				if (!string.IsNullOrEmpty(text))
				{
					str = str + " AND C.CustomerClassID IN (" + text + ") ";
				}
				str += "  GROUP BY DN.SysDocID ,DN.VoucherID, DN.TransactionDate ,DN.CustomerID , C.CustomerName \r\n\t\t\t\t\t\t\t  HAVING SUM(Quantity - ISNULL(QuantityReturned,0)) > 0  ";
				FillDataSet(dataSet, "Export_PickList", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetExportPickListToPrint(string sysDocID, string voucherID, bool showLotDetail)
		{
			return GetExportPickListToPrint(sysDocID, new string[1]
			{
				voucherID
			}, showLotDetail);
		}

		public DataSet GetExportPickListToPrint(string sysDocID, string[] voucherID, bool showLotDetail)
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
				string cmdText = "SELECT DISTINCT SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,CA.ContactName,TransactionDate,SI.ContainerNumber,\r\n                            SI.SalesPersonID,RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                            ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                            SI.TermID,TermName,IsVoid,SI.Reference,Discount AS Discount,CA.Phone1,CA.Mobile,CA.Fax,CA.ContactName,\r\n                            Total AS Total,SI.PONumber,SI.Note, SI.InvoiceVoucherID, D.Drivername,D.Note AS [Driver No.],\r\n                            (SELECT TOP 1 RegistrationNumber FROM Vehicle WHERE VehicleID = SI.VehicleID) RegistrationNumber, SI.Reference2,\r\n                            SI.DateCreated,SI.DateUpdated,SI.CreatedBy,SI.UpdatedBy,V.VehicleName,V.VehicleID,J.JobName,JC.CostCategoryName,P.PortName\r\n                            FROM  Export_PickList SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                            LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                            LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                            LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                            LEFT OUTER JOIN Driver D ON D.DriverID=SI.DriverID\r\n                            LEFT OUTER JOIN Vehicle V ON V.VehicleID=SI.VehicleID\r\n                            LEFT JOIN Job J ON J.JobID=SI.JobID\r\n                            LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=SI.CostCategoryID\r\n                            LEFT JOIN Port P ON P.PortID=SI.Port\r\n                            WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Export_PickList", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Export_PickList"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT DISTINCT EPD.SysDocID, EPD.VoucherID, EPD.ProductID, EPD.Description, EPD.LocationID,EPD.RowIndex,PL.Reference, \r\n                        ISNULL(EPD.UnitQuantity, EPD.Quantity) AS Quantity,PSP.* , EPD.Remarks\r\n                        FROM Export_PickList_Detail EPD LEFT JOIN \r\n                        Product_Lot_Sales_PickList PSP ON EPD.SysDocID=PSP.DocID AND EPD.VoucherID=PSP.InvoiceNumber AND EPD.ProductID=PSP.ItemCode AND EPD.RowIndex=PSP.RowIndex LEFT JOIN\r\n                         Product_Lot PL ON PL.LotNumber=PSP.LotNo \r\n                        WHERE EPD.SysDocID='" + sysDocID + "' AND EPD.VoucherID IN (" + text + ")  ORDER BY EPD.RowIndex,PSP.LotNo";
				FillDataSet(dataSet, "Export_PickList_Detail", cmdText);
				dataSet.Relations.Add("ExportPickList", new DataColumn[2]
				{
					dataSet.Tables["Export_PickList"].Columns["SysDocID"],
					dataSet.Tables["Export_PickList"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Export_PickList_Detail"].Columns["SysDocID"],
					dataSet.Tables["Export_PickList_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Export_PickList"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Export_PickList"].Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					row["TotalInWords"] = NumToWord.GetNumInWords(result);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			string exp = "SELECT COUNT(*) FROM Invoice_DNote ID WHERE SourceDocType = 6 AND DnoteSysDocID = '" + sysDocID + "' AND DNOTEVoucherID = '" + voucherNumber + "'";
			object obj = ExecuteScalar(exp);
			exp = "";
			exp = "SELECT COUNT(*) FROM Export_PickList WHERE (ISNULL(IsInvoiced,'False')= 'True' OR ISNULL(IsShipped,'False')='True') AND (SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherNumber + "')";
			obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			exp = "SELECT COUNT(*) FROM Export_PickList WHERE (ISNULL(IsInvoiced,'False')= 'True' OR ISNULL(IsShipped,'False')='True') AND (SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherNumber + "')";
			obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			exp = "SELECT ISNULL(SUM(ISNULL(QuantityReturned,0) + ISNULL(QuantityShipped,0)),0) AS Quantity FROM Export_PickList_Detail\r\n                        WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherNumber + "'";
			obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && decimal.Parse(obj.ToString()) != 0m)
			{
				return false;
			}
			return true;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT    ISNULL(IsVoid,'False') AS V, INV.SysDocID [Doc ID],INV.VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Delivery Date],\r\n                            INV.SalespersonID [Salesperson],SUM(DND.Quantity) AS Quantity, INV.Reference AS Ref1, INV.Reference2 AS Ref2\r\n                            FROM         Export_PickList INV\r\n                            INNER JOIN Export_PickList_Detail DND ON DND.SysDocID=INV.SysDocID AND DND.VoucherID=INV.VoucherID\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			text3 += " GROUP BY IsVoid,INV.SysDocID,INV.VoucherID,INV.CustomerID ,CustomerName,TransactionDate,INV.SalespersonID,INV.Reference, INV.Reference2";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Export_PickList", sqlCommand);
			return dataSet;
		}

		public DataSet GetDOsForPackingList(string customerID, bool isExport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT DO.SysDocID [Doc ID],DO.VoucherID [Number], TransactionDate AS [Date],DO.CustomerID + '-' + CUS.CustomerName AS Customer \r\n                                FROM Delivery_Note DO\r\n                                INNER JOIN Customer CUS ON DO.CustomerID=CUS.CustomerID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsShipped,'False')='False'  \r\n                                AND ISNULL(IsExport,'False')='" + isExport.ToString() + "' ";
				if (customerID != "")
				{
					text = text + " AND (DO.CustomerID = '" + customerID + "')";
				}
				FillDataSet(dataSet, "Export_PickList", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDOItemsToShip(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT DO.customerID, DOD.SysDocID [Doc ID], DOD.VoucherID,ProductID,Description,DOD.UnitID,RowIndex,UnitQuantity,LocationID,\r\n                                ISNULL(UnitQuantity,Quantity) - ISNULL(QuantityReturned,0) AS Quantity,ISNULL(QuantityShipped,0) AS QuantityShipped\r\n                                FROM Delivery_Note_Detail DOD INNER JOIN Delivery_Note DO ON DO.SysDocID=DOD.SysDocID AND DO.VoucherID=DOD.VoucherID\r\n                              \r\n                             WHERE DOD.SysDocID='" + sysDocID + "' AND DOD.VoucherID='" + voucherID + "'";
				FillDataSet(dataSet, "Export_PickList_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPackingListItemsToInvoice(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT DISTINCT DO.customerID, DOD.SysDocID [Doc ID], DOD.VoucherID,DOD.ProductID,DOD.Description,DOD.UnitID,DOD.RowIndex,DOD.UnitQuantity,DOD.LocationID,\r\n                                ISNULL(DOD.UnitQuantity,DOD.Quantity) - ISNULL(QuantityReturned,0) AS Quantity,ISNULL(QuantityShipped,0) AS QuantityShipped\r\n                                FROM Delivery_Note_Detail DOD INNER JOIN Export_PackingList_Detail PKD ON PKD.SourceSysDocID=DOD.SysDocID AND PKD.SourceVoucherID=DOD.VoucherID\r\n                                INNER JOIN Delivery_Note DO ON DO.SysDocID=DOD.SysDocID AND DO.VoucherID=DOD.VoucherID\r\n\t\t\t\t\t\t\t\tWHERE PKD.SysDocID= '" + sysDocID + "' AND PKD.VoucherID = '" + voucherID + "' AND ISNULL(DO.IsInvoiced,'False') = 'False' ";
				FillDataSet(dataSet, "Export_PickList_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool AllocateInvoiceToLot(InventoryTransactionData inventoryTransactionData, SqlTransaction sqlTransaction)
		{
			string text = "";
			bool flag = true;
			DataRow dataRow = inventoryTransactionData.InventoryTransactionTable.Rows[0];
			InventoryTransactionTypes inventoryTransactionTypes = (InventoryTransactionTypes)byte.Parse(dataRow["TransactionType"].ToString());
			DataSet lotData = new DataSet();
			DateTime dateTime = DateTime.Parse(dataRow["TransactionDate"].ToString());
			StoreConfiguration.ToSqlDateTimeString(dateTime);
			string commaSeperatedIDs = GetCommaSeperatedIDs(inventoryTransactionData, "Inventory_Transactions", "ProductID");
			text = " SELECT DISTINCT ProductID,ItemTYpe,IsTrackLot,IsTrackSerial,CostMethod FROM Product WHERE ProductID IN (" + commaSeperatedIDs + ") ";
			FillDataSet(lotData, "Product", text, sqlTransaction);
			ArrayList arrayList = new ArrayList();
			foreach (DataRow row in lotData.Tables[0].Rows)
			{
				arrayList.Add(row["ProductID"].ToString());
			}
			new Databases(base.DBConfig).BulkCopy(arrayList, "#TMP_Item", "ProductID", sqlTransaction);
			if (inventoryTransactionTypes == InventoryTransactionTypes.CustomerCreditMemo)
			{
				text = "Select DISTINCT LotNo AS LotNumber,SUM(PLS.SoldQty) AS SoldQty,PLS.ItemCode,ReceiptDate,ReceiptNumber,PLS.LocationID,PL.Cost,AvgCost, \r\n                            SUM(PLS.SoldQty) AS LotQty,PL.BinID,PL.Reference2 FROM Product_Lot_Sales PLS \r\n                            INNER JOIN Product_Lot PL ON PL.LotNumber = PLS.LotNo\r\n                            WHERE PLS.ItemCode IN (SELECT ProductID FROM #TMP_Item) and \r\n                             ISNULL(IsDeleted,'False') = 'False'\r\n                             GROUP BY LotNo,PLS.ItemCode,ReceiptDate,ReceiptNumber,PLS.LocationID,PL.Cost,AvgCost,PL.BinID,PL.Reference2\r\n                            Order BY ReceiptDate,ReceiptNumber DESC  \r\n                            DROP TABLE #TMP_Item";
				FillDataSet(lotData, "Product_Lot", text, sqlTransaction);
			}
			else
			{
				text = "Select LotNumber,SoldQty,ItemCode,ReceiptDate,ReceiptNumber,LocationID,Cost,AvgCost, (LotQty - ISNULL(ReturnedQty,0)) - ISNULL(SoldQty,0) AS LotQty,BinID,Reference2 FROM product_lot\r\n\t\t\t\t\t\t\t\tWHERE ItemCode IN (SELECT ProductID FROM #TMP_Item) and  \r\n\t\t\t\t\t\t\t\t(LotQty - ISNULL(ReturnedQty,0)) - ISNULL(SoldQty,0)>0 and ISNULL(IsDeleted,'False') = 'False'\r\n\t\t\t\t\t\t\t\tOrder BY ReceiptDate,ReceiptNumber DESC \r\n                                DROP TABLE #TMP_Item ";
				FillDataSet(lotData, "Product_Lot", text, sqlTransaction);
			}
			string supplierCode = dataRow["PayeeID"].ToString();
			foreach (DataRow row2 in inventoryTransactionData.InventoryTransactionTable.Rows)
			{
				decimal d = decimal.Parse(row2["Quantity"].ToString());
				int costMethod = 1;
				int itemType = 1;
				bool result = false;
				bool result2 = false;
				string text2 = row2["ProductID"].ToString();
				string locationID = row2["LocationID"].ToString();
				DataRow[] array = lotData.Tables["Product"].Select("ProductID = '" + text2 + "' ");
				if (array != null && array.Length != 0)
				{
					costMethod = int.Parse(array[0]["CostMethod"].ToString());
					itemType = int.Parse(array[0]["ItemType"].ToString());
					bool.TryParse(array[0]["IsTrackLot"].ToString(), out result);
					bool.TryParse(array[0]["IsTrackSerial"].ToString(), out result2);
				}
				if (!(d > 0m))
				{
					string sysDocID = row2["SysDocID"].ToString();
					string voucherID = row2["VoucherID"].ToString();
					int rowIndex = int.Parse(row2["RowIndex"].ToString());
					decimal price = default(decimal);
					if (row2["UnitPrice"] != DBNull.Value)
					{
						price = decimal.Parse(row2["UnitPrice"].ToString());
					}
					decimal quantity = default(decimal);
					if (row2["Quantity"] != DBNull.Value)
					{
						quantity = decimal.Parse(row2["Quantity"].ToString());
					}
					flag &= AllocateProductLineToLot(inventoryTransactionData, ref lotData, dateTime, sysDocID, voucherID, text2, locationID, rowIndex, quantity, price, itemType, costMethod, supplierCode, inventoryTransactionTypes, sqlTransaction);
				}
			}
			return flag;
		}

		private bool AllocateProductLineToLot(InventoryTransactionData transactionData, ref DataSet lotData, DateTime transactionDate, string sysDocID, string voucherID, string productID, string locationID, int rowIndex, decimal quantity, decimal price, int itemType, int costMethod, string supplierCode, InventoryTransactionTypes transactionType, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			ItemTypes itemTypes = (ItemTypes)itemType;
			if (itemTypes != ItemTypes.Inventory && itemTypes != ItemTypes.ConsignmentItem && itemTypes != ItemTypes.Inventory3PL && itemTypes != ItemTypes.Assembly)
			{
				return true;
			}
			bool flag2 = false;
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "IsTrackLot", "ProductID", productID, sqlTransaction);
			if (fieldValue != null && fieldValue.ToString() != "")
			{
				flag2 = bool.Parse(fieldValue.ToString());
			}
			if (transactionType == InventoryTransactionTypes.CustomerCreditMemo || transactionType == InventoryTransactionTypes.JobInventoryReturn)
			{
				quantity = -1m * quantity;
			}
			decimal num = quantity;
			string text = "";
			if (quantity != 0m && (flag2 || itemTypes == ItemTypes.ConsignmentItem || itemTypes == ItemTypes.Inventory3PL))
			{
				DataRow[] array = transactionData.Tables["Product_Lot_Issue_Detail"].Select("ProductID = '" + productID + "' AND RowIndex = " + rowIndex);
				if (array.Length == 0)
				{
					throw new CompanyException("Lot allocation not found item: '" + productID + "'");
				}
				DataRow[] array2 = array;
				foreach (DataRow dataRow in array2)
				{
					decimal num2 = decimal.Parse(dataRow["SoldQty"].ToString());
					int num3 = int.Parse(dataRow["LotNumber"].ToString());
					string b = dataRow["LocationID"].ToString();
					if (locationID != b)
					{
						throw new CompanyException("One of the selected lots is in a different location.\n Product Code:" + productID + "\nLot Number:" + dataRow["Reference"].ToString());
					}
					DataRow[] array3 = lotData.Tables["Product_Lot"].Select("LotNumber = " + num3);
					if (array3.Length == 0)
					{
						throw new CompanyException("Error in lot allocation. Lot number: " + num3 + " not found.");
					}
					DataRow dataRow2 = array3[0];
					decimal d = decimal.Parse(dataRow2["LotQty"].ToString());
					if (d < num2)
					{
						throw new CompanyException("Available quantity in lot number: " + num3 + " is less than issued quantity.");
					}
					dataRow2["LotQty"] = d - Math.Abs(num2);
					switch (transactionType)
					{
					case InventoryTransactionTypes.VendorCreditMemo:
					case InventoryTransactionTypes.ConsignInReturn:
						text = text + "\n  UPDATE Product_Lot SET ReturnedQty = ISNULL(ReturnedQty,0) + " + num2 + " WHERE LotNumber='" + dataRow["LotNumber"].ToString() + "' AND ItemCode='" + dataRow["ProductID"].ToString() + "' ";
						break;
					case InventoryTransactionTypes.CustomerCreditMemo:
					case InventoryTransactionTypes.JobInventoryReturn:
						text = ((!(num2 < 0m)) ? (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) - (") : (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) + ("));
						text = text + num2 + ") WHERE LotNumber='" + dataRow["LotNumber"].ToString() + "' AND ItemCode='" + dataRow["ProductID"].ToString() + "' ";
						break;
					default:
						text = ((!(num2 < 0m)) ? (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) + (") : (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) - ("));
						text = text + num2 + ") WHERE LotNumber='" + dataRow["LotNumber"].ToString() + "' AND ItemCode='" + dataRow["ProductID"].ToString() + "' ";
						break;
					}
					decimal num4 = default(decimal);
					if (costMethod != 1 && costMethod == 2)
					{
						num4 = ((dataRow["Cost"] != null && dataRow["Cost"].ToString() != string.Empty) ? decimal.Parse(dataRow["Cost"].ToString()) : 0m);
					}
					if (transactionType == InventoryTransactionTypes.CustomerCreditMemo || transactionType == InventoryTransactionTypes.JobInventoryReturn)
					{
						num2 = -1m * num2;
					}
					if (num2 != 0m)
					{
						text = "";
						text = text + "\n  INSERT INTO [Product_Lot_Sales_PickList] (DocID,InvoiceNumber,RowIndex,ItemCode,LocationID,LotNo,SoldQty,UnitPrice,Cost,BinID,Reference2) VALUES('" + sysDocID + "','" + voucherID + "'," + rowIndex + ",'" + productID + "','" + locationID + "'," + dataRow["LotNumber"].ToString() + "," + num2.ToString() + "," + price + "," + num4.ToString() + ",'" + dataRow["BinID"].ToString() + "','" + dataRow["Reference2"].ToString() + "') ";
						num += Math.Abs(num2);
					}
					if (text != "")
					{
						flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
					}
				}
			}
			else
			{
				DataRow[] array4 = lotData.Tables["Product_Lot"].Select("ItemCode = '" + productID + "' AND LocationID = '" + locationID + "'", "ReceiptDate");
				if (num > 0m)
				{
					throw new CompanyException("Cannot allocate negative quantity. Transaction No:" + voucherID + " Item:" + productID);
				}
				DataRow[] array2 = array4;
				foreach (DataRow dataRow3 in array2)
				{
					if (num == 0m)
					{
						break;
					}
					decimal num5 = decimal.Parse(dataRow3["LotQty"].ToString());
					decimal num6 = default(decimal);
					if (num5 >= Math.Abs(num))
					{
						num5 += num;
						num6 = Math.Abs(num);
						num = default(decimal);
						dataRow3["LotQty"] = num5;
					}
					else
					{
						num += num5;
						num6 = num5;
						dataRow3["LotQty"] = default(decimal);
					}
					switch (transactionType)
					{
					case InventoryTransactionTypes.VendorCreditMemo:
						text = text + "\n  UPDATE Product_Lot SET ReturnedQty = ISNULL(ReturnedQty,0) + " + num6 + " WHERE LotNumber='" + dataRow3["LotNumber"].ToString() + "' AND ItemCode='" + dataRow3["ItemCode"].ToString() + "' ";
						break;
					case InventoryTransactionTypes.CustomerCreditMemo:
					case InventoryTransactionTypes.JobInventoryReturn:
						text = ((!(num6 < 0m)) ? (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) - (") : (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) + ("));
						text = text + num6 + ") WHERE LotNumber='" + dataRow3["LotNumber"].ToString() + "' AND ItemCode='" + dataRow3["ItemCode"].ToString() + "' ";
						break;
					default:
						text = ((!(num6 < 0m)) ? (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) + (") : (text + "\n Update Product_Lot SET SoldQty = ISNULL(SoldQty,0) - ("));
						text = text + num6 + ") WHERE LotNumber='" + dataRow3["LotNumber"].ToString() + "' AND ItemCode='" + dataRow3["ItemCode"].ToString() + "' ";
						break;
					}
					decimal num7 = default(decimal);
					num7 = decimal.Parse(dataRow3["Cost"].ToString());
					if (transactionType == InventoryTransactionTypes.CustomerCreditMemo || transactionType == InventoryTransactionTypes.JobInventoryReturn)
					{
						num6 = -1m * num6;
					}
					text = "";
					if (num6 != 0m)
					{
						text = text + "\n  INSERT INTO [Product_Lot_Sales_PickList] (DocID,InvoiceNumber,RowIndex,ItemCode,LocationID,LotNo,SoldQty,UnitPrice,Cost,BinID,Reference2) VALUES('" + sysDocID + "','" + voucherID + "'," + rowIndex + ",'" + productID + "','" + locationID + "'," + dataRow3["LotNumber"].ToString() + "," + num6.ToString() + "," + price + "," + num7.ToString() + ",'" + dataRow3["BinID"].ToString() + "','" + dataRow3["Reference2"].ToString() + "') ";
					}
				}
			}
			if (Math.Abs(num) > 0m)
			{
				if (flag2)
				{
					throw new CompanyException("You cannot issue more than available quantity for an item which you are tracking the lot.");
				}
				text = " INSERT INTO [Unallocated_Lot_Items] (SysDocID,VoucherID,ProductID,RowIndex,LocationID,Quantity,Price) VALUES ('" + sysDocID + "','" + voucherID + "','" + productID + "'," + rowIndex + ",'" + locationID + "'," + Math.Abs(num) + "," + price + ")";
			}
			return flag;
		}

		internal bool ClosePickList(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = "";
				text = "UPDATE Export_PickList SET Status= 2 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(text, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public bool AllowDelete(string sysDocID, string voucherNumber)
		{
			string exp = "SELECT COUNT(*) FROM Delivery_Note_Detail PEA WHERE  SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherNumber + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}
	}
}
