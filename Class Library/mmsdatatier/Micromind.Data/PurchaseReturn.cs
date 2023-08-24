using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PurchaseReturn : StoreObject
	{
		private const string PURCHASERETURN_TABLE = "Purchase_Return";

		private const string PURCHASERETURNDETAIL_TABLE = "Purchase_Return_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string VENDORID_PARM = "@VendorID";

		private const string PURCHASEFLOW_PARM = "@PurchaseFlow";

		private const string ISCASH_PARM = "@IsCash";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string BUYER_PARM = "@BuyerID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string VENDORADDRESS_PARM = "@VendorAddress";

		private const string STATUS_PARM = "@Status";

		private const string PRICEINCLUDETAX_PARM = "@PriceIncludeTax";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string DISCOUNTFC_PARM = "@DiscountFC";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TOTAL_PARM = "@Total";

		private const string TOTALFC_PARM = "@TotalFC";

		private const string VENDORREFERENCENUMBER_PARM = "@VendorReferenceNo";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITPRICEFC_PARM = "@UnitPriceFC";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string DNOTEVOUCHERID_PARM = "@DNoteVoucherID";

		private const string DNOTESYSDOCID_PARM = "@DNoteSysDocID";

		private const string ORDERROWINDEX_PARM = "@OrderRowIndex";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string SPECIFICATIONID_PARM = "@SpecificationID";

		private const string STYLEID_PARM = "@StyleID";

		private const string TAXPERCENTAGE_PARM = "@TaxPercentage";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string REMARKS_PARM = "@Remarks";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string AMOUNT_PARM = "@Amount";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string INVOICEPAYMENT_PARM = "@Invoice_Payment";

		public PurchaseReturn(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePurchaseReturnText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Return", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("IsCash", "@IsCash"), new FieldValue("PurchaseFlow", "@PurchaseFlow"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("VendorAddress", "@VendorAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("VendorReferenceNo", "@VendorReferenceNo"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Purchase_Return", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseReturnCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseReturnText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseReturnText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@IsCash", SqlDbType.Bit);
			parameters.Add("@PurchaseFlow", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@BuyerID", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@VendorAddress", SqlDbType.NVarChar);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@DiscountFC", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@TotalFC", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@VendorReferenceNo", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@IsCash"].SourceColumn = "IsCash";
			parameters["@PurchaseFlow"].SourceColumn = "PurchaseFlow";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@PriceIncludeTax"].SourceColumn = "PriceIncludeTax";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@VendorAddress"].SourceColumn = "VendorAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@DiscountFC"].SourceColumn = "DiscountFC";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@TotalFC"].SourceColumn = "TotalFC";
			parameters["@Note"].SourceColumn = "Note";
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

		private string GetInsertUpdatePurchaseReturnDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Return_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitID", "@UnitID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("SpecificationID", "@SpecificationID"), new FieldValue("StyleID", "@StyleID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxPercentage", "@TaxPercentage"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("FactorType", "@FactorType"), new FieldValue("DNoteSysDocID", "@DNoteSysDocID"), new FieldValue("DNoteVoucherID", "@DNoteVoucherID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseReturnDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseReturnDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseReturnDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@SpecificationID", SqlDbType.NVarChar);
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@AmountFC", SqlDbType.Decimal);
			parameters.Add("@DNoteSysDocID", SqlDbType.NVarChar);
			parameters.Add("@DNoteVoucherID", SqlDbType.NVarChar);
			parameters.Add("@OrderRowIndex", SqlDbType.Int);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@TaxPercentage", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@UnitPriceFC"].SourceColumn = "UnitPriceFC";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@SpecificationID"].SourceColumn = "SpecificationID";
			parameters["@StyleID"].SourceColumn = "StyleID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@DNoteSysDocID"].SourceColumn = "DNoteSysDocID";
			parameters["@DNoteVoucherID"].SourceColumn = "DNoteVoucherID";
			parameters["@OrderRowIndex"].SourceColumn = "OrderRowIndex";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@TaxPercentage"].SourceColumn = "TaxPercentage";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdatePaymentText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Invoice_Payment", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("AccountID", "@AccountID"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Amount", "@Amount"), new FieldValue("PaymentMethodType", "@PaymentMethodType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePaymentCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePaymentText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePaymentText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(PurchaseReturnData purchaseReturnData, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = "";
				DataSet dataSet = new DataSet();
				if (purchaseReturnData.PurchaseReturnTable.Rows.Count == 0)
				{
					throw new CompanyException("Transaction row not found.");
				}
				if (purchaseReturnData.PurchaseReturnDetailTable.Rows.Count == 0)
				{
					throw new CompanyException("At least one detail row should be added to the transaction.");
				}
				DataRow dataRow = purchaseReturnData.PurchaseReturnTable.Rows[0];
				string text2 = dataRow["VoucherID"].ToString();
				string text3 = dataRow["SysDocID"].ToString();
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (itemSourceTypes == ItemSourceTypes.PurchaseInvoice)
				{
					DataRow dataRow2 = purchaseReturnData.PurchaseReturnDetailTable.Rows[0];
					string text4 = dataRow2["SourceVoucherID"].ToString();
					string text5 = dataRow2["SourceSysDocID"].ToString();
					if (text4 != "" || text5 != "")
					{
						text = text + "SELECT PID.SysDocID,PID.VoucherID,PID.RowIndex,PID.ProductID,ISNULL(PID.UnitQuantity,PID.Quantity) AS Quantity,ISNULL(SUM(PRD.Quantity),0) AS QuantityReturned, ISNULL(PID.UnitQuantity,PID.Quantity) - ISNULL(SUM( PRD.Quantity),0) AS QuantityBalance \r\n                                 FROM Purchase_Invoice_Detail PID\r\n                                 LEFT OUTER JOIN Purchase_Return_Detail PRD ON PID.SysDocID= PRD.SourceSysDocID AND PID.VoucherID = PRD.SourceVoucherID AND PID.RowIndex = PRD.SourceRowIndex \r\n                                 AND ( PRD.SysDocID <> '" + text3 + "' OR  PRD.VoucherID <> '" + text2 + "')\r\n\r\n                                    WHERE PID.SysDocID = '" + text5 + "' AND PID.VoucherID = '" + text4 + "' \r\n\t\t\t\t\t\t\t\t\tGROUP BY PID.SysDocID,PID.VoucherID,PID.RowIndex,PID.ProductID,PID.Quantity,PID.UnitQuantity";
						FillDataSet(dataSet, "Invoice", text, sqlTransaction);
						foreach (DataRow row in purchaseReturnData.PurchaseReturnDetailTable.Rows)
						{
							float num = float.Parse(row["Quantity"].ToString());
							int num2 = int.Parse(row["SourceRowIndex"].ToString());
							DataRow[] array = dataSet.Tables["Invoice"].Select("RowIndex = " + num2);
							if (array.Length != 0)
							{
								float num3 = float.Parse(array[0]["QuantityBalance"].ToString());
								if (num > num3)
								{
									throw new CompanyException("Returned quantity cannot be greater than balance quantity.\n Item:" + row["ProductID"].ToString());
								}
							}
						}
					}
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertUpdatePurchaseReturn(PurchaseReturnData purchaseReturnData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePurchaseReturnCommand = GetInsertUpdatePurchaseReturnCommand(isUpdate);
			string text = "";
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = purchaseReturnData.PurchaseReturnTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text2 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				flag &= ValidateData(purchaseReturnData, sqlTransaction);
				bool result = false;
				bool.TryParse(dataRow["IsCash"].ToString(), out result);
				decimal num = default(decimal);
				foreach (DataRow row in purchaseReturnData.PurchaseReturnDetailTable.Rows)
				{
					decimal num2 = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result2);
					decimal.TryParse(row["UnitPrice"].ToString(), out result3);
					num2 = Math.Round(result2 * result3, currencyDecimalPoints);
					num += num2;
				}
				dataRow["Total"] = num;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Purchase_Return", "VoucherID", dataRow["SysDocID"].ToString(), text2, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				bool flag2 = false;
				decimal result4 = 1m;
				string a = "M";
				if (dataRow["CurrencyID"] != DBNull.Value && baseCurrencyID != dataRow["CurrencyID"].ToString())
				{
					flag2 = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result4);
					a = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
				}
				if (flag2)
				{
					decimal result5 = default(decimal);
					dataRow["TotalFC"] = dataRow["Total"];
					decimal.TryParse(dataRow["Total"].ToString(), out result5);
					result5 = ((!(a == "M")) ? Math.Round(result5 / result4, 4) : Math.Round(result5 * result4, 4));
					dataRow["Total"] = result5;
					result5 = default(decimal);
					dataRow["DiscountFC"] = dataRow["Discount"];
					decimal.TryParse(dataRow["DiscountFC"].ToString(), out result5);
					result5 = ((!(a == "M")) ? Math.Round(result5 / result4, 4) : Math.Round(result5 * result4, 4));
					dataRow["Discount"] = result5;
					result5 = default(decimal);
					dataRow["TaxAmountFC"] = dataRow["TaxAmount"];
					decimal.TryParse(dataRow["TaxAmount"].ToString(), out result5);
					result5 = ((!(a == "M")) ? Math.Round(result5 / result4, 4) : Math.Round(result5 * result4, 4));
					dataRow["TaxAmount"] = result5;
				}
				foreach (DataRow row2 in purchaseReturnData.PurchaseReturnDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					text = row2["ProductID"].ToString();
					string text3 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text, sqlTransaction);
					if (fieldValue != null)
					{
						text3 = fieldValue.ToString();
					}
					if (text3 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text3)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text + "\nUnit:" + row2["UnitID"].ToString());
						float num3 = float.Parse(obj2["Factor"].ToString());
						string text4 = obj2["FactorType"].ToString();
						float num4 = float.Parse(row2["Quantity"].ToString());
						row2["UnitFactor"] = num3;
						row2["FactorType"] = text4;
						row2["UnitQuantity"] = row2["Quantity"];
						num4 = ((!(text4 == "M")) ? float.Parse(Math.Round(num4 * num3, 5).ToString()) : float.Parse(Math.Round(num4 / num3, 5).ToString()));
						row2["Quantity"] = num4;
					}
					if (flag2)
					{
						decimal result6 = default(decimal);
						decimal result7 = default(decimal);
						decimal result8 = default(decimal);
						row2["UnitPriceFC"] = row2["UnitPrice"];
						row2["AmountFC"] = row2["Amount"];
						decimal.TryParse(row2["UnitPrice"].ToString(), out result6);
						decimal.TryParse(row2["Amount"].ToString(), out result7);
						decimal.TryParse(row2["TaxAmount"].ToString(), out result8);
						result6 = ((!(a == "M")) ? Math.Round(result6 / result4, 4) : Math.Round(result6 * result4, 4));
						row2["UnitPrice"] = result6;
						if (a == "M")
						{
							result8 = Math.Round(result8 * result4, 4);
						}
						else
						{
							result6 = Math.Round(result8 / result4, 4);
						}
						row2["TaxAmountLC"] = result8;
						result7 = ((!(a == "M")) ? Math.Round(result7 / result4, currencyDecimalPoints) : Math.Round(result7 * result4, currencyDecimalPoints));
						row2["Amount"] = result7;
					}
				}
				insertUpdatePurchaseReturnCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(purchaseReturnData, "Purchase_Return", insertUpdatePurchaseReturnCommand)) : (flag & Insert(purchaseReturnData, "Purchase_Return", insertUpdatePurchaseReturnCommand)));
				insertUpdatePurchaseReturnCommand = GetInsertUpdatePurchaseReturnDetailsCommand(isUpdate: false);
				insertUpdatePurchaseReturnCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeletePurchaseReturnDetailsRows(sysDocID, text2, isDeletingTransaction: false, sqlTransaction);
				}
				if (purchaseReturnData.Tables["Purchase_Return_Detail"].Rows.Count > 0)
				{
					flag &= Insert(purchaseReturnData, "Purchase_Return_Detail", insertUpdatePurchaseReturnCommand);
				}
				insertUpdatePurchaseReturnCommand = GetInsertUpdatePaymentCommand(isUpdate: false);
				insertUpdatePurchaseReturnCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeletePaymentRows(sysDocID, text2, sqlTransaction);
				}
				DataRow dataRow3 = null;
				if (result)
				{
					dataRow3 = purchaseReturnData.PaymentTable.Rows[0];
					PaymentMethodTypes paymentMethodTypes = PaymentMethodTypes.Cash;
					if (result)
					{
						paymentMethodTypes = (PaymentMethodTypes)byte.Parse(dataRow3["PaymentMethodType"].ToString());
					}
					string registerID = dataRow3["RegisterID"].ToString();
					string text5 = "";
					text5 = (string)(dataRow3["AccountID"] = ((paymentMethodTypes != PaymentMethodTypes.CreditCard) ? new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID") : new Register(base.DBConfig).GetRegisterAccountID(registerID, "CardReceivedAccountID")));
					dataRow3.EndEdit();
				}
				if (purchaseReturnData.Tables["Invoice_Payment"].Rows.Count > 0)
				{
					flag &= Insert(purchaseReturnData, "Invoice_Payment", insertUpdatePurchaseReturnCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row3 in purchaseReturnData.PurchaseReturnDetailTable.Rows)
				{
					DataRow dataRow5 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["SysDocID"] = row3["SysDocID"];
					dataRow5["VoucherID"] = row3["VoucherID"];
					if (row3["LocationID"].ToString() == "")
					{
						throw new Exception("Location cannot be empty.");
					}
					dataRow5["LocationID"] = row3["LocationID"];
					dataRow5["ProductID"] = row3["ProductID"];
					dataRow5["Quantity"] = -1m * decimal.Parse(row3["Quantity"].ToString());
					dataRow5["Reference"] = dataRow["Reference"];
					if (result)
					{
						dataRow5["SysDocType"] = (byte)35;
					}
					else
					{
						dataRow5["SysDocType"] = (byte)37;
					}
					dataRow5["TransactionDate"] = dataRow["TransactionDate"];
					dataRow5["TransactionType"] = (byte)4;
					dataRow5["UnitPrice"] = row3["UnitPrice"];
					dataRow5["RowIndex"] = row3["RowIndex"];
					dataRow5["SpecificationID"] = row3["SpecificationID"];
					dataRow5["StyleID"] = row3["StyleID"];
					dataRow5["PayeeType"] = "V";
					dataRow5["PayeeID"] = dataRow["VendorID"];
					dataRow5["DivisionID"] = dataRow["DivisionID"];
					dataRow5["CompanyID"] = dataRow["CompanyID"];
					dataRow5.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow5);
				}
				inventoryTransactionData.Merge(purchaseReturnData.Tables["Product_Lot_Issue_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(purchaseReturnData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				string text6 = "";
				string text7 = "";
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (itemSourceTypes == ItemSourceTypes.PurchaseInvoice)
				{
					DataRow dataRow6 = purchaseReturnData.PurchaseReturnDetailTable.Rows[0];
					text7 = dataRow6["SourceVoucherID"].ToString();
					text6 = dataRow6["SourceSysDocID"].ToString();
					if (text7 != "" || text6 != "")
					{
						flag &= new PurchaseInvoice(base.DBConfig).UpdateRowReturnedQuantity(text6, text7, sqlTransaction);
					}
				}
				if (purchaseReturnData.Tables.Contains("Tax_Detail"))
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(purchaseReturnData, sysDocID, text2, isUpdate, sqlTransaction);
				}
				GLData journalData = CreateReturnGLData(purchaseReturnData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateInventoryTransactionRowID(sysDocID, text2, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Purchase_Return", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Purchase Return";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Purchase_Return", "VoucherID", sqlTransaction);
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
				string exp = "UPDATE PID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = PID.SysDocID AND IT.VoucherID = PID.VoucherID AND IT.RowIndex = PID.RowIndex) \r\n                                    FROM Purchase_Return_Detail PID INNER JOIN Purchase_Return PI ON PI.SysDocID = PID.SysDocID AND PI.VoucherID = PID.VoucherID\r\n                                     where PID.SysDocID = '" + sysDocID + "' and PID.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		private bool AdjustReturnedQuantity(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				bool flag = true;
				string text = "";
				string text2 = "";
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT TOP 1 SourceSysDocID,SourceVoucherID,PR.SourceDocType FROM Purchase_Return_Detail PRD INNER JOIN Purchase_Return PR ON PR.SysDocID  = PRD.SysDocID AND PR.VoucherID = PRD.VoucherID\r\n\t                    WHERE PRD.SysDocID = '" + sysDocID + "' AND PRD.VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Return", textCommand);
				if (dataSet != null && dataSet.Tables["Return"].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables[0].Rows[0];
					ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
					if (dataRow["SourceDocType"] != DBNull.Value)
					{
						itemSourceTypes = (ItemSourceTypes)int.Parse(dataRow["SourceDocType"].ToString());
					}
					if (itemSourceTypes == ItemSourceTypes.PurchaseInvoice)
					{
						text = dataRow["SourceVoucherID"].ToString();
						text2 = dataRow["SourceSysDocID"].ToString();
						if (text != "" || text2 != "")
						{
							flag &= new PurchaseInvoice(base.DBConfig).UpdateRowReturnedQuantity(text2, text, sqlTransaction);
						}
					}
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateReturnGLData(PurchaseReturnData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.PurchaseReturnTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string text = dataRow["VendorID"].ToString();
			string text2 = dataRow["SysDocID"].ToString();
			string voucherID = dataRow["VoucherID"].ToString();
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			bool result = false;
			if (!dataRow["PriceIncludeTax"].IsDBNullOrEmpty())
			{
				bool.TryParse(dataRow["PriceIncludeTax"].ToString(), out result);
			}
			bool flag = bool.Parse(dataRow["IsCash"].ToString());
			string text3 = new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", text2, sqlTransaction).ToString();
			string text4 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text3, sqlTransaction).ToString();
			string value3 = new Databases(base.DBConfig).GetFieldValue("Location", "DiscountGivenAccountID", "LocationID", text3, sqlTransaction).ToString();
			new Databases(base.DBConfig).GetFieldValue("Location", "PurchaseTaxAccountID", "LocationID", text3, sqlTransaction).ToString();
			string text5 = new Databases(base.DBConfig).GetFieldValue("Vendor", "APAccountID", "VendorID", text, sqlTransaction).ToString();
			if (text5 == "")
			{
				text5 = new Databases(base.DBConfig).GetFieldValue("Location", "APAccountID", "LocationID", text3, sqlTransaction).ToString();
			}
			string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
			bool flag2 = false;
			decimal result2 = 1m;
			if (dataRow["CurrencyID"] != DBNull.Value && baseCurrencyID != dataRow["CurrencyID"].ToString())
			{
				flag2 = true;
				decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result2);
			}
			string currencyID = dataRow["CurrencyID"].ToString();
			string currencyRateType = new Currencies(base.DBConfig).GetCurrencyRateType(currencyID);
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.CreditPurchaseReturn;
			if (flag)
			{
				sysDocTypes = SysDocTypes.CashPurchaseReturn;
			}
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["CurrencyID"] = dataRow["CurrencyID"];
			dataRow2["CurrencyRate"] = dataRow["CurrencyRate"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Purchase Return - ";
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			decimal num = default(decimal);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			Hashtable hashtable2 = new Hashtable();
			ArrayList arrayList2 = new ArrayList();
			decimal d = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			DataSet dataSet = new DataSet();
			foreach (DataRow row in transactionData.PurchaseReturnDetailTable.Rows)
			{
				decimal num5 = default(decimal);
				int rowIndex = int.Parse(row["RowIndex"].ToString());
				string warehouseLocationID = row["LocationID"].ToString();
				string text6 = row["ProductID"].ToString();
				dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(text6, text3, warehouseLocationID, text2, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("Product accounts information not found for product or location.");
				}
				DataRow dataRow4 = dataSet.Tables[0].Rows[0];
				string text7 = dataRow4["COGSAccountID"].ToString();
				string text8 = dataRow4["InventoryAssetAccountID"].ToString();
				ItemTypes itemTypes = ItemTypes.Inventory;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "ItemType", "ProductID", text6, sqlTransaction);
				if (fieldValue == null || !(fieldValue.ToString() != ""))
				{
					throw new CompanyException("Item type is not selected for the product:" + text6);
				}
				itemTypes = (ItemTypes)byte.Parse(fieldValue.ToString());
				if (row["UnitQuantity"] != DBNull.Value)
				{
					decimal.Parse(row["UnitQuantity"].ToString());
				}
				else
				{
					decimal.Parse(row["Quantity"].ToString());
				}
				decimal result3 = default(decimal);
				decimal result4 = default(decimal);
				decimal result5 = default(decimal);
				decimal result6 = default(decimal);
				decimal.TryParse(row["TaxAmount"].ToString(), out result5);
				if (flag2)
				{
					decimal.TryParse(row["UnitPriceFC"].ToString(), out result3);
				}
				else
				{
					decimal.TryParse(row["UnitPrice"].ToString(), out result3);
				}
				if (flag2)
				{
					decimal.TryParse(row["AmountFC"].ToString(), out result4);
				}
				else
				{
					decimal.TryParse(row["Amount"].ToString(), out result4);
				}
				decimal d2 = result4;
				if (flag2)
				{
					d2 = decimal.Parse(row["Amount"].ToString());
				}
				if (flag2)
				{
					decimal.TryParse(row["TaxAmountLC"].ToString(), out result6);
				}
				if (itemTypes != ItemTypes.Inventory)
				{
					num3 += Math.Round(result4, currencyDecimalPoints);
					num4 += Math.Round(d2, currencyDecimalPoints);
					string text9 = text7;
					if (hashtable.ContainsKey(text9))
					{
						num = decimal.Parse(hashtable[text9].ToString());
						if (result)
						{
							num += Math.Round(result4 - result5, currencyDecimalPoints);
						}
						else
						{
							num += Math.Round(result4, currencyDecimalPoints);
						}
						hashtable[text9] = num;
					}
					else
					{
						if (result)
						{
							hashtable.Add(text9, Math.Round(result4 - result5, currencyDecimalPoints));
						}
						else
						{
							hashtable.Add(text9, Math.Round(result4, currencyDecimalPoints));
						}
						arrayList.Add(text9);
						if (result)
						{
							d += result4 - result5;
						}
						else
						{
							d += result4;
						}
					}
				}
				else
				{
					if (itemTypes == ItemTypes.ConsignmentItem)
					{
						throw new CompanyException("Consignment items cannot be used in purchase return.");
					}
					decimal num6 = Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text6, text2, voucherID, rowIndex, mergeWithRefRows: false, sqlTransaction));
					if (flag2)
					{
						num6 = CommonLib.ConvertToFC(num6, currencyID, currencyRateType, result2);
					}
					string text9 = text8;
					if (hashtable2.ContainsKey(text9))
					{
						num = decimal.Parse(hashtable2[text9].ToString());
						num += Math.Round(num6, currencyDecimalPoints);
						hashtable2[text9] = num;
					}
					else
					{
						hashtable2.Add(text9, Math.Round(num6, currencyDecimalPoints));
						arrayList2.Add(text9);
					}
					num5 = ((!result) ? (result4 - num6) : (result4 - result5 - num6));
					text9 = text7;
					if (hashtable.ContainsKey(text9))
					{
						num = decimal.Parse(hashtable[text9].ToString());
						num += Math.Round(num5, currencyDecimalPoints);
						hashtable[text9] = num;
					}
					else
					{
						hashtable.Add(text9, Math.Round(num5, currencyDecimalPoints));
						arrayList.Add(text9);
					}
					d += num5;
					num2 += Math.Round(result4, currencyDecimalPoints);
					num3 += Math.Round(result4, currencyDecimalPoints);
					num4 += Math.Round(d2, currencyDecimalPoints);
				}
			}
			if (num3 != 0m)
			{
				for (int i = 0; i < hashtable2.Count; i++)
				{
					DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					string text9 = arrayList2[i].ToString();
					num = decimal.Parse(hashtable2[text9].ToString());
					text4 = arrayList2[i].ToString();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = text4;
					dataRow5["PayeeID"] = text;
					if (flag2)
					{
						dataRow5["CreditFC"] = num;
						dataRow5["DebitFC"] = DBNull.Value;
					}
					else
					{
						dataRow5["Credit"] = num;
						dataRow5["Debit"] = DBNull.Value;
					}
					dataRow5["JVEntryType"] = (byte)1;
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
			}
			if (d != 0m)
			{
				for (int j = 0; j < hashtable.Count; j++)
				{
					DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					string text9 = arrayList[j].ToString();
					num = decimal.Parse(hashtable[text9].ToString());
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = text9;
					dataRow5["PayeeID"] = text;
					if (num > 0m)
					{
						if (flag2)
						{
							dataRow5["DebitFC"] = DBNull.Value;
							dataRow5["CreditFC"] = num;
						}
						else
						{
							dataRow5["Debit"] = DBNull.Value;
							dataRow5["Credit"] = num;
						}
					}
					else if (flag2)
					{
						dataRow5["DebitFC"] = Math.Abs(num);
						dataRow5["CreditFC"] = DBNull.Value;
					}
					else
					{
						dataRow5["Debit"] = Math.Abs(num);
						dataRow5["Credit"] = DBNull.Value;
					}
					dataRow5["JVEntryType"] = (byte)2;
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
			}
			decimal result7 = default(decimal);
			decimal num7 = default(decimal);
			decimal result8 = default(decimal);
			decimal num8 = default(decimal);
			if (dataRow["DiscountFC"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["DiscountFC"].ToString(), out result7);
			}
			else
			{
				decimal.TryParse(dataRow["Discount"].ToString(), out result7);
			}
			num7 = result7;
			if (flag2)
			{
				decimal.TryParse(dataRow["Discount"].ToString(), out num7);
			}
			if (dataRow["TaxAmountFC"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["TaxAmountFC"].ToString(), out result8);
			}
			else
			{
				decimal.TryParse(dataRow["TaxAmount"].ToString(), out result8);
			}
			num8 = result8;
			if (flag2)
			{
				decimal.TryParse(dataRow["TaxAmount"].ToString(), out num8);
			}
			PayeeTaxOptions payeeTaxOptions = PayeeTaxOptions.NonTaxable;
			if (dataRow["TaxOption"] != DBNull.Value)
			{
				payeeTaxOptions = (PayeeTaxOptions)byte.Parse(dataRow["TaxOption"].ToString());
			}
			if (payeeTaxOptions == PayeeTaxOptions.ReverseCharge && result)
			{
				throw new CompanyException("Price inclusive of tax is not allowed for reverse charge calculation.");
			}
			if (result7 > 0m)
			{
				DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["AccountID"] = value3;
				dataRow5["PayeeID"] = text;
				dataRow5["PayeeType"] = "A";
				dataRow5["Debit"] = result7;
				dataRow5["Credit"] = DBNull.Value;
				if (flag2)
				{
					dataRow5["DebitFC"] = result7;
					dataRow5["CreditFC"] = DBNull.Value;
				}
				else
				{
					dataRow5["Debit"] = result7;
					dataRow5["Credit"] = DBNull.Value;
				}
				dataRow5["JVEntryType"] = (byte)5;
				dataRow5["Reference"] = dataRow["Reference"];
				dataRow5["CompanyID"] = value;
				dataRow5["DivisionID"] = value2;
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
			}
			if (result8 > 0m)
			{
				if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
				{
					throw new CompanyException("Tax details not found for the transaction.");
				}
				DataRow[] array = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
				decimal num9 = default(decimal);
				for (int k = 0; k < array.Length; k++)
				{
					num9 = default(decimal);
					DataRow obj = array[k];
					DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					string text10 = "";
					text10 = obj["TaxItemID"].ToString();
					string text11 = "";
					string exp = "SELECT PurchaseTaxAccountID FROM Tax WHERE  TaxCode = '" + text10.Trim() + "'";
					object obj2 = ExecuteScalar(exp);
					if (obj2 != null)
					{
						text11 = obj2.ToString();
					}
					if (text11 == "")
					{
						throw new CompanyException("AccountID is not set for tax item: " + text10 + ".");
					}
					decimal.TryParse(obj["TaxAmount"].ToString(), out num9);
					dataRow5["AccountID"] = text11;
					dataRow5["PayeeID"] = text;
					dataRow5["PayeeType"] = "A";
					if (flag2)
					{
						dataRow5["DebitFC"] = DBNull.Value;
						dataRow5["CreditFC"] = Math.Round(num9, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					}
					else
					{
						dataRow5["Debit"] = DBNull.Value;
						dataRow5["Credit"] = Math.Round(num9, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					}
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["JVEntryType"] = (byte)6;
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
			}
			if (flag)
			{
				DataRow dataRow6 = transactionData.PaymentTable.Rows[0];
				DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["AccountID"] = dataRow6["AccountID"].ToString();
				dataRow5["PayeeID"] = text;
				dataRow5["PayeeType"] = "A";
				dataRow5["IsARAP"] = false;
				dataRow5["Debit"] = num3;
				dataRow5["Credit"] = DBNull.Value;
				decimal num10 = num3 + result8;
				if ((payeeTaxOptions == PayeeTaxOptions.ReverseCharge) | result)
				{
					num10 = num3 - result7;
				}
				if (flag2)
				{
					dataRow5["DebitFC"] = num10;
					dataRow5["CreditFC"] = DBNull.Value;
				}
				else
				{
					dataRow5["Debit"] = num10;
					dataRow5["Credit"] = DBNull.Value;
				}
				dataRow5["Reference"] = dataRow["Reference"];
				dataRow5["CompanyID"] = value;
				dataRow5["DivisionID"] = value2;
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
			}
			else
			{
				DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["AccountID"] = text5;
				dataRow5["PayeeID"] = text;
				dataRow5["PayeeType"] = "V";
				dataRow5["IsARAP"] = true;
				decimal num11 = num3 - result7 + result8;
				if ((payeeTaxOptions == PayeeTaxOptions.ReverseCharge) | result)
				{
					num11 = num3 - result7;
				}
				if (flag2)
				{
					dataRow5["DebitFC"] = num11;
					dataRow5["CreditFC"] = DBNull.Value;
				}
				else
				{
					dataRow5["Debit"] = num11;
					dataRow5["Credit"] = DBNull.Value;
				}
				dataRow5["Reference"] = dataRow["Reference"];
				dataRow5["JVEntryType"] = (byte)8;
				dataRow5["CompanyID"] = value;
				dataRow5["DivisionID"] = value2;
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
			}
			return gLData;
		}

		public PurchaseReturnData GetPurchaseReturnByID(string sysDocID, string voucherID)
		{
			return GetPurchaseReturnByID(sysDocID, voucherID, null);
		}

		internal PurchaseReturnData GetPurchaseReturnByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				PurchaseReturnData purchaseReturnData = new PurchaseReturnData();
				string text = "SELECT * FROM Purchase_Return WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				new SqlCommand(text);
				FillDataSet(purchaseReturnData, "Purchase_Return", text, sqlTransaction);
				if (purchaseReturnData == null || purchaseReturnData.Tables.Count == 0 || purchaseReturnData.Tables["Purchase_Return"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT TD.*,Product.ItemType,Product.Description,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID,IsTrackLot,IsTrackSerial\r\n                        FROM Purchase_Return_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(purchaseReturnData, "Purchase_Return_Detail", text, sqlTransaction);
				text = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseReturnData, "Tax_Detail", text);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (purchaseReturnData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					purchaseReturnData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				purchaseReturnData.Merge(transactionIssuesProductLots, preserveChanges: false);
				return purchaseReturnData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInvoicesToReturn(string vendorID, bool cashOnly, DateTime fromDate, DateTime toDate)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(fromDate);
			string text2 = StoreConfiguration.ToSqlDateTimeString(toDate);
			string text3 = " SELECT DISTINCT 1 AS Type, PI.SysDocID [Doc ID], PI.VoucherID [Number], TransactionDate AS [Date], PI.VendorID + '-' + VEN.VendorName AS [Vendor]\r\n                            FROM Purchase_Invoice PI INNER JOIN Purchase_Invoice_Detail PID ON PI.SysDocID=PID.SysDocID AND PI.VoucherID=PID.VoucherID\r\n                            INNER JOIN Vendor VEN ON VEN.VendorID = PI.VendorID\r\n                            WHERE ISNULL(ISNULL(UnitQuantity,Quantity),0) - ISNULL(PID.QuantityReceived, 0) > 0  AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n                             AND ISNULL(IsVoid,'False')='False'";
			if (vendorID != "")
			{
				text3 = text3 + " AND PI.VendorID='" + vendorID + "' ";
			}
			if (cashOnly)
			{
				text3 += " AND ISNULL(IsCash,'False') = 'True' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Return", sqlCommand);
			return dataSet;
		}

		internal bool DeletePurchaseReturnDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				PurchaseReturnData purchaseReturnData = new PurchaseReturnData();
				string textCommand = "SELECT SOD.*,ISNULL(SO.IsCash,'False') AS [IsCash],ISNULL(ISVOID,'False') AS IsVoid FROM Purchase_Return_Detail SOD INNER JOIN Purchase_Return SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(purchaseReturnData, "Purchase_Return_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(purchaseReturnData.PurchaseReturnDetailTable.Rows[0]["IsCash"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(purchaseReturnData.PurchaseReturnDetailTable.Rows[0]["IsVoid"].ToString(), out result2);
				if (!result2)
				{
					flag = ((!result) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(37, sysDocID, voucherID, isDeletingTransaction, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(35, sysDocID, voucherID, isDeletingTransaction, sqlTransaction)));
				}
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				textCommand = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Purchase_Return_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				return flag & new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePaymentRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Invoice_Payment WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidPurchaseReturn(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidPurchaseReturn(sysDocID, voucherID, isVoid, null);
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

		private bool VoidPurchaseReturn(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				bool result = false;
				new PurchaseReturnData();
				string exp = "SELECT ISNULL(IsCash,'False') AS IsCash FROM Purchase_Return\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					bool.TryParse(obj.ToString(), out result);
				}
				flag = ((!result) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(37, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(35, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)));
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				exp = "UPDATE Purchase_Return SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag &= AdjustReturnedQuantity(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Purchase Return", voucherID, sysDocID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool DeletePurchaseReturn(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Purchase_Return", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidPurchaseReturn(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeletePurchaseReturnDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Purchase_Return WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Purchase Return", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetPurchaseReturnToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SI.*,Vendor.VendorName,VA.AddressPrintFormat AS VendorAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                ISNULL(DiscountFC,Discount) AS Discount,ISNULL(TotalFC,Total) - ISNULL(ISNULL(DiscountFC,Discount),0) AS GrandTotal,\r\n                                ISNULL(ISNULL(TaxAmountFC,TaxAmount) ,0) AS Tax,ISNULL(TotalFC,Total) AS Total,PONumber,SI.Note,Vendor.TaxIDNumber as VTaxIDNo\r\n                                FROM  Purchase_Return SI INNER JOIN Vendor ON SI.VendorID=Vendor.VendorID\r\n                                LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID \r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Purchase_Return", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Purchase_Return"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,PRD.ProductID,PRD.Description,ISNULL(UnitQuantity,PRD.Quantity) AS Quantity,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,\r\n                        ISNULL(UnitPriceFC,PRD.UnitPrice) AS UnitPrice,\r\n                        ISNULL(UnitQuantity,PRD.Quantity)*ISNULL(UnitPriceFC,PRD.UnitPrice) AS Total,PRD.UnitID,LocationID, PRD.TaxAmount, PRD.TaxGroupID, TG.TaxGroupName,PB.BrandName AS Brand,P.Description2,P.UPC\r\n                        FROM   Purchase_Return_Detail PRD\r\n                        INNER JOIN Product P ON P.ProductID = PRD.ProductID\r\n\t\t\t\t\t\tLEFT JOIN Tax_Group TG ON PRD.TaxGroupID=TG.TaxGroupID\r\n                        LEFT JOIN Product_Brand PB ON PB.BrandID=P.BrandID\r\n\t\t\t\t\t\t\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Purchase_Return_Detail", cmdText);
				dataSet.Relations.Add("PurchaseReturn", new DataColumn[2]
				{
					dataSet.Tables["Purchase_Return"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Return"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Purchase_Return_Detail"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Return_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Purchase_Return"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Purchase_Return"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Discount"].ToString(), out result2);
					decimal.TryParse(row["Tax"].ToString(), out result4);
					decimal.TryParse(row["GrandTotal"].ToString(), out result3);
					row["TotalInWords"] = NumToWord.GetNumInWords(decimalPoints: new CompanyInformations(base.DBConfig).CurrencyDecimalPoints, amount: result3 + result4);
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
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Return Date],\r\n                            CASE ISNULL(IsCash,'False') WHEN 'True' THEN 'Cash' ELSE 'Credit' END AS [Type],Total - ISNULL(Discount,0) AS [Amount], Reference, Reference2,INV.CurrencyID [Currency]\r\n                            FROM         Purchase_Return INV\r\n                            Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Return", sqlCommand);
			return dataSet;
		}
	}
}
