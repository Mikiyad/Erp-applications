using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PurchaseReceipt : StoreObject
	{
		private const string PURCHASERECEIPT_TABLE = "Purchase_Receipt";

		private const string PURCHASERECEIPTDETAIL_TABLE = "Purchase_Receipt_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string VENDORID_PARM = "@VendorID";

		private const string PURCHASEFLOW_PARM = "@PurchaseFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string BUYERID_PARM = "@BuyerID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string VENDORREFERENCENUMBER_PARM = "@VendorReferenceNo";

		private const string NOTE_PARM = "@Note";

		private const string PONUMBER_PARM = "@PONumber";

		private const string SOURCEDOCTYPE_PARM = "@ItemSourceTypes";

		private const string ISVOID_PARM = "@IsVoid";

		private const string DISCOUNT_PARM = "@Discount";

		private const string DISCOUNTFC_PARM = "@DiscountFC";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TOTAL_PARM = "@Total";

		private const string TOTALFC_PARM = "@TotalFC";

		private const string ISIMPORT_PARM = "@IsImport";

		private const string POSYSDOCID = "@POSysDocID";

		private const string POVOUCHERID = "@POVoucherID";

		private const string SOURCESYSDOCID = "@SourceSysDocID";

		private const string SOURCEVOUCHERID = "@SourceVoucherID";

		private const string TRANSPORTERID_PARM = "@TransporterID";

		private const string DRIVERID_PARM = "@DriverID";

		private const string VEHICLEID_PARM = "@VehicleID";

		private const string CONTAINERNO_PARM = "@ContainerNumber";

		private const string CONTAINERSIZEID_PARM = "@ContainerSizeID";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORY_PARM = "@CostCategoryID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string ORDERVOUCHERID_PARM = "@OrderVoucherID";

		private const string ORDERSYSDOCID_PARM = "@OrderSysDocID";

		private const string ORDERROWINDEX_PARM = "@OrderRowIndex";

		private const string ISPORROW_PARM = "@IsPORRow";

		private const string ROWSOURCE_PARM = "@ROWSOURCE";

		private const string PKVOUCHERID_PARM = "@PKVoucherID";

		private const string PKSYSDOCID_PARM = "@PKSysDocID";

		private const string PKROWINDEX_PARM = "@PKRowIndex";

		private const string LISTVOUCHERID_PARM = "@ListVoucherID";

		private const string LISTSYSDOCID_PARM = "@ListSysDocID";

		private const string LISTROWINDEX_PARM = "@ListRowIndex";

		private const string REMARKS_PARM = "@Remarks";

		private const string SPECIFICATIONID_PARM = "@SpecificationID";

		private const string STYLEID_PARM = "@StyleID";

		private const string REFSLNO_PARM = "@RefSlNo";

		private const string REFTEXT1_PARM = "@RefText1";

		private const string REFTEXT2_PARM = "@RefText2";

		private const string REFNUM1_PARM = "@RefNum1";

		private const string REFNUM2_PARM = "@RefNum2";

		private const string REFDATE1_PARM = "@RefDate1";

		private const string REFDATE2_PARM = "@RefDate2";

		public PurchaseReceipt(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePurchaseReceiptText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Receipt", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("PurchaseFlow", "@PurchaseFlow"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("SourceDocType", "@ItemSourceTypes"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("VendorReferenceNo", "@VendorReferenceNo"), new FieldValue("IsImport", "@IsImport"), new FieldValue("POSysDocID", "@POSysDocID"), new FieldValue("POVoucherID", "@POVoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("TransporterID", "@TransporterID"), new FieldValue("DriverID", "@DriverID"), new FieldValue("VehicleID", "@VehicleID"), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("ContainerSizeID", "@ContainerSizeID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseReceiptCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseReceiptText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseReceiptText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@PurchaseFlow", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@BuyerID", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@IsImport", SqlDbType.Bit);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@ItemSourceTypes", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@VendorReferenceNo", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@DiscountFC", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@TotalFC", SqlDbType.Decimal);
			parameters.Add("@POSysDocID", SqlDbType.NVarChar);
			parameters.Add("@POVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransporterID", SqlDbType.NVarChar);
			parameters.Add("@DriverID", SqlDbType.NVarChar);
			parameters.Add("@VehicleID", SqlDbType.NVarChar);
			parameters.Add("@ContainerNumber", SqlDbType.NVarChar);
			parameters.Add("@ContainerSizeID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@PurchaseFlow"].SourceColumn = "PurchaseFlow";
			parameters["@IsImport"].SourceColumn = "IsImport";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@VendorReferenceNo"].SourceColumn = "VendorReferenceNo";
			parameters["@ItemSourceTypes"].SourceColumn = "SourceDocType";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@DiscountFC"].SourceColumn = "DiscountFC";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@TotalFC"].SourceColumn = "TotalFC";
			parameters["@POSysDocID"].SourceColumn = "POSysDocID";
			parameters["@POVoucherID"].SourceColumn = "POVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@TransporterID"].SourceColumn = "TransporterID";
			parameters["@DriverID"].SourceColumn = "DriverID";
			parameters["@VehicleID"].SourceColumn = "VehicleID";
			parameters["@ContainerNumber"].SourceColumn = "ContainerNumber";
			parameters["@ContainerSizeID"].SourceColumn = "ContainerSizeID";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdatePurchaseReceiptDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Receipt_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("OrderSysDocID", "@OrderSysDocID"), new FieldValue("OrderVoucherID", "@OrderVoucherID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("PKVoucherID", "@PKVoucherID"), new FieldValue("PKSysDocID", "@PKSysDocID"), new FieldValue("PKRowIndex", "@PKRowIndex"), new FieldValue("ListVoucherID", "@ListVoucherID"), new FieldValue("ListSysDocID", "@ListSysDocID"), new FieldValue("ListRowIndex", "@ListRowIndex"), new FieldValue("ROWSOURCE", "@ROWSOURCE"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("RefSlNo", "@RefSlNo"), new FieldValue("RefText1", "@RefText1"), new FieldValue("RefText2", "@RefText2"), new FieldValue("RefNum1", "@RefNum1"), new FieldValue("RefNum2", "@RefNum2"), new FieldValue("RefDate1", "@RefDate1"), new FieldValue("RefDate2", "@RefDate2"), new FieldValue("SpecificationID", "@SpecificationID"), new FieldValue("StyleID", "@StyleID"), new FieldValue("Remarks", "@Remarks"), new FieldValue("IsPORRow", "@IsPORRow"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseReceiptDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseReceiptDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseReceiptDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@OrderSysDocID", SqlDbType.NVarChar);
			parameters.Add("@OrderVoucherID", SqlDbType.NVarChar);
			parameters.Add("@OrderRowIndex", SqlDbType.Int);
			parameters.Add("@ROWSOURCE", SqlDbType.Int);
			parameters.Add("@IsPORRow", SqlDbType.Bit);
			parameters.Add("@PKSysDocID", SqlDbType.NVarChar);
			parameters.Add("@PKVoucherID", SqlDbType.NVarChar);
			parameters.Add("@PKRowIndex", SqlDbType.Int);
			parameters.Add("@SpecificationID", SqlDbType.NVarChar);
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters.Add("@ListSysDocID", SqlDbType.NVarChar);
			parameters.Add("@ListVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ListRowIndex", SqlDbType.Int);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
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
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@OrderVoucherID"].SourceColumn = "OrderVoucherID";
			parameters["@OrderSysDocID"].SourceColumn = "OrderSysDocID";
			parameters["@OrderRowIndex"].SourceColumn = "OrderRowIndex";
			parameters["@IsPORRow"].SourceColumn = "IsPORRow";
			parameters["@ROWSOURCE"].SourceColumn = "ROWSOURCE";
			parameters["@PKSysDocID"].SourceColumn = "PKSysDocID";
			parameters["@PKVoucherID"].SourceColumn = "PKVoucherID";
			parameters["@SpecificationID"].SourceColumn = "SpecificationID";
			parameters["@StyleID"].SourceColumn = "StyleID";
			parameters["@ListSysDocID"].SourceColumn = "ListSysDocID";
			parameters["@ListVoucherID"].SourceColumn = "ListVoucherID";
			parameters["@ListRowIndex"].SourceColumn = "ListRowIndex";
			parameters["@PKRowIndex"].SourceColumn = "PKRowIndex";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
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

		private bool ValidateData(PurchaseReceiptData journalData)
		{
			return true;
		}

		private bool CanUpdate(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = false;
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("Purchase_Receipt", "IsInvoiced", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
			if (fieldValue != null && fieldValue.ToString() != "")
			{
				flag = bool.Parse(fieldValue.ToString());
			}
			if (flag)
			{
				return false;
			}
			return true;
		}

		public bool InsertUpdatePurchaseReceipt(PurchaseReceiptData purchaseReceiptData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePurchaseReceiptCommand = GetInsertUpdatePurchaseReceiptCommand(isUpdate);
			string text = "";
			string text2 = "";
			string text3 = "";
			try
			{
				DataRow dataRow = purchaseReceiptData.PurchaseReceiptTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text4 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (isUpdate && !CanUpdate(sysDocID, text4, sqlTransaction))
				{
					throw new CompanyException("Cannot modify this document because it is already invoiced.", 1037);
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Purchase_Receipt", "VoucherID", dataRow["SysDocID"].ToString(), text4, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				bool flag2 = false;
				bool num = bool.Parse(dataRow["ActivateGrnEdit"].ToString());
				bool flag3 = new PurchaseReceipt(base.DBConfig).CanEdit(sysDocID, text4);
				if (num && !flag3)
				{
					flag2 = true;
				}
				bool flag4 = false;
				if (dataRow["IsImport"] != DBNull.Value)
				{
					flag4 = Convert.ToBoolean(dataRow["IsImport"].ToString());
				}
				foreach (DataRow row in purchaseReceiptData.PurchaseReceiptDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					text3 = row["ProductID"].ToString();
					string checkFieldValue = row["LocationID"].ToString();
					decimal result = default(decimal);
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product_Location", "Quantity", "ProductID", text3, "LocationID", checkFieldValue, sqlTransaction);
					if (fieldValue != null)
					{
						decimal.TryParse(fieldValue.ToString(), out result);
					}
					float num2 = 0f;
					string text5 = "";
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text3, sqlTransaction);
					if (fieldValue != null)
					{
						text5 = fieldValue.ToString();
					}
					if (text5 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text5)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text3, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text3 + "\nUnit:" + row["UnitID"].ToString());
						float num3 = float.Parse(obj["Factor"].ToString());
						string text6 = obj["FactorType"].ToString();
						num2 = float.Parse(row["Quantity"].ToString());
						row["UnitFactor"] = num3;
						row["FactorType"] = text6;
						row["UnitQuantity"] = row["Quantity"];
						num2 = ((!(text6 == "M")) ? float.Parse(Math.Round(num2 * num3, 5).ToString()) : float.Parse(Math.Round(num2 / num3, 5).ToString()));
						row["Quantity"] = num2;
					}
				}
				insertUpdatePurchaseReceiptCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(purchaseReceiptData, "Purchase_Receipt", insertUpdatePurchaseReceiptCommand)) : (flag & Insert(purchaseReceiptData, "Purchase_Receipt", insertUpdatePurchaseReceiptCommand)));
				if (flag2 && isUpdate)
				{
					flag &= InsertGRNDetails(purchaseReceiptData, sqlTransaction);
				}
				insertUpdatePurchaseReceiptCommand = GetInsertUpdatePurchaseReceiptDetailsCommand(isUpdate: false);
				insertUpdatePurchaseReceiptCommand.Transaction = sqlTransaction;
				if (isUpdate && !flag2)
				{
					flag &= DeletePurchaseReceiptDetailsRows(sysDocID, text4, isDeletingTransaction: false, sqlTransaction);
				}
				if (purchaseReceiptData.Tables["Purchase_Receipt_Detail"].Rows.Count > 0 && !flag2)
				{
					flag &= Insert(purchaseReceiptData, "Purchase_Receipt_Detail", insertUpdatePurchaseReceiptCommand);
				}
				if (!flag2)
				{
					InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
					foreach (DataRow row2 in purchaseReceiptData.PurchaseReceiptDetailTable.Rows)
					{
						text3 = row2["ProductID"].ToString();
						DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
						dataRow4.BeginEdit();
						dataRow4["SysDocID"] = row2["SysDocID"];
						dataRow4["VoucherID"] = row2["VoucherID"];
						if (row2["LocationID"].ToString() == "")
						{
							throw new Exception("Location cannot be empty.");
						}
						dataRow4["LocationID"] = row2["LocationID"];
						dataRow4["JobID"] = row2["JobID"];
						dataRow4["ProductID"] = row2["ProductID"];
						dataRow4["Quantity"] = decimal.Parse(row2["Quantity"].ToString());
						dataRow4["Reference"] = dataRow["Reference"];
						if (flag4)
						{
							dataRow4["SysDocType"] = (byte)50;
						}
						else
						{
							dataRow4["SysDocType"] = (byte)32;
						}
						object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Product", "LastCost", "ProductID", text3, sqlTransaction);
						if (fieldValue2 != null && fieldValue2.ToString() != "")
						{
							dataRow4["UnitPrice"] = fieldValue2.ToString();
						}
						else
						{
							dataRow4["UnitPrice"] = 0;
						}
						dataRow4["IsNonCostedGRN"] = true;
						dataRow4["TransactionDate"] = dataRow["TransactionDate"];
						dataRow4["TransactionType"] = (byte)1;
						dataRow4["PayeeType"] = "V";
						dataRow4["PayeeID"] = dataRow["VendorID"];
						dataRow4["Cost"] = dataRow4["UnitPrice"];
						dataRow4["RowIndex"] = row2["RowIndex"];
						dataRow4["SpecificationID"] = row2["SpecificationID"];
						dataRow4["StyleID"] = row2["StyleID"];
						dataRow4["CompanyID"] = dataRow["CompanyID"];
						dataRow4["DivisionID"] = dataRow["DivisionID"];
						dataRow4["UnitID"] = row2["UnitID"];
						if (row2["UnitQuantity"] != DBNull.Value && row2["UnitFactor"] != DBNull.Value)
						{
							dataRow4["UnitQuantity"] = row2["UnitQuantity"];
							dataRow4["Factor"] = row2["UnitFactor"];
							dataRow4["FactorType"] = row2["FactorType"];
							decimal.Parse(row2["UnitFactor"].ToString());
							row2["FactorType"].ToString();
							decimal d = decimal.Parse(row2["UnitQuantity"].ToString());
							decimal num4 = decimal.Parse(row2["Quantity"].ToString());
							decimal d2 = decimal.Parse(row2["UnitPrice"].ToString());
							decimal num5 = default(decimal);
							num5 = ((!(num4 != 0m)) ? default(decimal) : (d * d2 / num4));
							dataRow4["UnitPrice"] = num5;
						}
						dataRow4.EndEdit();
						inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
					}
					inventoryTransactionData.Merge(purchaseReceiptData.Tables["Product_Lot_Receiving_Detail"]);
					flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(purchaseReceiptData, isUpdate: false, sqlTransaction);
					if (inventoryTransactionData.InventoryTransactionTable.Rows.Count > 0)
					{
						flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
					}
				}
				else
				{
					InsertGRNInventoryDetails(purchaseReceiptData, sqlTransaction, flag4);
				}
				string text7 = "";
				string text8 = "";
				if (itemSourceTypes == ItemSourceTypes.PurchaseOrder || itemSourceTypes == ItemSourceTypes.PackingList)
				{
					foreach (DataRow row3 in purchaseReceiptData.PurchaseReceiptDetailTable.Rows)
					{
						text3 = row3["ProductID"].ToString();
						text = row3["OrderVoucherID"].ToString();
						text2 = row3["OrderSysDocID"].ToString();
						int result2 = 0;
						if (!(text == "") && !(text2 == ""))
						{
							int.TryParse(row3["OrderRowIndex"].ToString(), out result2);
							float result3 = 0f;
							if (row3["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row3["UnitQuantity"].ToString(), out result3);
							}
							else
							{
								float.TryParse(row3["Quantity"].ToString(), out result3);
							}
							float num6 = new Products(base.DBConfig).GetOrderedQuantity(text3, sqlTransaction) - result3;
							if (num6 < 0f)
							{
								num6 = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateOrderedQuantity(text3, num6, sqlTransaction);
							switch (itemSourceTypes)
							{
							case ItemSourceTypes.PurchaseOrder:
								if (result2 != -1)
								{
									flag &= new PurchaseOrder(base.DBConfig).UpdateRowReceivedQuantity(text2, text, result2, result3, sqlTransaction);
									if (isUpdate)
									{
										flag &= new PurchaseOrder(base.DBConfig).ReOpenOrder(text2, text, sqlTransaction);
									}
								}
								break;
							case ItemSourceTypes.PackingList:
							{
								text7 = row3["PKVoucherID"].ToString();
								text8 = row3["PKSysDocID"].ToString();
								int num7 = int.Parse(row3["PKRowIndex"].ToString());
								if (num7 != -1)
								{
									flag &= new POShipment(base.DBConfig).UpdateRowReceivedQuantity(text8, text7, num7, result3, sqlTransaction);
								}
								break;
							}
							}
						}
					}
					text = purchaseReceiptData.PurchaseReceiptDetailTable.Rows[0]["OrderVoucherID"].ToString();
					text2 = purchaseReceiptData.PurchaseReceiptDetailTable.Rows[0]["OrderSysDocID"].ToString();
					if (text != "")
					{
						flag &= new PurchaseOrder(base.DBConfig).CloseReceivedOrder(text2, text, sqlTransaction);
					}
					text7 = purchaseReceiptData.PurchaseReceiptDetailTable.Rows[0]["PKVoucherID"].ToString();
					text8 = purchaseReceiptData.PurchaseReceiptDetailTable.Rows[0]["PKSysDocID"].ToString();
					if (text7 != "")
					{
						flag &= new POShipment(base.DBConfig).CloseReceivedShipment(text8, text7, sqlTransaction);
					}
				}
				flag &= UpdateInventoryTransactionRowID(sysDocID, text4, sqlTransaction);
				if (bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.MaterialReservationOnSo, false).ToString()))
				{
					flag &= new Reservation(base.DBConfig).InsertUpdateReservation(purchaseReceiptData, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Purchase_Receipt", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Goods Receive Note";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text4, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text4, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Purchase_Receipt", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				if (!flag4)
				{
					flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.GoodsReceivedNote, sysDocID, text4, "Purchase_Receipt", sqlTransaction);
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ImportGoodsReceivedNote, sysDocID, text4, "Purchase_Receipt", sqlTransaction);
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
				string exp = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Purchase_Receipt_Detail SID  \r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		public PurchaseReceiptData GetPurchaseReceiptByID(string sysDocID, string voucherID)
		{
			try
			{
				PurchaseReceiptData purchaseReceiptData = new PurchaseReceiptData();
				string cmdText = "SELECT PR.*, PaymentTermID, PS.BOLNumber, PS.ClearingAgent, PS.ContainerNumber, PS.Port,B.FullName [BuyerName],V.TaxOption FROM Purchase_Receipt PR   INNER JOIN Vendor V ON PR.VendorID = V.VendorID  LEFT JOIN Buyer B ON PR.BuyerID = B.BuyerID  LEFT OUTER JOIN PO_Shipment PS ON PR.Reference = PS.VoucherID WHERE PR.VoucherID='" + voucherID + "' AND PR.SysDocID='" + sysDocID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(purchaseReceiptData, "Purchase_Receipt", sqlCommand);
				if (purchaseReceiptData == null || purchaseReceiptData.Tables.Count == 0 || purchaseReceiptData.Tables["Purchase_Receipt"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT POD.UnitPrice,TD.*,Product.Description,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.IsTrackLot,Product.IsTrackSerial, Product.MatrixParentID,Product.ItemType,ISNull(Product.TaxGroupID,PC.TaxGroupID) AS ItemTaxGroupID\r\n                       ,TD.TaxOption ,POD.QuantityReceived AS Received, POD.Quantity AS Ordered\r\n                        FROM Purchase_Receipt_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        LEFT OUTER JOIN Purchase_Order_Detail POD ON TD.OrderSysDocID=POD.SysDocID AND  TD.OrderVoucherID=POD.VoucherID AND TD.OrderRowIndex=POD.RowIndex \r\n                        LEFT OUTER JOIN Product_Class PC ON PC.ClassID = Product.ClassID\r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(purchaseReceiptData, "Purchase_Receipt_Detail", cmdText);
				cmdText = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseReceiptData, "Product_Lot_Receiving_Detail", cmdText);
				cmdText = "SELECT * FROM [Job_Inventory_Issue]\r\n\t\t\t\t\t\tWHERE SourceVoucherID='" + voucherID + "' AND SourceSysDocID='" + sysDocID + "'";
				FillDataSet(purchaseReceiptData, "Job_Inventory_Issue", cmdText);
				cmdText = "SELECT * FROM Purchase_Order PO INNER JOIN Purchase_Receipt PR ON PO.SysDocID=PR.SourceSysDocID AND PO.VoucherID=PR.SourceVoucherID\r\n\t\t\t\t\t\tWHERE PR.VoucherID='" + voucherID + "' AND PR.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseReceiptData, "Purchase_Order", cmdText);
				cmdText = "SELECT DISTINCT PID.SysDocID, PID.VoucherID FROM Purchase_Invoice_Detail PID INNER JOIN Purchase_Receipt_Detail PR ON PR.SysDocID=PID.OrderSysDocID AND PR.VoucherID=PID.OrderVoucherID\r\n\t\t\t\t\t\tWHERE PR.VoucherID='" + voucherID + "' AND PR.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseReceiptData, "Purchase_Invoice_Detail", cmdText);
				cmdText = "SELECT DISTINCT GRD.SysDocID, GRD.VoucherID FROM GRN_Return_Detail GRD INNER JOIN Purchase_Receipt_Detail PR ON PR.SysDocID=GRD.SourceSysDocID AND PR.VoucherID=GRD.SourceVoucherID\r\n\t\t\t\t\t\tWHERE PR.VoucherID='" + voucherID + "' AND PR.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseReceiptData, "GRN_Return_Detail", cmdText);
				cmdText = "SELECT DISTINCT POS.SysDocID, POS.VoucherID FROM PO_Shipment POS INNER JOIN Purchase_Receipt PR ON PR.SourceSysDocID=POS.SysDocID AND PR.SourceVoucherID=POS.VoucherID\r\n\t\t\t\t\t\tWHERE PR.VoucherID='" + voucherID + "' AND PR.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseReceiptData, "PO_Shipment", cmdText);
				return purchaseReceiptData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePurchaseReceiptDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Purchase_Receipt", "IsInvoiced", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = bool.Parse(fieldValue.ToString());
				}
				if (flag2)
				{
					throw new CompanyException("Cannot delete this document because it is already invoiced.", 1025);
				}
				PurchaseReceiptData purchaseReceiptData = new PurchaseReceiptData();
				string textCommand = "SELECT SOD.*,ISVOID,IsImport FROM Purchase_Receipt_Detail SOD INNER JOIN Purchase_Receipt SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(purchaseReceiptData, "Purchase_Receipt_Detail", textCommand, sqlTransaction);
				bool result = false;
				if (purchaseReceiptData.PurchaseReceiptDetailTable.Rows.Count == 0)
				{
					return true;
				}
				bool.TryParse(purchaseReceiptData.PurchaseReceiptDetailTable.Rows[0]["IsImport"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(purchaseReceiptData.PurchaseReceiptDetailTable.Rows[0]["IsVoid"].ToString(), out result2);
				if (!result2)
				{
					flag = ((!result) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(32, sysDocID, voucherID, isDeletingTransaction, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(50, sysDocID, voucherID, isDeletingTransaction, sqlTransaction)));
					string text = "";
					string text2 = "";
					string text3 = "";
					foreach (DataRow row in purchaseReceiptData.PurchaseReceiptDetailTable.Rows)
					{
						ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
						if (row["RowSource"] != DBNull.Value)
						{
							itemSourceTypes = (ItemSourceTypes)byte.Parse(row["RowSource"].ToString());
						}
						text3 = row["ProductID"].ToString();
						text = row["OrderVoucherID"].ToString();
						text2 = row["OrderSysDocID"].ToString();
						int result3 = 0;
						if (!(text == "") && !(text2 == ""))
						{
							int.TryParse(row["OrderRowIndex"].ToString(), out result3);
							float result4 = 0f;
							if (row["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row["UnitQuantity"].ToString(), out result4);
							}
							else
							{
								float.TryParse(row["Quantity"].ToString(), out result4);
							}
							float num = new Products(base.DBConfig).GetOrderedQuantity(text3, sqlTransaction) + result4;
							if (num < 0f)
							{
								num = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateOrderedQuantity(text3, num, sqlTransaction);
							if (result3 != -1)
							{
								flag &= new PurchaseOrder(base.DBConfig).UpdateRowReceivedQuantity(text2, text, result3, -1f * result4, sqlTransaction);
							}
							if (itemSourceTypes == ItemSourceTypes.PackingList)
							{
								string text4 = row["PKSysDocID"].ToString();
								string text5 = row["PKVoucherID"].ToString();
								int num2 = int.Parse(row["PKRowIndex"].ToString());
								if (num2 != -1)
								{
									flag &= new POShipment(base.DBConfig).UpdateRowReceivedQuantity(text4, text5, num2, -1f * result4, sqlTransaction);
								}
								string exp = "UPDATE POS  SET Status = 2, IsReceived = 'False'\r\n                                            FROM PO_Shipment POS INNER JOIN PO_Shipment_Detail POSD ON POS.SysDocID = POSD.SysDocID AND POS.VoucherID = POSD.VoucherID\r\n                                            WHERE POS.SysDocID = '" + text4 + "' AND POS.VoucherID = '" + text5 + "' AND Status IN (1,2,3) \r\n                                            AND (SELECT  CASE WHEN SUM(ISNULL(QuantityReceived,0)) <= SUM(Quantity) THEN 'True' ELSE 'False' END   FROM PO_Shipment_Detail WHERE SysDocID = '" + text4 + "' AND VoucherID = '" + text5 + "') = 'True' ";
								flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
							}
						}
					}
				}
				if (bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.MaterialReservationOnSo, false).ToString()))
				{
					flag &= new Reservation(base.DBConfig).DeleteReservationDetailsRows(sysDocID, voucherID, isDeletingTransaction: false, sqlTransaction);
				}
				textCommand = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Purchase_Receipt_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidPurchaseReceipt(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidPurchaseReceipt(sysDocID, voucherID, isVoid, null);
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

		private bool VoidPurchaseReceipt(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Cannot modify this document because it is already invoiced.", 1037);
				}
				if (!CanEdit(sysDocID, voucherID))
				{
					throw new CompanyException("This transaction cannot be modifed because some of items are refered by other transactions.");
				}
				PurchaseReceiptData purchaseReceiptData = new PurchaseReceiptData();
				string textCommand = "SELECT PRD.*, PR.IsImport FROM Purchase_Receipt_Detail PRD\r\n                                    INNER JOIN Purchase_Receipt PR  ON PRD.SysDocID = PR.SysDocID AND PRD.VoucherID = PR.VoucherID\r\n                              WHERE PRD.SysDocID = '" + sysDocID + "' AND PRD.VoucherID = '" + voucherID + "'";
				FillDataSet(purchaseReceiptData, "Purchase_Receipt_Detail", textCommand, sqlTransaction);
				bool flag2 = false;
				if (purchaseReceiptData != null && purchaseReceiptData.Tables.Count > 0)
				{
					flag2 = Convert.ToBoolean(purchaseReceiptData.PurchaseReceiptDetailTable.Rows[0]["IsImport"].ToString());
				}
				flag = ((!flag2) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(32, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(50, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)));
				string text = "";
				string text2 = "";
				string text3 = "";
				foreach (DataRow row in purchaseReceiptData.PurchaseReceiptDetailTable.Rows)
				{
					ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
					if (row["RowSource"] != DBNull.Value)
					{
						itemSourceTypes = (ItemSourceTypes)byte.Parse(row["RowSource"].ToString());
					}
					text3 = row["ProductID"].ToString();
					text = row["OrderVoucherID"].ToString();
					text2 = row["OrderSysDocID"].ToString();
					int result = 0;
					if (!(text == "") && !(text2 == ""))
					{
						int.TryParse(row["OrderRowIndex"].ToString(), out result);
						float result2 = 0f;
						if (row["UnitQuantity"] != DBNull.Value)
						{
							float.TryParse(row["UnitQuantity"].ToString(), out result2);
						}
						else
						{
							float.TryParse(row["Quantity"].ToString(), out result2);
						}
						float num = new Products(base.DBConfig).GetReservedQuantity(text3, sqlTransaction) + result2;
						if (num < 0f)
						{
							num = 0f;
						}
						flag &= new Products(base.DBConfig).UpdateReservedQuantity(text3, num, sqlTransaction);
						if (result != -1)
						{
							flag &= new PurchaseOrder(base.DBConfig).UpdateRowReceivedQuantity(text2, text, result, -1f * result2, sqlTransaction);
						}
						if (flag)
						{
							textCommand = "UPDATE Purchase_Order SET Status=1 WHERE SysDocID='" + text2 + "' AND VoucherID='" + text + "' ";
							flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
						}
						if (itemSourceTypes == ItemSourceTypes.PackingList)
						{
							string text4 = row["PKSysDocID"].ToString();
							string text5 = row["PKVoucherID"].ToString();
							int num2 = int.Parse(row["PKRowIndex"].ToString());
							if (num2 != -1)
							{
								flag &= new POShipment(base.DBConfig).UpdateRowReceivedQuantity(text4, text5, num2, -1f * result2, sqlTransaction);
							}
							textCommand = "UPDATE POS  SET Status = 1, IsReceived = 'False'\r\n                                            FROM PO_Shipment POS INNER JOIN PO_Shipment_Detail POSD ON POS.SysDocID = POSD.SysDocID AND POS.VoucherID = POSD.VoucherID\r\n                                            WHERE POS.SysDocID = '" + text4 + "' AND POS.VoucherID = '" + text5 + "' AND Status IN (1,2,3) \r\n                                            AND (SELECT  CASE WHEN SUM(ISNULL(QuantityReceived,0)) <= SUM(Quantity) THEN 'True' ELSE 'False' END   FROM PO_Shipment_Detail WHERE SysDocID = '" + text4 + "' AND VoucherID = '" + text5 + "') = 'True' ";
							flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
						}
					}
				}
				textCommand = "UPDATE Purchase_Receipt SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Purchase Receipt", voucherID, sysDocID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeletePurchaseReceipt(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Cannot delete this document because it is already invoiced.", 1037);
				}
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Purchase_Receipt", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidPurchaseReceipt(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeletePurchaseReceiptDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				text = "DELETE FROM Purchase_Receipt WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Purchase Receipt", voucherID, sysDocID, activityType, sqlTransaction);
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

		internal bool CloseReceivedReceipt(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string str = "SELECT COUNT(RowIndex)FROM Purchase_Receipt_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				str += " AND CASE WHEN UnitQuantity IS NULL THEN Quantity ELSE UnitQuantity END - ISNULL(QuantityReceived,0)=0";
				object obj = ExecuteScalar(str, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				str = "UPDATE Purchase_Receipt SET IsDelivered = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
			float result2 = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReceived FROM Purchase_Receipt_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				textCommand = "UPDATE Purchase_Receipt_Detail SET QuantityReceived=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateGRNReturnedQuantity(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "UPDATE GRND SET QuantityReturned = (SELECT SUM(Quantity) FROM GRN_Return_Detail GRNR INNER JOIN GRN_Return GR ON GR.SysDocID = GRNR.SysDocID\r\n\t\t\t\t\t\t\t\t\tAND GR.VoucherID = GRNR.VoucherID WHERE \r\n                                    GRND.SysDocID = GRNR.SourceSysDocID  AND GRND.VoucherID = GRNR.SourceVoucherID AND GRND.RowIndex = GRNR.SourceRowIndex\r\n                                    AND ISNULL(IsVoid,'False')= 'False')\r\n                                    FROM Purchase_Receipt_Detail GRND\r\n                                     WHERE SysDocID = '" + sysDocID + "' AnD VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				exp = "UPDATE IT SET ReturnedQuantity = (SELECT SUM(Quantity) FROM GRN_Return_Detail GRNR INNER JOIN GRN_Return GR ON GR.SysDocID = GRNR.SysDocID\r\n\t\t\t\t\t\t\t\t\tAND GR.VoucherID = GRNR.VoucherID WHERE \r\n                                    IT.SysDocID = GRNR.SourceSysDocID  AND IT.VoucherID = GRNR.SourceVoucherID AND IT.RowIndex = GRNR.SourceRowIndex\r\n                                         AND ISNULL(IsVoid,'False')= 'False')\r\n                                    FROM Inventory_Transactions IT\r\n                                     WHERE SysDocID = '" + sysDocID + "' AnD VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				exp = "UPDATE POD SET QuantityReceived = (SELECT SUM(PRD.Quantity) - SUM(ISNULL(PRD.QuantityReturned,0)) FROM Purchase_Receipt_Detail PRD INNER JOIN GRN_Return_Detail GRD ON GRD.SourceSysDocID = PRD.SysDocID\r\n                AND GRD.SourceVoucherID = PRD.VoucherID INNER JOIN Purchase_Receipt PR ON PR.SysDocID=PRD.SysDocID AND PR.VoucherID=PRD.VoucherID\r\n                WHERE  PRD.SysDocID = GRD.SourceSysDocID  AND PRD.VoucherID = GRD.SourceVoucherID AND PRD.RowIndex = GRD.SourceRowIndex\r\n                AND ISNULL(PR.IsVoid,'False')= 'False'  AND PRD.SysDocID ='" + sysDocID + "' AND  PRD.VoucherID ='" + voucherID + "' ) FROM Purchase_Order_Detail POD\r\n                WHERE POD.SysDocID IN (SELECT PRD2.OrderSysDocID FROM Purchase_Receipt_Detail PRD2 INNER JOIN  GRN_Return_Detail GRD2 ON PRD2.SysDocID=GRD2.SourceSysDocID AND PRD2.VoucherID=GRD2.SourceVoucherID WHERE PRD2.SysDocID = '" + sysDocID + "' AnD PRD2.VoucherID = '" + voucherID + "')\r\n                AND  POD.VoucherID IN (SELECT PRD2.OrderVoucherID FROM Purchase_Receipt_Detail PRD2 INNER JOIN  GRN_Return_Detail GRD2 ON PRD2.SysDocID=GRD2.SourceSysDocID AND PRD2.VoucherID=GRD2.SourceVoucherID WHERE PRD2.SysDocID = '" + sysDocID + "' AnD PRD2.VoucherID = '" + voucherID + "')";
				return flag & (ExecuteNonQuery(exp, sqlTransaction) >= 0);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetUninvoicedGRNs(string vendorID, bool isImport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = " SELECT PR.SysDocID [Doc ID],PR.VoucherID [Number],TransactionDate AS [Date],PR.VendorID + '-' + V.VendorName AS [Vendor], ContainerNumber AS [Container No],[Reference] as Reference1,Reference2 FROM Purchase_Receipt PR\r\n                                INNER JOIN Purchase_Receipt_Detail PRD ON PRD.SysDocID = PR.SysDocID AND PRD.VoucherID = PR.VoucherID \r\n                             INNER JOIN Vendor V ON PR.VendorID=V.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsInvoiced,'False')='False' ";
				if (vendorID != "")
				{
					str = str + " AND PR.VendorID='" + vendorID + "'";
				}
				str = str + " AND ISNULL(PR.IsImport,'False')='" + isImport.ToString() + "'";
				str += " GROUP BY PR.SysDocID  ,PR.VoucherID  ,TransactionDate  ,PR.VendorID, V.VendorName , ContainerNumber,Reference,Reference2\r\n\t\t\t\t\t\t\t  HAVING SUM(Quantity - ISNULL(QuantityReturned,0)) > 0 ";
				FillDataSet(dataSet, "Purchase_Receipt", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetUninvoicedGRNsOnLocation(string vendorID, bool isImport, string locationID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = " SELECT PR.SysDocID [Doc ID],PR.VoucherID [Number],TransactionDate AS [Date],PR.VendorID + '-' + V.VendorName AS [Vendor], ContainerNumber AS [Container No] FROM Purchase_Receipt PR\r\n                                INNER JOIN Purchase_Receipt_Detail PRD ON PRD.SysDocID = PR.SysDocID AND PRD.VoucherID = PR.VoucherID \r\n                                INNER JOIN System_Document SD ON SD.SysDocID=PR.SysDocID\r\n                             INNER JOIN Vendor V ON PR.VendorID=V.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsInvoiced,'False')='False' ";
				if (vendorID != "")
				{
					str = str + " AND PR.VendorID='" + vendorID + "'";
				}
				str = str + " AND ISNULL(PR.IsImport,'False')='" + isImport.ToString() + "'";
				if (!string.IsNullOrEmpty(locationID))
				{
					str = str + " AND SD.LocationID='" + locationID + "'";
				}
				str += " GROUP BY PR.SysDocID  ,PR.VoucherID  ,TransactionDate  ,PR.VendorID, V.VendorName , ContainerNumber\r\n\t\t\t\t\t\t\t  HAVING SUM(Quantity - ISNULL(QuantityReturned,0)) > 0 ";
				FillDataSet(dataSet, "Purchase_Receipt", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool CanEdit(string sysDocID, string voucherID)
		{
			try
			{
				string exp = "SELECT COUNT(*) FROM Product_Lot PL \r\n                            WHERE DocID = '" + sysDocID + "' AND ReceiptNumber = '" + voucherID + "' AND PL.LotNumber IN (SELECT LotNumber FROM Product_Lot_Issue_Detail PLD WHERE PLD.ProductID = PL.ItemCode)";
				object obj = ExecuteScalar(exp);
				if (obj != null && obj.ToString() != "")
				{
					exp = "SELECT COUNT(*) FROM Job_Inventory_Issue JI \r\n                            WHERE SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherID + "'";
					if (int.Parse(ExecuteScalar(exp).ToString()) > 0)
					{
						return false;
					}
				}
				if (obj.ToString() != null && obj.ToString() != "")
				{
					return int.Parse(obj.ToString()) == 0;
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		public bool ValidateProcessedDocumentEdit(string sysDocID, string voucherID, DateTime currentTransactionDate)
		{
			try
			{
				new DataSet();
				string exp = "SELECT Top 1 TransactionDate FROM Inventory_Transactions PL \r\n                            WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				object obj = ExecuteScalar(exp);
				if (obj != null && obj.ToString() != "")
				{
					DateTime t = DateTime.Parse(obj.ToString());
					if (currentTransactionDate > t)
					{
						return false;
					}
					return true;
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, bool includeLocal, bool includeImport, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Quote Date],\r\n                            INV.BuyerID [Buyer],Total [Amount], Reference as Ref1, Reference2 as Ref2,INV.CurrencyID[Currency]\r\n                            FROM         Purchase_Receipt INV\r\n                            Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			if (!includeImport)
			{
				text3 += " AND ISNULL(IsImport,'False') = 'False' ";
			}
			if (!includeLocal)
			{
				text3 += " AND ISNULL(IsImport,'False') = 'True' ";
			}
			if (sysDocID != "")
			{
				text3 = text3 + " AND INV.SysDocID = '" + sysDocID + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Receipt", sqlCommand);
			return dataSet;
		}

		public DataSet GetPurchaseReceiptToPrint(string sysDocID, string[] voucherID, bool showLotDetail, bool showOrderandShipmentinGRN)
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
				string cmdText = "SELECT DISTINCT SI.*,Vendor.VendorName,VA.AddressPrintFormat AS VendorAddress,ShippingMethodName, \r\n                                (SELECT TOP 1 LocationName FROM Purchase_Receipt_Detail PRD LEFT JOIN Location L ON  PRD.LocationID=PRD.LocationID \r\n                                WHERE PRD.SysDocID=SI.SysDocID AND PRD.VoucherID=SI.VoucherID) AS Location,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,                                \r\n                                ISNULL(SI.TaxAmount ,0) AS Tax, SI.Total AS Total,(SELECT TransporterName FROM Transporter\r\n                                 WHERE TransporterID = SI.TransporterID) AS TransporterName, PO.PortLoading, PO.PortDestination,\r\n                                VA.Phone1, VA.Phone2, VA.Mobile,VA.Fax,Va.Email, PT.TermName,Vendor.TaxIDNumber as VTaxIDNo, CS.ContainerSizeName,  V.VehicleName, D.DriverName\r\n\r\n                                FROM  Purchase_Receipt SI INNER JOIN Vendor  ON SI.VendorID=Vendor.VendorID\r\n                                LEFT OUTER JOIN Purchase_Order PO ON SI.POSysDocID = PO.SysDocID AND SI.POVoucherID = PO.VoucherID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID  \r\n                                LEFT OUTER JOIN ContainerSize CS ON Si.ContainerSizeID=CS.ContainerSizeID \r\n                                LEFT OUTER JOIN Vehicle V ON SI.VehicleID=V.VehicleID\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN Driver D on Si.DriverID=D.DriverID\r\n                                WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Purchase_Receipt", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Purchase_Receipt"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "\r\n\t\t\t\t\t\t\tSELECT DISTINCT PRD.SysDocID, PRD.VoucherID,PRD.ProductID,PRD.Description,ISNULL(PRD.UnitQuantity,PRD.Quantity) AS ReceivedQuantity, \r\n                       ISNULL((SELECT TOP 1 ISNULL(POD.UnitQuantity, POD.Quantity) FROM Purchase_Order_Detail POD \r\n                        WHERE POD.SysDocID = PRD.OrderSysDocID AND POD.VoucherID = PRD.OrderVoucherID  AND POD.ProductID = PRD.ProductID),\r\n                        (SELECT TOP 1 ISNULL(POD.UnitQuantity, POD.Quantity) FROM PO_Shipment_Detail POD \r\n                        WHERE POD.SysDocID = PRD.PKSysDocID AND POD.VoucherID = PRD.PKVoucherID  AND POD.ProductID = PRD.ProductID)) AS OrderedQuantity,\r\n                        ISNULL((SELECT TOP 1 ISNULL(POD.QuantityReceived, POD.Quantity) FROM Purchase_Order_Detail POD \r\n                        WHERE POD.SysDocID = PRD.OrderSysDocID AND POD.VoucherID = PRD.OrderVoucherID  AND POD.ProductID = PRD.ProductID),0) AS QtyReceived,\r\n\r\n                        P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID, PRD.UnitPrice AS UnitPrice,PRD.LocationID,\r\n                        ISNULL(PRD.UnitQuantity,PRD.Quantity)*ISNULL(PRD.UnitPrice,0) AS Total,PRD.UnitID, P.BrandID, PRD.RowIndex AS RowIndex,\r\n\t\t\t\t\t\t PB.BrandName, P.Description2,PRD.Remarks,(select TransactionDate from Purchase_Order where SysDocID=PRD.OrderSysDocID AND VoucherID=PRD.OrderVoucherID)AS LPODate,\r\n\t\t\t\t\t\t  PRD.OrderVoucherID AS LPONumber,PRD.SpecificationID, SpecificationName,PRD.RefSlNo, PRD.RefText1,PRD.RefText2,PRD.RefNum1,PRD.RefNum2,PRD.RefDate1,PRD.RefDate2\r\n                        FROM   Purchase_Receipt_Detail PRD\r\n                        INNER JOIN Product P ON P.ProductID=PRD.ProductID\r\n                        LEFT JOIN  Product_Lot_Receiving_Detail PRP ON PRD.SysDocID=PRP.SysDocID AND PRD.VoucherID=PRP.VoucherID AND PRD.ProductID=PRP.ProductID LEFT JOIN\r\n                        Product_Lot PL ON PL.LotNumber=PRP.LotNumber \r\n                        LEFT JOIN Product_Brand PB ON P.BrandID=PB.BrandID\r\n\t\t\t\t\t\tLEFT JOIN Product_Specification PS ON PS.SpecificationID=PRD.SpecificationID\r\n                        WHERE PRD.SysDocID='" + sysDocID + "' AND PRD.VoucherID IN (" + text + ")";
				if (showOrderandShipmentinGRN)
				{
					cmdText = cmdText + "UNION SELECT   '" + sysDocID + "' AS SysDocID ," + text + " AS VoucherID, PDS.ProductID,PDS.Description,'0' AS ReceivedQuantity,ISNULL(PDS.UnitQuantity, PDS.Quantity) AS OrderedQuantity,'0' AS QtyReceived,\r\n                P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,'0' AS UnitPrice,'' AS LocationID,'0' AS Total,PDS.UnitID, P.BrandID, PDS.RowIndex AS RowIndex,PB.BrandName, P.Description2,PRD.Remarks,(select TransactionDate from Purchase_Order where SysDocID=PRD.OrderSysDocID AND VoucherID=PRD.OrderVoucherID)AS LPODate,\r\n                PRD.OrderVoucherID AS LPONumber,PRD.SpecificationID, SpecificationName,PRD.RefSlNo, PRD.RefText1,PRD.RefText2,PRD.RefNum1,PRD.RefNum2,PRD.RefDate1,PRD.RefDate2 \r\n                from PO_Shipment_Detail PDS LEFT JOIN Purchase_Receipt_Detail PRD ON PRD.PKSysDocID=PDS.SysDocID AND PRD.VoucherID=PDS.VoucherID \r\n                INNER JOIN Product P ON P.ProductID=PDS.ProductID\r\n                LEFT JOIN Product_Brand PB ON P.BrandID=PB.BrandID\r\n                LEFT JOIN Product_Specification PS ON PS.SpecificationID=PRD.SpecificationID\r\n                LEFT JOIN  Product_Lot_Receiving_Detail PRP ON PRD.SysDocID=PRP.SysDocID AND PRD.VoucherID=PRP.VoucherID AND PRD.ProductID=PRP.ProductID LEFT JOIN\r\n                Product_Lot PL ON PL.LotNumber=PRP.LotNumber \r\n                WHERE PDS.ProductID NOT IN (SELECT ProductID FROM Purchase_Receipt_Detail PRD WHERE PRD.SysDocID='" + sysDocID + "' AND PRD.VoucherID IN (" + text + ")) AND \r\n                PDS.SysDocID IN (SELECT DISTINCT PRD.PKSysDocID FROM Purchase_Receipt_Detail PRD WHERE PRD.SysDocID='" + sysDocID + "' AND PRD.VoucherID IN (" + text + ")) AND PDS.VoucherID IN (SELECT DISTINCT PRD.PKVoucherID  FROM Purchase_Receipt_Detail PRD WHERE PRD.SysDocID='" + sysDocID + "' AND PRD.VoucherID IN (" + text + "))";
					cmdText = cmdText + "UNION SELECT   '" + sysDocID + "' AS SysDocID ," + text + " AS VoucherID, PDS.ProductID,PDS.Description,'0' AS ReceivedQuantity,ISNULL(PDS.UnitQuantity, PDS.Quantity) AS OrderedQuantity,ISNULL(PDS.QuantityReceived, PDS.Quantity) AS QtyReceived,\r\n                        P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,'0' AS UnitPrice,'' AS LocationID,'0' AS Total,PDS.UnitID, P.BrandID, PDS.RowIndex AS RowIndex,PB.BrandName, P.Description2,\r\n                PRD.Remarks,(select TransactionDate from Purchase_Order where SysDocID=PRD.OrderSysDocID AND VoucherID=PRD.OrderVoucherID)AS LPODate, PRD.OrderVoucherID AS LPONumber,PRD.SpecificationID, SpecificationName,PRD.RefSlNo, PRD.RefText1,PRD.RefText2,PRD.RefNum1,PRD.RefNum2,PRD.RefDate1,PRD.RefDate2 \r\n                from Purchase_Order_Detail PDS LEFT JOIN Purchase_Receipt_Detail PRD ON PRD.PKSysDocID=PDS.SysDocID AND PRD.VoucherID=PDS.VoucherID \r\n                INNER JOIN Product P ON P.ProductID=PDS.ProductID\r\n                LEFT JOIN Product_Brand PB ON P.BrandID=PB.BrandID\r\n                LEFT JOIN Product_Specification PS ON PS.SpecificationID=PRD.SpecificationID\r\n                LEFT JOIN  Product_Lot_Receiving_Detail PRP ON PRD.SysDocID=PRP.SysDocID AND PRD.VoucherID=PRP.VoucherID AND PRD.ProductID=PRP.ProductID LEFT JOIN\r\n                Product_Lot PL ON PL.LotNumber=PRP.LotNumber \r\n                WHERE PDS.ProductID NOT IN (SELECT ProductID FROM Purchase_Receipt_Detail PRD WHERE PRD.SysDocID='" + sysDocID + "' AND PRD.VoucherID IN (" + text + ")) AND \r\n                PDS.SysDocID IN (SELECT DISTINCT PRD.OrderSysDocID FROM Purchase_Receipt_Detail PRD WHERE PRD.SysDocID='" + sysDocID + "' AND PRD.VoucherID IN (" + text + ")) AND PDS.VoucherID IN (SELECT DISTINCT PRD.OrderVoucherID  FROM Purchase_Receipt_Detail PRD WHERE PRD.SysDocID='" + sysDocID + "' AND PRD.VoucherID IN (" + text + ")) \tORDER BY PRD.RowIndex";
				}
				FillDataSet(dataSet, "Purchase_Receipt_Detail", cmdText);
				if (showLotDetail)
				{
					string text2 = "";
					string text3 = "";
					if (dataSet.Tables["Purchase_Receipt_Detail"].Rows.Count > 0)
					{
						text2 = dataSet.Tables["Purchase_Receipt_Detail"].Rows[0]["SysdocID"].ToString();
						text3 = dataSet.Tables["Purchase_Receipt_Detail"].Rows[0]["VoucherID"].ToString();
					}
					cmdText = "SELECT P.*,PRP.SoldQty,PRP.LotNumber,PRP.LotQty,PRP.ExpiryDate,PRP.ProductionDate,PRP.ReceiptDate,PRP.Reference2 AS CAST#, PRP.BinId AS Bin FROM   Purchase_Receipt_Detail PRD\r\n                        INNER JOIN Product P ON P.ProductID=PRD.ProductID\r\n                        LEFT JOIN  Product_Lot_Receiving_Detail PRP ON PRD.SysDocID=PRP.SysDocID AND PRD.VoucherID=PRP.VoucherID AND PRD.ProductID=PRP.ProductID LEFT JOIN\r\n                         Product_Lot PL ON PL.LotNumber=PRP.LotNumber \r\n                        WHERE PRD.SysDocID='" + text2 + "' AND PRD.VoucherID IN ('" + text3 + "')";
					FillDataSet(dataSet, "ProductLot", cmdText);
					dataSet.Relations.Add("ProductLotRel", new DataColumn[1]
					{
						dataSet.Tables["Purchase_Receipt_Detail"].Columns["ProductID"]
					}, new DataColumn[1]
					{
						dataSet.Tables["ProductLot"].Columns["ProductID"]
					}, createConstraints: false);
				}
				dataSet.Relations.Add("PurchaseReceipt", new DataColumn[2]
				{
					dataSet.Tables["Purchase_Receipt"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Receipt"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Purchase_Receipt_Detail"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Receipt_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Purchase_Receipt"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Purchase_Receipt"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Tax"].ToString(), out result2);
					row["TotalInWords"] = NumToWord.GetNumInWords(result + result2);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseReceiptAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT DISTINCT GRN.SysDocID [Doc ID], GRN.VoucherID [Number], GRN.TransactionDate AS [Date],( SELECT Top 1 GRND.JobID FROM  Purchase_Receipt_Detail GRND WHERE GRN.SysDocID=GRND.SysDocID AND GRN.VoucherID=GRND.VoucherID  ORDER BY GRND.RowIndex ASC ) as JobID,\r\n                GRN.Note, GRN.Reference  FROM Purchase_Receipt GRN LEFT JOIN Purchase_Receipt_Detail GRND ON GRN.SysDocID=GRND.SysDocID AND GRN.VoucherID=GRND.VoucherID \r\n                WHERE GRN.SysDocID NOT IN (SELECT DISTINCT ISNULL(SourceSysDocID,'')  FROM Job_Inventory_Issue) OR GRN.VoucherID NOT IN (SELECT DISTINCT ISNULL(SourceVoucherID,'')  FROM Job_Inventory_Issue)";
				FillDataSet(dataSet, "Purchase_Receipt", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetUninvoicedGRNsReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string text3 = "SELECT PR.SysDocID [Doc ID], PR.VoucherID [Number], TransactionDate AS [Date], PONumber [PO Number],\r\n                                PR.VendorID + '-' + V.VendorName AS [Vendor], P.Description, P.Description2,PRD.*,\r\n                                (SELECT DISTINCT PID.Quantity FROM Purchase_Invoice_Detail PID WHERE PID.OrderSysDocID= PRD.SysDocID AND PID.OrderVoucherID=PRD.VoucherID\r\n                                AND PID.ProductID=PRD.ProductID AND PID.OrderRowIndex=PRD.RowIndex )AS QTY  \r\n\r\n                                FROM Purchase_Receipt_Detail PRD INNER JOIN Purchase_Receipt PR ON PRD.SysDocID = PR.SysDocID AND PRD.VoucherID = PR.VoucherID\r\n                                INNER JOIN Vendor V ON PR.VendorID=V.VendorID\r\n                                LEFT JOIN Product P ON P.ProductID=PRD.ProductID                                     \r\n                                  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsInvoiced,'False')='False' AND  PRD.Quantity-ISNULL(QuantityReturned,0) <>0 ";
			text3 = text3 + " AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromItem != "")
			{
				text3 = text3 + " AND PRD.ProductID >= '" + fromItem + "' ";
			}
			if (toItem != "")
			{
				text3 = text3 + " AND PRD.ProductID <= '" + toItem + "' ";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
			}
			if (toClass != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
			}
			if (fromCategory != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
			}
			if (toCategory != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
			}
			if (fromManufacturer != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (fromBrand != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID >= '" + fromBrand + "')";
			}
			if (toBrand != "")
			{
				text3 = text3 + " AND PRD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID <= '" + toBrand + "')";
			}
			if (fromLocation != "")
			{
				text3 = text3 + " AND PRD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
			}
			if (vendorIDs != "")
			{
				text3 = text3 + " AND PR.VendorID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND PR.VendorID >= '" + fromVendor + "'";
			}
			if (toVendor != "")
			{
				text3 = text3 + " AND PR.VendorID <= '" + toVendor + "'";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND PR.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID >= '" + fromClass + "') ";
			}
			if (toClass != "")
			{
				text3 = text3 + " AND PR.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID <= '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND PR.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID >= '" + fromGroup + "') ";
			}
			if (toGroup != "")
			{
				text3 = text3 + " AND PR.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID <= '" + toGroup + "') ";
			}
			text3 += " ORDER BY VendorName ";
			FillDataSet(dataSet, "Purchase_Receipt", text3);
			return dataSet;
		}

		public DataSet GetGRNList(string sysDocID, DateTime fromDate, DateTime toDate, bool includeInvoiced)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT PR.SysDocID [Doc ID], PR.VoucherID [Number], TransactionDate AS [Date],VendorName as Vendor,ContainerNumber as [Container#]    \r\n                            FROM Purchase_Receipt PR INNER JOIN Vendor V ON PR.VendorID=V.VendorID\r\n                            WHERE ISNULL(IsVoid,'False')='False' AND  SysDocID='" + sysDocID + "' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (!includeInvoiced)
			{
				str += " AND ISNULL(IsInvoiced,'False')='False' ";
			}
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "Purchase_Receipt", str);
			return dataSet;
		}

		public DataSet GetGRNList(string sysDocID, bool includeInvoiced)
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT PR.SysDocID [Doc ID], PR.VoucherID [Number], TransactionDate AS [Date]    \r\n                            FROM Purchase_Receipt PR \r\n                            WHERE ISNULL(IsVoid,'False')='False'";
			if (!includeInvoiced)
			{
				str += " AND ISNULL(IsInvoiced,'False')='False' ";
			}
			if (sysDocID != "")
			{
				str = str + " AND SysDocID='" + sysDocID + "'";
			}
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "Purchase_Receipt", str);
			return dataSet;
		}

		public DataSet GetPurchaseClaimGRNList(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT PR.SysDocID [Doc ID], PR.VoucherID [Number],PR.ContainerNumber [Container No], TransactionDate AS [Date]    \r\n                            FROM Purchase_Receipt PR \r\n                            WHERE ISNULL(IsVoid,'False')='False' ";
			if (sysDocID != "")
			{
				str = str + " AND SysDocID='" + sysDocID + "'";
			}
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "Purchase_Receipt", str);
			return dataSet;
		}

		public DataSet GetGRNListToQC(string sysDocID, DateTime fromDate, DateTime toDate, bool includeLocal)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT PR.SysDocID [Doc ID], PR.VoucherID [Number], TransactionDate AS [Date],PR.VendorID ,V.VendorID + ' - ' + VendorName AS Vendor,\r\n                            ContainerNumber AS Container#\r\n                            FROM Purchase_Receipt PR INNER JOIN Vendor V ON V.VendorID = PR.VendorID\r\n\t\t\t\t\t\t\tWHERE NOT EXISTS (SELECT * FROM Quality_Task QT WHERE QT.GRNSysDocID = PR.SysDocID AND QT.GRNVoucherID = PR.VoucherID)\r\n                            AND ISNULL(IsVoid,'False')='False'  AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (sysDocID != "")
			{
				str = str + "  AND  SysDocID='" + sysDocID + "' ";
			}
			if (!includeLocal)
			{
				str += " AND ISNULL(IsImport,'False')='True' ";
			}
			str += " ORDER BY TransactionDate ";
			FillDataSet(dataSet, "Purchase_Receipt", str);
			return dataSet;
		}

		public DataSet GetItemMovementGRNReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string sysDocID, string voucherID, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			CommonLib.ToSqlDateTimeString(from);
			CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string text = "SELECT  PL.LotNumber,PL.ItemCode,Product.Description,SD.DocName,IT.VoucherID, PL.LocationID,LotQty AS [QTY In] ,0 AS [QTY Out],IT.TransactionDate AS [Date]                             \r\n                            FROM Product_Lot PL LEFT JOIN System_Document SD on PL.DocID=SD.SysDocID\r\n                            LEFT JOIN Inventory_Transactions IT ON IT.SysDocID=PL.DocID AND IT.VoucherID=PL.ReceiptNumber AND IT.ProductID=PL.ItemCode  \r\n                                    LEFT JOIN Product ON Product.ProductID=IT.ProductID                                      \r\n                            WHERE PL.LotNumber IN (SELECT LotNumber  from Product_Lot where DocID='" + sysDocID + "' AND ReceiptNumber='" + voucherID + "') \r\n                                    OR SourceLotNumber IN\r\n                                (SELECT LotNumber  from Product_Lot where DocID='" + sysDocID + "' AND ReceiptNumber='" + voucherID + "')";
			if (fromItem != "")
			{
				text = text + " AND PL.ItemCode >= '" + fromItem + "' ";
			}
			if (toItem != "")
			{
				text = text + " AND PL.ItemCode <= '" + toItem + "' ";
			}
			if (fromClass != "")
			{
				text = text + " AND PL.ItemCode IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
			}
			if (toClass != "")
			{
				text = text + " AND PL.ItemCode IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
			}
			if (fromCategory != "")
			{
				text = text + " AND PL.ItemCode IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
			}
			if (toCategory != "")
			{
				text = text + " AND PL.ItemCode IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "') ";
			}
			if (fromManufacturer != "")
			{
				text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (fromBrand != "")
			{
				text = text + " AND PL.ItemCode IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
			}
			text = text + " UNION\r\n\r\n                            SELECT  PLS.LotNo,PLS.ItemCode,Product.Description,SD.DocName,IT.VoucherID,PLS.LocationID,0 AS [QTY In], SoldQty AS [QTY Out],IT.TransactionDate AS [Date]                                    \r\n                                FROM Product_Lot_Sales PLS LEFT JOIN System_Document SD on PLS.DocID=SD.SysDocID\r\n                                LEFT JOIN Inventory_Transactions IT ON IT.SysDocID=PLS.DocID AND IT.VoucherID=PLS.InvoiceNumber AND IT.ProductID=PLS.ItemCode  \r\n                                    LEFT JOIN Product ON Product.ProductID=IT.ProductID                                   \r\n                            WHERE PLS.LotNo  IN ( SELECT LotNumber  FROM Product_Lot  where LotNumber IN \r\n                                (SELECT LotNumber  FROM Product_Lot WHERE DocID='" + sysDocID + "' AND ReceiptNumber='" + voucherID + "') \r\n                                OR SourceLotNumber IN (SELECT LotNumber  FROM Product_Lot where DocID='" + sysDocID + "' AND ReceiptNumber='" + voucherID + "' )) ";
			if (fromItem != "")
			{
				text = text + " AND PLS.ProductID >= '" + fromItem + "' ";
			}
			if (toItem != "")
			{
				text = text + " AND PLS.ProductID <= '" + toItem + "' ";
			}
			if (fromClass != "")
			{
				text = text + " AND PLS.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
			}
			if (toClass != "")
			{
				text = text + " AND PLS.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
			}
			if (fromCategory != "")
			{
				text = text + " AND PLS.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
			}
			if (toCategory != "")
			{
				text = text + " AND PLS.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
			}
			if (fromManufacturer != "")
			{
				text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
			}
			if (fromStyle != "")
			{
				text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
			}
			if (fromOrigin != "")
			{
				text = text + " AND IT.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
			}
			if (fromBrand != "")
			{
				text = text + " AND PLS.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
			}
			text += "ORDER BY ItemCode,IT.TransactionDate";
			FillDataSet(dataSet, "Purchase_Receipt", text);
			return dataSet;
		}

		public DataSet GetBarCodeReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string sysDocID, string voucherID, string tableName, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			CommonLib.ToSqlDateTimeString(from);
			CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string text = "";
			if (voucherID != "")
			{
				text = "SELECT TD.SysDocID, TD.VoucherID,TransactionDate,TD.ProductID,P.Description,p.UnitID,u.UnitName,p.CategoryID,pc.CategoryName,p.BrandID,pb.BrandName,p.ManufacturerID,p.AverageCost,ISNULL(TD.UnitQuantity, TD.Quantity) AS QTY, P.Attribute,\r\n                            Case p.ItemType When 1 then 'Inventory' when 2 then 'Non-Inventory' when 3 then 'Service' when 4 then 'Discount' when 5 then 'Consignment Item' when 6 \r\n                            then 'Inventory 3PL' when 7 then 'Assembly' when 8 then 'Project Fee' END AS [Item  Type], ReorderLevel, MinPrice, UnitPrice1,UnitPrice2,UnitPrice3,StandardCost,LastCost, VendorRef,UPC,StandardCost,AverageCost,LastCost          \r\n                            from " + tableName + " T  INNER JOIN " + tableName + "_Detail TD ON T.SysDocID=TD.SysDocID AND T.VoucherID=TD.VoucherID  INNER JOIN Product p ON TD.ProductID=p.ProductID\r\n                            LEFT JOIN Unit u ON u.UnitID = p.UnitID\r\n                            LEFT JOIN Product_Category pc ON pc.CategoryID = p.CategoryID\r\n                            LEFT JOIN Product_Brand pb ON pb.BrandID = p.BrandID \r\n                            where TD.SysDocID = '" + sysDocID + "' AND TD.VoucherID = '" + voucherID + "'";
				if (fromItem != "")
				{
					text = text + " AND TD.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND TD.ProductID <= '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					text = text + " AND TD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromItemClass + "')";
				}
				if (toItemClass != "")
				{
					text = text + " AND TD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toItemClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND TD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND TD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND TD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND TD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND TD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text = text + " AND TD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text += " ORDER BY TD.ProductID,TransactionDate";
			}
			else
			{
				text = "SELECT p.ProductID,p.Description,p.UnitID,u.UnitName,p.CategoryID,pc.CategoryName,p.BrandID,pb.BrandName,p.ManufacturerID,p.AverageCost,1 AS QTY, P.Attribute,\r\n                            Case ItemType When 1 then 'Inventory' when 2 then 'Non-Inventory' when 3 then 'Service' when 4 then 'Discount' when 5 then 'Consignment Item' when 6 \r\n                            then 'Inventory 3PL' when 7 then 'Assembly' when 8 then 'Project Fee' END AS [Item  Type], ReorderLevel, MinPrice, UnitPrice1,UnitPrice2,UnitPrice3,StandardCost,LastCost, VendorRef,UPC,StandardCost,AverageCost,LastCost                         \r\n                            FROM Product p \r\n                            LEFT JOIN Unit u ON u.UnitID = p.UnitID\r\n                            LEFT JOIN Product_Category pc ON pc.CategoryID = p.CategoryID\r\n                            LEFT JOIN Product_Brand pb ON pb.BrandID = p.BrandID    \r\n                            where 1=1";
				if (fromItem != "")
				{
					text = text + " AND p.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND p.ProductID <= '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					text = text + " AND p.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromItemClass + "')";
				}
				if (toItemClass != "")
				{
					text = text + " AND p.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toItemClass + "')";
				}
				if (fromCategory != "")
				{
					text = text + " AND p.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND p.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND p.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND p.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND p.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text = text + " AND p.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text += " ORDER BY p.ProductID";
			}
			FillDataSet(dataSet, "Products", text);
			return dataSet;
		}

		public DataSet GetBarCodeReport(DataSet data)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			string text2 = "";
			foreach (DataRow row in data.Tables[0].Rows)
			{
				text2 = text2 + "'" + row["Item Code"].ToString() + "'";
				if (data.Tables[0].Rows.IndexOf(row) < data.Tables[0].Rows.Count - 1)
				{
					text2 += ",";
				}
			}
			text = "SELECT p.ProductID,p.Description,p.UnitID,u.UnitName,p.CategoryID,pc.CategoryName,p.BrandID,pb.BrandName,p.ManufacturerID,pm.ManufacturerName,p.AverageCost,C.CountryName, P.Attribute,\r\n                            Case ItemType When 1 then 'Inventory' when 2 then 'Non-Inventory' when 3 then 'Service' when 4 then 'Discount' when 5 then 'Consignment Item' when 6 \r\n                            then 'Inventory 3PL' when 7 then 'Assembly' when 8 then 'Project Fee' END AS [Item  Type], ReorderLevel, MinPrice, UnitPrice1,UnitPrice2,UnitPrice3,StandardCost, VendorRef,UPC,StandardCost,AverageCost,\r\n                             ISNULL((select Top 1 CASE WHEN FactorType='D' THEN  PID.UnitPrice/PID.UnitFactor\r\n                            WHEN FactorType='M'  THEN  PID.UnitPrice*PID.UnitFactor\r\n                            ELSE PID.UnitPrice END AS UnitPrice from Purchase_Invoice_Detail PID \r\n                            INNER JOIN Purchase_Invoice PI ON PI.SysDocID=PID.SysDocID AND PI.VoucherID=PID.VoucherID INNER JOIN Product P1 ON PID.ProductID=P1.ProductID  where p1.ProductID=P.ProductID order by TransactionDate desc),LastCost)AS [LAST COST]\r\n                            FROM Product p \r\n                            LEFT JOIN Unit u ON u.UnitID = p.UnitID\r\n                            LEFT JOIN Product_Category pc ON pc.CategoryID = p.CategoryID\r\n                            LEFT JOIN Product_Brand pb ON pb.BrandID = p.BrandID \r\n                            LEFT JOIN Product_Manufacturer pm ON pm.ManufacturerID=p.ManufacturerID\r\n                            LEFT JOIN Country C ON C.CountryID=p.Origin where 1=1 AND ProductID IN (" + text2 + ")";
			text += " ORDER BY p.ProductID";
			FillDataSet(dataSet, "Products", text);
			foreach (DataRow row2 in data.Tables[0].Rows)
			{
				if (!dataSet.Tables[0].Columns.Contains("PrintQty"))
				{
					dataSet.Tables[0].Columns.Add("PrintQty");
				}
				foreach (DataRow row3 in dataSet.Tables[0].Rows)
				{
					if (row3["ProductID"].ToString() == row2["Item Code"].ToString())
					{
						row3["PrintQty"] = row2["Quantity"].ToString();
					}
				}
			}
			return dataSet;
		}

		public DataSet GetSalesByGRNSummaryReport_last(DateTime from, DateTime to, string fromCategory, string toCategory, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string sysDocID, string voucherID, string ContainerNo)
		{
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT CI.SysDocID,CI.VoucherID, CI.SysDocID + '#' + CI.VoucherID AS GRNNumber, VendorName,VA.Address1,CI.ContainerNumber,CI.TransactionDate, CI.PONumber, CI.Reference \r\n                                FROM  Purchase_Receipt CI LEFT JOIN Vendor V ON CI.VendorID=V.VendorID LEFT JOIN Vendor_Address VA ON V.VendorID=VA.VendorID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' ";
				FillDataSet(dataSet, "Vendor", textCommand);
				textCommand = "    SELECT DocID,ReceiptNumber,LotNumber, ItemCode, Description, LotQty,SUM(ISNULL([Quantity Delivered],0))  AS [Quantity Delivered] ,\r\n                             SUM(ISNULL(FOC,0)) AS FOC,  SUM(ISNULL([Quantity Invoiced],0)) AS [Quantity Invoiced],\r\n                            (SELECT SUM(AssetValue) FROM Inventory_Transactions WHERE SysDocID=DocID AND VoucherID=ReceiptNumber AND ProductID=ItemCode) AS COGS,\r\n                            (SUM(ISNULL(Amount,0)) -(SELECT SUM(AssetValue) FROM Inventory_Transactions WHERE SysDocID=DocID AND VoucherID=ReceiptNumber AND ProductID=ItemCode)) AS Profit, \r\n                             SUM(ISNULL(Amount,0)) AS Amount \r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tFROM (\r\n                            SELECT PL.DocID,PL.ReceiptNumber,PL.LotNumber,  CON.ProductID AS ItemCode,CON.Description,PL.LotQty,SUM(ISNULL(CON.DeliveredQty,0)) AS [Quantity Delivered],\r\n                            SUM(NonSaleQty) +  SUM(ISNULL(CON.FOCQuantity,0)) AS FOC,\r\n\t\t\t\t\t\t\tSUM(ISNULL(CON.InvoiceQty,0) - ISNULL(CON.FOCQuantity,0)) AS [Quantity Invoiced]\r\n                            , SUM(ISNULL(ISNULL(Round((CON.InvoiceQty - ISNULL(FOCQuantity,0)) * Con.UnitPrice," + currencyDecimalPoints + "),0),0)) AS Amount \r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tFROM (\r\n                            SELECT PLD.*,P.Description, IT.TransactionDate,CASE WHEN IT.PayeeType = 'C' THEN IT.PayeeID ELSE NULL END AS CustomerID ,CUS.CustomerName, ISNULL(INV.VoucherID,PLD.VoucherID) AS InvoiceVoucherID, \r\n                            CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE  ISNULL(INV.UnitPrice,CashInv.UnitPrice) END AS UnitPrice,\r\n\t\t\t\t\t\t\t CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(SoldQty,0)  - (ISNULL(DRD.Quantity,0) + ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0)) END AS InvoiceQty,INV.TransactionDate AS InvoiceDate,\r\n                             ISNULL(SoldQty,0) - (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))    AS DeliveredQty \r\n\r\n                            FROM (\r\n                            SELECT SD.SysDocType,SD.DocName, PLS.DocID AS SysDocID, InvoiceNumber AS VoucherID,RowIndex,ItemCode AS ProductID,PLS.LocationID,LotNo AS LotNumber,SoldQty,ISNULL(FOCQuantity,0) AS FOCQuantity, CASE WHEN SD.SysDocType IN (87) THEN SoldQty ELSE 0 END  AS NonSaleQty\r\n                            FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID \r\n                            WHERE LotNo IN \r\n                            (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "') OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n                            AND SysDocType NOT IN (19,20,21,40)\r\n\r\n                            UNION\r\n\r\n                            SELECT SD.SysDocType,SD.DocName,PLS.SysDocID, VoucherID,RowIndex, ProductID,PLS.LocationID,SourceLotNumber AS  LotNumber,-1 * LotQty AS SoldQty,0 AS FOCQuantity, 0 AS NonSaleQty\r\n                            FROM Product_Lot_Receiving_Detail PLS INNER JOIN System_Document SD ON PLS.SysDocID = SD.SysDocID WHERE SourceLotNumber IN \r\n                            (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "') OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n                            AND SysDocType NOT IN (19,20,21,40))  AS PLD\r\n                            LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = PLD.SysDocID AND IT.VoucherID = PLD.VoucherID AND IT.RowIndex = PLD.RowIndex\r\n                            LEFT OUTER JOIN  (SELECT SID2.Quantity,SID2.OrderSysDocID,SID2.VoucherID,SID2.OrderVoucherID,SID2.OrderRowIndex,SID2.UnitPrice,SI2.TransactionDate\r\n\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID2 INNER JOIN Sales_INVOICE SI2 ON SID2.SysDocID = SI2.SysDocID AND SID2.VoucherID = SI2.VoucherID AND ISNULL(SI2.IsVoid,'False') = 'False' ) AS INV ON INV.OrderSysDocID = PLD.SysDocID\r\n\t\t\t\t\t\t\t AND INV.OrderVoucherID = PLD.VoucherID AND INV.OrderRowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t\t\t\t --Cash Sales\r\n\t\t\t\t\t\t\t LEFT OUTER JOIN (SELECT SID3.Quantity,SID3.SysDocID,SID3.VoucherID,SID3.RowIndex, SID3.UnitPrice,SI3.TransactionDate\r\n\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID3 INNER JOIN Sales_INVOICE SI3 ON SID3.SysDocID = SI3.SysDocID AND SID3.VoucherID = SI3.VoucherID AND ISNULL(SI3.IsVoid,'False') = 'False' ) AS CashInv\r\n\t\t\t\t\t\t\t ON CashInv.SysDocID = PLD.SysDocID  AND CashInv.VoucherID = PLD.VoucherID AND CashInv.RowIndex = PLD.RowIndex\r\n\r\n                            LEFT OUTER JOIN Sales_Return_Detail  SRD ON SRD.SourceSysDocID = PLD.SysDocID AND SRD.SourceVoucherID = PLD.VoucherID AND SRD.SourceRowIndex = PLD.RowIndex\r\n                           \r\n                            LEFT OUTER JOIN (SELECT SI2.OrderSysDocID,SI2.OrderVoucherID,SI2.OrderRowIndex, SRD2.Quantity FROM Sales_Return_Detail SRD2 INNER JOIN Sales_Invoice_Detail SI2 ON SRD2.SourceSysDocID=SI2.SysDocID\r\n\t\t\t\t\t\t\tAND SRD2.SourceVoucherID = SI2.voucherID AND SRD2.SourceRowIndex = SI2.rowIndex) CrSR ON CRSR.OrderSysDocID = PLD.SysDocID AND CRSR.OrderVoucherID = PLD.VoucherID AND CRSR.OrderRowIndex = PLD.RowIndex\r\n\r\n\r\n                            LEFT OUTER JOIN Product P ON P.ProductID = PLD.ProductID\r\n                            LEFT OUTER JOIN Delivery_Return DR ON DR.DNoteSysDocID = PLD.SysDocID AND DR.DNoteVoucherID = PLD.VoucherID  AND ISNULL(DR.IsVoid,'False') = 'False'\r\n\t\t\t\t\t        LEFT OUTER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID AND DRD.DNRowIndex = PLD.RowIndex\r\n                            LEFT OUTER JOIN Customer CUS ON CUS.CUstomerID = IT.PayeeID AND IT.PayeeType = 'C') CON\r\n                            INNER JOIN Product_Lot PL ON PL.DocID = '" + sysDocID + "' AND PL.ReceiptNumber = '" + voucherID + "' AND PL.ItemCode = CON.ProductID\r\n                            GROUP BY CON.ProductID,Con.Description, PL.DocID,PL.ReceiptNumber,PL.LotNumber,PL.LotQty   \r\n\r\n\r\n                                UNION\r\n\r\n                                SELECT CID.SysDocID AS DocID, CID.VoucherID AS ReceiptNumber,PL.LotNumber, CID.ProductID AS ItemCode,P.Description,PL.LotQty AS LotQty, 0 AS [Quantity Delivered], 0 AS FOC,0  AS [Quantity Invoiced], 0 AS Amount\r\n                                FROM Purchase_Receipt_Detail CID INNER JOIN Purchase_Receipt CI ON CID.SysDocID= CI.SysDocID AnD CID.VoucherID = CI.VoucherID\r\n                                INNER JOIN Product_LOT PL ON PL.DocID = CID.SysDocID AND PL.ReceiptNumber = CID.VoucherID AND PL.RowIndex = CID.RowIndex\r\n                                INNER JOIN Product P ON P.ProductID = CID.ProductID\r\n                                WHERE ISNULL(CI.IsVoid,'False')= 'False' AND CI.VoucherID = '" + voucherID + "' AND CI.SysDocID = '" + sysDocID + "' \r\n                                AND PL.LOTNumber  NOT  IN (SELECT LotNumber FROM Product_Lot_Issue_Detail PLI INNER JOIN System_Document SD ON SD.SysDocID = PLI.SysDocID WHERE SysDocType NOT IN (19,20,21,40))\r\n                                AND PL.LOTNumber  NOT  IN (SELECT SourceLotNumber FROM Product_Lot_Issue_Detail PLI INNER JOIN System_Document SD ON SD.SysDocID = PLI.SysDocID WHERE SOURCELotNumber IS NOT NULL AND SysDocType NOT IN (19,20,21,40))\r\n                                  ) AS X GROUP BY DocID,ReceiptNumber,LotNumber, ItemCode, Description, LotQty  ORDER BY ItemCode\r\n                                ";
				FillDataSet(dataSet, "Purchase_Receipt", textCommand);
				textCommand = "SELECT '" + sysDocID + "' AS DocID, '" + voucherID + "' AS ReceiptNumber, CON.CustomerID,ISNULL(Con.CustomerName,'Non Sale Issue') AS CustomerName,CON.Description,COn.DocName,\r\n                        CASE WHEN SysDocType IN (87) THEN SUM(CON.DeliveredQty) ELSE 0 END  + SUM(CON.FOCQuantity) AS FOC,CON.InvoiceDate,SUM(CON.InvoiceQty - FOCQuantity) AS [Quantity Invoiced],\r\n                        CON.InvoiceVoucherID,COn.LocationID, CON.ProductID AS ItemCode,SUM(CON.DeliveredQty) AS [Quantity Delivered],CON.SysDocID,CON.SysDocType,CON.TransactionDate,CON.UnitPrice AS [Unit Price],\r\n                        CON.VoucherID, ISNULL(Round(SUM(CON.InvoiceQty - FOCQuantity) * Con.UnitPrice,2),0) AS Amount FROM \r\n\r\n\r\n                        (SELECT PLD.*,P.Description, IT.TransactionDate,CASE WHEN IT.PayeeType = 'C' THEN IT.PayeeID ELSE NULL END AS CustomerID ,\r\n                        CUS.CustomerName, ISNULL(INV.VoucherID,PLD.VoucherID) AS InvoiceVoucherID, \r\n                         CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(INV.UnitPrice,CashInv.UnitPrice)  END AS UnitPrice,ISNULL(DRD.Quantity,0) + ISNULL(SRD.Quantity,0) AS QuantityReturned,\r\n                         CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(SoldQty,0)- (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))  END AS InvoiceQty ,ISNULL(INV.TransactionDate,CashInv.TransactionDate) AS InvoiceDate,\r\n                         ISNULL(SoldQty,0) - (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))  AS DeliveredQty\r\n                          FROM ( \r\n  \r\n  \r\n                          SELECT SD.SysDocType,SD.DocName, PLS.DocID AS SysDocID, InvoiceNumber AS VoucherID,RowIndex,ItemCode AS ProductID,PLS.LocationID,LotNo AS LotNumber,SoldQty,ISNULL(FOCQuantity,0) AS FOCQuantity\r\n                          FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID WHERE LotNo IN \r\n                          (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "') OR SourceLotNumber IN \r\n                          (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n                           AND SysDocType NOT IN (19,20,21,40)\r\n                         UNION\r\n\t\t                   SELECT SD.SysDocType,SD.DocName,PLS.SysDocID, VoucherID,RowIndex, ProductID,PLS.LocationID,SourceLotNumber AS  LotNumber,-1 * LotQty AS SoldQty,0 AS FOCQuantity\r\n\t\t                    FROM Product_Lot_Receiving_Detail PLS INNER JOIN System_Document SD ON PLS.SysDocID = SD.SysDocID WHERE SourceLotNumber IN \r\n\t\t\t                  (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')\r\n\t\t\t                   OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n\t\t\t                   AND SysDocType NOT IN (19,20,21,40))  AS PLD\r\n\r\n\t\t\t                    LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = PLD.SysDocID AND IT.VoucherID = PLD.VoucherID AND IT.RowIndex = PLD.RowIndex\r\n\t\t\t\t                 LEFT OUTER JOIN  (SELECT SID2.Quantity,SID2.OrderSysDocID,SID2.VoucherID,SID2.OrderVoucherID,SID2.OrderRowIndex,SID2.UnitPrice,SI2.TransactionDate\r\n\t\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID2 INNER JOIN Sales_INVOICE SI2 ON SID2.SysDocID = SI2.SysDocID AND SID2.VoucherID = SI2.VoucherID AND ISNULL(SI2.IsVoid,'False') = 'False' ) AS INV ON INV.OrderSysDocID = PLD.SysDocID\r\n\t\t\t\t\t\t\t\t AND INV.OrderVoucherID = PLD.VoucherID AND INV.OrderRowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t\t\t\t\t --Cash Sales\r\n\t\t\t\t\t\t\t\t LEFT OUTER JOIN (SELECT SID3.Quantity,SID3.SysDocID,SID3.VoucherID,SID3.RowIndex, SID3.UnitPrice,SI3.TransactionDate\r\n\t\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID3 INNER JOIN Sales_INVOICE SI3 ON SID3.SysDocID = SI3.SysDocID AND SID3.VoucherID = SI3.VoucherID AND ISNULL(SI3.IsVoid,'False') = 'False' ) AS CashInv\r\n\t\t\t\t\t\t\t\t ON CashInv.SysDocID = PLD.SysDocID  AND CashInv.VoucherID = PLD.VoucherID AND CashInv.RowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t                    LEFT OUTER JOIN Delivery_Return DR ON DR.DNoteSysDocID = PLD.SysDocID AND DR.DNoteVoucherID = PLD.VoucherID  AND ISNULL(DR.IsVoid,'False') = 'False'\r\n\t\t\t\t\t                 LEFT OUTER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID AND DRD.DNRowIndex = PLD.RowIndex\r\n\t\t\t\t\t                  LEFT OUTER JOIN Sales_Return_Detail  SRD ON SRD.SourceSysDocID = PLD.SysDocID AND SRD.SourceVoucherID = PLD.VoucherID AND SRD.SourceRowIndex = PLD.RowIndex  \r\n\t\t\t\t\t                 LEFT OUTER JOIN (SELECT SI2.OrderSysDocID,SI2.OrderVoucherID,SI2.OrderRowIndex, SRD2.Quantity FROM Sales_Return_Detail SRD2 INNER JOIN Sales_Invoice_Detail SI2 ON SRD2.SourceSysDocID=SI2.SysDocID\r\n\t\t\t\t\t\t\t\t\t\t\tAND SRD2.SourceVoucherID = SI2.voucherID AND SRD2.SourceRowIndex = SI2.rowIndex) CrSR ON CRSR.OrderSysDocID = PLD.SysDocID AND CRSR.OrderVoucherID = PLD.VoucherID AND CRSR.OrderRowIndex = PLD.RowIndex\r\n\r\n                                            LEFT OUTER JOIN Product P ON P.ProductID = PLD.ProductID\r\n\t\t\t\t\t                      LEFT OUTER JOIN Customer CUS ON CUS.CUstomerID = IT.PayeeID AND IT.PayeeType = 'C') CON\r\n\t\t\t\t\t\t                   WHERE DeliveredQty > 0 \r\n\t\t\t\t\t\t   GROUP BY   CON.CustomerID,Con.CustomerName,CON.Description,COn.DocName, CON.InvoiceDate, CON.InvoiceVoucherID,COn.LocationID, CON.ProductID ,CON.SysDocID,CON.SysDocType,CON.TransactionDate,CON.UnitPrice  , CON.VoucherID \r\n                                 ORDER BY ItemCode,TransactionDate  ";
				FillDataSet(dataSet, "Purchase_Receipt_Detail", textCommand);
				dataSet.Relations.Add("VendorGRN", new DataColumn[2]
				{
					dataSet.Tables["Vendor"].Columns["SysDocID"],
					dataSet.Tables["Vendor"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Purchase_Receipt"].Columns["DocID"],
					dataSet.Tables["Purchase_Receipt"].Columns["ReceiptNumber"]
				}, createConstraints: false);
				dataSet.Relations.Add("VendorGRNDetail", new DataColumn[2]
				{
					dataSet.Tables["Vendor"].Columns["SysDocID"],
					dataSet.Tables["Vendor"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Purchase_Receipt_Detail"].Columns["DocID"],
					dataSet.Tables["Purchase_Receipt_Detail"].Columns["ReceiptNumber"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesByGRNSummaryReport(DateTime from, DateTime to, string fromCategory, string toCategory, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string sysDocID, string voucherID, string ContainerNo, string vendorIDs)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataSet dataSet = new DataSet();
				string str = "SELECT DISTINCT CI.SysDocID,CI.VoucherID,CI.VendorID, CI.SysDocID + '#' + CI.VoucherID AS GRNNumber, VendorName,VA.Address1,CI.ContainerNumber,CI.TransactionDate, CI.PONumber, CI.Reference \r\n                                FROM  Purchase_Receipt CI \r\n\t                            LEFT JOIN  Purchase_Receipt_Detail CID ON CI.SysDocID=CID.SysDocID AND CI.VoucherID=CID.VoucherID\r\n                                LEFT JOIN Vendor V ON CI.VendorID=V.VendorID LEFT JOIN Vendor_Address VA ON V.VendorID=VA.VendorID\r\n                                WHERE   TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (voucherID != "")
				{
					str = str + " AND CI.SysDocID = '" + sysDocID + "'";
				}
				if (voucherID != "")
				{
					str = str + " AND CI.VoucherID = '" + voucherID + "' ";
				}
				if (fromCategory != "")
				{
					str = str + " AND CID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					str = str + " AND CID.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "') ";
				}
				if (vendorIDs != "")
				{
					str = str + " AND CI.VendorID IN(" + vendorIDs + ")";
				}
				if (fromVendor != "")
				{
					str = str + " AND CI.VendorID >= '" + fromVendor + "'";
				}
				if (toVendor != "")
				{
					str = str + " AND CI.VendorID <= '" + toVendor + "'";
				}
				if (fromClass != "")
				{
					str = str + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID >= '" + fromClass + "') ";
				}
				if (toClass != "")
				{
					str = str + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID <= '" + toClass + "') ";
				}
				if (fromGroup != "")
				{
					str = str + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID >= '" + fromGroup + "') ";
				}
				if (toGroup != "")
				{
					str = str + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID <= '" + toGroup + "') ";
				}
				str += " ORDER BY VendorName ";
				FillDataSet(dataSet, "Vendor", str);
				if (dataSet.Tables["Vendor"].Rows.Count <= 0)
				{
					return dataSet;
				}
				string commaSeperatedIDs = GetCommaSeperatedIDs(dataSet, "Vendor", "VoucherID");
				string commaSeperatedIDs2 = GetCommaSeperatedIDs(dataSet, "Vendor", "SysDocID");
				voucherID = commaSeperatedIDs;
				sysDocID = commaSeperatedIDs2;
				str = "    SELECT DocID,ReceiptNumber,LotNumber, ItemCode, Description, LotQty,SUM(ISNULL([Quantity Delivered],0))  AS [Quantity Delivered] ,\r\n                             SUM(ISNULL(FOC,0)) AS FOC,  SUM(ISNULL([Quantity Invoiced],0)) AS [Quantity Invoiced],\r\n                            (SELECT SUM(AssetValue) FROM Inventory_Transactions WHERE SysDocID=DocID AND VoucherID=ReceiptNumber AND ProductID=ItemCode) AS COGS,\r\n                            (SUM(ISNULL(Amount,0)) -(SELECT SUM(AssetValue) FROM Inventory_Transactions WHERE SysDocID=DocID AND VoucherID=ReceiptNumber AND ProductID=ItemCode)) AS Profit, \r\n                             SUM(ISNULL(Amount,0)) AS Amount \r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tFROM (\r\n                            SELECT PL.DocID,PL.ReceiptNumber,PL.LotNumber,  CON.ProductID AS ItemCode,CON.Description,PL.LotQty,SUM(ISNULL(CON.DeliveredQty,0)) AS [Quantity Delivered],\r\n                            SUM(NonSaleQty) +  SUM(ISNULL(CON.FOCQuantity,0)) AS FOC,\r\n\t\t\t\t\t\t\tSUM(ISNULL(CON.InvoiceQty,0) - ISNULL(CON.FOCQuantity,0)) AS [Quantity Invoiced]\r\n                            , SUM(ISNULL(ISNULL(Round((CON.InvoiceQty - ISNULL(FOCQuantity,0)) * Con.UnitPrice," + currencyDecimalPoints + "),0),0)) AS Amount \r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tFROM (\r\n                            SELECT PLD.*,P.Description, IT.TransactionDate,CASE WHEN IT.PayeeType = 'C' THEN IT.PayeeID ELSE NULL END AS CustomerID ,CUS.CustomerName, ISNULL(INV.VoucherID,PLD.VoucherID) AS InvoiceVoucherID, \r\n                            CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE  ISNULL(INV.UnitPrice,CashInv.UnitPrice) END AS UnitPrice,\r\n\t\t\t\t\t\t\t CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(SoldQty,0)  - (ISNULL(DRD.Quantity,0) + ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0)) END AS InvoiceQty,INV.TransactionDate AS InvoiceDate,\r\n                             ISNULL(SoldQty,0) - (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))    AS DeliveredQty \r\n\r\n                            FROM (\r\n                            SELECT SD.SysDocType,SD.DocName, PLS.DocID AS SysDocID, InvoiceNumber AS VoucherID,RowIndex,ItemCode AS ProductID,PLS.LocationID,LotNo AS LotNumber,SoldQty,ISNULL(FOCQuantity,0) AS FOCQuantity, CASE WHEN SD.SysDocType IN (87) THEN SoldQty ELSE 0 END  AS NonSaleQty\r\n                            FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID \r\n                            WHERE LotNo IN \r\n                            (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber IN (" + voucherID + ") AND DocID IN (" + sysDocID + ")) OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber IN (" + voucherID + ") AND DocID IN (" + sysDocID + "))))\r\n                            AND SysDocType NOT IN (19,20,21,40,48)\r\n\r\n                            UNION\r\n\r\n                            SELECT SD.SysDocType,SD.DocName,PLS.SysDocID, VoucherID,RowIndex, ProductID,PLS.LocationID,SourceLotNumber AS  LotNumber,-1 * LotQty AS SoldQty,0 AS FOCQuantity, 0 AS NonSaleQty\r\n                            FROM Product_Lot_Receiving_Detail PLS INNER JOIN System_Document SD ON PLS.SysDocID = SD.SysDocID WHERE SourceLotNumber IN \r\n                            (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber IN (" + voucherID + ") AND DocID IN (" + sysDocID + ")) OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber IN (" + voucherID + ") AND DocID IN (" + sysDocID + "))))\r\n                            AND SysDocType NOT IN (19,20,21,40,48))  AS PLD\r\n                            LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = PLD.SysDocID AND IT.VoucherID = PLD.VoucherID AND IT.RowIndex = PLD.RowIndex\r\n                            LEFT OUTER JOIN  (SELECT SID2.Quantity,SID2.OrderSysDocID,SID2.VoucherID,SID2.OrderVoucherID,SID2.OrderRowIndex,SID2.UnitPrice,SI2.TransactionDate\r\n\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID2 INNER JOIN Sales_INVOICE SI2 ON SID2.SysDocID = SI2.SysDocID AND SID2.VoucherID = SI2.VoucherID AND ISNULL(SI2.IsVoid,'False') = 'False' ) AS INV ON INV.OrderSysDocID = PLD.SysDocID\r\n\t\t\t\t\t\t\t AND INV.OrderVoucherID = PLD.VoucherID AND INV.OrderRowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t\t\t\t --Cash Sales\r\n\t\t\t\t\t\t\t LEFT OUTER JOIN (SELECT SID3.Quantity,SID3.SysDocID,SID3.VoucherID,SID3.RowIndex, SID3.UnitPrice,SI3.TransactionDate\r\n\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID3 INNER JOIN Sales_INVOICE SI3 ON SID3.SysDocID = SI3.SysDocID AND SID3.VoucherID = SI3.VoucherID AND ISNULL(SI3.IsVoid,'False') = 'False' ) AS CashInv\r\n\t\t\t\t\t\t\t ON CashInv.SysDocID = PLD.SysDocID  AND CashInv.VoucherID = PLD.VoucherID AND CashInv.RowIndex = PLD.RowIndex\r\n\r\n                            LEFT OUTER JOIN Sales_Return_Detail  SRD ON SRD.SourceSysDocID = PLD.SysDocID AND SRD.SourceVoucherID = PLD.VoucherID AND SRD.SourceRowIndex = PLD.RowIndex\r\n                           \r\n                            LEFT OUTER JOIN (SELECT SI2.OrderSysDocID,SI2.OrderVoucherID,SI2.OrderRowIndex, SRD2.Quantity FROM Sales_Return_Detail SRD2 INNER JOIN Sales_Invoice_Detail SI2 ON SRD2.SourceSysDocID=SI2.SysDocID\r\n\t\t\t\t\t\t\tAND SRD2.SourceVoucherID = SI2.voucherID AND SRD2.SourceRowIndex = SI2.rowIndex) CrSR ON CRSR.OrderSysDocID = PLD.SysDocID AND CRSR.OrderVoucherID = PLD.VoucherID AND CRSR.OrderRowIndex = PLD.RowIndex\r\n\r\n\r\n                            LEFT OUTER JOIN Product P ON P.ProductID = PLD.ProductID\r\n                            LEFT OUTER JOIN Delivery_Return DR ON DR.DNoteSysDocID = PLD.SysDocID AND DR.DNoteVoucherID = PLD.VoucherID  AND ISNULL(DR.IsVoid,'False') = 'False'\r\n\t\t\t\t\t        LEFT OUTER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID AND DRD.DNRowIndex = PLD.RowIndex\r\n                            LEFT OUTER JOIN Customer CUS ON CUS.CUstomerID = IT.PayeeID AND IT.PayeeType = 'C') CON\r\n                            INNER JOIN Product_Lot PL ON PL.DocID IN (" + sysDocID + ") AND PL.ReceiptNumber IN (" + voucherID + ") AND PL.ItemCode = CON.ProductID\r\n                            GROUP BY CON.ProductID,Con.Description, PL.DocID,PL.ReceiptNumber,PL.LotNumber,PL.LotQty   \r\n\r\n\r\n                                UNION\r\n\r\n                                SELECT CID.SysDocID AS DocID, CID.VoucherID AS ReceiptNumber,PL.LotNumber, CID.ProductID AS ItemCode,P.Description,PL.LotQty AS LotQty, 0 AS [Quantity Delivered], 0 AS FOC,0  AS [Quantity Invoiced], 0 AS Amount\r\n                                FROM Purchase_Receipt_Detail CID INNER JOIN Purchase_Receipt CI ON CID.SysDocID= CI.SysDocID AnD CID.VoucherID = CI.VoucherID\r\n                                INNER JOIN Product_LOT PL ON PL.DocID = CID.SysDocID AND PL.ReceiptNumber = CID.VoucherID AND PL.RowIndex = CID.RowIndex\r\n                                INNER JOIN Product P ON P.ProductID = CID.ProductID\r\n                                WHERE ISNULL(CI.IsVoid,'False')= 'False' AND CI.VoucherID IN (" + voucherID + ") AND CI.SysDocID IN (" + sysDocID + ") \r\n                                AND PL.LOTNumber  NOT  IN (SELECT LotNumber FROM Product_Lot_Issue_Detail PLI INNER JOIN System_Document SD ON SD.SysDocID = PLI.SysDocID WHERE SysDocType NOT IN (19,20,21,40,48))\r\n                                AND PL.LOTNumber  NOT  IN (SELECT SourceLotNumber FROM Product_Lot_Issue_Detail PLI INNER JOIN System_Document SD ON SD.SysDocID = PLI.SysDocID WHERE SOURCELotNumber IS NOT NULL AND SysDocType NOT IN (19,20,21,40,48))\r\n                                  ) AS X GROUP BY DocID,ReceiptNumber,LotNumber, ItemCode, Description, LotQty  ORDER BY ItemCode\r\n                                ";
				FillDataSet(dataSet, "Purchase_Receipt", str);
				dataSet.Relations.Add("VendorGRN", new DataColumn[2]
				{
					dataSet.Tables["Vendor"].Columns["SysDocID"],
					dataSet.Tables["Vendor"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Purchase_Receipt"].Columns["DocID"],
					dataSet.Tables["Purchase_Receipt"].Columns["ReceiptNumber"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSalesByGRNDetailReport(string sysDocID, string voucherID)
		{
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT CI.SysDocID,CI.VoucherID, CI.SysDocID + '#' + CI.VoucherID AS GRNNumber, VendorName,VA.Address1,CI.ContainerNumber,CI.TransactionDate, CI.PONumber, CI.Reference \r\n                                FROM  Purchase_Receipt CI LEFT JOIN Vendor V ON CI.VendorID=V.VendorID LEFT JOIN Vendor_Address VA ON V.VendorID=VA.VendorID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' ";
				FillDataSet(dataSet, "Vendor", textCommand);
				textCommand = "(SELECT LotNumber into #tmp1 FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')\r\n                             OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n                \r\n                 SELECT DocID,ReceiptNumber,LotNumber, ItemCode, Description, LotQty,SUM(ISNULL([Quantity Delivered],0))  AS [Quantity Delivered] ,\r\n                             SUM(ISNULL(FOC,0)) AS FOC,  SUM(ISNULL([Quantity Invoiced],0)) AS [Quantity Invoiced],\r\n                            (SELECT SUM(AssetValue) FROM Inventory_Transactions WHERE SysDocID=DocID AND VoucherID=ReceiptNumber AND ProductID=ItemCode) AS COGS,\r\n                            (SUM(ISNULL(Amount,0)) -(SELECT SUM(AssetValue) FROM Inventory_Transactions WHERE SysDocID=DocID AND VoucherID=ReceiptNumber AND ProductID=ItemCode)) AS Profit, \r\n                             SUM(ISNULL(Amount,0)) AS Amount \r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tFROM (\r\n                            SELECT PL.DocID,PL.ReceiptNumber,PL.LotNumber,  CON.ProductID AS ItemCode,CON.Description,PL.LotQty,SUM(ISNULL(CON.DeliveredQty,0)) AS [Quantity Delivered],\r\n                            SUM(NonSaleQty) +  SUM(ISNULL(CON.FOCQuantity,0)) AS FOC,\r\n\t\t\t\t\t\t\tSUM(ISNULL(CON.InvoiceQty,0) - ISNULL(CON.FOCQuantity,0)) AS [Quantity Invoiced]\r\n                            , SUM(ISNULL(ISNULL(Round((CON.InvoiceQty - ISNULL(FOCQuantity,0)) * Con.UnitPrice," + currencyDecimalPoints + "),0),0)) AS Amount \r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tFROM (\r\n                            SELECT PLD.*,P.Description, IT.TransactionDate,CASE WHEN IT.PayeeType = 'C' THEN IT.PayeeID ELSE NULL END AS CustomerID ,CUS.CustomerName, ISNULL(INV.VoucherID,PLD.VoucherID) AS InvoiceVoucherID, \r\n                            CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE  ISNULL(INV.UnitPrice,CashInv.UnitPrice) END AS UnitPrice,\r\n\t\t\t\t\t\t\t CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(SoldQty,0)  - (ISNULL(DRD.Quantity,0) + ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0)) END AS InvoiceQty,INV.TransactionDate AS InvoiceDate,\r\n                             ISNULL(SoldQty,0) - (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))    AS DeliveredQty \r\n\r\n                            FROM (\r\n                            SELECT SD.SysDocType,SD.DocName, PLS.DocID AS SysDocID, InvoiceNumber AS VoucherID,RowIndex,ItemCode AS ProductID,PLS.LocationID,LotNo AS LotNumber,SoldQty,ISNULL(FOCQuantity,0) AS FOCQuantity, CASE WHEN SD.SysDocType IN (87) THEN SoldQty ELSE 0 END  AS NonSaleQty\r\n                            FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID \r\n                            WHERE LotNo IN \r\n                           (SELECT LotNumber FROM #tmp1)\r\n                            AND SysDocType NOT IN (19,20,21,40,48)\r\n\r\n                            UNION\r\n\r\n                            SELECT SD.SysDocType,SD.DocName,PLS.SysDocID, VoucherID,RowIndex, ProductID,PLS.LocationID,SourceLotNumber AS  LotNumber,-1 * LotQty AS SoldQty,0 AS FOCQuantity, 0 AS NonSaleQty\r\n                            FROM Product_Lot_Receiving_Detail PLS INNER JOIN System_Document SD ON PLS.SysDocID = SD.SysDocID WHERE SourceLotNumber IN \r\n                            (SELECT LotNumber FROM #tmp1)\r\n                            AND SysDocType NOT IN (19,20,21,40,48))  AS PLD\r\n                            LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = PLD.SysDocID AND IT.VoucherID = PLD.VoucherID AND IT.RowIndex = PLD.RowIndex\r\n                            LEFT OUTER JOIN  (SELECT SID2.Quantity,SID2.OrderSysDocID,SID2.VoucherID,SID2.OrderVoucherID,SID2.OrderRowIndex,SID2.UnitPrice,SI2.TransactionDate\r\n\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID2 INNER JOIN Sales_INVOICE SI2 ON SID2.SysDocID = SI2.SysDocID AND SID2.VoucherID = SI2.VoucherID AND ISNULL(SI2.IsVoid,'False') = 'False' ) AS INV ON INV.OrderSysDocID = PLD.SysDocID\r\n\t\t\t\t\t\t\t AND INV.OrderVoucherID = PLD.VoucherID AND INV.OrderRowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t\t\t\t --Cash Sales\r\n\t\t\t\t\t\t\t LEFT OUTER JOIN (SELECT SID3.Quantity,SID3.SysDocID,SID3.VoucherID,SID3.RowIndex, SID3.UnitPrice,SI3.TransactionDate\r\n\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID3 INNER JOIN Sales_INVOICE SI3 ON SID3.SysDocID = SI3.SysDocID AND SID3.VoucherID = SI3.VoucherID AND ISNULL(SI3.IsVoid,'False') = 'False' ) AS CashInv\r\n\t\t\t\t\t\t\t ON CashInv.SysDocID = PLD.SysDocID  AND CashInv.VoucherID = PLD.VoucherID AND CashInv.RowIndex = PLD.RowIndex\r\n\r\n                            LEFT OUTER JOIN Sales_Return_Detail  SRD ON SRD.SourceSysDocID = PLD.SysDocID AND SRD.SourceVoucherID = PLD.VoucherID AND SRD.SourceRowIndex = PLD.RowIndex\r\n                           \r\n                            LEFT OUTER JOIN (SELECT SI2.OrderSysDocID,SI2.OrderVoucherID,SI2.OrderRowIndex, SRD2.Quantity FROM Sales_Return_Detail SRD2 INNER JOIN Sales_Invoice_Detail SI2 ON SRD2.SourceSysDocID=SI2.SysDocID\r\n\t\t\t\t\t\t\tAND SRD2.SourceVoucherID = SI2.voucherID AND SRD2.SourceRowIndex = SI2.rowIndex) CrSR ON CRSR.OrderSysDocID = PLD.SysDocID AND CRSR.OrderVoucherID = PLD.VoucherID AND CRSR.OrderRowIndex = PLD.RowIndex\r\n\r\n\r\n                            LEFT OUTER JOIN Product P ON P.ProductID = PLD.ProductID\r\n                            LEFT OUTER JOIN Delivery_Return DR ON DR.DNoteSysDocID = PLD.SysDocID AND DR.DNoteVoucherID = PLD.VoucherID  AND ISNULL(DR.IsVoid,'False') = 'False'\r\n\t\t\t\t\t        LEFT OUTER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID AND DRD.DNRowIndex = PLD.RowIndex\r\n                            LEFT OUTER JOIN Customer CUS ON CUS.CUstomerID = IT.PayeeID AND IT.PayeeType = 'C') CON\r\n                            INNER JOIN Product_Lot PL ON PL.DocID = '" + sysDocID + "' AND PL.ReceiptNumber = '" + voucherID + "' AND PL.ItemCode = CON.ProductID\r\n                            GROUP BY CON.ProductID,Con.Description, PL.DocID,PL.ReceiptNumber,PL.LotNumber,PL.LotQty   \r\n\r\n\r\n                                UNION\r\n\r\n                                SELECT CID.SysDocID AS DocID, CID.VoucherID AS ReceiptNumber,PL.LotNumber, CID.ProductID AS ItemCode,P.Description,PL.LotQty AS LotQty, 0 AS [Quantity Delivered], 0 AS FOC,0  AS [Quantity Invoiced], 0 AS Amount\r\n                                FROM Purchase_Receipt_Detail CID INNER JOIN Purchase_Receipt CI ON CID.SysDocID= CI.SysDocID AnD CID.VoucherID = CI.VoucherID\r\n                                INNER JOIN Product_LOT PL ON PL.DocID = CID.SysDocID AND PL.ReceiptNumber = CID.VoucherID AND PL.RowIndex = CID.RowIndex\r\n                                INNER JOIN Product P ON P.ProductID = CID.ProductID\r\n                                WHERE ISNULL(CI.IsVoid,'False')= 'False' AND CI.VoucherID = '" + voucherID + "' AND CI.SysDocID = '" + sysDocID + "' \r\n                                AND PL.LOTNumber  NOT  IN (SELECT LotNumber FROM Product_Lot_Issue_Detail PLI INNER JOIN System_Document SD ON SD.SysDocID = PLI.SysDocID WHERE SysDocType NOT IN (19,20,21,40,48))\r\n                                AND PL.LOTNumber  NOT  IN (SELECT SourceLotNumber FROM Product_Lot_Issue_Detail PLI INNER JOIN System_Document SD ON SD.SysDocID = PLI.SysDocID WHERE SOURCELotNumber IS NOT NULL AND SysDocType NOT IN (19,20,21,40,48))\r\n                                  ) AS X GROUP BY DocID,ReceiptNumber,LotNumber, ItemCode, Description, LotQty  ORDER BY ItemCode\r\n                                ";
				textCommand += "drop table #tmp1";
				FillDataSet(dataSet, "Purchase_Receipt", textCommand);
				textCommand = "(SELECT LotNumber into #tmp1 FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')\r\n                             OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n                SELECT '" + sysDocID + "' AS DocID, '" + voucherID + "' AS ReceiptNumber, CON.CustomerID,ISNULL(Con.CustomerName,'Non Sale Issue') AS CustomerName,CON.Description,COn.DocName,\r\n                        CASE WHEN SysDocType IN (87) THEN SUM(CON.DeliveredQty) ELSE 0 END  + SUM(CON.FOCQuantity) AS FOC,CON.InvoiceDate,SUM(CON.InvoiceQty - FOCQuantity) AS [Quantity Invoiced],\r\n                        CON.InvoiceVoucherID,COn.LocationID, CON.ProductID AS ItemCode,SUM(CON.DeliveredQty) AS [Quantity Delivered],CON.SysDocID,CON.SysDocType,CON.TransactionDate,CON.UnitPrice AS [Unit Price],\r\n                        CON.VoucherID, ISNULL(Round(SUM(CON.InvoiceQty - FOCQuantity) * Con.UnitPrice,2),0) AS Amount FROM \r\n\r\n\r\n                        (SELECT PLD.*,P.Description, IT.TransactionDate,CASE WHEN IT.PayeeType = 'C' THEN IT.PayeeID ELSE NULL END AS CustomerID ,\r\n                        CUS.CustomerName, ISNULL(INV.VoucherID,PLD.VoucherID) AS InvoiceVoucherID, \r\n                         CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(INV.UnitPrice,CashInv.UnitPrice)  END AS UnitPrice,ISNULL(DRD.Quantity,0) + ISNULL(SRD.Quantity,0) AS QuantityReturned,\r\n                         CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(SoldQty,0)- (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))  END AS InvoiceQty ,ISNULL(INV.TransactionDate,CashInv.TransactionDate) AS InvoiceDate,\r\n                         ISNULL(SoldQty,0) - (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))  AS DeliveredQty\r\n                          FROM ( \r\n  \r\n  \r\n                          SELECT SD.SysDocType,SD.DocName, PLS.DocID AS SysDocID, InvoiceNumber AS VoucherID,RowIndex,ItemCode AS ProductID,PLS.LocationID,LotNo AS LotNumber,SoldQty,ISNULL(FOCQuantity,0) AS FOCQuantity\r\n                          FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID WHERE LotNo IN \r\n                          (SELECT LotNumber FROM #tmp1)\r\n                           AND SysDocType NOT IN (19,20,21,40,48)\r\n                         UNION\r\n\t\t                   SELECT SD.SysDocType,SD.DocName,PLS.SysDocID, VoucherID,RowIndex, ProductID,PLS.LocationID,SourceLotNumber AS  LotNumber,-1 * LotQty AS SoldQty,0 AS FOCQuantity\r\n\t\t                    FROM Product_Lot_Receiving_Detail PLS INNER JOIN System_Document SD ON PLS.SysDocID = SD.SysDocID WHERE SourceLotNumber IN \r\n\t\t\t                   (SELECT LotNumber FROM #tmp1)\r\n\t\t\t                   AND SysDocType NOT IN (19,20,21,40,48))  AS PLD\r\n\r\n\t\t\t                    LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = PLD.SysDocID AND IT.VoucherID = PLD.VoucherID AND IT.RowIndex = PLD.RowIndex\r\n\t\t\t\t                 LEFT OUTER JOIN  (SELECT SID2.Quantity,SID2.OrderSysDocID,SID2.VoucherID,SID2.OrderVoucherID,SID2.OrderRowIndex,SID2.UnitPrice,SI2.TransactionDate\r\n\t\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID2 INNER JOIN Sales_INVOICE SI2 ON SID2.SysDocID = SI2.SysDocID AND SID2.VoucherID = SI2.VoucherID AND ISNULL(SI2.IsVoid,'False') = 'False' ) AS INV ON INV.OrderSysDocID = PLD.SysDocID\r\n\t\t\t\t\t\t\t\t AND INV.OrderVoucherID = PLD.VoucherID AND INV.OrderRowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t\t\t\t\t --Cash Sales\r\n\t\t\t\t\t\t\t\t LEFT OUTER JOIN (SELECT SID3.Quantity,SID3.SysDocID,SID3.VoucherID,SID3.RowIndex, SID3.UnitPrice,SI3.TransactionDate\r\n\t\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID3 INNER JOIN Sales_INVOICE SI3 ON SID3.SysDocID = SI3.SysDocID AND SID3.VoucherID = SI3.VoucherID AND ISNULL(SI3.IsVoid,'False') = 'False' ) AS CashInv\r\n\t\t\t\t\t\t\t\t ON CashInv.SysDocID = PLD.SysDocID  AND CashInv.VoucherID = PLD.VoucherID AND CashInv.RowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t                    LEFT OUTER JOIN Delivery_Return DR ON DR.DNoteSysDocID = PLD.SysDocID AND DR.DNoteVoucherID = PLD.VoucherID  AND ISNULL(DR.IsVoid,'False') = 'False'\r\n\t\t\t\t\t                 LEFT OUTER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID AND DRD.DNRowIndex = PLD.RowIndex\r\n\t\t\t\t\t                  LEFT OUTER JOIN Sales_Return_Detail  SRD ON SRD.SourceSysDocID = PLD.SysDocID AND SRD.SourceVoucherID = PLD.VoucherID AND SRD.SourceRowIndex = PLD.RowIndex  \r\n\t\t\t\t\t                 LEFT OUTER JOIN (SELECT SI2.OrderSysDocID,SI2.OrderVoucherID,SI2.OrderRowIndex, SRD2.Quantity FROM Sales_Return_Detail SRD2 INNER JOIN Sales_Invoice_Detail SI2 ON SRD2.SourceSysDocID=SI2.SysDocID\r\n\t\t\t\t\t\t\t\t\t\t\tAND SRD2.SourceVoucherID = SI2.voucherID AND SRD2.SourceRowIndex = SI2.rowIndex) CrSR ON CRSR.OrderSysDocID = PLD.SysDocID AND CRSR.OrderVoucherID = PLD.VoucherID AND CRSR.OrderRowIndex = PLD.RowIndex\r\n\r\n                                            LEFT OUTER JOIN Product P ON P.ProductID = PLD.ProductID\r\n\t\t\t\t\t                      LEFT OUTER JOIN Customer CUS ON CUS.CUstomerID = IT.PayeeID AND IT.PayeeType = 'C') CON\r\n\t\t\t\t\t\t                   WHERE DeliveredQty > 0 \r\n\t\t\t\t\t\t   GROUP BY   CON.CustomerID,Con.CustomerName,CON.Description,COn.DocName, CON.InvoiceDate, CON.InvoiceVoucherID,COn.LocationID, CON.ProductID ,CON.SysDocID,CON.SysDocType,CON.TransactionDate,CON.UnitPrice  , CON.VoucherID \r\n                                 ORDER BY ItemCode,TransactionDate  ";
				textCommand += "drop table #tmp1";
				FillDataSet(dataSet, "Purchase_Receipt_Detail", textCommand);
				dataSet.Relations.Add("VendorGRN", new DataColumn[2]
				{
					dataSet.Tables["Vendor"].Columns["SysDocID"],
					dataSet.Tables["Vendor"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Purchase_Receipt"].Columns["DocID"],
					dataSet.Tables["Purchase_Receipt"].Columns["ReceiptNumber"]
				}, createConstraints: false);
				dataSet.Relations.Add("VendorGRNDetail", new DataColumn[2]
				{
					dataSet.Tables["Vendor"].Columns["SysDocID"],
					dataSet.Tables["Vendor"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Purchase_Receipt_Detail"].Columns["DocID"],
					dataSet.Tables["Purchase_Receipt_Detail"].Columns["ReceiptNumber"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetVoucherNumbersFromTransaction(string tableName, string sysDocID, string voucherID, DateTime fromDate, DateTime toDate)
		{
			string text = "";
			string text2 = StoreConfiguration.ToSqlDateTimeString(fromDate);
			string text3 = StoreConfiguration.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			if (tableName != "")
			{
				text = "SELECT DISTINCT T.SysDocID [Doc ID],T.VoucherID, TransactionDate from " + tableName + " T    WHERE T.SysDocID= '" + sysDocID + "' AND TransactionDate BETWEEN '" + text2 + "' AND '" + text3 + "'";
				FillDataSet(dataSet, "Transactions", text);
			}
			return dataSet;
		}

		public DataSet GetGRNItemTaxDetails(DataTable dtItems)
		{
			try
			{
				DataSet dataSet = new DataSet();
				ArrayList arrayList = new ArrayList();
				foreach (DataRow row in dtItems.Rows)
				{
					if (dtItems.Columns.Contains("ProductID"))
					{
						arrayList.Add(row["ProductID"].ToString());
					}
					else
					{
						arrayList.Add(row["Item Code"].ToString());
					}
				}
				string str = string.Join("','", arrayList.ToArray());
				string textCommand = "SELECT Product.ProductID,Product.TaxGroupID AS ItemTaxGroupID\r\n                                ,IsNull(Product.TaxOption,2) As TaxOption  FROM Product                        \r\n              \r\n                WHERE Product.ProductID IN ('" + str + "' )";
				FillDataSet(dataSet, "Purchase_Receipt_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetContainerData(string fromClass, string toClass, int status, DateTime fromDate, DateTime toDate)
		{
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(fromDate);
				string text2 = StoreConfiguration.ToSqlDateTimeString(toDate);
				DataSet dataSet = new DataSet();
				string text3 = "Select PR.SysDocID,PR.VoucherID,PR.TransactionDate,PR.ContainerNumber,V.VendorID,V.VendorName [Vendor],(select Top 1 P.CategoryID from Purchase_Receipt_Detail PRD Left Join Product P On P.ProductID=PRD.ProductID WHere Sysdocid=PR.SysDocID and voucherid=PR.VoucherID Group By P.CategoryID Order By Count(P.CategoryID) Desc) AS CategoryName,\r\n                              Isnull(PR.ClaimAmount,0) AS ClaimAmount,Isnull(PR.ClaimAmountFC,0) AS ClaimAmountFC,ClaimCurrencyID,PR.GroupName,PR.ClaimRef1,PR.ClaimRef2,Case When PR.ClaimStatus =1 Then 'Pending' When PR.ClaimStatus =2 Then 'Closed' Else 'Open' End as [Status],PO.SysDocID as POSysDocID,PO.VoucherID as POVoucherID,PK.SourceSysDocID,PK.SourceVoucherID,\r\n                              Case When PO.SysDocID IS NOT NULL THEN Isnull((SELECT top 1 Isnull(MinGuarantee,0)MinGuarantee FROM Purchase_Order_DetaiL where SysDocID=PO.SysDocID And VoucherId=PO.VoucherId And Isnull(MinGuarantee,0)!=0 Group By MinGuarantee HAVING COUNT(*) > 0 Order By COUNT(*) Desc),0) ELSE \r\n\t\t\t\t\t\t\t  Isnull((SELECT top 1 Isnull(MinGuarantee,0)MinGuarantee FROM Purchase_Order_DetaiL where SysDocID=PK.SourceSysDocID And VoucherId=PK.SourceVoucherId And Isnull(MinGuarantee,0)!=0 Group By MinGuarantee HAVING COUNT(*) > 0 Order By COUNT(*) Desc),0) END AS MinGuarantee\r\n                              From Purchase_Receipt PR \r\n                              Left Join Vendor V ON V.VendorID=PR.VendorID\r\n                              Left Join Purchase_Order PO ON PO.SysDocID=PR.SourceSysDocID And PO.VoucherID=PR.SourceVoucherID\r\n\t\t\t\t\t\t\t  Left Join (SELECT Distinct PO1.SysDocID as SourceSysDocID,PO1.VoucherID as SourceVoucherID,POS.SysDocID,POS.VoucherID FROM PO_Shipment_Detail POS Left Join Purchase_Order PO1 ON PO1.SysDocID=POS.SourceSysDocID And PO1.VoucherID=POS.SourceVoucherID) as PK ON PK.SysDocID=PR.SourceSysDocID And PK.VoucherID=PR.SourceVoucherID\r\n                              where Convert(date,PR.TransactionDate) BETWEEN CONVERT(date,'" + text + "') AND CONVERT(date,'" + text2 + "')";
				if (fromClass != "")
				{
					text3 = text3 + " AND V.VendorClassID>='" + fromClass + "'";
				}
				if (toClass != "")
				{
					text3 = text3 + " AND V.VendorClassID<='" + toClass + "'";
				}
				if (status == 0)
				{
					text3 += " AND Isnull(ClaimStatus, 0) !=2";
				}
				FillDataSet(dataSet, "Container_Details", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetContainerLotData(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "Select ItemCode,P.Description,LotQty [Qty Received],SoldQty [Sold],Isnull(ReturnedQty,0) [Returned],\r\n                              Isnull(LotQty,0)-Isnull(SoldQty,0)+Isnull(ReturnedQty,0) as [Balance] from Product_Lot PL\r\n                              Left Join Product P ON P.ProductID=PL.ItemCode where DocID='" + sysDocID + "' AND ReceiptNumber='" + voucherID + "'";
				FillDataSet(dataSet, "Container_Lot_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool UpdateContainerDetails(PurchaseReceiptData purchaseReceiptData)
		{
			return new PurchaseReceipt(base.DBConfig).UpdateContainerDetails(purchaseReceiptData, null);
		}

		private bool UpdateContainerDetails(PurchaseReceiptData purchaseReceiptData, SqlTransaction sqlTransaction)
		{
			try
			{
				if (purchaseReceiptData == null || purchaseReceiptData.Tables.Count == 0 || purchaseReceiptData.Tables["Purchase_Receipt"].Rows.Count == 0)
				{
					return false;
				}
				bool flag = true;
				DataRow dataRow = purchaseReceiptData.PurchaseReceiptTable.Rows[0];
				string exp = "UPDATE Purchase_Receipt SET ClaimStatus=" + dataRow["ClaimStatus"] + ",GroupName='" + dataRow["GroupName"] + "',\r\n                                ClaimAmount=" + dataRow["ClaimAmount"] + ",ClaimAmountFC='" + dataRow["ClaimAmountFC"] + "',ClaimRef1='" + dataRow["ClaimRef1"] + "',ClaimRef2='" + dataRow["ClaimRef2"] + "',\r\n                               ClaimRemarks='" + dataRow["ClaimRemarks"] + "',ClaimCurrencyID='" + dataRow["ClaimCurrencyID"] + "',ClaimCurrencyRate=" + dataRow["ClaimCurrencyRate"] + " where SysDocID = '" + dataRow["SysDocID"] + "' and Voucherid = '" + dataRow["VoucherID"] + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				if (flag)
				{
					flag &= UpdateTableRowInsertUpdateInfo("Purchase_Receipt", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, isInsert: false);
					string entiyID = dataRow["SysDocID"].ToString() + "-" + dataRow["VoucherID"].ToString();
					string entityName = "Shipment Claim Status";
					flag &= AddActivityLog(entityName, entiyID, "US_CS", ActivityTypes.Update, sqlTransaction);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertGRNDetails(DataSet purchaseReceiptData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				SqlCommand insertUpdatePurchaseReceiptDetailsCommand = GetInsertUpdatePurchaseReceiptDetailsCommand(isUpdate: false);
				insertUpdatePurchaseReceiptDetailsCommand.Transaction = sqlTransaction;
				string text = "IsNew = 'True'";
				string sort = "RowIndex ASC";
				DataTable dataTable = new DataTable();
				DataTable dataTable2 = new DataView(purchaseReceiptData.Tables["Purchase_Receipt_Detail"])
				{
					RowFilter = text
				}.ToTable();
				if (dataTable2.Rows.Count <= 0)
				{
					return true;
				}
				dataTable = purchaseReceiptData.Tables["Purchase_Receipt_Detail"].Select(text, sort).CopyToDataTable();
				if (dataTable.Rows.Count > 0)
				{
					DataSet dataSet = new DataSet();
					dataTable.TableName = "Purchase_Receipt_Detail";
					dataSet.Tables.Add(dataTable2);
					return flag & Insert(dataSet, "Purchase_Receipt_Detail", insertUpdatePurchaseReceiptDetailsCommand);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertGRNInventoryDetails(PurchaseReceiptData purchaseReceiptData, SqlTransaction sqlTransaction, bool isImport)
		{
			bool flag = true;
			try
			{
				string rowFilter = "IsNew = 'True'";
				DataSet dataSet = new DataSet();
				DataTable dataTable = new DataTable();
				dataTable = new DataView(purchaseReceiptData.Tables["Purchase_Receipt_Detail"])
				{
					RowFilter = rowFilter
				}.ToTable();
				dataTable.TableName = "Purchase_Receipt_Detail";
				dataSet.Tables.Add(dataTable);
				DataRow dataRow = purchaseReceiptData.PurchaseReceiptTable.Rows[0];
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row in dataSet.Tables["Purchase_Receipt_Detail"].Rows)
				{
					string idFieldValue = row["ProductID"].ToString();
					DataRow dataRow3 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["SysDocID"] = row["SysDocID"];
					dataRow3["VoucherID"] = row["VoucherID"];
					if (row["LocationID"].ToString() == "")
					{
						throw new Exception("Location cannot be empty.");
					}
					dataRow3["LocationID"] = row["LocationID"];
					dataRow3["JobID"] = row["JobID"];
					dataRow3["ProductID"] = row["ProductID"];
					dataRow3["Quantity"] = decimal.Parse(row["Quantity"].ToString());
					dataRow3["Reference"] = dataRow["Reference"];
					if (isImport)
					{
						dataRow3["SysDocType"] = (byte)50;
					}
					else
					{
						dataRow3["SysDocType"] = (byte)32;
					}
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "LastCost", "ProductID", idFieldValue, sqlTransaction);
					if (fieldValue != null && fieldValue.ToString() != "")
					{
						dataRow3["UnitPrice"] = fieldValue.ToString();
					}
					else
					{
						dataRow3["UnitPrice"] = 0;
					}
					dataRow3["IsNonCostedGRN"] = true;
					dataRow3["TransactionDate"] = dataRow["TransactionDate"];
					dataRow3["TransactionType"] = (byte)1;
					dataRow3["PayeeType"] = "V";
					dataRow3["PayeeID"] = dataRow["VendorID"];
					dataRow3["Cost"] = dataRow3["UnitPrice"];
					dataRow3["RowIndex"] = row["RowIndex"];
					dataRow3["SpecificationID"] = row["SpecificationID"];
					dataRow3["StyleID"] = row["StyleID"];
					dataRow3["CompanyID"] = dataRow["CompanyID"];
					dataRow3["DivisionID"] = dataRow["DivisionID"];
					dataRow3["UnitID"] = row["UnitID"];
					if (row["UnitQuantity"] != DBNull.Value && row["UnitFactor"] != DBNull.Value)
					{
						dataRow3["UnitQuantity"] = row["UnitQuantity"];
						dataRow3["Factor"] = row["UnitFactor"];
						dataRow3["FactorType"] = row["FactorType"];
						decimal.Parse(row["UnitFactor"].ToString());
						row["FactorType"].ToString();
						decimal d = decimal.Parse(row["UnitQuantity"].ToString());
						decimal num = decimal.Parse(row["Quantity"].ToString());
						decimal d2 = decimal.Parse(row["UnitPrice"].ToString());
						decimal num2 = default(decimal);
						num2 = ((!(num != 0m)) ? default(decimal) : (d * d2 / num));
						dataRow3["UnitPrice"] = num2;
					}
					dataRow3.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow3);
				}
				if (inventoryTransactionData.InventoryTransactionTable.Rows.Count > 0)
				{
					inventoryTransactionData.Merge(purchaseReceiptData.Tables["Product_Lot_Receiving_Detail"]);
					flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(purchaseReceiptData, isUpdate: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).AllocateInvoiceToLot(inventoryTransactionData, sqlTransaction);
					DataRow dataRow4 = inventoryTransactionData.Tables["Inventory_Transactions"].Rows[0];
					SqlCommand sqlCommand = new SqlCommand("Insert into Inventory_Transactions ([SysDocID],[VoucherID],[SysDocType],[TransactionDate],[ProductID],[LotNumber],[RowIndex],[JobID],[CostCategoryID],[EqWorkOrderID]\r\n                                          ,[CompanyID],[DivisionID] ,[LocationID],[FOCQuantity],[Quantity],[UnitQuantity],[Factor],[FactorType],[UnitID] ,[UnitPrice],[Discount],[AverageCost],[AssetValue]\r\n                                          ,[Reference] ,[Description],[PayeeType],[PayeeID],[TransactionType],[Cost],[SpecificationID],[StyleID],[IsNonCostedGRN],[IsRecost] ,[RefSysDocID],[RefVoucherID]\r\n                                          ,[RefRowIndex] ,[RefTransactionID],[DateCreated],[CreatedBy]) values (@SysDocID,@VoucherID,@SysDocType,@TransactionDate,@ProductID,@LotNumber,@RowIndex,@JobID,@CostCategoryID,@EqWorkOrderID\r\n                                          ,@CompanyID,@DivisionID ,@LocationID ,@FOCQuantity,@Quantity,@UnitQuantity,@Factor,@FactorType,@UnitID ,@UnitPrice,@Discount,@AverageCost,@AssetValue\r\n                                          ,@Reference ,@Description,@PayeeType,@PayeeID,@TransactionType,@Cost,@SpecificationID,@StyleID,@IsNonCostedGRN,@IsRecost ,@RefSysDocID,@RefVoucherID\r\n                                          ,@RefRowIndex ,@RefTransactionID,@DateCreated,@CreatedBy)", base.DBConfig.Connection);
					sqlCommand.Parameters.Add(new SqlParameter("@SysDocID", SqlDbType.NVarChar)).Value = dataRow4["SysDocID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@VoucherID", SqlDbType.NVarChar)).Value = dataRow4["VoucherID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@CompanyID", SqlDbType.TinyInt)).Value = dataRow4["CompanyID"];
					sqlCommand.Parameters.Add(new SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = dataRow4["DivisionID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@Reference", SqlDbType.NVarChar)).Value = dataRow4["Reference"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@SysDocType", SqlDbType.Int)).Value = dataRow4["SysDocType"];
					sqlCommand.Parameters.Add(new SqlParameter("@TransactionDate", SqlDbType.DateTime)).Value = dataRow4["TransactionDate"];
					sqlCommand.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.NVarChar)).Value = dataRow4["ProductID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@LotNumber", SqlDbType.NVarChar)).Value = dataRow4["LotNumber"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@RowIndex", SqlDbType.Int)).Value = dataRow4["RowIndex"];
					sqlCommand.Parameters.Add(new SqlParameter("@JobID", SqlDbType.NVarChar)).Value = dataRow4["JobID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@CostCategoryID", SqlDbType.NVarChar)).Value = dataRow4["CostCategoryID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@EqWorkOrderID", SqlDbType.NVarChar)).Value = dataRow4["EqWorkOrderID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.NVarChar)).Value = dataRow4["LocationID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@FOCQuantity", SqlDbType.Real)).Value = dataRow4["FOCQuantity"];
					sqlCommand.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Real)).Value = dataRow4["Quantity"];
					sqlCommand.Parameters.Add(new SqlParameter("@UnitQuantity", SqlDbType.Real)).Value = dataRow4["UnitQuantity"];
					sqlCommand.Parameters.Add(new SqlParameter("@Factor", SqlDbType.Real)).Value = dataRow4["Factor"];
					sqlCommand.Parameters.Add(new SqlParameter("@FactorType", SqlDbType.Char)).Value = dataRow4["FactorType"];
					sqlCommand.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.NVarChar)).Value = dataRow4["UnitID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@UnitPrice", SqlDbType.Money)).Value = dataRow4["UnitPrice"];
					sqlCommand.Parameters.Add(new SqlParameter("@Discount", SqlDbType.Money)).Value = dataRow4["Discount"];
					sqlCommand.Parameters.Add(new SqlParameter("@AverageCost", SqlDbType.Money)).Value = dataRow4["AverageCost"];
					sqlCommand.Parameters.Add(new SqlParameter("@AssetValue", SqlDbType.Money)).Value = dataRow4["AssetValue"];
					sqlCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar)).Value = dataRow4["Description"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@PayeeType", SqlDbType.NVarChar)).Value = dataRow4["PayeeType"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@PayeeID", SqlDbType.NVarChar)).Value = dataRow4["PayeeID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@TransactionType", SqlDbType.NVarChar)).Value = dataRow4["TransactionType"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@Cost", SqlDbType.Money)).Value = dataRow4["Cost"];
					sqlCommand.Parameters.Add(new SqlParameter("@SpecificationID", SqlDbType.NVarChar)).Value = dataRow4["SpecificationID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@StyleID", SqlDbType.NVarChar)).Value = dataRow4["StyleID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@IsNonCostedGRN", SqlDbType.Bit)).Value = dataRow4["IsNonCostedGRN"];
					sqlCommand.Parameters.Add(new SqlParameter("@IsRecost", SqlDbType.Bit)).Value = dataRow4["IsRecost"];
					sqlCommand.Parameters.Add(new SqlParameter("@RefSysDocID", SqlDbType.NVarChar)).Value = dataRow4["RefSysDocID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@RefVoucherID", SqlDbType.NVarChar)).Value = dataRow4["RefVoucherID"].ToString();
					sqlCommand.Parameters.Add(new SqlParameter("@RefRowIndex", SqlDbType.Int)).Value = dataRow4["RefRowIndex"];
					sqlCommand.Parameters.Add(new SqlParameter("@RefTransactionID", SqlDbType.Int)).Value = dataRow4["RefTransactionID"];
					sqlCommand.Parameters.Add(new SqlParameter("@DateCreated", SqlDbType.DateTime)).Value = DateTime.Now;
					sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar)).Value = "";
					sqlCommand.CommandType = CommandType.Text;
					sqlCommand.Transaction = sqlTransaction;
					flag &= (sqlCommand.ExecuteNonQuery() > 0);
				}
				string text = dataRow["SysDocID"].ToString();
				string text2 = dataRow["VoucherID"].ToString();
				string value = dataRow["VendorID"].ToString();
				DateTime dateTime = DateTime.Parse(dataRow["TransactionDate"].ToString());
				string exp = "SELECT MIN(TransactionDate) FROM Product_Lot_Sales WHERE LotNo IN(SELECT LotNumber FROM Product_Lot  WHERE DocID = '" + text + "' AND ReceiptNumber = '" + text2 + "')";
				object obj = ExecuteScalar(exp);
				if (obj == null)
				{
					return flag;
				}
				if (obj.ToString() != "")
				{
					if (DateTime.Parse(obj.ToString()) < dateTime)
					{
						throw new CompanyException("Cannot modify this document because the transaction date is higher than refered transaction date.");
					}
					exp = "Update Product_Lot SET ReceiptDate = @ReceiptDate,SupplierCode=@VendorID WHERE DocID = @DocID AND ReceiptNumber =@ReceiptNumber;\r\n                                       Update Inventory_Transactions SET TransactionDate = @ReceiptDate,PayeeID=@VendorID WHERE SysDocID = @DocID AND VoucherID =@ReceiptNumber";
					SqlCommand sqlCommand2 = new SqlCommand(exp, base.DBConfig.Connection);
					sqlCommand2.Parameters.Add(new SqlParameter("@ReceiptDate", SqlDbType.DateTime)).Value = dateTime;
					sqlCommand2.Parameters.Add(new SqlParameter("@VendorID", SqlDbType.NVarChar)).Value = value;
					sqlCommand2.Parameters.Add(new SqlParameter("@DocID", SqlDbType.NVarChar)).Value = text;
					sqlCommand2.Parameters.Add(new SqlParameter("@ReceiptNumber", SqlDbType.NVarChar)).Value = text2;
					sqlCommand2.CommandType = CommandType.Text;
					sqlCommand2.Transaction = sqlTransaction;
					return flag & (sqlCommand2.ExecuteNonQuery() > 0);
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
