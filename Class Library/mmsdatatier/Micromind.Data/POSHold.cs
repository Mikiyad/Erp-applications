using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class POSHold : StoreObject
	{
		private const string POSHOLD_TABLE = "POS_Hold";

		private const string POSHOLDDETAIL_TABLE = "POS_Hold_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string SEARCHVALUE_PARM = "@SearchValue";

		private const string DISCOUNT_PARM = "@Discount";

		private const string DISCOUNTFC_PARM = "@DiscountFC";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TOTAL_PARM = "@Total";

		private const string TOTALFC_PARM = "@TotalFC";

		private const string ISCASH_PARM = "@IsCash";

		private const string DUEDATE_PARM = "@DueDate";

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

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string PRICEINCLUDETAX_PARM = "@PriceIncludeTax";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string ORDERVOUCHERID_PARM = "@OrderVoucherID";

		private const string ORDERSYSDOCID_PARM = "@OrderSysDocID";

		private const string DNOTEVOUCHERID_PARM = "@DNoteVoucherID";

		private const string DNOTESYSDOCID_PARM = "@DNoteSysDocID";

		private const string ORDERROWINDEX_PARM = "@OrderRowIndex";

		private const string ISDNROW_PARM = "@IsDNRow";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string INVOICEPAYMENT_PARM = "@Invoice_Payment";

		public POSHold(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePOSHoldText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("POS_Hold", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("DueDate", "@DueDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("SearchValue", "@SearchValue"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("IsCash", "@IsCash"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("POS_Hold", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePOSHoldCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePOSHoldText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePOSHoldText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@SearchValue", SqlDbType.NVarChar);
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
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@PriceIncludeTax"].SourceColumn = "PriceIncludeTax";
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

		private string GetInsertUpdatePOSHoldDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("POS_HOLD_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("Discount", "@Discount"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("OrderSysDocID", "@OrderSysDocID"), new FieldValue("OrderVoucherID", "@OrderVoucherID"), new FieldValue("DNoteSysDocID", "@DNoteSysDocID"), new FieldValue("DNoteVoucherID", "@DNoteVoucherID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("IsDNRow", "@IsDNRow"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePOSHoldDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePOSHoldDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePOSHoldDetailsText(isUpdate: false), base.DBConfig.Connection);
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

		private bool ValidateData(POSHoldData journalData)
		{
			return true;
		}

		public bool InsertUpdatePOSHold(SalesPOSData salesInvoiceData, bool isUpdate)
		{
			return InsertUpdatePOSHold(salesInvoiceData, isUpdate, null);
		}

		public bool InsertUpdatePOSHold(SalesPOSData salesInvoiceData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			string text = "";
			bool flag = true;
			SqlCommand sqlCommand = null;
			string text2 = "";
			try
			{
				DataRow dataRow = salesInvoiceData.SalesPOSTable.Rows[0];
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				dataRow["RegisterID"].ToString();
				decimal num = default(decimal);
				foreach (DataRow row in salesInvoiceData.SalesPOSDetailTable.Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result2);
					decimal.TryParse(row["UnitPrice"].ToString(), out result3);
					decimal.TryParse(row["Amount"].ToString(), out result);
					num += result;
				}
				string text3 = "";
				text = "SELECT Max(CONVERT(INT,VoucherID)) FROM POS_Hold";
				object obj2 = ExecuteScalar(text, sqlTransaction);
				text3 = (string)(dataRow["VoucherID"] = ((obj2 == null || !(obj2.ToString() != "")) ? "1" : (int.Parse(obj2.ToString()) + 1).ToString()));
				foreach (DataRow row2 in salesInvoiceData.SalesPOSDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					text2 = row2["ProductID"].ToString();
					string checkFieldValue = row2["LocationID"].ToString();
					decimal result4 = default(decimal);
					obj2 = new Databases(base.DBConfig).GetFieldValue("Product_Location", "Quantity", "ProductID", text2, "LocationID", checkFieldValue, sqlTransaction);
					if (obj2 != null)
					{
						decimal.TryParse(obj2.ToString(), out result4);
					}
					float num2 = 0f;
					string text4 = "";
					obj2 = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text2, sqlTransaction);
					if (obj2 != null)
					{
						text4 = obj2.ToString();
					}
					if (text4 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text4)
					{
						DataRow obj4 = new Products(base.DBConfig).GetProductUnitRow(text2, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text2 + "\nUnit:" + row2["UnitID"].ToString());
						float num3 = float.Parse(obj4["Factor"].ToString());
						string text5 = obj4["FactorType"].ToString();
						num2 = float.Parse(row2["Quantity"].ToString());
						row2["UnitFactor"] = num3;
						row2["FactorType"] = text5;
						row2["UnitQuantity"] = row2["Quantity"];
						num2 = ((!(text5 == "M")) ? float.Parse(Math.Round(num2 * num3, 5).ToString()) : float.Parse(Math.Round(num2 / num3, 5).ToString()));
						row2["Quantity"] = num2;
					}
				}
				sqlCommand = GetInsertUpdatePOSHoldCommand(isUpdate);
				sqlCommand.Transaction = sqlTransaction;
				salesInvoiceData.SalesPOSTable.TableName = "POS_Hold";
				salesInvoiceData.SalesPOSDetailTable.TableName = "POS_HOLD_Detail";
				flag = (isUpdate ? (flag & Update(salesInvoiceData, "POS_Hold", sqlCommand)) : (flag & Insert(salesInvoiceData, "POS_Hold", sqlCommand)));
				if (salesInvoiceData.Tables["POS_HOLD_Detail"].Rows.Count > 0)
				{
					sqlCommand = GetInsertUpdatePOSHoldDetailsCommand(isUpdate: false);
					sqlCommand.Transaction = sqlTransaction;
					flag &= Insert(salesInvoiceData, "POS_HOLD_Detail", sqlCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("POS_Hold", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
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

		public POSHoldData GetPOSHoldByID(string sysDocID, string voucherID)
		{
			return GetPOSHoldByID(sysDocID, voucherID, null);
		}

		internal POSHoldData GetPOSHoldByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				POSHoldData pOSHoldData = new POSHoldData();
				string text = "SELECT * FROM POS_Hold WHERE VoucherID='" + voucherID + "'";
				new SqlCommand(text).Transaction = sqlTransaction;
				FillDataSet(pOSHoldData, "POS_Hold", text, sqlTransaction);
				if (pOSHoldData == null || pOSHoldData.Tables.Count == 0 || pOSHoldData.Tables["POS_Hold"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT TD.*,Product.Description,Product.UPC,Product.ItemType\r\n                        FROM POS_HOld_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "'";
				FillDataSet(pOSHoldData, "POS_Hold_Detail", text, sqlTransaction);
				return pOSHoldData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePOSHoldDetailsRows(string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				new POSHoldData();
				string commandText = "DELETE FROM POS_HOLD_Detail WHERE VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool DeletePOSHold(string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeletePOSHoldDetailsRows(voucherID, sqlTransaction);
				text = "DELETE FROM POS_Hold WHERE  VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
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
				FillDataSet(dataSet, "POS_Hold", text);
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

		public DataSet GetPOSHoldToPrint(string sysDocID, string voucherID)
		{
			return GetPOSHoldToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPOSHoldToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,TransactionDate,\r\n                                IsCash,SI.SalesPersonID,RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,IsVoid,Reference,ISNULL(DiscountFC,Discount) AS Discount,\r\n                                ISNULL(ISNULL(TaxAmountFC,TaxAmount) ,0) AS Tax,ISNULL(TotalFC,Total) AS Total,PONumber,SI.Note\r\n                                FROM  Sales_POS SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "POS_Hold", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["POS_Hold"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,ProductID,Description,ISNULL(UnitQuantity,Quantity) AS Quantity,\r\n                        ISNULL(UnitPriceFC,UnitPrice) AS UnitPrice,\r\n                        ISNULL(UnitQuantity,Quantity)*ISNULL(UnitPriceFC,UnitPrice) AS Total,UnitID,LocationID\r\n                        FROM   Sales_POS_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "POS_HOLD_Detail", cmdText);
				dataSet.Relations.Add("CustomerInvoice", new DataColumn[2]
				{
					dataSet.Tables["POS_Hold"].Columns["SysDocID"],
					dataSet.Tables["POS_Hold"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["POS_Hold_Detail"].Columns["SysDocID"],
					dataSet.Tables["POS_HOLD_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["POS_Hold"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["POS_Hold"].Rows)
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

		public DataSet GetHoldDocumentList(string registerID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT VoucherID Num, CustomerID Customer,SearchValue AS [Hold ID],TransactionDate [Date] FROM POS_HOLD\r\n                                WHERE RegisterID='" + registerID + "' ");
			FillDataSet(dataSet, "POS_Hold", sqlCommand);
			return dataSet;
		}

		public bool SetSearchValue(string sysDocID, string voucherID, string searchValue)
		{
			try
			{
				string exp = "SELECT Max(CONVERT(INT,VoucherID)) FROM POS_Hold";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				object obj = ExecuteScalar(exp, sqlTransaction);
				voucherID = ((obj == null || !(obj.ToString() != "")) ? "1" : (int.Parse(obj.ToString()) + 1).ToString());
				exp = "UPDATE POS_HOLD SET SearchValue= '" + searchValue + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}
	}
}
