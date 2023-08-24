using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PurchaseQuote : StoreObject
	{
		private const string PURCHASEQUOTE_TABLE = "Purchase_Quote";

		private const string PURCHASEQUOTEDETAIL_TABLE = "Purchase_Quote_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string VENDORID_PARM = "@VendorID";

		private const string ISIMPORT_PARM = "@IsImport";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string PURCHASEFLOW_PARM = "@PurchaseFlow";

		private const string BUYERID_PARM = "@BuyerID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string VENDORADDRESS_PARM = "@VendorAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string PRICEINCLUDETAX_PARM = "@PriceIncludeTax";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE_PARM2 = "@Reference2";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string DISCOUNT_PARM = "@Discount";

		private const string TOTAL_PARM = "@Total";

		private const string DUEDATE_PARM = "@DueDate";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string VENDORREFERENCENUMBER_PARM = "@VendorReferenceNo";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

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

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		public PurchaseQuote(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePurchaseQuoteText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Quote", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("PurchaseFlow", "@PurchaseFlow"), new FieldValue("IsImport", "@IsImport"), new FieldValue("DueDate", "@DueDate"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("VendorAddress", "@VendorAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Discount", "@Discount"), new FieldValue("Total", "@Total"), new FieldValue("PONumber", "@PONumber"), new FieldValue("TermID", "@TermID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("VendorReferenceNo", "@VendorReferenceNo"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Purchase_Quote", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseQuoteCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseQuoteText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseQuoteText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@VendorAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters.Add("@VendorReferenceNo", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@PurchaseFlow"].SourceColumn = "PurchaseFlow";
			parameters["@IsImport"].SourceColumn = "IsImport";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@VendorAddress"].SourceColumn = "VendorAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@PriceIncludeTax"].SourceColumn = "PriceIncludeTax";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
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

		private string GetInsertUpdatePurchaseQuoteDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Quote_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitID", "@UnitID"), new FieldValue("JobID", "@JobID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("SubunitPrice", "@SubunitPrice"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseQuoteDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseQuoteDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseQuoteDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(PurchaseQuoteData journalData)
		{
			return true;
		}

		public bool CanUpdate(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Purchase_Quote_Detail POD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityOrdered,0))>0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public bool InsertUpdatePurchaseQuote_New(PurchaseQuoteData purchaseQuoteData, bool isUpdate)
		{
			bool flag = true;
			bool flag2 = false;
			try
			{
				if (purchaseQuoteData.PurchaseQuoteTable.Rows.Count > 1)
				{
					int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
					string text = "";
					string text2 = "";
					SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
					SqlCommand sqlCommand = GetInsertUpdatePurchaseQuoteCommand(isUpdate);
					int num = 0;
					{
						foreach (DataRow row in purchaseQuoteData.PurchaseQuoteTable.Rows)
						{
							text = row["VoucherID"].ToString();
							text2 = row["SysDocID"].ToString();
							if (!row["IsImport"].IsDBNullOrEmpty())
							{
								flag2 = bool.Parse(row["IsImport"].ToString());
							}
							if (isUpdate && !CanUpdate(text2, text, sqlTransaction))
							{
								throw new CompanyException("Unable to update. Some of the items has been already ordered.", 1037);
							}
							decimal num2 = default(decimal);
							foreach (DataRow row2 in purchaseQuoteData.PurchaseQuoteDetailTable.Rows)
							{
								decimal num3 = default(decimal);
								decimal result = default(decimal);
								decimal result2 = default(decimal);
								decimal.TryParse(row2["Quantity"].ToString(), out result);
								decimal.TryParse(row2["UnitPrice"].ToString(), out result2);
								num3 = Math.Round(result * result2, currencyDecimalPoints);
								num2 += num3;
							}
							row["Total"] = num2;
							if (num == 0 && !isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Purchase_Quote", "VoucherID", row["SysDocID"].ToString(), text, sqlTransaction))
							{
								throw new CompanyException("Document number already exist.", 1046);
							}
							foreach (DataRow row3 in purchaseQuoteData.PurchaseQuoteDetailTable.Rows)
							{
								row3["SysDocID"] = row["SysDocID"];
								row3["VoucherID"] = row["VoucherID"];
							}
							DataSet dataSet = purchaseQuoteData.Copy();
							sqlCommand.Transaction = sqlTransaction;
							if (num == 0)
							{
								flag = (isUpdate ? (flag & Update(dataSet, "Purchase_Quote", sqlCommand)) : (flag & Insert(dataSet, "Purchase_Quote", sqlCommand)));
							}
							num = 1;
							sqlCommand = GetInsertUpdatePurchaseQuoteDetailsCommand(isUpdate: false);
							sqlCommand.Transaction = sqlTransaction;
							if (isUpdate)
							{
								flag &= DeletePurchaseQuoteDetailsRows(text2, text, sqlTransaction);
							}
							if (purchaseQuoteData.Tables["Purchase_Quote_Detail"].Rows.Count > 0)
							{
								flag &= Insert(dataSet, "Purchase_Quote_Detail", sqlCommand, sqlTransaction);
							}
							if (purchaseQuoteData.Tables.Contains("Tax_Detail"))
							{
								flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(purchaseQuoteData, text2, text, isUpdate, sqlTransaction);
							}
							dataSet.Clear();
							dataSet.AcceptChanges();
							if (flag)
							{
								flag &= UpdateTableRowInsertUpdateInfo("Purchase_Quote", "SysDocID", row["SysDocID"].ToString(), "VoucherID", row["VoucherID"].ToString(), sqlTransaction, !isUpdate);
								string entityName = "Purchase Quote";
								flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
								if (isUpdate)
								{
									throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
								}
								flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(row["SysDocID"].ToString(), row["VoucherID"].ToString(), "Purchase_Quote", "VoucherID", sqlTransaction);
								if (flag)
								{
									if (flag2)
									{
										new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ProformaInvoice, text2, text, "Purchase_Quote", sqlTransaction);
									}
									else
									{
										new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PurchaseQuote, text2, text, "Purchase_Quote", sqlTransaction);
									}
								}
							}
							base.DBConfig.EndTransaction(flag);
						}
						return flag;
					}
				}
				int currencyDecimalPoints2 = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow2 = purchaseQuoteData.PurchaseQuoteTable.Rows[0];
				SqlTransaction sqlTransaction2 = base.DBConfig.StartNewTransaction();
				SqlCommand insertUpdatePurchaseQuoteCommand = GetInsertUpdatePurchaseQuoteCommand(isUpdate);
				string text3 = dataRow2["VoucherID"].ToString();
				string sysDocID = dataRow2["SysDocID"].ToString();
				if (!dataRow2["IsImport"].IsDBNullOrEmpty())
				{
					flag2 = bool.Parse(dataRow2["IsImport"].ToString());
				}
				if (isUpdate && !CanUpdate(sysDocID, text3, sqlTransaction2))
				{
					throw new CompanyException("Unable to update. Some of the items has been already ordered.", 1037);
				}
				decimal num4 = default(decimal);
				foreach (DataRow row4 in purchaseQuoteData.PurchaseQuoteDetailTable.Rows)
				{
					decimal num5 = default(decimal);
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal.TryParse(row4["Quantity"].ToString(), out result3);
					decimal.TryParse(row4["UnitPrice"].ToString(), out result4);
					num5 = Math.Round(result3 * result4, currencyDecimalPoints2);
					num4 += num5;
				}
				dataRow2["Total"] = num4;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Purchase_Quote", "VoucherID", dataRow2["SysDocID"].ToString(), text3, sqlTransaction2))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row5 in purchaseQuoteData.PurchaseQuoteDetailTable.Rows)
				{
					row5["SysDocID"] = dataRow2["SysDocID"];
					row5["VoucherID"] = dataRow2["VoucherID"];
				}
				insertUpdatePurchaseQuoteCommand.Transaction = sqlTransaction2;
				flag = (isUpdate ? (flag & Update(purchaseQuoteData, "Purchase_Quote", insertUpdatePurchaseQuoteCommand)) : (flag & Insert(purchaseQuoteData, "Purchase_Quote", insertUpdatePurchaseQuoteCommand)));
				insertUpdatePurchaseQuoteCommand = GetInsertUpdatePurchaseQuoteDetailsCommand(isUpdate: false);
				insertUpdatePurchaseQuoteCommand.Transaction = sqlTransaction2;
				if (isUpdate)
				{
					flag &= DeletePurchaseQuoteDetailsRows(sysDocID, text3, sqlTransaction2);
				}
				if (purchaseQuoteData.Tables["Purchase_Quote_Detail"].Rows.Count > 0)
				{
					flag &= Insert(purchaseQuoteData, "Purchase_Quote_Detail", insertUpdatePurchaseQuoteCommand);
				}
				if (purchaseQuoteData.Tables.Contains("Tax_Detail"))
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(purchaseQuoteData, sysDocID, text3, isUpdate, sqlTransaction2);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Purchase_Quote", "SysDocID", dataRow2["SysDocID"].ToString(), "VoucherID", dataRow2["VoucherID"].ToString(), sqlTransaction2, !isUpdate);
				string entityName2 = "Purchase Quote";
				flag = (isUpdate ? (flag & AddActivityLog(entityName2, text3, sysDocID, ActivityTypes.Update, sqlTransaction2)) : (flag & AddActivityLog(entityName2, text3, sysDocID, ActivityTypes.Add, sqlTransaction2)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow2["SysDocID"].ToString(), dataRow2["VoucherID"].ToString(), "Purchase_Quote", "VoucherID", sqlTransaction2);
				}
				if (!flag)
				{
					return flag;
				}
				if (!flag2)
				{
					new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PurchaseQuote, sysDocID, text3, "Purchase_Quote", sqlTransaction2);
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ProformaInvoice, sysDocID, text3, "Purchase_Quote", sqlTransaction2);
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

		public bool InsertUpdatePurchaseQuote(PurchaseQuoteData purchaseQuoteData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePurchaseQuoteCommand = GetInsertUpdatePurchaseQuoteCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = purchaseQuoteData.PurchaseQuoteTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (isUpdate && !CanUpdate(sysDocID, text, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items has been already ordered.", 1037);
				}
				decimal num = default(decimal);
				foreach (DataRow row in purchaseQuoteData.PurchaseQuoteDetailTable.Rows)
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
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Purchase_Quote", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row2 in purchaseQuoteData.PurchaseQuoteDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
				}
				insertUpdatePurchaseQuoteCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(purchaseQuoteData, "Purchase_Quote", insertUpdatePurchaseQuoteCommand)) : (flag & Insert(purchaseQuoteData, "Purchase_Quote", insertUpdatePurchaseQuoteCommand)));
				insertUpdatePurchaseQuoteCommand = GetInsertUpdatePurchaseQuoteDetailsCommand(isUpdate: false);
				insertUpdatePurchaseQuoteCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeletePurchaseQuoteDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (purchaseQuoteData.Tables["Purchase_Quote_Detail"].Rows.Count > 0)
				{
					flag &= Insert(purchaseQuoteData, "Purchase_Quote_Detail", insertUpdatePurchaseQuoteCommand);
				}
				if (purchaseQuoteData.Tables.Contains("Tax_Detail"))
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(purchaseQuoteData, sysDocID, text, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Purchase_Quote", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Purchase Quote";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Purchase_Quote", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PurchaseQuote, sysDocID, text, "Purchase_Quote", sqlTransaction);
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

		public PurchaseQuoteData GetPurchaseQuoteByID(string sysDocID, string voucherID)
		{
			try
			{
				PurchaseQuoteData purchaseQuoteData = new PurchaseQuoteData();
				string textCommand = "SELECT * FROM Purchase_Quote WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseQuoteData, "Purchase_Quote", textCommand);
				if (purchaseQuoteData == null || purchaseQuoteData.Tables.Count == 0 || purchaseQuoteData.Tables["Purchase_Quote"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID,ISNull(Product.TaxGroupID,PC.TaxGroupID) AS TaxGroupID\r\n                        FROM Purchase_Quote_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        LEFT OUTER JOIN Product_Class PC ON PC.ClassID = Product.ClassID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(purchaseQuoteData, "Purchase_Quote_Detail", textCommand);
				textCommand = "SELECT * FROM Purchase_Receipt PR  INNER JOIN Purchase_Quote PO ON PO.SysDocID=PR.SourceSysDocID AND PO.VoucherID=PR.SourceVoucherID\r\n\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseQuoteData, "Purchase_Receipt", textCommand);
				textCommand = "SELECT DISTINCT JMR.VoucherID,JMR.* FROM Job_Material_Requisition JMR INNER JOIN Purchase_Quote_Detail POD ON JMR.VoucherID=POD.SourceVoucherID AND JMR.SysDocID=POD.SourceSysDocID WHERE POD.VoucherID='" + voucherID + "' AND POD.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseQuoteData, "Job_Material_Requisition", textCommand);
				textCommand = "SELECT DISTINCT PO.VoucherID,PO.SysDocID FROM Purchase_Order_Detail PO INNER JOIN Purchase_Quote_Detail PQD ON PQD.VoucherID=PO.SourceVoucherID AND PQD.SysDocID=PO.SourceSysDocID WHERE PQD.VoucherID='" + voucherID + "' AND PQD.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseQuoteData, "Purchase_Order_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseQuoteData, "Tax_Detail", textCommand);
				return purchaseQuoteData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePurchaseQuoteDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Purchase_Quote_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag = Delete(commandText, sqlTransaction);
				return flag & new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePurchaseQuoteRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Purchase_Quote WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidPurchaseQuote(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to void. Some of the items has been already ordered.", 1037);
				}
				string exp = "UPDATE Purchase_Quote SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Purchase Quote", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeletePurchaseQuote(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to delete. Some of the items has been already ordered.", 1037);
				}
				flag &= DeletePurchaseQuoteDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Purchase_Quote WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Purchase Quote", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetPurchaseQuoteToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems)
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
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.VendorID,VendorName,TransactionDate,B.FullName, VA.ContactName, ISNULL(VA.Phone1, VA.Phone2) TEL, VA.Fax,\r\n                                SI.BuyerID,VA.AddressPrintFormat AS VendorAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,IsVoid,SI.Reference,SI.Reference2,Discount AS Discount,ISNULL(Total,0) - ISNULL(Discount,0) AS GrandTotal,\r\n                                ISNULL(TaxAmount ,0) AS Tax,Total AS Total,SI.PONumber,SI.Note,VA.Mobile, VA.Email,Vendor.TaxIDNumber as STaxIDNo,job.JobName as Job,Job.Note [Job Note] \r\n                                FROM  Purchase_Quote SI INNER JOIN Vendor ON SI.VendorID=Vendor.VendorID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT JOIN Buyer B ON SI.BuyerID=B.BuyerID \r\n                                LEFT JOIN Job  ON SI.JobId=Job.Jobid \r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Purchase_Quote", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Purchase_Quote"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = ((!mergeMatrixItems) ? ("SELECT     SysDocID,VoucherID,PPC.ProductParentID,SINVD.ProductID,SINVD.Description,PPC.Attribute1,PPC.Attribute2,PPC.Attribute3,ISNULL(UnitQuantity,SINVD.Quantity) AS Quantity,\r\n                            SINVD.UnitPrice AS UnitPrice,   \r\n                            ISNULL(UnitQuantity,SINVD.Quantity)*ISNULL(SINVD.UnitPrice,0) AS Total,SINVD.UnitID, RowIndex + 1 RowIndex, PB.BrandName,\r\n                            P.Description2,SINVD.TaxAmount, SINVD.TaxGroupID,TG.TaxGroupName,SINVD.Remarks,J.JobName,\r\n                            P.ClassID, PC.ClassName, P.CategoryID, PG.CategoryName, P.ManufacturerID, PM.ManufacturerName, P.UPC, P.Attribute1 ,P.Attribute2 ,SINVD.UnitID, Unit.UnitName\r\n                            FROM   Purchase_Quote_Detail SINVD\r\n                            LEFT JOIN Tax_Group TG ON SINVD.TaxGroupID=TG.TaxGroupID\r\n                            LEFT OUTER JOIN Product_Parent_Components PPC ON SINVD.ProductID=PPC.ProductID\r\n                            INNER JOIN Product P ON P.ProductID = SINVD.ProductID\r\n                            LEFT JOIN Product_Brand PB ON P.BrandID=PB.BrandID \r\n                            LEFT OUTER JOIN Job J ON J.JobID=SINVD.JobID\r\n                            LEFT OUTER JOIN Product_Class PC On P.ClassID=PC.ClassID\r\n                            LEFT OUTER JOIN Product_Category PG ON P.CategoryID=PG.CategoryID\r\n                            LEFT OUTER JOIN Product_Manufacturer PM ON P.ManufacturerID=PM.ManufacturerID\r\n                            LEFT OUTER JOIN Unit ON SINVD.UnitID=Unit.UnitID \r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex") : ("SELECT     SysDocID,VoucherID, ISNULL(PPC.ProductParentID,SINVD.ProductID) AS ProductID,ISNULL(PP.Description,SINVD.Description) AS Description,SUM(ISNULL(UnitQuantity,SINVD.Quantity)) AS Quantity,\r\n                            SINVD.UnitPrice AS UnitPrice,\r\n                            SUM(ISNULL(UnitQuantity,SINVD.Quantity) * SINVD.UnitPrice) AS Total,SINVD.UnitID, RowIndex + 1 RowIndex\r\n                            FROM   Purchase_Quote_Detail SINVD\r\n\t\t\t\t\t\t    LEFT OUTER JOIN Product_Parent_Components PPC ON SINVD.ProductID=PPC.ProductID\r\n\t\t\t\t\t\t    LEFT OUTER JOIN Product_Parent PP ON PP.ProductParentID = PPC.ProductParentID\r\n                            WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")\r\n\t\t\t\t\t\t    GROUP BY SysDocID,VoucherID,RowIndex,PPC.ProductParentID,UnitPrice,ISNULL(PPC.ProductParentID,SINVD.ProductID),ISNULL(PP.Description,SINVD.Description),SINVD.UnitID\r\n                                 ORDER BY RowIndex "));
				FillDataSet(dataSet, "Purchase_Quote_Detail", cmdText);
				dataSet.Relations.Add("PurchaseQuote", new DataColumn[2]
				{
					dataSet.Tables["Purchase_Quote"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Quote"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Purchase_Quote_Detail"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Quote_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Purchase_Quote"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Purchase_Quote"].Rows)
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

		public DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Quote Date], INV.Reference as Ref1, INV.Reference2 as Ref2,\r\n                            INV.BuyerID [Buyer],Total - ISNULL(Discount,0) AS [Amount], INV.CurrencyID [Currency],\r\n                            J.JobID,J.JobName,ISNULL((CASE INV.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' END) ,(CASE Vendor.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' END))AS TAXOPTION\r\n                            FROM         Purchase_Quote INV\r\n                            LEFT JOIN Job J ON INV.JobID=J.JobID\r\n                            Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE ISNULL(IsImport,'False')= '" + isImport.ToString() + "'";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Quote", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenQuotesSummary(string vendorID, bool isImport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT PQ.SysDocID [Doc ID],PQ.VoucherID [Number],TransactionDate AS [Date],PQ.VendorID + '-' + Ven.VendorName AS [Vendor],\r\n                                PQ.Reference,J.JobName, Total AS Amount  FROM Purchase_Quote PQ\r\n                                LEFT OUTER JOIN Job J ON J.JobID=PQ.JobID\r\n                                INNER JOIN Vendor Ven ON PQ.VendorID=Ven.VendorID  where  ISNULL(IsVoid,'False')='False' AND ISNULL(PQ.Status,1) = 1  ";
				if (vendorID != "")
				{
					text = text + " AND (PQ.VendorID='" + vendorID + "' OR ISNULL(Ven.ParentVendorID,'') = '" + vendorID + "') ";
				}
				text = text + " AND ISNULL(IsImport,'False') = '" + isImport.ToString() + "'";
				FillDataSet(dataSet, "Purchase_Quote", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseComparisonReport(string refNumber)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT DISTINCT Reference FROM Purchase_Quote WHERE Reference = '" + refNumber + "'";
				FillDataSet(dataSet, "Reference_Quote", textCommand);
				textCommand = "SELECT PQ.VendorID,v.VendorName, PQ.Reference, PQD.ProductID, PQD.Quantity, PQD.VoucherID,\r\n                            PQD.UnitPrice,(pqd.Quantity* PQD.UnitPrice) AS Total, PQD.Description, JobName, PQ.JobID\r\n                            FROM  Purchase_Quote PQ INNER JOIN Purchase_Quote_Detail PQD \r\n                            ON PQ.SysDocID = PQD.SysDocID AND PQ.VoucherID = PQD.VoucherID\r\n                            LEFT JOIN VENDOR V ON PQ.VendorID=v.VendorID\r\n                            LEFT OUTER JOIN Job J ON PQD.JobID=J.JobID\r\n\t\t                    Where 1 = 1 AND PQ.Reference = '" + refNumber + "'";
				FillDataSet(dataSet, "Purchase_Quote", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowOrderedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			bool flag = true;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityOrdered FROM Purchase_Quote_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
					float.TryParse(dataRow["QuantityOrdered"].ToString(), out result2);
				}
				result2 += quantity;
				textCommand = "UPDATE Purchase_Quote_Detail SET QuantityOrdered=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				CloseOrderedQuote(sysDocID, voucherID, sqlTransaction);
				return flag;
			}
			catch
			{
				return false;
			}
		}

		internal bool CloseOrderedQuote(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string str = "SELECT COUNT(RowIndex)FROM Purchase_Quote_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				str += " AND CASE WHEN UnitQuantity IS NULL THEN Quantity ELSE UnitQuantity END - ISNULL(QuantityOrdered,0) > 0";
				object obj = ExecuteScalar(str, sqlTransaction);
				if (obj == null)
				{
					return true;
				}
				bool flag = false;
				if (int.Parse(obj.ToString()) == 0)
				{
					flag = true;
				}
				if (flag)
				{
					str = "UPDATE Purchase_Quote SET Status = 2 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
					return ExecuteNonQuery(str, sqlTransaction) > 0;
				}
				str = "UPDATE Purchase_Quote SET Status = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(str, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GettPurchaseQuoteList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PQ.Reference ,pq.VendorID,pq.VoucherID ,pq.Discount, pq.Total ,pq.DueDate,pq.TransactionDate\r\n                            FROM [Purchase_Quote] PQ\r\n                             WHERE ISNULL(IsVoid,'False')='False' ";
			FillDataSet(dataSet, "Purchase_Quote", textCommand);
			return dataSet;
		}
	}
}
