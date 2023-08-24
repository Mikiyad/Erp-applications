using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class SalesReturn : StoreObject
	{
		private const string SALESRETURN_TABLE = "Sales_Return";

		private const string SALESRETURNDETAIL_TABLE = "Sales_Return_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string SALESFLOW_PARM = "@SalesFlow";

		private const string ISCASH_PARM = "@IsCash";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string REPORTTO_PARM = "@ReportTo";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string STATUS_PARM = "@Status";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string PRICEINCLUDETAX_PARM = "@PriceIncludeTax";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

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

		private const string REASONID_PARM = "@ReasonID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string ROUNDOFF_PARM = "@RoundOff";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITPRICEFC_PARM = "@UnitPriceFC";

		private const string DESCRIPTION_PARM = "@Description";

		private const string JOBID_PARM = "@JobID";

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

		private const string SPECIFICATIONID_PARM = "@SpecificationID";

		private const string STYLEID_PARM = "@StyleID";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string TAXPERCENTAGE_PARM = "@TaxPercentage";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string AMOUNT_PARM = "@Amount";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string INVOICEPAYMENT_PARM = "@Invoice_Payment";

		public SalesReturn(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSalesReturnText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Return", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("SalesFlow", "@SalesFlow"), new FieldValue("IsCash", "@IsCash"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("ReportTo", "@ReportTo"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("Total", "@Total"), new FieldValue("RoundOff", "@RoundOff"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("ReasonID", "@ReasonID"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("Note", "@Note"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Sales_Return", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesReturnCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesReturnText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesReturnText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@IsCash", SqlDbType.Bit);
			parameters.Add("@SalesFlow", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@ReportTo", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@ReasonID", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@DiscountFC", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@TotalFC", SqlDbType.Decimal);
			parameters.Add("@RoundOff", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@SalesFlow"].SourceColumn = "SalesFlow";
			parameters["@IsCash"].SourceColumn = "IsCash";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@ReportTo"].SourceColumn = "ReportTo";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@PriceIncludeTax"].SourceColumn = "PriceIncludeTax";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@ReasonID"].SourceColumn = "ReasonID";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@DiscountFC"].SourceColumn = "DiscountFC";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@TotalFC"].SourceColumn = "TotalFC";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
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

		private string GetInsertUpdateSalesReturnDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Return_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SpecificationID", "@SpecificationID"), new FieldValue("StyleID", "@StyleID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxPercentage", "@TaxPercentage"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("DNoteSysDocID", "@DNoteSysDocID"), new FieldValue("DNoteVoucherID", "@DNoteVoucherID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesReturnDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesReturnDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesReturnDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@AmountFC", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SpecificationID", SqlDbType.NVarChar);
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@DNoteSysDocID", SqlDbType.NVarChar);
			parameters.Add("@DNoteVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@OrderRowIndex", SqlDbType.Int);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxPercentage", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@UnitPriceFC"].SourceColumn = "UnitPriceFC";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@SpecificationID"].SourceColumn = "SpecificationID";
			parameters["@StyleID"].SourceColumn = "StyleID";
			parameters["@DNoteSysDocID"].SourceColumn = "DNoteSysDocID";
			parameters["@DNoteVoucherID"].SourceColumn = "DNoteVoucherID";
			parameters["@OrderRowIndex"].SourceColumn = "OrderRowIndex";
			parameters["@OrderRowIndex"].SourceColumn = "OrderRowIndex";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxPercentage"].SourceColumn = "TaxPercentage";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
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

		private bool ValidateData(SalesReturnData journalData)
		{
			return true;
		}

		public bool InsertUpdateSalesReturn(SalesReturnData salesReturnData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateSalesReturnCommand = GetInsertUpdateSalesReturnCommand(isUpdate);
			string text = "";
			try
			{
				DataRow dataRow = salesReturnData.SalesReturnTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text2 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				bool result = false;
				string text3 = "";
				text3 = ((0 == 0) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				if (text3 != "")
				{
					int.Parse(text3.ToString());
				}
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				bool.TryParse(dataRow["IsCash"].ToString(), out result);
				decimal num = default(decimal);
				foreach (DataRow row in salesReturnData.SalesReturnDetailTable.Rows)
				{
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result3);
					decimal.TryParse(row["UnitPrice"].ToString(), out result4);
					decimal.TryParse(row["Amount"].ToString(), out result2);
					num += result2;
				}
				dataRow["Total"] = num;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Sales_Return", "VoucherID", dataRow["SysDocID"].ToString(), text2, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				bool flag2 = false;
				decimal result5 = 1m;
				string a = "M";
				if (dataRow["CurrencyID"] != DBNull.Value && baseCurrencyID != dataRow["CurrencyID"].ToString())
				{
					flag2 = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result5);
					a = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
				}
				if (flag2)
				{
					decimal result6 = default(decimal);
					dataRow["TotalFC"] = dataRow["Total"];
					decimal.TryParse(dataRow["Total"].ToString(), out result6);
					result6 = ((!(a == "M")) ? Math.Round(result6 / result5, 4) : Math.Round(result6 * result5, 4));
					dataRow["Total"] = result6;
					result6 = default(decimal);
					dataRow["DiscountFC"] = dataRow["Discount"];
					decimal.TryParse(dataRow["DiscountFC"].ToString(), out result6);
					result6 = ((!(a == "M")) ? Math.Round(result6 / result5, 4) : Math.Round(result6 * result5, 4));
					dataRow["Discount"] = result6;
					result6 = default(decimal);
					dataRow["TaxAmountFC"] = dataRow["TaxAmount"];
					decimal.TryParse(dataRow["TaxAmount"].ToString(), out result6);
					result6 = ((!(a == "M")) ? Math.Round(result6 / result5, 4) : Math.Round(result6 * result5, 4));
					dataRow["TaxAmount"] = result6;
				}
				foreach (DataRow row2 in salesReturnData.SalesReturnDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					text = row2["ProductID"].ToString();
					string text4 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text, sqlTransaction);
					if (fieldValue != null)
					{
						text4 = fieldValue.ToString();
					}
					if (text4 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text4)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text + "\nUnit:" + row2["UnitID"].ToString());
						float num2 = float.Parse(obj2["Factor"].ToString());
						string text5 = obj2["FactorType"].ToString();
						float num3 = float.Parse(row2["Quantity"].ToString());
						row2["UnitFactor"] = num2;
						row2["FactorType"] = text5;
						row2["UnitQuantity"] = row2["Quantity"];
						num3 = ((!(text5 == "M")) ? float.Parse(Math.Round(num3 * num2, 5).ToString()) : float.Parse(Math.Round(num3 / num2, 5).ToString()));
						row2["Quantity"] = num3;
					}
					if (flag2)
					{
						decimal result7 = default(decimal);
						decimal result8 = default(decimal);
						row2["UnitPriceFC"] = row2["UnitPrice"];
						row2["AmountFC"] = row2["Amount"];
						decimal.TryParse(row2["UnitPrice"].ToString(), out result7);
						decimal.TryParse(row2["Amount"].ToString(), out result8);
						result7 = ((!(a == "M")) ? Math.Round(result7 / result5, 4) : Math.Round(result7 * result5, 4));
						row2["UnitPrice"] = result7;
						result8 = ((!(a == "M")) ? Math.Round(result8 / result5, currencyDecimalPoints) : Math.Round(result8 * result5, currencyDecimalPoints));
						row2["Amount"] = result8;
					}
				}
				insertUpdateSalesReturnCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(salesReturnData, "Sales_Return", insertUpdateSalesReturnCommand)) : (flag & Insert(salesReturnData, "Sales_Return", insertUpdateSalesReturnCommand)));
				insertUpdateSalesReturnCommand = GetInsertUpdateSalesReturnDetailsCommand(isUpdate: false);
				insertUpdateSalesReturnCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteSalesReturnDetailsRows(sysDocID, text2, isDeletingTransaction: false, sqlTransaction);
				}
				if (salesReturnData.Tables["Sales_Return_Detail"].Rows.Count > 0)
				{
					flag &= Insert(salesReturnData, "Sales_Return_Detail", insertUpdateSalesReturnCommand);
				}
				insertUpdateSalesReturnCommand = GetInsertUpdatePaymentCommand(isUpdate: false);
				insertUpdateSalesReturnCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeletePaymentRows(sysDocID, text2, sqlTransaction);
				}
				if (result)
				{
					DataRow dataRow3 = salesReturnData.PaymentTable.Rows[0];
					PaymentMethodTypes paymentMethodTypes = (PaymentMethodTypes)byte.Parse(dataRow3["PaymentMethodType"].ToString());
					string registerID = dataRow3["RegisterID"].ToString();
					string text6 = "";
					text6 = ((paymentMethodTypes != PaymentMethodTypes.CreditCard) ? new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID") : new Register(base.DBConfig).GetRegisterAccountID(registerID, "CardReceivedAccountID"));
					if (text6 == "")
					{
						throw new CompanyException("One or more register accounts are not set.", 1034);
					}
					dataRow3["AccountID"] = text6;
					dataRow3.EndEdit();
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row3 in salesReturnData.SalesReturnDetailTable.Rows)
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
					dataRow5["Quantity"] = decimal.Parse(row3["Quantity"].ToString());
					dataRow5["Reference"] = dataRow["Reference"];
					if (result)
					{
						dataRow5["SysDocType"] = (byte)28;
					}
					else
					{
						dataRow5["SysDocType"] = (byte)27;
					}
					dataRow5["TransactionDate"] = dataRow["TransactionDate"];
					dataRow5["TransactionType"] = (byte)3;
					dataRow5["UnitPrice"] = row3["UnitPrice"];
					dataRow5["RowIndex"] = row3["RowIndex"];
					dataRow5["SpecificationID"] = row3["SpecificationID"];
					dataRow5["StyleID"] = row3["StyleID"];
					dataRow5["PayeeType"] = "C";
					dataRow5["PayeeID"] = dataRow["CustomerID"];
					dataRow5["DivisionID"] = dataRow["DivisionID"];
					dataRow5["CompanyID"] = dataRow["CompanyID"];
					if (row3["UnitQuantity"] != DBNull.Value && row3["UnitFactor"] != DBNull.Value)
					{
						dataRow5["UnitQuantity"] = row3["UnitQuantity"];
						dataRow5["Factor"] = row3["UnitFactor"];
						dataRow5["FactorType"] = row3["FactorType"];
						decimal.Parse(row3["UnitFactor"].ToString());
						row3["FactorType"].ToString();
						decimal d = decimal.Parse(row3["UnitQuantity"].ToString());
						decimal num4 = decimal.Parse(row3["Quantity"].ToString());
						decimal d2 = decimal.Parse(row3["UnitPrice"].ToString());
						decimal num5 = default(decimal);
						num5 = ((!(num4 != 0m)) ? default(decimal) : (d * d2 / num4));
						dataRow5["UnitPrice"] = num5;
					}
					dataRow5.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow5);
				}
				inventoryTransactionData.Merge(salesReturnData.Tables["Product_Lot_Receiving_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(salesReturnData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				string text7 = "";
				string text8 = "";
				int num6 = -1;
				if (itemSourceTypes != 0)
				{
					foreach (DataRow row4 in salesReturnData.SalesReturnDetailTable.Rows)
					{
						text = row4["ProductID"].ToString();
						text8 = row4["SourceVoucherID"].ToString();
						text7 = row4["SourceSysDocID"].ToString();
						num6 = 0;
						if (!(text8 == "") && !(text7 == ""))
						{
							int.TryParse(row4["SourceRowIndex"].ToString(), out num6);
							float result9 = 0f;
							if (row4["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row4["UnitQuantity"].ToString(), out result9);
							}
							else
							{
								float.TryParse(row4["Quantity"].ToString(), out result9);
							}
							if (itemSourceTypes == ItemSourceTypes.SalesInvoice)
							{
								flag &= new SalesInvoice(base.DBConfig).UpdateRowReturnedQuantity(text7, text8, num6, result9, sqlTransaction);
							}
						}
					}
				}
				if (salesReturnData.Tables.Contains("Tax_Detail") && salesReturnData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(salesReturnData, sysDocID, text2, isUpdate, sqlTransaction);
				}
				if (itemSourceTypes == ItemSourceTypes.None || itemSourceTypes == ItemSourceTypes.SalesInvoice)
				{
					GLData journalData = CreateReturnGLData(salesReturnData, sqlTransaction);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				}
				flag &= UpdateInventoryTransactionRowID(sysDocID, text2, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Sales_Return", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Sales Return";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Sales_Return", "VoucherID", sqlTransaction);
				}
				if (flag)
				{
					flag = ((!result) ? (flag & new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.CreditSalesReturn, sysDocID, text2, "Sales_Return", sqlTransaction)) : (flag & new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.CashSalesReturn, sysDocID, text2, "Sales_Return", sqlTransaction)));
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

		private GLData CreateReturnGLData(SalesReturnData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.SalesReturnTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string text = dataRow["CustomerID"].ToString();
			string voucherID = dataRow["VoucherID"].ToString();
			string text2 = dataRow["SysDocID"].ToString();
			bool flag = bool.Parse(dataRow["IsCash"].ToString());
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			bool result = false;
			if (!dataRow["PriceIncludeTax"].IsDBNullOrEmpty())
			{
				bool.TryParse(dataRow["PriceIncludeTax"].ToString(), out result);
			}
			string text3 = "";
			if (transactionData != null && transactionData.SalesReturnDetailTable.Rows.Count > 0 && transactionData.SalesReturnDetailTable.Rows[0]["SourceVoucherID"] != DBNull.Value)
			{
				text3 = "Ref Inv:" + transactionData.SalesReturnDetailTable.Rows[0]["SourceVoucherID"].ToString();
			}
			string textCommand = "SELECT SD.LocationID,ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID,ISNULL(SD.COGSAccountID,LOC.COGSAccountID) AS COGSAccountID,\r\n                                ISNULL(SD.DiscountGivenAccountID,LOC.DiscountGivenAccountID) AS DiscountGivenAccountID,Loc.ConsignInAccountID,\r\n                                LOC.InventoryAccountID,ISNULL(LOC.SalesAccountID,SD.SalesAccountID) AS SalesAccountID,ISNULL(SD.SalesTaxAccountID,LOC.SalesTaxAccountID) AS SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID,LOC.RoundOffAccountID AS RoundOffAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
			if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
			{
				throw new CompanyException("There is no location assigned to this system document or location record is missing.");
			}
			DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
			string text4 = dataRow2["LocationID"].ToString();
			string text5 = "";
			string value3 = dataRow2["DiscountGivenAccountID"].ToString();
			dataRow2["SalesTaxAccountID"].ToString();
			string value4 = dataRow2["ARAccountID"].ToString();
			string value5 = dataRow2["RoundOffAccountID"].ToString();
			string a = dataRow2["BaseCurrencyID"].ToString();
			bool flag2 = false;
			decimal result2 = 1m;
			if (dataRow["CurrencyID"] != DBNull.Value && a != dataRow["CurrencyID"].ToString())
			{
				flag2 = true;
				decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result2);
			}
			decimal result3 = default(decimal);
			if (dataRow["RoundOff"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["RoundOff"].ToString(), out result3);
			}
			DataRow dataRow3 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.CreditSalesReturn;
			if (flag)
			{
				sysDocTypes = SysDocTypes.CashSalesReturn;
			}
			dataRow3["JournalID"] = 0;
			dataRow3["JournalDate"] = dataRow["TransactionDate"];
			dataRow3["SysDocID"] = dataRow["SysDocID"];
			dataRow3["SysDocType"] = (byte)sysDocTypes;
			dataRow3["VoucherID"] = dataRow["VoucherID"];
			dataRow3["CurrencyID"] = dataRow["CurrencyID"];
			dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["Narration"] = text3;
			dataRow3.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow3);
			decimal num = default(decimal);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			Hashtable hashtable2 = new Hashtable();
			ArrayList arrayList2 = new ArrayList();
			Hashtable hashtable3 = new Hashtable();
			ArrayList arrayList3 = new ArrayList();
			decimal d = default(decimal);
			decimal d2 = default(decimal);
			foreach (DataRow row in transactionData.SalesReturnDetailTable.Rows)
			{
				string text6 = row["ProductID"].ToString();
				string warehouseLocationID = row["LocationID"].ToString();
				int rowIndex = int.Parse(row["RowIndex"].ToString());
				dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(text6, text4, warehouseLocationID, text2, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("Product accounts information not found for product or location.");
				}
				DataRow dataRow5 = dataSet.Tables[0].Rows[0];
				string text7 = dataRow5["IncomeAccountID"].ToString();
				string text8 = dataRow5["ConsignInAccountID"].ToString();
				string text9 = dataRow5["COGSAccountID"].ToString();
				string text10 = dataRow5["InventoryAssetAccountID"].ToString();
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
				decimal d3 = default(decimal);
				if (itemTypes != ItemTypes.ConsignmentItem)
				{
					new Products(base.DBConfig).GetProductCurrentCost(text6, sqlTransaction);
					d3 = Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text6, text2, voucherID, rowIndex, mergeWithRefRows: false, sqlTransaction));
				}
				decimal result4 = default(decimal);
				if (flag2)
				{
					decimal.TryParse(row["UnitPriceFC"].ToString(), out result4);
				}
				else
				{
					decimal.TryParse(row["UnitPrice"].ToString(), out result4);
				}
				decimal result5 = default(decimal);
				decimal result6 = default(decimal);
				decimal.TryParse(row["TaxAmount"].ToString(), out result6);
				if (flag2)
				{
					decimal.TryParse(row["AmountFC"].ToString(), out result5);
				}
				else
				{
					decimal.TryParse(row["Amount"].ToString(), out result5);
				}
				string text11;
				if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.Assembly)
				{
					text11 = text9;
					if (hashtable.ContainsKey(text11))
					{
						num = decimal.Parse(hashtable[text11].ToString());
						num += Math.Round(d3, currencyDecimalPoints);
						hashtable[text11] = num;
					}
					else
					{
						hashtable.Add(text11, Math.Round(d3, currencyDecimalPoints));
						arrayList.Add(text11);
					}
					text11 = text10;
					if (hashtable3.ContainsKey(text11))
					{
						num = decimal.Parse(hashtable3[text11].ToString());
						num += Math.Round(d3, currencyDecimalPoints);
						hashtable3[text11] = num;
					}
					else
					{
						hashtable3.Add(text11, Math.Round(d3, currencyDecimalPoints));
						arrayList3.Add(text11);
					}
				}
				text11 = ((itemTypes != ItemTypes.ConsignmentItem) ? text7 : text8);
				if (hashtable2.ContainsKey(text11))
				{
					num = decimal.Parse(hashtable2[text11].ToString());
					if (result)
					{
						num += Math.Round(result5 - result6, currencyDecimalPoints);
					}
					else
					{
						num += Math.Round(result5, currencyDecimalPoints);
					}
					hashtable2[text11] = num;
				}
				else
				{
					num = ((!result) ? Math.Round(result5, currencyDecimalPoints) : Math.Round(result5 - result6, currencyDecimalPoints));
					hashtable2.Add(text11, Math.Round(num, currencyDecimalPoints));
					arrayList2.Add(text11);
				}
				d += Math.Round(d3, currencyDecimalPoints);
				d2 += Math.Round(result5, currencyDecimalPoints);
			}
			decimal result7 = default(decimal);
			decimal result8 = default(decimal);
			if (dataRow["DiscountFC"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["DiscountFC"].ToString(), out result7);
			}
			else
			{
				decimal.TryParse(dataRow["Discount"].ToString(), out result7);
			}
			if (dataRow["TaxAmountFC"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["TaxAmountFC"].ToString(), out result8);
			}
			else
			{
				decimal.TryParse(dataRow["TaxAmount"].ToString(), out result8);
			}
			if (d != 0m)
			{
				for (int i = 0; i < hashtable3.Count; i++)
				{
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					string text11 = arrayList3[i].ToString();
					num = decimal.Parse(hashtable3[text11].ToString());
					text5 = arrayList3[i].ToString();
					dataRow6["JournalID"] = 0;
					dataRow6["AccountID"] = text5;
					dataRow6["PayeeID"] = text;
					dataRow6["Debit"] = num;
					dataRow6["Credit"] = DBNull.Value;
					dataRow6["IsBaseOnly"] = true;
					dataRow6["Reference"] = dataRow["Reference"];
					dataRow6["JVEntryType"] = (byte)1;
					dataRow6["CompanyID"] = value;
					dataRow6["DivisionID"] = value2;
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
				}
			}
			if (d != 0m)
			{
				for (int j = 0; j < hashtable.Count; j++)
				{
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					string text11 = arrayList[j].ToString();
					num = decimal.Parse(hashtable[text11].ToString());
					dataRow6["JournalID"] = 0;
					dataRow6["AccountID"] = text11;
					dataRow6["PayeeID"] = text;
					dataRow6["Debit"] = DBNull.Value;
					dataRow6["Credit"] = num;
					dataRow6["IsBaseOnly"] = true;
					dataRow6["JVEntryType"] = (byte)2;
					dataRow6["Reference"] = dataRow["Reference"];
					dataRow6["CompanyID"] = value;
					dataRow6["DivisionID"] = value2;
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
				}
			}
			for (int k = 0; k < hashtable2.Count; k++)
			{
				DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
				dataRow6.BeginEdit();
				new Databases(base.DBConfig).GetFieldValue("Location", "SalesAccountID", "LocationID", text4, sqlTransaction).ToString();
				string text11 = arrayList2[k].ToString();
				num = decimal.Parse(hashtable2[text11].ToString());
				dataRow6["JournalID"] = 0;
				dataRow6["AccountID"] = text11;
				dataRow6["PayeeID"] = text;
				if (flag2)
				{
					if (num > 0m)
					{
						dataRow6["DebitFC"] = num;
						dataRow6["CreditFC"] = DBNull.Value;
					}
					else
					{
						dataRow6["DebitFC"] = DBNull.Value;
						dataRow6["CreditFC"] = Math.Abs(num);
					}
				}
				else if (num > 0m)
				{
					dataRow6["Debit"] = num;
					dataRow6["Credit"] = DBNull.Value;
				}
				else
				{
					dataRow6["Debit"] = DBNull.Value;
					dataRow6["Credit"] = Math.Abs(num);
				}
				dataRow6["Reference"] = dataRow["Reference"];
				dataRow6["JVEntryType"] = (byte)3;
				dataRow6["CompanyID"] = value;
				dataRow6["DivisionID"] = value2;
				dataRow6.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow6);
			}
			if (result7 > 0m)
			{
				DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
				dataRow6.BeginEdit();
				dataRow6["JournalID"] = 0;
				dataRow6["AccountID"] = value3;
				dataRow6["PayeeID"] = text;
				dataRow6["PayeeType"] = "A";
				if (flag2)
				{
					dataRow6["DebitFC"] = DBNull.Value;
					dataRow6["CreditFC"] = result7;
				}
				else
				{
					dataRow6["Debit"] = DBNull.Value;
					dataRow6["Credit"] = result7;
				}
				dataRow6["Reference"] = dataRow["Reference"];
				dataRow6["JVEntryType"] = (byte)5;
				dataRow6["CompanyID"] = value;
				dataRow6["DivisionID"] = value2;
				dataRow6.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow6);
			}
			if (result3 != 0m)
			{
				DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
				dataRow6.BeginEdit();
				dataRow6["JournalID"] = 0;
				dataRow6["AccountID"] = value5;
				dataRow6["PayeeID"] = text;
				dataRow6["PayeeType"] = "A";
				if (result3 < 0m)
				{
					dataRow6["Credit"] = Math.Abs(result3);
					dataRow6["Debit"] = DBNull.Value;
				}
				else
				{
					dataRow6["Debit"] = result3;
					dataRow6["Credit"] = DBNull.Value;
				}
				dataRow6["Reference"] = dataRow["Reference"];
				dataRow6["JVEntryType"] = (byte)10;
				dataRow6["CompanyID"] = value;
				dataRow6["DivisionID"] = value2;
				dataRow6.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow6);
			}
			if (result8 > 0m)
			{
				if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
				{
					throw new CompanyException("Tax details not found for the transaction.");
				}
				DataRow[] array = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
				decimal num2 = default(decimal);
				for (int l = 0; l < array.Length; l++)
				{
					num2 = default(decimal);
					DataRow obj = array[l];
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					dataRow6["JournalID"] = 0;
					string text12 = "";
					text12 = obj["TaxItemID"].ToString();
					string text13 = "";
					textCommand = "SELECT SalesTaxAccountID FROM Tax WHERE  TaxCode = '" + text12.Trim() + "'";
					object obj2 = ExecuteScalar(textCommand);
					if (obj2 != null)
					{
						text13 = obj2.ToString();
					}
					if (text13 == "")
					{
						throw new CompanyException("AccountID is not set for tax item: " + text12 + ".");
					}
					decimal.TryParse(obj["TaxAmount"].ToString(), out num2);
					dataRow6["AccountID"] = text13;
					dataRow6["PayeeID"] = text;
					dataRow6["PayeeType"] = "A";
					dataRow6["JobID"] = "";
					if (flag2)
					{
						dataRow6["DebitFC"] = Math.Round(num2, currencyDecimalPoints, MidpointRounding.AwayFromZero);
						dataRow6["CreditFC"] = DBNull.Value;
					}
					else
					{
						dataRow6["Debit"] = Math.Round(num2, currencyDecimalPoints, MidpointRounding.AwayFromZero);
						dataRow6["Credit"] = DBNull.Value;
					}
					dataRow6["Reference"] = dataRow["Reference"];
					dataRow6["JVEntryType"] = (byte)6;
					dataRow6["CompanyID"] = value;
					dataRow6["DivisionID"] = value2;
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
				}
			}
			if (flag)
			{
				DataRow dataRow7 = transactionData.PaymentTable.Rows[0];
				DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
				dataRow6.BeginEdit();
				dataRow6["JournalID"] = 0;
				dataRow6["AccountID"] = dataRow7["AccountID"].ToString();
				dataRow6["PayeeID"] = text;
				dataRow6["PayeeType"] = "A";
				dataRow6["IsARAP"] = false;
				if (flag2)
				{
					dataRow6["DebitFC"] = DBNull.Value;
					dataRow6["CreditFC"] = dataRow7["Amount"];
				}
				else
				{
					dataRow6["Debit"] = DBNull.Value;
					dataRow6["Credit"] = dataRow7["Amount"];
				}
				dataRow6["Reference"] = dataRow["Reference"];
				dataRow6["CompanyID"] = value;
				dataRow6["DivisionID"] = value2;
				dataRow6.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow6);
			}
			else
			{
				DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
				dataRow6.BeginEdit();
				dataRow6["JournalID"] = 0;
				dataRow6["AccountID"] = value4;
				dataRow6["PayeeID"] = text;
				dataRow6["PayeeType"] = "C";
				dataRow6["IsARAP"] = true;
				if (flag2)
				{
					dataRow6["DebitFC"] = DBNull.Value;
					if (result)
					{
						dataRow6["CreditFC"] = d2 - result7 + result3;
					}
					else
					{
						dataRow6["CreditFC"] = d2 - result7 + result8 + result3;
					}
				}
				else
				{
					dataRow6["Debit"] = DBNull.Value;
					if (result)
					{
						dataRow6["Credit"] = d2 - result7 + result3;
					}
					else
					{
						dataRow6["Credit"] = d2 - result7 + result8 + result3;
					}
				}
				dataRow6["Reference"] = dataRow["Reference"];
				dataRow6["JVEntryType"] = (byte)7;
				dataRow6["CompanyID"] = value;
				dataRow6["DivisionID"] = value2;
				if (text3 != "")
				{
					dataRow6["Description"] = text3;
				}
				dataRow6.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow6);
			}
			return gLData;
		}

		private bool UpdateInventoryTransactionRowID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Sales_Return_Detail SID INNER JOIN Sales_Return SI ON SI.SysDocID = SID.SysDocID AND SI.VoucherID = SID.VoucherID\r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		public SalesReturnData GetSalesReturnByID(string sysDocID, string voucherID)
		{
			return GetSalesReturnByID(sysDocID, voucherID, null);
		}

		internal SalesReturnData GetSalesReturnByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				SalesReturnData salesReturnData = new SalesReturnData();
				string textCommand = "SELECT * FROM Sales_Return WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesReturnData, "Sales_Return", textCommand, sqlTransaction);
				if (salesReturnData == null || salesReturnData.Tables.Count == 0 || salesReturnData.Tables["Sales_Return"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID,Product.ItemType,\r\n                        CASE WHEN Product.ItemType = 5 THEN 'True' ELSE IsTrackLot END  AS IsTrackLot,IsTrackSerial,ISNull(Product.TaxGroupID,PC.TaxGroupID) AS TaxGroupID\r\n                        FROM Sales_Return_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                         LEFT OUTER JOIN Product_Class PC ON PC.ClassID = Product.ClassID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(salesReturnData, "Sales_Return_Detail", textCommand, sqlTransaction);
				textCommand = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesReturnData, "Product_Lot_Receiving_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesReturnData, "Tax_Detail", textCommand);
				return salesReturnData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteSalesReturnDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				SalesReturnData salesReturnData = new SalesReturnData();
				string textCommand = "SELECT SO.SourceDocType AS SourceDocType, SOD.*,ISNULL(SO.IsCash,'False') AS [IsCash],ISNULL(ISVOID,'False') AS IsVoid FROM Sales_Return_Detail SOD INNER JOIN Sales_Return SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(salesReturnData, "Sales_Return_Detail", textCommand, sqlTransaction);
				DataRow dataRow = salesReturnData.SalesReturnDetailTable.Rows[0];
				bool result = false;
				bool.TryParse(salesReturnData.SalesReturnDetailTable.Rows[0]["IsCash"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(dataRow["IsVoid"].ToString(), out result2);
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (!result2)
				{
					flag = ((!result) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(27, sysDocID, voucherID, isDeletingTransaction, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(28, sysDocID, voucherID, isDeletingTransaction, sqlTransaction)));
				}
				textCommand = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Sales_Return_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				return flag & new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool AdjustReturnedQuantity(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			string text = "";
			string text2 = "";
			bool flag = true;
			SalesReturnData salesReturnByID = GetSalesReturnByID(sysDocID, voucherID, sqlTransaction);
			ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
			DataRow dataRow = null;
			bool flag2 = false;
			if (salesReturnByID != null && salesReturnByID.Tables["Sales_Return_Detail"].Rows.Count > 0)
			{
				dataRow = salesReturnByID.Tables["Sales_Return"].Rows[0];
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (dataRow["IsVoid"] != DBNull.Value)
				{
					flag2 = bool.Parse(dataRow["IsVoid"].ToString());
				}
			}
			if (itemSourceTypes == ItemSourceTypes.SalesInvoice && !flag2)
			{
				foreach (DataRow row in salesReturnByID.SalesReturnDetailTable.Rows)
				{
					row["ProductID"].ToString();
					text = row["SourceVoucherID"].ToString();
					text2 = row["SourceSysDocID"].ToString();
					int result = 0;
					if (!(text == "") && !(text2 == ""))
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
						flag &= new SalesInvoice(base.DBConfig).UpdateRowReturnedQuantity(text2, text, result, -1f * result2, sqlTransaction);
					}
				}
				return flag;
			}
			return flag;
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

		public bool CanEdit(string sysDocID, string voucherID)
		{
			return CanEdit(sysDocID, voucherID, null);
		}

		public bool CanEdit(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			if (!new InventoryTransaction(base.DBConfig).AllowDeleteInventoryTransaction(sysDocID, voucherID, sqlTransaction))
			{
				return false;
			}
			return true;
		}

		public bool VoidSalesReturn(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				if (!CanEdit(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("This transaction cannot be modifed because some of items are refered by other transactions.");
				}
				new SalesReturnData();
				string exp = "SELECT ISNULL(IsCash,'False') AS IsCash FROM Sales_Return\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					bool.TryParse(obj.ToString(), out result);
				}
				flag &= AdjustReturnedQuantity(sysDocID, voucherID, sqlTransaction);
				flag = ((!result) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(27, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(28, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)));
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				exp = "UPDATE Sales_Return SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				exp = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(exp, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Sales Return", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteSalesReturn(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= AdjustReturnedQuantity(sysDocID, voucherID, sqlTransaction);
				flag &= DeleteSalesReturnDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Sales_Return WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Sales Return", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetSalesReturnToPrint(string sysDocID, string voucherID)
		{
			return GetSalesReturnToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetSalesReturnToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.CustomerID,Customer.TaxIDNumber,CustomerName,CustomerAddress,TransactionDate,\r\n                                IsCash,SI.SalesPersonID,RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                IsVoid,Reference,Reference2, ISNULL(DiscountFC,Discount) AS Discount,ISNULL(ISNULL(TotalFC,Total),0) - ISNULL(ISNULL(DiscountFC,Discount),0) AS GrandTotal,\r\n                                ISNULL(ISNULL(TaxAmountFC,TaxAmount) ,0) AS Tax,ISNULL(RoundOff,0) as RoundOff,ISNULL(TotalFC,Total) AS Total,PONumber,SI.Note,GL.[GenericListName] as [Reason],Customer.ShortName\r\n                                FROM  Sales_Return SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT OUTER JOIN  Generic_List GL On GL.GenericListId=SI.ReasonId\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Sales_Return", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Sales_Return"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,SRD.ProductID,SRD.Description,ISNULL(UnitQuantity,SRD.Quantity) AS Quantity,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,SRD.TaxAmount, SRD.TaxGroupID,TG.TaxGroupName,\r\n                        ISNULL(UnitPriceFC,SRD.UnitPrice) AS UnitPrice,\r\n                        ISNULL(UnitQuantity,SRD.Quantity)*ISNULL(UnitPriceFC,SRD.UnitPrice) AS Total, SRD.UnitID,LocationID,PB.BrandName AS Brand,P.Description2,P.UPC, RowIndex, PS.SpecificationName\r\n                        FROM   Sales_Return_Detail SRD\r\n                        INNER JOIN Product P ON P.ProductID = SRD.ProductID\r\n\t\t\t\t\t\tLEFT JOIN Tax_Group TG ON SRD.TaxGroupID=TG.TaxGroupID\r\n\t\t\t\t\t\tLEFT JOIN Product_Brand PB ON PB.BrandID=P.BrandID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Specification PS ON SRD.SpecificationID=PS.SpecificationID\r\n\t\t\t\t\t\t\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") AND SRD.Quantity > 0  ORDER BY RowIndex";
				FillDataSet(dataSet, "Sales_Return_Detail", cmdText);
				dataSet.Relations.Add("CustomerReturn", new DataColumn[2]
				{
					dataSet.Tables["Sales_Return"].Columns["SysDocID"],
					dataSet.Tables["Sales_Return"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Sales_Return_Detail"].Columns["SysDocID"],
					dataSet.Tables["Sales_Return_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Sales_Return"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Sales_Return"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Discount"].ToString(), out result2);
					decimal.TryParse(row["Tax"].ToString(), out result3);
					decimal.TryParse(row["RoundOff"].ToString(), out result4);
					row["TotalInWords"] = NumToWord.GetNumInWords(decimalPoints: new CompanyInformations(base.DBConfig).CurrencyDecimalPoints, amount: result - result2 + result3 + result4);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInvoicesToReturn(string customerID, bool cashOnly, DateTime fromDate, DateTime toDate, int backdaysAllowed)
		{
			return GetInvoicesToReturn("", customerID, cashOnly, fromDate, toDate, backdaysAllowed);
		}

		public DataSet GetInvoicesToReturn(string returnSysDocID, string customerID, bool cashOnly, DateTime fromDate, DateTime toDate, int backdaysAllowed)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			if (returnSysDocID != "")
			{
				DataSet entityLinks = new SystemDocuments(base.DBConfig).GetEntityLinks(returnSysDocID, SysDocEntityTypes.CustomerClass);
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
			string text2 = StoreConfiguration.ToSqlDateTimeString(fromDate);
			string text3 = StoreConfiguration.ToSqlDateTimeString(toDate);
			string text4 = "  SELECT DISTINCT 1 AS Type,SI.SysDocID [Doc ID],SI.VoucherID [Number],TransactionDate AS [Date],SI.CustomerID + '-' + CUS.CustomerName AS [Customer], J.JobName\r\n                            FROM Sales_Invoice SI INNER JOIN Sales_Invoice_Detail SID ON SI.SysDocID=SID.SysDocID AND SI.VoucherID=SID.VoucherID\r\n                            INNER JOIN Customer CUS ON Cus.CustomerID = SI.CustomerID\r\n\r\nLEFT OUTER JOIN Job J ON J.JobID=SI.JobID  WHERE ISNULL(ISNULL(UnitQuantity,Quantity),0) - ISNULL(SID.QuantityReturned,0) >0 AND TransactionDate BETWEEN '" + text2 + "' AND '" + text3 + "'\r\n                             AND ISNULL(IsVoid,'False')='False' ";
			if (customerID != "")
			{
				text4 = text4 + " AND SI.CustomerID='" + customerID + "' ";
			}
			if (!string.IsNullOrEmpty(text))
			{
				text4 = text4 + " AND CUS.CustomerClassID IN (" + text + ") ";
			}
			if (cashOnly)
			{
				text4 += " AND ISNULL(IsCash,'False') = 'True' ";
			}
			if (backdaysAllowed != 0)
			{
				text4 = text4 + " AND DATEDIFF(day,TransactionDate,Getdate())<= " + backdaysAllowed;
			}
			SqlCommand sqlCommand = new SqlCommand(text4);
			FillDataSet(dataSet, "Sales_Return", sqlCommand);
			return dataSet;
		}

		public DataSet GetInvoicesToReturn(string returnSysDocID, string customerID, bool cashOnly, DateTime fromDate, DateTime toDate, int backdaysAllowed, string locationID)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			if (returnSysDocID != "")
			{
				DataSet entityLinks = new SystemDocuments(base.DBConfig).GetEntityLinks(returnSysDocID, SysDocEntityTypes.CustomerClass);
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
			string text2 = StoreConfiguration.ToSqlDateTimeString(fromDate);
			string text3 = StoreConfiguration.ToSqlDateTimeString(toDate);
			string text4 = " SELECT DISTINCT 1 AS Type,SI.SysDocID [Doc ID],SI.VoucherID [Number],TransactionDate AS [Date],SI.CustomerID + '-' + CUS.CustomerName AS [Customer]\r\n                            FROM Sales_Invoice SI \r\n                             INNER JOIN System_Document SD ON SD.SysDocID=SI.SysDocID  \r\n                            INNER JOIN Sales_Invoice_Detail SID ON SI.SysDocID=SID.SysDocID AND SI.VoucherID=SID.VoucherID\r\n                            INNER JOIN Customer CUS ON Cus.CustomerID = SI.CustomerID\r\n                            WHERE ISNULL(ISNULL(UnitQuantity,Quantity),0) - ISNULL(SID.QuantityReturned,0) >0 AND TransactionDate BETWEEN '" + text2 + "' AND '" + text3 + "'\r\n                             AND ISNULL(IsVoid,'False')='False'";
			if (customerID != "")
			{
				text4 = text4 + " AND SI.CustomerID='" + customerID + "' ";
			}
			if (!string.IsNullOrEmpty(text))
			{
				text4 = text4 + " AND CUS.CustomerClassID IN (" + text + ") ";
			}
			if (!string.IsNullOrEmpty(locationID))
			{
				text4 = text4 + " AND  SD.LocationID ='" + locationID + "' ";
			}
			if (cashOnly)
			{
				text4 += " AND ISNULL(IsCash,'False') = 'True' ";
			}
			if (backdaysAllowed != 0)
			{
				text4 = text4 + " AND DATEDIFF(day,TransactionDate,Getdate())<= " + backdaysAllowed;
			}
			SqlCommand sqlCommand = new SqlCommand(text4);
			FillDataSet(dataSet, "Sales_Return", sqlCommand);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT    ISNULL(IsVoid,'False') AS V, SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Invoice Date],\r\n                            CASE ISNULL(IsCash,'False') WHEN 'True' THEN 'Cash' ELSE 'Credit' END AS [Type],INV.SalespersonID [Salesperson],INV.CurrencyID,J.JobID,J.JobName,Total - ISNULL(Discount,0) AS [Amount], INV.Reference, INV.Reference2,ISNULL((CASE INV.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge'  END) ,(CASE Customer.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' END))AS TAXOPTION, INV.TaxAmount\r\n                            FROM         Sales_Return INV\r\n                            LEFT JOIN Job J ON INV.JobID=J.JobID\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID";
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
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Sales_Return", sqlCommand);
			return dataSet;
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
	}
}
