using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class DeliveryNote : StoreObject
	{
		private const string DELIVERYNOTE_TABLE = "Delivery_Note";

		private const string DELIVERYNOTEDETAIL_TABLE = "Delivery_Note_Detail";

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

		private const string PODATE_PARM = "@PODate";

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

		private const string REMARKS_PARM = "@Remarks";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string ORDERVOUCHERID_PARM = "@OrderVoucherID";

		private const string REFSLNO_PARM = "@RefSlNo";

		private const string REFTEXT1_PARM = "@RefText1";

		private const string REFTEXT2_PARM = "@RefText2";

		private const string REFNUM1_PARM = "@RefNum1";

		private const string REFNUM2_PARM = "@RefNum2";

		private const string REFDATE1_PARM = "@RefDate1";

		private const string REFDATE2_PARM = "@RefDate2";

		private const string SPECIFICATIONID_PARM = "@SpecificationID";

		private const string STYLEID_PARM = "@StyleID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string ROWSOURCE_PARM = "@RowSource";

		private const string LISTVOUCHERID_PARM = "@ListVoucherID";

		private const string LISTSYSDOCID_PARM = "@ListSysDocID";

		private const string LISTROWINDEX_PARM = "@ListRowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public DeliveryNote(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateDeliveryNoteText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Delivery_Note", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("SalesFlow", "@SalesFlow"), new FieldValue("IsExport", "@IsExport"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("BillingAddressID", "@BillingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("ShipToAddress", "@ShipToAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Discount", "@Discount"), new FieldValue("Total", "@Total"), new FieldValue("PONumber", "@PONumber"), new FieldValue("PODate", "@PODate"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("Note", "@Note"), new FieldValue("CLUserID", "@CLUserID"), new FieldValue("Port", "@Port"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("DriverID", "@DriverID"), new FieldValue("VehicleID", "@VehicleID"), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("ContainerSizeID", "@ContainerSizeID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Delivery_Note", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDeliveryNoteCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDeliveryNoteText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDeliveryNoteText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@PODate", SqlDbType.DateTime);
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
			parameters["@PODate"].SourceColumn = "PODate";
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

		private string GetInsertUpdateDeliveryNoteDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Delivery_Note_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("SpecificationID", "@SpecificationID"), new FieldValue("StyleID", "@StyleID"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("RowSource", "@RowSource"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("ListVoucherID", "@ListVoucherID"), new FieldValue("ListSysDocID", "@ListSysDocID"), new FieldValue("ListRowIndex", "@ListRowIndex"), new FieldValue("JobID", "@JobID"), new FieldValue("RefSlNo", "@RefSlNo"), new FieldValue("RefText1", "@RefText1"), new FieldValue("RefText2", "@RefText2"), new FieldValue("RefNum1", "@RefNum1"), new FieldValue("RefNum2", "@RefNum2"), new FieldValue("RefDate1", "@RefDate1"), new FieldValue("RefDate2", "@RefDate2"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDeliveryNoteDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDeliveryNoteDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDeliveryNoteDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@Remarks", SqlDbType.NVarChar);
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
			parameters.Add("@SpecificationID", SqlDbType.NVarChar);
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters.Add("@ListSysDocID", SqlDbType.NVarChar);
			parameters.Add("@ListVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ListRowIndex", SqlDbType.Int);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
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
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Remarks"].SourceColumn = "Remarks";
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
			parameters["@SpecificationID"].SourceColumn = "SpecificationID";
			parameters["@StyleID"].SourceColumn = "StyleID";
			parameters["@ListVoucherID"].SourceColumn = "ListVoucherID";
			parameters["@ListRowIndex"].SourceColumn = "ListRowIndex";
			parameters["@ListSysDocID"].SourceColumn = "ListSysDocID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
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

		private bool ValidateData(DeliveryNoteData journalData)
		{
			return true;
		}

		public bool InsertUpdateDeliveryNote(DeliveryNoteData deliveryNoteData, bool isUpdate, bool TempSave = false)
		{
			bool flag = true;
			SqlCommand insertUpdateDeliveryNoteCommand = GetInsertUpdateDeliveryNoteCommand(isUpdate);
			string text = "";
			string text2 = "";
			string text3 = "";
			try
			{
				DataRow dataRow = deliveryNoteData.DeliveryNoteTable.Rows[0];
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
				bool result = false;
				object obj = null;
				obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.AllowESCreatefromPickList, null);
				if (obj != null)
				{
					bool.TryParse(obj.ToString(), out result);
				}
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text6 != "")
				{
					salesFlows = (SalesFlows)int.Parse(text6.ToString());
				}
				_ = 2;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Delivery_Note", "VoucherID", dataRow["SysDocID"].ToString(), text4, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in deliveryNoteData.DeliveryNoteDetailTable.Rows)
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
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text3, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text3 + "\nUnit:" + row["UnitID"].ToString());
						float num = float.Parse(obj2["Factor"].ToString());
						string text8 = obj2["FactorType"].ToString();
						float num2 = float.Parse(row["Quantity"].ToString());
						row["UnitFactor"] = num;
						row["FactorType"] = text8;
						row["UnitQuantity"] = row["Quantity"];
						num2 = ((!(text8 == "M")) ? float.Parse(Math.Round(num2 * num, 5).ToString()) : float.Parse(Math.Round(num2 / num, 5).ToString()));
						row["Quantity"] = num2;
					}
				}
				insertUpdateDeliveryNoteCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(deliveryNoteData, "Delivery_Note", insertUpdateDeliveryNoteCommand)) : (flag & Insert(deliveryNoteData, "Delivery_Note", insertUpdateDeliveryNoteCommand)));
				insertUpdateDeliveryNoteCommand = GetInsertUpdateDeliveryNoteDetailsCommand(isUpdate: false);
				insertUpdateDeliveryNoteCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteDeliveryNoteDetailsRows(text5, text4, isDeletingTransaction: false, sqlTransaction);
				}
				if (deliveryNoteData.Tables["Delivery_Note_Detail"].Rows.Count > 0)
				{
					flag &= Insert(deliveryNoteData, "Delivery_Note_Detail", insertUpdateDeliveryNoteCommand);
				}
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
					foreach (DataRow row2 in deliveryNoteData.DeliveryNoteDetailTable.Rows)
					{
						DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
						dataRow4.BeginEdit();
						dataRow4["SysDocID"] = row2["SysDocID"];
						dataRow4["VoucherID"] = row2["VoucherID"];
						dataRow4["LocationID"] = row2["LocationID"];
						dataRow4["JobID"] = row2["JobID"];
						dataRow4["SpecificationID"] = row2["SpecificationID"];
						dataRow4["StyleID"] = row2["StyleID"];
						dataRow4["CostCategoryID"] = row2["CostCategoryID"];
						dataRow4["ProductID"] = row2["ProductID"];
						dataRow4["Quantity"] = -1m * decimal.Parse(row2["Quantity"].ToString());
						dataRow4["Reference"] = dataRow["Reference"];
						if (flag2)
						{
							dataRow4["SysDocType"] = (byte)53;
						}
						else
						{
							dataRow4["SysDocType"] = (byte)24;
						}
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
					inventoryTransactionData.Merge(deliveryNoteData.Tables["Product_Lot_Issue_Detail"]);
					flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(deliveryNoteData, isUpdate: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
					GLData journalData = CreateDNGLData(deliveryNoteData, sqlTransaction);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
					flag &= UpdateInventoryTransactionRowID(text5, text4, sqlTransaction);
					if (itemSourceTypes == ItemSourceTypes.SalesOrder)
					{
						foreach (DataRow row3 in deliveryNoteData.DeliveryNoteDetailTable.Rows)
						{
							text3 = row3["ProductID"].ToString();
							text = row3["SourceVoucherID"].ToString();
							text2 = row3["SourceSysDocID"].ToString();
							int result2 = 0;
							if (!(text == "") && !(text2 == ""))
							{
								int.TryParse(row3["SourceRowIndex"].ToString(), out result2);
								float result3 = 0f;
								if (row3["UnitQuantity"] != DBNull.Value)
								{
									float.TryParse(row3["UnitQuantity"].ToString(), out result3);
								}
								else
								{
									float.TryParse(row3["Quantity"].ToString(), out result3);
								}
								float num5 = new Products(base.DBConfig).GetReservedQuantity(text3, sqlTransaction) - result3;
								if (num5 < 0f)
								{
									num5 = 0f;
								}
								flag &= new Products(base.DBConfig).UpdateReservedQuantity(text3, num5, sqlTransaction);
								if (flag2)
								{
									if (!result)
									{
										flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text2, text, result2, result3, sqlTransaction);
									}
								}
								else
								{
									flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text2, text, result2, result3, sqlTransaction);
									if (isUpdate)
									{
										flag &= (flag &= new SalesOrder(base.DBConfig).ReOpenOrder(text2, text, sqlTransaction));
									}
								}
							}
						}
						text = deliveryNoteData.DeliveryNoteDetailTable.Rows[0]["SourceVoucherID"].ToString();
						text2 = deliveryNoteData.DeliveryNoteDetailTable.Rows[0]["SourceSysDocID"].ToString();
						if (text != "")
						{
							flag = (result ? (flag & new ExportPickList(base.DBConfig).ClosePickList(text2, text, sqlTransaction)) : (flag & new SalesOrder(base.DBConfig).CloseShippedOrder(text2, text, sqlTransaction)));
						}
					}
				}
				else
				{
					string text9 = "";
					string text10 = "";
					text3 = "";
					ArrayList arrayList = new ArrayList();
					foreach (DataRow row4 in deliveryNoteData.DeliveryNoteDetailTable.Rows)
					{
						text3 = row4["ProductID"].ToString();
						text9 = row4["SourceVoucherID"].ToString();
						text10 = row4["SourceSysDocID"].ToString();
						int result4 = 0;
						if (!(text9 == "") && !(text10 == ""))
						{
							int.TryParse(row4["SourceRowIndex"].ToString(), out result4);
							float result5 = 0f;
							if (row4["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row4["UnitQuantity"].ToString(), out result5);
							}
							else
							{
								float.TryParse(row4["Quantity"].ToString(), out result5);
							}
							flag &= new SalesInvoice(base.DBConfig).UpdateRowShippedQuantity(text10, text9, result4, result5, sqlTransaction);
						}
					}
					foreach (DataRow row5 in deliveryNoteData.DeliveryNoteDetailTable.Rows)
					{
						text9 = row5["SourceVoucherID"].ToString();
						text10 = row5["SourceSysDocID"].ToString();
						if (!arrayList.Contains(text9 + text10))
						{
							arrayList.Add(text9 + text10);
							if (text9 != "")
							{
								flag &= new SalesInvoice(base.DBConfig).CloseShippedInvoice(text10, text9, sqlTransaction);
							}
						}
					}
				}
				foreach (DataRow row6 in deliveryNoteData.InvoiceDNoteTable.Rows)
				{
					row6.BeginEdit();
					row6["DNoteSysDocID"] = dataRow["SysDocID"];
					row6["DNoteVoucherID"] = dataRow["VoucherID"];
					row6["SourceDocType"] = (byte)5;
					row6.EndEdit();
				}
				if (itemSourceTypes == ItemSourceTypes.SalesInvoice)
				{
					string exp = "DELETE FROM Invoice_DNote WHERE DnoteSysDocID='" + text5 + "' AND DNoteVoucherID='" + text4 + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
					if (deliveryNoteData.Tables["Invoice_DNote"].Rows.Count > 0)
					{
						insertUpdateDeliveryNoteCommand = GetInsertUpdateInvoiceDNoteCommand(isUpdate: false);
						insertUpdateDeliveryNoteCommand.Transaction = sqlTransaction;
						flag &= Insert(deliveryNoteData, "Invoice_DNote", insertUpdateDeliveryNoteCommand);
					}
				}
				if (bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.MaterialReservationOnSo, false).ToString()))
				{
					flag &= new Reservation(base.DBConfig).InsertUpdateReservation(deliveryNoteData, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Delivery_Note", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Delivery Note";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text4, text5, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text4, text5, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Delivery_Note", "VoucherID", sqlTransaction);
				}
				if (!isUpdate && TempSave)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateToLastDocumentNumberWithTemporary(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Delivery_Note", "VoucherID", sqlTransaction);
				}
				if (TempSave && !string.IsNullOrEmpty(dataRow["TempKey"].ToString()))
				{
					flag &= new Settings(base.DBConfig).insertTempDeleteActvity(dataRow["TempKey"].ToString(), flag, sqlTransaction, dataRow["VoucherID"].ToString(), int.Parse(dataRow["AutoKeyID"].ToString()));
				}
				if (flag)
				{
					if (flag2)
					{
						new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ExportDeliveryNote, text5, text4, "Delivery_Note", sqlTransaction);
					}
					else
					{
						new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.DeliveryNote, text5, text4, "Delivery_Note", sqlTransaction);
					}
				}
				ModifyTransactions(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), dataRow["CurrentUser"].ToString(), isModify: true, "toupdate");
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
				string exp = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Delivery_Note_Detail SID  \r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateDNGLData(DeliveryNoteData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.DeliveryNoteTable.Rows[0];
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
				SysDocTypes sysDocTypes = SysDocTypes.DeliveryNote;
				if (result)
				{
					sysDocTypes = SysDocTypes.ExportDeliveryNote;
				}
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
				decimal num2 = default(decimal);
				foreach (DataRow row in transactionData.DeliveryNoteDetailTable.Rows)
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
						decimal num3 = default(decimal);
						num3 = ((itemTypes == ItemTypes.ConsignmentItem) ? default(decimal) : Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text4, text, text2, rowIndex, mergeWithRefRows: false, sqlTransaction)));
						if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.Assembly)
						{
							string text6 = text5;
							if (hashtable.ContainsKey(text6))
							{
								num = decimal.Parse(hashtable[text6].ToString());
								num += Math.Round(num3, currencyDecimalPoints);
								hashtable[text6] = num;
							}
							else
							{
								hashtable.Add(text6, Math.Round(num3, currencyDecimalPoints));
								arrayList.Add(text6);
							}
							num2 += Math.Round(num3, currencyDecimalPoints);
						}
					}
				}
				if (num2 != 0m)
				{
					for (int i = 0; i < hashtable.Count; i++)
					{
						DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
						dataRow4.BeginEdit();
						string text6 = arrayList[i].ToString();
						num = decimal.Parse(hashtable[text6].ToString());
						dataRow4["JournalID"] = 0;
						dataRow4["AccountID"] = text6;
						dataRow4["JVEntryType"] = JVEntryTypes.InventoryAsset;
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
				if (num2 != 0m)
				{
					if (text3 == "")
					{
						throw new CompanyException("Inventory on delivery account is not set.");
					}
					DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text3;
					dataRow4["Debit"] = num2;
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["JobID"] = value4;
					dataRow4["JVEntryType"] = JVEntryTypes.InventoryAsset;
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

		public DeliveryNoteData GetDeliveryNoteByID(string sysDocID, string voucherID)
		{
			try
			{
				DeliveryNoteData deliveryNoteData = new DeliveryNoteData();
				string textCommand = "SELECT DN.*,C.DiscountPercent AS [DiscountPercent], PaymentTermID  FROM Delivery_Note DN INNER JOIN Customer C ON DN.CustomerID = C.CustomerID \r\n               WHERE DN.VoucherID='" + voucherID + "' AND DN.SysDocID='" + sysDocID + "'";
				FillDataSet(deliveryNoteData, "Delivery_Note", textCommand);
				if (deliveryNoteData == null || deliveryNoteData.Tables.Count == 0 || deliveryNoteData.Tables["Delivery_Note"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT DISTINCT TD.*,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.ItemType,BrandName AS Brand,\r\n                        Product.Attribute3,Product.MatrixParentID,IsTrackLot,IsTrackSerial,ISNull(Product.TaxGroupID,PC.TaxGroupID) AS TaxGroupID,\r\n                        TD.TaxOption AS TaxOption, SOD.Quantity as Ordered, SOD.QuantityShipped as ShippedQty,\r\n                        (SELECT TOP 1  CASE WHEN PL.SourceLotNumber IS NULL THEN PL.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL2.SourceLotNumber IS NULL THEN PL2.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL3.SourceLotNumber IS NULL THEN PL3.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL4.SourceLotNumber IS NULL THEN PL4.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL5.SourceLotNumber IS NULL THEN PL5.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL6.SourceLotNumber IS NULL THEN PL6.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL7.SourceLotNumber IS NULL THEN PL7.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL8.SourceLotNumber IS NULL THEN PL8.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL6.SourceLotNumber IS NULL THEN PL9.ReceiptNumber ELSE \r\n                            PL9.ReceiptNumber END \r\n                            FROM Product_LOT PL9 WHERE PL9.LotNumber = PL8.SourceLotNumber) END \r\n                            FROM Product_LOT PL8 WHERE PL8.LotNumber = PL7.SourceLotNumber) END \r\n                            FROM Product_LOT PL7 WHERE PL7.LotNumber = PL6.SourceLotNumber) END \r\n                            FROM Product_LOT PL6 WHERE PL6.LotNumber = PL5.SourceLotNumber) END \r\n                            FROM Product_LOT PL5 WHERE PL5.LotNumber = PL4.SourceLotNumber) END \r\n                            FROM Product_LOT PL4 WHERE PL4.LotNumber = PL3.SourceLotNumber) END \r\n                            FROM Product_LOT PL3 WHERE PL3.LotNumber = PL2.SourceLotNumber) END \r\n                            FROM Product_LOT PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END\r\n\t\t\t\t\t\t\t FROM Product_Lot_Sales PLS\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Lot PL \r\n                        ON  PLS.ItemCode =  PL.ItemCode AND PLS.LotNo = PL.LotNumber\r\n\t\t\t\t\t\tWHERE  ( PLS.LotNo = pl.LotNumber  or PLS.LotNo = pl.SourceLotNumber) \r\n                        and td.ProductID = pls.ItemCode and  PLS.DocID='" + sysDocID + "' AND PLS.InvoiceNumber='" + voucherID + "' ) AS [Consign#]\r\n                        FROM Delivery_Note_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        INNER JOIN Delivery_Note DN ON TD.SysDocID=DN.SysDocID AND TD.VoucherID=DN.VoucherID\r\n                        LEFT OUTER JOIN Product_Brand Brand ON Brand.BrandID = Product.BrandID\r\n                        LEFT OUTER JOIN Sales_Order_Detail SOD ON TD.SourceSysDocID=SOD.SysDocID AND  TD.SourceVoucherID=SOD.VoucherID AND TD.SourceRowIndex=SOD.RowIndex \r\n                        LEFT OUTER JOIN Product_Class PC ON PC.ClassID = Product.ClassID\r\n                         \r\n                        WHERE TD.VoucherID='" + voucherID + "'  AND TD.SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex";
				FillDataSet(deliveryNoteData, "Delivery_Note_Detail", textCommand);
				textCommand = "SELECT * FROM Invoice_DNote\r\n                        WHERE DNoteVoucherID='" + voucherID + "' AND DNoteSysDocID='" + sysDocID + "'";
				FillDataSet(deliveryNoteData, "Invoice_DNote", textCommand);
				textCommand = "SELECT  DISTINCT SourceVoucherID, SourceSysDocID from Delivery_Note_Detail\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(deliveryNoteData, "SourceTableDetails", textCommand);
				textCommand = "SELECT * from Sales_order SO INNER JOIN Delivery_Note_Detail dnd ON SO.SysDocID=dnd.SourceSysDocID AND SO.VoucherID=dnd.SourceVoucherID\r\n\t\t\t\t\t\tWHERE dnd.VoucherID='" + voucherID + "' AND dnd.SysDocID='" + sysDocID + "'";
				FillDataSet(deliveryNoteData, "Sales_Order", textCommand);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (deliveryNoteData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					deliveryNoteData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				deliveryNoteData.Merge(transactionIssuesProductLots, preserveChanges: false);
				return deliveryNoteData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteDeliveryNoteDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				DeliveryNoteData deliveryNoteData = new DeliveryNoteData();
				string textCommand = "SELECT SOD.*,ISVOID,IsExport FROM Delivery_Note_Detail SOD INNER JOIN Delivery_Note SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(deliveryNoteData, "Delivery_Note_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(deliveryNoteData.DeliveryNoteDetailTable.Rows[0]["IsExport"].ToString(), out result);
				bool result2 = false;
				object obj = null;
				obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.AllowESCreatefromPickList, null);
				if (obj != null)
				{
					bool.TryParse(obj.ToString(), out result2);
				}
				string text = "";
				text = ((!result) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text != "")
				{
					salesFlows = (SalesFlows)int.Parse(text.ToString());
				}
				bool result3 = false;
				bool.TryParse(deliveryNoteData.DeliveryNoteDetailTable.Rows[0]["IsVoid"].ToString(), out result3);
				if (!result3)
				{
					if (salesFlows == SalesFlows.SOThenDNThenInvoice)
					{
						flag = ((!result) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(24, sysDocID, voucherID, isDeletingTransaction, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(53, sysDocID, voucherID, isDeletingTransaction, sqlTransaction)));
						string text2 = "";
						string text3 = "";
						string text4 = "";
						foreach (DataRow row in deliveryNoteData.DeliveryNoteDetailTable.Rows)
						{
							text4 = row["ProductID"].ToString();
							text2 = row["SourceVoucherID"].ToString();
							text3 = row["SourceSysDocID"].ToString();
							int result4 = 0;
							if (!(text2 == "") && !(text3 == ""))
							{
								int.TryParse(row["SourceRowIndex"].ToString(), out result4);
								float result5 = 0f;
								if (row["UnitQuantity"] != DBNull.Value)
								{
									float.TryParse(row["UnitQuantity"].ToString(), out result5);
								}
								else
								{
									float.TryParse(row["Quantity"].ToString(), out result5);
								}
								float num = new Products(base.DBConfig).GetReservedQuantity(text4, sqlTransaction) + result5;
								if (num < 0f)
								{
									num = 0f;
								}
								flag &= new Products(base.DBConfig).UpdateReservedQuantity(text4, num, sqlTransaction);
								if (result)
								{
									if (!result2)
									{
										flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text3, text2, result4, -1f * result5, sqlTransaction);
									}
								}
								else
								{
									flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text3, text2, result4, -1f * result5, sqlTransaction);
								}
							}
						}
					}
					else
					{
						string text5 = "";
						string text6 = "";
						if (salesFlows != SalesFlows.SOThenDNThenInvoice)
						{
							foreach (DataRow row2 in deliveryNoteData.DeliveryNoteDetailTable.Rows)
							{
								row2["ProductID"].ToString();
								text5 = row2["SourceVoucherID"].ToString();
								text6 = row2["SourceSysDocID"].ToString();
								int result6 = 0;
								if (!(text5 == "") && !(text6 == ""))
								{
									int.TryParse(row2["SourceRowIndex"].ToString(), out result6);
									float result7 = 0f;
									if (row2["UnitQuantity"] != DBNull.Value)
									{
										float.TryParse(row2["UnitQuantity"].ToString(), out result7);
									}
									else
									{
										float.TryParse(row2["Quantity"].ToString(), out result7);
									}
									flag &= new SalesInvoice(base.DBConfig).UpdateRowShippedQuantity(text6, text5, result6, -1f * result7, sqlTransaction);
									flag &= new SalesInvoice(base.DBConfig).ReOpenInvoice(text6, text5, sqlTransaction);
								}
							}
						}
					}
				}
				textCommand = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Delivery_Note_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
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

		public bool VoidDeliveryNote(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Delivery_Note", "IsExport", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = bool.Parse(fieldValue.ToString());
				}
				bool result = false;
				object obj = null;
				obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.AllowESCreatefromPickList, null);
				if (obj != null)
				{
					bool.TryParse(obj.ToString(), out result);
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
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				string exp = "UPDATE Delivery_Note SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				DeliveryNoteData deliveryNoteData = new DeliveryNoteData();
				exp = "SELECT * FROM Delivery_Note_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(deliveryNoteData, "Delivery_Note_Detail", exp, sqlTransaction);
				if (salesFlows != SalesFlows.SOThenDNThenInvoice)
				{
					string text2 = "";
					string text3 = "";
					foreach (DataRow row in deliveryNoteData.DeliveryNoteDetailTable.Rows)
					{
						row["ProductID"].ToString();
						text2 = row["SourceVoucherID"].ToString();
						text3 = row["SourceSysDocID"].ToString();
						int result2 = 0;
						if (!(text2 == "") && !(text3 == ""))
						{
							int.TryParse(row["SourceRowIndex"].ToString(), out result2);
							float result3 = 0f;
							if (row["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row["UnitQuantity"].ToString(), out result3);
							}
							else
							{
								float.TryParse(row["Quantity"].ToString(), out result3);
							}
							flag &= new SalesInvoice(base.DBConfig).UpdateRowShippedQuantity(text3, text2, result2, -1f * result3, sqlTransaction);
							flag &= new SalesInvoice(base.DBConfig).ReOpenInvoice(text3, text2, sqlTransaction);
						}
					}
				}
				else
				{
					string text4 = "";
					string text5 = "";
					string text6 = "";
					foreach (DataRow row2 in deliveryNoteData.DeliveryNoteDetailTable.Rows)
					{
						text6 = row2["ProductID"].ToString();
						text4 = row2["SourceVoucherID"].ToString();
						text5 = row2["SourceSysDocID"].ToString();
						int result4 = 0;
						if (!(text4 == "") && !(text5 == ""))
						{
							int.TryParse(row2["SourceRowIndex"].ToString(), out result4);
							float result5 = 0f;
							if (row2["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row2["UnitQuantity"].ToString(), out result5);
							}
							else
							{
								float.TryParse(row2["Quantity"].ToString(), out result5);
							}
							float num = new Products(base.DBConfig).GetReservedQuantity(text6, sqlTransaction) + result5;
							if (num < 0f)
							{
								num = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateReservedQuantity(text6, num, sqlTransaction);
							if (flag2)
							{
								if (!result)
								{
									flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text5, text4, result4, -1f * result5, sqlTransaction);
								}
							}
							else
							{
								flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text5, text4, result4, -1f * result5, sqlTransaction);
							}
							if (flag)
							{
								exp = (result ? ("UPDATE Export_PickList SET Status=1 WHERE SysDocID='" + text5 + "' AND VoucherID='" + text4 + "' ") : ("UPDATE Sales_Order SET Status=1 WHERE SysDocID='" + text5 + "' AND VoucherID='" + text4 + "' "));
								flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
							}
						}
					}
					flag = ((!flag2) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(24, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(53, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)));
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
				AddActivityLog("Delivery Note", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteDeliveryNote(string sysDocID, string voucherID)
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
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Delivery_Note", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidDeliveryNote(sysDocID, voucherID, isVoid: true);
				}
				flag &= DeleteDeliveryNoteDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Delivery_Note WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Delivery Note", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetUninvoicedDeliveryNotes(string customerID, bool isExport)
		{
			return GetUninvoicedDeliveryNotes("", customerID, isExport);
		}

		public DataSet GetUninvoicedDeliveryNotes(string sysDocID, string customerID, bool isExport)
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
				string str = "SELECT DN.SysDocID [Doc ID],DN.VoucherID [Number],J.JobName, DN.TransactionDate AS [Date],DN.CustomerID + '-' + C.CustomerName AS [Customer], DN.Reference AS [SO Number]\r\n                              FROM Delivery_Note DN\r\n                              INNER JOIN Delivery_Note_Detail DND ON DND.SysDocID = DN.SysDocID AND DND.VoucherID = DN.VoucherID \r\n                              INNER JOIN Customer C ON DN.CustomerID=C.CustomerID    \r\n\t\t\t\t\t\t\t  LEFT JOIN JOB J ON DN.JobID=J.JobID                         \r\n                              WHERE ISNULL(DN.IsVoid,'False')='False' AND ISNULL(DN.IsInvoiced,'False')='False' AND ISNULL(DN.SalesFlow,0) = 2 \r\n                                 ";
				if (customerID != "")
				{
					str = str + " AND DN.CustomerID='" + customerID + "' ";
				}
				if (!string.IsNullOrEmpty(text))
				{
					str = str + " AND C.CustomerClassID IN (" + text + ") ";
				}
				str += "  GROUP BY DN.SysDocID ,DN.VoucherID, DN.TransactionDate ,DN.CustomerID , C.CustomerName ,J.JobName , DN.Reference \r\n\t\t\t\t\t\t\t  HAVING SUM(Quantity - ISNULL(QuantityReturned,0)) > 0  ";
				FillDataSet(dataSet, "Delivery_Note", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetUninvoicedDeliveryNotesOnLocation(string sysDocID, string customerID, bool isExport, string locationID)
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
				string str = "SELECT DN.SysDocID [Doc ID],DN.VoucherID [Number],J.JobName, DN.TransactionDate AS [Date],DN.CustomerID + '-' + C.CustomerName AS [Customer], DN.Reference AS [SO Number]\r\n                              FROM Delivery_Note DN\r\n                              INNER JOIN Delivery_Note_Detail DND ON DND.SysDocID = DN.SysDocID AND DND.VoucherID = DN.VoucherID \r\n                              INNER JOIN Customer C ON DN.CustomerID=C.CustomerID    \r\n\t\t\t\t\t\t\t  LEFT JOIN JOB J ON DN.JobID=J.JobID \r\n                              INNER JOIN System_Document SD ON SD.SysDocID=DN.SysDocID                         \r\n                              WHERE ISNULL(DN.IsVoid,'False')='False' AND ISNULL(DN.IsInvoiced,'False')='False' AND ISNULL(DN.SalesFlow,0) = 2 \r\n                                 ";
				if (customerID != "")
				{
					str = str + " AND DN.CustomerID='" + customerID + "' ";
				}
				if (!string.IsNullOrEmpty(text))
				{
					str = str + " AND C.CustomerClassID IN (" + text + ") ";
				}
				if (!string.IsNullOrEmpty(locationID))
				{
					str = str + " AND  SD.LocationID ='" + locationID + "' ";
				}
				str += "  GROUP BY DN.SysDocID ,DN.VoucherID, DN.TransactionDate ,DN.CustomerID , C.CustomerName ,J.JobName , DN.Reference \r\n\t\t\t\t\t\t\t  HAVING SUM(Quantity - ISNULL(QuantityReturned,0)) > 0  ";
				FillDataSet(dataSet, "Delivery_Note", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDeliveryNoteToPrint(string sysDocID, string voucherID, bool showLotDetail)
		{
			return GetDeliveryNoteToPrint(sysDocID, new string[1]
			{
				voucherID
			}, showLotDetail);
		}

		public DataSet GetDeliveryNoteToPrint(string sysDocID, string[] voucherID, bool showLotDetail)
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
				bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.ExcludeZeroQtyInDN, false).ToString());
				DataSet dataSet = new DataSet();
				string cmdText = "SELECT DISTINCT SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,CA.ContactName,TransactionDate,'' AS 'GTransactionDate',SI.ContainerNumber,\r\n                            SI.SalesPersonID,SP.FullName,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                            ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                            SI.TermID,TermName,IsVoid,SI.Reference,Discount AS Discount,CA.Phone1,CA.Mobile,CA.Fax,CA.ContactName,\r\n                            Total AS Total,SI.PONumber,SI.Note, SI.InvoiceVoucherID, D.Drivername,D.Note AS [Driver No.],\r\n                            (SELECT TOP 1 RegistrationNumber FROM Vehicle WHERE VehicleID = SI.VehicleID) RegistrationNumber, SI.Reference2,(SELECT  DISTINCT TOP 1 SO.TransactionDate FROM Delivery_Note_Detail DND LEFT OUTER JOIN \r\n                            Sales_Order SO ON DND.SourceSysDocID=SO.SysDocID AND DND.SourceVoucherID=SO.VoucherID\r\n                            WHERE DND.SysDocID=SI.SysDocID AND DND.VoucherID=SI.VoucherID) AS [OrderDate],SI.DateCreated,SI.DateUpdated,\r\n                            SI.CreatedBy,SI.UpdatedBy,V.VehicleName,V.VehicleID,J.JobName,JC.CostCategoryName,P.PortName,SI.VehicleID AS OutVehicle,\r\n                            Customer.TaxIDNumber,SI.ShipToAddress,CA.Comment,CA.AddressName,Customer.ShortName,J.Note [Job Note] \r\n                            FROM  Delivery_Note SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                            LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                            LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                            LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                            LEFT OUTER JOIN Driver D ON D.DriverID=SI.DriverID\r\n                            LEFT OUTER JOIN Vehicle V ON V.VehicleID=SI.VehicleID\r\n                            LEFT JOIN Job J ON J.JobID=SI.JobID\r\n                            LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=SI.CostCategoryID\r\n                            LEFT JOIN Port P ON P.PortID=SI.Port\r\n\t\t\t\t\t\t\tLEFT JOIN Salesperson SP ON SI.SalespersonID=SP.SalespersonID\r\n                            WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Delivery_Note", sqlCommand);
				DateTime dateTime = Convert.ToDateTime(dataSet.Tables["Delivery_Note"].Rows[0]["TransactionDate"].ToString());
				dataSet.Tables["Delivery_Note"].Rows[0]["GTransactionDate"] = dateTime;
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Delivery_Note"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT DISTINCT DND.SysDocID, DND.VoucherID, DND.ProductID, DND.Description, DND.LocationID, ISNULL(DND.UnitQuantity, DND.Quantity) AS Quantity, \r\n                        Product.BrandID, Product.CategoryID,Product.ClassID, Product.QuantityPerUnit,\r\n                        DND.UnitPrice, Product.Size, ISNULL(DND.UnitQuantity, DND.Quantity) * DND.UnitPrice AS Total, DND.UnitID,\r\n                        \r\n                        (SELECT CASE WHEN FactorType='D' THEN ISNULL(UnitQuantity,DND.Quantity)*Factor ELSE ISNULL(UnitQuantity,DND.Quantity)/Factor END FROM Product_Unit PU WHERE PU.UnitID=DND.UnitID AND PU.ProductID= DND.ProductID ) AS Weight,  \r\n                        DND.RowIndex,DND.SysDocID as OrderSysdocID,DND.VoucherID as OrderVoucherID,C.CountryName,\r\n                        PC.CategoryName,J.JobName,JC.CostCategoryName,Product.Description2,DND.Remarks,PB.BrandName,\r\n                        (select TransactionDate from Sales_Order where SysDocID=DND.SourceSysDocID AND VoucherID=DND.SourceVoucherID)AS SODate,DND.SpecificationID, SpecificationName,PST.StyleName [Style],\r\n                        (SELECT TOP 1 PLD.Description  FROM Price_List_Detail PLD INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID \r\n                        WHERE ISNULL(PL.Inactive, 'True') = 'False' AND PL.CustomerID = DN.CustomerID AND  PLD.ProductID = DND.ProductID) CustomerProductDesc, \r\n\r\n                        (SELECT TOP 1 PLD.Remarks  FROM Price_List_Detail PLD INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID \r\n                        WHERE ISNULL(PL.Inactive, 'True') = 'False' AND PL.CustomerID = DN.CustomerID AND  PLD.ProductID = DND.ProductID) PriceListRemarks, Product.UPC,(SELECT TOP 1 PLD.CustomerProductID FROM Price_List_Detail PLD INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID \r\n                             WHERE ISNULL(PL.Inactive, 'True') = 'False' AND PL.CustomerID = DN.CustomerID AND  PLD.ProductID = DND.ProductID) CustomerProductID,Product.Weight,Product.Attribute1\r\n                        FROM   Delivery_Note_Detail DND \r\n                        INNER JOIN Delivery_Note DN on DND.SysDocID=DN.SysDocID AND DND.VoucherID=DN.VoucherID \r\n                        INNER JOIN Product ON DND.ProductID = Product.ProductID\r\n                        LEFT JOIN Product_Brand PB ON Product.BrandID=PB.BrandID\r\n                        LEFT OUTER JOIN Product_Lot_Sales PL ON  Product.ProductID =  PL.ItemCode \r\n                        AND PL.DocID = DND.SysDocID AND PL.InvoiceNumber = DND.VoucherID\r\n                        LEFT  OUTER JOIN Product_Lot PLt ON  PLT.LotNumber =  PL.LotNo\r\n                        LEFT OUTER JOIN Product_Category PC ON PC.CategoryID=Product.CategoryID\r\n                        LEFT OUTER JOIN Country C ON Product.Origin=C.CountryID \r\n                        LEFT JOIN Job J ON J.JobID=DND.JobID\r\n                        LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=DND.CostCategoryID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Specification PS ON PS.SpecificationID=DND.SpecificationID\r\n                        LEFT JOIN Product_Style PST ON PST.StyleID=DND.StyleID\r\n                        WHERE DND.SysDocID='" + sysDocID + "' AND DND.VoucherID IN (" + text + ")  ";
				if (flag)
				{
					cmdText += " AND ISNULL(DND.UnitQuantity, DND.Quantity)>0 ";
				}
				cmdText += " ORDER BY DND.RowIndex";
				FillDataSet(dataSet, "Delivery_Note_Detail", cmdText);
				dataSet.Relations.Add("CustomerDeliveryNote", new DataColumn[2]
				{
					dataSet.Tables["Delivery_Note"].Columns["SysDocID"],
					dataSet.Tables["Delivery_Note"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Delivery_Note_Detail"].Columns["SysDocID"],
					dataSet.Tables["Delivery_Note_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Delivery_Note"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Delivery_Note"].Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					row["TotalInWords"] = NumToWord.GetNumInWords(result);
				}
				if (showLotDetail)
				{
					string text2 = "";
					string text3 = "";
					if (dataSet.Tables["Delivery_Note_Detail"].Rows.Count > 0)
					{
						text2 = dataSet.Tables["Delivery_Note_Detail"].Rows[0]["OrderSysdocID"].ToString();
						text3 = dataSet.Tables["Delivery_Note_Detail"].Rows[0]["OrderVoucherID"].ToString();
					}
					cmdText = "SELECT PL.*,PLS.SoldQty FROM Product_Lot PL LEFT JOIN Product_Lot_Sales PLS ON PL.LotNumber=PLS.LotNo WHERE PLS.DocID=\r\n                            '" + text2 + "' AND PLS.InvoiceNumber IN ('" + text3 + "')";
					FillDataSet(dataSet, "ProductLot", cmdText);
					dataSet.Relations.Add("ProductLotRel", new DataColumn[1]
					{
						dataSet.Tables["Delivery_Note_Detail"].Columns["ProductID"]
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

		public DataSet GetDeliveryNoteToPrint(string sysDocID, string voucherID, bool showLotDetail, bool ExcludeZeroQtyInDN)
		{
			return GetDeliveryNoteToPrint(sysDocID, new string[1]
			{
				voucherID
			}, showLotDetail, ExcludeZeroQtyInDN);
		}

		public DataSet GetDeliveryNoteToPrint(string sysDocID, string[] voucherID, bool showLotDetail, bool ExcludeZeroQtyInDN)
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
				string cmdText = "SELECT DISTINCT SI.SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,CA.Email,CA.ContactName,TransactionDate,SI.ContainerNumber,SD.LocationID SYSLOCID, LocationName AS SYSLocationName,\r\n                            SI.SalesPersonID,SP.FullName,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                            ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                            SI.TermID,TermName,IsVoid,SI.Reference,Discount AS Discount,CA.Phone1,CA.Mobile,CA.Fax,CA.ContactName,\r\n                            Total AS Total,SI.PONumber,SI.PODate, SI.Note, SI.InvoiceVoucherID, D.Drivername,D.Note AS [Driver No.],\r\n                            (SELECT TOP 1 RegistrationNumber FROM Vehicle WHERE VehicleID = SI.VehicleID) RegistrationNumber, SI.Reference2,\r\n\t\t\t\t\t\t\t(SELECT Top 1 TransactionDate FROM Sales_Order WHERE VoucherID =SI.Reference) AS [Order Date],\r\n\t\t\t\t\t\t\tSI.DateCreated,SI.DateUpdated,SI.CreatedBy,SI.UpdatedBy,V.VehicleName,V.VehicleID,J.JobName,JC.CostCategoryName,P.PortName,SI.VehicleID AS OutVehicle,Customer.TaxIDNumber,SI.ShipToAddress,CA.Comment,CA.AddressName,\r\n                            Customer.ShortName,J.Note [Job Note] \r\n                            FROM  Delivery_Note SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                            LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                            LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                            LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                            LEFT OUTER JOIN Driver D ON D.DriverID=SI.DriverID\r\n                            LEFT OUTER JOIN Vehicle V ON V.VehicleID=SI.VehicleID\r\n                            LEFT JOIN Job J ON J.JobID=SI.JobID\r\n                            LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=SI.CostCategoryID\r\n                            LEFT JOIN Port P ON P.PortID=SI.Port\r\n\t\t\t\t\t\t\tLEFT JOIN Salesperson SP ON SI.SalespersonID=SP.SalespersonID\r\n\t\t\t\t\t\t\tLEFT JOIN System_Document SD ON SD.SysDocID=SI.SysDocID\r\n\t\t\t\t\t\t\tLEFT JOIN Location ON SD.LocationID=Location.LocationID\r\n                            WHERE SI.SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Delivery_Note", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Delivery_Note"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT DISTINCT DND.SysDocID, DND.VoucherID, DND.ProductID, DND.Description, DND.LocationID, ISNULL(DND.UnitQuantity, DND.Quantity) AS Quantity, DND.Remarks,\r\n                        Product.BrandID, PB.BrandName, Product.CategoryID,Product.ClassID, Product.QuantityPerUnit,convert(nvarchar(max),Product.Note) AS Note, Product.Description2,\r\n                        DND.UnitPrice, Product.Size, ISNULL(DND.UnitQuantity, DND.Quantity) * DND.UnitPrice AS Total, DND.UnitID,(SELECT TOP 1 CASE WHEN PU.FactorType='M' \r\n                        THEN ('1 '+'  '+ Product.UnitID +'= '+CAST (PU.Factor AS varchar)+' '+PU.UnitID)  \r\n                        ELSE ('1'+'  '+PU.UnitID +'= '+ CAST(PU.Factor AS varchar)+' '+Product.UnitID) END \r\n                        AS PACKING from Product_Unit PU WHERE Product.ProductID=PU.ProductID )  AS [Packing],Product.UnitID AS MainUnit,\r\n                        (SELECT TOP 1  PU.Factor from Product_Unit PU WHERE Product.ProductID=PU.ProductID ) AS Factor,(SELECT TOP 1  PU.UnitID from Product_Unit PU WHERE Product.ProductID=PU.ProductID ) AS SubUnit,\r\n                        (select TransactionDate from Sales_Order where SysDocID=DND.SourceSysDocID AND VoucherID=DND.SourceVoucherID)AS SODate,\r\n                        (SELECT CASE WHEN FactorType='D' THEN ISNULL(UnitQuantity,DND.Quantity)*Factor ELSE ISNULL(UnitQuantity,DND.Quantity)/Factor END FROM Product_Unit PU WHERE PU.UnitID=DND.UnitID AND PU.ProductID= DND.ProductID ) AS Weight,  \r\n                        DND.RowIndex,DND.SourceSysDocID as OrderSysdocID,DND.SourceVoucherID as OrderVoucherID,C.CountryName,PC.CategoryName,J.JobName,JC.CostCategoryName,F.GenericListName as Finish,CAST(Product.photo AS VARBINARY(Max)) AS photo\r\n                        ,(SELECT TOP 1 PLD.Description  FROM Price_List_Detail PLD INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID \r\n                        WHERE ISNULL(PL.Inactive, 'True') = 'False' AND PL.CustomerID = DN.CustomerID AND  PLD.ProductID = DND.ProductID) CustomerProductDesc, \r\n\r\n                        (SELECT TOP 1 PLD.Remarks  FROM Price_List_Detail PLD INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID \r\n                        WHERE ISNULL(PL.Inactive, 'True') = 'False' AND PL.CustomerID = DN.CustomerID AND  PLD.ProductID = DND.ProductID) PriceListRemarks, Product.UPC,DND.RefSlNo,\r\n                        DND.RefText1,DND.RefText2,DND.RefNum1,DND.RefNum2,DND.RefDate1,DND.RefDate2,Product.Attribute1,Product.Attribute2\r\n                        FROM   Delivery_Note_Detail DND\r\n                        INNER JOIN Delivery_Note DN on DND.SysDocID=DN.SysDocID AND DND.VoucherID=DN.VoucherID \r\n                        INNER JOIN Product ON DND.ProductID = Product.ProductID\r\n                        LEFT JOIN Product_Brand PB ON Product.BrandID=PB.BrandID\r\n                        LEFT OUTER JOIN Product_Lot_Sales PL ON  Product.ProductID =  PL.ItemCode \r\n                        AND PL.DocID = DND.SysDocID AND PL.InvoiceNumber = DND.VoucherID\r\n                        LEFT  OUTER JOIN Product_Lot PLt ON  PLT.LotNumber =  PL.LotNo\r\n                        LEFT OUTER JOIN Product_Category PC ON PC.CategoryID=Product.CategoryID\r\n                        LEFT OUTER JOIN Country C ON Product.Origin=C.CountryID \r\n                        LEFT JOIN Job J ON J.JobID=DND.JobID\r\n                        LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=DND.CostCategoryID\r\n                        LEFT JOIN (SELECT GenericListID,GenericListName FROM Generic_List WHERE GenericListtYPE=33) AS F ON Product.FinishingID= F.GenericListID\r\n                        WHERE DND.SysDocID='" + sysDocID + "' AND DND.VoucherID IN (" + text + ") ";
				if (ExcludeZeroQtyInDN)
				{
					cmdText += " AND ISNULL(DND.UnitQuantity, DND.Quantity)>0 ";
				}
				cmdText += " ORDER BY DND.RowIndex";
				FillDataSet(dataSet, "Delivery_Note_Detail", cmdText);
				dataSet.Relations.Add("CustomerDeliveryNote", new DataColumn[2]
				{
					dataSet.Tables["Delivery_Note"].Columns["SysDocID"],
					dataSet.Tables["Delivery_Note"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Delivery_Note_Detail"].Columns["SysDocID"],
					dataSet.Tables["Delivery_Note_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Delivery_Note"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Delivery_Note"].Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					row["TotalInWords"] = NumToWord.GetNumInWords(result);
				}
				if (showLotDetail)
				{
					cmdText = "SELECT PL.*,PLS.SoldQty,PLS.LotNo FROM Product_Lot PL LEFT JOIN Product_Lot_Sales PLS ON PL.LotNumber=PLS.LotNo WHERE PLS.DocID=\r\n                            '" + sysDocID + "' AND PLS.InvoiceNumber IN (" + text + ")";
					FillDataSet(dataSet, "ProductLot", cmdText);
					dataSet.Relations.Add("ProductLotRel", new DataColumn[1]
					{
						dataSet.Tables["Delivery_Note_Detail"].Columns["ProductID"]
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

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			string exp = "SELECT COUNT(*) FROM Invoice_DNote ID WHERE SourceDocType = 6 AND DnoteSysDocID = '" + sysDocID + "' AND DNOTEVoucherID = '" + voucherNumber + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			exp = "SELECT COUNT(*) FROM Delivery_Note WHERE (ISNULL(IsInvoiced,'False')= 'True' OR ISNULL(IsShipped,'False')='True') AND (SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherNumber + "')";
			obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			exp = "SELECT COUNT(*) FROM Delivery_Note WHERE (ISNULL(IsInvoiced,'False')= 'True' OR ISNULL(IsShipped,'False')='True') AND (SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherNumber + "')";
			obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			exp = "SELECT ISNULL(SUM(ISNULL(QuantityReturned,0) + ISNULL(QuantityShipped,0)),0) AS Quantity FROM Delivery_Note_Detail\r\n                        WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherNumber + "'";
			obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && decimal.Parse(obj.ToString()) != 0m)
			{
				return false;
			}
			return true;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT    ISNULL(IsVoid,'False') AS V, INV.SysDocID [Doc ID],INV.VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Delivery Date],\r\n                            INV.SalespersonID [Salesperson],SUM(DND.Quantity) AS Quantity,INV.CurrencyID[Currency] ,J.JobID,J.JobName, INV.Reference AS Ref1,INV.Reference2 AS Ref2,ISNULL((CASE INV.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' ELSE  'NON TAXABLE'  END) ,(CASE Customer.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' ELSE  'NON TAXABLE'  END))AS TAXOPTION\r\n                            FROM         Delivery_note INV\r\n                            INNER JOIN Delivery_Note_Detail DND ON DND.SysDocID=INV.SysDocID AND DND.VoucherID=INV.VoucherID\r\n                            LEFT JOIN Job J ON INV.JobID=J.JobID\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			if (sysDocID != "")
			{
				text3 = text3 + " AND INV.SysDocID = '" + sysDocID + "'";
			}
			text3 += " GROUP BY IsVoid,INV.SysDocID,INV.VoucherID,INV.CustomerID ,CustomerName,TransactionDate,INV.CurrencyID,INV.SalespersonID, INV.Reference, INV.Reference2,J.JobID,J.JobName,INV.TaxOption,Customer.TaxOption";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Delivery_Note", sqlCommand);
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
				FillDataSet(dataSet, "Delivery_Note", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDOsForShipment(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT DO.SysDocID [Doc ID],DO.VoucherID [Number], TransactionDate AS [Date],DO.CustomerID + '-' + CUS.CustomerName AS Customer \r\n                                FROM Delivery_Note DO\r\n                                INNER JOIN Customer CUS ON DO.CustomerID=CUS.CustomerID  \r\n                                LEFT OUTER JOIN shipping_method SM ON DO.ShippingMethodID=SM.ShippingMethodID\r\n                                WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsShipped,'False')='False'  AND ISNULL(SM.TrackShipment,'False')='True' \r\n                               ";
				if (customerID != "")
				{
					text = text + " AND (DO.CustomerID = '" + customerID + "')";
				}
				FillDataSet(dataSet, "Delivery_Note", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDOsForShipment(string sysDocID, string customerID, string locationID)
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
				string text2 = "SELECT DO.SysDocID [Doc ID],DO.VoucherID [Number], TransactionDate AS [Date],DO.CustomerID + '-' + CUS.CustomerName AS Customer \r\n                                FROM Delivery_Note DO\r\n                                INNER JOIN Customer CUS ON DO.CustomerID=CUS.CustomerID  \r\n                                 INNER JOIN System_Document SD ON SD.SysDocID=DO.SysDocID\r\n                                WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsShipped,'False')='False'  \r\n                               ";
				if (customerID != "")
				{
					text2 = text2 + " AND (DO.CustomerID = '" + customerID + "')";
				}
				if (!string.IsNullOrEmpty(text))
				{
					text2 = text2 + " AND CUS.CustomerClassID IN (" + text + ") ";
				}
				if (!string.IsNullOrEmpty(locationID))
				{
					text2 = text2 + " AND  SD.LocationID ='" + locationID + "' ";
				}
				FillDataSet(dataSet, "Delivery_Note", text2);
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
				string textCommand = "SELECT DO.customerID, DOD.SysDocID [Doc ID], DOD.VoucherID,ProductID,Description,DOD.UnitID,RowIndex,UnitQuantity,LocationID,\r\n                                ISNULL(UnitQuantity,Quantity) - ISNULL(QuantityReturned,0) AS Quantity,ISNULL(QuantityShipped,0) AS QuantityShipped,DOD.Remarks, DOD.SpecificationID, DOD.StyleID\r\n                                FROM Delivery_Note_Detail DOD INNER JOIN Delivery_Note DO ON DO.SysDocID=DOD.SysDocID AND DO.VoucherID=DOD.VoucherID                              \r\n                             WHERE DOD.SysDocID='" + sysDocID + "' AND DOD.VoucherID='" + voucherID + "'";
				FillDataSet(dataSet, "Delivery_Note_Detail", textCommand);
				textCommand = "select ProductID,ISNULL(Sum(Quantity),0) as ShippedQuantity from Export_PackingList_Detail EPD\r\n                         WHERE EPD.SourceSysdocID='" + sysDocID + "' and EPD.SourceVoucherID='" + voucherID + "' GROUP BY ProductID ";
				FillDataSet(dataSet, "Shipped_Details", textCommand);
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
				FillDataSet(dataSet, "Delivery_Note_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool ModifyTransactions(string sysDocID, string voucherID, string userID, bool isModify, string toUpdate)
		{
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (isModify)
				{
					isModify = true;
					object obj = null;
					if (toUpdate == "")
					{
						text = "INSERT INTO Modify_Transactions  Values( '" + sysDocID + "' , '" + voucherID + "', '" + userID + "', '" + isModify.ToString() + "')";
						obj = ExecuteScalar(text, sqlTransaction);
						base.DBConfig.EndTransaction(result: true);
					}
					if (toUpdate != "")
					{
						isModify = false;
						text = "UPDATE Modify_Transactions   SET IsModify='" + isModify.ToString() + "' WHERE  SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'AND UserID='" + userID + "'";
						bool result = ExecuteNonQuery(text, sqlTransaction) > 0;
						base.DBConfig.EndTransaction(result: true);
						return result;
					}
					if (obj == null || int.Parse(obj.ToString()) > 0)
					{
						return true;
					}
					return false;
				}
				text = "SELECT  COUNT(ISNull(IsModify,0)) FROM Modify_Transactions  WHERE IsModify='1' AND SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND UserID='" + userID + "'";
				object obj2 = ExecuteScalar(text, sqlTransaction);
				base.DBConfig.EndTransaction(result: true);
				if (obj2 == null || int.Parse(obj2.ToString()) > 0)
				{
					return true;
				}
				return false;
			}
			catch
			{
				base.DBConfig.EndTransaction(result: true);
				return false;
			}
		}

		public bool ReCostTransaction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				DeliveryNoteData deliveryNoteByID = GetDeliveryNoteByID(sysDocID, voucherID);
				object obj = null;
				DataRow dataRow = deliveryNoteByID.DeliveryNoteTable.Rows[0];
				bool flag2 = false;
				if (dataRow["IsExport"] != DBNull.Value)
				{
					flag2 = bool.Parse(dataRow["IsExport"].ToString());
				}
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					byte.Parse(dataRow["SourceDocType"].ToString());
				}
				string text = "";
				text = ((!flag2) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				bool result = false;
				object obj2 = null;
				obj2 = new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.AllowESCreatefromPickList, null);
				if (obj2 != null)
				{
					bool.TryParse(obj2.ToString(), out result);
				}
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text != "")
				{
					salesFlows = (SalesFlows)int.Parse(text.ToString());
				}
				_ = 2;
				GLData gLData = CreateDNGLData(deliveryNoteByID, sqlTransaction);
				string exp = "SELECT JournalID FROM Journal WHERE SysDocID = '" + sysDocID + "' AND VOucherID = '" + voucherID + "'";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj.IsDBNullOrEmpty())
				{
					throw new CompanyException("JournalID not found for invoice '" + voucherID + "'");
				}
				int.Parse(obj.ToString());
				if (!new Journal(base.DBConfig).IsJournalInOpenPeriod(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Transaction date is in a period which is locked or closed.");
				}
				exp = " SELECT JournalID,JournalDetailID,SysDocID,VoucherID,JD.AccountID,AC.AccountName,Debit,Credit FROM Journal_Details JD INNER JOIN Account AC ON JD.AccountID = AC.AccountID\r\n                         WHERE AC.subType IN (6, 8) AND SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Invoice", exp, sqlTransaction);
				if (dataSet.Tables[0].Rows.Count != 2)
				{
					throw new CompanyException("Could not match the COGS and Inventory Asset accounts.");
				}
				decimal d = default(decimal);
				foreach (DataRow row in dataSet.Tables[0].Rows)
				{
					int num = int.Parse(row["JournalDetailID"].ToString());
					string text2 = row["AccountID"].ToString();
					DataRow[] array = gLData.JournalDetailsTable.Select("AccountID = '" + text2 + "'");
					if (array.Length == 0)
					{
						throw new CompanyException("Accounts not found. maybe changed.");
					}
					DataRow dataRow2 = array[0];
					if (!dataRow2["Debit"].IsDBNullOrEmpty())
					{
						d += decimal.Parse(dataRow2["Debit"].ToString());
					}
					if (!dataRow2["Credit"].IsDBNullOrEmpty())
					{
						d -= decimal.Parse(dataRow2["Credit"].ToString());
					}
					exp = " UPDATE Journal_Details SET Debit = ";
					exp = ((!dataRow2["Debit"].IsDBNullOrEmpty()) ? (exp + dataRow2["Debit"].ToString()) : (exp + " NULL "));
					exp = ((!dataRow2["Credit"].IsDBNullOrEmpty()) ? (exp + " , Credit = " + dataRow2["Credit"].ToString()) : (exp + " , Credit = NULL "));
					exp = exp + "  WHERE JournalDetailID = " + num + " and AccountID = '" + text2 + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				bool flag3 = new Journal(base.DBConfig).IsJournalInBalance(sysDocID, voucherID, sqlTransaction);
				if (d != 0m || !flag3)
				{
					flag = false;
					throw new CompanyException("Debit and credit not in balance.");
				}
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
