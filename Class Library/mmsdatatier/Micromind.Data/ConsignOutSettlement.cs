using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ConsignOutSettlement : StoreObject
	{
		private const string CONSIGNOUTSETTLEMENT_TABLE = "ConsignOut_Settlement";

		private const string CONSIGNOUTSETTLEMENTDETAIL_TABLE = "ConsignOut_Settlement_Detail";

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

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string ISDEDUCT_PARM = "@IsDeduct";

		public ConsignOutSettlement(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSettlementText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ConsignOut_Settlement", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("ConsignSysDocID", "@ConsignSysDocID"), new FieldValue("ConsignVoucherID", "@ConsignVoucherID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("DueDate", "@DueDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("IsCash", "@IsCash"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("ConsignOut_Settlement", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			sqlBuilder.AddInsertUpdateParameters("ConsignOut_Settlement_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ConsignSysDocID", "@ConsignSysDocID"), new FieldValue("ConsignVoucherID", "@ConsignVoucherID"), new FieldValue("ConsignRowIndex", "@ConsignRowIndex"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitExpense", "@UnitExpense"), new FieldValue("ExpenseAmount", "@ExpenseAmount"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("OrderSysDocID", "@OrderSysDocID"), new FieldValue("OrderVoucherID", "@OrderVoucherID"), new FieldValue("DNoteSysDocID", "@DNoteSysDocID"), new FieldValue("DNoteVoucherID", "@DNoteVoucherID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("IsDNRow", "@IsDNRow"), new FieldValue("IsRecost", "@IsRecost"));
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
			sqlBuilder.AddInsertUpdateParameters("ConsignOut_Expense", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Reference", "@Reference"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("IsDeduct", "@IsDeduct"));
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
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters.Add("@IsDeduct", SqlDbType.Bit);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ExpenseID"].SourceColumn = "ExpenseID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@IsDeduct"].SourceColumn = "IsDeduct";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(ConsignOutSettlementData journalData)
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
					text = "SELECT SID.SysDocID,SID.VoucherID,Quantity FROM ConsignOut_Settlement_Detail SID INNER JOIN ConsignOut_Settlement SI ON \r\n                         SID.VoucherID=SI.VoucherID AND SID.SysDocID=SI.SysDocID WHERE LocationID='" + locationID + "' AND ProductID='" + productID + "' AND ISNULL(IsRecost,'False')='True' ORDER BY SI.TransactionDate DESC";
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
									text = "UPDATE ConsignOut_Settlement_Detail SET IsRecost='False' WHERE LocationID='" + locationID + "' AND ProductID='" + productID + "' AND ISNULL(IsRecost,'False')='True'";
									flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
								}
							}
						}
						return flag;
					}
				}
				text = "UPDATE ConsignOut_Settlement_Detail SET IsRecost='False' WHERE LocationID='" + locationID + "' AND ProductID='" + productID + "' AND ISNULL(IsRecost,'False')='True'";
				return flag & (ExecuteNonQuery(text, sqlTransaction) >= 0);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool InsertUpdateSettlement(ConsignOutSettlementData settlementData, bool isUpdate)
		{
			return InsertUpdateSettlement(settlementData, isUpdate, null);
		}

		public bool InsertUpdateSettlement(ConsignOutSettlementData settlementData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand sqlCommand = null;
			string text = "";
			try
			{
				DataRow dataRow = settlementData.ConsignOutSettlementTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				string text2 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				string sysDocID2 = dataRow["ConsignSysDocID"].ToString();
				string voucherID = dataRow["ConsignVoucherID"].ToString();
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
				foreach (DataRow row in settlementData.ConsignOutSettlementDetailTable.Rows)
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
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("ConsignOut_Settlement", "VoucherID", dataRow["SysDocID"].ToString(), text2, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				bool flag2 = false;
				decimal result4 = 1m;
				string a = "M";
				string a2 = dataRow["CurrencyID"].ToString();
				if (dataRow["CurrencyID"] != DBNull.Value && baseCurrencyID != dataRow["CurrencyID"].ToString())
				{
					flag2 = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result4);
					a = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
				}
				else
				{
					a2 = baseCurrencyID;
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
				}
				foreach (DataRow row2 in settlementData.ConsignOutSettlementDetailTable.Rows)
				{
					float num3 = 0f;
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					text = row2["ProductID"].ToString();
					string checkFieldValue = row2["LocationID"].ToString();
					int.Parse(row2["RowIndex"].ToString());
					decimal result6 = default(decimal);
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product_Location", "Quantity", "ProductID", text, "LocationID", checkFieldValue, sqlTransaction);
					if (fieldValue != null)
					{
						decimal.TryParse(fieldValue.ToString(), out result6);
					}
					string idFieldValue = row2["ConsignSysDocID"].ToString();
					string checkFieldValue2 = row2["ConsignVoucherID"].ToString();
					string checkFieldValue3 = row2["ConsignRowIndex"].ToString();
					object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Consign_Out_Detail", "QuantitySettled", "SysDocID", idFieldValue, "VoucherID", checkFieldValue2, "RowIndex", checkFieldValue3, sqlTransaction);
					float num4 = 0f;
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						num4 = float.Parse(fieldValue2.ToString());
					}
					fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Consign_Out_Detail", "QuantityReturned", "SysDocID", idFieldValue, "VoucherID", checkFieldValue2, "RowIndex", checkFieldValue3, sqlTransaction);
					float num5 = 0f;
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						num5 = float.Parse(fieldValue2.ToString());
					}
					fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Consign_Out_Detail", "Quantity", "SysDocID", idFieldValue, "VoucherID", checkFieldValue2, "RowIndex", checkFieldValue3, sqlTransaction);
					float num6 = 0f;
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						num6 = float.Parse(fieldValue2.ToString());
					}
					if (num4 + num5 > num6)
					{
						throw new CompanyException("Settlement quantity cannot be greater than balance quantity.", 1026);
					}
					num3 = float.Parse(row2["Quantity"].ToString());
					string text3 = "";
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text, sqlTransaction);
					if (fieldValue != null)
					{
						text3 = fieldValue.ToString();
					}
					if (text3 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text3)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text + "\nUnit:" + row2["UnitID"].ToString());
						float num7 = float.Parse(obj2["Factor"].ToString());
						string text4 = obj2["FactorType"].ToString();
						num3 = float.Parse(row2["Quantity"].ToString());
						row2["UnitFactor"] = num7;
						row2["FactorType"] = text4;
						row2["UnitQuantity"] = row2["Quantity"];
						num3 = ((!(text4 == "M")) ? float.Parse(Math.Round(num3 * num7, 5).ToString()) : float.Parse(Math.Round(num3 / num7, 5).ToString()));
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
						result7 = ((!(a == "M")) ? Math.Round(result7 / result4, 4) : Math.Round(result7 * result4, 4));
						row2["UnitPrice"] = result7;
						result8 = ((!(a == "M")) ? Math.Round(result8 / result4, currencyDecimalPoints) : Math.Round(result8 * result4, currencyDecimalPoints));
						row2["Amount"] = result8;
					}
				}
				if (isUpdate)
				{
					flag &= DeleteSettlementDetailsRows(sysDocID, text2, isDeletingTransaction: false, sqlTransaction);
				}
				sqlCommand = GetInsertUpdateSettlementCommand(isUpdate);
				sqlCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(settlementData, "ConsignOut_Settlement", sqlCommand)) : (flag & Insert(settlementData, "ConsignOut_Settlement", sqlCommand)));
				if (settlementData.Tables["ConsignOut_Settlement_Detail"].Rows.Count > 0)
				{
					sqlCommand = GetInsertUpdateSettlementDetailsCommand(isUpdate: false);
					sqlCommand.Transaction = sqlTransaction;
					flag &= Insert(settlementData, "ConsignOut_Settlement_Detail", sqlCommand);
				}
				text = "";
				foreach (DataRow row3 in settlementData.ConsignOutSettlementDetailTable.Rows)
				{
					text = row3["ProductID"].ToString();
					int result9 = 0;
					int.TryParse(row3["ConsignRowIndex"].ToString(), out result9);
					float result10 = 0f;
					if (row3["UnitQuantity"] != DBNull.Value)
					{
						float.TryParse(row3["UnitQuantity"].ToString(), out result10);
					}
					else
					{
						float.TryParse(row3["Quantity"].ToString(), out result10);
					}
					flag &= UpdateRowSettledQuantity(sysDocID2, voucherID, result9, result10, sqlTransaction);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row4 in settlementData.ConsignOutSettlementDetailTable.Rows)
				{
					DataRow dataRow6 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow6.BeginEdit();
					dataRow6["SysDocID"] = row4["SysDocID"];
					dataRow6["VoucherID"] = row4["VoucherID"];
					if (row4["LocationID"].ToString() == "" || row4["LocationID"].ToString() == "")
					{
						throw new Exception("Location cannot be empty.");
					}
					dataRow6["LocationID"] = row4["LocationID"];
					dataRow6["ProductID"] = row4["ProductID"];
					dataRow6["Quantity"] = -1m * decimal.Parse(row4["Quantity"].ToString());
					dataRow6["Reference"] = dataRow["Reference"];
					dataRow6["SysDocType"] = (byte)48;
					dataRow6["TransactionDate"] = dataRow["TransactionDate"];
					dataRow6["TransactionType"] = (byte)11;
					dataRow6["UnitPrice"] = row4["UnitPrice"];
					dataRow6["UnitPrice"] = row4["UnitPrice"];
					dataRow6["RowIndex"] = row4["RowIndex"];
					dataRow6["PayeeType"] = "C";
					dataRow6["PayeeID"] = dataRow["CustomerID"];
					dataRow6["Cost"] = row4["ExpenseAmount"];
					dataRow6["DivisionID"] = dataRow["DivisionID"];
					dataRow6["CompanyID"] = dataRow["CompanyID"];
					dataRow6.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow6);
				}
				inventoryTransactionData.Merge(settlementData.Tables["Product_Lot_Issue_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(settlementData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				foreach (DataRow row5 in settlementData.ExpenseTable.Rows)
				{
					row5["SysDocID"] = dataRow["SysDocID"];
					row5["VoucherID"] = dataRow["VoucherID"];
					if (a2 != "" && a2 != baseCurrencyID)
					{
						decimal d = decimal.Parse(row5["Amount"].ToString());
						row5["AmountFC"] = row5["Amount"];
						d = ((!(row5["RateType"].ToString() == "M")) ? Math.Round(d / result4, currencyDecimalPoints) : Math.Round(d * result4, currencyDecimalPoints));
						row5["Amount"] = d;
					}
					else
					{
						row5["CurrencyRate"] = 1;
					}
				}
				sqlCommand = GetInsertUpdateExpenseCommand(isUpdate: false);
				sqlCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					DeleteExpenseRows(sysDocID, text2, sqlTransaction);
				}
				if (settlementData.Tables["ConsignOut_Expense"].Rows.Count > 0)
				{
					flag &= Insert(settlementData, "ConsignOut_Expense", sqlCommand);
				}
				GLData journalData = CreateInvoiceGLData(settlementData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateInventoryTransactionRowID(sysDocID, text2, sqlTransaction);
				flag &= new ConsignOut(base.DBConfig).CloseOpenConsignment(sysDocID2, voucherID, sqlTransaction);
				flag &= new ConsignOut(base.DBConfig).UpdateConsignmentStatus(sysDocID2, voucherID, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("ConsignOut_Settlement", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Settlement";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "ConsignOut_Settlement", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ConsignOutSettlement, sysDocID, text2, "ConsignOut_Settlement", sqlTransaction);
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
				string exp = "UPDATE CSD SET ITRowID = (SELECT TOP 1 TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = CSD.SysDocID AND IT.VoucherID = CSD.VoucherID AND IT.RowIndex = CSD.RowIndex AND IT.Quantity =-1* CSD.Quantity) \r\n                                    FROM ConsignOut_Settlement_Detail CSD INNER JOIN ConsignOut_Settlement CS ON CS.SysDocID = CSD.SysDocID AND CS.VoucherID = CSD.VoucherID\r\n                                     where CSD.SysDocID = '" + sysDocID + "' and CSD.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteExpenseRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM ConsignOut_Expense WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateInvoiceGLData(ConsignOutSettlementData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.ConsignOutSettlementTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string text = dataRow["CustomerID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string voucherID = dataRow["VoucherID"].ToString();
				string value = dataRow["CompanyID"].ToString();
				string value2 = dataRow["DivisionID"].ToString();
				string textCommand = "SELECT SD.LocationID,ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID,LOC.ConsignOutCOGSAccountID,LOC.DiscountGivenAccountID,\r\n                                LOC.InventoryAccountID,LOC.ConsignOutSalesAccountID,LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				string docLocationID = dataRow2["LocationID"].ToString();
				string value3 = dataRow2["DiscountGivenAccountID"].ToString();
				string value4 = dataRow2["SalesTaxAccountID"].ToString();
				string value5 = dataRow2["ARAccountID"].ToString();
				string text3 = dataRow2["ConsignOutSalesAccountID"].ToString();
				string text4 = dataRow2["ConsignOutCOGSAccountID"].ToString();
				string a = dataRow2["BaseCurrencyID"].ToString();
				bool flag = false;
				decimal result = 1m;
				if (dataRow["CurrencyID"] != DBNull.Value && a != dataRow["CurrencyID"].ToString())
				{
					flag = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result);
				}
				bool result2 = false;
				bool.TryParse(dataRow["IsCash"].ToString(), out result2);
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.ConsignOutSettlement;
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
				dataRow3["Narration"] = "Consign Out Settlement - ";
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
				foreach (DataRow row in transactionData.ConsignOutSettlementDetailTable.Rows)
				{
					string text5 = row["ProductID"].ToString();
					string warehouseLocationID = row["LocationID"].ToString();
					decimal num2 = default(decimal);
					decimal result3 = default(decimal);
					int rowIndex = int.Parse(row["RowIndex"].ToString());
					dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(text5, docLocationID, warehouseLocationID, text2, sqlTransaction);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException("Product accounts information not found for product or location.");
					}
					string text6 = dataSet.Tables[0].Rows[0]["InventoryAssetAccountID"].ToString();
					if (flag)
					{
						decimal.TryParse(row["AmountFC"].ToString(), out result3);
					}
					else
					{
						decimal.TryParse(row["Amount"].ToString(), out result3);
					}
					ItemTypes itemTypes = ItemTypes.Inventory;
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "ItemType", "ProductID", text5, sqlTransaction);
					if (fieldValue == null || !(fieldValue.ToString() != ""))
					{
						throw new CompanyException("Item type is not selected for the product:" + text5);
					}
					itemTypes = (ItemTypes)byte.Parse(fieldValue.ToString());
					num2 = ((row["UnitQuantity"] == DBNull.Value) ? decimal.Parse(row["Quantity"].ToString()) : decimal.Parse(row["UnitQuantity"].ToString()));
					new Products(base.DBConfig).GetAverageCost(text5, sqlTransaction);
					decimal num3 = default(decimal);
					num3 = ((!(num2 >= 0m)) ? Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text5, text2, voucherID, rowIndex, -1m * num2, sqlTransaction)) : (-1m * Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text5, text2, voucherID, rowIndex, -1m * num2, sqlTransaction))));
					decimal result4 = default(decimal);
					if (flag)
					{
						decimal.TryParse(row["UnitPriceFC"].ToString(), out result4);
					}
					else
					{
						decimal.TryParse(row["UnitPrice"].ToString(), out result4);
					}
					string text7;
					if (itemTypes == ItemTypes.Inventory)
					{
						text7 = text4;
						if (hashtable.ContainsKey(text7))
						{
							num = decimal.Parse(hashtable[text7].ToString());
							num += Math.Round(num3, currencyDecimalPoints);
							hashtable[text7] = num;
						}
						else
						{
							hashtable.Add(text7, Math.Round(num3, currencyDecimalPoints));
							arrayList.Add(text7);
						}
						text7 = text6;
						if (hashtable3.ContainsKey(text7))
						{
							num = decimal.Parse(hashtable3[text7].ToString());
							num += Math.Round(num3, currencyDecimalPoints);
							hashtable3[text7] = num;
						}
						else
						{
							hashtable3.Add(text7, Math.Round(num3, currencyDecimalPoints));
							arrayList3.Add(text7);
						}
						d += Math.Round(num3, currencyDecimalPoints);
					}
					text7 = text3;
					if (hashtable2.ContainsKey(text7))
					{
						num = decimal.Parse(hashtable2[text7].ToString());
						num += Math.Round(result3, currencyDecimalPoints);
						hashtable2[text7] = num;
					}
					else
					{
						num = Math.Round(result3, currencyDecimalPoints);
						hashtable2.Add(text7, Math.Round(num, currencyDecimalPoints));
						arrayList2.Add(text7);
					}
					d2 += Math.Round(result3, currencyDecimalPoints);
				}
				DataRow dataRow5;
				if (d != 0m)
				{
					for (int i = 0; i < hashtable3.Count; i++)
					{
						dataRow5 = gLData.JournalDetailsTable.NewRow();
						dataRow5.BeginEdit();
						string text7 = arrayList3[i].ToString();
						if (text7 == "")
						{
							throw new CompanyException("Inventory asset account is not assigned to the location or product.");
						}
						num = decimal.Parse(hashtable3[text7].ToString());
						dataRow5["JournalID"] = 0;
						dataRow5["AccountID"] = text7;
						dataRow5["PayeeID"] = text;
						if (num > 0m)
						{
							dataRow5["Debit"] = Math.Abs(num);
							dataRow5["Credit"] = DBNull.Value;
						}
						else
						{
							dataRow5["Debit"] = DBNull.Value;
							dataRow5["Credit"] = Math.Abs(num);
						}
						dataRow5["IsBaseOnly"] = true;
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
						dataRow5 = gLData.JournalDetailsTable.NewRow();
						dataRow5.BeginEdit();
						string text7 = arrayList[j].ToString();
						if (text7 == "")
						{
							throw new CompanyException("Consignment Out COGS account is not assigned to the location or product.");
						}
						num = decimal.Parse(hashtable[text7].ToString());
						dataRow5["JournalID"] = 0;
						dataRow5["AccountID"] = text7;
						dataRow5["PayeeID"] = text;
						if (num > 0m)
						{
							dataRow5["Debit"] = DBNull.Value;
							dataRow5["Credit"] = Math.Abs(num);
						}
						else
						{
							dataRow5["Debit"] = Math.Abs(num);
							dataRow5["Credit"] = DBNull.Value;
						}
						dataRow5["IsBaseOnly"] = true;
						dataRow5["CompanyID"] = value;
						dataRow5["DivisionID"] = value2;
						dataRow5["Reference"] = dataRow["Reference"];
						dataRow5["JVEntryType"] = (byte)2;
						dataRow5.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow5);
					}
				}
				for (int k = 0; k < hashtable2.Count; k++)
				{
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					string text7 = arrayList2[k].ToString();
					if (text7 == "")
					{
						throw new CompanyException("Consignment Out sales account is not assigned to the location or product.");
					}
					num = decimal.Parse(hashtable2[text7].ToString());
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = text7;
					dataRow5["PayeeID"] = text;
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
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["JVEntryType"] = (byte)3;
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				decimal d3 = default(decimal);
				foreach (DataRow row2 in transactionData.ExpenseTable.Rows)
				{
					string text8 = row2["ExpenseID"].ToString();
					num = default(decimal);
					num = ((!flag) ? decimal.Parse(row2["Amount"].ToString()) : decimal.Parse(row2["AmountFC"].ToString()));
					string expenseAccountID = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text8, sqlTransaction);
					decimal result5 = 1m;
					decimal.TryParse(row2["CurrencyRate"].ToString(), out result5);
					num = Math.Round(num, currencyDecimalPoints);
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = expenseAccountID;
					if (flag)
					{
						if (num > 0m)
						{
							dataRow5["DebitFC"] = num;
							dataRow5["CreditFC"] = DBNull.Value;
						}
						else
						{
							dataRow5["DebitFC"] = DBNull.Value;
							dataRow5["CreditFC"] = Math.Abs(num);
						}
					}
					else if (num > 0m)
					{
						dataRow5["Debit"] = num;
						dataRow5["Credit"] = DBNull.Value;
					}
					else
					{
						dataRow5["Debit"] = DBNull.Value;
						dataRow5["Credit"] = Math.Abs(num);
					}
					dataRow5["JVEntryType"] = (byte)4;
					dataRow5["Reference"] = text8;
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					bool flag2 = true;
					if (!row2["IsDeduct"].IsDBNullOrEmpty())
					{
						flag2 = Convert.ToBoolean(row2["IsDeduct"].ToString());
					}
					if (flag2)
					{
						gLData.JournalDetailsTable.Rows.Add(dataRow5);
						d3 += num;
					}
				}
				decimal result6 = default(decimal);
				decimal result7 = default(decimal);
				if (dataRow["DiscountFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["DiscountFC"].ToString(), out result6);
				}
				else
				{
					decimal.TryParse(dataRow["Discount"].ToString(), out result6);
				}
				if (dataRow["TaxAmountFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["TaxAmountFC"].ToString(), out result7);
				}
				else
				{
					decimal.TryParse(dataRow["TaxAmount"].ToString(), out result7);
				}
				if (result6 > 0m)
				{
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = value3;
					dataRow5["PayeeID"] = text;
					dataRow5["PayeeType"] = "A";
					if (flag)
					{
						dataRow5["DebitFC"] = result6;
						dataRow5["CreditFC"] = DBNull.Value;
					}
					else
					{
						dataRow5["Debit"] = result6;
						dataRow5["Credit"] = DBNull.Value;
					}
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["JVEntryType"] = (byte)5;
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				if (result7 > 0m)
				{
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = value4;
					dataRow5["PayeeID"] = text;
					dataRow5["PayeeType"] = "A";
					if (flag)
					{
						dataRow5["DebitFC"] = DBNull.Value;
						dataRow5["CreditFC"] = result7;
					}
					else
					{
						dataRow5["Debit"] = DBNull.Value;
						dataRow5["Credit"] = result7;
					}
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["JVEntryType"] = (byte)6;
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["DueDate"] = dataRow["DueDate"];
				dataRow5["AccountID"] = value5;
				dataRow5["PayeeID"] = text;
				dataRow5["PayeeType"] = "C";
				dataRow5["IsARAP"] = true;
				if (flag)
				{
					dataRow5["DebitFC"] = d2 - d3 - result6 + result7;
					dataRow5["CreditFC"] = DBNull.Value;
				}
				else
				{
					dataRow5["Debit"] = d2 - d3 - result6 + result7;
					dataRow5["Credit"] = DBNull.Value;
				}
				dataRow5["JVEntryType"] = (byte)7;
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

		public bool ReCostTransactionOld(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				ConsignOutSettlementData consignOutSettlementData = null;
				consignOutSettlementData = GetSettlementByID(sysDocID, voucherID);
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (consignOutSettlementData == null || consignOutSettlementData.Tables.Count == 0 || consignOutSettlementData.ConsignOutSettlementTable.Rows.Count == 0)
				{
					throw new Exception("Transaction not found.\nTransaction: " + sysDocID + "-" + voucherID);
				}
				GLData gLData = null;
				gLData = CreateInvoiceGLData(consignOutSettlementData, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(gLData, isUpdate: false, sqlTransaction);
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

		public bool ReCostTransaction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				ConsignOutSettlementData consignOutSettlementData = null;
				consignOutSettlementData = GetSettlementByID(sysDocID, voucherID);
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (consignOutSettlementData == null || consignOutSettlementData.Tables.Count == 0 || consignOutSettlementData.ConsignOutSettlementTable.Rows.Count == 0)
				{
					throw new Exception("Transaction not found.\nTransaction: " + sysDocID + "-" + voucherID);
				}
				GLData gLData = null;
				gLData = CreateInvoiceGLData(consignOutSettlementData, sqlTransaction);
				object obj = null;
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

		public ConsignOutSettlementData GetSettlementByID(string sysDocID, string voucherID)
		{
			return GetSettlementByID(sysDocID, voucherID, null);
		}

		internal ConsignOutSettlementData GetSettlementByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				ConsignOutSettlementData consignOutSettlementData = new ConsignOutSettlementData();
				string text = "SELECT * FROM ConsignOut_Settlement WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				new SqlCommand(text).Transaction = sqlTransaction;
				FillDataSet(consignOutSettlementData, "ConsignOut_Settlement", text, sqlTransaction);
				if (consignOutSettlementData == null || consignOutSettlementData.Tables.Count == 0 || consignOutSettlementData.Tables["ConsignOut_Settlement"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT SD.*,P.Description,P.ItemType,IsTrackLot,IsTrackSerial,COD.Quantity AS DeliveredQty,COD.RowIndex AS ConsignRowIndex,QuantitySettled, COD.Quantity - ISNULL(QuantitySettled,0) - ISNULL(QuantityReturned,0) + SD.Quantity AS BalanceQty\r\n                        FROM ConsignOut_Settlement_Detail SD INNER JOIN Product P ON SD.ProductID=P.ProductID\r\n                        INNER JOIN Consign_Out_Detail COD ON COD.SysDocID=SD.ConsignSysDocID\r\n                        AND COD.VoucherID=SD.ConsignVoucherID AND COD.RowIndex=SD.ConsignRowIndex\r\n                        WHERE SD.VoucherID='" + voucherID + "' AND SD.SysDocID='" + sysDocID + "'";
				FillDataSet(consignOutSettlementData, "ConsignOut_Settlement_Detail", text, sqlTransaction);
				text = "SELECT *,ISNULL(IsDeduct,1) AS Deductable FROM ConsignOut_Expense\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(consignOutSettlementData, "ConsignOut_Expense", text);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (consignOutSettlementData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					consignOutSettlementData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				consignOutSettlementData.Merge(transactionIssuesProductLots, preserveChanges: false);
				return consignOutSettlementData;
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
				ConsignOutSettlementData consignOutSettlementData = new ConsignOutSettlementData();
				string textCommand = "SELECT SO.ConsignSysDocID,SO.ConsignVoucherID,SOD.*,ISNULL(ISVOID,'False') AS IsVoid\r\n                                    FROM ConsignOut_Settlement_Detail SOD INNER JOIN ConsignOut_Settlement SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                                    WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(consignOutSettlementData, "ConsignOut_Settlement_Detail", textCommand, sqlTransaction);
				if (consignOutSettlementData.ConsignOutSettlementDetailTable.Rows.Count == 0)
				{
					return true;
				}
				string sysDocID2 = consignOutSettlementData.ConsignOutSettlementDetailTable.Rows[0]["ConsignSysDocID"].ToString();
				string voucherID2 = consignOutSettlementData.ConsignOutSettlementDetailTable.Rows[0]["ConsignVoucherID"].ToString();
				bool result = false;
				bool.TryParse(consignOutSettlementData.ConsignOutSettlementDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (!result)
				{
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(48, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
					foreach (DataRow row in consignOutSettlementData.ConsignOutSettlementDetailTable.Rows)
					{
						row["ProductID"].ToString();
						string s = row["ConsignRowIndex"].ToString();
						float num = float.Parse(row["Quantity"].ToString());
						flag &= UpdateRowSettledQuantity(sysDocID2, voucherID2, int.Parse(s), -1f * num, sqlTransaction);
					}
				}
				textCommand = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM ConsignOut_Settlement_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				string textCommand = "SELECT Quantity,UnitQuantity,QuantitySettled FROM Consign_Out_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				textCommand = "UPDATE Consign_Out_Detail SET QuantitySettled=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				ConsignOutSettlementData consignOutSettlementData = new ConsignOutSettlementData();
				string textCommand = "SELECT SO.ConsignSysDocID,SO.ConsignVoucherID,SOD.*,ISNULL(ISVOID,'False') AS IsVoid,ISNULL(IsCash,'False') AS IsCash FROM ConsignOut_Settlement_Detail SOD INNER JOIN ConsignOut_Settlement SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(consignOutSettlementData, "ConsignOut_Settlement_Detail", textCommand, sqlTransaction);
				string sysDocID2 = consignOutSettlementData.Tables["ConsignOut_Settlement_Detail"].Rows[0]["ConsignSysDocID"].ToString();
				string voucherID2 = consignOutSettlementData.Tables["ConsignOut_Settlement_Detail"].Rows[0]["ConsignVoucherID"].ToString();
				bool result = false;
				bool.TryParse(consignOutSettlementData.ConsignOutSettlementDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(consignOutSettlementData.ConsignOutSettlementDetailTable.Rows[0]["IsCash"].ToString(), out result2);
				if (result == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				flag = ((!result2) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(48, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(3, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)));
				foreach (DataRow row in consignOutSettlementData.ConsignOutSettlementDetailTable.Rows)
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
					flag &= UpdateRowSettledQuantity(sysDocID2, voucherID2, result3, -1f * result4, sqlTransaction);
				}
				flag &= new ConsignOut(base.DBConfig).CloseOpenConsignment(sysDocID2, voucherID2, sqlTransaction);
				flag &= new ConsignOut(base.DBConfig).UpdateConsignmentStatus(sysDocID2, voucherID2, sqlTransaction);
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				textCommand = "UPDATE ConsignOut_Settlement SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Consign Out Settlement", voucherID, sysDocID, activityType, sqlTransaction);
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
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("ConsignOut_Settlement", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidSettlement(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeleteSettlementDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM ConsignOut_Settlement WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Consign OUt Settlement", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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
				string str = "SELECT COUNT(RowIndex)FROM ConsignOut_Settlement_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				str += " AND CASE WHEN UnitQuantity IS NULL THEN Quantity ELSE UnitQuantity END - ISNULL(QuantityShipped,0)=0";
				object obj = ExecuteScalar(str, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				str = "UPDATE ConsignOut_Settlement SET IsDelivered = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityShipped FROM ConsignOut_Settlement_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				textCommand = "UPDATE ConsignOut_Settlement_Detail SET QuantityShipped=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer] FROM ConsignOut_Settlement SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID  WHERE ISNULL(IsDelivered,0)=0";
				if (customerID != "")
				{
					text = text + " AND SO.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "ConsignOut_Settlement", text);
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
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,TransactionDate,SI.ConsignSysDocID,SI.ConsignVoucherID,\r\n                                IsCash,SI.SalesPersonID,RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,IsVoid,Reference,ISNULL(DiscountFC,Discount) AS Discount,\r\n                                ISNULL(ISNULL(TaxAmountFC,TaxAmount) ,0) AS Tax,ISNULL(TotalFC,Total) AS Total,PONumber,SI.Note,SI.CreatedBy,SI.UpdatedBy,SI.DateCreated,SI.DateUpdated\r\n                                FROM  ConsignOut_Settlement SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "ConsignOut_Settlement", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["ConsignOut_Settlement"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,ProductID,Description,ISNULL(UnitQuantity,Quantity) AS Quantity,\r\n                        ISNULL(UnitPriceFC,UnitPrice) AS UnitPrice,\r\n                        ISNULL(UnitQuantity,Quantity)*ISNULL(UnitPriceFC,UnitPrice) AS Total,UnitID,LocationID\r\n                        FROM   ConsignOut_Settlement_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "ConsignOut_Settlement_Detail", cmdText);
				cmdText = "SELECT * FROM ConsignOut_Expense  WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "ConsignOut_Expense", cmdText);
				dataSet.Relations.Add("CustomerInvoice", new DataColumn[2]
				{
					dataSet.Tables["ConsignOut_Settlement"].Columns["SysDocID"],
					dataSet.Tables["ConsignOut_Settlement"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["ConsignOut_Settlement_Detail"].Columns["SysDocID"],
					dataSet.Tables["ConsignOut_Settlement_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("ConsignOutExpense", new DataColumn[2]
				{
					dataSet.Tables["ConsignOut_Settlement"].Columns["SysDocID"],
					dataSet.Tables["ConsignOut_Settlement"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["ConsignOut_Expense"].Columns["SysDocID"],
					dataSet.Tables["ConsignOut_Expense"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["ConsignOut_Settlement"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["ConsignOut_Settlement"].Rows)
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

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   ISNULL(IsVoid,'False') AS 'V', SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Invoice Date],\r\n                            CASE ISNULL(IsCash,'False') WHEN 'True' THEN 'Cash' ELSE 'Credit' END AS [Type],INV.SalespersonID [Salesperson],Total [Amount]\r\n                            FROM         ConsignOut_Settlement INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "ConsignOut_Settlement", sqlCommand);
			return dataSet;
		}
	}
}
