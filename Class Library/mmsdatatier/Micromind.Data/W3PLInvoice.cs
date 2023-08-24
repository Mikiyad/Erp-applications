using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class W3PLInvoice : StoreObject
	{
		private const string W3PLINVOICE_TABLE = "W3PL_Invoice";

		private const string W3PLINVOICEDETAIL_TABLE = "W3PL_Invoice_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

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

		private const string STOREQUANTITY_PARM = "@StoreQuantity";

		private const string DLQUANTITY_PARM = "@DLQuantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITPRICEFC_PARM = "@UnitPriceFC";

		private const string UNITEXPENSE_PARM = "@UnitExpense";

		private const string EXPENSEAMOUNT_PARM = "@ExpenseAmount";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITWEIGHT_PARM = "@UnitWeight";

		private const string TOTALWEIGHT_PARM = "@TotalWeight";

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

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string INVOICEPAYMENT_PARM = "@Invoice_Payment";

		private const string EXPENSEID_PARM = "@ExpenseID";

		private const string EXPENSENAME_PARM = "@ExpenseName";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string CONSIGNINSETTLEDITEMS_TABLE = "ConsignIn_Settled_Items";

		private const string SALESROWID_PARM = "@SalesRowID";

		private const string ITEMCODE_PARM = "@ItemCode";

		private const string PRICE_PARM = "@Price";

		public W3PLInvoice(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateInvoiceText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("W3PL_Invoice", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("DueDate", "@DueDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("CommissionPercent", "@CommissionPercent"), new FieldValue("CommissionAmount", "@CommissionAmount"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("IsCash", "@IsCash"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("W3PL_Invoice", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInvoiceCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInvoiceText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInvoiceText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
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
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
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

		private string GetInsertUpdateInvoiceDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("W3PL_Invoice_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitWeight", "@UnitWeight"), new FieldValue("TotalWeight", "@TotalWeight"), new FieldValue("DLQuantity", "@DLQuantity"), new FieldValue("StoreQuantity", "@StoreQuantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitExpense", "@UnitExpense"), new FieldValue("ExpenseAmount", "@ExpenseAmount"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SubunitPrice", "@SubunitPrice"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInvoiceDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateInvoiceDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateInvoiceDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@DLQuantity", SqlDbType.Real);
			parameters.Add("@StoreQuantity", SqlDbType.Real);
			parameters.Add("@UnitExpense", SqlDbType.Decimal);
			parameters.Add("@UnitWeight", SqlDbType.Decimal);
			parameters.Add("@TotalWeight", SqlDbType.Decimal);
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
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@DLQuantity"].SourceColumn = "DLQuantity";
			parameters["@StoreQuantity"].SourceColumn = "StoreQuantity";
			parameters["@UnitWeight"].SourceColumn = "UnitWeight";
			parameters["@TotalWeight"].SourceColumn = "TotalWeight";
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
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateExpenseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("W3PL_Expense", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Reference", "@Reference"), new FieldValue("CurrencyRate", "@CurrencyRate"));
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
			sqlBuilder.AddInsertUpdateParameters("W3PLInvoice_Settled_Items", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("SalesRowID", "@SalesRowID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Quantity", "@Quantity"));
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

		private bool ValidateData(W3PLInvoiceData journalData)
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
					text = "SELECT SID.SysDocID,SID.VoucherID,Quantity FROM W3PL_Invoice_Detail SID INNER JOIN W3PL_Invoice SI ON \r\n                         SID.VoucherID=SI.VoucherID AND SID.SysDocID=SI.SysDocID WHERE LocationID='" + locationID + "' AND ProductID='" + productID + "' AND ISNULL(IsRecost,'False')='True' ORDER BY SI.TransactionDate DESC";
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
									text = "UPDATE W3PL_Invoice_Detail SET IsRecost='False' WHERE LocationID='" + locationID + "' AND ProductID='" + productID + "' AND ISNULL(IsRecost,'False')='True'";
									flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
								}
							}
						}
						return flag;
					}
				}
				text = "UPDATE W3PL_Invoice_Detail SET IsRecost='False' WHERE LocationID='" + locationID + "' AND ProductID='" + productID + "' AND ISNULL(IsRecost,'False')='True'";
				return flag & (ExecuteNonQuery(text, sqlTransaction) >= 0);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool AllowModify(string sysDocID, string voucherNumber, DateTime date, SqlTransaction sqlTransaction)
		{
			string text = StoreConfiguration.ToSqlDateTimeString(date);
			string exp = "SELECT COUNT(*) FROM W3PL_Invoice_Detail WHERE SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherNumber + "' AND StartDate > '" + text + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public bool InsertUpdateInvoice(W3PLInvoiceData invoiceData, bool isUpdate)
		{
			return InsertUpdateInvoice(invoiceData, isUpdate, null);
		}

		public bool InsertUpdateInvoice(W3PLInvoiceData invoiceData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand sqlCommand = null;
			string text = "";
			try
			{
				DataRow dataRow = invoiceData.W3PLInvoiceTable.Rows[0];
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string text2 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				DateTime dateTime = DateTime.MinValue;
				foreach (DataRow row in invoiceData.W3PLInvoiceDetailTable.Rows)
				{
					DateTime dateTime2 = DateTime.Parse(row["StartDate"].ToString());
					if (dateTime2 > dateTime)
					{
						dateTime = dateTime2;
					}
				}
				ArrayList arrayList = new ArrayList();
				foreach (DataRow row2 in invoiceData.W3PLInvoiceDetailTable.Rows)
				{
					string text3 = row2["SourceSysDocID"].ToString();
					string text4 = row2["SourceVoucherID"].ToString();
					if (!arrayList.Contains(text3 + text4))
					{
						arrayList.Add(text3 + text4);
						if (!AllowModify(text3, text4, dateTime, sqlTransaction))
						{
							throw new CompanyException("Unable to modify or delete the transaction. Some of the items in this transaction are in use.", 1037);
						}
					}
				}
				dataRow["DueDate"] = dataRow["TransactionDate"];
				string termID = dataRow["TermID"].ToString();
				PaymentTermData termByID = new Terms(base.DBConfig).GetTermByID(termID);
				DataRow dataRow2 = null;
				if (termByID != null && termByID.Tables.Count > 0 && termByID.Tables[0].Rows.Count > 0)
				{
					dataRow2 = termByID.Tables[0].Rows[0];
					byte b = byte.Parse(dataRow2["TermType"].ToString());
					DateTime dateTime3 = DateTime.Parse(dataRow["TransactionDate"].ToString());
					switch (b)
					{
					case 1:
					{
						int num = int.Parse(dataRow2["NetDays"].ToString());
						DateTime dateTime5 = dateTime3.AddDays(num);
						dataRow["DueDate"] = dateTime5;
						break;
					}
					case 2:
					{
						dateTime3 = dateTime3.AddMonths(1);
						DateTime dateTime4 = new DateTime(dateTime3.Year, dateTime3.Month, 1, dateTime3.Hour, dateTime3.Minute, dateTime3.Second, dateTime3.Millisecond);
						dataRow["DueDate"] = dateTime4;
						break;
					}
					}
				}
				decimal num2 = default(decimal);
				foreach (DataRow row3 in invoiceData.W3PLInvoiceDetailTable.Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row3["Quantity"].ToString(), out result2);
					decimal.TryParse(row3["UnitPrice"].ToString(), out result3);
					decimal.TryParse(row3["Amount"].ToString(), out result);
					num2 += result;
				}
				dataRow["Total"] = num2;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("W3PL_Invoice", "VoucherID", dataRow["SysDocID"].ToString(), text2, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				bool flag2 = false;
				decimal result4 = 1m;
				string a = "M";
				dataRow["CurrencyID"].ToString();
				if (dataRow["CurrencyID"] != DBNull.Value && baseCurrencyID != dataRow["CurrencyID"].ToString())
				{
					flag2 = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result4);
					a = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
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
					foreach (DataRow row4 in invoiceData.ExpenseTable.Rows)
					{
						decimal result6 = default(decimal);
						decimal.TryParse(row4["Amount"].ToString(), out result6);
						if (a == "M")
						{
							row4["Amount"] = Math.Round(result6 * result4, currencyDecimalPoints);
						}
						else
						{
							row4["Amount"] = Math.Round(result6 / result4, currencyDecimalPoints);
						}
						row4["AmountFC"] = result6;
					}
				}
				foreach (DataRow row5 in invoiceData.W3PLInvoiceDetailTable.Rows)
				{
					float num3 = 0f;
					row5["SysDocID"] = dataRow["SysDocID"];
					row5["VoucherID"] = dataRow["VoucherID"];
					text = row5["ProductID"].ToString();
					string checkFieldValue = row5["LocationID"].ToString();
					int.Parse(row5["RowIndex"].ToString());
					decimal result7 = default(decimal);
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product_Location", "Quantity", "ProductID", text, "LocationID", checkFieldValue, sqlTransaction);
					if (fieldValue != null)
					{
						decimal.TryParse(fieldValue.ToString(), out result7);
					}
					string idFieldValue = row5["SourceSysDocID"].ToString();
					string checkFieldValue2 = row5["SourceVoucherID"].ToString();
					string checkFieldValue3 = row5["SourceRowIndex"].ToString();
					object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Consign_In_Detail", "QuantitySettled", "SysDocID", idFieldValue, "VoucherID", checkFieldValue2, "RowIndex", checkFieldValue3, sqlTransaction);
					float num4 = 0f;
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						num4 = float.Parse(fieldValue2.ToString());
					}
					fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Consign_In_Detail", "QuantityReturned", "SysDocID", idFieldValue, "VoucherID", checkFieldValue2, "RowIndex", checkFieldValue3, sqlTransaction);
					float num5 = 0f;
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						num5 = float.Parse(fieldValue2.ToString());
					}
					fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Consign_In_Detail", "Quantity", "SysDocID", idFieldValue, "VoucherID", checkFieldValue2, "RowIndex", checkFieldValue3, sqlTransaction);
					float num6 = 0f;
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						num6 = float.Parse(fieldValue2.ToString());
					}
					if (num4 + num5 > num6)
					{
						throw new CompanyException("Invoice quantity cannot be greater than balance quantity.", 1026);
					}
					num3 = float.Parse(row5["Quantity"].ToString());
					string text5 = "";
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text, sqlTransaction);
					if (fieldValue != null)
					{
						text5 = fieldValue.ToString();
					}
					if (text5 != "" && row5["UnitID"] != DBNull.Value && row5["UnitID"].ToString() != text5)
					{
						DataRow obj3 = new Products(base.DBConfig).GetProductUnitRow(text, row5["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text + "\nUnit:" + row5["UnitID"].ToString());
						float num7 = float.Parse(obj3["Factor"].ToString());
						string text6 = obj3["FactorType"].ToString();
						num3 = float.Parse(row5["Quantity"].ToString());
						row5["UnitFactor"] = num7;
						row5["FactorType"] = text6;
						row5["UnitQuantity"] = row5["Quantity"];
						num3 = ((!(text6 == "M")) ? float.Parse(Math.Round(num3 * num7, 5).ToString()) : float.Parse(Math.Round(num3 / num7, 5).ToString()));
						row5["Quantity"] = num3;
					}
					if (flag2)
					{
						decimal result8 = default(decimal);
						decimal result9 = default(decimal);
						row5["UnitPriceFC"] = row5["UnitPrice"];
						row5["AmountFC"] = row5["Amount"];
						decimal.TryParse(row5["UnitPrice"].ToString(), out result8);
						decimal.TryParse(row5["Amount"].ToString(), out result9);
						result8 = ((!(a == "M")) ? Math.Round(result8 / result4, 4) : Math.Round(result8 * result4, 4));
						row5["UnitPrice"] = result8;
						result9 = ((!(a == "M")) ? Math.Round(result9 / result4, currencyDecimalPoints) : Math.Round(result9 * result4, currencyDecimalPoints));
						row5["Amount"] = result9;
					}
				}
				if (isUpdate)
				{
					flag &= DeleteInvoiceDetailsRows(sysDocID, text2, isDeletingTransaction: false, sqlTransaction);
				}
				sqlCommand = GetInsertUpdateInvoiceCommand(isUpdate);
				sqlCommand.Transaction = sqlTransaction;
				if (!isUpdate)
				{
					if (invoiceData.Tables["W3PL_Invoice"].Rows.Count > 0)
					{
						flag &= Insert(invoiceData, "W3PL_Invoice", sqlCommand);
					}
				}
				else
				{
					flag &= Update(invoiceData, "W3PL_Invoice", sqlCommand);
				}
				sqlCommand = GetInsertUpdateInvoiceDetailsCommand(isUpdate: false);
				sqlCommand.Transaction = sqlTransaction;
				if (invoiceData.Tables["W3PL_Invoice_Detail"].Rows.Count > 0)
				{
					flag &= Insert(invoiceData, "W3PL_Invoice_Detail", sqlCommand);
				}
				foreach (DataRow row6 in invoiceData.ExpenseTable.Rows)
				{
					row6["SysDocID"] = dataRow["SysDocID"];
					row6["VoucherID"] = dataRow["VoucherID"];
				}
				sqlCommand = GetInsertUpdateExpenseCommand(isUpdate: false);
				sqlCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					DeleteExpenseRows(sysDocID, text2, sqlTransaction);
				}
				if (invoiceData.Tables["W3PL_Expense"].Rows.Count > 0)
				{
					flag &= Insert(invoiceData, "W3PL_Expense", sqlCommand);
				}
				GLData journalData = CreateInvoiceGLData(invoiceData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("W3PL_Invoice", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Rental Invoice";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "W3PL_Invoice", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.W3PLInvoice, sysDocID, text2, "W3PL_Invoice", sqlTransaction);
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
				string exp = "UPDATE Journal_Details SET IsBilled = 'False' WHERE JournalDetailID In (SELECT SourceRowIndex FROM W3PL_Expense WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "')";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				string commandText = "DELETE FROM W3PL_Expense WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				string commandText = "DELETE FROM W3PLInvoice_Settled_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateInvoiceGLData(W3PLInvoiceData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.W3PLInvoiceTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string text = dataRow["CustomerID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				dataRow["VoucherID"].ToString();
				string textCommand = "SELECT SD.LocationID,ISNULL(SD.SalesAccountID,LOC.SalesAccountID) AS IncomeAccountID, ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID ,LOC.COGSAccountID,LOC.DiscountGivenAccountID,\r\n                                LOC.InventoryAccountID,LOC.SalesAccountID,LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
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
				string value = dataRow2["ARAccountID"].ToString();
				string value2 = dataRow2["IncomeAccountID"].ToString();
				string a = dataRow2["BaseCurrencyID"].ToString();
				bool flag = false;
				decimal result = 1m;
				if (dataRow["CurrencyID"] != DBNull.Value && a != dataRow["CurrencyID"].ToString())
				{
					flag = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result);
				}
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.W3PLInvoice;
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["CurrencyID"] = dataRow["CurrencyID"];
				dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Rental Invoice - ";
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
				foreach (DataRow row in transactionData.W3PLInvoiceDetailTable.Rows)
				{
					decimal result2 = default(decimal);
					if (flag)
					{
						decimal.TryParse(row["AmountFC"].ToString(), out result2);
					}
					else
					{
						decimal.TryParse(row["Amount"].ToString(), out result2);
					}
					num2 += Math.Round(result2, currencyDecimalPoints);
				}
				DataRow dataRow5;
				if (num2 != 0m)
				{
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = value;
					dataRow5["PayeeID"] = text;
					dataRow5["PayeeType"] = "C";
					dataRow5["IsARAP"] = true;
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
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				decimal d = default(decimal);
				foreach (DataRow row2 in transactionData.ExpenseTable.Rows)
				{
					int num3 = -1;
					if (row2["SourceRowIndex"] != DBNull.Value)
					{
						num3 = int.Parse(row2["SourceRowIndex"].ToString());
					}
					string text3 = row2["ExpenseID"].ToString();
					num = default(decimal);
					num = ((!flag) ? decimal.Parse(row2["Amount"].ToString()) : decimal.Parse(row2["AmountFC"].ToString()));
					string value3 = "";
					if (num3 > 0)
					{
						textCommand = "SELECT AccountID FROM Journal_Details JD WHERE JournalDetailID = " + num3;
						object obj = ExecuteScalar(textCommand, sqlTransaction);
						if (obj != null && obj.ToString() != "")
						{
							value3 = obj.ToString();
						}
					}
					else
					{
						value3 = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text3, sqlTransaction);
					}
					decimal result3 = 1m;
					decimal.TryParse(row2["CurrencyRate"].ToString(), out result3);
					num = Math.Round(num, currencyDecimalPoints);
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = value3;
					if (flag)
					{
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
					dataRow5["Reference"] = text3;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
					d += num;
				}
				dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["DueDate"] = dataRow["DueDate"];
				dataRow5["AccountID"] = value2;
				dataRow5["PayeeID"] = text;
				decimal num4 = num2 - d;
				if (flag)
				{
					if (num4 >= 0m)
					{
						dataRow5["DebitFC"] = DBNull.Value;
						dataRow5["CreditFC"] = num4;
					}
					else
					{
						dataRow5["DebitFC"] = Math.Abs(num4);
						dataRow5["CreditFC"] = DBNull.Value;
					}
				}
				else if (num4 >= 0m)
				{
					dataRow5["Debit"] = DBNull.Value;
					dataRow5["Credit"] = num4;
				}
				else
				{
					dataRow5["Debit"] = Math.Abs(num4);
					dataRow5["Credit"] = DBNull.Value;
				}
				dataRow5["Reference"] = dataRow["Reference"];
				dataRow5["Description"] = dataRow["Note"];
				dataRow5.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow5);
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public W3PLInvoiceData GetInvoiceByID(string sysDocID, string voucherID)
		{
			return GetInvoiceByID(sysDocID, voucherID, null);
		}

		internal W3PLInvoiceData GetInvoiceByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				W3PLInvoiceData w3PLInvoiceData = new W3PLInvoiceData();
				string text = "SELECT * FROM W3PL_Invoice WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				new SqlCommand(text).Transaction = sqlTransaction;
				FillDataSet(w3PLInvoiceData, "W3PL_Invoice", text, sqlTransaction);
				if (w3PLInvoiceData == null || w3PLInvoiceData.Tables.Count == 0 || w3PLInvoiceData.Tables["W3PL_Invoice"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT SD.*,P.ItemType\r\n                        FROM W3PL_Invoice_Detail SD INNER JOIN Product P ON SD.ProductID=P.ProductID\r\n                       \r\n                        WHERE SD.VoucherID='" + voucherID + "' AND SD.SysDocID='" + sysDocID + "'";
				FillDataSet(w3PLInvoiceData, "W3PL_Invoice_Detail", text, sqlTransaction);
				text = "SELECT * FROM W3PL_Expense\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(w3PLInvoiceData, "W3PL_Expense", text);
				return w3PLInvoiceData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteInvoiceDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				W3PLInvoiceData w3PLInvoiceData = new W3PLInvoiceData();
				string textCommand = "SELECT SOD.SourceSysDocID,SOD.SourceVoucherID,SOD.*,ISNULL(ISVOID,'False') AS IsVoid\r\n                                    FROM W3PL_Invoice_Detail SOD INNER JOIN W3PL_Invoice SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                                    WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(w3PLInvoiceData, "W3PL_Invoice_Detail", textCommand, sqlTransaction);
				if (w3PLInvoiceData.W3PLInvoiceDetailTable.Rows.Count == 0)
				{
					return true;
				}
				bool result = false;
				bool.TryParse(w3PLInvoiceData.W3PLInvoiceDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				textCommand = "DELETE FROM W3PL_Invoice_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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

		public bool VoidInvoice(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidInvoice(sysDocID, voucherID, isVoid, null);
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

		private bool VoidInvoice(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				W3PLInvoiceData w3PLInvoiceData = new W3PLInvoiceData();
				string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid FROM W3PL_Invoice_Detail SOD INNER JOIN W3PL_Invoice SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(w3PLInvoiceData, "W3PL_Invoice_Detail", textCommand, sqlTransaction);
				DateTime dateTime = DateTime.MinValue;
				foreach (DataRow row in w3PLInvoiceData.W3PLInvoiceDetailTable.Rows)
				{
					DateTime dateTime2 = DateTime.Parse(row["StartDate"].ToString());
					if (dateTime2 > dateTime)
					{
						dateTime = dateTime2;
					}
				}
				ArrayList arrayList = new ArrayList();
				foreach (DataRow row2 in w3PLInvoiceData.W3PLInvoiceDetailTable.Rows)
				{
					string text = row2["SourceSysDocID"].ToString();
					string text2 = row2["SourceVoucherID"].ToString();
					if (!arrayList.Contains(text + text2))
					{
						arrayList.Add(text + text2);
						if (!AllowModify(text, text2, dateTime, sqlTransaction))
						{
							throw new CompanyException("Unable to modify or delete the transaction. Some of the items in this transaction are in use.", 1037);
						}
					}
				}
				bool result = false;
				bool.TryParse(w3PLInvoiceData.W3PLInvoiceDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (result == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				textCommand = "UPDATE W3PL_Invoice SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("W3PL Rent Invoice", voucherID, sysDocID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteInvoice(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("W3PL_Invoice", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidInvoice(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeleteInvoiceDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM W3PL_Invoice WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Consign In Invoice", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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
				string str = "SELECT COUNT(RowIndex)FROM W3PL_Invoice_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				str += " AND CASE WHEN UnitQuantity IS NULL THEN Quantity ELSE UnitQuantity END - ISNULL(QuantityShipped,0)=0";
				object obj = ExecuteScalar(str, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				str = "UPDATE W3PL_Invoice SET IsDelivered = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityShipped FROM W3PL_Invoice_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				textCommand = "UPDATE W3PL_Invoice_Detail SET QuantityShipped=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer] FROM W3PL_Invoice SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID  WHERE ISNULL(IsDelivered,0)=0";
				if (vendorID != "")
				{
					text = text + " AND SO.CustomerID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "W3PL_Invoice", text);
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

		public DataSet GetInvoiceToPrint(string sysDocID, string voucherID)
		{
			return GetInvoiceToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetInvoiceToPrint(string sysDocID, string[] voucherID)
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
				string text2 = "SELECT  DISTINCT   SI.SysDocID,SI.VoucherID,SI.CustomerID,CustomerName,SI.CustomerAddress,SI.TransactionDate,\r\n                                IsCash,SI.SalesPersonID,SI.RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                CommissionAmount,CommissionPercent,SI.DateCreated,SI.DateUpdated,SI.CreatedBy,SI.UpdatedBy,CI.ContainerNo,CI.TransactionDate AS [Arrival Date],\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,SI.IsVoid,SI.Reference,ISNULL(SI.DiscountFC,SI.Discount) AS Discount,\r\n                                (SELECT SUM(CE.Amount) FROM ConsignIn_Expense CE WHERE CE.SysDocID='" + sysDocID + "' AND CE.VoucherID IN (" + text + ")) AS [TotalExpense],";
				text2 = text2 + "ISNULL(ISNULL(TaxAmountFC,TaxAmount) ,0) AS Tax,ISNULL(SI.TotalFC,SI.Total) AS Total,SI.PONumber,SI.Note,SI.ConsignSysDocID,SI.ConsignVoucherID,SI.PONUMBER as Invoice#\r\n                                FROM  W3PL_Invoice SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT JOIN Consign_In CI ON CI.SysDocID=SI.ConsignSysDocID AND CI.VoucherID=SI.ConsignVoucherID\r\n                                WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(text2);
				FillDataSet(dataSet, "W3PL_Invoice", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["W3PL_Invoice"].Rows.Count == 0)
				{
					return null;
				}
				text2 = "SELECT Temp.*,CID.Quantity,CID.QuantityDamage,CID.QuantityReturned,CID.QuantitySettled,CID.QuantitySold,\r\n                        (SELECT (SUM(ISNULL(FOCQuantity,0)+CASE WHEN SD.SysDocType IN (87) THEN SoldQty ELSE 0 END)) \r\n                        FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID \r\n                        WHERE LotNo IN \r\n                        (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = TEMP.SourceVoucherID AND DocID = Temp.SourceSysDocID) OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = TEMP.SourceVoucherID AND DocID = Temp.SourceSysDocID)))\r\n                        AND SysDocType NOT IN (19,20,21,40) AND ItemCode=Temp.ProductID )AS FOCQuantity\r\n                        FROM \r\n                        (SELECT  CSD.SysDocID,CSD.VoucherID,CSD.SourceSysDocID,CSD.SourceVoucherID,CSD.ProductID,CSD.Description,SUM(ISNULL(CSD.UnitQuantity,CSD.Quantity)) AS Quantity,\r\n                        SUM( (ISNULL(CSD.UnitPriceFC,CSD.UnitPrice))* (ISNULL(CSD.UnitQuantity,CSD.Quantity)))/SUM(ISNULL(CSD.UnitQuantity,CSD.Quantity)) AS Price,\r\n                        SUM(ISNULL(CSD.UnitQuantity,CSD.Quantity)*ISNULL(CSD.UnitPriceFC,CSD.UnitPrice)) AS Total,CSD.UnitID,CSD.LocationID,\r\n\t\t\t\t\t\tCSD.DLQuantity,CSD.StoreQuantity,CSD.StartDate,CSD.EndDate,CSD.UnitWeight,CSD.TotalWeight,CSD.Amount,CSD.ExpenseAmount,CSD.UnitExpense\r\n                        FROM  W3PL_Invoice_Detail CSD  \r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				text2 += " GROUP BY CSD.SysDocID,CSD.VoucherID,CSD.ProductID,CSD.Description,CSD.UnitID,CSD.LocationID,CSD.SourceSysDocID,CSD.SourceVoucherID,\r\n\t\t\t\t\t\tCSD.DLQuantity,CSD.StoreQuantity,CSD.StartDate,CSD.EndDate,CSD.UnitWeight,CSD.TotalWeight,CSD.Amount,CSD.ExpenseAmount,CSD.UnitExpense\r\n\t\t\t\t\t\t) AS TEMP LEFT JOIN Consign_In_Detail CID ON CID.SysDocID=Temp.SourceSysDocID AND CID.VoucherID=Temp.SourceVoucherID  AND CID.ProductID=Temp.ProductID ";
				FillDataSet(dataSet, "W3PL_Invoice_Detail", text2);
				text2 = "SELECT EC.ExpenseName,EC.ExpenseID AS MstCode,CinE.* FROM ConsignIn_Expense CinE LEFT JOIN  Expense_Code EC ON CinE.ExpenseID=EC.ExpenseID WHERE CinE.SysDocID='" + sysDocID + "' AND CinE.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "W3PL_Expense", text2);
				dataSet.Relations.Add("CustomerInvoice", new DataColumn[2]
				{
					dataSet.Tables["W3PL_Invoice"].Columns["SysDocID"],
					dataSet.Tables["W3PL_Invoice"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["W3PL_Invoice_Detail"].Columns["SysDocID"],
					dataSet.Tables["W3PL_Invoice_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("ConsignInExpense", new DataColumn[2]
				{
					dataSet.Tables["W3PL_Invoice"].Columns["SysDocID"],
					dataSet.Tables["W3PL_Invoice"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["W3PL_Expense"].Columns["SysDocID"],
					dataSet.Tables["W3PL_Expense"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["W3PL_Invoice"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["W3PL_Invoice"].Rows)
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
			string textCommand = "SELECT Con.SysDocID as DocID,CON.InvoiceVoucherID AS InvoiceNumber, RecordID, CON.CustomerID,ISNULL(Con.CustomerName,'Non Sale Issue') AS CustomerName,\r\n                        CASE WHEN SysDocType IN (87) THEN SUM(CON.DeliveredQty) ELSE 0 END  + SUM(CON.FOCQuantity) AS FOC,CON.InvoiceDate,SUM(CON.InvoiceQty - FOCQuantity) AS QtyInvoiced,\r\n                       CON.ProductID AS ItemCode,CON.SysDocID,CON.SysDocType,CON.UnitPrice AS UnitPrice,\r\n                        CON.VoucherID, ISNULL(Round(SUM(CON.InvoiceQty - FOCQuantity) * Con.UnitPrice,2),0) AS Amount FROM \r\n\r\n\r\n                        (SELECT PLD.*,P.Description, IT.TransactionDate,CASE WHEN IT.PayeeType = 'C' THEN IT.PayeeID ELSE NULL END AS CustomerID ,\r\n                        CUS.CustomerName, ISNULL(INV.VoucherID,PLD.VoucherID) AS InvoiceVoucherID, \r\n                         CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(INV.UnitPrice,CashInv.UnitPrice)  END AS UnitPrice,ISNULL(DRD.Quantity,0) + ISNULL(SRD.Quantity,0) AS QuantityReturned,\r\n                         CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(SoldQty,0)- (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))  END AS InvoiceQty ,ISNULL(INV.TransactionDate,CashInv.TransactionDate) AS InvoiceDate,\r\n                         ISNULL(SoldQty,0) - (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))  AS DeliveredQty\r\n                          FROM ( \r\n  \r\n  \r\n                          SELECT SD.SysDocType,SD.DocName,PLS.RecordID, PLS.DocID AS SysDocID, InvoiceNumber AS VoucherID,RowIndex,ItemCode AS ProductID,PLS.LocationID,LotNo AS LotNumber,SoldQty,ISNULL(FOCQuantity,0) AS FOCQuantity\r\n                          FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID WHERE LotNo IN \r\n                          (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "') OR SourceLotNumber IN \r\n                          (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n                           AND SysDocType NOT IN (19,20,21,40) AND ISNULL(IsSettled,'False') = 'False'\r\n                         UNION\r\n\t\t                   SELECT SD.SysDocType,SD.DocName,-1 AS RecordID,PLS.SysDocID, VoucherID,RowIndex, ProductID,PLS.LocationID,SourceLotNumber AS  LotNumber,-1 * LotQty AS SoldQty,0 AS FOCQuantity\r\n\t\t                    FROM Product_Lot_Receiving_Detail PLS INNER JOIN System_Document SD ON PLS.SysDocID = SD.SysDocID WHERE SourceLotNumber IN \r\n\t\t\t                  (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')\r\n\t\t\t                   OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n\t\t\t                   AND SysDocType NOT IN (19,20,21,40))  AS PLD\r\n\r\n\t\t\t                    LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = PLD.SysDocID AND IT.VoucherID = PLD.VoucherID AND IT.RowIndex = PLD.RowIndex\r\n\t\t\t\t                 LEFT OUTER JOIN  (SELECT SID2.Quantity,SID2.OrderSysDocID,SID2.VoucherID,SID2.OrderVoucherID,SID2.OrderRowIndex,SID2.UnitPrice,SI2.TransactionDate\r\n\t\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID2 INNER JOIN Sales_INVOICE SI2 ON SID2.SysDocID = SI2.SysDocID AND SID2.VoucherID = SI2.VoucherID AND ISNULL(SI2.IsVoid,'False') = 'False' ) AS INV ON INV.OrderSysDocID = PLD.SysDocID\r\n\t\t\t\t\t\t\t\t AND INV.OrderVoucherID = PLD.VoucherID AND INV.OrderRowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t\t\t\t\t --Cash Sales\r\n\t\t\t\t\t\t\t\t LEFT OUTER JOIN (SELECT SID3.Quantity,SID3.SysDocID,SID3.VoucherID,SID3.RowIndex, SID3.UnitPrice,SI3.TransactionDate\r\n\t\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID3 INNER JOIN Sales_INVOICE SI3 ON SID3.SysDocID = SI3.SysDocID AND SID3.VoucherID = SI3.VoucherID AND ISNULL(SI3.IsVoid,'False') = 'False' ) AS CashInv\r\n\t\t\t\t\t\t\t\t ON CashInv.SysDocID = PLD.SysDocID  AND CashInv.VoucherID = PLD.VoucherID AND CashInv.RowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t                     LEFT OUTER JOIN (SELECT DRD.Quantity,DR.SysDocID,DR.VoucherID,DRD.DNRowIndex,DR.DNoteSysDocID,DR.DNoteVoucherID FROM Delivery_Return DR\r\n\t\t\t\t\t                 INNER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID WHERE   ISNULL(DR.IsVoid,'False') = 'False') DRD\r\n\t\t\t\t\t\t\t\t\t ON  DRD.DNoteSysDocID = PLD.SysDocID AND DRD.DNoteVoucherID = PLD.VoucherID  AND  DRD.DNRowIndex = PLD.RowIndex \r\n\t\t\t\t\t                  LEFT OUTER JOIN Sales_Return_Detail  SRD ON SRD.SourceSysDocID = PLD.SysDocID AND SRD.SourceVoucherID = PLD.VoucherID AND SRD.SourceRowIndex = PLD.RowIndex  \r\n\t\t\t\t\t                 LEFT OUTER JOIN (SELECT SI2.OrderSysDocID,SI2.OrderVoucherID,SI2.OrderRowIndex, SRD2.Quantity FROM Sales_Return_Detail SRD2 INNER JOIN Sales_Invoice_Detail SI2 ON SRD2.SourceSysDocID=SI2.SysDocID\r\n\t\t\t\t\t\t\t\t\t\t\tAND SRD2.SourceVoucherID = SI2.voucherID AND SRD2.SourceRowIndex = SI2.rowIndex) CrSR ON CRSR.OrderSysDocID = PLD.SysDocID AND CRSR.OrderVoucherID = PLD.VoucherID AND CRSR.OrderRowIndex = PLD.RowIndex\r\n\r\n                                            LEFT OUTER JOIN Product P ON P.ProductID = PLD.ProductID\r\n\t\t\t\t\t                      LEFT OUTER JOIN Customer CUS ON CUS.CUstomerID = IT.PayeeID AND IT.PayeeType = 'C') CON\r\n\t\t\t\t\t\t                   WHERE DeliveredQty > 0   \r\n\t\t\t\t\t\t   GROUP BY  RecordID, CON.CustomerID,Con.CustomerName,CON.Description,COn.DocName, CON.InvoiceDate, CON.InvoiceVoucherID,COn.LocationID, CON.ProductID ,CON.SysDocID,CON.SysDocType,CON.TransactionDate,CON.UnitPrice  , CON.VoucherID \r\n                                 ORDER BY ItemCode,TransactionDate  ";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Product_Lot_Sales", textCommand);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   ISNULL(IsVoid,'False') AS 'V', SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Invoice Date],\r\n                            CASE ISNULL(IsCash,'False') WHEN 'True' THEN 'Cash' ELSE 'Credit' END AS [Type],INV.SalespersonID [Salesperson],Total [Amount]\r\n                            FROM         W3PL_Invoice INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "W3PL_Invoice", sqlCommand);
			return dataSet;
		}

		public DataSet GetConsignInReceiptReport(DateTime fromrec, DateTime torec, DateTime fromset, DateTime toset, string fromvendor, string tovendor, string sysDocID, string voucherID)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromrec);
				string text2 = CommonLib.ToSqlDateTimeString(torec);
				string text3 = CommonLib.ToSqlDateTimeString(fromset);
				string text4 = CommonLib.ToSqlDateTimeString(toset);
				DataSet dataSet = new DataSet();
				string text5 = text5 = "SELECT CS.CustomerID,V.CustomerName,V.CountryID,CI.TransactionDate AS [CON Date],CS.ConsignSysDocID,CS.ConsignVoucherID,CS.TransactionDate AS [Set Date],CS.SysDocID,\r\n                                    CS.VoucherID,CS.Total,CS.CommissionPercent,CS.CommissionAmount,\r\n                                    (SELECT SUM(CE.Amount) FROM ConsignIn_Expense CE WHERE CE.ExpenseID IN ('CIN03') AND CE.VoucherID=CS.VoucherID AND CE.SysDocID=CS.SysDocID) AS [UNLOADING],\r\n                                    (SELECT SUM(CE.Amount) FROM ConsignIn_Expense CE WHERE CE.ExpenseID IN ('CIN05') AND CE.VoucherID=CS.VoucherID AND CE.SysDocID=CS.SysDocID)AS [CLD/CHG],\r\n                                    (SELECT -1*SUM(IT.UnitPrice*IT.Quantity) AS SOLD\r\n                                    FROM Product_Lot_Sales PLS INNER JOIN Inventory_Transactions IT ON PLS.DocID = IT.SysDocID AND PLS.InvoiceNumber=IT.VoucherID AND PLS.RowIndex=IT.RowIndex WHERE LotNo IN \r\n                                    (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber=CS.ConsignVoucherID AND DocID =CS.ConsignSysDocID) OR SourceLotNumber IN \r\n                                    (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber =CS.ConsignVoucherID AND DocID =CS.ConsignSysDocID)))) AS SOLD\r\n                                    FROM W3PL_Invoice CS LEFT JOIN Consign_In CI ON CS.ConsignSysDocID=CI.SysDocID AND CS.ConsignVoucherID=CI.VoucherID\r\n                                    LEFT JOIN Customer V ON V.CustomerID=CI.CustomerID WHERE CI.TransactionDate Between '" + text + "' AND '" + text2 + "'\r\n                                   AND CS.TransactionDate Between '" + text3 + "' AND '" + text4 + "'";
				if (fromvendor != "")
				{
					text5 = text5 + "  AND CS.CustomerID >= '" + fromvendor + "' ";
				}
				if (tovendor != "")
				{
					text5 = text5 + " AND CS.CustomerID <= '" + tovendor + "' ";
				}
				if (voucherID != "")
				{
					text5 = text5 + " AND CS.ConsignSysDocID = '" + sysDocID + "'AND CS.ConsignVoucherID='" + voucherID + "' ";
				}
				text5 += " ORDER BY V.CustomerName   ";
				FillDataSet(dataSet, "W3PL_Invoice", text5);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
