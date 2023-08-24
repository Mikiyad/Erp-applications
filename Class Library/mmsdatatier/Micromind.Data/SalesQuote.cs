using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Micromind.Data
{
	public sealed class SalesQuote : StoreObject
	{
		private const string SALESQUOTE_TABLE = "Sales_Quote";

		private const string SALESQUOTEDETAIL_TABLE = "Sales_Quote_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string SALESFLOW_PARM = "@SalesFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string PRICEINCLUDETAX_PARM = "@PriceIncludeTax";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string SHIPTOADDRESS_PARM = "@ShipToAddress";

		private const string BILLINGADDRESSID_PARM = "@BillingAddressID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string DISCOUNT_PARM = "@Discount";

		private const string TOTAL_PARM = "@Total";

		private const string EXPIRYDATE_PARM = "@ExpiryDate";

		private const string JOBID_PARM = "@JobID";

		private const string ROUNDOFF_PARM = "@RoundOff";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		public const string ENTITYTYPE_PARM = "@EntityType";

		private const string LASTREVISEDDATE_PARM = "@LastRevisedDate";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string REMARKS1_PARM = "@Remarks1";

		private const string REMARKS2_PARM = "@Remarks2";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string DESCRIPTION_PARM = "@Remarks";

		private const string REMARKS_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string TAXPERCENTAGE_PARM = "@TaxPercentage";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string COST_PARM = "@Cost";

		private const string REFSLNO_PARM = "@RefSlNo";

		private const string REFTEXT1_PARM = "@RefText1";

		private const string REFTEXT2_PARM = "@RefText2";

		private const string REFNUM1_PARM = "@RefNum1";

		private const string REFNUM2_PARM = "@RefNum2";

		private const string REFDATE1_PARM = "@RefDate1";

		private const string REFDATE2_PARM = "@RefDate2";

		public SalesQuote(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSalesQuoteText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Quote", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("SalesFlow", "@SalesFlow"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("ShipToAddress", "@ShipToAddress"), new FieldValue("BillingAddressID", "@BillingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Discount", "@Discount"), new FieldValue("Total", "@Total"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("PONumber", "@PONumber"), new FieldValue("EntityType", "@EntityType"), new FieldValue("RoundOff", "@RoundOff"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("LastRevisedDate", "@LastRevisedDate"), new FieldValue("Remarks1", "@Remarks1"), new FieldValue("Remarks2", "@Remarks2"), new FieldValue("Note", "@Note"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Sales_Quote", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesQuoteCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesQuoteText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesQuoteText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@SalesFlow", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@ShipToAddress", SqlDbType.NVarChar);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@BillingAddressID", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@ExpiryDate", SqlDbType.DateTime);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@EntityType", SqlDbType.NVarChar);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@RoundOff", SqlDbType.Decimal);
			parameters.Add("@LastRevisedDate", SqlDbType.DateTime);
			parameters.Add("@Remarks1", SqlDbType.NVarChar);
			parameters.Add("@Remarks2", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@SalesFlow"].SourceColumn = "SalesFlow";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@PriceIncludeTax"].SourceColumn = "PriceIncludeTax";
			parameters["@ShipToAddress"].SourceColumn = "ShipToAddress";
			parameters["@BillingAddressID"].SourceColumn = "BillingAddressID";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@ExpiryDate"].SourceColumn = "ExpiryDate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@EntityType"].SourceColumn = "EntityType";
			parameters["@RoundOff"].SourceColumn = "RoundOff";
			parameters["@LastRevisedDate"].SourceColumn = "LastRevisedDate";
			parameters["@Remarks1"].SourceColumn = "Remarks1";
			parameters["@Remarks2"].SourceColumn = "Remarks2";
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

		private string GetInsertUpdateSalesQuoteDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Quote_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Remarks"), new FieldValue("Remarks", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("Cost", "@Cost"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("TaxPercentage", "@TaxPercentage"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("RefSlNo", "@RefSlNo"), new FieldValue("RefText1", "@RefText1"), new FieldValue("RefText2", "@RefText2"), new FieldValue("RefNum1", "@RefNum1"), new FieldValue("RefNum2", "@RefNum2"), new FieldValue("RefDate1", "@RefDate1"), new FieldValue("RefDate2", "@RefDate2"), new FieldValue("SubunitPrice", "@SubunitPrice"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesQuoteDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesQuoteDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesQuoteDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Cost", SqlDbType.Decimal);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@TaxPercentage", SqlDbType.Decimal);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@RefSlNo", SqlDbType.NVarChar);
			parameters.Add("@RefText1", SqlDbType.NVarChar);
			parameters.Add("@RefText2", SqlDbType.NVarChar);
			parameters.Add("@RefNum1", SqlDbType.Decimal);
			parameters.Add("@RefNum2", SqlDbType.Decimal);
			parameters.Add("@RefDate1", SqlDbType.DateTime);
			parameters.Add("@RefDate2", SqlDbType.DateTime);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Remarks"].SourceColumn = "Description";
			parameters["@Description"].SourceColumn = "Remarks";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxPercentage"].SourceColumn = "TaxPercentage";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
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

		private bool ValidateData(SalesQuoteData journalData)
		{
			return true;
		}

		public bool InsertUpdateSalesQuote(SalesQuoteData salesQuoteData, bool isUpdate, bool IsRevised)
		{
			bool flag = true;
			SqlCommand insertUpdateSalesQuoteCommand = GetInsertUpdateSalesQuoteCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = salesQuoteData.SalesQuoteTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				decimal num = default(decimal);
				foreach (DataRow row in salesQuoteData.SalesQuoteDetailTable.Rows)
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
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Sales_Quote", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row2 in salesQuoteData.SalesQuoteDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
				}
				if (IsRevised)
				{
					DateTime dateTime = DateTime.Now;
					if (salesQuoteData.Tables["Sales_Quote"].Rows[0]["PreviousRevisedDate"].ToString() != "")
					{
						dateTime = DateTime.Parse(salesQuoteData.Tables["Sales_Quote"].Rows[0]["PreviousRevisedDate"].ToString());
					}
					int nextRevsionNo = GetNextRevsionNo(text2, text);
					string text3 = "";
					text3 = "INSERT INTO Sales_Quote_History SELECT SysDocID,VoucherID," + nextRevsionNo + ",CustomerID,TransactionDate,EntityType,SalespersonID,SalesFlow,RequiredDate,ExpiryDate,ShippingAddressID,BillingAddressID,CustomerAddress,\r\n                ShipToAddress,Status,CurrencyID,TermID,ShippingMethodID,JobID,CostCategoryID,IsVoid,Reference,Discount,TaxOption,PayeeTaxGroupID,TaxAmount,Total,PONumber,Note,ApprovalStatus,\r\n                            '" + dateTime + "',DateCreated,DateUpdated,CreatedBy,UpdatedBy FROM Sales_Quote \r\n                                    WHERE VoucherID='" + text + "' AND SysDocID='" + text2 + "'";
					flag &= (ExecuteNonQuery(text3, sqlTransaction) >= 0);
					text3 = "INSERT INTO Sales_Quote_Detail_History SELECT SysDocID,VoucherID," + nextRevsionNo + ",ProductID,Quantity,UnitPrice,Description,UnitID,UnitQuantity,UnitFactor,FactorType,SubunitPrice,TaxOption,TaxGroupID,TaxAmount,RowIndex\r\n                             FROM Sales_Quote_Detail \r\n                                    WHERE VoucherID='" + text + "' AND SysDocID='" + text2 + "'";
					flag &= (ExecuteNonQuery(text3, sqlTransaction) >= 0);
				}
				insertUpdateSalesQuoteCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(salesQuoteData, "Sales_Quote", insertUpdateSalesQuoteCommand)) : (flag & Insert(salesQuoteData, "Sales_Quote", insertUpdateSalesQuoteCommand)));
				insertUpdateSalesQuoteCommand = GetInsertUpdateSalesQuoteDetailsCommand(isUpdate: false);
				insertUpdateSalesQuoteCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteSalesQuoteDetailsRows(text2, text, sqlTransaction);
				}
				if (salesQuoteData.Tables["Sales_Quote_Detail"].Rows.Count > 0)
				{
					flag &= Insert(salesQuoteData, "Sales_Quote_Detail", insertUpdateSalesQuoteCommand);
				}
				if (salesQuoteData.Tables.Contains("Tax_Detail") && salesQuoteData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(salesQuoteData, text2, text, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Sales_Quote", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Sales Quote";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Sales_Quote", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SalesQuote, text2, text, "Sales_Quote", sqlTransaction);
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

		public SalesQuoteData GetSalesQuoteByID(string sysDocID, string voucherID)
		{
			try
			{
				SalesQuoteData salesQuoteData = new SalesQuoteData();
				string textCommand = "SELECT * FROM Sales_Quote WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesQuoteData, "Sales_Quote", textCommand);
				if (salesQuoteData == null || salesQuoteData.Tables.Count == 0 || salesQuoteData.Tables["Sales_Quote"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID,Product.IsTrackLot\r\n                        FROM Sales_Quote_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        LEFT OUTER JOIN Product_Class PC ON PC.ClassID = Product.ClassID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(salesQuoteData, "Sales_Quote_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesQuoteData, "Tax_Detail", textCommand);
				textCommand = "SELECT DISTINCT SED.VoucherID,SED.SysDocID from Sales_Quote_Detail SQD INNER JOIN  Sales_Enquiry_Detail SED ON SED.SysDocID=SQD.SourceSysDocID AND SED.VoucherID=SQD.SourceVoucherID WHERE SQD.VoucherID='" + voucherID + "' AND SQD.SysDocID='" + sysDocID + "'";
				FillDataSet(salesQuoteData, "Sales_Enquiry_Detail", textCommand);
				textCommand = "SELECT DISTINCT SOD.VoucherID,SOD.SysDocID FROM Sales_Quote_Detail SQD INNER JOIN   Sales_Order_Detail SOD ON SQD.VoucherID=SOD.SourceVoucherID AND SQD.SysDocID=SOD.SourceSysDocID WHERE SQD.VoucherID='" + voucherID + "' AND SQD.SysDocID='" + sysDocID + "'";
				FillDataSet(salesQuoteData, "Sales_Order_Detail", textCommand);
				textCommand = "SELECT DISTINCT DND.VoucherID,DND.SysDocID FROM Sales_Quote_Detail SQD INNER JOIN   Delivery_Note_Detail DND ON SQD.VoucherID=DND.SourceVoucherID AND SQD.SysDocID=DND.SourceSysDocID  WHERE SQD.VoucherID='" + voucherID + "' AND SQD.SysDocID='" + sysDocID + "'";
				FillDataSet(salesQuoteData, "Delivery_Note_Detail", textCommand);
				textCommand = "SELECT isnull(MAX(JI.RevisionNo),0) RevisionNo\r\n                            FROM Sales_Quote_History JI  WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(salesQuoteData, "Revision", textCommand);
				return salesQuoteData;
			}
			catch
			{
				throw;
			}
		}

		public SalesQuoteData GetSalesQuoteRevByID(string sysDocID, string voucherID, int RevisedNo)
		{
			try
			{
				SalesQuoteData salesQuoteData = new SalesQuoteData();
				string textCommand = "SELECT * FROM Sales_Quote_History  WHERE RevisionNo=" + RevisedNo + " AND VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesQuoteData, "Sales_Quote", textCommand);
				if (salesQuoteData == null || salesQuoteData.Tables.Count == 0 || salesQuoteData.Tables["Sales_Quote"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID\r\n                        FROM Sales_Quote_Detail_History TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE RevisionNo=" + RevisedNo + " AND VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(salesQuoteData, "Sales_Quote_Detail", textCommand);
				textCommand = "SELECT DISTINCT SED.VoucherID,SED.SysDocID from Sales_Quote_Detail SQD INNER JOIN  Sales_Enquiry_Detail SED ON SED.SysDocID=SQD.SourceSysDocID AND SED.VoucherID=SQD.SourceVoucherID WHERE SQD.VoucherID='" + voucherID + "' AND SQD.SysDocID='" + sysDocID + "'";
				FillDataSet(salesQuoteData, "Sales_Enquiry_Detail", textCommand);
				textCommand = "SELECT DISTINCT SOD.VoucherID,SOD.SysDocID FROM Sales_Quote_Detail SQD INNER JOIN   Sales_Order_Detail SOD ON SQD.VoucherID=SOD.SourceVoucherID AND SQD.SysDocID=SOD.SourceSysDocID WHERE SQD.VoucherID='" + voucherID + "' AND SQD.SysDocID='" + sysDocID + "'";
				FillDataSet(salesQuoteData, "Sales_Order_Detail", textCommand);
				textCommand = "SELECT DISTINCT DND.VoucherID,DND.SysDocID FROM Sales_Quote_Detail SQD INNER JOIN   Delivery_Note_Detail DND ON SQD.VoucherID=DND.SourceVoucherID AND SQD.SysDocID=DND.SourceSysDocID  WHERE SQD.VoucherID='" + voucherID + "' AND SQD.SysDocID='" + sysDocID + "'";
				FillDataSet(salesQuoteData, "Delivery_Note_Detail", textCommand);
				return salesQuoteData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteSalesQuoteDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Sales_Quote_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag = Delete(commandText, sqlTransaction);
				return flag & new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidSalesQuote(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE Sales_Quote SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Sales Quote", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteSalesQuote(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteSalesQuoteDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Sales_Quote WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Sales Quote", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetSalesQuoteToPrint(string sysDocID, string voucherID)
		{
			return GetSalesQuoteToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetSalesQuoteToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT  SysDocID,VoucherID,SI.CustomerID,CustomerName,Customer.TaxIDNumber as CTaxIDNo,CustomerAddress,CA.Phone1,CA.Mobile,CA.Email,CA.Fax,CA.ContactName,TransactionDate,CASE SI.ApprovalStatus\twhen 1 THEN 'PENDING' WHEN 2 THEN 'WAITING' WHEN 3 THEN 'REJECTED' WHEN 10 THEN 'APPROVED' ELSE 'NA' END AS [APPROVAL STATUS] ,\r\n                               (SELECT TOP 1 ApproverID FROM Approval_Task AT WHERE AT.DocumentSysDocID=SysDocID AND AT.DocumentCode=VoucherID ORDER BY AT.DateApproved DESC) AS [Approved By],\r\n\t\t\t\t\t\t\t    SI.SalesPersonID,SP.FullName,SP.Phone1 AS [SP Mob],SP.Note AS [SP Des],RequiredDate,ShipToAddress AS ShippingAddress,ShippingMethodName,SI.JobID,J.JobName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,SI.ExpiryDate,\r\n                                SI.TermID,TermName,PT.Note AS [Payment Note],IsVoid,SI.Reference,Discount AS Discount,ISNULL(Total,0) - ISNULL(Discount,0) AS GrandTotal,\r\n                                ISNULL(TaxAmount ,0) AS Tax, ISNULL(RoundOff,0) as RoundOff,   Total AS Total,SI.PONumber,SI.Note,(SELECT TOP 1 RevisionNo FROM Sales_Quote_History SQH WHERE SQH.SysDocID=SI.SysDocID AND SQH.VoucherID=SI.VoucherID ORDER By RevisionNo DESC) AS Revision,(SELECT TOP 1 L.Note FROM System_Document SD INNER JOIN Location L ON SD.LocationID=L.LocationID WHERE SD.SysDocID=SI.SysDocID) AS [Showroom],SI.Remarks1, SI.Remarks2\r\n                                ,A.AreaID,A.AreaName [Area],J.Note [Job Note]  FROM  Sales_Quote SI LEFT JOIN Approval_Task AT ON SI.SysDocID=AT.DocumentCode AND SI.VoucherID=AT.DocumentCode \r\n\t\t\t\t\t\t\t\tINNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT OUTER JOIN Salesperson SP ON SP.SalespersonID=SI.SalespersonID\r\n                                LEFT OUTER JOIN Job J ON J.JobID=SI.JobID\t\r\n                                LEFT OUTER JOIN Area A  ON A.AreaID=Customer.AreaID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Sales_Quote", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Sales_Quote"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,SQD.ProductID,SQD.Description,ISNULL(UnitQuantity,SQD.Quantity) AS Quantity,P.Attribute1,\r\n                        P.Attribute2,P.Attribute3,P.MatrixParentID,P.Photo,PB.BrandName,P.Description2, convert(nvarchar(max),P.Note) AS Note,SQD.TaxAmount, SQD.TaxGroupID,\r\n                        SQD.UnitPrice AS UnitPrice,\r\n                        ISNULL(UnitQuantity,SQD.Quantity) * SQD.UnitPrice AS Total,SQD.UnitID, SQD.Remarks,P.Size,F.GenericListName as Finish, P.ClassID, PC.ClassName, P.ManufacturerID, PM.ManufacturerName,\r\n                        P.CategoryID, PG.CategoryName, P.UPC,SQD.RefSlNo,SQD.RefText1,SQD.RefText2,SQD.RefNum1,SQD.RefNum2,SQD.RefDate1,SQD.RefDate2 , Unit.UnitName\r\n                        FROM   Sales_Quote_Detail SQD\r\n                        INNER JOIN Product P ON P.ProductID = SQD.ProductID\r\n                        LEFT JOIN Product_Brand PB ON P.BrandID=PB.BrandID\r\n                        LEFT OUTER JOIN Product_Class PC ON P.ClassID=PC.ClassID\r\n                        LEFT OUTER JOIN Product_Manufacturer PM ON P.ManufacturerID=PM.ManufacturerID\r\n                        LEFT OUTER JOIN Product_Category PG ON P.CategoryID=PG.CategoryID\r\n                        LEFT OUTER JOIN Unit ON SQD.UnitID=unit.UnitID\r\n                        LEFT JOIN (SELECT GenericListID,GenericListName FROM Generic_List WHERE GenericListtYPE=33) AS F ON P.FinishingID= F.GenericListID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Sales_Quote_Detail", cmdText);
				dataSet.Relations.Add("CustomerQuote", new DataColumn[2]
				{
					dataSet.Tables["Sales_Quote"].Columns["SysDocID"],
					dataSet.Tables["Sales_Quote"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Sales_Quote_Detail"].Columns["SysDocID"],
					dataSet.Tables["Sales_Quote_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Sales_Quote"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Sales_Quote"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal num = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Discount"].ToString(), out result2);
					decimal.TryParse(row["Tax"].ToString(), out result3);
					int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
					num = decimal.Parse(row["RoundOff"].ToString(), NumberStyles.Any);
					row["TotalInWords"] = NumToWord.GetNumInWords(result - result2 + result3 + num, currencyDecimalPoints);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenQuotesSummary(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer] ,J.JobName FROM Sales_Quote SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID \r\n                             LEFT OUTER JOIN Job J ON J.JobID=SO.JobID\r\n                                WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(SO.Status,2) =2 \r\n                                AND (ExpiryDate IS NULL OR ExpiryDate >= GetDate())";
				if (customerID != "")
				{
					text = text + " AND SO.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "Sales_Quote", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenQuotesSummary(string customerID, DateTime fromDate, DateTime toDate)
		{
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(fromDate);
				string text2 = StoreConfiguration.ToSqlDateTimeString(toDate);
				DataSet dataSet = new DataSet();
				string text3 = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer], J.JobName FROM Sales_Quote SO\r\n                                INNER JOIN Customer C ON SO.CustomerID=C.CustomerID \r\n                                LEFT OUTER JOIN Job J ON J.JobID=SO.JobID\r\n\r\n                                 WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(SO.Status,2) =2 \r\n                                AND (ExpiryDate IS NULL OR ExpiryDate >= GetDate()) AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
				if (customerID != "")
				{
					text3 = text3 + " AND SO.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "Sales_Quote", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenQuotesSummary(string sysDocID, string customerID, string locationID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "";
				if (sysDocID != "")
				{
					DataSet entityLinks = new SystemDocuments(base.DBConfig).GetEntityLinks(sysDocID, SysDocEntityTypes.CustomerClass);
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
				string text2 = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer] \r\n                            FROM Sales_Quote SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID \r\n                             INNER JOIN System_Document SD ON SD.SysDocID=SO.SysDocID   \r\n                             WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(Status,2) =2 \r\n                             AND (ExpiryDate IS NULL OR ExpiryDate >= GetDate())";
				if (customerID != "")
				{
					text2 = text2 + " AND SO.CustomerID='" + customerID + "'";
				}
				if (!string.IsNullOrEmpty(text))
				{
					text2 = text2 + " AND C.CustomerClassID IN (" + text + ") ";
				}
				if (!string.IsNullOrEmpty(locationID))
				{
					text2 = text2 + " AND  SD.LocationID ='" + locationID + "' ";
				}
				FillDataSet(dataSet, "Sales_Quote", text2);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Quote Date],\r\n                            INV.SalespersonID [Salesperson],J.JobID,J.JobName,Total - ISNULL(Discount,0) AS [Amount],INV.CurrencyID [Currency],\r\n                            ISNULL((CASE INV.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' END) ,\r\n                            (CASE Customer.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' END)) AS TAXOPTION, INV.TaxAmount,  INV.Reference\r\n                            FROM         Sales_Quote INV\r\n                            LEFT JOIN Job J ON INV.JobID=J.JobID\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (sysDocID != "")
			{
				text3 = text3 + " AND INV.SysDocID = '" + sysDocID + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Sales_Quote", sqlCommand);
			return dataSet;
		}

		public int GetNextRevsionNo(string sysDocID, string voucherID)
		{
			DataSet dataSet = new DataSet();
			int result = 0;
			string textCommand = "SELECT TOP 1 JI.RevisionNo AS RevisionNo  \r\n                            FROM Sales_Quote_History JI  WHERE JI.SysDocID='" + sysDocID + "' AND JI.VoucherID = '" + voucherID + "' ORDER BY RevisionNo DESC";
			FillDataSet(dataSet, "Sales_Quote_History", textCommand);
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				int.TryParse(dataSet.Tables[0].Rows[0]["RevisionNo"].ToString(), out result);
				return result + 1;
			}
			return 1;
		}

		public DataSet GetLoadRevisionCombo(string sysDocID, string voucherID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT JI.SysDocID [Doc ID], JI.VoucherID [VoucherID],JI.TransactionDate AS [Date],JI.RevisionNo,CONVERT(INT,JI.RevisionNo) as RevisionNoInt   \r\n                            FROM Sales_Quote_History JI  WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "' ORDER BY JI.RevisionNo desc";
			FillDataSet(dataSet, "Sales_Quote", textCommand);
			return dataSet;
		}

		public DataSet GetSalesQuoteRevToPrint(string sysDocID, string voucherID, int RevisedNo)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,CA.Phone1,CA.Mobile,CA.Fax,CA.ContactName,TransactionDate,\r\n                                SI.SalesPersonID,SP.FullName,RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,SI.ExpiryDate,\r\n                                SI.TermID,TermName,IsVoid,Reference,Discount AS Discount,ISNULL(Total,0) - ISNULL(Discount,0) AS GrandTotal,\r\n                                ISNULL(TaxAmount ,0) AS Tax,Total AS Total,PONumber,SI.Note\r\n                                ,A.AreaID,A.AreaName [Area]  FROM  Sales_Quote_History SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT OUTER JOIN Salesperson SP ON SP.SalespersonID=SI.SalespersonID\r\n                                LEFT OUTER JOIN Area A  ON A.AreaID=Customer.AreaID\r\n                                WHERE RevisionNo=" + RevisedNo + " AND SysDocID = '" + sysDocID + "' AND VoucherID  ='" + voucherID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Sales_Quote", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Sales_Quote"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,SQD.ProductID,SQD.Description,ISNULL(UnitQuantity,SQD.Quantity) AS Quantity,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,\r\n                        SQD.UnitPrice AS UnitPrice,\r\n                        ISNULL(UnitQuantity,SQD.Quantity) * SQD.UnitPrice AS Total,SQD.UnitID\r\n                        FROM   Sales_Quote_Detail_History SQD\r\n                        INNER JOIN Product P ON P.ProductID = SQD.ProductID\r\n                        WHERE RevisionNo=" + RevisedNo + "AND SysDocID='" + sysDocID + "' AND VoucherID ='" + voucherID + "' ORDER BY RowIndex";
				FillDataSet(dataSet, "Sales_Quote_Detail", cmdText);
				dataSet.Relations.Add("CustomerQuote", new DataColumn[2]
				{
					dataSet.Tables["Sales_Quote"].Columns["SysDocID"],
					dataSet.Tables["Sales_Quote"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Sales_Quote_Detail"].Columns["SysDocID"],
					dataSet.Tables["Sales_Quote_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Sales_Quote"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Sales_Quote"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Discount"].ToString(), out result2);
					row["TotalInWords"] = NumToWord.GetNumInWords(result - result2);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool UpdateSalesQuoteStatus(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "Select Count(*) FROM Sales_Quote_Detail sqd\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' ";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Sales_Quote SET Status=4 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public bool ReactivateSalesQuote(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "Select Count(*) FROM Sales_Quote_Detail sqd\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' ";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Sales_Quote SET Status=2 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}
	}
}
