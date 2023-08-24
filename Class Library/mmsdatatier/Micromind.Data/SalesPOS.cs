using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class SalesPOS : StoreObject
	{
		private const string SALESPOS_TABLE = "Sales_POS";

		private const string SALESPOSDETAIL_TABLE = "Sales_POS_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

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

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string SHIFTID_PARM = "@ShiftID";

		private const string BATCHID_PARM = "@BatchID";

		private const string PRICEINCLUDETAX_PARM = "@PriceIncludeTax";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string DISCOUNTFC_PARM = "@DiscountFC";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TOTAL_PARM = "@Total";

		private const string TOTALFC_PARM = "@TotalFC";

		private const string ISCASH_PARM = "@IsCash";

		private const string DUEDATE_PARM = "@DueDate";

		private const string SEARCHVALUE_PARM = "@SearchValue";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITPRICEFC_PARM = "@UnitPriceFC";

		private const string DESCRIPTION_PARM = "@Description";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

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

		private const string BARCODE_PARM = "@Barcode";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string PAYMENTMETHODID_PARM = "@PaymentMethodID";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string CHANGE_PARM = "@Change";

		private const string INVOICEPAYMENT_PARM = "@Invoice_Payment";

		public SalesPOS(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSalesPOSText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_POS", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("DueDate", "@DueDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("BatchID", "@BatchID"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("ShiftID", "@ShiftID"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("SearchValue", "@SearchValue"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("IsCash", "@IsCash"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Note", "@Note"), new FieldValue("Change", "@Change"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Sales_POS", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesPOSCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesPOSText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesPOSText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@ShiftID", SqlDbType.Int);
			parameters.Add("@BatchID", SqlDbType.Int);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@SearchValue", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@DiscountFC", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@TotalFC", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@IsCash", SqlDbType.Bit);
			parameters.Add("@Change", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@PriceIncludeTax"].SourceColumn = "PriceIncludeTax";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@BatchID"].SourceColumn = "BatchID";
			parameters["@ShiftID"].SourceColumn = "ShiftID";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			parameters["@SearchValue"].SourceColumn = "SearchValue";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@DiscountFC"].SourceColumn = "DiscountFC";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@TotalFC"].SourceColumn = "TotalFC";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@IsCash"].SourceColumn = "IsCash";
			parameters["@Change"].SourceColumn = "Change";
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

		private string GetInsertUpdateSalesPOSDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_POS_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("Discount", "@Discount"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("FactorType", "@FactorType"), new FieldValue("OrderSysDocID", "@OrderSysDocID"), new FieldValue("OrderVoucherID", "@OrderVoucherID"), new FieldValue("DNoteSysDocID", "@DNoteSysDocID"), new FieldValue("DNoteVoucherID", "@DNoteVoucherID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("IsDNRow", "@IsDNRow"), new FieldValue("Barcode", "@Barcode"), new FieldValue("IsRecost", "@IsRecost"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesPOSDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesPOSDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesPOSDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@OrderSysDocID", SqlDbType.NVarChar);
			parameters.Add("@OrderVoucherID", SqlDbType.NVarChar);
			parameters.Add("@DNoteSysDocID", SqlDbType.NVarChar);
			parameters.Add("@DNoteVoucherID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@OrderRowIndex", SqlDbType.Int);
			parameters.Add("@IsDNRow", SqlDbType.Bit);
			parameters.Add("@IsRecost", SqlDbType.Bit);
			parameters.Add("@Barcode", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
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
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@OrderVoucherID"].SourceColumn = "OrderVoucherID";
			parameters["@OrderSysDocID"].SourceColumn = "OrderSysDocID";
			parameters["@DNoteSysDocID"].SourceColumn = "DNoteSysDocID";
			parameters["@DNoteVoucherID"].SourceColumn = "DNoteVoucherID";
			parameters["@OrderRowIndex"].SourceColumn = "OrderRowIndex";
			parameters["@IsDNRow"].SourceColumn = "IsDNRow";
			parameters["@IsRecost"].SourceColumn = "IsRecost";
			parameters["@Barcode"].SourceColumn = "Barcode";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdatePaymentText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Invoice_Payment", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("AccountID", "@AccountID"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Amount", "@Amount"), new FieldValue("PaymentMethodID", "@PaymentMethodID"), new FieldValue("PaymentMethodType", "@PaymentMethodType"));
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
			parameters.Add("@PaymentMethodID", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@PaymentMethodID"].SourceColumn = "PaymentMethodID";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(SalesPOSData journalData)
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
					text = "SELECT SID.SysDocID,SID.VoucherID,Quantity FROM Sales_POS_Detail SID INNER JOIN Sales_POS SI ON \r\n                         SID.VoucherID=SI.VoucherID AND SID.SysDocID=SI.SysDocID WHERE LocationID='" + locationID + "' AND ProductID='" + productID + "' AND ISNULL(IsRecost,'False')='True' ORDER BY SI.TransactionDate DESC";
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
									text = "UPDATE Sales_POS_Detail SET IsRecost='False' WHERE LocationID='" + locationID + "' AND ProductID='" + productID + "' AND ISNULL(IsRecost,'False')='True'";
									flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
								}
							}
						}
						return flag;
					}
				}
				text = "UPDATE Sales_POS_Detail SET IsRecost='False' WHERE LocationID='" + locationID + "' AND ProductID='" + productID + "' AND ISNULL(IsRecost,'False')='True'";
				return flag & (ExecuteNonQuery(text, sqlTransaction) >= 0);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool InsertUpdateSalesPOS(SalesPOSData salesInvoiceData, bool isUpdate)
		{
			return InsertUpdateSalesPOS(salesInvoiceData, isUpdate, null);
		}

		public bool InsertUpdateSalesPOS(SalesPOSData salesInvoiceData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand sqlCommand = null;
			string text = "";
			try
			{
				DataRow dataRow = salesInvoiceData.SalesPOSTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				string idFieldValue = dataRow["RegisterID"].ToString();
				string text3 = (string)(dataRow["SysDocID"] = new Databases(base.DBConfig).GetFieldValue("POS_CashRegister", "ReceiptDocID", "CashRegisterID", idFieldValue, sqlTransaction).ToString());
				bool result = false;
				bool.TryParse(dataRow["IsCash"].ToString(), out result);
				dataRow["DueDate"] = dataRow["TransactionDate"];
				dataRow["TermID"].ToString();
				decimal num = default(decimal);
				foreach (DataRow row in salesInvoiceData.SalesPOSDetailTable.Rows)
				{
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result3);
					decimal.TryParse(row["UnitPrice"].ToString(), out result4);
					decimal.TryParse(row["Amount"].ToString(), out result2);
					num += result2;
				}
				string text4 = dataRow["VoucherID"].ToString();
				string text5 = text4;
				if (!isUpdate)
				{
					text5 = text4;
				}
				dataRow["Total"] = num;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Sales_POS", "VoucherID", dataRow["SysDocID"].ToString(), text5, sqlTransaction))
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
				foreach (DataRow row2 in salesInvoiceData.SalesPOSDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					text = row2["ProductID"].ToString();
					string checkFieldValue = row2["LocationID"].ToString();
					decimal result7 = default(decimal);
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product_Location", "Quantity", "ProductID", text, "LocationID", checkFieldValue, sqlTransaction);
					if (fieldValue != null)
					{
						decimal.TryParse(fieldValue.ToString(), out result7);
					}
					float num2 = 0f;
					string text6 = "";
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text, sqlTransaction);
					if (fieldValue != null)
					{
						text6 = fieldValue.ToString();
					}
					num2 = float.Parse(row2["Quantity"].ToString());
					if (text6 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text6)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text + "\nUnit:" + row2["UnitID"].ToString());
						float num3 = float.Parse(obj2["Factor"].ToString());
						string text7 = obj2["FactorType"].ToString();
						row2["UnitFactor"] = num3;
						row2["FactorType"] = text7;
						row2["UnitQuantity"] = row2["Quantity"];
						num2 = ((!(text7 == "M")) ? float.Parse(Math.Round(num2 * num3, 5).ToString()) : float.Parse(Math.Round(num2 / num3, 5).ToString()));
						row2["Quantity"] = num2;
					}
					if ((decimal)num2 > result7)
					{
						row2["IsRecost"] = true;
					}
					if (flag2)
					{
						decimal result8 = default(decimal);
						decimal result9 = default(decimal);
						row2["UnitPriceFC"] = row2["UnitPrice"];
						row2["AmountFC"] = row2["Amount"];
						decimal.TryParse(row2["UnitPrice"].ToString(), out result8);
						decimal.TryParse(row2["Amount"].ToString(), out result9);
						result8 = ((!(a == "M")) ? Math.Round(result8 / result5, 4) : Math.Round(result8 * result5, 4));
						row2["UnitPrice"] = result8;
						result9 = ((!(a == "M")) ? Math.Round(result9 / result5, currencyDecimalPoints) : Math.Round(result9 * result5, currencyDecimalPoints));
						row2["Amount"] = result9;
					}
				}
				if (isUpdate)
				{
					flag &= DeleteSalesPOSDetailsRows(text3, text5, isDeletingTransaction: false, sqlTransaction);
					flag &= DeletePaymentRows(text3, text5, sqlTransaction);
				}
				sqlCommand = GetInsertUpdateSalesPOSCommand(isUpdate);
				sqlCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(salesInvoiceData, "Sales_POS", sqlCommand)) : (flag & Insert(salesInvoiceData, "Sales_POS", sqlCommand)));
				if (salesInvoiceData.Tables["Sales_POS_Detail"].Rows.Count > 0)
				{
					sqlCommand = GetInsertUpdateSalesPOSDetailsCommand(isUpdate: false);
					sqlCommand.Transaction = sqlTransaction;
					flag &= Insert(salesInvoiceData, "Sales_POS_Detail", sqlCommand);
				}
				foreach (DataRow row3 in salesInvoiceData.PaymentTable.Rows)
				{
					row3["SysDocID"] = text3;
					row3["VoucherID"] = text4;
					row3.EndEdit();
				}
				if (salesInvoiceData.Tables["Invoice_Payment"].Rows.Count > 0)
				{
					sqlCommand = GetInsertUpdatePaymentCommand(isUpdate: false);
					sqlCommand.Transaction = sqlTransaction;
					flag &= Insert(salesInvoiceData, "Invoice_Payment", sqlCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row4 in salesInvoiceData.SalesPOSDetailTable.Rows)
				{
					DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = row4["SysDocID"];
					dataRow4["VoucherID"] = row4["VoucherID"];
					if (row4["LocationID"].ToString() == "")
					{
						throw new Exception("Location cannot be empty.");
					}
					dataRow4["LocationID"] = row4["LocationID"];
					dataRow4["ProductID"] = row4["ProductID"];
					dataRow4["Quantity"] = -1m * decimal.Parse(row4["Quantity"].ToString());
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["SysDocType"] = (byte)46;
					dataRow4["TransactionDate"] = dataRow["TransactionDate"];
					dataRow4["TransactionType"] = (byte)2;
					dataRow4["UnitPrice"] = row4["UnitPrice"];
					dataRow4["UnitPrice"] = row4["UnitPrice"];
					dataRow4["RowIndex"] = row4["RowIndex"];
					dataRow4["PayeeType"] = "C";
					dataRow4["PayeeID"] = dataRow["CustomerID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					if (row4["UnitQuantity"] != DBNull.Value && row4["UnitFactor"] != DBNull.Value)
					{
						dataRow4["UnitQuantity"] = row4["UnitQuantity"];
						dataRow4["Factor"] = row4["UnitFactor"];
						dataRow4["FactorType"] = row4["FactorType"];
						decimal.Parse(row4["UnitFactor"].ToString());
						row4["FactorType"].ToString();
						decimal d = decimal.Parse(row4["UnitQuantity"].ToString());
						decimal num4 = decimal.Parse(row4["Quantity"].ToString());
						decimal d2 = decimal.Parse(row4["UnitPrice"].ToString());
						decimal num5 = default(decimal);
						num5 = ((!(num4 != 0m)) ? default(decimal) : (d * d2 / num4));
						dataRow4["UnitPrice"] = num5;
					}
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
				}
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				if (salesInvoiceData.Tables.Contains("Tax_Detail"))
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(salesInvoiceData, text3, text5, isUpdate, sqlTransaction);
				}
				GLData journalData = CreateInvoiceGLData(salesInvoiceData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateInventoryTransactionRowID(text3, text5, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Sales_POS", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Sales Invoice";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text5, text3, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text5, text3, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Sales_POS", "VoucherID", sqlTransaction);
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

		private bool UpdateInventoryTransactionRowID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Sales_POS_Detail SID  \r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateInvoiceGLData(SalesPOSData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.SalesPOSTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string text = dataRow["CustomerID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string voucherID = dataRow["VoucherID"].ToString();
				dataRow["RegisterID"].ToString();
				string value = dataRow["CompanyID"].ToString();
				string value2 = dataRow["DivisionID"].ToString();
				bool result = false;
				if (!dataRow["PriceIncludeTax"].IsDBNullOrEmpty())
				{
					bool.TryParse(dataRow["PriceIncludeTax"].ToString(), out result);
				}
				if (text2 == "")
				{
					throw new CompanyException("System document is empty.");
				}
				string textCommand = "SELECT SD.LocationID,ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID,ISNULL(SD.COGSAccountID,LOC.COGSAccountID) AS COGSAccountID,\r\n                                ISNULL(SD.DiscountGivenAccountID,LOC.DiscountGivenAccountID) AS DiscountGivenAccountID,LOC.InventoryAccountID,ISNULL(SD.SalesAccountID,LOC.SalesAccountID) AS SalesAccountID,\r\n                                 LOC.UnInvoicedInventoryAccountID, ISNULL(SD.SalesTaxAccountID,LOC.SalesTaxAccountID) AS SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID,Loc.ConsignInAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				string docLocationID = dataRow2["LocationID"].ToString();
				string text3 = dataRow2["DiscountGivenAccountID"].ToString();
				dataRow2["SalesTaxAccountID"].ToString();
				dataRow2["ARAccountID"].ToString();
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				bool flag = false;
				decimal result2 = 1m;
				if (dataRow["CurrencyID"] != DBNull.Value && baseCurrencyID != dataRow["CurrencyID"].ToString())
				{
					flag = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result2);
				}
				bool result3 = false;
				bool.TryParse(dataRow["IsCash"].ToString(), out result3);
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.SalesPOS;
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["CurrencyID"] = dataRow["CurrencyID"];
				dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Sales Receipt - ";
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
				decimal num2 = default(decimal);
				decimal num3 = default(decimal);
				foreach (DataRow row in transactionData.SalesPOSDetailTable.Rows)
				{
					string text4 = row["ProductID"].ToString();
					string warehouseLocationID = row["LocationID"].ToString();
					int num4 = int.Parse(row["RowIndex"].ToString());
					dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(text4, docLocationID, warehouseLocationID, text2, sqlTransaction);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException("Product accounts information not found for product or location.");
					}
					DataRow dataRow5 = dataSet.Tables[0].Rows[0];
					string text5 = dataRow5["IncomeAccountID"].ToString();
					string text6 = dataRow5["ConsignInAccountID"].ToString();
					string text7 = dataRow5["COGSAccountID"].ToString();
					string text8 = dataRow5["InventoryAssetAccountID"].ToString();
					decimal num5 = default(decimal);
					decimal result4 = default(decimal);
					decimal result5 = default(decimal);
					DataRow[] array = transactionData.TaxDetailsTable.Select("RowIndex = " + num4);
					if (array.Length != 0)
					{
						decimal.TryParse(array[0]["TaxAmount"].ToString(), out result5);
					}
					if (flag)
					{
						decimal.TryParse(row["AmountFC"].ToString(), out result4);
					}
					else
					{
						decimal.TryParse(row["Amount"].ToString(), out result4);
					}
					ItemTypes itemTypes = ItemTypes.Inventory;
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "ItemType", "ProductID", text4, sqlTransaction);
					if (fieldValue == null || !(fieldValue.ToString() != ""))
					{
						throw new CompanyException("Item type is not selected for the product:" + text4);
					}
					itemTypes = (ItemTypes)byte.Parse(fieldValue.ToString());
					num5 = ((row["UnitQuantity"] == DBNull.Value) ? decimal.Parse(row["Quantity"].ToString()) : decimal.Parse(row["UnitQuantity"].ToString()));
					new Products(base.DBConfig).GetAverageCost(text4, sqlTransaction);
					decimal num6 = default(decimal);
					if (itemTypes != ItemTypes.ConsignmentItem)
					{
						num6 = ((!(num5 >= 0m)) ? (-1m * Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text4, text2, voucherID, num4, mergeWithRefRows: false, sqlTransaction))) : Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text4, text2, voucherID, num4, mergeWithRefRows: false, sqlTransaction)));
						if (num5 < 0m)
						{
							num6 = -1m * Math.Abs(num6);
						}
					}
					decimal result6 = default(decimal);
					if (flag)
					{
						decimal.TryParse(row["UnitPriceFC"].ToString(), out result6);
					}
					else
					{
						decimal.TryParse(row["UnitPrice"].ToString(), out result6);
					}
					string text9;
					if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.Assembly)
					{
						text9 = text7;
						if (hashtable.ContainsKey(text9))
						{
							num = decimal.Parse(hashtable[text9].ToString());
							num += Math.Round(num6, currencyDecimalPoints);
							hashtable[text9] = num;
						}
						else
						{
							hashtable.Add(text9, Math.Round(num6, currencyDecimalPoints));
							arrayList.Add(text9);
						}
						text9 = text8;
						if (hashtable3.ContainsKey(text9))
						{
							num = decimal.Parse(hashtable3[text9].ToString());
							num += Math.Round(num6, currencyDecimalPoints);
							hashtable3[text9] = num;
						}
						else
						{
							hashtable3.Add(text9, Math.Round(num6, currencyDecimalPoints));
							arrayList3.Add(text9);
						}
						d += Math.Round(num6, currencyDecimalPoints);
					}
					text9 = ((itemTypes != ItemTypes.ConsignmentItem) ? text5 : text6);
					if (hashtable2.ContainsKey(text9))
					{
						num = decimal.Parse(hashtable2[text9].ToString());
						if (result)
						{
							num += Math.Round(result4 - result5, 4);
						}
						else
						{
							num += Math.Round(result4, 4);
						}
						hashtable2[text9] = num;
					}
					else
					{
						num = ((!result) ? Math.Round(result4, currencyDecimalPoints) : Math.Round(result4 - result5, 4));
						hashtable2.Add(text9, Math.Round(num, 4));
						arrayList2.Add(text9);
					}
					num3 += result5;
					num2 += Math.Round(num, currencyDecimalPoints);
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
						string text9 = arrayList3[i].ToString();
						num = decimal.Parse(hashtable3[text9].ToString());
						dataRow6["JournalID"] = 0;
						dataRow6["AccountID"] = text9;
						dataRow6["PayeeID"] = text;
						if (num > 0m)
						{
							dataRow6["Debit"] = DBNull.Value;
							dataRow6["Credit"] = num;
						}
						else
						{
							dataRow6["Debit"] = Math.Abs(num);
							dataRow6["Credit"] = DBNull.Value;
						}
						dataRow6["IsBaseOnly"] = true;
						dataRow6["Reference"] = dataRow["Reference"];
						dataRow6["DivisionID"] = value2;
						dataRow6["CompanyID"] = value;
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
						string text9 = arrayList[j].ToString();
						num = decimal.Parse(hashtable[text9].ToString());
						dataRow6["JournalID"] = 0;
						dataRow6["AccountID"] = text9;
						dataRow6["PayeeID"] = text;
						if (num > 0m)
						{
							dataRow6["Debit"] = num;
							dataRow6["Credit"] = DBNull.Value;
						}
						else
						{
							dataRow6["Debit"] = DBNull.Value;
							dataRow6["Credit"] = Math.Abs(num);
						}
						dataRow6["IsBaseOnly"] = true;
						dataRow6["Reference"] = dataRow["Reference"];
						dataRow6["DivisionID"] = value2;
						dataRow6["CompanyID"] = value;
						dataRow6.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow6);
					}
				}
				for (int k = 0; k < hashtable2.Count; k++)
				{
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					string text9 = arrayList2[k].ToString();
					num = decimal.Parse(hashtable2[text9].ToString());
					num = Math.Round(num, currencyDecimalPoints);
					dataRow6["JournalID"] = 0;
					if (text9 == "")
					{
						throw new CompanyException("Sales account not set.", 1033);
					}
					dataRow6["AccountID"] = text9;
					dataRow6["PayeeID"] = text;
					if (flag)
					{
						if (num > 0m)
						{
							dataRow6["DebitFC"] = DBNull.Value;
							dataRow6["CreditFC"] = num;
						}
						else
						{
							dataRow6["DebitFC"] = Math.Abs(num);
							dataRow6["CreditFC"] = DBNull.Value;
						}
					}
					else if (num > 0m)
					{
						dataRow6["Debit"] = DBNull.Value;
						dataRow6["Credit"] = num;
					}
					else
					{
						dataRow6["Debit"] = Math.Abs(num);
						dataRow6["Credit"] = DBNull.Value;
					}
					dataRow6["Reference"] = dataRow["Reference"];
					dataRow6["DivisionID"] = value2;
					dataRow6["CompanyID"] = value;
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
				}
				if (result7 > 0m)
				{
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					if (text3 == "")
					{
						throw new CompanyException("Discount Account is not set.", 1032);
					}
					dataRow6["JournalID"] = 0;
					dataRow6["AccountID"] = text3;
					dataRow6["PayeeID"] = text;
					dataRow6["PayeeType"] = "A";
					if (flag)
					{
						dataRow6["DebitFC"] = result7;
						dataRow6["CreditFC"] = DBNull.Value;
					}
					else
					{
						dataRow6["Debit"] = result7;
						dataRow6["Credit"] = DBNull.Value;
					}
					dataRow6["Reference"] = dataRow["Reference"];
					dataRow6["DivisionID"] = value2;
					dataRow6["CompanyID"] = value;
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
				}
				if (result8 != 0m)
				{
					if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
					{
						throw new CompanyException("Tax details not found for the transaction.");
					}
					DataRow[] array2 = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
					decimal num7 = default(decimal);
					for (int l = 0; l < array2.Length; l++)
					{
						num7 = default(decimal);
						DataRow obj = array2[l];
						DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
						dataRow6.BeginEdit();
						dataRow6["JournalID"] = 0;
						string text10 = "";
						text10 = obj["TaxItemID"].ToString();
						string text11 = "";
						textCommand = "SELECT SalesTaxAccountID FROM Tax WHERE  TaxCode = '" + text10.Trim() + "'";
						object fieldValue = ExecuteScalar(textCommand);
						if (fieldValue != null)
						{
							text11 = fieldValue.ToString();
						}
						if (text11 == "")
						{
							throw new CompanyException("AccountID is not set for tax item: " + text10 + ".");
						}
						decimal.TryParse(obj["TaxAmount"].ToString(), out num7);
						dataRow6["AccountID"] = text11;
						dataRow6["PayeeID"] = text;
						dataRow6["PayeeType"] = "A";
						if (result8 > 0m)
						{
							if (flag)
							{
								dataRow6["DebitFC"] = DBNull.Value;
								dataRow6["CreditFC"] = Math.Round(Math.Abs(num7), currencyDecimalPoints);
							}
							else
							{
								dataRow6["Debit"] = DBNull.Value;
								dataRow6["Credit"] = Math.Round(Math.Abs(num7), currencyDecimalPoints);
							}
						}
						else if (flag)
						{
							dataRow6["CreditFC"] = DBNull.Value;
							dataRow6["DebitFC"] = Math.Round(Math.Abs(num7), currencyDecimalPoints);
						}
						else
						{
							dataRow6["Credit"] = DBNull.Value;
							dataRow6["Debit"] = Math.Round(Math.Abs(num7), currencyDecimalPoints);
						}
						dataRow6["Reference"] = dataRow["Reference"];
						dataRow6["DivisionID"] = value2;
						dataRow6["CompanyID"] = value;
						dataRow6.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow6);
					}
				}
				foreach (DataRow row2 in transactionData.PaymentTable.Rows)
				{
					PaymentMethodTypes paymentMethodTypes = (PaymentMethodTypes)byte.Parse(row2["PaymentMethodType"].ToString());
					if (paymentMethodTypes == PaymentMethodTypes.Check)
					{
						throw new CompanyException("Cheque payment method is not supported for this transaction.", 1031);
					}
					decimal result9 = default(decimal);
					decimal.TryParse(row2["Amount"].ToString(), out result9);
					if (result9 != 0m)
					{
						DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
						dataRow6.BeginEdit();
						dataRow6["JournalID"] = 0;
						dataRow6["AccountID"] = row2["AccountID"].ToString();
						dataRow6["PayeeID"] = text;
						dataRow6["PayeeType"] = "A";
						if (paymentMethodTypes == PaymentMethodTypes.AccountReceivable)
						{
							dataRow6["IsARAP"] = true;
							dataRow6["PayeeType"] = "C";
						}
						else
						{
							dataRow6["IsARAP"] = false;
						}
						if (flag)
						{
							if (result9 > 0m)
							{
								dataRow6["DebitFC"] = row2["Amount"];
								dataRow6["CreditFC"] = DBNull.Value;
							}
							else
							{
								dataRow6["DebitFC"] = DBNull.Value;
								dataRow6["CreditFC"] = Math.Abs(decimal.Parse(row2["Amount"].ToString()));
							}
						}
						else if (result9 > 0m)
						{
							dataRow6["Debit"] = row2["Amount"];
							dataRow6["Credit"] = DBNull.Value;
						}
						else
						{
							dataRow6["Debit"] = DBNull.Value;
							dataRow6["Credit"] = Math.Abs(decimal.Parse(row2["Amount"].ToString()));
						}
						dataRow6["Reference"] = dataRow["Reference"];
						dataRow6["Description"] = dataRow["Note"];
						dataRow6["DivisionID"] = value2;
						dataRow6["CompanyID"] = value;
						dataRow6.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow6);
					}
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public bool ReCostTransaction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				SalesPOSData salesPOSByID = GetSalesPOSByID(sysDocID, voucherID, sqlTransaction);
				object obj = null;
				_ = salesPOSByID.SalesPOSTable.Rows[0];
				GLData gLData = CreateInvoiceGLData(salesPOSByID, sqlTransaction);
				string exp = "SELECT JournalID FROM Journal WHERE SysDocID = '" + sysDocID + "' AND VOucherID = '" + voucherID + "'";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj.IsDBNullOrEmpty())
				{
					throw new CompanyException("JournalID not found for invoice '" + voucherID + "'");
				}
				int.Parse(obj.ToString());
				exp = " DELETE FROM Journal_Details WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' AND REFERENCE = 'SYS_COGS' ";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
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
					string text = row["AccountID"].ToString();
					DataRow[] array = gLData.JournalDetailsTable.Select("AccountID = '" + text + "'");
					if (array.Length == 0)
					{
						throw new CompanyException("Accounts not found. maybe changed.");
					}
					DataRow dataRow = array[0];
					if (!dataRow["Debit"].IsDBNullOrEmpty())
					{
						d += decimal.Parse(dataRow["Debit"].ToString());
					}
					if (!dataRow["Credit"].IsDBNullOrEmpty())
					{
						d -= decimal.Parse(dataRow["Credit"].ToString());
					}
					exp = " UPDATE Journal_Details SET Debit = ";
					exp = ((!dataRow["Debit"].IsDBNullOrEmpty()) ? (exp + dataRow["Debit"].ToString()) : (exp + " NULL "));
					exp = ((!dataRow["Credit"].IsDBNullOrEmpty()) ? (exp + " , Credit = " + dataRow["Credit"].ToString()) : (exp + " , Credit = NULL "));
					exp = exp + "  WHERE JournalDetailID = " + num + " and AccountID = '" + text + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				bool flag2 = new Journal(base.DBConfig).IsJournalInBalance(sysDocID, voucherID, sqlTransaction);
				if (d != 0m || !flag2)
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

		public SalesPOSData GetSalesPOSByID(string sysDocID, string voucherID)
		{
			return GetSalesPOSByID(sysDocID, voucherID, null);
		}

		internal SalesPOSData GetSalesPOSByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				SalesPOSData salesPOSData = new SalesPOSData();
				string text = "SELECT Sales_POS.*,CustomerName FROM Sales_POS INNER JOIN Customer CUS ON Sales_POS.CustomerID=CUS.CustomerID WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				new SqlCommand(text).Transaction = sqlTransaction;
				FillDataSet(salesPOSData, "Sales_POS", text, sqlTransaction);
				if (salesPOSData == null || salesPOSData.Tables.Count == 0 || salesPOSData.Tables["Sales_POS"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT TD.*,Product.Description,Product.ItemType\r\n                        FROM Sales_POS_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesPOSData, "Sales_POS_Detail", text, sqlTransaction);
				text = "SELECT IP.SysDocID,IP.VoucherID,TransactionDate,RegisterID,Amount ,PaymentMethodType,PaymentMethodID\r\n                        FROM Invoice_Payment IP\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesPOSData, "Invoice_Payment", text, sqlTransaction);
				text = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesPOSData, "Tax_Detail", text);
				return salesPOSData;
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
				new SalesPOSData();
				string commandText = "DELETE  FROM Invoice_Payment\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteSalesPOSDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				SalesPOSData salesPOSData = new SalesPOSData();
				string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid,ISNULL(IsCash,'False')AS IsCash FROM Sales_POS_Detail SOD INNER JOIN Sales_POS SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(salesPOSData, "Sales_POS_Detail", textCommand, sqlTransaction);
				if (salesPOSData.SalesPOSDetailTable.Rows.Count == 0)
				{
					return true;
				}
				string text = "";
				object companyOptionValue = new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString());
				if (companyOptionValue != null)
				{
					text = companyOptionValue.ToString();
				}
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text != "")
				{
					salesFlows = (SalesFlows)int.Parse(text.ToString());
				}
				_ = 2;
				bool result = false;
				bool.TryParse(salesPOSData.SalesPOSDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(salesPOSData.SalesPOSDetailTable.Rows[0]["IsCash"].ToString(), out result2);
				if (!result)
				{
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(46, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
					string text2 = "";
					string text3 = "";
					string text4 = "";
					foreach (DataRow row in salesPOSData.SalesPOSDetailTable.Rows)
					{
						text4 = row["ProductID"].ToString();
						text2 = row["OrderVoucherID"].ToString();
						text3 = row["OrderSysDocID"].ToString();
						int result3 = 0;
						if (!(text2 == "") && !(text3 == ""))
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
							float num = new Products(base.DBConfig).GetReservedQuantity(text4, sqlTransaction) + result4;
							if (num < 0f)
							{
								num = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateReservedQuantity(text4, num, sqlTransaction);
							flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text3, text2, result3, result4, sqlTransaction);
						}
					}
				}
				textCommand = "DELETE FROM Sales_POS_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidSalesPOS(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidSalesPOS(sysDocID, voucherID, isVoid, null);
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

		private bool VoidSalesPOS(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				SalesPOSData salesPOSData = new SalesPOSData();
				string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid,ISNULL(IsCash,'False') AS IsCash FROM Sales_POS_Detail SOD INNER JOIN Sales_POS SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(salesPOSData, "Sales_POS_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(salesPOSData.SalesPOSDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(salesPOSData.SalesPOSDetailTable.Rows[0]["IsCash"].ToString(), out result2);
				if (result == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(46, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				textCommand = "UPDATE Sales_POS SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("POS Sales Receipt", voucherID, sysDocID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteSalesPOS(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Sales_POS", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidSalesPOS(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeleteSalesPOSDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Sales_POS WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("POS Sale Receipt", voucherID, sysDocID, activityType, sqlTransaction);
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
				string str = "SELECT COUNT(RowIndex)FROM Sales_POS_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				str += " AND CASE WHEN UnitQuantity IS NULL THEN Quantity ELSE UnitQuantity END - ISNULL(QuantityShipped,0)=0";
				object obj = ExecuteScalar(str, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				str = "UPDATE Sales_POS SET IsDelivered = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityShipped FROM Sales_POS_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				textCommand = "UPDATE Sales_POS_Detail SET QuantityShipped=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetInvoicesForDelivery(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer] FROM Sales_POS SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID  WHERE ISNULL(IsDelivered,0)=0";
				if (customerID != "")
				{
					text = text + " AND SO.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "Sales_POS", text);
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

		public DataSet GetSalesPOSToPrint(string sysDocID, string voucherID)
		{
			return GetSalesPOSToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetSalesPOSToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SI.SysDocID, SI.VoucherID,SI.CustomerID,CustomerName,CustomerAddress, SI.TransactionDate,\r\n                                IsCash,SI.SalesPersonID,RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,IsVoid,Reference,ISNULL(DiscountFC,Discount) AS Discount,\r\n                                ISNULL(ISNULL(TaxAmountFC,TaxAmount) ,0) AS Tax,ISNULL(TotalFC,Total) AS Total, SI.Change, PONumber,SI.Note, BatchID, ShiftID, SI.RegisterID,SI.PayeeTaxGroupID,SI.TaxAmount,SP.FullName AS [Sales Person]\r\n                                FROM  Sales_POS SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID \r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT OUTER JOIN Salesperson SP ON SP.SalespersonID=SI.SalespersonID\r\n                                WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Sales_POS", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Sales_POS"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,Sales_POS_Detail.ProductID,Description,ISNULL(UnitQuantity,Quantity) AS Quantity,ISNULL(Discount,0) AS Discount,\r\n                        ISNULL(UnitPriceFC,UnitPrice) AS UnitPrice,TaxGroupID,TaxAmount,\r\n                        ISNULL(UnitQuantity,Quantity)*ISNULL(UnitPriceFC,UnitPrice) AS Total,UnitID,LocationID,P.IsPriceEmbedded, RowIndex + 1 AS RowIndex\r\n                        FROM   Sales_POS_Detail Left Join (SELECT ProductID,Isnull(IsPriceEmbedded,'False') as IsPriceEmbedded From Product) P ON P.ProductID=Sales_POS_Detail.ProductID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Sales_POS_Detail", cmdText);
				cmdText = "SELECT IP.SysDocID, IP.VoucherID, IP.TransactionDate, IP.Amount, IP.PaymentMethodID, PaymentMethodType, IP.RegisterID \r\n                        FROM Invoice_Payment IP\r\n                        WHERE IP.SysDocID = '" + sysDocID + "' AND IP.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Invoice_Payment", cmdText);
				dataSet.Relations.Add("CustomerInvoice", new DataColumn[2]
				{
					dataSet.Tables["Sales_POS"].Columns["SysDocID"],
					dataSet.Tables["Sales_POS"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Sales_POS_Detail"].Columns["SysDocID"],
					dataSet.Tables["Sales_POS_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Sales_POS"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Sales_POS"].Rows)
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

		public DataSet GetSalesReceiptLookupList(string sysDocID, DateTime from, DateTime to)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   SysDocID,VoucherID,INV.CustomerID [Customer Code],CustomerName [Customer Name],VoucherID + ' ' + INV.CustomerID + ' ' + customerName AS SearchColumn,TransactionDate [Date],\r\n                            Total [Amount]\r\n                            FROM         Sales_POS INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID WHERE 1=1 ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (sysDocID != "")
			{
				text3 = text3 + " AND SysDocID = '" + sysDocID + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Sales_POS", sqlCommand);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   ISNULL(IsVoid,'False') AS 'V', SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Invoice Date],\r\n                            CASE ISNULL(IsCash,'False') WHEN 'True' THEN 'Cash' ELSE 'Credit' END AS [Type],INV.SalespersonID [Salesperson],Total [Amount], Reference\r\n                            FROM         Sales_POS INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Sales_POS", sqlCommand);
			return dataSet;
		}

		public DataSet GetPOSXReport(DateTime from, DateTime to, int shifID, int batchID, string registerID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SElECT Pay.PaymentMethodID, ISNULL(Reg.DisplayName,Pay.PaymentMethodID) AS PaymentMethodName, SUM(Amount) AS Amount \r\n                                FROM Invoice_Payment Pay\r\n                                INNER JOIN Sales_POS SP ON Pay.SysDocID = SP.SysDocID AND Pay.VoucherID = SP.VoucherID\r\n                                LEFT OUTER JOIN POS_CashRegister_PaymentMethod REG ON Reg.PaymentMethodID = Pay.PaymentMethodID AND Reg.CashRegisterID=Pay.RegisterID\r\n                                WHERE ShiftID= " + shifID + " AND BatchID= " + batchID + " AND Pay.RegisterID = '" + registerID + "' AND ISNULL(IsVoid,'False') = 'False' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			text3 += "GROUP BY Pay.PaymentMethodID, Reg.DisplayName";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Payments", sqlCommand);
			text3 = "SELECT SUM(CASE WHEN SPD.Quantity >=0 THEN SPD.Quantity*SPD.UnitPrice ELSE 0 END) AS TotalSale,\r\n                        SUM(CASE WHEN SPD.Quantity<0 THEN SPD.Quantity*SPD.UnitPrice ELSE 0 END) AS TotalReturn,\r\n                        SUM(CASE WHEN SPD.Quantity >=0 THEN SPD.TaxAmount ELSE 0 END) AS TotalTax,\r\n                        SUM(CASE WHEN SPD.Quantity >=0 THEN SPD.Discount ELSE 0 END) AS TotalDiscount,\r\n                        SUM(-1*IT.AssetValue) AS [Cost],\r\n                        SP. ShiftID, SP.BatchID\r\n                         FROM Sales_POS_Detail SPD INNER JOIN Sales_POS SP ON SPD.SysDocID = SP.SysDocID AND SPD.VoucherID = SP.VoucherID \r\n                        INNER JOIN Inventory_Transactions IT ON IT.SysDocID=SPD.SysDocID AND IT.VoucherID=SPD.VoucherID AND IT.ProductID=SPD.ProductID  AND IT.RowIndex=SPD.RowIndex\r\n                         WHERE ShiftID= " + shifID + " AND BatchID= " + batchID + " AND RegisterID = '" + registerID + "' AND ISNULL(SP.IsVoid,'False') = 'False' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			text3 += " GROUP BY ShiftID, BatchID";
			sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "TempSummary1", sqlCommand);
			text3 = " Select SUM(CASE WHEN Total >=0 THEN 1 ELSE 0 END) AS SalesCount ,\r\n                     SUM(CASE WHEN Total <0 THEN 1 ELSE 0 END) AS ReturnCount\r\n                      FROM Sales_POS   \r\n                        WHERE ShiftID= " + shifID + " AND BatchID= " + batchID + " AND RegisterID = '" + registerID + "' AND ISNULL(IsVoid,'False') = 'False' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "TempSummary2", sqlCommand);
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				MergeDataSet(dataSet);
			}
			text3 = "SELECT SP.SysDocID,PC.CategoryName,COUNT(SPD.ProductID) [No. Items ],SUM(SPD.Amount) AS [Sale]\r\n                    FROM Sales_POS SP INNER JOIN\r\n                    Sales_POS_Detail SPD  ON SP.SysDocID=SPD.SysDocID AND SP.VoucherID=SPD.VoucherID INNER JOIN Product P ON \r\n                    SPD.ProductID=P.ProductID\r\n                    LEFT JOIN Product_Category PC ON PC.CategoryID=P.CategoryID\r\n                    INNER JOIN Customer C ON C.CustomerID=SP.CustomerID\r\n                    WHERE ShiftID= " + shifID + "AND  BatchID= " + batchID + " AND ISNULL(IsVoid,'False') = 'False' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			text3 += " GROUP BY PC.CategoryName,SP.SysDocID";
			sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Category", sqlCommand);
			return dataSet;
		}

		public DataSet GetPOSYReport(DateTime from, DateTime to, int shifID, int batchID, string registerID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SElECT Pay.PaymentMethodID, ISNULL(Reg.DisplayName,Pay.PaymentMethodID) AS PaymentMethodName, SUM(Amount) AS Amount \r\n                                FROM Invoice_Payment Pay\r\n                                INNER JOIN Sales_POS SP ON Pay.SysDocID = SP.SysDocID AND Pay.VoucherID = SP.VoucherID\r\n                                LEFT OUTER JOIN POS_CashRegister_PaymentMethod REG ON Reg.PaymentMethodID = Pay.PaymentMethodID AND Reg.CashRegisterID=Pay.RegisterID\r\n                                WHERE ShiftID= " + shifID + " AND BatchID= " + batchID + " AND ISNULL(IsVoid,'False') = 'False' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			text3 += "GROUP BY Pay.PaymentMethodID, Reg.DisplayName";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Payments", sqlCommand);
			text3 = "SELECT SUM(CASE WHEN SPD.Quantity >=0 THEN SPD.Quantity*SPD.UnitPrice ELSE 0 END) AS TotalSale,\r\n                        SUM(CASE WHEN SPD.Quantity<0 THEN SPD.Quantity*SPD.UnitPrice ELSE 0 END) AS TotalReturn,\r\n                        SUM(CASE WHEN SPD.Quantity >=0 THEN SPD.TaxAmount ELSE 0 END) AS TotalTax,\r\n                        SUM(CASE WHEN SPD.Quantity >=0 THEN SPD.Discount ELSE 0 END) AS TotalDiscount,\r\n                        SUM(-1*IT.AssetValue) AS [Cost],\r\n                        SP. ShiftID, SP.BatchID\r\n                        FROM Sales_POS_Detail SPD INNER JOIN Sales_POS SP ON SPD.SysDocID = SP.SysDocID AND SPD.VoucherID = SP.VoucherID \r\n                        INNER JOIN Inventory_Transactions IT ON IT.SysDocID=SPD.SysDocID AND IT.VoucherID=SPD.VoucherID AND IT.ProductID=SPD.ProductID AND IT.RowIndex=SPD.RowIndex\r\n                        WHERE ShiftID= " + shifID + " AND BatchID= " + batchID + " AND ISNULL(SP.IsVoid,'False') = 'False' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			text3 += " GROUP BY ShiftID, BatchID";
			sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "TempSummary1", sqlCommand);
			text3 = " Select SUM(CASE WHEN Total >=0 THEN 1 ELSE 0 END) AS SalesCount ,\r\n                     SUM(CASE WHEN Total <0 THEN 1 ELSE 0 END) AS ReturnCount\r\n                      FROM Sales_POS   \r\n                        WHERE ShiftID= " + shifID + " AND BatchID= " + batchID + " AND ISNULL(IsVoid,'False') = 'False' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "TempSummary2", sqlCommand);
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				MergeDataSet(dataSet);
			}
			text3 = "SELECT SP.SysDocID,PC.CategoryName,COUNT(SPD.ProductID) [No. Items ],SUM(SPD.Amount) AS [Sale]\r\n                    FROM Sales_POS SP INNER JOIN\r\n                    Sales_POS_Detail SPD  ON SP.SysDocID=SPD.SysDocID AND SP.VoucherID=SPD.VoucherID INNER JOIN Product P ON \r\n                    SPD.ProductID=P.ProductID\r\n                    LEFT JOIN Product_Category PC ON PC.CategoryID=P.CategoryID\r\n                    INNER JOIN Customer C ON C.CustomerID=SP.CustomerID\r\n                    WHERE ShiftID= " + shifID + "AND  BatchID= " + batchID + " AND ISNULL(IsVoid,'False') = 'False' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			text3 += " GROUP BY PC.CategoryName,SP.SysDocID";
			sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Category", sqlCommand);
			return dataSet;
		}

		public DataSet GetPOSZReport(DateTime from, DateTime to, int shifID, int batchID, string registerID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SElECT Pay.PaymentMethodID, ISNULL(Reg.DisplayName,Pay.PaymentMethodID) AS PaymentMethodName, SUM(Amount) AS Amount \r\n                                FROM Invoice_Payment Pay\r\n                                INNER JOIN Sales_POS SP ON Pay.SysDocID = SP.SysDocID AND Pay.VoucherID = SP.VoucherID\r\n                                LEFT OUTER JOIN POS_CashRegister_PaymentMethod REG ON Reg.PaymentMethodID = Pay.PaymentMethodID AND Reg.CashRegisterID=Pay.RegisterID\r\n                                WHERE BatchID= " + batchID + " AND ISNULL(IsVoid,'False') = 'False' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			text3 += "GROUP BY Pay.PaymentMethodID, Reg.DisplayName";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Payments", sqlCommand);
			text3 = "SELECT SUM(CASE WHEN SPD.Quantity >=0 THEN SPD.Quantity*SPD.UnitPrice ELSE 0 END) AS TotalSale,\r\n                        SUM(CASE WHEN SPD.Quantity<0 THEN SPD.Quantity*SPD.UnitPrice ELSE 0 END) AS TotalReturn,SUM(SPD.TaxAmount) AS TotalTax,\r\n                        SUM(CASE WHEN SPD.Quantity >=0 THEN SPD.Discount ELSE 0 END) AS TotalDiscount,\r\n                        SP.BatchID,SUM(-1*IT.AssetValue) AS [Cost]\r\n                         FROM Sales_POS_Detail SPD INNER JOIN Sales_POS SP ON SPD.SysDocID = SP.SysDocID AND SPD.VoucherID = SP.VoucherID \r\n                        INNER JOIN Inventory_Transactions IT ON IT.SysDocID=SPD.SysDocID AND IT.VoucherID=SPD.VoucherID AND IT.ProductID=SPD.ProductID AND IT.RowIndex=SPD.RowIndex\r\n                            WHERE BatchID= " + batchID + " AND ISNULL(SP.IsVoid,'False') = 'False' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			text3 += " GROUP BY BatchID";
			sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "TempSummary1", sqlCommand);
			text3 = " Select SUM(CASE WHEN Total >=0 THEN 1 ELSE 0 END) AS SalesCount ,\r\n                     SUM(CASE WHEN Total <0 THEN 1 ELSE 0 END) AS ReturnCount\r\n                      FROM Sales_POS   \r\n                        WHERE BatchID= " + batchID + " AND ISNULL(IsVoid,'False') = 'False' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "TempSummary2", sqlCommand);
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				MergeDataSet(dataSet);
			}
			text3 = "SELECT SP.SysDocID,PC.CategoryName,COUNT(SPD.ProductID) [No. Items ],SUM(SPD.Amount) AS [Sale]\r\n                    FROM Sales_POS SP INNER JOIN\r\n                    Sales_POS_Detail SPD  ON SP.SysDocID=SPD.SysDocID AND SP.VoucherID=SPD.VoucherID INNER JOIN Product P ON \r\n                    SPD.ProductID=P.ProductID\r\n                    LEFT JOIN Product_Category PC ON PC.CategoryID=P.CategoryID\r\n                    INNER JOIN Customer C ON C.CustomerID=SP.CustomerID\r\n                    WHERE 1=1 \r\n                    AND  BatchID= " + batchID + " AND ISNULL(IsVoid,'False') = 'False' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			text3 += " GROUP BY PC.CategoryName,SP.SysDocID";
			sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Category", sqlCommand);
			return dataSet;
		}

		private void MergeDataSet(DataSet data)
		{
			DataTable dataTable = new DataTable("Summary");
			DataRow dataRow = dataTable.NewRow();
			dataTable.Columns.Add("TotalSale");
			dataTable.Columns.Add("TotalReturn");
			dataTable.Columns.Add("TotalTax");
			dataTable.Columns.Add("SalesCount");
			dataTable.Columns.Add("ReturnCount");
			dataTable.Columns.Add("TotalDiscount");
			dataTable.Columns.Add("NetSale");
			dataTable.Columns.Add("ShiftID");
			dataTable.Columns.Add("BatchID");
			decimal d = 0.00m;
			decimal num = 0.00m;
			decimal num2 = 0.00m;
			decimal num3 = 0.00m;
			decimal num4 = 0.00m;
			if (data.Tables["TempSummary1"].Rows[0]["TotalSale"].ToString() != "" && data.Tables["TempSummary1"].Rows[0]["TotalSale"].ToString() != null)
			{
				d = decimal.Parse(data.Tables["TempSummary1"].Rows[0]["TotalSale"].ToString());
			}
			if (data.Tables["TempSummary1"].Rows[0]["TotalReturn"].ToString() != "" && data.Tables["TempSummary1"].Rows[0]["TotalReturn"].ToString() != null)
			{
				num = decimal.Parse(data.Tables["TempSummary1"].Rows[0]["TotalReturn"].ToString());
			}
			if (data.Tables["TempSummary1"].Rows[0]["TotalTax"].ToString() != "" && data.Tables["TempSummary1"].Rows[0]["TotalTax"].ToString() != null)
			{
				num2 = decimal.Parse(data.Tables["TempSummary1"].Rows[0]["TotalTax"].ToString());
			}
			if (data.Tables["TempSummary1"].Rows[0]["TotalDiscount"].ToString() != "" && data.Tables["TempSummary1"].Rows[0]["TotalDiscount"].ToString() != null)
			{
				num3 = decimal.Parse(data.Tables["TempSummary1"].Rows[0]["TotalDiscount"].ToString());
			}
			dataRow["TotalSale"] = d.ToString("#.00");
			dataRow["TotalReturn"] = num.ToString("#.00");
			dataRow["TotalTax"] = num2.ToString("#.00");
			dataRow["SalesCount"] = data.Tables["TempSummary2"].Rows[0]["SalesCount"].ToString();
			dataRow["ReturnCount"] = data.Tables["TempSummary2"].Rows[0]["ReturnCount"].ToString();
			dataRow["TotalDiscount"] = num3.ToString("#.00");
			dataRow["NetSale"] = (d + -((num < 0m) ? decimal.Negate(num) : num) - ((num3 < 0m) ? decimal.Negate(num3) : num3)).ToString("#.00");
			if (data.Tables["TempSummary1"].Columns["ShiftID"] != null)
			{
				dataRow["ShiftID"] = data.Tables["TempSummary1"].Rows[0]["ShiftID"].ToString();
			}
			dataRow["BatchID"] = data.Tables["TempSummary1"].Rows[0]["BatchID"].ToString();
			dataTable.Rows.Add(dataRow);
			data.Tables.Remove("TempSummary1");
			data.Tables.Remove("TempSummary2");
			data.Tables.Add(dataTable);
		}
	}
}
