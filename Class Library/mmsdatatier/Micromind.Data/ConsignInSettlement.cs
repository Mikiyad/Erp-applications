using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ConsignInSettlement : StoreObject
	{
		private const string CONSIGNINSETTLEMENT_TABLE = "ConsignIn_Settlement";

		private const string CONSIGNINSETTLEMENTDETAIL_TABLE = "ConsignIn_Settlement_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string VENDORID_PARM = "@VendorID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string VENDORADDRESS_PARM = "@VendorAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string DISCOUNTFC_PARM = "@DiscountFC";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TOTAL_PARM = "@Total";

		private const string TOTALFC_PARM = "@TotalFC";

		private const string ISCASH_PARM = "@IsCash";

		private const string DUEDATE_PARM = "@DueDate";

		private const string CONSIGNSYSDOCID_PARM = "@ConsignSysDocID";

		private const string CONSIGNVOUCHERID_PARM = "@ConsignVoucherID";

		private const string COMMISSIONPERCENT_PARM = "@CommissionPercent";

		private const string COMMISSIONAMOUNT_PARM = "@CommissionAmount";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITPRICEFC_PARM = "@UnitPriceFC";

		private const string UNITEXPENSE_PARM = "@UnitExpense";

		private const string EXPENSEAMOUNT_PARM = "@ExpenseAmount";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string ORDERVOUCHERID_PARM = "@OrderVoucherID";

		private const string ORDERSYSDOCID_PARM = "@OrderSysDocID";

		private const string DNOTEVOUCHERID_PARM = "@DNoteVoucherID";

		private const string DNOTESYSDOCID_PARM = "@DNoteSysDocID";

		private const string ORDERROWINDEX_PARM = "@OrderRowIndex";

		private const string ISDNROW_PARM = "@IsDNRow";

		private const string ISRECOST_PARM = "@IsRecost";

		private const string CONSIGNROWINDEX_PARM = "@ConsignRowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string INVOICEPAYMENT_PARM = "@Invoice_Payment";

		private const string EXPENSEID_PARM = "@ExpenseID";

		private const string EXPENSENAME_PARM = "@ExpenseName";

		private const string BILLABLEAMOUNT_PARM = "@BillableAmount";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string CONSIGNINSETTLEDITEMS_TABLE = "ConsignIn_Settled_Items";

		private const string SALESROWID_PARM = "@SalesRowID";

		private const string ITEMCODE_PARM = "@ItemCode";

		private const string PRICE_PARM = "@Price";

		public ConsignInSettlement(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSettlementText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ConsignIn_Settlement", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("ConsignSysDocID", "@ConsignSysDocID"), new FieldValue("ConsignVoucherID", "@ConsignVoucherID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("DueDate", "@DueDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("VendorAddress", "@VendorAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("CommissionPercent", "@CommissionPercent"), new FieldValue("CommissionAmount", "@CommissionAmount"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("IsCash", "@IsCash"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("ConsignIn_Settlement", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSettlementCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSettlementText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSettlementText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@ConsignSysDocID", SqlDbType.NVarChar);
			parameters.Add("@ConsignVoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@VendorAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@DiscountFC", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters.Add("@CommissionPercent", SqlDbType.Decimal);
			parameters.Add("@CommissionAmount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@TotalFC", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@IsCash", SqlDbType.Bit);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@ConsignSysDocID"].SourceColumn = "ConsignSysDocID";
			parameters["@ConsignVoucherID"].SourceColumn = "ConsignVoucherID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@VendorAddress"].SourceColumn = "VendorAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@DiscountFC"].SourceColumn = "DiscountFC";
			parameters["@CommissionPercent"].SourceColumn = "CommissionPercent";
			parameters["@CommissionAmount"].SourceColumn = "CommissionAmount";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@TotalFC"].SourceColumn = "TotalFC";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@IsCash"].SourceColumn = "IsCash";
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

		private string GetInsertUpdateSettlementDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ConsignIn_Settlement_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ConsignSysDocID", "@ConsignSysDocID"), new FieldValue("ConsignVoucherID", "@ConsignVoucherID"), new FieldValue("ConsignRowIndex", "@ConsignRowIndex"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitExpense", "@UnitExpense"), new FieldValue("ExpenseAmount", "@ExpenseAmount"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("OrderSysDocID", "@OrderSysDocID"), new FieldValue("OrderVoucherID", "@OrderVoucherID"), new FieldValue("DNoteSysDocID", "@DNoteSysDocID"), new FieldValue("DNoteVoucherID", "@DNoteVoucherID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("IsDNRow", "@IsDNRow"), new FieldValue("IsRecost", "@IsRecost"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSettlementDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSettlementDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSettlementDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ConsignSysDocID", SqlDbType.NVarChar);
			parameters.Add("@ConsignVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ConsignRowIndex", SqlDbType.Int);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitExpense", SqlDbType.Decimal);
			parameters.Add("@ExpenseAmount", SqlDbType.Decimal);
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
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@OrderSysDocID", SqlDbType.NVarChar);
			parameters.Add("@OrderVoucherID", SqlDbType.NVarChar);
			parameters.Add("@DNoteSysDocID", SqlDbType.NVarChar);
			parameters.Add("@DNoteVoucherID", SqlDbType.NVarChar);
			parameters.Add("@OrderRowIndex", SqlDbType.Int);
			parameters.Add("@IsDNRow", SqlDbType.Bit);
			parameters.Add("@IsRecost", SqlDbType.Bit);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ConsignSysDocID"].SourceColumn = "ConsignSysDocID";
			parameters["@ConsignVoucherID"].SourceColumn = "ConsignVoucherID";
			parameters["@ConsignRowIndex"].SourceColumn = "ConsignRowIndex";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitExpense"].SourceColumn = "UnitExpense";
			parameters["@ExpenseAmount"].SourceColumn = "ExpenseAmount";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@UnitPriceFC"].SourceColumn = "UnitPriceFC";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@OrderVoucherID"].SourceColumn = "OrderVoucherID";
			parameters["@OrderSysDocID"].SourceColumn = "OrderSysDocID";
			parameters["@DNoteSysDocID"].SourceColumn = "DNoteSysDocID";
			parameters["@DNoteVoucherID"].SourceColumn = "DNoteVoucherID";
			parameters["@OrderRowIndex"].SourceColumn = "OrderRowIndex";
			parameters["@IsDNRow"].SourceColumn = "IsDNRow";
			parameters["@IsRecost"].SourceColumn = "IsRecost";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateExpenseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ConsignIn_Expense", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("Description", "@Description"), new FieldValue("BillableAmount", "@BillableAmount"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Reference", "@Reference"), new FieldValue("CurrencyRate", "@CurrencyRate"));
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
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@BillableAmount", SqlDbType.Money);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ExpenseID"].SourceColumn = "ExpenseID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@BillableAmount"].SourceColumn = "BillableAmount";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateSettledItemsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ConsignIn_Settled_Items", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("SalesRowID", "@SalesRowID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Quantity", "@Quantity"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSettledItemsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSettledItemsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSettledItemsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SalesRowID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@UnitPrice", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SalesRowID"].SourceColumn = "SalesRowID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Quantity"].SourceColumn = "Quantity";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(ConsignInSettlementData journalData)
		{
			return true;
		}

		public bool RecostInvoice(string productID, string locationID, decimal purchaseQuantity, decimal shortQuantity, decimal oldAvgCost, decimal newAvgCost, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string text = "";
				if (purchaseQuantity <= 0m)
				{
					return true;
				}
				if (!(oldAvgCost == newAvgCost) || !(new Products(base.DBConfig).GetLocationQuantity(productID, locationID, sqlTransaction) >= 0f))
				{
					text = "SELECT SID.SysDocID,SID.VoucherID,Quantity FROM ConsignIn_Settlement_Detail SID INNER JOIN ConsignIn_Settlement SI ON \r\n                         SID.VoucherID=SI.VoucherID AND SID.SysDocID=SI.SysDocID WHERE LocationID='" + locationID + "' AND ProductID='" + productID + "' AND ISNULL(IsRecost,'False')='True' ORDER BY SI.TransactionDate DESC";
					DataSet dataSet = new DataSet();
					FillDataSet(dataSet, "Products", text, sqlTransaction);
					decimal num = purchaseQuantity;
					decimal num2 = oldAvgCost;
					{
						foreach (DataRow row in dataSet.Tables["Products"].Rows)
						{
							bool flag2 = false;
							decimal num3 = decimal.Parse(row["Quantity"].ToString());
							if (!(num3 == 0m))
							{
								if (!(num > 0m))
								{
									return flag;
								}
								decimal num4 = default(decimal);
								if (num >= num3)
								{
									if (num3 > shortQuantity)
									{
										num4 = shortQuantity * newAvgCost - shortQuantity * oldAvgCost;
										num -= shortQuantity;
										num2 = (shortQuantity * newAvgCost + (num3 - shortQuantity) * oldAvgCost) / num3;
										shortQuantity = default(decimal);
									}
									else
									{
										num4 = num3 * newAvgCost - num3 * oldAvgCost;
										num -= num3;
										shortQuantity -= num3;
										num2 = newAvgCost;
									}
									flag2 = true;
								}
								else if (num > shortQuantity)
								{
									num4 = shortQuantity * newAvgCost - shortQuantity * oldAvgCost;
									num -= shortQuantity;
									num2 = (shortQuantity * newAvgCost + (num3 - shortQuantity) * oldAvgCost) / num3;
									shortQuantity = default(decimal);
									flag2 = true;
								}
								else
								{
									num4 = num * newAvgCost - num * oldAvgCost;
									shortQuantity -= num;
									num2 = (num * newAvgCost + (num3 - num) * oldAvgCost) / num3;
									num = default(decimal);
								}
								string text2 = new Databases(base.DBConfig).GetFieldValue("Location", "COGSAccountID", "LocationID", locationID, sqlTransaction).ToString();
								string text3 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", locationID, sqlTransaction).ToString();
								text = "UPDATE Journal_Details SET Debit=(Debit + " + num4 + ") WHERE \r\n                         JournalID IN (Select JournalID FROM Journal WHERE SysDocID='" + row["SysDocID"].ToString() + "' AND VoucherID='" + row["VoucherID"].ToString() + "') AND AccountID='" + text2 + "'";
								flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
								text = "UPDATE Journal_Details SET Credit=(Credit + " + num4 + ") WHERE \r\n                         JournalID IN (Select JournalID FROM Journal WHERE SysDocID='" + row["SysDocID"].ToString() + "' AND VoucherID='" + row["VoucherID"].ToString() + "') AND AccountID='" + text3 + "'";
								flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
								text = "UPDATE Inventory_Transactions SET AverageCost=" + num2 + "\r\n                               ,AssetValue=(Quantity * " + num2 + ") WHERE \r\n                              SysDocID='" + row["SysDocID"].ToString() + "' AND VoucherID='" + row["VoucherID"].ToString() + "' AND ProductID='" + productID + "'";
								flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
								if (flag2)
								{
									text = "UPDATE ConsignIn_Settlement_Detail SET IsRecost='False' WHERE LocationID='" + locationID + "' AND ProductID='" + productID + "' AND ISNULL(IsRecost,'False')='True'";
									flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
								}
							}
						}
						return flag;
					}
				}
				text = "UPDATE ConsignIn_Settlement_Detail SET IsRecost='False' WHERE LocationID='" + locationID + "' AND ProductID='" + productID + "' AND ISNULL(IsRecost,'False')='True'";
				return flag & (ExecuteNonQuery(text, sqlTransaction) >= 0);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool InsertUpdateSettlement(ConsignInSettlementData settlementData, bool isUpdate)
		{
			return InsertUpdateSettlement(settlementData, isUpdate, null);
		}

		public bool InsertUpdateSettlement(ConsignInSettlementData settlementData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			string text = "";
			bool flag = true;
			SqlCommand sqlCommand = null;
			string text2 = "";
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = settlementData.ConsignInSettlementTable.Rows[0];
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				string text3 = dataRow["VoucherID"].ToString();
				string text4 = dataRow["SysDocID"].ToString();
				string text5 = dataRow["ConsignSysDocID"].ToString();
				string text6 = dataRow["ConsignVoucherID"].ToString();
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Consign_In", "Status", "SysDocID", text5, "VoucherID", text6, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = (byte.Parse(fieldValue.ToString()) == 3);
				}
				if (flag2)
				{
					throw new CompanyException("This consignment is already closed and cannot be modified.", 1061);
				}
				dataRow["DueDate"] = dataRow["TransactionDate"];
				string termID = dataRow["TermID"].ToString();
				PaymentTermData termByID = new Terms(base.DBConfig).GetTermByID(termID);
				DataRow dataRow2 = null;
				if (termByID != null && termByID.Tables.Count > 0 && termByID.Tables[0].Rows.Count > 0)
				{
					dataRow2 = termByID.Tables[0].Rows[0];
					byte b = byte.Parse(dataRow2["TermType"].ToString());
					DateTime dateTime = DateTime.Parse(dataRow["TransactionDate"].ToString());
					switch (b)
					{
					case 1:
					{
						int num = int.Parse(dataRow2["NetDays"].ToString());
						DateTime dateTime3 = dateTime.AddDays(num);
						dataRow["DueDate"] = dateTime3;
						break;
					}
					case 2:
					{
						dateTime = dateTime.AddMonths(1);
						DateTime dateTime2 = new DateTime(dateTime.Year, dateTime.Month, 1, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
						dataRow["DueDate"] = dateTime2;
						break;
					}
					}
				}
				decimal num2 = default(decimal);
				foreach (DataRow row in settlementData.ConsignInSettlementDetailTable.Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result2);
					decimal.TryParse(row["UnitPrice"].ToString(), out result3);
					decimal.TryParse(row["Amount"].ToString(), out result);
					num2 += result;
				}
				dataRow["Total"] = num2;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("ConsignIn_Settlement", "VoucherID", dataRow["SysDocID"].ToString(), text3, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				bool flag3 = false;
				decimal result4 = 1m;
				string a = "M";
				dataRow["CurrencyID"].ToString();
				if (dataRow["CurrencyID"] != DBNull.Value && baseCurrencyID != dataRow["CurrencyID"].ToString())
				{
					flag3 = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result4);
					a = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
				}
				if (flag3)
				{
					decimal result5 = default(decimal);
					decimal num3 = default(decimal);
					decimal.TryParse(dataRow["Total"].ToString(), out result5);
					num3 = ((!(a == "M")) ? Math.Round(result5 * result4, 4) : Math.Round(result5 / result4, 4));
					dataRow["TotalFC"] = num3;
					foreach (DataRow row2 in settlementData.ExpenseTable.Rows)
					{
						decimal.TryParse(row2["Amount"].ToString(), out result5);
						if (a == "M")
						{
							row2["AmountFC"] = Math.Round(result5 / result4, currencyDecimalPoints);
						}
						else
						{
							row2["AmountFC"] = Math.Round(result5 * result4, currencyDecimalPoints);
						}
					}
				}
				foreach (DataRow row3 in settlementData.ConsignInSettlementDetailTable.Rows)
				{
					float num4 = 0f;
					row3["SysDocID"] = dataRow["SysDocID"];
					row3["VoucherID"] = dataRow["VoucherID"];
					text2 = row3["ProductID"].ToString();
					string checkFieldValue = row3["LocationID"].ToString();
					int.Parse(row3["RowIndex"].ToString());
					decimal result6 = default(decimal);
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Product_Location", "Quantity", "ProductID", text2, "LocationID", checkFieldValue, sqlTransaction);
					if (fieldValue != null)
					{
						decimal.TryParse(fieldValue.ToString(), out result6);
					}
					string idFieldValue = row3["ConsignSysDocID"].ToString();
					string checkFieldValue2 = row3["ConsignVoucherID"].ToString();
					string checkFieldValue3 = row3["ConsignRowIndex"].ToString();
					object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Consign_In_Detail", "QuantitySettled", "SysDocID", idFieldValue, "VoucherID", checkFieldValue2, "RowIndex", checkFieldValue3, sqlTransaction);
					float num5 = 0f;
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						num5 = float.Parse(fieldValue2.ToString());
					}
					fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Consign_In_Detail", "QuantityReturned", "SysDocID", idFieldValue, "VoucherID", checkFieldValue2, "RowIndex", checkFieldValue3, sqlTransaction);
					float num6 = 0f;
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						num6 = float.Parse(fieldValue2.ToString());
					}
					fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Consign_In_Detail", "Quantity", "SysDocID", idFieldValue, "VoucherID", checkFieldValue2, "RowIndex", checkFieldValue3, sqlTransaction);
					float num7 = 0f;
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						num7 = float.Parse(fieldValue2.ToString());
					}
					if (num5 + num6 > num7)
					{
						throw new CompanyException("Settlement quantity cannot be greater than balance quantity.", 1026);
					}
					num4 = float.Parse(row3["Quantity"].ToString());
					string text7 = "";
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text2, sqlTransaction);
					if (fieldValue != null)
					{
						text7 = fieldValue.ToString();
					}
					if (text7 != "" && row3["UnitID"] != DBNull.Value && row3["UnitID"].ToString() != text7)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text2, row3["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text2 + "\nUnit:" + row3["UnitID"].ToString());
						float num8 = float.Parse(obj2["Factor"].ToString());
						string text8 = obj2["FactorType"].ToString();
						num4 = float.Parse(row3["Quantity"].ToString());
						row3["UnitFactor"] = num8;
						row3["FactorType"] = text8;
						row3["UnitQuantity"] = row3["Quantity"];
						num4 = ((!(text8 == "M")) ? float.Parse(Math.Round(num4 * num8, 5).ToString()) : float.Parse(Math.Round(num4 / num8, 5).ToString()));
						row3["Quantity"] = num4;
					}
				}
				if (isUpdate)
				{
					flag &= DeleteSettlementDetailsRows(text4, text3, isDeletingTransaction: false, sqlTransaction);
				}
				sqlCommand = GetInsertUpdateSettlementCommand(isUpdate);
				sqlCommand.Transaction = sqlTransaction;
				if (!isUpdate)
				{
					if (settlementData.Tables["ConsignIn_Settlement"].Rows.Count > 0)
					{
						flag &= Insert(settlementData, "ConsignIn_Settlement", sqlCommand);
					}
				}
				else
				{
					flag &= Update(settlementData, "ConsignIn_Settlement", sqlCommand);
				}
				sqlCommand = GetInsertUpdateSettlementDetailsCommand(isUpdate: false);
				sqlCommand.Transaction = sqlTransaction;
				if (settlementData.Tables["ConsignIn_Settlement_Detail"].Rows.Count > 0)
				{
					flag &= Insert(settlementData, "ConsignIn_Settlement_Detail", sqlCommand);
				}
				text2 = "";
				foreach (DataRow row4 in settlementData.ConsignInSettlementDetailTable.Rows)
				{
					text2 = row4["ProductID"].ToString();
					int result7 = 0;
					int.TryParse(row4["ConsignRowIndex"].ToString(), out result7);
					float result8 = 0f;
					if (row4["UnitQuantity"] != DBNull.Value)
					{
						float.TryParse(row4["UnitQuantity"].ToString(), out result8);
					}
					else
					{
						float.TryParse(row4["Quantity"].ToString(), out result8);
					}
					flag &= UpdateRowSettledQuantity(text5, text6, result7, result8, sqlTransaction);
				}
				foreach (DataRow row5 in settlementData.ExpenseTable.Rows)
				{
					string text9 = row5["SourceSysDocID"].ToString();
					string text10 = row5["SourceVoucherID"].ToString();
					string text11 = row5["SourceRowIndex"].ToString();
					if (text10 != "")
					{
						text = "UPDATE Journal_Details SET IsBilled='True' WHERE SysDocID = '" + text9 + "' AND VoucherID = '" + text10 + "' AND JournalDetailID = " + text11;
						flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
					}
				}
				foreach (DataRow row6 in settlementData.ExpenseTable.Rows)
				{
					row6["SysDocID"] = dataRow["SysDocID"];
					row6["VoucherID"] = dataRow["VoucherID"];
				}
				sqlCommand = GetInsertUpdateExpenseCommand(isUpdate: false);
				sqlCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					DeleteExpenseRows(text4, text3, sqlTransaction);
				}
				if (settlementData.Tables["ConsignIn_Expense"].Rows.Count > 0)
				{
					flag &= Insert(settlementData, "ConsignIn_Expense", sqlCommand);
				}
				sqlCommand = GetInsertUpdateSettledItemsCommand(isUpdate: false);
				sqlCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					DeleteSettledRows(text4, text3, sqlTransaction);
				}
				if (settlementData.Tables["ConsignIn_Settled_Items"].Rows.Count > 0)
				{
					flag &= Insert(settlementData, "ConsignIn_Settled_Items", sqlCommand);
				}
				text = "UPDATE Product_Lot_Sales SET IsSettled = 'True'\r\n                        WHERE RecordID IN (SELECT SalesRowID FROM ConsignIn_Settled_Items WHERE SysDocID = '" + text4 + "' AND VoucherID = '" + text3 + "')";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				GLData journalData = CreateInvoiceGLData(settlementData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				text = "UPDATE ConsignIn_Settlement SET TotalCOGS=(SELECT SUM(COGS) FROM ConsignIn_Settlement_Detail WHERE SysDocID='" + text4 + "' AND VoucherID='" + text3 + "')\r\n                             WHERE SysDocID='" + text4 + "' AND VoucherID='" + text3 + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				flag &= new ConsignIn(base.DBConfig).SetStatus(text5, text6, ConsignInStatusEnum.Settled, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("ConsignIn_Settlement", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Settlement";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text3, text4, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text3, text4, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "ConsignIn_Settlement", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ConsignInSettlement, text4, text3, "ConsignIn_Settlement", sqlTransaction);
				return flag;
			}
			catch (Exception)
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		internal bool DeleteExpenseRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "UPDATE Journal_Details SET IsBilled = 'False' WHERE JournalDetailID In (SELECT SourceRowIndex FROM ConsignIn_Expense WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "')";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				string commandText = "DELETE FROM ConsignIn_Expense WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteSettledRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "UPDATE Product_Lot_Sales SET IsSettled = 'False'\r\n                        WHERE RecordID IN (SELECT SalesRowID FROM ConsignIn_Settled_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "')";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				string commandText = "DELETE FROM ConsignIn_Settled_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateInvoiceGLData(ConsignInSettlementData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.ConsignInSettlementTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string text = dataRow["VendorID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				dataRow["VoucherID"].ToString();
				dataRow["ConsignSysDocID"].ToString();
				dataRow["ConsignVoucherID"].ToString();
				string value = dataRow["CompanyID"].ToString();
				string value2 = dataRow["DivisionID"].ToString();
				string textCommand = "SELECT SD.LocationID, ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID ,LOC.COGSAccountID,LOC.DiscountGivenAccountID,\r\n                                LOC.InventoryAccountID,LOC.SalesAccountID,LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID,Loc.ConsignInAccountID,LOC.ConsignInCommissionAccountID,LOC.ConsignInDiffAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Vendor VEN ON VendorID='" + text + "'\r\n                                LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", text2, sqlTransaction).ToString();
				dataRow2["DiscountGivenAccountID"].ToString();
				dataRow2["SalesTaxAccountID"].ToString();
				string value3 = dataRow2["APAccountID"].ToString();
				string a = dataRow2["BaseCurrencyID"].ToString();
				string value4 = dataRow2["ConsignInAccountID"].ToString();
				string value5 = dataRow2["ConsignInCommissionAccountID"].ToString();
				string text3 = dataRow2["ConsignInDiffAccountID"].ToString();
				bool flag = false;
				decimal result = 1m;
				string currencyID = "";
				if (dataRow["CurrencyID"] != DBNull.Value && a != dataRow["CurrencyID"].ToString())
				{
					flag = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result);
					currencyID = dataRow["CurrencyID"].ToString();
				}
				string currencyRateType = new Currencies(base.DBConfig).GetCurrencyRateType(currencyID);
				bool result2 = false;
				bool.TryParse(dataRow["IsCash"].ToString(), out result2);
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.ConsignInSettlement;
				if (result2)
				{
					sysDocTypes = SysDocTypes.SalesReceipt;
				}
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["CurrencyID"] = dataRow["CurrencyID"];
				dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Settlement - ";
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				decimal num = default(decimal);
				new Hashtable();
				new ArrayList();
				new Hashtable();
				new ArrayList();
				new Hashtable();
				new ArrayList();
				decimal num2 = default(decimal);
				foreach (DataRow row in transactionData.ConsignInSettlementDetailTable.Rows)
				{
					decimal result3 = default(decimal);
					if (flag)
					{
						decimal.TryParse(row["AmountFC"].ToString(), out result3);
					}
					else
					{
						decimal.TryParse(row["Amount"].ToString(), out result3);
					}
					num2 += Math.Round(result3, currencyDecimalPoints);
				}
				DataRow dataRow5;
				if (num2 != 0m)
				{
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = value4;
					dataRow5["PayeeID"] = text;
					if (flag)
					{
						dataRow5["DebitFC"] = num2;
						dataRow5["CreditFC"] = DBNull.Value;
					}
					else
					{
						dataRow5["Debit"] = num2;
						dataRow5["Credit"] = DBNull.Value;
					}
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				decimal d = default(decimal);
				decimal num3 = default(decimal);
				foreach (DataRow row2 in transactionData.ExpenseTable.Rows)
				{
					int num4 = -1;
					if (row2["SourceRowIndex"] != DBNull.Value)
					{
						num4 = int.Parse(row2["SourceRowIndex"].ToString());
					}
					string text4 = row2["ExpenseID"].ToString();
					num = default(decimal);
					num = ((!flag) ? decimal.Parse(row2["Amount"].ToString()) : decimal.Parse(row2["AmountFC"].ToString()));
					decimal num5 = default(decimal);
					decimal num6 = default(decimal);
					if (!row2["BillableAmount"].IsDBNullOrEmpty())
					{
						num5 = decimal.Parse(row2["BillableAmount"].ToString());
					}
					num6 = decimal.Parse(row2["Amount"].ToString());
					if (num5 != 0m)
					{
						num3 = num6 - num5;
					}
					string value6 = "";
					if (num4 > 0)
					{
						textCommand = "SELECT AccountID FROM Journal_Details JD WHERE JournalDetailID = " + num4;
						object obj = ExecuteScalar(textCommand, sqlTransaction);
						if (obj != null && obj.ToString() != "")
						{
							value6 = obj.ToString();
						}
					}
					else
					{
						value6 = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text4, sqlTransaction);
					}
					decimal result4 = 1m;
					decimal.TryParse(row2["CurrencyRate"].ToString(), out result4);
					num = Math.Round(num, currencyDecimalPoints);
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = value6;
					d += num;
					if (flag)
					{
						num5 = ((!(currencyRateType == "M")) ? Math.Round(num5 * result, currencyDecimalPoints, MidpointRounding.AwayFromZero) : Math.Round(num5 / result, currencyDecimalPoints, MidpointRounding.AwayFromZero));
						num = Math.Abs(num5);
						if (num > 0m)
						{
							dataRow5["DebitFC"] = DBNull.Value;
							dataRow5["CreditFC"] = num;
						}
						else
						{
							dataRow5["DebitFC"] = Math.Abs(num);
							dataRow5["CreditFC"] = DBNull.Value;
						}
					}
					else if (num > 0m)
					{
						dataRow5["Debit"] = DBNull.Value;
						dataRow5["Credit"] = num;
					}
					else
					{
						dataRow5["Debit"] = Math.Abs(num);
						dataRow5["Credit"] = DBNull.Value;
					}
					dataRow5["Reference"] = text4;
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				decimal num7 = default(decimal);
				num7 = decimal.Parse(dataRow["CommissionAmount"].ToString());
				if (num7 > 0m)
				{
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = value5;
					if (flag)
					{
						dataRow5["DebitFC"] = DBNull.Value;
						dataRow5["CreditFC"] = num7;
					}
					else
					{
						dataRow5["Debit"] = DBNull.Value;
						dataRow5["Credit"] = num7;
					}
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				if (num3 != 0m)
				{
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = text3;
					if (text3 == "")
					{
						throw new CompanyException("Consignment In variance account is not set.");
					}
					if (flag)
					{
						num3 = ((!(currencyRateType == "M")) ? Math.Round(num3 * result, currencyDecimalPoints, MidpointRounding.AwayFromZero) : Math.Round(num3 / result, currencyDecimalPoints, MidpointRounding.AwayFromZero));
						if (num3 > 0m)
						{
							dataRow5["DebitFC"] = DBNull.Value;
							dataRow5["CreditFC"] = Math.Abs(num3);
						}
						else
						{
							dataRow5["DebitFC"] = Math.Abs(num3);
							dataRow5["CreditFC"] = DBNull.Value;
						}
					}
					else if (num3 > 0m)
					{
						dataRow5["Debit"] = DBNull.Value;
						dataRow5["Credit"] = num3;
					}
					else
					{
						dataRow5["Debit"] = Math.Abs(num3);
						dataRow5["Credit"] = DBNull.Value;
					}
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["DueDate"] = dataRow["DueDate"];
				dataRow5["AccountID"] = value3;
				dataRow5["PayeeID"] = text;
				dataRow5["PayeeType"] = "V";
				dataRow5["IsARAP"] = true;
				decimal num8 = num2 - d - num7;
				if (flag)
				{
					if (num8 >= 0m)
					{
						dataRow5["DebitFC"] = DBNull.Value;
						dataRow5["CreditFC"] = num8;
					}
					else
					{
						dataRow5["DebitFC"] = Math.Abs(num8);
						dataRow5["CreditFC"] = DBNull.Value;
					}
				}
				else if (num8 >= 0m)
				{
					dataRow5["Debit"] = DBNull.Value;
					dataRow5["Credit"] = num8;
				}
				else
				{
					dataRow5["Debit"] = Math.Abs(num8);
					dataRow5["Credit"] = DBNull.Value;
				}
				dataRow5["Reference"] = dataRow["Reference"];
				dataRow5["Description"] = dataRow["Note"];
				dataRow5["CompanyID"] = value;
				dataRow5["DivisionID"] = value2;
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public ConsignInSettlementData GetSettlementByID(string sysDocID, string voucherID)
		{
			return GetSettlementByID(sysDocID, voucherID, null);
		}

		internal ConsignInSettlementData GetSettlementByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				ConsignInSettlementData consignInSettlementData = new ConsignInSettlementData();
				string text = "SELECT * FROM ConsignIn_Settlement WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				new SqlCommand(text).Transaction = sqlTransaction;
				FillDataSet(consignInSettlementData, "ConsignIn_Settlement", text, sqlTransaction);
				if (consignInSettlementData == null || consignInSettlementData.Tables.Count == 0 || consignInSettlementData.Tables["ConsignIn_Settlement"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT SD.*,P.Description,P.ItemType,COD.Quantity AS DeliveredQty,COD.RowIndex AS ConsignRowIndex,QuantitySettled, COD.Quantity - ISNULL(QuantitySettled,0) - ISNULL(QuantityReturned,0) AS BalanceQty\r\n                        FROM ConsignIn_Settlement_Detail SD INNER JOIN Product P ON SD.ProductID=P.ProductID\r\n                        INNER JOIN Consign_In_Detail COD ON COD.SysDocID=SD.ConsignSysDocID\r\n                        AND COD.VoucherID=SD.ConsignVoucherID AND COD.RowIndex=SD.ConsignRowIndex\r\n                        WHERE SD.VoucherID='" + voucherID + "' AND SD.SysDocID='" + sysDocID + "'";
				FillDataSet(consignInSettlementData, "ConsignIn_Settlement_Detail", text, sqlTransaction);
				text = "SELECT * FROM ConsignIn_Expense\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(consignInSettlementData, "ConsignIn_Expense", text);
				return consignInSettlementData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteSettlementDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				ConsignInSettlementData consignInSettlementData = new ConsignInSettlementData();
				string textCommand = "SELECT SO.ConsignSysDocID,SO.ConsignVoucherID,SOD.*,ISNULL(ISVOID,'False') AS IsVoid\r\n                                    FROM ConsignIn_Settlement_Detail SOD INNER JOIN ConsignIn_Settlement SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                                    WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(consignInSettlementData, "ConsignIn_Settlement_Detail", textCommand, sqlTransaction);
				if (consignInSettlementData.ConsignInSettlementDetailTable.Rows.Count == 0)
				{
					return true;
				}
				string sysDocID2 = consignInSettlementData.ConsignInSettlementDetailTable.Rows[0]["ConsignSysDocID"].ToString();
				string voucherID2 = consignInSettlementData.ConsignInSettlementDetailTable.Rows[0]["ConsignVoucherID"].ToString();
				bool result = false;
				bool.TryParse(consignInSettlementData.ConsignInSettlementDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (!result)
				{
					foreach (DataRow row in consignInSettlementData.ConsignInSettlementDetailTable.Rows)
					{
						row["ProductID"].ToString();
						string s = row["ConsignRowIndex"].ToString();
						float num = float.Parse(row["Quantity"].ToString());
						flag &= UpdateRowSettledQuantity(sysDocID2, voucherID2, int.Parse(s), -1f * num, sqlTransaction);
					}
				}
				flag &= new ConsignIn(base.DBConfig).CloseOpenConsignment(sysDocID2, voucherID2, sqlTransaction);
				textCommand = "DELETE FROM ConsignIn_Settlement_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowSettledQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantitySettled FROM Consign_In_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Consign", textCommand);
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
					float.TryParse(dataRow["QuantitySettled"].ToString(), out result2);
				}
				result2 += quantity;
				textCommand = "UPDATE Consign_In_Detail SET QuantitySettled=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public bool VoidSettlement(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidSettlement(sysDocID, voucherID, isVoid, null);
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

		private bool VoidSettlement(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				ConsignInSettlementData consignInSettlementData = new ConsignInSettlementData();
				string textCommand = "SELECT SO.ConsignSysDocID,SO.ConsignVoucherID,SOD.*,ISNULL(ISVOID,'False') AS IsVoid,ISNULL(IsCash,'False') AS IsCash FROM ConsignIn_Settlement_Detail SOD INNER JOIN ConsignIn_Settlement SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(consignInSettlementData, "ConsignIn_Settlement_Detail", textCommand, sqlTransaction);
				string text = consignInSettlementData.Tables["ConsignIn_Settlement_Detail"].Rows[0]["ConsignSysDocID"].ToString();
				string text2 = consignInSettlementData.Tables["ConsignIn_Settlement_Detail"].Rows[0]["ConsignVoucherID"].ToString();
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Consign_In", "Status", "SysDocID", text, "VoucherID", text2, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = (byte.Parse(fieldValue.ToString()) == 3);
				}
				if (flag2)
				{
					throw new CompanyException("This consignment is already closed and cannot be modified.", 1061);
				}
				bool result = false;
				bool.TryParse(consignInSettlementData.ConsignInSettlementDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(consignInSettlementData.ConsignInSettlementDetailTable.Rows[0]["IsCash"].ToString(), out result2);
				if (result == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				flag = ((!result2) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(56, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(3, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)));
				foreach (DataRow row in consignInSettlementData.ConsignInSettlementDetailTable.Rows)
				{
					row["ProductID"].ToString();
					int result3 = 0;
					int.TryParse(row["ConsignRowIndex"].ToString(), out result3);
					float result4 = 0f;
					if (row["UnitQuantity"] != DBNull.Value)
					{
						float.TryParse(row["UnitQuantity"].ToString(), out result4);
					}
					else
					{
						float.TryParse(row["Quantity"].ToString(), out result4);
					}
					flag &= UpdateRowSettledQuantity(text, text2, result3, -1f * result4, sqlTransaction);
				}
				DeleteSettledRows(sysDocID, voucherID, sqlTransaction);
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				textCommand = "UPDATE ConsignIn_Settlement SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				flag &= new ConsignIn(base.DBConfig).SetStatus(text, text2, ConsignInStatusEnum.Open, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Consign In Settlement", voucherID, sysDocID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteSettlement(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("ConsignIn_Settlement", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidSettlement(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeleteSettlementDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM ConsignIn_Settlement WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Consign In Settlement", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		internal bool CloseShippedInvoice(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string str = "SELECT COUNT(RowIndex)FROM ConsignIn_Settlement_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				str += " AND CASE WHEN UnitQuantity IS NULL THEN Quantity ELSE UnitQuantity END - ISNULL(QuantityShipped,0)=0";
				object obj = ExecuteScalar(str, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				str = "UPDATE ConsignIn_Settlement SET IsDelivered = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(str, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool UpdateRowShippedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityShipped FROM ConsignIn_Settlement_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				textCommand = "UPDATE ConsignIn_Settlement_Detail SET QuantityShipped=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
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
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor] FROM ConsignIn_Settlement SO\r\n                             INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsDelivered,0)=0";
				if (vendorID != "")
				{
					text = text + " AND SO.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "ConsignIn_Settlement", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price)
		{
			decimal result = default(decimal);
			string exp = "SELECT MinPrice FROM Product WHERE ProductID='" + productID + "'";
			DataSet dataSet = new DataSet();
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				decimal.TryParse(obj.ToString(), out result);
			}
			if (unitID != "")
			{
				exp = "SELECT FactorType,Factor FROM Product_Unit WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "'";
				dataSet = new DataSet();
				FillDataSet(dataSet, "Unit", exp);
				if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
				{
					string a = dataSet.Tables[0].Rows[0]["FactorType"].ToString();
					decimal num = decimal.Parse(dataSet.Tables[0].Rows[0]["Factor"].ToString());
					if (a == "M")
					{
						result /= num;
					}
					else
					{
						result *= num;
					}
				}
			}
			if (currencyID != "")
			{
				result /= currencyRate;
			}
			if (price < result)
			{
				return true;
			}
			return false;
		}

		public DataSet GetSettlementToPrint(string sysDocID, string voucherID)
		{
			return GetSettlementToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetSettlementToPrint(string sysDocID, string[] voucherID)
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
				string text2 = "SELECT  DISTINCT   SI.SysDocID,SI.VoucherID,SI.VendorID,VendorName,SI.VendorAddress,SI.TransactionDate,\r\n                                IsCash,SI.SalesPersonID,SI.RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                CommissionAmount,CommissionPercent,SI.DateCreated,SI.DateUpdated,SI.CreatedBy,SI.UpdatedBy,CI.ContainerNo,CI.TransactionDate AS [Arrival Date],\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,SI.IsVoid,SI.Reference,ISNULL(SI.DiscountFC,SI.Discount) AS Discount,\r\n                                (SELECT SUM(ISNULL(CE.AmountFC,CE.Amount)) FROM ConsignIn_Expense CE WHERE CE.SysDocID='" + sysDocID + "' AND CE.VoucherID IN (" + text + ")) AS [TotalExpense],";
				text2 = text2 + "ISNULL(ISNULL(TaxAmountFC,TaxAmount) ,0) AS Tax,ISNULL(SI.TotalFC,SI.Total) AS Total,SI.PONumber,SI.Note,SI.ConsignSysDocID,SI.ConsignVoucherID,SI.PONUMBER as Settlement#\r\n                                FROM  ConsignIn_Settlement SI INNER JOIN Vendor ON SI.VendorID=Vendor.VendorID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Vendor_Address CA ON CA.AddressID=ShippingAddressID AND CA.VendorID=SI.VendorID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT JOIN Consign_In CI ON CI.SysDocID=SI.ConsignSysDocID AND CI.VoucherID=SI.ConsignVoucherID\r\n                                WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(text2);
				FillDataSet(dataSet, "ConsignIn_Settlement", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["ConsignIn_Settlement"].Rows.Count == 0)
				{
					return null;
				}
				text2 = "SELECT TEMP.SysDocID,Temp.VoucherID,Temp.ProductID,Temp.Description,Temp.Quantity AS[Setteled],Temp.TotalFC,Temp.Price AS [Unit Price],\r\n                        Temp.Total AS [Amount],Temp.UnitID,Temp.LocationID,CID.Quantity,CID.QuantityDamage,CID.QuantityReturned,CID.QuantitySettled,CID.QuantitySold,\r\n                        (SELECT (SUM(ISNULL(FOCQuantity,0)+CASE WHEN SD.SysDocType IN (87) THEN SoldQty ELSE 0 END)) \r\n                        FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID \r\n                        WHERE LotNo IN \r\n                        (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = TEMP.ConsignVoucherID AND DocID = Temp.ConsignSysDocID) OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = TEMP.ConsignVoucherID AND DocID = Temp.ConsignSysDocID)))\r\n                        AND SysDocType NOT IN (19,20,21,40) AND ItemCode=Temp.ProductID )AS FOCQuantity\r\n                        FROM \r\n                        (SELECT  CSD.SysDocID,CSD.VoucherID,CSD.ConsignSysDocID,CSD.ConsignVoucherID,CSD.ProductID,CSD.Description,SUM(ISNULL(CSD.UnitQuantity,CSD.Quantity)) AS Quantity,\r\n                        SUM( (ISNULL(CSD.UnitPriceFC,CSD.UnitPrice))* (ISNULL(CSD.UnitQuantity,CSD.Quantity)))/SUM(ISNULL(CSD.UnitQuantity,CSD.Quantity)) AS Price,\r\n                        SUM(ISNULL(CSD.UnitQuantity,CSD.Quantity)*ISNULL(CSD.UnitPriceFC,CSD.UnitPrice)) AS Total,SUM(CSD.AmountFC) AS TotalFC,CSD.UnitID,CSD.LocationID\r\n                        FROM  ConsignIn_Settlement_Detail CSD \r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				text2 += " GROUP BY CSD.SysDocID,CSD.VoucherID,CSD.ProductID,CSD.Description,CSD.UnitID,CSD.LocationID,CSD.ConsignSysDocID,CSD.ConsignVoucherID) AS TEMP LEFT JOIN Consign_In_Detail CID ON CID.SysDocID=Temp.ConsignSysDocID AND CID.VoucherID=Temp.ConsignVoucherID  AND CID.ProductID=Temp.ProductID ";
				FillDataSet(dataSet, "ConsignIn_Settlement_Detail", text2);
				text2 = "SELECT EC.ExpenseName,EC.ExpenseID AS MstCode,CinE.*,ISNULL(CinE.AmountFC,CinE.Amount) AS Expense FROM ConsignIn_Expense CinE LEFT JOIN  Expense_Code EC ON CinE.ExpenseID=EC.ExpenseID WHERE CinE.SysDocID='" + sysDocID + "' AND CinE.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "ConsignIn_Expense", text2);
				dataSet.Relations.Add("VendorInvoice", new DataColumn[2]
				{
					dataSet.Tables["ConsignIn_Settlement"].Columns["SysDocID"],
					dataSet.Tables["ConsignIn_Settlement"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["ConsignIn_Settlement_Detail"].Columns["SysDocID"],
					dataSet.Tables["ConsignIn_Settlement_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("ConsignInExpense", new DataColumn[2]
				{
					dataSet.Tables["ConsignIn_Settlement"].Columns["SysDocID"],
					dataSet.Tables["ConsignIn_Settlement"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["ConsignIn_Expense"].Columns["SysDocID"],
					dataSet.Tables["ConsignIn_Expense"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["ConsignIn_Settlement"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["ConsignIn_Settlement"].Rows)
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

		public DataSet GetConsignInSoldItems(string sysDocID, string voucherID)
		{
			string exp = "SELECT TransactionDate FROM Consign_In WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
			DateTime date = DateTime.Parse(ExecuteScalar(exp).ToString()).AddDays(-30.0);
			string text = StoreConfiguration.ToSqlDateTimeString(date);
			exp = "SELECT Con.SysDocID as DocID,CON.InvoiceVoucherID AS InvoiceNumber, RecordID, CON.CustomerID,ISNULL(Con.CustomerName,'Non Sale Issue') AS CustomerName,\r\n           CASE WHEN SysDocType IN (87) THEN SUM(CON.DeliveredQty) ELSE 0 END  + SUM(CON.FOCQuantity) AS FOC,CON.InvoiceDate,SUM(CON.InvoiceQty - FOCQuantity) AS QtyInvoiced,\r\n          CON.ProductID AS ItemCode,CON.SysDocID,CON.SysDocType,CON.UnitPrice AS UnitPrice,\r\n           CON.VoucherID, ISNULL(Round(SUM(CON.InvoiceQty - FOCQuantity) * Con.UnitPrice,2),0) AS Amount FROM \r\n\r\n\r\n           (SELECT PLD.*,P.Description, IT.TransactionDate,CASE WHEN IT.PayeeType = 'C' THEN IT.PayeeID ELSE NULL END AS CustomerID ,\r\n           CUS.CustomerName, ISNULL(INV.VoucherID,PLD.VoucherID) AS InvoiceVoucherID, \r\n            CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(INV.UnitPrice,CashInv.UnitPrice)  END AS UnitPrice,ISNULL(DRD.Quantity,0) + ISNULL(SRD.Quantity,0) AS QuantityReturned,\r\n            CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(SoldQty,0)- (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))  END AS InvoiceQty ,ISNULL(INV.TransactionDate,CashInv.TransactionDate) AS InvoiceDate,\r\n            ISNULL(SoldQty,0) - (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))  AS DeliveredQty\r\n             FROM ( \r\n\r\n\r\n             SELECT SD.SysDocType,SD.DocName,PLS.RecordID, PLS.DocID AS SysDocID, InvoiceNumber AS VoucherID,RowIndex,ItemCode AS ProductID,PLS.LocationID,LotNo AS LotNumber,SoldQty,ISNULL(FOCQuantity,0) AS FOCQuantity\r\n             FROM Product_Lot_Sales PLS  WITH (NOLOCK) INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID WHERE LotNo IN \r\n             (SELECT LotNumber FROM Product_Lot PL WITH (NOLOCK) WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "') OR SourceLotNumber IN \r\n             (SELECT PL.LotNumber FROM Product_Lot PL WITH (NOLOCK) WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n              AND SysDocType NOT IN (19,20,21,40) AND ISNULL(IsSettled,'False') = 'False'\r\n            UNION\r\n              SELECT SD.SysDocType,SD.DocName,-1 AS RecordID,PLS.SysDocID, VoucherID,RowIndex, ProductID,PLS.LocationID,SourceLotNumber AS  LotNumber,-1 * LotQty AS SoldQty,0 AS FOCQuantity\r\n               FROM Product_Lot_Receiving_Detail PLS WITH (NOLOCK) INNER JOIN System_Document SD ON PLS.SysDocID = SD.SysDocID WHERE SourceLotNumber IN \r\n                 (SELECT LotNumber FROM Product_Lot PL WITH (NOLOCK) WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')\r\n                  OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WITH (NOLOCK) WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n                  AND SysDocType NOT IN (19,20,21,40))  AS PLD\r\n\r\n                   LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = PLD.SysDocID AND IT.VoucherID = PLD.VoucherID AND IT.RowIndex = PLD.RowIndex\r\n                    LEFT OUTER JOIN  (SELECT SID2.Quantity,SID2.OrderSysDocID,SID2.VoucherID,SID2.OrderVoucherID,SID2.OrderRowIndex,SID2.UnitPrice,SI2.TransactionDate\r\n                    FROM Sales_Invoice_Detail SID2 WITH (NOLOCK) INNER JOIN Sales_INVOICE SI2 ON SID2.SysDocID = SI2.SysDocID AND SID2.VoucherID = SI2.VoucherID AND ISNULL(SI2.IsVoid,'False') = 'False' AND SI2.TransactionDate>='" + text + "' ) AS INV ON INV.OrderSysDocID = PLD.SysDocID\r\n                    AND INV.OrderVoucherID = PLD.VoucherID AND INV.OrderRowIndex = PLD.RowIndex\r\n\r\n                    --Cash Sales\r\n                    LEFT OUTER JOIN (SELECT SID3.Quantity,SID3.SysDocID,SID3.VoucherID,SID3.RowIndex, SID3.UnitPrice,SI3.TransactionDate\r\n                    FROM Sales_Invoice_Detail SID3 WITH (NOLOCK) INNER JOIN Sales_INVOICE SI3 ON SID3.SysDocID = SI3.SysDocID AND SID3.VoucherID = SI3.VoucherID AND ISNULL(SI3.IsVoid,'False') = 'False' AND SI3.TransactionDate>='" + text + "' ) AS CashInv\r\n                    ON CashInv.SysDocID = PLD.SysDocID  AND CashInv.VoucherID = PLD.VoucherID AND CashInv.RowIndex = PLD.RowIndex\r\n\r\n                        LEFT OUTER JOIN (SELECT DRD.Quantity,DR.SysDocID,DR.VoucherID,DRD.DNRowIndex,DR.DNoteSysDocID,DR.DNoteVoucherID FROM Delivery_Return DR WITH (NOLOCK)\r\n                        INNER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID WHERE   ISNULL(DR.IsVoid,'False') = 'False') DRD\r\n                        ON  DRD.DNoteSysDocID = PLD.SysDocID AND DRD.DNoteVoucherID = PLD.VoucherID  AND  DRD.DNRowIndex = PLD.RowIndex \r\n                         LEFT OUTER JOIN Sales_Return_Detail  SRD ON SRD.SourceSysDocID = PLD.SysDocID AND SRD.SourceVoucherID = PLD.VoucherID AND SRD.SourceRowIndex = PLD.RowIndex  \r\n                        LEFT OUTER JOIN (SELECT SI2.OrderSysDocID,SI2.OrderVoucherID,SI2.OrderRowIndex, SRD2.Quantity FROM Sales_Return_Detail SRD2 WITH (NOLOCK) INNER JOIN Sales_Invoice_Detail SI2 ON SRD2.SourceSysDocID=SI2.SysDocID\r\n                               AND SRD2.SourceVoucherID = SI2.voucherID AND SRD2.SourceRowIndex = SI2.rowIndex) CrSR ON CRSR.OrderSysDocID = PLD.SysDocID AND CRSR.OrderVoucherID = PLD.VoucherID AND CRSR.OrderRowIndex = PLD.RowIndex\r\n\r\n                               LEFT OUTER JOIN Product P ON P.ProductID = PLD.ProductID\r\n                             LEFT OUTER JOIN Customer CUS ON CUS.CUstomerID = IT.PayeeID AND IT.PayeeType = 'C') CON\r\n                              WHERE DeliveredQty > 0   \r\n              GROUP BY  RecordID, CON.CustomerID,Con.CustomerName,CON.Description,COn.DocName, CON.InvoiceDate, CON.InvoiceVoucherID,COn.LocationID, CON.ProductID ,CON.SysDocID,CON.SysDocType,CON.TransactionDate,CON.UnitPrice  , CON.VoucherID \r\n                    ORDER BY ItemCode,TransactionDate  ";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Product_Lot_Sales", exp);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   ISNULL(IsVoid,'False') AS 'V', SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Invoice Date],\r\n               CASE ISNULL(IsCash,'False') WHEN 'True' THEN 'Cash' ELSE 'Credit' END AS [Type],INV.SalespersonID [Salesperson],Total [Amount]\r\n               FROM         ConsignIn_Settlement INV\r\n               Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "ConsignIn_Settlement", sqlCommand);
			return dataSet;
		}

		public DataSet GetConsignInReceiptReport(DateTime fromrec, DateTime torec, DateTime fromset, DateTime toset, string fromvendor, string tovendor, string sysDocID, string voucherID, string vendorIDs)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromrec);
				string text2 = CommonLib.ToSqlDateTimeString(torec);
				string text3 = CommonLib.ToSqlDateTimeString(fromset);
				string text4 = CommonLib.ToSqlDateTimeString(toset);
				DataSet dataSet = new DataSet();
				string text5 = text5 = "SELECT CS.VendorID,V.VendorName,V.CountryID,CI.TransactionDate AS [CON Date],CS.ConsignSysDocID,CS.ConsignVoucherID,CS.TransactionDate AS [Set Date],CS.SysDocID,\r\n                       CS.VoucherID,CS.Total,CS.CommissionPercent,CS.CommissionAmount,\r\n                       (ISNULL(CS.Total,0)-ISNULL(CS.CommissionAmount,0)-ISNULL((SELECT SUM(CE.Amount) FROM ConsignIn_Expense CE WHERE CE.VoucherID=CS.VoucherID AND CE.SysDocID=CS.SysDocID),0)) AS NetTotal,\r\n                       (SELECT SUM(CE.Amount) FROM ConsignIn_Expense CE WHERE CE.ExpenseID IN ('CIN03') AND CE.VoucherID=CS.VoucherID AND CE.SysDocID=CS.SysDocID) AS [UNLOADING],\r\n                       (SELECT SUM(CE.Amount) FROM ConsignIn_Expense CE WHERE CE.ExpenseID IN ('CIN05') AND CE.VoucherID=CS.VoucherID AND CE.SysDocID=CS.SysDocID)AS [CLD/CHG],\r\n                       (SELECT SUM(IT.UnitPrice*(PLS.SoldQty-PLS.FOCQuantity)) AS SOLD\r\n                       FROM Product_Lot_Sales PLS INNER JOIN Inventory_Transactions IT ON PLS.DocID = IT.SysDocID AND PLS.InvoiceNumber=IT.VoucherID AND PLS.RowIndex=IT.RowIndex WHERE LotNo IN \r\n                       (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber=CS.ConsignVoucherID AND DocID =CS.ConsignSysDocID) OR SourceLotNumber IN \r\n                       (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber =CS.ConsignVoucherID AND DocID =CS.ConsignSysDocID)))) AS SOLD\r\n                       FROM ConsignIn_Settlement CS LEFT JOIN Consign_In CI ON CS.ConsignSysDocID=CI.SysDocID AND CS.ConsignVoucherID=CI.VoucherID\r\n                       LEFT JOIN Vendor V ON V.VendorID=CI.VendorID WHERE CI.TransactionDate Between '" + text + "' AND '" + text2 + "'\r\n                      AND CS.TransactionDate Between '" + text3 + "' AND '" + text4 + "'";
				if (vendorIDs != "")
				{
					text5 = text5 + " AND VendorID IN(" + vendorIDs + ")";
				}
				if (fromvendor != "")
				{
					text5 = text5 + "  AND CS.VendorID >= '" + fromvendor + "' ";
				}
				if (tovendor != "")
				{
					text5 = text5 + " AND CS.VendorID <= '" + tovendor + "' ";
				}
				if (voucherID != "")
				{
					text5 = text5 + " AND CS.ConsignSysDocID = '" + sysDocID + "'AND CS.ConsignVoucherID='" + voucherID + "' ";
				}
				text5 += " ORDER BY V.VendorName   ";
				FillDataSet(dataSet, "ConsignIn_Settlement", text5);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
