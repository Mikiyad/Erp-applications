using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PropertyCancel : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string PROPERTYID_PARM = "@PropertyID";

		private const string UNITID_PARM = "@UnitID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string CONTRACTSTARTDATE_PARM = "@ContractStartDate";

		private const string NOTE_PARM = "@Note";

		private const string CONTRACTENDDATE_PARM = "@ContractEndDate";

		private const string TOTALDAYS_PARM = "@TotalDays";

		private const string LASTSTAYDATE_PARM = "@LastStayDate";

		private const string STATUS_PARM = "@Status";

		private const string AGREEMENTTYPE_PARM = "@AgreementType";

		private const string AGREEMENTSTATUS_PARM = "@AgreementStatus";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string PARENTSYSDOCID_PARM = "@ParentSysDocID";

		private const string PARENTVOUCHERID_PARM = "@ParentVoucherID";

		private const string PROPERTYAGENT_PARAM = "@PropertyAgentID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public const string INCOMEID_PARM = "@IncomeID";

		public const string AMOUNT_PARM = "@Amount";

		public const string RATETYPE_PARM = "@RateType";

		public const string DESCRIPTION_PARM = "@Description";

		public const string ROWINDEX1_PARM = "@RowIndex";

		public const string ROWINDEX2_PARM = "@RowIndex";

		public const string REFERENCE_PARM = "@Reference";

		public const string PAIDAMOUNT_PARM = "@PaidAmount";

		public const string CHARGES_PARM = "@Charges";

		public const string REFUNDAMOUNT_PARM = "@RefundAmount";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		public PropertyCancel(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePropertyCancelText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Cancel", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("ContractStartDate", "@ContractStartDate"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("PropertyID", "@PropertyID"), new FieldValue("UnitID", "@UnitID"), new FieldValue("Status", "@Status"), new FieldValue("AgreementType", "@AgreementType"), new FieldValue("AgreementStatus", "@AgreementStatus"), new FieldValue("Note", "@Note"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("ContractEndDate", "@ContractEndDate"), new FieldValue("TotalDays", "@TotalDays"), new FieldValue("LastStayDate", "@LastStayDate"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("ParentSysDocID", "@ParentSysDocID"), new FieldValue("PropertyAgentID", "@PropertyAgentID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("ParentVoucherID", "@ParentVoucherID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_Cancel", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePropertyCancelCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePropertyCancelText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePropertyCancelText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ContractStartDate", SqlDbType.DateTime);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@PropertyID", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@AgreementType", SqlDbType.NVarChar);
			parameters.Add("@AgreementStatus", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@ContractEndDate", SqlDbType.DateTime);
			parameters.Add("@TotalDays", SqlDbType.Int);
			parameters.Add("@LastStayDate", SqlDbType.DateTime);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@PropertyAgentID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ParentSysDocID", SqlDbType.NVarChar);
			parameters.Add("@ParentVoucherID", SqlDbType.NVarChar);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ContractStartDate"].SourceColumn = "ContractStartDate";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@PropertyID"].SourceColumn = "PropertyID";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@AgreementType"].SourceColumn = "AgreementType";
			parameters["@AgreementStatus"].SourceColumn = "AgreementStatus";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@ContractEndDate"].SourceColumn = "ContractEndDate";
			parameters["@TotalDays"].SourceColumn = "TotalDays";
			parameters["@LastStayDate"].SourceColumn = "LastStayDate";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@ParentSysDocID"].SourceColumn = "ParentSysDocID";
			parameters["@ParentVoucherID"].SourceColumn = "ParentVoucherID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@PropertyAgentID"].SourceColumn = "PropertyAgentID";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
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

		private string GetInsertUpdatePropertyCancelDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Cancel_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("IncomeID", "@IncomeID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Reference", "@Reference"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdatePropertyRefundDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Refund_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("IncomeID", "@IncomeID"), new FieldValue("Description", "@Description"), new FieldValue("PaidAmount", "@PaidAmount"), new FieldValue("Charges", "@Charges"), new FieldValue("RefundAmount", "@RefundAmount"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Reference", "@Reference"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePropertyCancelDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePropertyCancelDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePropertyCancelDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@IncomeID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@IncomeID"].SourceColumn = "IncomeID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Reference"].SourceColumn = "Reference";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetInsertUpdatePropertyRefundDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePropertyRefundDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePropertyRefundDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@IncomeID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@PaidAmount", SqlDbType.Money);
			parameters.Add("@Charges", SqlDbType.Money);
			parameters.Add("@RefundAmount", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@IncomeID"].SourceColumn = "IncomeID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@PaidAmount"].SourceColumn = "PaidAmount";
			parameters["@Charges"].SourceColumn = "Charges";
			parameters["@RefundAmount"].SourceColumn = "RefundAmount";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(PropertyCancelData journalData)
		{
			return true;
		}

		public bool InsertUpdatePropertyCancel(PropertyCancelData propertyCancelData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePropertyCancelCommand = GetInsertUpdatePropertyCancelCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = propertyCancelData.PropertyCancelTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				dataRow["SourceVoucherID"].ToString();
				dataRow["SourceSysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Property_Cancel", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in propertyCancelData.PropertyCancelDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				foreach (DataRow row2 in propertyCancelData.PropertyCancelDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					string a = row2["CurrencyID"].ToString();
					if (a != "" && a != baseCurrencyID)
					{
						decimal d = decimal.Parse(row2["Amount"].ToString());
						decimal result = 1m;
						decimal.TryParse(row2["CurrencyRate"].ToString(), out result);
						d = ((!(row2["RateType"].ToString() == "M")) ? Math.Round(d / result, currencyDecimalPoints) : Math.Round(d * result, currencyDecimalPoints));
						row2["Amount"] = d;
					}
					else
					{
						row2["CurrencyRate"] = 1;
					}
				}
				foreach (DataRow row3 in propertyCancelData.PropertyRefundDetailTable.Rows)
				{
					row3["SysDocID"] = dataRow["SysDocID"];
					row3["VoucherID"] = dataRow["VoucherID"];
					string a2 = row3["CurrencyID"].ToString();
					if (a2 != "" && a2 != baseCurrencyID)
					{
						decimal d2 = decimal.Parse(row3["Amount"].ToString());
						decimal result2 = 1m;
						decimal.TryParse(row3["CurrencyRate"].ToString(), out result2);
						d2 = ((!(row3["RateType"].ToString() == "M")) ? Math.Round(d2 / result2, currencyDecimalPoints) : Math.Round(d2 * result2, currencyDecimalPoints));
						row3["RefundAmount"] = d2;
					}
					else
					{
						row3["CurrencyRate"] = 1;
					}
				}
				if (isUpdate)
				{
					flag &= DeletePropertyCancelDetailsRows(text2, text, isDeletingTransaction: false, sqlTransaction);
				}
				insertUpdatePropertyCancelCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(propertyCancelData, "Property_Cancel", insertUpdatePropertyCancelCommand)) : (flag & Insert(propertyCancelData, "Property_Cancel", insertUpdatePropertyCancelCommand)));
				insertUpdatePropertyCancelCommand = GetInsertUpdatePropertyCancelDetailsCommand(isUpdate: false);
				insertUpdatePropertyCancelCommand.Transaction = sqlTransaction;
				if (propertyCancelData.Tables["Property_Cancel_Detail"].Rows.Count > 0)
				{
					flag &= Insert(propertyCancelData, "Property_Cancel_Detail", insertUpdatePropertyCancelCommand);
				}
				insertUpdatePropertyCancelCommand = GetInsertUpdatePropertyRefundDetailsCommand(isUpdate: false);
				insertUpdatePropertyCancelCommand.Transaction = sqlTransaction;
				if (propertyCancelData.Tables["Property_Refund_Detail"].Rows.Count > 0)
				{
					flag &= Insert(propertyCancelData, "Property_Refund_Detail", insertUpdatePropertyCancelCommand);
				}
				string textCommand = "SELECT * FROM Property_Cancel  WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Property_Rent", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException(" no data.");
				}
				DataRow dataRow4 = dataSet.Tables["Property_Rent"].Rows[0];
				string text3 = "";
				string text4 = dataRow4["ParentSysDocID"].ToString();
				string text5 = dataRow4["ParentVoucherID"].ToString();
				string text6 = dataRow4["SourceSysDocID"].ToString();
				text3 = dataRow4["SourceVoucherID"].ToString();
				string text7 = "";
				text7 = dataRow4["AgreementType"].ToString();
				if (text6 != "" && text3 != "")
				{
					textCommand = "UPDATE Property_Cancel SET ParentSysDocID='" + text6 + "',ParentVoucherID='" + text3 + "' WHERE SysDocID='" + text2 + "' AND VoucherID='" + text + "'";
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				}
				textCommand = "UPDATE Property_Rent SET AgreementStatus=3 WHERE SysDocID='" + text6 + "' AND VoucherID='" + text3 + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				textCommand = "UPDATE Property_Cancel SET AgreementStatus=3 WHERE SysDocID='" + text6 + "' AND VoucherID='" + text3 + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				if (text7 == "2")
				{
					textCommand = "SELECT * FROM Property_Rent  WHERE SysDocID = '" + text6 + "' AND VoucherID = '" + text3 + "'";
					dataSet = new DataSet();
					FillDataSet(dataSet, "Property_Rent", textCommand, sqlTransaction);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException(" no data.");
					}
					DataRow dataRow5 = dataSet.Tables["Property_Rent"].Rows[0];
					text4 = dataRow5["ParentSysDocID"].ToString();
					text5 = dataRow5["ParentVoucherID"].ToString();
					if (text4 != "" && text5 != "" && text7 == "2")
					{
						textCommand = "UPDATE Property_Cancel SET ParentSysDocID='" + text4 + "',ParentVoucherID='" + text5 + "' WHERE SysDocID='" + text2 + "' AND VoucherID='" + text + "'";
						flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
					}
				}
				GLData journalData = CreatePropertyCancelGLData(propertyCancelData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (flag)
				{
					flag &= new PropertyUnit(base.DBConfig).UpdatePropertyUnitStatus(dataRow["UnitID"].ToString(), 1, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Property_Cancel", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Property Cancel";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Property_Cancel", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PropertyCancel, text2, text, "Property_Cancel", sqlTransaction);
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

		private GLData CreatePropertyCancelGLData(PropertyCancelData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.PropertyCancelTable.Rows[0];
			_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string text = dataRow["CustomerID"].ToString();
			string text2 = dataRow["SysDocID"].ToString();
			dataRow["VoucherID"].ToString();
			dataRow["AgreementType"].ToString();
			string text3 = "";
			string text4 = "";
			string value = dataRow["PropertyID"].ToString();
			string value2 = dataRow["UnitID"].ToString();
			string textCommand = "SELECT SD.LocationID,ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID,ISNULL(SD.COGSAccountID,LOC.COGSAccountID) AS COGSAccountID,\r\n                                ISNULL(SD.DiscountGivenAccountID,LOC.DiscountGivenAccountID) AS DiscountGivenAccountID,LOC.InventoryAccountID,ISNULL(SD.SalesAccountID,LOC.SalesAccountID) AS SalesAccountID,\r\n                                 LOC.UnInvoicedInventoryAccountID, ISNULL(SD.SalesTaxAccountID,LOC.SalesTaxAccountID) AS SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID,Loc.ConsignInAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
			if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
			{
				throw new CompanyException("There is no location assigned to this system document or location record is missing.");
			}
			text3 = dataSet.Tables["Accounts"].Rows[0]["ARAccountID"].ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)103;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = "";
			dataRow2["Narration"] = "Rental Cancellation" + dataRow["VoucherID"];
			dataRow2["Note"] = dataRow["Note"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			new Hashtable();
			new ArrayList();
			decimal num = default(decimal);
			decimal d = default(decimal);
			decimal d2 = default(decimal);
			foreach (DataRow row in transactionData.PropertyRefundDetailTable.Rows)
			{
				string text5 = row["IncomeID"].ToString();
				num = decimal.Parse(row["RefundAmount"].ToString());
				text4 = new PropertyIncomeCode(base.DBConfig).GetIncomeAccountID(text5, sqlTransaction);
				DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["IsBaseOnly"] = true;
				dataRow3["AccountID"] = text4;
				if (num != 0m)
				{
					if (num > 0m)
					{
						dataRow3["Debit"] = Math.Abs(num);
						dataRow3["Credit"] = DBNull.Value;
					}
					else
					{
						dataRow3["Debit"] = num;
						dataRow3["Credit"] = DBNull.Value;
					}
					dataRow3["Reference"] = text5;
					dataRow3["AttributeID1"] = value;
					dataRow3["AttributeID2"] = value2;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
					d2 += num;
				}
			}
			foreach (DataRow row2 in transactionData.PropertyCancelDetailTable.Rows)
			{
				string text6 = row2["IncomeID"].ToString();
				num = decimal.Parse(row2["Amount"].ToString());
				text4 = new PropertyIncomeCode(base.DBConfig).GetIncomeAccountID(text6, sqlTransaction);
				DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["IsBaseOnly"] = true;
				dataRow3["AccountID"] = text4;
				if (num > 0m)
				{
					dataRow3["Debit"] = DBNull.Value;
					dataRow3["Credit"] = Math.Abs(num);
				}
				else
				{
					dataRow3["Debit"] = num;
					dataRow3["Credit"] = DBNull.Value;
				}
				dataRow3["Reference"] = text6;
				dataRow3["AttributeID1"] = value;
				dataRow3["AttributeID2"] = value2;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				d += num;
			}
			DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			dataRow4["JournalID"] = 0;
			dataRow4["AccountID"] = text3;
			dataRow4["PayeeID"] = text;
			dataRow4["Debit"] = DBNull.Value;
			dataRow4["Credit"] = d2 - d;
			dataRow4["IsBaseOnly"] = true;
			dataRow4["PayeeType"] = "C";
			dataRow4["Reference"] = "";
			dataRow4["IsARAP"] = true;
			dataRow4["AttributeID1"] = value;
			dataRow4["AttributeID2"] = value2;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			return gLData;
		}

		public PropertyCancelData GetPropertyCancelByID(string sysDocID, string voucherID)
		{
			try
			{
				PropertyCancelData propertyCancelData = new PropertyCancelData();
				string textCommand = "SELECT * FROM Property_Cancel WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(propertyCancelData, "Property_Cancel", textCommand);
				if (propertyCancelData == null || propertyCancelData.Tables.Count == 0 || propertyCancelData.Tables["Property_Cancel"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM Property_Cancel_Detail TD WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(propertyCancelData, "Property_Cancel_Detail", textCommand);
				textCommand = "SELECT TD.*\r\n                        FROM Property_Refund_Detail TD WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(propertyCancelData, "Property_Refund_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(propertyCancelData, "Tax_Detail", textCommand);
				return propertyCancelData;
			}
			catch
			{
				throw;
			}
		}

		public PropertyCancelData GetPropertyCancelDetail(string sysDocID, string voucherID)
		{
			try
			{
				PropertyCancelData propertyCancelData = new PropertyCancelData();
				string textCommand = "SELECT * FROM Property_Cancel WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(propertyCancelData, "Property_Cancel", textCommand);
				if (propertyCancelData == null || propertyCancelData.Tables.Count == 0 || propertyCancelData.Tables["Property_Cancel"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,PID.IncomeName,PID.IncomeType\r\n                        FROM Property_Cancel_Detail TD  INNER JOIN PropertyIncome_Code PID ON TD.IncomeID=PID.IncomeID WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(propertyCancelData, "Property_Cancel_Detail", textCommand);
				return propertyCancelData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPropertypaymentdetails(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT GPD.SysDocID,GPD.VoucherID,GPD.TransactionDate,ISNULL(CR.Amount,GPD.Amount ) AS Amount,CR.ChequeNumber,CR.ChequeDate,SD.DocName as payType,(CASE CR.Status\r\n                          WHEN 1  THEN 'Received' WHEN 2 THEN 'Cleared' WHEN 3 THEN 'Discounted' WHEN 4 THEN 'SenttoBank' WHEN 8 THEN 'Bounced' WHEN 9 THEN 'Cancelled' ELSE '' END) Status FROM General_Payment_Detail GPD\r\n                                LEFT JOIN System_Document SD ON GPD.SysDocID=SD.SysDocID\r\n                                LEFT OUTER JOIN Cheque_Received CR ON GPD.SysDocID=CR.SysDocID AND GPD.VoucherID=CR.VoucherID WHERE GPD.SourceSysDocID= '" + sysDocID + "' AND GPD.SourceVoucherID= ('" + voucherID + "')";
				text = text + "UNION SELECT GPD.SysDocID,GPD.VoucherID,GPD.TransactionDate,CR.Amount,CR.ChequeNumber,CR.ChequeDate,SD.DocName as payType,(CASE CR.Status\r\n                          WHEN 1  THEN 'Received' WHEN 2 THEN 'Cleared' WHEN 3 THEN 'Discounted' WHEN 4 THEN 'SenttoBank' WHEN 8 THEN 'Bounced' WHEN 9 THEN 'Cancelled' ELSE '' END) Status FROM General_Payment_Detail GPD\r\n                        LEFT JOIN System_Document SD ON GPD.SysDocID=SD.SysDocID\r\n                        LEFT JOIN Cheque_Received CR ON GPD.SysDocID=CR.SysDocID AND GPD.VoucherID=CR.VoucherID\r\n                        WHERE GPD.SourceSysDocID IN (SELECT DISTINCT SourceSysDocID FROM Property_Rent WHERE ParentvoucherID IN (SELECT DISTINCT ParentVoucherID FROM Property_Rent WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "')) AND GPD.SourceVoucherID IN (SELECT DISTINCT SourceVoucherID FROM Property_Rent WHERE ParentvoucherID IN (SELECT DISTINCT ParentVoucherID FROM Property_Rent WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'))AND Status NOT IN (2)";
				FillDataSet(dataSet, "Property_Payment", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePropertyCancelDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string textCommand = "SELECT * FROM Property_Cancel  WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Property_Cancel", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException(" no data.");
				}
				string commandText = "DELETE FROM Property_Cancel_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Property_Refund_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidPropertyCancel(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeletePropertyCancel(string sysDocID, string voucherID, string propertyUnitID, string sourceSysDocID, string sourceVoucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeletePropertyCancelDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				text = "DELETE FROM Property_Cancel WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				text = "Update Property_Unit SET UnitStatus=2 WHERE PropertyUnitID='" + propertyUnitID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				text = "Update Property_Rent SET AgreementStatus=" + 1 + " WHERE SysDocID='" + sourceSysDocID + "' AND VoucherID = '" + sourceVoucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Property Cancel", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetPropertyCancelToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT C.CustomerName,CA.AddressPrintFormat,CA.Email,CA.Phone1,CA.Mobile,PC.*,P.*,PU.*,C.TaxIDNumber as CTaxIDNo, PA.PropertyAgentName\r\n                                FROM Property_Cancel PC\r\n                                LEFT OUTER JOIN Customer C ON C.CustomerID = PC.CustomerID\r\n                                LEFT OUTER JOIN Property P ON PC.[PropertyID]=P.PropertyID\r\n                                LEFT OUTER JOIN  Customer_Address CA ON C.CustomerID=CA.CustomerID\r\n                                LEFT OUTER JOIN  Property_Unit PU ON PC.UnitID=PU.PropertyUnitID \r\n                                LEFT OUTER JOIN Property_Agent PA ON PC.PropertyAgentID=PA.PropertyAgentID \r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Property_Cancel", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Property_Cancel"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT * FROM Property_Cancel_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Property_Cancel_Detail", cmdText);
				cmdText = "SELECT * FROM Property_Refund_Detail RFD\r\n                         Left join PropertyIncome_Code PC ON PC.IncomeID=RFD.IncomeID\r\n                         WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Property_Refund_Detail", cmdText);
				dataSet.Relations.Add("PropertyCancelDetail", new DataColumn[2]
				{
					dataSet.Tables["Property_Cancel"].Columns["SysDocID"],
					dataSet.Tables["Property_Cancel"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Property_Cancel_Detail"].Columns["SysDocID"],
					dataSet.Tables["Property_Cancel_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("PropertyCancelRefund", new DataColumn[2]
				{
					dataSet.Tables["Property_Cancel"].Columns["SysDocID"],
					dataSet.Tables["Property_Cancel"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Property_Refund_Detail"].Columns["SysDocID"],
					dataSet.Tables["Property_Refund_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPropertyCancelList(string sysDocID, string CustomerID, DateTime from, DateTime to)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT PR.SysDocID [Doc ID], PR.VoucherID [Number], PR.TransactionDate AS [Date],PR.CustomerID,Cus.CustomerName AS [Tenant],PR.PropertyID,PR.UnitID\r\n                             FROM Property_Rent PR LEFT JOIN Customer CUS ON PR.CustomerID = Cus.CustomerID\t\t\t\t\t\t\t\r\n                             WHERE  PR.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND PR.AgreementStatus=1 ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + " AND PR.SysDocID='" + sysDocID + "'";
			}
			if (!string.IsNullOrEmpty(CustomerID))
			{
				str = str + " AND PR.CustomerID='" + CustomerID + "'";
			}
			str += " ORDER BY  PR.VoucherID,PR.TransactionDate ";
			FillDataSet(dataSet, "Property_Cancel", str);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool val1, bool val2, int AgreeType)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT PC.SysDocID [SysDoc], PC.VoucherID [VoucherID], PC.TransactionDate AS [Date],PC.CustomerID,Cus.CustomerName AS [Tenant],P.PropertyName as Property,PU.PropertyUnitName as PropertyUnit\r\n                                 ,PC.SourceSysDocID,PC.SourceVoucherID,PC.ContractStartDate,PC.ContractEndDate,PC.TotalDays,PC.LastStayDate,PC.Note\r\n                                 FROM Property_Cancel PC LEFT JOIN Customer CUS ON PC.CustomerID = Cus.CustomerID\r\n                                 LEFT OUTER JOIN  Property P ON P.PropertyID=PC.PropertyID \r\n\t\t\t\t\t\t    \t LEFT OUTER JOIN  Property_Unit PU ON PC.UnitID=PU.PropertyUnitID \r\n                             WHERE  PC.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND 1=1 ";
			str = ((AgreeType != 1) ? (str + " AND PC.AgreementType=2") : (str + " AND PC.AgreementType=1"));
			str += " ORDER BY PC.VoucherID,PC.TransactionDate ";
			FillDataSet(dataSet, "Property_Cancel", str);
			return dataSet;
		}

		public DataSet GetPropertyCancelDetailList(string sysDocID, string CustomerID, DateTime from, DateTime to)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT PR.SysDocID [Doc ID], PR.VoucherID [Number], PR.TransactionDate AS [Date],PR.CustomerID,Cus.CustomerName AS [Tenant],PR.PropertyID,PR.UnitID,PR.ContractStartDate,PR.ContractEndDate\r\n                             FROM Property_Rent PR LEFT JOIN Customer CUS ON PR.CustomerID = Cus.CustomerID\t\t\t\t\t\t\t\r\n                             WHERE  PR.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND PR.AgreementStatus=1 ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + " AND PR.SysDocID='" + sysDocID + "'";
			}
			if (!string.IsNullOrEmpty(CustomerID))
			{
				str = str + " AND PR.CustomerID='" + CustomerID + "'";
			}
			str += " ORDER BY  PR.VoucherID,PR.TransactionDate ";
			FillDataSet(dataSet, "Property_Cancel", str);
			return dataSet;
		}
	}
}
