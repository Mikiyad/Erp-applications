using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class RecurringInvoice : StoreObject
	{
		public const string RECURRINGTRANSACTIONS_TABLE = "Recurring_Transaction";

		public const string RECURRINGTRANSACTIONDETAILS_TABLE = "Recurring_Transaction_Details";

		public const string SYSDOCID_PARM = "@SysDocID";

		public const string VOUCHERID_PARM = "@VoucherID";

		public const string TRANSACTIONID_PARM = "@TransactionID";

		public const string CREATEDSYSDOCID_PARM = "@CreatedSysDocID";

		public const string CREATEDVOUCHERID_PARM = "@CreatedVoucherID";

		public const string TRANSACTIONDATE_PARM = "@TransactionDate";

		public const string TYPE_PARM = "@SysDocType";

		public const string STARTDATE_PARM = "@StartDate";

		public const string ENDDATE_PARM = "@EndDate";

		public const string REPEATEEVERY_PARM = "@RepeateEvery";

		public const string INTERVAL_PARM = "@Interval";

		public const string LASTRUNDATE_PARM = "@LastRunDate";

		public const string LASTVOUCHERID_PARM = "@LastVoucherID";

		public const string LASTSYSDOCID_PARM = "@LastSysDocID";

		public const string PROCESSEDBY_PARM = "@ProcessedBy";

		public const string STATUS_PARM = "@Status";

		public const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		public const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		public const string CREATEDBY_PARM = "@CreatedBy";

		public const string DATECREATED_PARM = "@DateCreated";

		public const string UPDATEDBY_PARM = "@UpdatedBy";

		public const string DATEUPDATED_PARM = "@DateUpdated";

		public RecurringInvoice(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Recurring_Transaction_Detail", new FieldValue("TransactionID", "@TransactionID", isUpdateConditionField: true), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("CreatedSysDocID", "@CreatedSysDocID"), new FieldValue("CreatedVoucherID", "@CreatedVoucherID"), new FieldValue("TransactionDate", "@TransactionDate"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Recurring_Transaction_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Recurring_Transaction", new FieldValue("TransactionID", "@TransactionID", isUpdateConditionField: true), new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("SysDocType", "@SysDocType"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("RepeateEvery", "@RepeateEvery"), new FieldValue("Interval", "@Interval"), new FieldValue("LastRunDate", "@LastRunDate"), new FieldValue("LastSysDocID", "@LastSysDocID"), new FieldValue("LastVoucherID", "@LastVoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("ProcessedBy", "@ProcessedBy"), new FieldValue("Status", "@Status"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Recurring_Transaction", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@TransactionID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CreatedSysDocID", SqlDbType.NVarChar);
			parameters.Add("@CreatedVoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters["@TransactionID"].SourceColumn = "TransactionID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CreatedSysDocID"].SourceColumn = "CreatedSysDocID";
			parameters["@CreatedVoucherID"].SourceColumn = "CreatedVoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
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

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@TransactionID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SysDocType", SqlDbType.Int);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@RepeateEvery", SqlDbType.NVarChar);
			parameters.Add("@Interval", SqlDbType.Int);
			parameters.Add("@LastRunDate", SqlDbType.DateTime);
			parameters.Add("@LastSysDocID", SqlDbType.NVarChar);
			parameters.Add("@LastVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProcessedBy", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.Bit);
			parameters["@TransactionID"].SourceColumn = "TransactionID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SysDocType"].SourceColumn = "SysDocType";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@RepeateEvery"].SourceColumn = "RepeateEvery";
			parameters["@Interval"].SourceColumn = "Interval";
			parameters["@LastRunDate"].SourceColumn = "LastRunDate";
			parameters["@LastSysDocID"].SourceColumn = "LastSysDocID";
			parameters["@LastVoucherID"].SourceColumn = "LastVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@ProcessedBy"].SourceColumn = "ProcessedBy";
			parameters["@Status"].SourceColumn = "Status";
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

		public bool CreateRecurringInvoice(RecurringInvoiceData recurringInvoiceData, bool isUpdate, bool isPosting)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = null;
				if (!isPosting)
				{
					SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
					sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
					flag = Insert(recurringInvoiceData, "Recurring_Transaction", insertUpdateCommand);
					if (!isUpdate)
					{
						insertUpdateCommand = GetInsertUpdateDetailCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						flag &= Insert(recurringInvoiceData, "Recurring_Transaction_Detail", insertUpdateCommand);
					}
				}
				else
				{
					SqlCommand insertUpdateDetailCommand = GetInsertUpdateDetailCommand(isUpdate: false);
					sqlTransaction = (insertUpdateDetailCommand.Transaction = base.DBConfig.StartNewTransaction());
					flag = Insert(recurringInvoiceData, "Recurring_Transaction_Detail", insertUpdateDetailCommand);
					flag &= new SalesInvoiceNI(base.DBConfig).GenerateRecurringPropertyInvoiceData(recurringInvoiceData);
					DateTime dateTime = DateTime.Parse(recurringInvoiceData.Tables["Recurring_Transaction"].Rows[0]["LastRunDate"].ToString());
					string text = recurringInvoiceData.Tables["Recurring_Transaction"].Rows[0]["LastSysDocID"].ToString();
					string text2 = recurringInvoiceData.Tables["Recurring_Transaction"].Rows[0]["LastVoucherID"].ToString();
					bool flag2 = bool.Parse(recurringInvoiceData.Tables["Recurring_Transaction"].Rows[0]["Status"].ToString());
					flag &= (ExecuteNonQuery("UPDATE Recurring_Transaction SET LastRunDate='" + dateTime.ToShortDateString() + "', LastSysDocID ='" + text + "',LastVoucherID='" + text2 + "',Status='" + flag2.ToString() + "' WHERE TransactionID='" + recurringInvoiceData.Tables["Recurring_Transaction"].Rows[0]["TransactionID"].ToString() + "'", sqlTransaction) >= 0);
				}
				string text3 = recurringInvoiceData.RecurringInvoiceTransactionTable.Rows[0]["TransactionID"].ToString();
				if (isUpdate)
				{
					AddActivityLog("Recurring Invoice", text3, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("Recurring Invoice", text3, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Recurring_Transaction", "TransactionID", text3, sqlTransaction, isInsert: true);
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

		public RecurringInvoiceData GetRecurringInvoice()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Recurring_Transaction");
			RecurringInvoiceData recurringInvoiceData = new RecurringInvoiceData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(recurringInvoiceData, "Recurring_Transaction", sqlBuilder);
			return recurringInvoiceData;
		}

		public bool DeleteRecurringInvoice(string typeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Recurring_Transaction WHERE TransactionID = '" + typeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Recurring Invoice", typeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public RecurringInvoiceData GetRecurringInvoice(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TransactionID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Recurring_Transaction";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			RecurringInvoiceData recurringInvoiceData = new RecurringInvoiceData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(recurringInvoiceData, "Recurring_Transaction", sqlBuilder);
			return recurringInvoiceData;
		}

		public RecurringInvoiceData GetRecurringInvoiceByID(string sysDocID, string voucherID)
		{
			try
			{
				RecurringInvoiceData recurringInvoiceData = new RecurringInvoiceData();
				string textCommand = "SELECT * FROM Recurring_Transaction \r\n\t\t\t\t\t\tWHERE SourceVoucherID='" + voucherID + "' AND SourceSysDocID='" + sysDocID + "'";
				FillDataSet(recurringInvoiceData, "Recurring_Transaction", textCommand);
				return recurringInvoiceData;
			}
			catch
			{
				throw;
			}
		}

		public RecurringInvoiceData GetRecurringInvoiceByID(string transactionID)
		{
			try
			{
				RecurringInvoiceData recurringInvoiceData = new RecurringInvoiceData();
				string textCommand = "SELECT * FROM Recurring_Transaction \r\n\t\t\t\t\t\tWHERE TransactionID='" + transactionID + "'";
				FillDataSet(recurringInvoiceData, "Recurring_Transaction", textCommand);
				return recurringInvoiceData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPeriodicInvoice()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT RT.*,SI.Total,C.CustomerName as Tenant FROM Recurring_Transaction RT LEFT JOIN Sales_Invoice_NonInv SI ON SI.SysDocID=RT.SysDocID AND SI.VoucherID=RT.VoucherID\r\n                               LEFT JOIN Customer C ON C.CustomerID=SI.CustomerID  \r\n                               LEFT JOIN Property_Rent PR ON PR.SysDocID=RT.SourceSysDocID and  PR.VoucherID=RT.SourceVoucherID\r\n                               WHERE RT.Status='False' And PR.AgreementStatus!=3";
				FillDataSet(dataSet, "Recurring_Transaction", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DateTime GetLastPostedDate(string transactionID)
		{
			string exp = "SELECT Top 1 TransactionDate FROM Recurring_Transaction_Detail WHERE TransactionID='" + transactionID + "' ORDER BY TransactionDate DESC";
			return DateTime.Parse(ExecuteScalar(exp).ToString());
		}

		public string GetNextRecurringInvoiceDocumentNumber(string sysDocID, string lastNumber)
		{
			new DataSet();
			string text = "";
			int result = 1;
			text = CommonLib.GetNumberPrefix(lastNumber);
			int.TryParse(lastNumber.Substring(text.Length, lastNumber.Length - text.Length), out result);
			int num = lastNumber.Length - text.Length;
			string text2 = "";
			for (int i = 0; i < num; i++)
			{
				text2 += "0";
			}
			return text + (result + 1).ToString(text2);
		}

		public bool InsertBasePropertyInvoiceData(PropertyRentData rentalData, bool isUpdate, bool isPosting)
		{
			RecurringInvoiceData recurringInvoiceData = new RecurringInvoiceData();
			DataRow dataRow = rentalData.PropertyRentTable.Rows[0];
			DataRow dataRow2 = recurringInvoiceData.RecurringInvoiceTransactionTable.NewRow();
			dataRow2["TransactionID"] = dataRow["TransactionID"].ToString();
			dataRow2["SysDocID"] = dataRow["PeriodicSysDocID"].ToString();
			dataRow2["VoucherID"] = dataRow["PeriodicVoucherID"].ToString();
			dataRow2["SysDocType"] = 260;
			dataRow2["StartDate"] = dataRow["InvoiceStartDate"].ToString();
			dataRow2["EndDate"] = dataRow["ContractEndDate"].ToString();
			dataRow2["RepeateEvery"] = dataRow["Frequency"].ToString();
			dataRow2["Interval"] = int.Parse(dataRow["FrequencyCount"].ToString());
			dataRow2["LastRunDate"] = dataRow["InvoiceStartDate"].ToString();
			dataRow2["LastSysDocID"] = dataRow["PeriodicSysDocID"].ToString();
			dataRow2["LastVoucherID"] = dataRow["PeriodicVoucherID"].ToString();
			dataRow2["SourceSysDocID"] = dataRow["SysDocID"].ToString();
			dataRow2["SourceVoucherID"] = dataRow["VoucherID"].ToString();
			dataRow2["ProcessedBy"] = dataRow["ProcessedBy"].ToString();
			dataRow2["Status"] = false;
			dataRow2.EndEdit();
			recurringInvoiceData.RecurringInvoiceTransactionTable.Rows.Add(dataRow2);
			dataRow2 = recurringInvoiceData.RecurringTransactionDetailsTable.NewRow();
			dataRow2["TransactionID"] = dataRow["TransactionID"].ToString();
			dataRow2["SysDocID"] = dataRow["PeriodicSysDocID"].ToString();
			dataRow2["VoucherID"] = dataRow["PeriodicVoucherID"].ToString();
			dataRow2["CreatedSysDocID"] = DBNull.Value;
			dataRow2["CreatedVoucherID"] = DBNull.Value;
			dataRow2["TransactionDate"] = dataRow["InvoiceStartDate"].ToString();
			dataRow2.EndEdit();
			recurringInvoiceData.RecurringTransactionDetailsTable.Rows.Add(dataRow2);
			return (byte)(1 & (CreateRecurringInvoice(recurringInvoiceData, isUpdate, isPosting: false) ? 1 : 0)) != 0;
		}
	}
}
