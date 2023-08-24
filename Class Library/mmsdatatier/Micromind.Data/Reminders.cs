using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Reminders : StoreObject
	{
		public const string REMINDERSETTING_TABLE = "Reminder_Setting";

		public const string REMINDERID_PARM = "@ReminderID";

		public const string USERID_PARM = "@UserID";

		public const string DAYS_PARM = "@Days";

		public const string ISSELECTED_PARM = "@IsSelected";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Reminders(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSettingText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Reminder_Setting", new FieldValue("ReminderID", "@ReminderID", isUpdateConditionField: true), new FieldValue("UserID", "@UserID"), new FieldValue("Days", "@Days"), new FieldValue("IsSelected", "@IsSelected"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Reminder_Setting", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSettingCommand(bool isUpdate)
		{
			insertCommand = new SqlCommand(GetInsertUpdateSettingText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@ReminderID", SqlDbType.NVarChar);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@Days", SqlDbType.NVarChar);
			parameters.Add("@IsSelected", SqlDbType.NVarChar);
			parameters["@ReminderID"].SourceColumn = "ReminderID";
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@Days"].SourceColumn = "Days";
			parameters["@IsSelected"].SourceColumn = "IsSelected";
			return insertCommand;
		}

		public bool InsertUpdateSetting(ReminderData reminderData)
		{
			bool flag = true;
			SqlCommand sqlCommand = null;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string str = reminderData.ReminderSettingTable.Rows[0]["UserID"].ToString();
				string commandText = "DELETE FROM Reminder_Setting WHERE USERID='" + str + "'";
				flag &= Delete(commandText, sqlTransaction);
				sqlCommand = GetInsertUpdateSettingCommand(isUpdate: false);
				sqlCommand.Transaction = sqlTransaction;
				flag &= Insert(reminderData, "Reminder_Setting", sqlCommand);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Reminder Setting", "", ActivityTypes.Update, sqlTransaction);
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

		public ReminderData GetReminderSettingByUser(string userID)
		{
			string textCommand = "SELECT * FROM Reminder_Setting WHERE UserID='" + userID + "'";
			ReminderData reminderData = new ReminderData();
			FillDataSet(reminderData, "Reminder_Setting", textCommand);
			return reminderData;
		}

		public DataSet GetReminderCount(string userID)
		{
			return null;
		}

		public DataSet GetReminders(string userID)
		{
			string textCommand = "SELECT * FROM Reminder_Setting WHERE UserID='" + userID + "'";
			ReminderData reminderData = new ReminderData();
			FillDataSet(reminderData, "Reminder_Setting", textCommand);
			int num = 0;
			num = GetReminderDays("1", reminderData);
			DateTime today = DateTime.Today;
			today.AddDays(num);
			DataSet dataSet = new DataSet();
			textCommand = "SELECT DISTINCT  'Overdue Invoices' AS [Reminder],ARJ.CustomerID AS PayeeID,CustomerName AS PayeeName,SysDocID,VoucherID,ARDate,ARDueDate,ISNULL(Debit,0) AS Amount \r\n                    FROM ARJournal ARJ INNER JOIN Customer ON Customer.CustomerID=ARJ.CustomerID\r\n                    WHERE Credit IS NULL AND ISNULL(IsVoid,'False')='False' \r\n                    AND\r\n                    (SELECT SUM(ISNULL(PaymentAmount,0) +ISNULL(DiscountAmount,0)) FROM AR_Payment_Allocation ARPA\r\n                    WHERE ARJ.JournalID = ARPA.ARJournalID)<ISNULL(Debit,0)\r\n                    AND ISNULL(ARDueDate,ARDate)<= '" + CommonLib.ToSqlDateTimeString(today) + "'";
			textCommand += " UNION ";
			num = 0;
			num = GetReminderDays("2", reminderData);
			today = DateTime.Today;
			today.AddDays(-1 * num);
			textCommand = textCommand + "SELECT 'Overdue Bills' AS [Reminder], APJ.VendorID AS PayeeID,VendorName AS PayeeName,SysDocID,VoucherID,APDate,APDueDate,ISNULL(Credit,0) AS Amount \r\n                    FROM APJournal APJ INNER JOIN Vendor ON Vendor.VendorID=APJ.VendorID\r\n                    WHERE Debit IS NULL AND ISNULL(IsVoid,'False')='False' \r\n                    AND\r\n                    (SELECT SUM(ISNULL(PaymentAmount,0) +ISNULL(DiscountAmount,0)) FROM AP_Payment_Allocation APPA\r\n                    WHERE APJ.APID=APPA.APJournalID)<ISNULL(Credit,0)\r\n                    AND ISNULL(APDueDate,APDate)<= '" + CommonLib.ToSqlDateTimeString(today) + "'";
			textCommand += " UNION ";
			num = 0;
			num = GetReminderDays("3", reminderData);
			today = DateTime.Today;
			today.AddDays(num);
			textCommand = textCommand + "SELECT 'PDC Received' AS [Reminder], Chq.PayeeID AS PayeeID,\r\n                (CASE PayeeType\r\n                        WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName\r\n                        ELSE Account.AccountName END) AS AccountName\r\n\t\t             ,SysDocID,VoucherID,ReceiptDate,ChequeDate,Amount\r\n                    FROM Cheque_Received CHQ \r\n\t\t\t\t\t\tLEFt OUTER JOIN \r\n                        Account ON Chq.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON Chq.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON Chq.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON Chq.PayeeID=Employee.EmployeeID\r\n                    WHERE Chq.Status=1 AND ISNULL(IsVoid,'False')='False' \r\n                     \r\n                    AND ChequeDate <= '" + CommonLib.ToSqlDateTimeString(today) + "'";
			num = 0;
			num = GetReminderDays("3", reminderData);
			today = DateTime.Today;
			today.AddDays(-1 * num);
			textCommand = textCommand + "SELECT 'PDC Issued' AS [Reminder], Chq.PayeeID AS PayeeID,\r\n                (CASE PayeeType\r\n                        WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName\r\n                        ELSE Account.AccountName END) AS AccountName\r\n\t\t             ,SysDocID,VoucherID,IssueDate,ChequeDate,Amount\r\n                    FROM Cheque_Issued CHQ \r\n\t\t\t\t\t\tLEFt OUTER JOIN \r\n                        Account ON Chq.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON Chq.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON Chq.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON Chq.PayeeID=Employee.EmployeeID\r\n                    WHERE Chq.Status=1 AND ISNULL(IsVoid,'False')='False' \r\n                     \r\n                    AND ChequeDate <= '" + CommonLib.ToSqlDateTimeString(today) + "'";
			FillDataSet(dataSet, "Reminders", textCommand);
			dataSet.Tables.Add("ReminderType");
			dataSet.Tables["ReminderType"].Columns.Add("ReminderID", typeof(int));
			dataSet.Tables["ReminderType"].Columns.Add("ReminderName", typeof(string));
			dataSet.Tables["ReminderType"].Rows.Add(1, "Overdue Invoices");
			dataSet.Tables["ReminderType"].Rows.Add(2, "Overdue Bills");
			return dataSet;
		}

		private int GetReminderDays(string reminderID, DataSet data)
		{
			DataRow[] array = data.Tables["Reminder_Setting"].Select("ReminderID = " + reminderID);
			if (array.Length == 0)
			{
				return 0;
			}
			return int.Parse(array[0]["Days"].ToString());
		}
	}
}
