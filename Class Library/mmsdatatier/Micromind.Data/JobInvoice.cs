using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Micromind.Data
{
	[Guid("6EA7ED05-97A5-4987-BA5B-7CFC29F2548C")]
	public sealed class JobInvoice : StoreObject
	{
		private const string JOBINVOICE_TABLE = "Job_Invoice";

		private const string JOBINVOICEDETAIL_TABLE = "Job_Invoice_Detail";

		public const string JOBINVOICESODETAIL_TABLE = "Job_Invoice_SO_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string JOBID_PARM = "@JobID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string TERMID_PARM = "@TermID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string DISCOUNTFC_PARM = "@DiscountFC";

		private const string RETENTIONAMOUNT_PARM = "@RetentionAmount";

		private const string RETENTIONAMOUNTFC_PARM = "@RetentionAmountFC";

		private const string RETENTIONPERCENT_PARM = "@RetentionPercent";

		private const string ADVANCEAMOUNT_PARM = "@AdvanceAmount";

		private const string ADVANCEAMOUNTFC_PARM = "@AdvanceAmountFC";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string SUBTOTAL_PARM = "@SubTotal";

		private const string TOTAL_PARM = "@Total";

		private const string TOTALFC_PARM = "@TotalFC";

		private const string DUEDATE_PARM = "@DueDate";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string DESCRIPTION_PARM = "@Description";

		private const string FEEID_PARM = "@FeeID";

		private const string ITEMTYPE_PARM = "@ItemType";

		private const string QUANTITY_PARM = "@Quantity";

		private const string ORDERROWINDEX_PARM = "@OrderRowIndex";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string COST_PARM = "@Cost";

		private const string COSTFC_PARM = "@CostFC";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

		private const string REMARKS_PARM = "@Remarks";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		public JobInvoice(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateJobInvoiceText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Invoice", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("JobID", "@JobID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("DueDate", "@DueDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("RetentionAmount", "@RetentionAmount"), new FieldValue("RetentionAmountFC", "@RetentionAmountFC"), new FieldValue("RetentionPercent", "@RetentionPercent"), new FieldValue("AdvanceAmount", "@AdvanceAmount"), new FieldValue("AdvanceAmountFC", "@AdvanceAmountFC"), new FieldValue("SubTotal", "@SubTotal"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Invoice", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobInvoiceCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobInvoiceText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobInvoiceText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@DiscountFC", SqlDbType.Decimal);
			parameters.Add("@RetentionAmount", SqlDbType.Decimal);
			parameters.Add("@RetentionAmountFC", SqlDbType.Decimal);
			parameters.Add("@RetentionPercent", SqlDbType.Decimal);
			parameters.Add("@AdvanceAmount", SqlDbType.Decimal);
			parameters.Add("@AdvanceAmountFC", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters.Add("@SubTotal", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@TotalFC", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@DiscountFC"].SourceColumn = "DiscountFC";
			parameters["@RetentionAmount"].SourceColumn = "RetentionAmount";
			parameters["@RetentionAmountFC"].SourceColumn = "RetentionAmountFC";
			parameters["@RetentionPercent"].SourceColumn = "RetentionPercent";
			parameters["@AdvanceAmount"].SourceColumn = "AdvanceAmount";
			parameters["@AdvanceAmountFC"].SourceColumn = "AdvanceAmountFC";
			parameters["@SubTotal"].SourceColumn = "SubTotal";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@TotalFC"].SourceColumn = "TotalFC";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
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

		private string GetInsertUpdateJobInvoiceDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Invoice_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ItemType", "@ItemType"), new FieldValue("FeeID", "@FeeID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("Cost", "@Cost"), new FieldValue("CostFC", "@CostFC"), new FieldValue("Quantity", "@Quantity"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("Remarks", "@Remarks"), new FieldValue("Description", "@Description"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobInvoiceDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobInvoiceDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobInvoiceDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ItemType", SqlDbType.TinyInt);
			parameters.Add("@FeeID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Cost", SqlDbType.Decimal);
			parameters.Add("@CostFC", SqlDbType.Decimal);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@AmountFC", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ItemType"].SourceColumn = "ItemType";
			parameters["@FeeID"].SourceColumn = "FeeID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@CostFC"].SourceColumn = "CostFC";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@Quantity"].SourceColumn = "Quantity";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateJobInvoiceSODetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Invoice_SO_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("FeeID", "@FeeID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Cost", "@Cost"), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("LocationID", "@LocationID"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Discount", "@Discount"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobInvoiceSODetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobInvoiceSODetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobInvoiceSODetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@FeeID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Cost", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@FeeID"].SourceColumn = "FeeID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@Discount"].SourceColumn = "Discount";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(JobInvoiceData journalData)
		{
			return true;
		}

		public bool RecostInvoice(string productID, string locationID, decimal purchaseQuantity, decimal shortQuantity, decimal oldAvgCost, decimal newAvgCost, SqlTransaction sqlTransaction)
		{
			return true;
		}

		public bool InsertUpdateJobInvoice(JobInvoiceData jobInvoiceData, bool isUpdate)
		{
			return InsertUpdateJobInvoice(jobInvoiceData, isUpdate, null);
		}

		public bool InsertUpdateJobInvoice(JobInvoiceData jobInvoiceData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			new ServerTestTimer().Start();
			string text = "";
			bool flag = true;
			SqlCommand sqlCommand = null;
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = jobInvoiceData.JobInvoiceTable.Rows[0];
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				string text2 = dataRow["VoucherID"].ToString();
				string text3 = dataRow["SysDocID"].ToString();
				string text4 = dataRow["JobID"].ToString();
				if (false)
				{
					new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString())?.ToString();
				}
				else
				{
					new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString())?.ToString();
				}
				decimal num = default(decimal);
				foreach (DataRow row in jobInvoiceData.JobInvoiceDetailTable.Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row["Amount"].ToString(), out result);
					num += result;
				}
				dataRow["Total"] = num;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Job_Invoice", "VoucherID", dataRow["SysDocID"].ToString(), text2, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				bool flag2 = false;
				decimal result2 = 1m;
				string a = "M";
				if (dataRow["CurrencyID"] != DBNull.Value && baseCurrencyID != dataRow["CurrencyID"].ToString())
				{
					flag2 = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result2);
					a = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
				}
				if (flag2)
				{
					decimal result3 = default(decimal);
					dataRow["TotalFC"] = dataRow["Total"];
					decimal.TryParse(dataRow["Total"].ToString(), out result3);
					result3 = ((!(a == "M")) ? Math.Round(result3 / result2, 4) : Math.Round(result3 * result2, 4));
					dataRow["Total"] = result3;
					result3 = default(decimal);
					dataRow["DiscountFC"] = dataRow["Discount"];
					decimal.TryParse(dataRow["DiscountFC"].ToString(), out result3);
					result3 = ((!(a == "M")) ? Math.Round(result3 / result2, 4) : Math.Round(result3 * result2, 4));
					dataRow["Discount"] = result3;
					result3 = default(decimal);
					dataRow["RetentionAmountFC"] = dataRow["RetentionAmount"];
					decimal.TryParse(dataRow["RetentionAmountFC"].ToString(), out result3);
					result3 = ((!(a == "M")) ? Math.Round(result3 / result2, 4) : Math.Round(result3 * result2, 4));
					dataRow["RetentionAmount"] = result3;
					result3 = default(decimal);
					dataRow["TaxAmountFC"] = dataRow["TaxAmount"];
					decimal.TryParse(dataRow["TaxAmount"].ToString(), out result3);
					result3 = ((!(a == "M")) ? Math.Round(result3 / result2, 4) : Math.Round(result3 * result2, 4));
					dataRow["TaxAmount"] = result3;
				}
				foreach (DataRow row2 in jobInvoiceData.JobInvoiceDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					if (flag2)
					{
						decimal result4 = default(decimal);
						row2["AmountFC"] = row2["Amount"];
						decimal.TryParse(row2["Amount"].ToString(), out result4);
						result4 = ((!(a == "M")) ? Math.Round(result4 / result2, currencyDecimalPoints) : Math.Round(result4 * result2, currencyDecimalPoints));
						row2["Amount"] = result4;
					}
					else
					{
						row2["AmountFC"] = DBNull.Value;
					}
				}
				foreach (DataRow row3 in jobInvoiceData.JobInvoiceSODetailTable.Rows)
				{
					row3["SysDocID"] = dataRow["SysDocID"];
					row3["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteJobInvoiceDetailsRows(text3, text2, sqlTransaction);
				}
				if (isUpdate)
				{
					flag &= DeleteJobInvoiceSODetailsRows(text3, text2, sqlTransaction);
				}
				foreach (DataRow row4 in jobInvoiceData.JobInvoiceDetailTable.Rows)
				{
					string text5 = row4["SourceSysDocID"].ToString();
					string text6 = row4["SourceVoucherID"].ToString();
					string text7 = row4["SourceRowIndex"].ToString();
					if (int.Parse(row4["ItemType"].ToString()) == 5)
					{
						text = "UPDATE Journal_Details SET IsBilled='True' WHERE SysDocID = '" + text5 + "' AND VoucherID = '" + text6 + "' AND JournalDetailID = '" + text7 + "' AND JobID = '" + text4 + "'";
						flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
					}
				}
				sqlCommand = GetInsertUpdateJobInvoiceCommand(isUpdate);
				sqlCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(jobInvoiceData, "Job_Invoice", sqlCommand)) : (flag & Insert(jobInvoiceData, "Job_Invoice", sqlCommand)));
				if (jobInvoiceData.Tables["Job_Invoice_Detail"].Rows.Count > 0)
				{
					sqlCommand = GetInsertUpdateJobInvoiceDetailsCommand(isUpdate: false);
					sqlCommand.Transaction = sqlTransaction;
					flag &= Insert(jobInvoiceData, "Job_Invoice_Detail", sqlCommand);
				}
				if (jobInvoiceData.Tables["Job_Invoice_SO_Detail"].Rows.Count > 0)
				{
					sqlCommand = GetInsertUpdateJobInvoiceSODetailsCommand(isUpdate: false);
					sqlCommand.Transaction = sqlTransaction;
					flag &= Insert(jobInvoiceData, "Job_Invoice_SO_Detail", sqlCommand);
				}
				text = "UPDATE Job SET RetentionAmount = (SELECT SUM(ISNULL(RetentionAmountFC,ISNULL(RetentionAmount,0))) AS RetentionAmount FROM Job_Invoice WHERE JobID = '" + text4 + "'),\r\n                       RetentionPaid = ( SELECT SUM(ISNULL(AmountFC,ISNULL(Amount,0))) AS Amount FROM Job_Invoice_Detail JID2 INNER JOIN Job_Invoice JI2 ON JID2.SysDocID = JI2.SysDocID AND JID2.VoucherID = JI2.VoucherID WHERE JI2.JobID = '" + text4 + "' AND ItemType = 3) \r\n                        WHERE JobID = '" + text4 + "' ";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				text = "UPDATE Job SET AdvanceApplied = (SELECT SUM(ISNULL(AdvanceAmountFC,ISNULL(AdvanceAmount,0))) AS AdvanceAmount FROM Job_Invoice WHERE JobID = '" + text4 + "'),\r\n                       AdvanceBilled = ( SELECT SUM(ISNULL(AmountFC,ISNULL(Amount,0))) AS Amount FROM Job_Invoice_Detail JID2 INNER JOIN Job_Invoice JI2 ON JID2.SysDocID = JI2.SysDocID AND JID2.VoucherID = JI2.VoucherID WHERE JI2.JobID = '" + text4 + "' AND ItemType = 4) \r\n                        WHERE JobID = '" + text4 + "' ";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				if (jobInvoiceData.Tables.Contains("Tax_Detail"))
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(jobInvoiceData, text3, text2, isUpdate, sqlTransaction);
				}
				GLData journalData = CreateInvoiceGLData(jobInvoiceData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				text = "UPDATE Job_Invoice SET TotalCOGS=(SELECT SUM(COGS) FROM Job_Invoice_Detail WHERE SysDocID='" + text3 + "' AND VoucherID='" + text2 + "')\r\n                             WHERE SysDocID='" + text3 + "' AND VoucherID='" + text2 + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Job_Invoice", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Job Invoice";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text2, text3, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text2, text3, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Job_Invoice", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.JobInvoice, text3, text2, "Job_Invoice", sqlTransaction);
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

		private GLData CreateInvoiceGLData(JobInvoiceData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.JobInvoiceTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string text = dataRow["CustomerID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				dataRow["VoucherID"].ToString();
				string text3 = dataRow["JobID"].ToString();
				string value = dataRow["CompanyID"].ToString();
				string value2 = dataRow["DivisionID"].ToString();
				string textCommand = "SELECT SD.LocationID, ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID,ISNULL(Job.IncomeAccountID,LOC.ProjectIncomeAccountID) AS IncomeAccountID,LOC.DiscountGivenAccountID,\r\n                                ISNULL(Job.WIPAccountID,LOC.ProjectWIPAccountID) AS WIPAccountID, LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID,ISNULL(Job.RetentionAccountID,LOC.ProjectRetentionAccountID) AS RetentionAccountID,\r\n                                ISNULL(Job.AdvanceAccountID,LOC.ProjectAdvanceAccountID) AS AdvanceAccountID ,\r\n                                ISNULL(Job.CostAccountID,LOC.ProjectCostAccountID) AS CostAccountID \r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                 LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Job ON Job.JobID = '" + text3 + "'\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				string text4 = dataRow2["LocationID"].ToString();
				string value3 = dataRow2["DiscountGivenAccountID"].ToString();
				dataRow2["SalesTaxAccountID"].ToString();
				string value4 = dataRow2["ARAccountID"].ToString();
				string a = dataRow2["BaseCurrencyID"].ToString();
				bool flag = false;
				decimal result = 1m;
				if (dataRow["CurrencyID"] != DBNull.Value && a != dataRow["CurrencyID"].ToString())
				{
					flag = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result);
				}
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.JobInvoice;
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["CurrencyID"] = dataRow["CurrencyID"];
				dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Project Invoice - ";
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				decimal num = default(decimal);
				new Hashtable();
				new ArrayList();
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				decimal d = default(decimal);
				textCommand = "SELECT CASE WHEN  P.IncomeAccountID IS NULL  THEN  Loc.ProjectIncomeAccountID ELSE P.IncomeAccountID END AS IncomeAccountID, \r\n\t                                CASE WHEN  P.WIPAccountID IS NULL  THEN  Loc.ProjectWIPAccountID ELSE P.WIPAccountID END AS WIPAccountID,\r\n                                    CASE WHEN  P.RetentionAccountID IS NULL  THEN  Loc.ProjectRetentionAccountID ELSE P.RetentionAccountID END AS RetentionAccountID,\r\n                                    CASE WHEN  P.CostAccountID IS NULL  THEN  Loc.ProjectCostAccountID ELSE P.CostAccountID END AS CostAccountID,\r\n                                    CASE WHEN  P.AdvanceAccountID IS NULL  THEN  Loc.ProjectAdvanceAccountID ELSE P.AdvanceAccountID END AS AdvanceAccountID\r\n                                    FROM Job P\r\n                                    LEFT OUTER JOIN Location LOC ON LOC.LocationID = '" + text4 + "'\r\n                                    WHERE JobID = '" + text3 + "'";
				string text5 = dataRow2["IncomeAccountID"].ToString();
				string text6 = dataRow2["CostAccountID"].ToString();
				dataRow2["WIPAccountID"].ToString();
				string text7 = dataRow2["RetentionAccountID"].ToString();
				string text8 = dataRow2["AdvanceAccountID"].ToString();
				if (text5 == "")
				{
					throw new CompanyException("Project income account is not found for the project or location.");
				}
				DataRow dataRow5;
				foreach (DataRow row in transactionData.JobInvoiceDetailTable.Rows)
				{
					int.Parse(row["RowIndex"].ToString());
					decimal result2 = default(decimal);
					int num2 = int.Parse(row["ItemType"].ToString());
					if (flag)
					{
						decimal.TryParse(row["AmountFC"].ToString(), out result2);
					}
					else
					{
						decimal.TryParse(row["Amount"].ToString(), out result2);
					}
					string text9;
					switch (num2)
					{
					case 3:
						text9 = text7;
						break;
					case 4:
						text9 = text8;
						break;
					default:
						text9 = text5;
						break;
					}
					if (num2 == 5)
					{
						dataRow5 = gLData.JournalDetailsTable.NewRow();
						dataRow5.BeginEdit();
						dataRow5["JournalID"] = 0;
						dataRow5["AccountID"] = row["FeeID"].ToString();
						dataRow5["PayeeID"] = text;
						dataRow5["PayeeType"] = "A";
						dataRow5["JobID"] = text3;
						dataRow5["Debit"] = DBNull.Value;
						dataRow5["Credit"] = row["Cost"].ToString();
						dataRow5["Reference"] = dataRow["Reference"];
						dataRow5["CompanyID"] = value;
						dataRow5["DivisionID"] = value2;
						dataRow5.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow5);
						dataRow5 = gLData.JournalDetailsTable.NewRow();
						dataRow5.BeginEdit();
						dataRow5["JournalID"] = 0;
						if (text6 == "")
						{
							throw new CompanyException("Project Cost account is not selected for the project or location.");
						}
						dataRow5["AccountID"] = text6;
						dataRow5["PayeeID"] = text;
						dataRow5["PayeeType"] = "A";
						dataRow5["JobID"] = text3;
						dataRow5["Debit"] = row["Cost"].ToString();
						dataRow5["Credit"] = DBNull.Value;
						dataRow5["Reference"] = dataRow["Reference"];
						dataRow5["CompanyID"] = value;
						dataRow5["DivisionID"] = value2;
						dataRow5.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow5);
					}
					if (hashtable.ContainsKey(text9))
					{
						num = decimal.Parse(hashtable[text9].ToString());
						num += Math.Round(result2, currencyDecimalPoints);
						hashtable[text9] = num;
					}
					else
					{
						num = Math.Round(result2, currencyDecimalPoints);
						hashtable.Add(text9, Math.Round(num, currencyDecimalPoints));
						arrayList.Add(text9);
					}
					d += Math.Round(result2, currencyDecimalPoints);
				}
				for (int i = 0; i < hashtable.Count; i++)
				{
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					string text9 = arrayList[i].ToString();
					num = decimal.Parse(hashtable[text9].ToString());
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = text9;
					dataRow5["PayeeID"] = text;
					dataRow5["JobID"] = text3;
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
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				decimal result3 = default(decimal);
				decimal result4 = default(decimal);
				decimal result5 = default(decimal);
				decimal result6 = default(decimal);
				if (dataRow["DiscountFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["DiscountFC"].ToString(), out result3);
				}
				else
				{
					decimal.TryParse(dataRow["Discount"].ToString(), out result3);
				}
				if (dataRow["TaxAmountFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["TaxAmountFC"].ToString(), out result4);
				}
				else
				{
					decimal.TryParse(dataRow["TaxAmount"].ToString(), out result4);
				}
				if (dataRow["RetentionAmountFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["RetentionAmountFC"].ToString(), out result5);
				}
				else
				{
					decimal.TryParse(dataRow["RetentionAmount"].ToString(), out result5);
				}
				if (dataRow["AdvanceAmountFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["AdvanceAmountFC"].ToString(), out result6);
				}
				else
				{
					decimal.TryParse(dataRow["AdvanceAmount"].ToString(), out result6);
				}
				if (result3 > 0m)
				{
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = value3;
					dataRow5["PayeeID"] = text;
					dataRow5["PayeeType"] = "A";
					dataRow5["JobID"] = text3;
					if (flag)
					{
						dataRow5["DebitFC"] = result3;
						dataRow5["CreditFC"] = DBNull.Value;
					}
					else
					{
						dataRow5["Debit"] = result3;
						dataRow5["Credit"] = DBNull.Value;
					}
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				if (result4 > 0m)
				{
					if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
					{
						throw new CompanyException("Tax details not found for the transaction.");
					}
					DataRow[] array = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
					object obj = null;
					decimal num3 = default(decimal);
					for (int j = 0; j < array.Length; j++)
					{
						num3 = default(decimal);
						DataRow obj2 = array[j];
						dataRow5 = gLData.JournalDetailsTable.NewRow();
						dataRow5.BeginEdit();
						dataRow5["JournalID"] = 0;
						string text10 = "";
						text10 = obj2["TaxItemID"].ToString();
						string text11 = "";
						textCommand = "SELECT SalesTaxAccountID FROM Tax WHERE  TaxCode = '" + text10.Trim() + "'";
						obj = ExecuteScalar(textCommand);
						if (obj != null)
						{
							text11 = obj.ToString();
						}
						if (text11 == "")
						{
							throw new CompanyException("AccountID is not set for tax item: " + text10 + ".");
						}
						decimal.TryParse(obj2["TaxAmount"].ToString(), out num3);
						dataRow5["AccountID"] = text11;
						dataRow5["PayeeID"] = text;
						dataRow5["PayeeType"] = "A";
						dataRow5["JobID"] = text3;
						if (flag)
						{
							dataRow5["DebitFC"] = DBNull.Value;
							dataRow5["CreditFC"] = Math.Round(num3, currencyDecimalPoints, MidpointRounding.AwayFromZero);
						}
						else
						{
							dataRow5["Debit"] = DBNull.Value;
							dataRow5["Credit"] = Math.Round(num3, currencyDecimalPoints, MidpointRounding.AwayFromZero);
						}
						dataRow5["Reference"] = dataRow["Reference"];
						dataRow5["CompanyID"] = value;
						dataRow5["DivisionID"] = value2;
						dataRow5.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow5);
					}
				}
				if (result5 > 0m)
				{
					if (text7 == "")
					{
						throw new CompanyException("Retention account is not set for the project or location.");
					}
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = text7;
					dataRow5["PayeeID"] = text;
					dataRow5["PayeeType"] = "A";
					dataRow5["JobID"] = text3;
					if (flag)
					{
						dataRow5["DebitFC"] = result5;
						dataRow5["CreditFC"] = DBNull.Value;
					}
					else
					{
						dataRow5["Debit"] = result5;
						dataRow5["Credit"] = DBNull.Value;
					}
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				if (result6 > 0m)
				{
					if (text8 == "")
					{
						throw new CompanyException("Advance account is not set for the project or location.");
					}
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = text8;
					dataRow5["PayeeID"] = text;
					dataRow5["PayeeType"] = "A";
					dataRow5["JobID"] = text3;
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
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["DueDate"] = dataRow["DueDate"];
					dataRow5["AccountID"] = value4;
					dataRow5["PayeeID"] = text;
					dataRow5["PayeeType"] = "C";
					dataRow5["IsARAP"] = true;
					dataRow5["JobID"] = text3;
					if (flag)
					{
						dataRow5["DebitFC"] = DBNull.Value;
						dataRow5["CreditFC"] = result6;
					}
					else
					{
						dataRow5["Debit"] = DBNull.Value;
						dataRow5["Credit"] = result6;
					}
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["Description"] = "Adjustment of Advance Payment";
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				dataRow5 = gLData.JournalDetailsTable.NewRow();
				dataRow5.BeginEdit();
				dataRow5["JournalID"] = 0;
				dataRow5["DueDate"] = dataRow["DueDate"];
				dataRow5["AccountID"] = value4;
				dataRow5["PayeeID"] = text;
				dataRow5["PayeeType"] = "C";
				dataRow5["IsARAP"] = true;
				dataRow5["JobID"] = text3;
				if (flag)
				{
					dataRow5["DebitFC"] = d - result3 - result5 + result4;
					dataRow5["CreditFC"] = DBNull.Value;
				}
				else
				{
					dataRow5["Debit"] = d - result3 - result5 + result4;
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

		public JobInvoiceData GetJobInvoiceByID(string sysDocID, string voucherID)
		{
			return GetJobInvoiceByID(sysDocID, voucherID, null);
		}

		internal JobInvoiceData GetJobInvoiceByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				JobInvoiceData jobInvoiceData = new JobInvoiceData();
				string text = "SELECT * FROM Job_Invoice WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				new SqlCommand(text).Transaction = sqlTransaction;
				FillDataSet(jobInvoiceData, "Job_Invoice", text, sqlTransaction);
				if (jobInvoiceData == null || jobInvoiceData.Tables.Count == 0 || jobInvoiceData.Tables["Job_Invoice"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT TD.*\r\n                        FROM Job_Invoice_Detail TD\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobInvoiceData, "Job_Invoice_Detail", text, sqlTransaction);
				text = "SELECT TD.*\r\n                        FROM Job_Invoice_SO_Detail TD\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobInvoiceData, "Job_Invoice_SO_Detail", text, sqlTransaction);
				text = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobInvoiceData, "Tax_Detail", text, sqlTransaction);
				return jobInvoiceData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobInvoiceDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				JobInvoiceData jobInvoiceData = new JobInvoiceData();
				string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid,SO.JobID FROM Job_Invoice_Detail SOD INNER JOIN Job_Invoice SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(jobInvoiceData, "Job_Invoice_Detail", textCommand, sqlTransaction);
				if (jobInvoiceData.JobInvoiceDetailTable.Rows.Count == 0)
				{
					return true;
				}
				string text = "";
				text = ((0 == 0) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				if (text != "")
				{
					int.Parse(text.ToString());
				}
				bool result = false;
				bool.TryParse(jobInvoiceData.JobInvoiceDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				string exp = "UPDATE Journal_Details SET IsBilled = 'False' WHERE JournalDetailID In (SELECT SourceRowIndex FROM Job_Invoice_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "')";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				textCommand = "DELETE FROM Job_Invoice_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				return flag & new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobInvoiceSODetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				JobInvoiceData jobInvoiceData = new JobInvoiceData();
				string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid FROM Job_Invoice_SO_Detail SOD INNER JOIN Job_Invoice SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(jobInvoiceData, "Job_Invoice_SO_Detail", textCommand, sqlTransaction);
				if (jobInvoiceData.JobInvoiceSODetailTable.Rows.Count == 0)
				{
					return true;
				}
				string text = "";
				text = ((0 == 0) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				if (text != "")
				{
					int.Parse(text.ToString());
				}
				bool result = false;
				bool.TryParse(jobInvoiceData.JobInvoiceSODetailTable.Rows[0]["IsVoid"].ToString(), out result);
				textCommand = "DELETE FROM Job_Invoice_SO_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				return flag & new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidJobInvoice(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidJobInvoice(sysDocID, voucherID, isVoid, null);
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

		private bool VoidJobInvoice(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				string text = "";
				text = ((0 == 0) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text != "")
				{
					salesFlows = (SalesFlows)int.Parse(text.ToString());
				}
				JobInvoiceData jobInvoiceData = new JobInvoiceData();
				string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid FROM Job_Invoice_Detail SOD INNER JOIN Job_Invoice SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(jobInvoiceData, "Job_Invoice_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(jobInvoiceData.JobInvoiceDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (result == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				if (salesFlows != SalesFlows.SOThenDNThenInvoice)
				{
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(74, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				textCommand = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				textCommand = "UPDATE Job_Invoice SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Job Invoice", voucherID, sysDocID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateInvoiceCOGS(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				bool flag = true;
				string textCommand = "SELECT * FROM JOB_INVOICE_DETAIL WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "JOB_INVOICE_DETAIL", textCommand, sqlTransaction);
				foreach (DataRow row in dataSet.Tables["JOB_INVOICE_DETAIL"].Rows)
				{
					string text = row["ProductID"].ToString();
					string locationID = "";
					int rowIndex = int.Parse(row["RowIndex"].ToString());
					decimal num = decimal.Parse(row["Quantity"].ToString());
					decimal productCOGS = new Products(base.DBConfig).GetProductCOGS(text, locationID, voucherID, sysDocID, rowIndex, num, sqlTransaction);
					decimal num2 = default(decimal);
					if (num != 0m)
					{
						num2 = Math.Round(productCOGS / num, 5);
					}
					textCommand = "UPDATE Inventory_Transactions SET AverageCost=" + num2 + "\r\n                               ,AssetValue= " + -1m * Math.Abs(productCOGS) + " WHERE \r\n                              SysDocID='" + row["SysDocID"].ToString() + "' AND VoucherID='" + row["VoucherID"].ToString() + "' AND ProductID='" + text + "'";
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
					textCommand = " Select COGS - (-1 *ISNULL((SELECT AssetValue FROM Inventory_Transactions WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' AND ProductID = '" + text + "' AND RowIndex = " + rowIndex.ToString() + "),0))\r\n                                    FROM Job_Invoice_Detail\r\n                                    WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' AND ProductID = '" + text + "' AND RowIndex = " + rowIndex.ToString();
					object obj = null;
					obj = ExecuteScalar(textCommand, sqlTransaction);
					if (obj != null && obj.ToString() != "")
					{
						decimal.Parse(obj.ToString());
					}
					textCommand = " UPDATE Job_Invoice_Detail SET COGS = ABS(ISNULL((SELECT AssetValue FROM Inventory_Transactions WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' AND ProductID = '" + text + "' AND RowIndex = " + rowIndex.ToString() + "),0))\r\n                                    WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' AND ProductID = '" + text + "' AND RowIndex = " + rowIndex.ToString();
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteJobInvoice(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Job_Invoice", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidJobInvoice(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeleteJobInvoiceDetailsRows(sysDocID, voucherID, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				JobInvoiceData jobInvoiceData = new JobInvoiceData();
				string textCommand = "SELECT SO.JobID FROM  Job_Invoice SO \r\n                              WHERE SO.SysDocID = '" + sysDocID + "' AND SO.VoucherID = '" + voucherID + "'";
				FillDataSet(jobInvoiceData, "Job_Invoice_Detail", textCommand, sqlTransaction);
				text = "DELETE FROM Job_Invoice WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (jobInvoiceData.JobInvoiceDetailTable.Rows.Count > 0)
				{
					string text2 = jobInvoiceData.JobInvoiceDetailTable.Rows[0]["JobID"].ToString();
					text = "UPDATE Job SET AdvanceApplied =(SELECT SUM(AdvanceAmount) FROM Job_Invoice JI \r\n                        WHERE JI.JobID='" + text2 + "') WHERE JobID='" + text2 + "'";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				}
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Job Invoice", voucherID, sysDocID, activityType, sqlTransaction);
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
				string exp = "SELECT COUNT(RowIndex)FROM Job_Invoice_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                                AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Job_Invoice_Detail SOD2 WHERE SOD.SysDocID=SOD2.SysDocID AND SOD.VoucherID=SOD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityShipped,0) ) FROM Job_Invoice_Detail SOD3 WHERE SOD.SysDocID=SOD3.SysDocID AND SOD.VoucherID=SOD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Job_Invoice SET IsDelivered = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetInvoicesForDelivery(string customerID, bool isExport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer] FROM Job_Invoice SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID  WHERE ISNULL(IsDelivered,0)=0 AND ISNULL(IsExport,'False')= '" + isExport.ToString() + "' ";
				if (customerID != "")
				{
					text = text + " AND SO.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "Job_Invoice", text);
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

		public DataSet GetJobInvoiceToPrint(string sysDocID, string voucherID, bool mergeMatrixItems)
		{
			return GetJobInvoiceToPrint(sysDocID, new string[1]
			{
				voucherID
			}, mergeMatrixItems);
		}

		public DataSet GetJobInvoiceToPrint(string sysDocID, string voucherID)
		{
			return GetJobInvoiceToPrint(sysDocID, new string[1]
			{
				voucherID
			}, mergeMatrixItems: false);
		}

		public DataSet GetJobInvoiceToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems)
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
				string cmdText = "SELECT DISTINCT JI.*,PT.TermName,C.CustomerName,S.FullName,J.JobName,ISNULL(ISNULL(JI.TaxAmountFC,JI.TaxAmount) ,0) AS Tax,( SELECT SUM(JFD.Amount) FROM Job_Fee_Detail JFD WHERE JFD.JobID=JI.JobID ) AS [Fee Value],\r\n                                (SELECT SUM(Ji1.Total) FROM Job_Invoice Ji1 WHERE Ji1.JobID=JI.JobID AND Ji1.TransactionDate <= JI.TransactionDate ) AS [Total Invoice],\r\n                                (SELECT SUM(Ji1.Total) FROM Job_Invoice Ji1 WHERE Ji1.JobID=JI.JobID AND Ji1.TransactionDate < JI.TransactionDate AND  Ji1.VoucherID NOT IN (" + text + ") ) AS [Previous Invoice] ,C.TaxIDNumber as CTaxIDNo ,\r\n                                CA.ContactName,CA.ContactTitle,CA.Email,CA.Mobile,CA.Phone1,CA.Phone2,CA.Fax,CA.PostalCode, CA.AddressPrintFormat, CA.Address1+' '+CA.Address2+' '+CA.Address3 AS Address,j.SiteLocationAddress, j.SiteLocationID,J.Note,\r\n                                 C.BankName,C.BankBranch,C.BankAccountNumber,j.PONumber,CA.ContactTitle, CA.Department, CA.Comment\r\n                                FROM Job_Invoice JI LEFT JOIN Customer C ON JI.CustomerID=C.CustomerID\r\n                                LEFT JOIN Salesperson S ON JI.SalespersonID=S.SalespersonID    \r\n                                LEFT JOIN Job J ON J.JobID=JI.JobID\r\n                                LEFT JOIN Payment_Term PT ON PT.PaymentTermID=JI.TermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.CustomerID = C.CustomerID\r\n                                WHERE SysDocID =  '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Job_Invoice", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Job_Invoice"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = ((!mergeMatrixItems) ? ("\tSELECT TD.*  FROM Job_Invoice_Detail TD\r\n                                WHERE TD.SysDocID='" + sysDocID + "' AND TD.VoucherID IN (" + text + ")  ORDER BY RowIndex") : ("SELECT     SysDocID,VoucherID, ISNULL(PPC.ProductParentID,SINVD.ProductID) AS ProductID,ISNULL(PP.Description,SINVD.Description) AS Description,SUM(ISNULL(UnitQuantity,SINVD.Quantity)) AS Quantity,\r\n                            ISNULL(UnitPriceFC,UnitPrice) AS UnitPrice,\r\n                            SUM(ISNULL(UnitQuantity,SINVD.Quantity)*ISNULL(UnitPriceFC,UnitPrice)) AS Total,SINVD.UnitID,LocationID\r\n                            FROM   Job_Invoice_Detail SINVD\r\n\t\t\t\t\t\t    LEFT OUTER JOIN Product_Parent_Components PPC ON SINVD.ProductID=PPC.ProductID\r\n\t\t\t\t\t\t    LEFT OUTER JOIN Product_Parent PP ON PP.ProductParentID = PPC.ProductParentID\r\n                            WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")\r\n\t\t\t\t\t\t    GROUP BY SysDocID,RowIndex,VoucherID,PPC.ProductParentID,ISNULL(UnitPriceFC,SINVD.UnitPrice),ISNULL(PPC.ProductParentID,SINVD.ProductID),ISNULL(PP.Description,SINVD.Description),SINVD.UnitID,LocationID \r\n                            ORDER BY RowIndex"));
				FillDataSet(dataSet, "Job_Invoice_Detail", cmdText);
				DataRow dataRow = dataSet.Tables["Job_Invoice"].Rows[0];
				string text2 = dataRow["JobID"].ToString();
				string text3 = StoreConfiguration.ToSqlDateTimeString(DateTime.Parse(dataRow["TransactionDate"].ToString()));
				cmdText = "SELECT TD.*,JI.Note,JI.TransactionDate,ARP.PaymentAmount [AllocatedAmount]  FROM Job_Invoice_Detail TD LEFT JOIN Job_Invoice JI ON TD.SysDocID=JI.SysDocID ANd TD.VoucherID=JI.VoucherID\r\n\t                    LEFT JOIN AR_Payment_Allocation ARP  ON TD.SysDocID=ARP.InvoiceSysDocID ANd TD.VoucherID=ARP.InvoiceVoucherID\r\n                        WHERE  JI.TransactionDate  < '" + text3 + "' AND JI.JobID='" + text2 + "' AND Amount<>0  ORDER BY VoucherID";
				FillDataSet(dataSet, "Job_PreviousInvoice", cmdText);
				dataSet.Relations.Add("CustomerInvoice", new DataColumn[2]
				{
					dataSet.Tables["Job_Invoice"].Columns["SysDocID"],
					dataSet.Tables["Job_Invoice"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Job_Invoice_Detail"].Columns["SysDocID"],
					dataSet.Tables["Job_Invoice_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Job_Invoice"].Columns.Add("TotalInWords", typeof(string));
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				foreach (DataRow row in dataSet.Tables["Job_Invoice"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Discount"].ToString(), out result2);
					decimal.TryParse(row["Tax"].ToString(), out result3);
					row["TotalInWords"] = NumToWord.GetNumInWords(result - result2 + result3, currencyDecimalPoints);
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
			string text3 = "SELECT   ISNULL(IsVoid,'False') AS 'V', SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],CustomerAddress [Address],J.JobID,J.JobName,TransactionDate [Invoice Date], DueDate AS [Due Date],\r\n\t\t\t\t\t\t\tINV.TermID [Term],INV.Reference,\r\n                            INV.SalespersonID + '-' + SP.FullName AS [Salesperon],Total - ISNULL(Discount,0) AS [Amount], ISNULL((CASE INV.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' ELSE  'NON TAXABLE'  END) ,(CASE Customer.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' ELSE  'NON TAXABLE'  END))AS TAXOPTION,INV.TaxAmount\r\n                            FROM         Job_Invoice INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Salesperson SP ON SP.SalespersonID = INV.SalespersonID\r\n                            LEFT OUTER JOIN Job J ON J.JobID=INV.JobID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Job_Invoice", sqlCommand);
			return dataSet;
		}
	}
}
