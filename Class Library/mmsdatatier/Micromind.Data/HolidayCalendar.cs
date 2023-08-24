using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class HolidayCalendar : StoreObject
	{
		private const string CALENDARID_PARM = "@CalendarID";

		private const string CALENDARNAME_PARM = "@CalendarName";

		private const string REMARKS_PARM = "@Remarks";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string OFFDAYS_PARM = "@OffDays";

		private const string OFFDATEFROM_PARM = "@OffDateFrom";

		private const string OFFDATETO_PARM = "@OffDateTo";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string HOLIDAYCALENDAR_TABLE = "Holiday_Calendar";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string FROMDATE_PARM = "@FromDate";

		private const string TODATE_PARM = "@ToDate";

		private const string DAYS_PARM = "@Days";

		private const string HOLIDAYTYPE_PARM = "@HolidayType";

		private const string HOLIDAYCALENDARDETAIL_TABLE = "Holiday_Calendar_Detail";

		public HolidayCalendar(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateHolidayCalendarText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Holiday_Calendar", new FieldValue("CalendarID", "@CalendarID", isUpdateConditionField: true), new FieldValue("CalendarName", "@CalendarName"), new FieldValue("OffDays", "@OffDays"), new FieldValue("OffDateFrom", "@OffDateFrom"), new FieldValue("OffDateTo", "@OffDateTo"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Holiday_Calendar", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateHolidayCalendarCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateHolidayCalendarText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateHolidayCalendarText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@CalendarID", SqlDbType.NVarChar);
			parameters.Add("@CalendarName", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@OffDays", SqlDbType.NVarChar);
			parameters.Add("@OffDateFrom", SqlDbType.DateTime);
			parameters.Add("@OffDateTo", SqlDbType.DateTime);
			parameters["@CalendarID"].SourceColumn = "CalendarID";
			parameters["@CalendarName"].SourceColumn = "CalendarName";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@OffDays"].SourceColumn = "OffDays";
			parameters["@OffDateFrom"].SourceColumn = "OffDateFrom";
			parameters["@OffDateTo"].SourceColumn = "OffDateTo";
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

		private string GetInsertUpdateHolidayCalendarDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Holiday_Calendar_Detail", new FieldValue("CalendarID", "@CalendarID"), new FieldValue("FromDate", "@FromDate"), new FieldValue("ToDate", "@ToDate"), new FieldValue("Days", "@Days"), new FieldValue("HolidayType", "@HolidayType"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateHolidayCalendarDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateHolidayCalendarDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateHolidayCalendarDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@CalendarID", SqlDbType.NVarChar);
			parameters.Add("@FromDate", SqlDbType.DateTime);
			parameters.Add("@ToDate", SqlDbType.DateTime);
			parameters.Add("@Days", SqlDbType.Int);
			parameters.Add("@HolidayType", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@CalendarID"].SourceColumn = "CalendarID";
			parameters["@FromDate"].SourceColumn = "FromDate";
			parameters["@ToDate"].SourceColumn = "ToDate";
			parameters["@Days"].SourceColumn = "Days";
			parameters["@HolidayType"].SourceColumn = "HolidayType";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(HolidayCalendarData journalData)
		{
			return true;
		}

		public bool InsertUpdateHolidayCalendar(HolidayCalendarData HolidayCalendarData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateHolidayCalendarCommand = GetInsertUpdateHolidayCalendarCommand(isUpdate);
			try
			{
				DataRow dataRow = HolidayCalendarData.HolidayCalendarTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["CalendarID"].ToString();
				foreach (DataRow row in HolidayCalendarData.HolidayCalendarDetailTable.Rows)
				{
					row["CalendarID"] = dataRow["CalendarID"];
				}
				if (isUpdate)
				{
					flag &= DeleteHolidayCalendarDetailsRows(text, sqlTransaction);
				}
				insertUpdateHolidayCalendarCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(HolidayCalendarData, "Holiday_Calendar", insertUpdateHolidayCalendarCommand)) : (flag & Insert(HolidayCalendarData, "Holiday_Calendar", insertUpdateHolidayCalendarCommand)));
				insertUpdateHolidayCalendarCommand = GetInsertUpdateHolidayCalendarDetailsCommand(isUpdate: false);
				insertUpdateHolidayCalendarCommand.Transaction = sqlTransaction;
				if (HolidayCalendarData.Tables["Holiday_Calendar_Detail"].Rows.Count > 0)
				{
					flag &= Insert(HolidayCalendarData, "Holiday_Calendar_Detail", insertUpdateHolidayCalendarCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				UpdateTableRowInsertUpdateInfo("Holiday_Calendar", "CalendarID", text, sqlTransaction, !isUpdate);
				string entityName = "Holiday Calendar";
				if (isUpdate)
				{
					flag &= AddActivityLog(entityName, text, ActivityTypes.Update, sqlTransaction);
					return flag;
				}
				flag &= AddActivityLog(entityName, text, ActivityTypes.Add, sqlTransaction);
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

		public HolidayCalendarData GetHolidayCalendarByID(string CalendarID)
		{
			try
			{
				HolidayCalendarData holidayCalendarData = new HolidayCalendarData();
				string textCommand = "SELECT * FROM Holiday_Calendar WHERE CalendarID='" + CalendarID + "'";
				FillDataSet(holidayCalendarData, "Holiday_Calendar", textCommand);
				if (holidayCalendarData == null || holidayCalendarData.Tables.Count == 0 || holidayCalendarData.Tables["Holiday_Calendar"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT *\r\n                        FROM Holiday_Calendar_Detail WHERE CalendarID='" + CalendarID + "' order by HolidayType,FromDate asc";
				FillDataSet(holidayCalendarData, "Holiday_Calendar_Detail", textCommand);
				return holidayCalendarData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteHolidayCalendarDetailsRows(string sysDocID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Holiday_Calendar_Detail WHERE CalendarID = '" + sysDocID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidHolidayCalendar(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteHolidayCalendar(string CalendarID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteHolidayCalendarDetailsRows(CalendarID, sqlTransaction);
				text = "DELETE FROM Holiday_Calendar WHERE CalendarID='" + CalendarID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Holiday Calendar", CalendarID, activityType, sqlTransaction);
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

		public DataSet GetHolidayCalendarToPrint(string sysDocID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string cmdText = "SELECT * from Holiday_Calendar WHERE CalendarID = '" + sysDocID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Holiday_Calendar", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Holiday_Calendar"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT * FROM Holiday_Calendar_Detail\r\n                        WHERE CalendarID='" + sysDocID + "' ORDER BY HolidayType,RowIndex";
				FillDataSet(dataSet, "Holiday_Calendar_Detail", cmdText);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetHolidayComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CalendarID [Code],CalendarName [Name]\r\n                            FROM Holiday_Calendar\r\n                            WHERE ISNULL(ISINACTIVE,'False')='False' ORDER BY CalendarID,CalendarName";
			FillDataSet(dataSet, "Holiday_Calendar", textCommand);
			return dataSet;
		}

		public DataSet GetHolidayCalendarList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CalendarID [Calendar Code],CalendarName [Calendar Name], IsInactive [Inactive]\r\n                           FROM Holiday_Calendar ";
			FillDataSet(dataSet, "Holiday_Calendar", textCommand);
			return dataSet;
		}
	}
}
