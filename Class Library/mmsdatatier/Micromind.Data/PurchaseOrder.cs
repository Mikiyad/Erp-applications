using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PurchaseOrder : StoreObject
	{
		private const string PURCHASEORDER_TABLE = "Purchase_Order";

		private const string PURCHASEORDERDETAIL_TABLE = "Purchase_Order_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string VENDORID_PARM = "@VendorID";

		private const string PURCHASEFLOW_PARM = "@PurchaseFlow";

		private const string ISIMPORT_PARM = "@IsImport";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string BUYERID_PARM = "@BuyerID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string DELIVERYADDRESSID_PARM = "@DeliveryAddressID";

		private const string VENDORADDRESS_PARM = "@VendorAddress";

		private const string BILLINGADDRESSID_PARM = "@BillingAddressID";

		private const string DELIVERYADDRESS_PARM = "@DeliveryAddress";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string PRICEINCLUDETAX_PARM = "@PriceIncludeTax";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE_PARM2 = "@Reference2";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TOTAL_PARM = "@Total";

		private const string DUEDATE_PARM = "@DueDate";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

		private const string PORTLOADING_PARM = "@PortLoading";

		private const string PORTDESTINATION_PARM = "@PortDestination";

		private const string ETA_PARM = "@ETA";

		private const string ETD_PARM = "@ETD";

		private const string ACTUALREQDATE_PARM = "@ActualReqDate";

		private const string INCOID_PARM = "@INCOID";

		private const string REMARKS1_PARM = "@Remarks1";

		private const string REMARKS2_PARM = "@Remarks2";

		private const string COSTCATEGORY_PARM = "@CostCategoryID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string VENDORREFERENCENUMBER_PARM = "@VendorReferenceNo";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string MINGUARANTEE_PARM = "@MinGuarantee";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string LENGTH_PARM = "@Length";

		private const string WIDTH_PARM = "@Width";

		private const string HEIGHT_PARM = "@Height";

		private const string NUMBER_PARM = "@Number";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string ISSOURCEDROW_PARM = "@IsSourcedRow";

		private const string JOBID_PARM = "@JobID";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXPERCENTAGE_PARM = "@TaxPercentage";

		private const string REMARKS_PARM = "@Remarks";

		private const string REFSLNO_PARM = "@RefSlNo";

		private const string REFTEXT1_PARM = "@RefText1";

		private const string REFTEXT2_PARM = "@RefText2";

		private const string REFNUM1_PARM = "@RefNum1";

		private const string REFNUM2_PARM = "@RefNum2";

		private const string REFDATE1_PARM = "@RefDate1";

		private const string REFDATE2_PARM = "@RefDate2";

		public PurchaseOrder(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePurchaseOrderText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Order", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("PurchaseFlow", "@PurchaseFlow"), new FieldValue("DueDate", "@DueDate"), new FieldValue("IsImport", "@IsImport"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("DeliveryAddressID", "@DeliveryAddressID"), new FieldValue("BillingAddressID", "@BillingAddressID"), new FieldValue("DeliveryAddress", "@DeliveryAddress"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("VendorAddress", "@VendorAddress"), new FieldValue("Status", "@Status"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Discount", "@Discount"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Total", "@Total"), new FieldValue("PONumber", "@PONumber"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("Remarks1", "@Remarks1"), new FieldValue("Remarks2", "@Remarks2"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("PortLoading", "@PortLoading"), new FieldValue("PortDestination", "@PortDestination"), new FieldValue("ETA", "@ETA"), new FieldValue("ETD", "@ETD"), new FieldValue("ActualReqDate", "@ActualReqDate"), new FieldValue("INCOID", "@INCOID"), new FieldValue("JobID", "@JobID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("VendorReferenceNo", "@VendorReferenceNo"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Purchase_Order", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseOrderCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseOrderText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseOrderText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@PurchaseFlow", SqlDbType.TinyInt);
			parameters.Add("@IsImport", SqlDbType.Bit);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@BuyerID", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@DeliveryAddressID", SqlDbType.NVarChar);
			parameters.Add("@BillingAddressID", SqlDbType.NVarChar);
			parameters.Add("@DeliveryAddress", SqlDbType.NVarChar);
			parameters.Add("@VendorAddress", SqlDbType.NVarChar);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@Remarks1", SqlDbType.NVarChar);
			parameters.Add("@Remarks2", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@PortLoading", SqlDbType.NVarChar);
			parameters.Add("@PortDestination", SqlDbType.NVarChar);
			parameters.Add("@ETA", SqlDbType.DateTime);
			parameters.Add("@ETD", SqlDbType.DateTime);
			parameters.Add("@ActualReqDate", SqlDbType.DateTime);
			parameters.Add("@INCOID", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters.Add("@VendorReferenceNo", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@PurchaseFlow"].SourceColumn = "PurchaseFlow";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@IsImport"].SourceColumn = "IsImport";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@DeliveryAddressID"].SourceColumn = "DeliveryAddressID";
			parameters["@BillingAddressID"].SourceColumn = "BillingAddressID";
			parameters["@DeliveryAddress"].SourceColumn = "DeliveryAddress";
			parameters["@VendorAddress"].SourceColumn = "VendorAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@PriceIncludeTax"].SourceColumn = "PriceIncludeTax";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@Remarks1"].SourceColumn = "Remarks1";
			parameters["@Remarks2"].SourceColumn = "Remarks2";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@PortLoading"].SourceColumn = "PortLoading";
			parameters["@PortDestination"].SourceColumn = "PortDestination";
			parameters["@ETA"].SourceColumn = "ETA";
			parameters["@ETD"].SourceColumn = "ETD";
			parameters["@ActualReqDate"].SourceColumn = "ActualReqDate";
			parameters["@INCOID"].SourceColumn = "INCOID";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@VendorReferenceNo"].SourceColumn = "VendorReferenceNo";
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

		private string GetInsertUpdatePurchaseOrderDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Order_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("IsSourcedRow", "@IsSourcedRow"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("MinGuarantee", "@MinGuarantee"), new FieldValue("TaxPercentage", "@TaxPercentage"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("Remarks", "@Remarks"), new FieldValue("Length", "@Length"), new FieldValue("Width", "@Width"), new FieldValue("Height", "@Height"), new FieldValue("Number", "@Number"), new FieldValue("RefSlNo", "@RefSlNo"), new FieldValue("RefText1", "@RefText1"), new FieldValue("RefText2", "@RefText2"), new FieldValue("RefNum1", "@RefNum1"), new FieldValue("RefNum2", "@RefNum2"), new FieldValue("RefDate1", "@RefDate1"), new FieldValue("RefDate2", "@RefDate2"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseOrderDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseOrderDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseOrderDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@MinGuarantee", SqlDbType.Decimal);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@IsSourcedRow", SqlDbType.Bit);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxPercentage", SqlDbType.Decimal);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@Length", SqlDbType.Decimal);
			parameters.Add("@Width", SqlDbType.Decimal);
			parameters.Add("@Height", SqlDbType.Decimal);
			parameters.Add("@Number", SqlDbType.Decimal);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
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
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@MinGuarantee"].SourceColumn = "MinGuarantee";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@IsSourcedRow"].SourceColumn = "IsSourcedRow";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@TaxPercentage"].SourceColumn = "TaxPercentage";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@Length"].SourceColumn = "Length";
			parameters["@Width"].SourceColumn = "Width";
			parameters["@Height"].SourceColumn = "Height";
			parameters["@Number"].SourceColumn = "Number";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
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

		private bool ValidateData(PurchaseOrderData journalData)
		{
			return true;
		}

		public int CanUpdate(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Purchase_Order_Detail POD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityReceived,0))>0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return 1;
			}
			exp = "SELECT SUM(Amount) FROM Purchase_Prepayment_Invoice PPI WHERE PPI.SourceSysDocID = '" + sysDocID + "' AND PPI.SourceVoucherID = '" + voucherNumber + "'";
			obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return 2;
			}
			exp = "Select Count(*) FROM Purchase_Order_Detail POD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityShipped,0))>0";
			obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return 1;
			}
			return 0;
		}

		public bool InsertUpdatePurchaseOrder(PurchaseOrderData purchaseOrderData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePurchaseOrderCommand = GetInsertUpdatePurchaseOrderCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = purchaseOrderData.PurchaseOrderTable.Rows[0];
				string text = "";
				string text2 = "";
				string text3 = "";
				bool flag2 = bool.Parse(dataRow["AllowPOEdit"].ToString());
				bool flag3 = false;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text4 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (!dataRow["IsImport"].IsDBNullOrEmpty())
				{
					flag3 = bool.Parse(dataRow["IsImport"].ToString());
				}
				if (isUpdate && !flag2)
				{
					switch (CanUpdate(sysDocID, text4, sqlTransaction))
					{
					case 1:
						throw new CompanyException("Unable to update. Some of the items in this order has been already received.", 1037);
					case 2:
						throw new CompanyException("This order has one or more prepayments.", 1062);
					}
				}
				decimal num = default(decimal);
				foreach (DataRow row in purchaseOrderData.PurchaseOrderDetailTable.Rows)
				{
					decimal num2 = default(decimal);
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result);
					decimal.TryParse(row["UnitPrice"].ToString(), out result2);
					num2 = Math.Round(result * result2, currencyDecimalPoints);
					num += num2;
				}
				dataRow["Total"] = num;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Purchase_Order", "VoucherID", dataRow["SysDocID"].ToString(), text4, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row2 in purchaseOrderData.PurchaseOrderDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					text3 = row2["ProductID"].ToString();
					string text5 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text3, sqlTransaction);
					if (fieldValue != null)
					{
						text5 = fieldValue.ToString();
					}
					if (text5 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text5)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text3, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text3 + "\nUnit:" + row2["UnitID"].ToString());
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
				insertUpdatePurchaseOrderCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(purchaseOrderData, "Purchase_Order", insertUpdatePurchaseOrderCommand)) : (flag & Insert(purchaseOrderData, "Purchase_Order", insertUpdatePurchaseOrderCommand)));
				if (flag2)
				{
					if (isUpdate)
					{
						flag &= UpdatePODetails(purchaseOrderData, sqlTransaction);
					}
					flag &= InsertPODetails(purchaseOrderData, sqlTransaction);
				}
				insertUpdatePurchaseOrderCommand = GetInsertUpdatePurchaseOrderDetailsCommand(isUpdate: false);
				insertUpdatePurchaseOrderCommand.Transaction = sqlTransaction;
				if (isUpdate && !flag2)
				{
					flag &= DeletePurchaseOrderDetailsRows(sysDocID, text4, sqlTransaction);
				}
				if (purchaseOrderData.Tables["Purchase_Order_Detail"].Rows.Count > 0 && !flag2)
				{
					flag &= Insert(purchaseOrderData, "Purchase_Order_Detail", insertUpdatePurchaseOrderCommand);
				}
				foreach (DataRow row3 in purchaseOrderData.PurchaseOrderDetailTable.Rows)
				{
					text3 = row3["ProductID"].ToString();
					float num5 = float.Parse(row3["Quantity"].ToString());
					float quantity = new Products(base.DBConfig).GetOrderedQuantity(text3, sqlTransaction) + num5;
					flag &= new Products(base.DBConfig).UpdateOrderedQuantity(text3, quantity, sqlTransaction);
				}
				if (itemSourceTypes == ItemSourceTypes.PurchaseQuote)
				{
					foreach (DataRow row4 in purchaseOrderData.PurchaseOrderDetailTable.Rows)
					{
						text3 = row4["ProductID"].ToString();
						object obj4 = row4["SourceDocType"].ToString();
						if (!(obj4.ToString() == ""))
						{
							itemSourceTypes = (ItemSourceTypes)byte.Parse(obj4.ToString());
							if (itemSourceTypes == ItemSourceTypes.PurchaseQuote)
							{
								text = row4["SourceVoucherID"].ToString();
								text2 = row4["SourceSysDocID"].ToString();
								int result3 = 0;
								if (!(text == "") && !(text2 == ""))
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
									flag &= new PurchaseQuote(base.DBConfig).UpdateRowOrderedQuantity(text2, text, result3, result4, sqlTransaction);
								}
							}
						}
					}
					if (text != "")
					{
						flag &= new PurchaseQuote(base.DBConfig).CloseOrderedQuote(text2, text, sqlTransaction);
					}
				}
				if (purchaseOrderData.Tables.Contains("Tax_Detail") && purchaseOrderData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(purchaseOrderData, sysDocID, text4, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Purchase_Order", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Purchase Order";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text4, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text4, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Purchase_Order", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				if (!flag3)
				{
					new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PurchaseOrder, sysDocID, text4, "Purchase_Order", sqlTransaction);
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ImportPurchaseOrder, sysDocID, text4, "Purchase_Order", sqlTransaction);
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

		public DataSet GetPOPaymentSummary(string sysDocID, string voucherID, string RequestsysDocID, string RequestvoucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT PO.CurrencyID, PO.TermID,PO.Total,PO.ETA, ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) FROM Payment_Request PR WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID AND PR.VoucherID <'" + RequestvoucherID + "' ),0) AS PaidAmount                                           \r\n                                FROM Purchase_Order PO  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Purchase_Order", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public PurchaseOrderData GetPurchaseOrderByID(string sysDocID, string voucherID)
		{
			try
			{
				PurchaseOrderData purchaseOrderData = new PurchaseOrderData();
				string textCommand = "SELECT PO.*,PT.TermName, ISNULL((SELECT SUM(Amount) FROM Payment_Request WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS PaidAmount ,\r\n                                ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) FROM Purchase_Prepayment_Invoice PPI WHERE PPI.SourceSysDocID = PO.SysDocID AND PPI.SourceVoucherID = PO.VoucherID),0) AS PrePaidAmount,\r\n                                CASE WHEN (SELECT COUNT(*) FROM PO_Shipment_Detail PR \r\n                                                                WHERE PO.SysDocID = PR.SourceSysDocID AND PO.VoucherID = PR.SourceVoucherID)>0 THEN 'True' ELSE 'False' END  AS IsPOShipped ,T.SysDocID AS PKSysDocID,T.VoucherID AS PKVoucherID\r\n                                FROM Purchase_Order PO LEFT OUTER JOIN (SELECT TOP 1 SysDocID,VoucherID,SourceSysDocID,SourceVoucherID FROM PO_Shipment_Detail POS\r\n                                WHERE  POS.SourceSysDocID = '" + sysDocID + "' AND   POS.SourceVoucherID = '" + voucherID + "') T  ON T.SourceSysDocID = PO.SysDocID AND T.SourceVoucherID = PO.VoucherID \r\n                                LEFT OUTER JOIN Payment_Term PT ON PT.PaymentTermID = PO.TermID\r\n\t\t\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Purchase_Order", textCommand);
				if (purchaseOrderData == null || purchaseOrderData.Tables.Count == 0 || purchaseOrderData.Tables["Purchase_Order"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*, Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID, Product.IsTrackLot, Product.IsTrackSerial,ISNull(Product.TaxGroupID,PC.TaxGroupID) AS TaxGroupID\r\n                        ,'True' AS IsNonEdit FROM Purchase_Order_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        LEFT OUTER JOIN Product_Class PC ON PC.ClassID = Product.ClassID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(purchaseOrderData, "Purchase_Order_Detail", textCommand);
				textCommand = "SELECT * FROM Purchase_Receipt PR  INNER JOIN Purchase_Order PO ON PO.SysDocID=PR.SourceSysDocID AND PO.VoucherID=PR.SourceVoucherID\r\n\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Purchase_Receipt", textCommand);
				textCommand = "SELECT DISTINCT JMR.VoucherID,JMR.* FROM Job_Material_Requisition JMR INNER JOIN Purchase_Order_Detail POD ON JMR.VoucherID=POD.SourceVoucherID AND JMR.SysDocID=POD.SourceSysDocID WHERE POD.VoucherID='" + voucherID + "' AND POD.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Job_Material_Requisition", textCommand);
				textCommand = "SELECT DISTINCT SOD.VoucherID,SOD.SysDocID FROM Sales_Order_Detail SOD INNER JOIN Purchase_Order_Detail POD ON SOD.VoucherID=POD.SourceVoucherID AND SOD.SysDocID=POD.SourceSysDocID WHERE POD.VoucherID='" + voucherID + "' AND POD.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Sales_Order_Detail", textCommand);
				textCommand = "SELECT DISTINCT PID.VoucherID,PID.SysDocID FROM Purchase_Invoice_Detail PID INNER JOIN Purchase_Order_Detail POD ON POD.VoucherID=PID.OrderVoucherID AND POD.SysDocID=PID.OrderSysDocID WHERE POD.VoucherID='" + voucherID + "' AND POD.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Purchase_Invoice_Detail", textCommand);
				textCommand = "SELECT DISTINCT PQD.VoucherID,PQD.SysDocID FROM Purchase_Quote_Detail PQD INNER JOIN Purchase_Order_Detail POD ON PQD.VoucherID=POD.SourceVoucherID AND PQD.SysDocID=POD.SourceSysDocID WHERE POD.VoucherID='" + voucherID + "' AND POD.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Purchase_Quote_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Tax_Detail", textCommand);
				return purchaseOrderData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseOrderDetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool openOrders, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "SELECT DISTINCT PO.TransactionDate,PO.VoucherID,V.VendorName,J.JobName,POD.UnitPrice,POD.ProductID,POD.UnitID,POD.Description,POD.Quantity,\r\n                                POD.QuantityReceived,POD.UnitPrice, PO.LocationID, L.LocationName,PO.CurrencyID,PO.CurrencyRate,Cur.CurrencyID AS BaseCurrencyID \r\n                                FROM Purchase_Order PO LEFT JOIN Purchase_Order_Detail POD ON PO.SysDocID=POD.SysDocID AND PO.VoucherID=POD.VoucherID\r\n                                LEFT JOIN JOB J ON J.JobID=POD.JobID LEFT JOIN Vendor V ON V.VendorID=PO.VendorID LEFT JOIN Product P ON POD.ProductID=P.ProductID\r\n                                 LEFT JOIN Location L ON PO.LocationID=L.LocationID\r\n                                LEFT JOIN Product ON POD.ProductID=Product.ProductID \r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True' \r\n                                    ";
				text3 = text3 + "WHERE PO.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND ISNULL(PO.IsVoid,'False')='False' ";
				if (jobID != "")
				{
					text3 = text3 + " AND POD.JobID='" + jobID + "'";
				}
				if (vendorID != "")
				{
					text3 = text3 + " AND PO.VendorID= '" + vendorID + "'";
				}
				if (fromItem != "")
				{
					text3 = text3 + " AND POD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE BrandId BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND PO.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				if (openOrders)
				{
					text3 += " AND PO.Status=1";
				}
				FillDataSet(dataSet, "Purchase_Order", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPOsForPackingList(string vendorID, bool isImport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT PO.SysDocID [Doc ID],PO.VoucherID [Number],V.ParentVendorID, TransactionDate AS [Date],PO.VendorID + '-' + V.VendorName AS [Vendor] FROM Purchase_Order PO\r\n                             INNER JOIN Vendor V ON PO.VendorID=V.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsShipped,'False')='False'  AND Status = 1 AND ISNULL(IsImport,'False')='" + isImport.ToString() + "'";
				if (vendorID != "")
				{
					text = text + " AND (PO.VendorID='" + vendorID + "' OR V.ParentVendorID = '" + vendorID + "')";
				}
				FillDataSet(dataSet, "Purchase_Order", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public decimal GetPODueAmount(string sysDocID, string voucherID)
		{
			try
			{
				DateTime dateTime = DateTime.MinValue;
				decimal d = default(decimal);
				DataSet dataSet = new DataSet();
				DataSet dataSet2 = new DataSet();
				string textCommand = "SELECT PaymentTermID, ISNULL(isInstallments,'False') AS Ignore ,NetDays,100 AS Percentage,TermType FROM Payment_Term PT INNER JOIN Purchase_Order PO \r\n                                    ON PO.TermID = PT.PaymentTermID WHERE PO.SysDocID = '" + sysDocID + "' AND PO.VoucherID = '" + voucherID + "'\r\n                                    UNION\r\n                                    SELECT PaymentTermID, 'False' AS Ignore ,Days AS NetDays,Percentage,TermType FROM Payment_Term_Installment PTI INNER JOIN Purchase_Order PO \r\n                                    ON PO.TermID = PTI.PaymentTermID  WHERE PO.SysDocID = '" + sysDocID + "' AND PO.VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Term", textCommand);
				decimal d2 = default(decimal);
				DataRow[] array = dataSet.Tables["Term"].Select("TermType = " + 3);
				if (array.Length != 0)
				{
					textCommand = "SELECT PO.TransactionDate PODate, Total POTotal,BL.TransactionDate BLDate,ATD,BL.ETA FROM Purchase_Order PO \r\n                            LEFT OUTER JOIN Bill_Of_Lading BL ON PO.SysDocID = BL.SourceSysDocID AND PO.VoucherID = BL.SourceVoucherID\r\n                            WHERE PO.SysDocID = '" + sysDocID + "' AND PO.VoucherID = '" + voucherID + "'";
					FillDataSet(dataSet2, "PO", textCommand);
					if (dataSet2 != null && dataSet2.Tables["PO"].Rows.Count > 0)
					{
						DataRow dataRow = dataSet2.Tables["PO"].Rows[0];
						d2 = decimal.Parse(dataRow["POTotal"].ToString());
						dateTime = DateTime.Parse(dataRow["PODate"].ToString());
					}
					DataRow[] array2 = array;
					foreach (DataRow obj in array2)
					{
						int num = int.Parse(obj["NetDays"].ToString());
						decimal d3 = decimal.Parse(obj["Percentage"].ToString());
						if (dateTime.AddDays(num) < DateTime.Today.EndOfDay())
						{
							d += d2 * d3 / 100m;
						}
					}
				}
				array = dataSet.Tables["Term"].Select("TermType = " + 8);
				if (array.Length != 0 && dataSet2 != null && dataSet2.Tables["PO"].Rows.Count > 0 && dataSet2.Tables["PO"].Rows[0]["BLDate"] != DBNull.Value)
				{
					DataRow dataRow2 = dataSet2.Tables["PO"].Rows[0];
					DateTime dateTime2 = DateTime.MinValue;
					_ = DateTime.MinValue;
					_ = DateTime.MinValue;
					if (!dataRow2["BLDate"].IsDBNullOrEmpty())
					{
						dateTime2 = DateTime.Parse(dataRow2["BLDate"].ToString());
					}
					DataRow[] array2 = array;
					foreach (DataRow obj2 in array2)
					{
						int num2 = int.Parse(obj2["NetDays"].ToString());
						decimal d4 = decimal.Parse(obj2["Percentage"].ToString());
						if (dateTime2.AddDays(num2) < DateTime.Today.EndOfDay())
						{
							d += d2 * d4 / 100m;
						}
					}
				}
				array = dataSet.Tables["Term"].Select("TermType = " + 4);
				if (array.Length != 0 && dataSet2 != null && dataSet2.Tables["PO"].Rows.Count > 0 && dataSet2.Tables["PO"].Rows[0]["ATD"] != DBNull.Value)
				{
					DataRow dataRow3 = dataSet2.Tables["PO"].Rows[0];
					DateTime dateTime3 = DateTime.MinValue;
					if (!dataRow3["ATD"].IsDBNullOrEmpty())
					{
						dateTime3 = DateTime.Parse(dataRow3["ATD"].ToString());
					}
					DataRow[] array2 = array;
					foreach (DataRow obj3 in array2)
					{
						int num3 = int.Parse(obj3["NetDays"].ToString());
						decimal d5 = decimal.Parse(obj3["Percentage"].ToString());
						if (dateTime3.AddDays(num3) < DateTime.Today.EndOfDay())
						{
							d += d2 * d5 / 100m;
						}
					}
				}
				array = dataSet.Tables["Term"].Select("TermType = " + 6);
				if (array.Length != 0 && dataSet2 != null && dataSet2.Tables["PO"].Rows.Count > 0 && dataSet2.Tables["PO"].Rows[0]["ETA"] != DBNull.Value)
				{
					DataRow dataRow4 = dataSet2.Tables["PO"].Rows[0];
					DateTime dateTime4 = DateTime.MinValue;
					if (!dataRow4["ETA"].IsDBNullOrEmpty())
					{
						dateTime4 = DateTime.Parse(dataRow4["ETA"].ToString());
					}
					DataRow[] array2 = array;
					foreach (DataRow obj4 in array2)
					{
						int num4 = int.Parse(obj4["NetDays"].ToString());
						decimal d6 = decimal.Parse(obj4["Percentage"].ToString());
						if (dateTime4.AddDays(-1 * num4) < DateTime.Today.EndOfDay())
						{
							d += d2 * d6 / 100m;
						}
					}
				}
				array = dataSet.Tables["Term"].Select("TermType = " + 7);
				if (array.Length != 0 && dataSet2 != null && dataSet2.Tables["PO"].Rows.Count > 0 && dataSet2.Tables["PO"].Rows[0]["ETA"] != DBNull.Value)
				{
					DataRow dataRow5 = dataSet2.Tables["PO"].Rows[0];
					DateTime dateTime5 = DateTime.MinValue;
					if (!dataRow5["ETA"].IsDBNullOrEmpty())
					{
						dateTime5 = DateTime.Parse(dataRow5["ETA"].ToString());
					}
					DataRow[] array2 = array;
					foreach (DataRow obj5 in array2)
					{
						int num5 = int.Parse(obj5["NetDays"].ToString());
						decimal d7 = decimal.Parse(obj5["Percentage"].ToString());
						if (dateTime5.AddDays(num5) < DateTime.Today.EndOfDay())
						{
							d += d2 * d7 / 100m;
						}
					}
				}
				array = dataSet.Tables["Term"].Select("TermType = " + 9);
				if (array.Length != 0)
				{
					textCommand = "SELECT TOP 1 GRNDate FROM (\r\n                                 SELECT PR.TransactionDate AS GRNDate FROM Purchase_Receipt  PR INNER JOIN Purchase_Receipt_Detail PRD ON PR.SysDocID = PRD.SysDocID AND PR.VoucherID = PRD.VoucherID\r\n                                 INNER JOIN PO_Shipment_Detail POSD ON POSD.SysDocID = PRD.PKSysDocID AND POSD.VoucherID = PRD.PKVoucherID\r\n                                 WHERE POSD.SourceSysDocID = '" + sysDocID + "' AND POSD.SourceVoucherID = '" + voucherID + "'\r\n                                 UNION \r\n                                 SELECT PR.TransactionDate AS GRNDate FROM Purchase_Receipt  PR INNER JOIN Purchase_Receipt_Detail PRD ON PR.SysDocID = PRD.SysDocID AND PR.VoucherID = PRD.VoucherID\r\n                                 WHERE PR.POSysDocID = '" + sysDocID + "' AND PR.POVoucherID = '" + voucherID + "') GRN";
					object obj6 = ExecuteScalar(textCommand);
					if (!obj6.IsNullOrEmpty())
					{
						DateTime dateTime6 = DateTime.Parse(obj6.ToString());
						DataRow[] array2 = array;
						foreach (DataRow obj7 in array2)
						{
							int num6 = int.Parse(obj7["NetDays"].ToString());
							decimal d8 = decimal.Parse(obj7["Percentage"].ToString());
							if (dateTime6.AddDays(num6) < DateTime.Today.EndOfDay())
							{
								d += d2 * d8 / 100m;
							}
						}
					}
				}
				textCommand = "SELECT ISNULL(SUM(ISNULL(AmountFC,Amount)),0) AS Amount FROM Purchase_Prepayment_Invoice PPI WHERE SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherID + "'";
				decimal d9 = default(decimal);
				object obj8 = ExecuteScalar(textCommand);
				if (!obj8.IsNullOrEmpty())
				{
					d9 = decimal.Parse(obj8.ToString());
				}
				return d - d9;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPOsItemsToShip(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT PO.VendorID,Ven.ParentVendorID, POD.SysDocID [Doc ID], POD.VoucherID AS [PO Number],ProductID,Description,POD.UnitID,RowIndex,UnitQuantity,\r\n                                ISNULL(UnitQuantity,Quantity) AS Quantity,ISNULL(QuantityShipped,0) AS QuantityShipped,\r\n                                ISNULL(SubunitPrice,UnitPrice) AS UnitPrice,ISNULL(QuantityReceived,0) AS QuantityReceived, PO.ShippingMethodID, PO.ETA, PO.PortDestination \r\n                                FROM Purchase_Order_Detail POD INNER JOIN Purchase_Order PO ON PO.SysDocID=POD.SysDocID AND PO.VoucherID=POD.VoucherID\r\n                                INNER JOIN Vendor VEN ON VEN.VendorID=PO.VendorID\r\n                              \r\n                             WHERE POD.SysDocID='" + sysDocID + "' AND POD.VoucherID='" + voucherID + "'";
				FillDataSet(dataSet, "Purchase_Order_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenOrdersSummary(string vendorID, bool includeImport, bool includeLocal)
		{
			return GetOpenOrdersSummary(vendorID, includeImport, includeLocal, showAll: false);
		}

		public DataSet GetOpenOrdersSummary(string vendorID, bool includeImport, bool includeLocal, bool showAll)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor],\r\n                                So.BuyerID+'-'+B.FullName as [Buyer], SO.Reference,J.JobName, (Isnull(Total,0)+Isnull(TaxAmount,0)) AS Amount  FROM Purchase_Order SO\r\n                                 INNER JOIN Vendor C ON SO.VendorID=C.VendorID \r\n\t\t\t\t\t\t\t     LEFT JOIN Buyer B ON SO.BuyerID=B.BuyerID\r\n\t\t\t\t\t\t\t     LEFT OUTER JOIN Job J on J.JobID=SO.JobID WHERE ISNULL(IsVoid,'False')='False' ";
				str = (showAll ? (str + " AND SO.Status NOT IN ('4', '5')") : (str + " AND SO.Status=1"));
				if (vendorID != "")
				{
					str = str + " AND SO.VendorID='" + vendorID + "'";
				}
				if (!includeImport || !includeLocal)
				{
					str = str + " AND ISNULL(IsImport,'False') = '" + includeImport.ToString() + "'";
				}
				FillDataSet(dataSet, "Purchase_Order", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenPOComboData(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT PO.VoucherID  ,PO.SysDocID  , TransactionDate AS [Date],PO.VendorID + '-' + V.VendorName AS [Vendor],Reference,Total AS Amount,\r\n                                ISNULL((SELECT SUM(Amount) FROM Payment_Request PR WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS Paid\r\n                                 FROM Purchase_Order PO\r\n                                     INNER JOIN Vendor V ON PO.VendorID= V.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status=1 ";
				if (vendorID != "")
				{
					text = text + " AND PO.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "Purchase_Order", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPOListForPayment(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT PO.VoucherID  ,PO.SysDocID  , TransactionDate AS [Date],PO.VendorID + '-' + V.VendorName AS [Vendor],ETA as [ETA],Reference,Total AS Amount,\r\n                                ISNULL((SELECT SUM(Amount) FROM Payment_Request PR WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS Paid\r\n                                 FROM Purchase_Order PO\r\n                                     INNER JOIN Vendor V ON PO.VendorID= V.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status IN (1,2,3) ";
				if (vendorID != "")
				{
					text = text + " AND PO.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "Purchase_Order", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseOrderAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor],Reference,Total AS Amount  FROM Purchase_Order SO\r\n                             INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
				FillDataSet(dataSet, "Purchase_Order", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowShippedQuantity(string sysDocID, string voucherID, int rowIndex, string productID, float quantity, SqlTransaction sqlTransaction)
		{
			try
			{
				return UpdateRowShippedQuantity(sysDocID, voucherID, rowIndex, productID, quantity, validateQuantity: true, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowShippedQuantity(string sysDocID, string voucherID, int rowIndex, string productID, float quantity, bool validateQuantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			float result3 = 0f;
			try
			{
				string textCommand = "SELECT ISNULL(Quantity,0) AS Quantity,UnitQuantity,ISNULL(QuantityShipped,0) AS QuantityShipped,ISNULL(QuantityReceived,0) AS QuantityReceived FROM Purchase_Order_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Product", textCommand);
				if (validateQuantity && dataSet != null && dataSet.Tables[0].Rows.Count > 0)
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
					float.TryParse(dataRow["QuantityReceived"].ToString(), out result3);
					if (result < result2 + result3 + quantity)
					{
						throw new CompanyException("Shipped quantity cannot be greater than ordered quantity.", 1013);
					}
				}
				result2 += quantity;
				textCommand = "UPDATE Purchase_Order_Detail SET QuantityShipped = " + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND ProductID='" + productID + "' AND RowIndex=" + rowIndex.ToString();
				return (byte)(1 & ((ExecuteNonQuery(textCommand, sqlTransaction) > 0) ? 1 : 0)) != 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowReceivedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			bool flag = true;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReceived FROM Purchase_Order_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
					float.TryParse(dataRow["QuantityReceived"].ToString(), out result2);
				}
				result2 += quantity;
				textCommand = "UPDATE Purchase_Order_Detail SET QuantityReceived=" + decimal.Parse(result2.ToString()) + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return flag & (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
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
				string exp = "SELECT COUNT(RowIndex)FROM Purchase_Order_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Purchase_Order_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityReceived,0) + ISNULL(QuantityShipped,0))  FROM Purchase_Order_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null)
				{
					return true;
				}
				bool flag = false;
				if (int.Parse(obj.ToString()) > 0)
				{
					flag = true;
				}
				int num = 1;
				if (flag)
				{
					num = 2;
				}
				exp = "UPDATE Purchase_Order SET Status = " + num + ", IsShipped = '" + flag.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) >= 0;
			}
			catch
			{
				throw;
			}
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status)
		{
			try
			{
				string exp = "UPDATE Purchase_Order SET Status= " + (int)status + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool CloseReceivedOrder(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM Purchase_Order_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Purchase_Order_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityReceived,0))  FROM Purchase_Order_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Purchase_Order SET Status= 2 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
				string exp = "SELECT COUNT(RowIndex)FROM Purchase_Order_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Purchase_Order_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityReceived,0) ) FROM Purchase_Order_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) > 0)
				{
					return true;
				}
				exp = "UPDATE Purchase_Order SET Status= 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool DeletePurchaseOrderDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string text = "";
				PurchaseOrderData purchaseOrderData = new PurchaseOrderData();
				string textCommand = "SELECT SOD.*,ISVOID FROM Purchase_Order_Detail SOD INNER JOIN Purchase_Order SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(purchaseOrderData, "Purchase_Order_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(purchaseOrderData.PurchaseOrderDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (!result)
				{
					foreach (DataRow row in purchaseOrderData.PurchaseOrderDetailTable.Rows)
					{
						text = row["ProductID"].ToString();
						float num = float.Parse(row["Quantity"].ToString());
						float num2 = new Products(base.DBConfig).GetOrderedQuantity(text, sqlTransaction) - num;
						if (num2 < 0f)
						{
							num2 = 0f;
						}
						flag &= new Products(base.DBConfig).UpdateOrderedQuantity(text, num2, sqlTransaction);
					}
					string text2 = "";
					string text3 = "";
					foreach (DataRow row2 in purchaseOrderData.PurchaseOrderDetailTable.Rows)
					{
						text = row2["ProductID"].ToString();
						text2 = row2["SourceVoucherID"].ToString();
						text3 = row2["SourceSysDocID"].ToString();
						int result2 = 0;
						if (!(text2 == "") && !(text3 == ""))
						{
							int.TryParse(row2["SourceRowIndex"].ToString(), out result2);
							float result3 = 0f;
							if (row2["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row2["UnitQuantity"].ToString(), out result3);
							}
							else
							{
								float.TryParse(row2["Quantity"].ToString(), out result3);
							}
							int num3 = Convert.ToInt32(new Databases(base.DBConfig).GetFieldValue("System_Document", "SysDocType", "SysDocID", row2["SourceSysDocID"].ToString(), sqlTransaction));
							if (num3 == 30 || num3 == 63)
							{
								flag &= new PurchaseQuote(base.DBConfig).UpdateRowOrderedQuantity(text3, text2, result2, -1f * result3, sqlTransaction);
							}
						}
					}
					string text4 = "";
					string text5 = "";
					foreach (DataRow row3 in purchaseOrderData.PurchaseOrderDetailTable.Rows)
					{
						text = row3["ProductID"].ToString();
						text4 = row3["SourceVoucherID"].ToString();
						text5 = row3["SourceSysDocID"].ToString();
						int result4 = 0;
						if (!(text4 == "") && !(text5 == ""))
						{
							int.TryParse(row3["SourceRowIndex"].ToString(), out result4);
							float result5 = 0f;
							if (row3["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row3["UnitQuantity"].ToString(), out result5);
							}
							else
							{
								float.TryParse(row3["Quantity"].ToString(), out result5);
							}
							float num4 = new Products(base.DBConfig).GetReservedQuantity(text, sqlTransaction) + result5;
							if (num4 < 0f)
							{
								num4 = 0f;
							}
							int num5 = Convert.ToInt32(new Databases(base.DBConfig).GetFieldValue("System_Document", "SysDocType", "SysDocID", row3["SourceSysDocID"].ToString(), sqlTransaction));
							if (num5 == 23 || num5 == 52)
							{
								flag &= new Products(base.DBConfig).UpdateReservedQuantity(text, num4, sqlTransaction);
								flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text5, text4, result4, -1f * result5, sqlTransaction);
								flag &= new SalesOrder(base.DBConfig).ReOpenOrder(text5, text4, sqlTransaction);
							}
						}
					}
				}
				textCommand = "DELETE FROM Purchase_Order_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				return flag & new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidPurchaseOrder(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				switch (CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
				case 1:
					throw new CompanyException("Unable to update. Some of the items in this order has been already received.", 1037);
				case 2:
					throw new CompanyException("This order has one or more prepayments.", 1062);
				default:
				{
					PurchaseOrderData purchaseOrderData = new PurchaseOrderData();
					string textCommand = "SELECT * FROM Purchase_Order_Detail   \r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					FillDataSet(purchaseOrderData, "Purchase_Order_Detail", textCommand, sqlTransaction);
					string text = "";
					string text2 = "";
					foreach (DataRow row in purchaseOrderData.PurchaseOrderDetailTable.Rows)
					{
						string productID = row["ProductID"].ToString();
						float result = 0f;
						if (row["UnitQuantity"] != DBNull.Value)
						{
							float.TryParse(row["UnitQuantity"].ToString(), out result);
						}
						else
						{
							float.TryParse(row["Quantity"].ToString(), out result);
						}
						float num = new Products(base.DBConfig).GetOrderedQuantity(productID, sqlTransaction) - result;
						if (num < 0f)
						{
							num = 0f;
						}
						flag &= new Products(base.DBConfig).UpdateOrderedQuantity(productID, num, sqlTransaction);
						int result2 = 0;
						text = row["SourceVoucherID"].ToString();
						text2 = row["SourceSysDocID"].ToString();
						int.TryParse(row["SourceRowIndex"].ToString(), out result2);
						if (!(text == "") && !(text2 == ""))
						{
							SysDocTypes sysDocTypes = SysDocTypes.PurchaseQuote;
							object fieldValue = new Databases(base.DBConfig).GetFieldValue("System_Document", "SysDocType", "SysDocID", text2, sqlTransaction);
							if (fieldValue != null)
							{
								sysDocTypes = (SysDocTypes)Enum.Parse(typeof(SysDocTypes), fieldValue.ToString());
							}
							if (sysDocTypes == SysDocTypes.PurchaseQuote)
							{
								flag &= new PurchaseQuote(base.DBConfig).UpdateRowOrderedQuantity(text2, text, result2, -1f * result, sqlTransaction);
							}
						}
					}
					textCommand = "UPDATE Purchase_Order SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
					AddActivityLog("Purchase Order", voucherID, sysDocID, activityType, sqlTransaction);
					return flag;
				}
				}
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

		public bool DeletePurchaseOrder(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				switch (CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
				case 1:
					throw new CompanyException("Unable to update. Some of the items in this order has been already received.", 1037);
				case 2:
					throw new CompanyException("This order has one or more prepayments.", 1062);
				default:
				{
					flag &= DeletePurchaseOrderDetailsRows(sysDocID, voucherID, sqlTransaction);
					text = "DELETE FROM Purchase_Order WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
					flag &= new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
					if (!flag)
					{
						return flag;
					}
					ActivityTypes activityType = ActivityTypes.Delete;
					AddActivityLog("Purchase Order", voucherID, sysDocID, activityType, sqlTransaction);
					return flag;
				}
				}
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
			string textCommand = "SELECT SysDocID,VoucherID,SO.VendorID,VendorName,TransactionDate,SO.BuyerID,Total,SO.CurrencyID,SO.CurrencyRate,Cur.CurrencyID AS BaseCurrencyID  \r\n                            FROM Purchase_Order SO INNER JOIN Vendor ON SO.VendorID=Vendor.VendorID \r\n                             LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True' \r\n                            WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
			FillDataSet(dataSet, "Orders", textCommand);
			return dataSet;
		}

		public DataSet GetLastVendorDeliveryAddress(string VendorID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT Top 1 TransactionDate, DeliveryAddress from Purchase_Order where VendorID='" + VendorID + "'  Order By TransactionDate Desc";
			FillDataSet(dataSet, "Address", textCommand);
			return dataSet;
		}

		public DataSet GetPurchaseOrderToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT DISTINCT SI.*,V.VendorName,V.TaxIDNumber as VTaxIDNo,B.FullName,B.Phone1 AS BPhone,B.Mobile AS BMobile,\r\n                               (SELECT TOP 1 J.JobName FROM Job J LEFT JOIN Purchase_Order_Detail PRD ON J.JobID=PRD.JobID WHERE SysDocID = \r\n                              '" + sysDocID + "' AND VoucherID IN (" + text + ")) AS Project,\r\n                              (SELECT TOP 1 J.SiteLocationAddress FROM Job J LEFT JOIN Purchase_Order_Detail PRD ON J.JobID=PRD.JobID WHERE SysDocID = \r\n                              '" + sysDocID + "' AND VoucherID IN (" + text + ")) AS [Site Address],\r\n                              VA.AddressPrintFormat AS VendorAddress,VA.Phone1,VA.Fax,VA.Mobile,VA.ContactName,PT.TermName,SM.ShippingMethodName,\r\n                              ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                              TermName,ISNULL(Total,0) - ISNULL(Discount,0) AS GrandTotal,\r\n                              ISNULL(TaxAmount ,0) AS Tax,Total AS Total,\r\n                    (SELECT TOP 1 J.JobID FROM Job J LEFT JOIN Purchase_Order_Detail PRD ON J.JobID=PRD.JobID WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")) AS JobID, VA.Mobile, VA.Email\r\n                       ,(SELECT SUBSTRING((SELECT DISTINCT ',' + TD.SourceVoucherID + ''  FROM  Purchase_Order_Detail TD \r\n                        WHERE TD.SysDocID = SI.SysDocID AND TD.VoucherID = SI.VoucherID FOR XML PATH('')),2,20000)) AS [PQRef],'' AS [ApproveRequired],J.Note [Job Note]         \r\n                        FROM  Purchase_Order SI INNER JOIN Vendor V ON SI.VendorID=V.VendorID\r\n                              LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                              LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'\r\n                              LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                              LEFT OUTER JOIN Buyer B ON B.BuyerID=SI.BuyerID\r\n                              LEFT JOIN Job J  ON SI.JobId=J.Jobid \r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Purchase_Order", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Purchase_Order"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = " SELECT-- PO.JobID,POD.CostCategoryID,POD.ProductID,SUM(ISNULL(POD.Quantity, 0)) AS OrderedQauntity, ISNULL(AX.MEQuantity, 0) AS[MEQuantity],\r\n                    CASE WHEN  (ISNULL(AX.MEQuantity, 0) < (SUM(ISNULL(POD.Quantity, 0))) \r\n                    OR ISNULL(AX.MEAmount, 0) < (SUM(ISNULL(POD.Quantity * POD.UnitPrice, 0))) )\r\n                    THEN 'NA'\r\n                    ELSE 'A'\r\n                    END as 'ApproveRequired'  FROM\r\n                    Purchase_Order PO INNER JOIN Purchase_Order_Detail POD ON PO.SysDocID = POD.SysDocID AND PO.VoucherID = POD.VoucherID\r\n                    LEFT JOIN (\r\n                    SELECT JME.JobID, JMED.CostCategoryID, JMED.ProductID, SUM(ISNULL(JMED.Quantity, 0)) as MEQuantity, SUM(ISNULL(JMED.Amount, 0)) as MEAmount  FROM\r\n                    Job_Material_Estimate JME INNER JOIN\r\n                    Job_Material_Estimate_Detail JMED ON JME.SysDocID = JMED.SysDocID AND JME.VoucherID = JMED.VoucherID\r\n                    GROUP BY JME.JobID, JMED.CostCategoryID, JMED.ProductID) AX ON AX.JobID = PO.JobID AND AX.CostCategoryID = POD.CostCategoryID\r\n                    AND AX.ProductID = POD.ProductID\r\n                    WHERE POD.SysDocID = '" + sysDocID + "' AND POD.VoucherID IN  (" + text + ")\r\n                    GROUP BY PO.JobID, POD.CostCategoryID,\r\n                    POD.ProductID, AX.MEQuantity, AX.MEAmount";
				FillDataSet(dataSet, "POApproval", cmdText);
				DataRow dataRow = dataSet.Tables["POApproval"].Rows[0];
				DataRow dataRow2 = dataSet.Tables["Purchase_Order"].Rows[0];
				if (dataSet.Tables["POApproval"].Select("ApproveRequired='NA'").Length != 0)
				{
					dataRow2["ApproveRequired"] = "NA";
				}
				else
				{
					dataRow2["ApproveRequired"] = dataRow["ApproveRequired"].ToString();
				}
				cmdText = "SELECT  SysDocID,VoucherID,POD.ProductID,PC.CategoryID,PC.CategoryName,POD.Description,ISNULL(UnitQuantity,POD.Quantity) AS Quantity,POD.TaxAmount, POD.TaxGroupID,TG.TaxGroupName,\r\n                        P.UnitID AS [P Unit],P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,POD.UnitPrice AS UnitPrice,C.CountryName,\r\n                        (SELECT CASE WHEN FactorType='D' THEN ISNULL(UnitQuantity,POD.Quantity)*Factor ELSE ISNULL(UnitQuantity,POD.Quantity)/Factor END FROM Product_Unit PU WHERE PU.UnitID=POD.UnitID AND PU.ProductID=POD.ProductID ) AS Weight,\r\n                        ISNULL(UnitQuantity,POD.Quantity)*ISNULL(POD.UnitPrice,0) AS Total,POD.UnitID, P.BrandID, POD.RowIndex + 1 RowIndex,PB.BrandName,P.Description2,POD.Remarks,POD.Length,POD.Width,POD.Height,POD.Number,P.Photo,ISNULL(POD.MinGuarantee,0) AS [Min Guarantee],P.Size,GL.Finish ,\r\n                        POD.RefSlNo,POD.RefText1,POD.RefText2,POD.RefNum1,POD.RefNum2,POD.RefDate1,POD.RefDate2 , PU.UnitName,P.UPC\r\n                        FROM   Purchase_Order_Detail POD\r\n                        INNER JOIN Product P ON P.ProductID = POD.ProductID\r\n                        LEFT OUTER JOIN Product_Category PC ON PC.CategoryID=P.CategoryID\r\n                        LEFT OUTER JOIN Country C ON P.Origin=C.CountryID\r\n                        LEFT JOIN Tax_Group TG ON POD.TaxGroupID=TG.TaxGroupID\r\n                        LEFT JOIN Product_Brand PB ON P.BrandID=PB.BrandID \r\n                        LEFT OUTER JOIN Unit PU ON PU.UnitID=POD.UnitID\r\n                        LEFT JOIN (Select GenericListID,GenericListName as [Finish] from Generic_List where GenericListType =" + 33 + ") as  GL On GL.GenericListID=P.FinishingID \r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Purchase_Order_Detail", cmdText);
				cmdText = "SELECT DISTINCT JMR.VoucherID,JMR.* FROM Job_Material_Requisition JMR INNER JOIN Purchase_Order_Detail POD ON JMR.VoucherID=POD.SourceVoucherID AND JMR.SysDocID=POD.SourceSysDocID WHERE POD.VoucherID=" + text + " AND POD.SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Job_Material_Requisition", cmdText);
				dataSet.Relations.Add("PurchaseOrder", new DataColumn[2]
				{
					dataSet.Tables["Purchase_Order"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Order"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Purchase_Order_Detail"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Order_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Purchase_Order"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Purchase_Order"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Discount"].ToString(), out result2);
					decimal.TryParse(row["Tax"].ToString(), out result3);
					row["TotalInWords"] = NumToWord.GetNumInWords(decimalPoints: new CompanyInformations(base.DBConfig).CurrencyDecimalPoints, amount: result - result2 + result3);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Order Date],INV.Status,\r\n                            CASE ISNULL(IsImport,'False') WHEN 'True' THEN 'Import' ELSE 'Local' END AS [Type],INV.BuyerID [Buyer],(SELECT DISTINCT (cast(POD.SourceVoucherID as varchar(200))+', ') as [text()] FROM Purchase_Order_Detail POD\r\n            WHERE POD.SysDocID=INV.SysDocID AND POD.VoucherID=INV.VoucherID for XML PATH(''))  as Ref1,INV.Reference, INV.CurrencyID Currency,Total - ISNULL(Discount,0) AS [Amount],INV.Reference2 as Ref2,J.JobID,J.JobName,\r\n                            ISNULL((CASE INV.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' END) ,(CASE Vendor.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' END))AS TAXOPTION,INV.TaxAmount\r\n                            FROM         Purchase_Order INV\r\n                            LEFT JOIN Job J ON INV.JobID=J.JobID\r\n                            INNER JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE 1=1 AND ISNULL(IsImport,'False') = '" + isImport.ToString() + "' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			if (sysDocID != "")
			{
				text3 = text3 + " AND INV.SysDocID = '" + sysDocID + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Order", sqlCommand);
			return dataSet;
		}

		public DataSet XGetPendingApprovalList(string approvalID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Order Date],Status,\r\n                            CASE ISNULL(IsImport,'False') WHEN 'True' THEN 'Import' ELSE 'Local' END AS [Type],INV.BuyerID [Buyer],Reference,INV.CurrencyID Currency,Total [Amount]\r\n                            FROM         Purchase_Order INV\r\n                            INNER JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE 1=1 ");
			FillDataSet(dataSet, "Purchase_Order", sqlCommand);
			return dataSet;
		}

		public ItemTypes GetItemType(string itemID)
		{
			object obj = ExecuteSelectScalar("Product", "ProductID", itemID, "ItemType");
			ItemTypes result = ItemTypes.Inventory;
			if (obj != null)
			{
				try
				{
					result = (ItemTypes)byte.Parse(obj.ToString());
					return result;
				}
				catch
				{
					return result;
				}
			}
			return result;
		}

		public bool ValidateOrder(string ItemCode, string jobID, decimal OrderQty)
		{
			DataSet dataSet = new DataSet();
			DataSet dataSet2 = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("select ProductID,(SUM(Variance/Quantity)*100 )+ SUM (Quantity) AS Expected From Job_Material_Estimate_Detail  JMD inner join Job_Material_Estimate JM ON JM.SysDocID=JMD.SysDocID AND JM.VoucherID=JMD.VoucherID\r\n                        where JMD.ProductID='" + ItemCode + "' AND JM.JobID='" + jobID + "' group by ProductID");
			FillDataSet(dataSet, "EstimateDtls", sqlCommand);
			sqlCommand = new SqlCommand("select ProductID,SUM(Quantity) AS OrderedQauntity From Purchase_Order_Detail JMD inner join Purchase_Order JM ON JM.SysDocID=JMD.SysDocID AND JM.VoucherID=JMD.VoucherID\r\n                where JMD.ProductID='" + ItemCode + "' AND JMD.JobID='" + jobID + "'  group by ProductID");
			FillDataSet(dataSet2, "OrderedDtls", sqlCommand);
			if (dataSet.Tables["EstimateDtls"].Rows.Count == 0)
			{
				return true;
			}
			if (dataSet.Tables["EstimateDtls"].Rows.Count > 0)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(dataSet.Tables["EstimateDtls"].Rows[0]["Expected"].ToString(), out result);
				if (dataSet2.Tables["OrderedDtls"].Rows.Count > 0)
				{
					decimal.TryParse(dataSet2.Tables["OrderedDtls"].Rows[0]["OrderedQauntity"].ToString(), out result2);
				}
				if (result2 <= Math.Round(result, 2))
				{
					return true;
				}
				return false;
			}
			return true;
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			string exp = "SELECT COUNT(*) FROM Purchase_Prepayment_Invoice WHERE (ISNULL(IsVoid,'False')<> 'True' ) AND (SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherNumber + "')";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public DataSet GetOpenOrdersSummaryWithNonInv(string vendorID, bool includeImport, bool includeLocal, bool showAll)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor],So.BuyerID+'-'+B.FullName as [Buyer],\r\n                                 Reference,(Isnull(Total,0)+Isnull(TaxAmount,0)) AS Amount  FROM Purchase_Order SO\r\n                                INNER JOIN Vendor C ON SO.VendorID=C.VendorID \r\n                                LEFT JOIN Buyer B ON SO.BuyerID=B.BuyerID\r\n                                WHERE ISNULL(IsVoid,'False')='False'";
				str = (showAll ? (str + " AND Status NOT IN ('4', '5')") : (str + " AND Status=1"));
				if (vendorID != "")
				{
					str = str + " AND SO.VendorID='" + vendorID + "'";
				}
				if (!includeImport || !includeLocal)
				{
					str = str + " AND ISNULL(IsImport,'False') = '" + includeImport.ToString() + "'";
				}
				str += " UNION ALL\r\n\r\n                                SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor],So.BuyerID+'-'+B.FullName as [Buyer],Reference,Total AS Amount  \r\n                                FROM Purchase_Order_NonInv SO\r\n                                INNER JOIN Vendor C ON SO.VendorID=C.VendorID \r\n                                LEFT JOIN Buyer B ON SO.BuyerID=B.BuyerID\r\n                                 WHERE ISNULL(IsVoid,'False')='False' ";
				str = (showAll ? (str + " AND Status NOT IN ('4', '5')") : (str + " AND Status=1"));
				if (vendorID != "")
				{
					str = str + " AND SO.VendorID='" + vendorID + "'";
				}
				if (!includeImport || !includeLocal)
				{
					str = str + " AND ISNULL(IsImport,'False') = '" + includeImport.ToString() + "'";
				}
				FillDataSet(dataSet, "Purchase_Order", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public PurchaseOrderData GetPurchaseOrderWithNIByID(string sysDocID, string voucherID)
		{
			try
			{
				PurchaseOrderData purchaseOrderData = new PurchaseOrderData();
				string textCommand = "SELECT PO.*,PT.TermName, ISNULL((SELECT SUM(Amount) FROM Payment_Request WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS PaidAmount ,\r\n                                ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) FROM Purchase_Prepayment_Invoice PPI WHERE PPI.SourceSysDocID = PO.SysDocID AND PPI.SourceVoucherID = PO.VoucherID),0) AS PrePaidAmount,\r\n                                CASE WHEN (SELECT COUNT(*) FROM PO_Shipment_Detail PR \r\n                                                                WHERE PO.SysDocID = PR.SourceSysDocID AND PO.VoucherID = PR.SourceVoucherID)>0 THEN 'True' ELSE 'False' END  AS IsPOShipped ,T.SysDocID AS PKSysDocID,T.VoucherID AS PKVoucherID\r\n                                FROM Purchase_Order PO LEFT OUTER JOIN (SELECT TOP 1 SysDocID,VoucherID,SourceSysDocID,SourceVoucherID FROM PO_Shipment_Detail POS\r\n                                WHERE  POS.SourceSysDocID = '" + sysDocID + "' AND   POS.SourceVoucherID = '" + voucherID + "') T  ON T.SourceSysDocID = PO.SysDocID AND T.SourceVoucherID = PO.VoucherID \r\n                                LEFT OUTER JOIN Payment_Term PT ON PT.PaymentTermID = PO.TermID\r\n\t\t\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Purchase_Order", textCommand);
				if (purchaseOrderData == null || purchaseOrderData.Tables.Count == 0 || purchaseOrderData.Tables["Purchase_Order"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*, Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID, Product.IsTrackLot, Product.IsTrackSerial,ISNull(Product.TaxGroupID,PC.TaxGroupID) AS TaxGroupID\r\n                        FROM Purchase_Order_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        LEFT OUTER JOIN Product_Class PC ON PC.ClassID = Product.ClassID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(purchaseOrderData, "Purchase_Order_Detail", textCommand);
				textCommand = "SELECT * FROM Purchase_Receipt PR  INNER JOIN Purchase_Order PO ON PO.SysDocID=PR.SourceSysDocID AND PO.VoucherID=PR.SourceVoucherID\r\n\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Purchase_Receipt", textCommand);
				textCommand = "SELECT DISTINCT JMR.VoucherID,JMR.* FROM Job_Material_Requisition JMR INNER JOIN Purchase_Order_Detail POD ON JMR.VoucherID=POD.SourceVoucherID AND JMR.SysDocID=POD.SourceSysDocID WHERE POD.VoucherID='" + voucherID + "' AND POD.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Job_Material_Requisition", textCommand);
				textCommand = "SELECT DISTINCT SOD.VoucherID,SOD.SysDocID FROM Sales_Order_Detail SOD INNER JOIN Purchase_Order_Detail POD ON SOD.VoucherID=POD.SourceVoucherID AND SOD.SysDocID=POD.SourceSysDocID WHERE POD.VoucherID='" + voucherID + "' AND POD.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Sales_Order_Detail", textCommand);
				textCommand = "SELECT DISTINCT PID.VoucherID,PID.SysDocID FROM Purchase_Invoice_Detail PID INNER JOIN Purchase_Order_Detail POD ON POD.VoucherID=PID.OrderVoucherID AND POD.SysDocID=PID.OrderSysDocID WHERE POD.VoucherID='" + voucherID + "' AND POD.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Purchase_Invoice_Detail", textCommand);
				textCommand = "SELECT DISTINCT PQD.VoucherID,PQD.SysDocID FROM Purchase_Quote_Detail PQD INNER JOIN Purchase_Order_Detail POD ON PQD.VoucherID=POD.SourceVoucherID AND PQD.SysDocID=POD.SourceSysDocID WHERE POD.VoucherID='" + voucherID + "' AND POD.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Purchase_Quote_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderData, "Tax_Detail", textCommand);
				return purchaseOrderData;
			}
			catch
			{
				throw;
			}
		}

		public bool UpdatePODetails(DataSet purchaseOrderData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				GetInsertUpdatePurchaseOrderDetailsCommand(isUpdate: true).Transaction = sqlTransaction;
				string text = "IsNonEdit = 'True'";
				string sort = "RowIndex ASC";
				purchaseOrderData.Tables["Purchase_Order_Detail"].Select(text, sort).CopyToDataTable();
				DataTable dataTable = new DataView(purchaseOrderData.Tables["Purchase_Order_Detail"])
				{
					RowFilter = text
				}.ToTable();
				if (dataTable.Rows.Count > 0)
				{
					new DataSet();
					new DataTable("Purchase_Order_Detail".ToString());
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
							decimal num = default(decimal);
							decimal result = default(decimal);
							decimal result2 = default(decimal);
							decimal result3 = default(decimal);
							decimal result4 = default(decimal);
							decimal result5 = default(decimal);
							decimal d = default(decimal);
							decimal result6 = default(decimal);
							decimal result7 = default(decimal);
							int result8 = 0;
							text2 = row["ProductID"].ToString();
							num = decimal.Parse(row["Quantity"].ToString());
							text3 = row["Description"].ToString();
							text4 = row["Remarks"].ToString();
							text5 = row["UnitID"].ToString();
							decimal.TryParse(row["UnitFactor"].ToString(), out result7);
							text6 = row["FactorType"].ToString();
							text7 = row["SourceSysDocID"].ToString();
							text8 = row["SourceVoucherID"].ToString();
							decimal.TryParse(row["SourceRowIndex"].ToString(), out result6);
							int.TryParse(row["TaxOption"].ToString(), out result8);
							text13 = row["TaxGroupID"].ToString();
							text14 = row["JobID"].ToString();
							text9 = row["CostCategoryID"].ToString();
							decimal.TryParse(row["UnitPrice"].ToString(), out result);
							decimal.TryParse(row["UnitQuantity"].ToString(), out result2);
							decimal.TryParse(row["SubunitPrice"].ToString(), out result3);
							decimal.TryParse(row["TaxPercentage"].ToString(), out result4);
							decimal.TryParse(row["TaxAmount"].ToString(), out result5);
							decimal? num2 = null;
							object value = null;
							if (result2 == 0m)
							{
								result2 = Convert.ToDecimal(value);
							}
							if (d == 0m)
							{
								d = num2.GetValueOrDefault(0m);
							}
							if (result3 == 0m)
							{
								d = num2.GetValueOrDefault(0m);
							}
							text10 = row["SysDocID"].ToString();
							text11 = row["VoucherID"].ToString();
							text12 = row["RowIndex"].ToString();
							string commandText = ("UPDATE Purchase_Order_Detail SET ProductID = '" + text2 + "',Quantity =" + num + ",UnitPrice = " + result + ",Description = '" + text3 + "',Remarks = '" + text4 + "',UnitID = '" + text5 + "',FactorType = '" + text6 + "',SourceSysDocID ='" + text7 + "',SourceVoucherID ='" + text8 + "',SourceRowIndex = " + result6 + ",TaxOption = " + result8 + ",TaxGroupID ='" + text13 + "',TaxPercentage = " + result4 + ",TaxAmount = " + result5 + ",JobID = '" + text14 + "',CostCategoryID = '" + text9 + "' WHERE SysDocID = '" + text10 + "' AND VoucherID = '" + text11 + "' AND RowIndex = " + text12) ?? "";
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

		public bool InsertPODetails(DataSet purchaseOrderData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				SqlCommand insertUpdatePurchaseOrderDetailsCommand = GetInsertUpdatePurchaseOrderDetailsCommand(isUpdate: false);
				insertUpdatePurchaseOrderDetailsCommand.Transaction = sqlTransaction;
				string text = "IsNonEdit = 'False'";
				string sort = "RowIndex ASC";
				DataTable dataTable = new DataTable();
				DataTable dataTable2 = new DataView(purchaseOrderData.Tables["Purchase_Order_Detail"])
				{
					RowFilter = text
				}.ToTable();
				if (dataTable2.Rows.Count <= 0)
				{
					return true;
				}
				dataTable = purchaseOrderData.Tables["Purchase_Order_Detail"].Select(text, sort).CopyToDataTable();
				if (dataTable.Rows.Count > 0)
				{
					DataSet dataSet = new DataSet();
					dataTable.TableName = "Purchase_Order_Detail";
					dataTable.Copy();
					dataSet.Tables.Add(dataTable2);
					return flag & Insert(dataSet, "Purchase_Order_Detail", insertUpdatePurchaseOrderDetailsCommand);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}
	}
}
