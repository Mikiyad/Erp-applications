using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Micromind.Data
{
	public class Schedules : StoreObject
	{
		private const string SCHEDULEID_PARM = "@ScheduleID";

		private const string SUBJECT_PARM = "@Subject";

		private const string STATUS_PARM = "@Status";

		private const string STARTINGDATE_PARM = "@StartingDate";

		private const string DUEDATE_PARM = "@DueDate";

		private const string ISRECURRING_PARM = "@IsRecurring";

		private const string RECURRINGPATTERN_PARM = "@RecurringPattern";

		private const string PRIORITY_PARM = "@Priority";

		private const string DAYSOFWEEK_PARM = "@DaysOfWeek";

		private const string DAYOFMONTH_PARM = "@DayOfMonth";

		private const string MONTHOFYEAR_PARM = "@MonthOfYear";

		private const string DAYOFMONTHINYEAR_PARM = "@DayOfMonthInYear";

		private const string COMPLETEDBY_PARM = "@CompleteddBy";

		private const string HASENDDATE_PARM = "@HasEndDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string COMPLETEDDATE_PARM = "@CompletedDate";

		private const string PERCENTCOMPLETED_PARM = "@PercentCompleted";

		private const string DATETIMESTAMP_FIELD = "@DateTimeStamp";

		private const string NOTE_PARM = "@Note";

		private const string USERID_PARM = "@UserID";

		public Schedules(Config config)
			: base(config)
		{
		}

		private string GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Schedules", new FieldValue("Subject", "@Subject"), new FieldValue("Status", "@Status"), new FieldValue("DueDate", "@DueDate"), new FieldValue("IsRecurring", "@IsRecurring"), new FieldValue("RecurringPattern", "@RecurringPattern"), new FieldValue("StartingDate", "@StartingDate"), new FieldValue("Priority", "@Priority"), new FieldValue("DaysOfWeek", "@DaysOfWeek"), new FieldValue("DayOfMonth", "@DayOfMonth"), new FieldValue("DayOfMonthInYear", "@DayOfMonthInYear"), new FieldValue("MonthOfYear", "@MonthOfYear"), new FieldValue("CompletedBy", "@CompleteddBy"), new FieldValue("HasEndDate", "@HasEndDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("CompletedDate", "@CompletedDate"), new FieldValue("PercentCompleted", "@PercentCompleted"), new FieldValue("Note", "@Note"));
			return sqlBuilder.GetInsertExpression();
		}

		private string GetUpdateText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Schedules", new FieldValue("ScheduleID", "@ScheduleID", isUpdateConditionField: true), new FieldValue("Subject", "@Subject"), new FieldValue("Status", "@Status"), new FieldValue("DueDate", "@DueDate"), new FieldValue("IsRecurring", "@IsRecurring"), new FieldValue("RecurringPattern", "@RecurringPattern"), new FieldValue("StartingDate", "@StartingDate"), new FieldValue("Priority", "@Priority"), new FieldValue("DaysOfWeek", "@DaysOfWeek"), new FieldValue("DayOfMonth", "@DayOfMonth"), new FieldValue("DayOfMonthInYear", "@DayOfMonthInYear"), new FieldValue("MonthOfYear", "@MonthOfYear"), new FieldValue("CompletedBy", "@CompleteddBy"), new FieldValue("HasEndDate", "@HasEndDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("CompletedDate", "@CompletedDate"), new FieldValue("PercentCompleted", "@PercentCompleted"), new FieldValue("Note", "@Note"));
			return sqlBuilder.GetUpdateExpression();
		}

		private string GetScheduleUserInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Schedule Users]", new FieldValue("UserID", "@UserID"), new FieldValue("ScheduleID", "@ScheduleID"));
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetScheduleUserInsertCommand()
		{
			if (insertCommand != null)
			{
				insertCommand = null;
			}
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetScheduleUserInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@UserID", SqlDbType.SmallInt);
				parameters.Add("@ScheduleID", SqlDbType.SmallInt);
				parameters["@UserID"].SourceColumn = "UserID";
				parameters["@ScheduleID"].SourceColumn = "ScheduleID";
			}
			return insertCommand;
		}

		private SqlCommand GetInsertCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@Subject", SqlDbType.NVarChar);
				parameters.Add("@Status", SqlDbType.TinyInt);
				parameters.Add("@DueDate", SqlDbType.DateTime);
				parameters.Add("@IsRecurring", SqlDbType.Bit);
				parameters.Add("@RecurringPattern", SqlDbType.TinyInt);
				parameters.Add("@StartingDate", SqlDbType.DateTime);
				parameters.Add("@Priority", SqlDbType.TinyInt);
				parameters.Add("@DaysOfWeek", SqlDbType.NVarChar);
				parameters.Add("@DayOfMonth", SqlDbType.TinyInt);
				parameters.Add("@DayOfMonthInYear", SqlDbType.TinyInt);
				parameters.Add("@MonthOfYear", SqlDbType.TinyInt);
				parameters.Add("@CompleteddBy", SqlDbType.SmallInt);
				parameters.Add("@HasEndDate", SqlDbType.Bit);
				parameters.Add("@EndDate", SqlDbType.DateTime);
				parameters.Add("@CompletedDate", SqlDbType.DateTime);
				parameters.Add("@PercentCompleted", SqlDbType.TinyInt);
				parameters.Add("@Note", SqlDbType.NText);
				parameters["@Subject"].SourceColumn = "Subject";
				parameters["@Status"].SourceColumn = "Status";
				parameters["@DueDate"].SourceColumn = "DueDate";
				parameters["@IsRecurring"].SourceColumn = "IsRecurring";
				parameters["@RecurringPattern"].SourceColumn = "RecurringPattern";
				parameters["@StartingDate"].SourceColumn = "StartingDate";
				parameters["@Priority"].SourceColumn = "Priority";
				parameters["@DaysOfWeek"].SourceColumn = "DaysOfWeek";
				parameters["@DayOfMonth"].SourceColumn = "DayOfMonth";
				parameters["@DayOfMonthInYear"].SourceColumn = "DayOfMonthInYear";
				parameters["@MonthOfYear"].SourceColumn = "MonthOfYear";
				parameters["@CompleteddBy"].SourceColumn = "CompletedBy";
				parameters["@HasEndDate"].SourceColumn = "HasEndDate";
				parameters["@EndDate"].SourceColumn = "EndDate";
				parameters["@CompletedDate"].SourceColumn = "CompletedDate";
				parameters["@PercentCompleted"].SourceColumn = "PercentCompleted";
				parameters["@Note"].SourceColumn = "Note";
			}
			return insertCommand;
		}

		private SqlCommand GetUpdateCommand()
		{
			if (updateCommand == null)
			{
				updateCommand = new SqlCommand(GetUpdateText(), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = updateCommand.Parameters;
				parameters.Add("@ScheduleID", SqlDbType.Int);
				parameters.Add("@Subject", SqlDbType.NVarChar);
				parameters.Add("@Status", SqlDbType.TinyInt);
				parameters.Add("@DueDate", SqlDbType.DateTime);
				parameters.Add("@IsRecurring", SqlDbType.Bit);
				parameters.Add("@RecurringPattern", SqlDbType.TinyInt);
				parameters.Add("@StartingDate", SqlDbType.DateTime);
				parameters.Add("@Priority", SqlDbType.TinyInt);
				parameters.Add("@DaysOfWeek", SqlDbType.NVarChar);
				parameters.Add("@DayOfMonth", SqlDbType.TinyInt);
				parameters.Add("@DayOfMonthInYear", SqlDbType.TinyInt);
				parameters.Add("@MonthOfYear", SqlDbType.TinyInt);
				parameters.Add("@CompleteddBy", SqlDbType.SmallInt);
				parameters.Add("@HasEndDate", SqlDbType.Bit);
				parameters.Add("@EndDate", SqlDbType.DateTime);
				parameters.Add("@CompletedDate", SqlDbType.DateTime);
				parameters.Add("@PercentCompleted", SqlDbType.TinyInt);
				parameters.Add("@Note", SqlDbType.NText);
				parameters["@ScheduleID"].SourceColumn = "ScheduleID";
				parameters["@Subject"].SourceColumn = "Subject";
				parameters["@Status"].SourceColumn = "Status";
				parameters["@DueDate"].SourceColumn = "DueDate";
				parameters["@IsRecurring"].SourceColumn = "IsRecurring";
				parameters["@RecurringPattern"].SourceColumn = "RecurringPattern";
				parameters["@StartingDate"].SourceColumn = "StartingDate";
				parameters["@Priority"].SourceColumn = "Priority";
				parameters["@DaysOfWeek"].SourceColumn = "DaysOfWeek";
				parameters["@DayOfMonth"].SourceColumn = "DayOfMonth";
				parameters["@DayOfMonthInYear"].SourceColumn = "DayOfMonthInYear";
				parameters["@MonthOfYear"].SourceColumn = "MonthOfYear";
				parameters["@CompleteddBy"].SourceColumn = "CompletedBy";
				parameters["@HasEndDate"].SourceColumn = "HasEndDate";
				parameters["@EndDate"].SourceColumn = "EndDate";
				parameters["@CompletedDate"].SourceColumn = "CompletedDate";
				parameters["@PercentCompleted"].SourceColumn = "PercentCompleted";
				parameters["@Note"].SourceColumn = "Note";
			}
			return updateCommand;
		}

		public bool InsertSchedule(ScheduleData scheduleData, ScheduleUserData scheduleUserData)
		{
			bool result = true;
			SqlCommand insertCommand = GetInsertCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				result = true;
				sqlTransaction = (insertCommand.Transaction = base.DBConfig.StartNewTransaction());
				Insert(scheduleData, "Schedules", insertCommand);
				if (scheduleData.ScheduleTable.Rows.Count <= 0)
				{
					return result;
				}
				object insertedRowIdentity = GetInsertedRowIdentity("Schedules", insertCommand);
				scheduleData.ScheduleTable.Rows[0]["ScheduleID"] = insertedRowIdentity;
				if (scheduleUserData != null && scheduleUserData.ScheduleUserTable.Rows.Count > 0)
				{
					foreach (DataRow row in scheduleUserData.ScheduleUserTable.Rows)
					{
						row["ScheduleID"] = insertedRowIdentity;
					}
					insertCommand = GetScheduleUserInsertCommand();
					insertCommand.Transaction = sqlTransaction;
					Insert(scheduleUserData, "[Schedule Users]", insertCommand);
				}
				UpdateTableRowByID("Schedules", "ScheduleID", "DateTimeStamp", insertedRowIdentity, DateTime.Now, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Schedules", "ScheduleID", insertedRowIdentity, sqlTransaction, isInsert: true);
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

		public bool InsertSchedule(ScheduleData scheduleData)
		{
			return InsertSchedule(scheduleData, null);
		}

		public bool UpdateSchedule(ScheduleData scheduleData, ScheduleUserData scheduleUserData)
		{
			bool result = true;
			SqlCommand updateCommand = GetUpdateCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				result = true;
				sqlTransaction = (updateCommand.Transaction = base.DBConfig.StartNewTransaction());
				Update(scheduleData, "Schedules", updateCommand);
				if (scheduleData.ScheduleTable.Rows.Count <= 0)
				{
					return result;
				}
				object obj = scheduleData.ScheduleTable.Rows[0]["ScheduleID"];
				if (scheduleUserData != null && scheduleUserData.ScheduleUserTable.Rows.Count > 0)
				{
					DeleteScheduleUser(int.Parse(obj.ToString()));
					foreach (DataRow row in scheduleUserData.ScheduleUserTable.Rows)
					{
						row["ScheduleID"] = obj;
					}
					updateCommand = GetScheduleUserInsertCommand();
					updateCommand.Transaction = sqlTransaction;
					Insert(scheduleUserData, "[Schedule Users]", updateCommand);
				}
				UpdateTableRowByID("Schedules", "ScheduleID", "DateTimeStamp", obj, DateTime.Now, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Schedules", "ScheduleID", obj, sqlTransaction, isInsert: false);
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

		public bool UpdateSchedule(ScheduleData scheduleData)
		{
			return UpdateSchedule(scheduleData, null);
		}

		public DataSet GetScheduleByUserByFields(int userID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			BuildSqlFields(sqlBuilder, columns);
			DataSet dataSet = null;
			try
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.TableName = "[Schedule Users]";
				commandHelper.FieldName = "UserID";
				commandHelper.FieldValue = userID;
				sqlBuilder.AddJointer("Schedules", "ScheduleID", "[Schedule Users]", "ScheduleID");
				sqlBuilder.AddCommandHelper(commandHelper);
				dataSet = new DataSet();
				sqlBuilder.UseDistinct = false;
				FillDataSet(dataSet, "Schedules", sqlBuilder);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public ScheduleData GetScheduleByID(int scheduleID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			ScheduleData scheduleData = null;
			try
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.TableName = "Schedules";
				commandHelper.FieldName = "ScheduleID";
				commandHelper.FieldValue = scheduleID;
				sqlBuilder.AddCommandHelper(commandHelper);
				scheduleData = new ScheduleData();
				sqlBuilder.UseDistinct = false;
				FillDataSet(scheduleData, "Schedules", sqlBuilder);
				return scheduleData;
			}
			catch
			{
				throw;
			}
		}

		public bool AssignScheduleUsers(int scheduleID, int[] usersID)
		{
			bool result = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteScheduleUser(scheduleID, usersID);
				if (usersID == null)
				{
					return result;
				}
				if (usersID.Length == 0)
				{
					return result;
				}
				ScheduleUserData scheduleUserData = new ScheduleUserData();
				foreach (int num in usersID)
				{
					DataRow dataRow = scheduleUserData.ScheduleUserTable.NewRow();
					dataRow["ScheduleID"] = scheduleID;
					dataRow["UserID"] = num;
					scheduleUserData.ScheduleUserTable.Rows.Add(dataRow);
				}
				SqlCommand scheduleUserInsertCommand = GetScheduleUserInsertCommand();
				scheduleUserInsertCommand.Transaction = sqlTransaction;
				result = Insert(scheduleUserData, "[Schedule Users]", scheduleUserInsertCommand);
				return result;
			}
			catch (Exception ex)
			{
				result = false;
				throw ex;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool DeleteSchedule(int scheduleID)
		{
			return DeleteSchedule(scheduleID, null);
		}

		internal bool DeleteSchedule(int scheduleID, SqlTransaction sqlTransaction)
		{
			try
			{
				DeleteTableRowByID("Schedules", "ScheduleID", scheduleID.ToString(), sqlTransaction);
			}
			catch
			{
				throw;
			}
			return true;
		}

		internal bool DeleteScheduleUser(int scheduleID)
		{
			try
			{
				DeleteTableRowByID("[Schedule Users]", "ScheduleID", scheduleID.ToString());
			}
			catch
			{
				throw;
			}
			return true;
		}

		internal bool DeleteScheduleUser(int scheduleID, int[] usersID)
		{
			if (usersID == null || usersID.Length == 0)
			{
				return DeleteScheduleUser(scheduleID);
			}
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("DELETE FROM [Schedule Users] WHERE ScheduleID=" + scheduleID.ToString() + " AND UserID IN(");
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int i = 0; i < usersID.Length; i++)
				{
					stringBuilder2.Append(usersID[i].ToString()).Append(",");
				}
				stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
				stringBuilder2.Append(") ");
				stringBuilder.Append(stringBuilder2.ToString()).Append(" ");
				return ExecuteNonQuery(stringBuilder.ToString()) > 0;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSchedulesByFields(int[] schedulesID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			BuildSqlFields(sqlBuilder, columns);
			if (schedulesID != null && schedulesID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "ScheduleID";
				commandHelper.FieldValue = schedulesID;
				commandHelper.TableName = "Schedules";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = null;
			try
			{
				sqlBuilder.AddOrderByColumn("Schedules", "DueDate", isAscending: false);
				dataSet = new ScheduleData();
				sqlBuilder.UseDistinct = false;
				dataSet.EnforceConstraints = false;
				FillDataSet(dataSet, "Schedules", sqlBuilder);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool SetStatus(int scheduleID, ScheduleStatus status, DateTime dateCompleted)
		{
			if (dateCompleted == DateTime.MinValue)
			{
				SetStatus(scheduleID, status);
			}
			bool result = true;
			SqlTransaction sqlTransaction = null;
			string val = "'" + dateCompleted.ToString(StoreConfiguration.CurrentCulture) + "'";
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				UpdateTableRowByID("Schedules", "ScheduleID", "Status", scheduleID, (int)status, sqlTransaction);
				UpdateTableRowByID("Schedules", "ScheduleID", "CompletedDate", scheduleID, val, sqlTransaction);
				if (status != 0)
				{
					return result;
				}
				AddRecurrencePattern(scheduleID);
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

		public bool SetStatus(int scheduleID, ScheduleStatus status)
		{
			bool result = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				UpdateTableRowByID("Schedules", "ScheduleID", "Status", scheduleID, (int)status, sqlTransaction);
				if (status == ScheduleStatus.Completed)
				{
					AddRecurrencePattern(scheduleID);
					return result;
				}
				string val = "'" + DBNull.Value + "'";
				UpdateTableRowByID("Schedules", "ScheduleID", "CompletedDate", scheduleID, val, sqlTransaction);
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

		private void AddRecurrencePattern(int id)
		{
			ScheduleData scheduleData = null;
			ScheduleData scheduleData2 = null;
			try
			{
				scheduleData = GetScheduleByID(id);
				if (scheduleData != null && scheduleData.ScheduleTable.Rows.Count != 0)
				{
					DataRow dataRow = scheduleData.ScheduleTable.Rows[0];
					if (bool.Parse(dataRow["IsRecurring"].ToString()))
					{
						int pattern = int.Parse(dataRow["RecurringPattern"].ToString());
						DateTime endDate = DateTime.Parse(dataRow["EndDate"].ToString());
						DateTime dueDate = DateTime.Parse(dataRow["DueDate"].ToString());
						string daysOfWeek = dataRow["DaysOfWeek"].ToString();
						bool hasEndDate = bool.Parse(dataRow["HasEndDate"].ToString());
						int dayOfMonth = int.Parse(dataRow["DayOfMonth"].ToString());
						int dayOfMonthInYear = int.Parse(dataRow["DayOfMonthInYear"].ToString());
						int monthOfYear = int.Parse(dataRow["MonthOfYear"].ToString());
						DateTime nextDueDate = GetNextDueDate(pattern, hasEndDate, endDate, dueDate, daysOfWeek, dayOfMonth, dayOfMonthInYear, monthOfYear);
						if (nextDueDate != DateTime.MinValue)
						{
							scheduleData2 = new ScheduleData();
							DataRow dataRow2 = scheduleData2.ScheduleTable.NewRow();
							foreach (DataColumn column in scheduleData.ScheduleTable.Columns)
							{
								if (dataRow2.Table.Columns.Contains(column.ColumnName))
								{
									dataRow2[column.ColumnName] = dataRow[column.ColumnName];
								}
							}
							dataRow2["DueDate"] = nextDueDate;
							dataRow2["Status"] = ScheduleStatus.Pending;
							scheduleData2.ScheduleTable.Rows.Add(dataRow2);
							InsertSchedule(scheduleData2);
						}
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (scheduleData != null)
				{
					scheduleData.Dispose();
					scheduleData = null;
				}
				if (scheduleData2 != null)
				{
					scheduleData2.Dispose();
					scheduleData2 = null;
				}
			}
		}

		private DateTime GetNextDueDate(int pattern, bool hasEndDate, DateTime endDate, DateTime dueDate, string daysOfWeek, int dayOfMonth, int dayOfMonthInYear, int monthOfYear)
		{
			switch (pattern)
			{
			case 1:
				dueDate = GetDaysPatternDueDate(hasEndDate, endDate, dueDate);
				break;
			case 2:
				dueDate = GetWeekPatternDueDate(hasEndDate, endDate, dueDate, daysOfWeek);
				break;
			case 3:
				dueDate = GetMonthPatternDueDate(hasEndDate, endDate, dueDate, dayOfMonth);
				break;
			case 4:
				dueDate = GetYearPatternDueDate(hasEndDate, endDate, dueDate, dayOfMonthInYear, monthOfYear);
				break;
			default:
				dueDate = DateTime.MinValue;
				break;
			}
			return dueDate;
		}

		private DateTime GetDaysPatternDueDate(bool hasEndDate, DateTime endDate, DateTime dueDate)
		{
			dueDate = dueDate.AddDays(1.0);
			if (hasEndDate && dueDate >= endDate)
			{
				return DateTime.MinValue;
			}
			return dueDate;
		}

		private DateTime GetWeekPatternDueDate(bool hasEndDate, DateTime endDate, DateTime dueDate, string daysOfWeek)
		{
			string[] array = daysOfWeek.Split(',');
			if (array.Length == 0)
			{
				return DateTime.MinValue;
			}
			int dayOfWeek = (int)dueDate.DayOfWeek;
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = -1;
			}
			int[] array3 = new int[array.Length];
			for (int j = 0; j < array3.Length; j++)
			{
				array3[j] = -1;
			}
			int num = 0;
			int num2 = 0;
			for (int k = 0; k < array.Length; k++)
			{
				int num3 = int.Parse(array[k]) - 1;
				if (num3 < dayOfWeek)
				{
					array3[num2++] = num3;
				}
				else if (num3 > dayOfWeek)
				{
					array2[num++] = num3;
				}
			}
			int smallestPositiveNumber = GetSmallestPositiveNumber(array2);
			int largestPositiveNumber = GetLargestPositiveNumber(array3);
			dayOfWeek = ((smallestPositiveNumber >= 0) ? Math.Abs(smallestPositiveNumber - dayOfWeek) : ((largestPositiveNumber < 0) ? 7 : Math.Abs(7 - dayOfWeek + largestPositiveNumber)));
			dueDate = dueDate.AddDays(dayOfWeek);
			if (hasEndDate && dueDate >= endDate)
			{
				return DateTime.MinValue;
			}
			return dueDate;
		}

		private DateTime GetMonthPatternDueDate(bool hasEndDate, DateTime endDate, DateTime dueDate, int dayOfMonth)
		{
			dueDate = dueDate.AddMonths(1);
			dueDate = new DateTime(dueDate.Year, dueDate.Month, dayOfMonth);
			if (hasEndDate && dueDate >= endDate)
			{
				return DateTime.MinValue;
			}
			return dueDate;
		}

		private DateTime GetYearPatternDueDate(bool hasEndDate, DateTime endDate, DateTime dueDate, int dayOfMonthInYear, int monthOfYear)
		{
			dueDate = dueDate.AddYears(1);
			dueDate = new DateTime(dueDate.Year, monthOfYear, dayOfMonthInYear);
			if (hasEndDate && dueDate >= endDate)
			{
				return DateTime.MinValue;
			}
			return dueDate;
		}

		private int GetSmallestPositiveNumber(int[] nums)
		{
			if (nums.Length == 0)
			{
				throw new ApplicationException("GetSmallest: Array must have at least one value.");
			}
			int num = nums[0];
			for (int i = 1; i < nums.Length; i++)
			{
				if (nums[i] >= 0 && nums[i] < num)
				{
					num = nums[i];
				}
			}
			return num;
		}

		private int GetLargestPositiveNumber(int[] nums)
		{
			if (nums.Length == 0)
			{
				throw new ApplicationException("GetLowest: Array must have at least one value.");
			}
			int num = nums[0];
			for (int i = 1; i < nums.Length; i++)
			{
				if (nums[i] >= 0 && nums[i] > num)
				{
					num = nums[i];
				}
			}
			return num;
		}
	}
}
