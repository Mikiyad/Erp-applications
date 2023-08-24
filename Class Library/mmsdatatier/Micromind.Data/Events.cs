using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Events : StoreObject
	{
		public const string EVENTID_PARM = "@EventID";

		public const string EVENTNAME_PARM = "@EventName";

		public const string TYPE_PARM = "@Type";

		public const string STARTDATE_PARM = "@StartDate";

		public const string ENDDATE_PARM = "@EndDate";

		public const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string LEADID_PARM = "@LeadID";

		public const string EMPLOYEEID_PARM = "@EmployeeID";

		public Events(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Events", new FieldValue("EventID", "@EventID", isUpdateConditionField: true), new FieldValue("EventName", "@EventName"), new FieldValue("Type", "@Type"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("LeadID", "@LeadID"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Events", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@EventID", SqlDbType.NVarChar);
			parameters.Add("@EventName", SqlDbType.NVarChar);
			parameters.Add("@Type", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@LeadID", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@EventID"].SourceColumn = "EventID";
			parameters["@EventName"].SourceColumn = "EventName";
			parameters["@Type"].SourceColumn = "Type";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@LeadID"].SourceColumn = "LeadID";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@Note"].SourceColumn = "Note";
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

		private string GetInsertUpdateDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Event_Employee", new FieldValue("EventID", "@EventID", isUpdateConditionField: true), new FieldValue("EmployeeID", "@EmployeeID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Events", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@EventID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters["@EventID"].SourceColumn = "EventID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		internal bool DeleteEventEmployeeDetailsRows(string EventID, SqlTransaction sqlTransaction)
		{
			bool result = true;
			try
			{
				string commandText = "DELETE FROM Event_Employee WHERE EventID = '" + EventID + "'";
				result = Delete(commandText, sqlTransaction);
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

		public bool InsertUpdateEvent(EventData eventData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				string text = eventData.EventTable.Rows[0]["EventID"].ToString();
				if (isUpdate)
				{
					flag &= DeleteEventEmployeeDetailsRows(text, sqlTransaction);
				}
				flag = ((!isUpdate) ? Insert(eventData, "Events", insertUpdateCommand) : Update(eventData, "Events", insertUpdateCommand));
				if (eventData.Tables["Event_Employee"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateDetailCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(eventData, "Event_Employee", insertUpdateCommand);
				}
				if (isUpdate)
				{
					AddActivityLog("Event", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog(" Event", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Events", "EventID", text, sqlTransaction, !isUpdate);
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

		public bool DeleteEvent(string eventID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag = DeleteEventEmployeeDetailsRows(eventID, sqlTransaction);
				if (flag)
				{
					string commandText = "DELETE FROM Events WHERE EventID = '" + eventID + "'";
					flag = Delete(commandText, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Event", eventID, ActivityTypes.Delete, null);
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

		public EventData GetEvent()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Events");
			EventData eventData = new EventData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(eventData, "Events", sqlBuilder);
			return eventData;
		}

		public EventData GetEventByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "EventID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Events";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EventData eventData = new EventData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(eventData, "Events", sqlBuilder);
			if (eventData == null || eventData.Tables.Count == 0 || eventData.Tables[0].Rows.Count == 0)
			{
				return eventData;
			}
			string textCommand = "SELECT EE.EventID,EE.EmployeeID [Doc ID],ISNULL(E.FirstName,'')+ ' ' + ISNULL(E.MiddleName,'') + ' ' + ISNULL(E.LastName,'') [Number] FROM Event_Employee EE\r\n                            LEFT JOIN Employee E ON EE.EmployeeID=E.EmployeeID\r\n                            WHERE EE.EventID='" + id + "'";
			FillDataSet(eventData, "Event_Employee", textCommand);
			return eventData;
		}

		public DataSet GetEventByFields(params string[] columns)
		{
			return GetEventByFields(null, isInactive: true, columns);
		}

		public DataSet GetEventByFields(string[] eventID, params string[] columns)
		{
			return GetEventByFields(eventID, isInactive: true, columns);
		}

		public DataSet GetEventByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Events");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "EventID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Events";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "IsInactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.NVarChar;
				commandHelper2.TableName = "Events";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Events", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEventList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EventID AS [Event Code], EventName AS Name, StartDate AS [Start Date], EndDate AS [End Date] ,GL.GenericListName AS [Event Type] FROM  Events\r\n                                LEFT JOIN Generic_List GL ON  GL.GenericListID=Events.Type";
			FillDataSet(dataSet, "Events", textCommand);
			return dataSet;
		}

		public DataSet GetEventDocumentAddress(string eventID, string addressField)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT " + addressField + ",AddressPrintFormat\r\n                           FROM Event INNER JOIN Event_Address CA ON Event.EventID=CA.EventID AND Event." + addressField + "=CA.AddressID\r\n                           WHERE CA.EventID='" + eventID + "'";
			FillDataSet(dataSet, "Events", textCommand);
			return dataSet;
		}

		public DataSet GetEventComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EventID [Code],EventName [Name],SalesPersonID\r\n                            FROM Event\r\n                            WHERE ISINACTIVE<>1 ORDER BY EventID,EventName";
			FillDataSet(dataSet, "Events", textCommand);
			return dataSet;
		}

		public DataSet GetEventSourceComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EventSourceID [Code],EventSourceName [Name]\r\n                            FROM Event_Source\r\n                            WHERE INACTIVE<>1 ORDER BY EventSourceID,EventSourceName";
			FillDataSet(dataSet, "Events", textCommand);
			return dataSet;
		}

		public DataSet GetEventListReport(string fromEvent, string toEvent, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT  EventID,EventName,CompanyName,EventClassID, AreaID,\r\n                                ContactName,CountryID,EventGroupID \r\n                                FROM Event \r\n                                WHERE 1=1 ";
			if (fromEvent != "")
			{
				text = text + " AND EventID>='" + fromEvent + "'";
			}
			if (toEvent != "")
			{
				text = text + " AND EventID<='" + toEvent + "'";
			}
			if (fromClass != "")
			{
				text = text + " AND EventClassID>='" + fromClass + "'";
			}
			if (toClass != "")
			{
				text = text + " AND EventClassID<='" + toClass + "'";
			}
			if (fromGroup != "")
			{
				text = text + " AND EventGroupID>='" + fromGroup + "'";
			}
			if (toGroup != "")
			{
				text = text + " AND EventGroupID<='" + toGroup + "'";
			}
			if (fromArea != "")
			{
				text = text + " AND AreaID>='" + fromArea + "'";
			}
			if (toArea != "")
			{
				text = text + " AND AreaID<='" + toArea + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Events", text);
			return dataSet;
		}

		public DataSet GetUpcomingEventsReport(string fromLead, string toLead, string fromUser, string toUser, bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT  E.EventID, E.EventName, E.StartDate, E.EndDate, E.Type,\r\n                            E.IsInactive,  E.Note\r\n                            FROM Events E\r\n                            WHERE 1=1 ";
			if (fromLead != "")
			{
				text = text + " AND E.LeadID>='" + fromLead + "'";
			}
			if (toLead != "")
			{
				text = text + " AND E.LeadID<='" + toLead + "'";
			}
			if (!showInactive)
			{
				text += " AND ISNULL(E.IsInactive,'False') = 'False'";
			}
			FillDataSet(dataSet, "Events", text);
			return dataSet;
		}

		public DataSet GetTopEvents(DateTime from, DateTime to, int count)
		{
			DataSet dataSet = new DataSet();
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string textCommand = "SELECT TOP " + count.ToString() + " EventName Name,SUM(Total-Discount) AS Sales \r\n                                    FROM Sales_Invoice SI\r\n                                    INNER JOIN Event CUS ON CUS.EventID=SI.EventID\r\n                                    WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n                                    GROUP BY EventName \r\n                                    HAVING SUM(Total-Discount)>0\r\n                                    ORDER BY Sales DESC";
			FillDataSet(dataSet, "Events", textCommand);
			return dataSet;
		}

		public bool SetFlag(string eventID, byte flagID)
		{
			try
			{
				string exp = (flagID <= 0) ? ("UPDATE Event SET Flag = NULL WHERE EventID = '" + eventID + "'") : ("UPDATE Event SET Flag = " + flagID + " WHERE EventID = '" + eventID + "'");
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}
	}
}
