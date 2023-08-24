using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Micromind.Data
{
	public sealed class Notes : StoreObject
	{
		private const string NOTEID_PARM = "@NoteID";

		private const string USERID_PARM = "@UserID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string NOTETEXT_PARM = "@NoteText";

		private const string TITLETEXT_PARM = "@TitleText";

		private const string NOTECOLOR_PARM = "@NoteColor";

		private const string NOTEFLAG_PARM = "@NoteFlag";

		private const string NOTEHEIGHT_PARM = "@NoteHeight";

		private const string NOTEWIDTH_PARM = "@NoteWidth";

		private const string NOTEXLOCATION_PARM = "@NoteXLocation";

		private const string NOTEYLOCATION_PARM = "@NoteYLocation";

		private const string NOTEISRIGHTTOLEFT_PARM = "@NoteIsRightToLeft";

		private const string NOTESCREENID_PARM = "@NoteScreenID";

		private const string REMINDERDATE_PARM = "@ReminderDate";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string ISALARM_PARM = "@IsAlarm";

		private const string NOTETYPE_PARM = "@NoteType";

		private const string REFERENCEID_PARM = "@ReferenceID";

		public Notes(Config config)
			: base(config)
		{
		}

		private string GetNoteUsersInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Note Users]", new FieldValue("NoteID", "@NoteID"), new FieldValue("UserID", "@UserID"), new FieldValue("NoteFlag", "@NoteFlag"));
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Notes", new FieldValue("NoteText", "@NoteText"), new FieldValue("TitleText", "@TitleText"), new FieldValue("NoteColor", "@NoteColor"), new FieldValue("CreatedBy", "@CreatedBy"), new FieldValue("NoteHeight", "@NoteHeight"), new FieldValue("NoteWidth", "@NoteWidth"), new FieldValue("NoteXLocation", "@NoteXLocation"), new FieldValue("NoteYLocation", "@NoteYLocation"), new FieldValue("NoteIsRightToLeft", "@NoteIsRightToLeft"), new FieldValue("ReminderDate", "@ReminderDate"), new FieldValue("NoteType", "@NoteType"), new FieldValue("ReferenceID", "@ReferenceID"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("IsAlarm", "@IsAlarm"), new FieldValue("NoteScreenID", "@NoteScreenID"));
			return sqlBuilder.GetInsertExpression();
		}

		private string GetUpdateText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Notes", new FieldValue("NoteID", "@NoteID", isUpdateConditionField: true), new FieldValue("NoteText", "@NoteText"), new FieldValue("TitleText", "@TitleText"), new FieldValue("NoteColor", "@NoteColor"), new FieldValue("CreatedBy", "@CreatedBy"), new FieldValue("NoteHeight", "@NoteHeight"), new FieldValue("NoteWidth", "@NoteWidth"), new FieldValue("NoteXLocation", "@NoteXLocation"), new FieldValue("NoteYLocation", "@NoteYLocation"), new FieldValue("NoteIsRightToLeft", "@NoteIsRightToLeft"), new FieldValue("NoteScreenID", "@NoteScreenID"), new FieldValue("NoteType", "@NoteType"), new FieldValue("ReferenceID", "@ReferenceID"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("IsAlarm", "@IsAlarm"), new FieldValue("ReminderDate", "@ReminderDate"));
			return sqlBuilder.GetUpdateExpression();
		}

		private string GetUpdateLocationText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Notes", new FieldValue("NoteID", "@NoteID", isUpdateConditionField: true), new FieldValue("TitleText", "@TitleText"), new FieldValue("NoteColor", "@NoteColor"), new FieldValue("NoteHeight", "@NoteHeight"), new FieldValue("NoteWidth", "@NoteWidth"), new FieldValue("NoteXLocation", "@NoteXLocation"), new FieldValue("ReminderDate", "@ReminderDate"), new FieldValue("NoteYLocation", "@NoteYLocation"));
			return sqlBuilder.GetUpdateExpression();
		}

		private SqlCommand GetNoteUsersInsertCommand()
		{
			insertCommand = null;
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetNoteUsersInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@NoteID", SqlDbType.Int);
				parameters.Add("@UserID", SqlDbType.SmallInt);
				parameters.Add("@NoteFlag", SqlDbType.TinyInt);
				parameters["@NoteID"].SourceColumn = "NoteID";
				parameters["@UserID"].SourceColumn = "UserID";
				parameters["@NoteFlag"].SourceColumn = "NoteFlag";
			}
			return insertCommand;
		}

		private SqlCommand GetInsertCommand()
		{
			insertCommand = null;
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@NoteText", SqlDbType.NText);
				parameters.Add("@TitleText", SqlDbType.NVarChar);
				parameters.Add("@NoteColor", SqlDbType.NVarChar);
				parameters.Add("@CreatedBy", SqlDbType.SmallInt);
				parameters.Add("@NoteHeight", SqlDbType.SmallInt);
				parameters.Add("@NoteWidth", SqlDbType.SmallInt);
				parameters.Add("@NoteXLocation", SqlDbType.SmallInt);
				parameters.Add("@NoteYLocation", SqlDbType.SmallInt);
				parameters.Add("@NoteIsRightToLeft", SqlDbType.Bit);
				parameters.Add("@ReminderDate", SqlDbType.DateTime);
				parameters.Add("@NoteScreenID", SqlDbType.NVarChar);
				parameters.Add("@NoteType", SqlDbType.TinyInt);
				parameters.Add("@ReferenceID", SqlDbType.Int);
				parameters.Add("@IsInactive", SqlDbType.Bit);
				parameters.Add("@IsAlarm", SqlDbType.Bit);
				parameters["@NoteText"].SourceColumn = "NoteText";
				parameters["@TitleText"].SourceColumn = "TitleText";
				parameters["@NoteColor"].SourceColumn = "NoteColor";
				parameters["@CreatedBy"].SourceColumn = "CreatedBy";
				parameters["@NoteHeight"].SourceColumn = "NoteHeight";
				parameters["@NoteWidth"].SourceColumn = "NoteWidth";
				parameters["@NoteXLocation"].SourceColumn = "NoteXLocation";
				parameters["@NoteYLocation"].SourceColumn = "NoteYLocation";
				parameters["@NoteIsRightToLeft"].SourceColumn = "NoteIsRightToLeft";
				parameters["@ReminderDate"].SourceColumn = "ReminderDate";
				parameters["@NoteScreenID"].SourceColumn = "NoteScreenID";
				parameters["@NoteType"].SourceColumn = "NoteType";
				parameters["@ReferenceID"].SourceColumn = "ReferenceID";
				parameters["@IsInactive"].SourceColumn = "IsInactive";
				parameters["@IsAlarm"].SourceColumn = "IsAlarm";
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
				parameters.Add("@NoteID", SqlDbType.Int);
				parameters.Add("@NoteText", SqlDbType.NText);
				parameters.Add("@TitleText", SqlDbType.NVarChar);
				parameters.Add("@NoteColor", SqlDbType.NVarChar);
				parameters.Add("@CreatedBy", SqlDbType.SmallInt);
				parameters.Add("@NoteHeight", SqlDbType.SmallInt);
				parameters.Add("@NoteWidth", SqlDbType.SmallInt);
				parameters.Add("@NoteXLocation", SqlDbType.SmallInt);
				parameters.Add("@NoteYLocation", SqlDbType.SmallInt);
				parameters.Add("@NoteIsRightToLeft", SqlDbType.Bit);
				parameters.Add("@NoteScreenID", SqlDbType.NVarChar);
				parameters.Add("@ReminderDate", SqlDbType.DateTime);
				parameters.Add("@NoteType", SqlDbType.TinyInt);
				parameters.Add("@ReferenceID", SqlDbType.Int);
				parameters.Add("@IsInactive", SqlDbType.Bit);
				parameters.Add("@IsAlarm", SqlDbType.Bit);
				parameters["@NoteID"].SourceColumn = "NoteID";
				parameters["@NoteText"].SourceColumn = "NoteText";
				parameters["@TitleText"].SourceColumn = "TitleText";
				parameters["@NoteColor"].SourceColumn = "NoteColor";
				parameters["@CreatedBy"].SourceColumn = "CreatedBy";
				parameters["@NoteHeight"].SourceColumn = "NoteHeight";
				parameters["@NoteWidth"].SourceColumn = "NoteWidth";
				parameters["@NoteXLocation"].SourceColumn = "NoteXLocation";
				parameters["@NoteYLocation"].SourceColumn = "NoteYLocation";
				parameters["@NoteIsRightToLeft"].SourceColumn = "NoteIsRightToLeft";
				parameters["@NoteScreenID"].SourceColumn = "NoteScreenID";
				parameters["@ReminderDate"].SourceColumn = "ReminderDate";
				parameters["@NoteType"].SourceColumn = "NoteType";
				parameters["@ReferenceID"].SourceColumn = "ReferenceID";
				parameters["@IsInactive"].SourceColumn = "IsInactive";
				parameters["@IsAlarm"].SourceColumn = "IsAlarm";
			}
			return updateCommand;
		}

		private SqlCommand GetUpdateLocationCommand()
		{
			if (updateCommand == null)
			{
				updateCommand = new SqlCommand(GetUpdateLocationText(), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = updateCommand.Parameters;
				parameters.Add("@NoteID", SqlDbType.Int);
				parameters.Add("@TitleText", SqlDbType.NVarChar);
				parameters.Add("@NoteColor", SqlDbType.NVarChar);
				parameters.Add("@NoteHeight", SqlDbType.SmallInt);
				parameters.Add("@NoteWidth", SqlDbType.SmallInt);
				parameters.Add("@NoteXLocation", SqlDbType.SmallInt);
				parameters.Add("@NoteYLocation", SqlDbType.SmallInt);
				parameters.Add("@ReminderDate", SqlDbType.DateTime);
				parameters["@NoteID"].SourceColumn = "NoteID";
				parameters["@TitleText"].SourceColumn = "TitleText";
				parameters["@NoteColor"].SourceColumn = "NoteColor";
				parameters["@NoteHeight"].SourceColumn = "NoteHeight";
				parameters["@NoteWidth"].SourceColumn = "NoteWidth";
				parameters["@NoteXLocation"].SourceColumn = "NoteXLocation";
				parameters["@NoteYLocation"].SourceColumn = "NoteYLocation";
				parameters["@ReminderDate"].SourceColumn = "ReminderDate";
			}
			return updateCommand;
		}

		public bool InsertNote(NoteData noteData)
		{
			bool result = true;
			SqlCommand insertCommand = GetInsertCommand();
			SqlTransaction sqlTransaction = null;
			object obj = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				Insert(noteData, "Notes", insertCommand, sqlTransaction);
				obj = GetInsertedRowIdentity("Notes", insertCommand);
				UpdateTableRowByID("Notes", "NoteID", "DateTimeStamp", obj, DateTime.Now, sqlTransaction);
				AddActivityLog("Note", null, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Notes", "NoteID", obj, sqlTransaction, isInsert: true);
				noteData.NoteTable.Rows[0]["NoteID"] = obj;
				string text = noteData.NoteTable.Rows[0]["CreatedBy"].ToString();
				if (text.Length <= 0)
				{
					return result;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("UPDATE ").Append("Notes").Append(" ");
				stringBuilder.Append("SET ").Append("CreatedBy").Append("=")
					.Append(text.ToString());
				ExecuteNonQuery(stringBuilder.ToString(), sqlTransaction);
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("INSERT ").Append("[Note Users]").Append(" ");
				stringBuilder2.Append("VALUES").Append("(").Append(text.ToString())
					.Append(",")
					.Append(obj.ToString())
					.Append(",");
				stringBuilder2.Append((byte)0).Append(")");
				ExecuteNonQuery(stringBuilder2.ToString(), sqlTransaction);
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

		public bool UpdateNote(NoteData noteData)
		{
			bool flag = true;
			object obj = null;
			SqlCommand updateCommand = GetUpdateCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (updateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(noteData, "Notes", updateCommand);
				if (!flag)
				{
					return flag;
				}
				obj = noteData.NoteTable.Rows[0]["NoteID"];
				UpdateTableRowByID("Notes", "NoteID", "DateTimeStamp", obj, DateTime.Now, sqlTransaction);
				AddActivityLog("Note", null, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Notes", "NoteID", obj, sqlTransaction, isInsert: false);
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

		public bool UpdateNoteLocation(NoteData noteData)
		{
			bool flag = true;
			SqlCommand updateLocationCommand = GetUpdateLocationCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (updateLocationCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(noteData, "Notes", updateLocationCommand);
				if (!flag)
				{
					return flag;
				}
				_ = noteData.NoteTable.Rows[0]["NoteID"];
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

		private string GetUserID(string loginName, SqlTransaction sqlTransaction)
		{
			object obj = ExecuteSelectScalar("Users", "", loginName, "UserID", sqlTransaction);
			if (obj == null)
			{
				return "";
			}
			return obj.ToString();
		}

		public NoteData GetNotesByUserID(int userID)
		{
			return GetNotesByUserID(userID, isInactive: true);
		}

		public NoteData GetNotesByUserID(int userID, bool isInactive)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			NoteData noteData = null;
			try
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.TableName = "[Note Users]";
				commandHelper.FieldName = "UserID";
				commandHelper.FieldValue = userID;
				sqlBuilder.AddCommandHelper(commandHelper);
				if (!isInactive)
				{
					commandHelper = new CommandHelper();
					commandHelper.TableName = "Notes";
					commandHelper.FieldName = "IsInactive";
					commandHelper.SqlOp.LogicalFieldOp = LogicalFieldOperator.OR;
					commandHelper.FieldValue = 0;
					sqlBuilder.AddCommandHelper(commandHelper);
					commandHelper = new CommandHelper();
					commandHelper.TableName = "Notes";
					commandHelper.FieldName = "IsInactive";
					commandHelper.SqlOp.CompareValueOp = CompareValueOperator.IS;
					commandHelper.FieldValue = "NULL";
					sqlBuilder.AddCommandHelper(commandHelper);
				}
				sqlBuilder.AddColumn("Notes", "NoteID");
				sqlBuilder.AddColumn("Notes", "CreatedBy");
				sqlBuilder.AddColumn("Notes", "NoteText");
				sqlBuilder.AddColumn("Notes", "NoteColor");
				sqlBuilder.AddColumn("[Note Users]", "NoteFlag");
				sqlBuilder.AddColumn("Notes", "NoteHeight");
				sqlBuilder.AddColumn("Notes", "NoteWidth");
				sqlBuilder.AddColumn("Notes", "NoteXLocation");
				sqlBuilder.AddColumn("Notes", "NoteYLocation");
				sqlBuilder.AddColumn("Notes", "TitleText");
				sqlBuilder.AddColumn("Notes", "NoteIsRightToLeft");
				sqlBuilder.AddColumn("Notes", "NoteScreenID");
				sqlBuilder.AddColumn("Notes", "ReminderDate");
				sqlBuilder.AddColumn("Notes", "IsInactive");
				sqlBuilder.AddColumn("Notes", "DateTimeStamp");
				sqlBuilder.AddJointer("Notes", "NoteID", "[Note Users]", "NoteID");
				sqlBuilder.AddJointer("[Note Users]", "UserID", "Users", "UserID");
				sqlBuilder.JointerSourceTable = "Notes";
				noteData = new NoteData();
				noteData.EnforceConstraints = false;
				sqlBuilder.UseDistinct = false;
				FillDataSet(noteData, "Notes", sqlBuilder);
				return noteData;
			}
			catch
			{
				throw;
			}
		}

		public NoteData GetNotesByID(int id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			NoteData noteData = null;
			try
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.TableName = "Notes";
				commandHelper.FieldName = "NoteID";
				commandHelper.FieldValue = id;
				sqlBuilder.AddCommandHelper(commandHelper);
				noteData = new NoteData();
				noteData.EnforceConstraints = false;
				sqlBuilder.UseDistinct = false;
				FillDataSet(noteData, "Notes", sqlBuilder);
				return noteData;
			}
			catch
			{
				throw;
			}
		}

		public NoteData GetNotesByType(NoteTypes noteType)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			NoteData noteData = null;
			try
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.TableName = "Notes";
				commandHelper.FieldName = "NoteType";
				commandHelper.FieldValue = (byte)noteType;
				sqlBuilder.AddCommandHelper(commandHelper);
				commandHelper = new CommandHelper();
				commandHelper.TableName = "[Note Users]";
				commandHelper.FieldName = "UserID";
				commandHelper.FieldValue = new Security(base.DBConfig).GetCurrentUserID();
				commandHelper.SqlFieldType = SqlDbType.Int;
				sqlBuilder.AddCommandHelper(commandHelper);
				sqlBuilder.AddJointer("Notes", "NoteID", "[Note Users]", "NoteID");
				noteData = new NoteData();
				noteData.EnforceConstraints = false;
				sqlBuilder.UseDistinct = false;
				FillDataSet(noteData, "Notes", sqlBuilder);
				return noteData;
			}
			catch
			{
				throw;
			}
		}

		public NoteData GetNotesByType(NoteTypes noteType, int referenceID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			NoteData noteData = null;
			try
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.TableName = "Notes";
				commandHelper.FieldName = "NoteType";
				commandHelper.FieldValue = (byte)noteType;
				sqlBuilder.AddCommandHelper(commandHelper);
				commandHelper = new CommandHelper();
				commandHelper.TableName = "Notes";
				commandHelper.FieldName = "ReferenceID";
				commandHelper.FieldValue = referenceID;
				sqlBuilder.AddCommandHelper(commandHelper);
				commandHelper = new CommandHelper();
				commandHelper.TableName = "[Note Users]";
				commandHelper.FieldName = "UserID";
				commandHelper.FieldValue = new Security(base.DBConfig).GetCurrentUserID();
				commandHelper.SqlFieldType = SqlDbType.Int;
				sqlBuilder.AddCommandHelper(commandHelper);
				sqlBuilder.AddJointer("Notes", "NoteID", "[Note Users]", "NoteID");
				noteData = new NoteData();
				noteData.EnforceConstraints = false;
				sqlBuilder.UseDistinct = false;
				FillDataSet(noteData, "Notes", sqlBuilder);
				return noteData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetNotesByFields(NoteTypes noteType, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			CommandHelper commandHelper = new CommandHelper();
			commandHelper.TableName = "Notes";
			commandHelper.FieldName = "NoteType";
			commandHelper.FieldValue = (byte)noteType;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Note Users]";
			commandHelper.FieldName = "UserID";
			commandHelper.FieldValue = new Security(base.DBConfig).GetCurrentUserID();
			commandHelper.SqlFieldType = SqlDbType.Int;
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.AddJointer("Notes", "NoteID", "[Note Users]", "NoteID");
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Notes", sqlBuilder);
			return dataSet;
		}

		public DataSet GetNotesByFields(NoteFlags[] noteFlags, NoteTypes[] noteTypes, int[] references, int maxNotes, DateTime reminderFrom, DateTime reminderTo, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Note Users]";
			commandHelper.FieldName = "UserID";
			commandHelper.FieldValue = new Security(base.DBConfig).GetCurrentUserID();
			commandHelper.SqlFieldType = SqlDbType.Int;
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.AddJointer("Notes", "NoteID", "[Note Users]", "NoteID");
			if (noteFlags != null && noteFlags.Length != 0)
			{
				commandHelper = new CommandHelper();
				commandHelper.TableName = "[Note Users]";
				commandHelper.FieldName = "NoteFlag";
				commandHelper.FieldValue = noteFlags;
				commandHelper.SqlFieldType = SqlDbType.Int;
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (noteTypes != null && noteTypes.Length != 0)
			{
				commandHelper = new CommandHelper();
				commandHelper.TableName = "Notes";
				commandHelper.FieldName = "NoteType";
				commandHelper.FieldValue = noteTypes;
				commandHelper.SqlFieldType = SqlDbType.Int;
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (references != null && references.Length != 0)
			{
				commandHelper = new CommandHelper();
				commandHelper.TableName = "Notes";
				commandHelper.FieldName = "ReferenceID";
				commandHelper.FieldValue = references;
				commandHelper.SqlFieldType = SqlDbType.Int;
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				commandHelper = new CommandHelper();
				commandHelper.TableName = "Notes";
				commandHelper.FieldName = "IsInactive";
				commandHelper.FieldValue = 0;
				commandHelper.AllowNull = true;
				commandHelper.SqlFieldType = SqlDbType.Int;
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (reminderFrom.Year != DateTime.MinValue.Year)
			{
				commandHelper = new CommandHelper();
				commandHelper.FieldName = "ReminderDate";
				commandHelper.SqlFieldType = SqlDbType.DateTime;
				commandHelper.FieldValue = reminderFrom.ToString(StoreConfiguration.CurrentCulture);
				commandHelper.FieldValue2 = reminderTo.ToString(StoreConfiguration.CurrentCulture);
				commandHelper.TableName = "Notes";
				commandHelper.SqlOp.LogicalValueOp = LogicalValueOperator.BETWEEN;
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, maxNotes, "Notes", sqlBuilder, null);
			return dataSet;
		}

		public DataSet GetNotesByFields(NoteTypes noteType, int referenceID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			CommandHelper commandHelper = new CommandHelper();
			commandHelper.TableName = "Notes";
			commandHelper.FieldName = "NoteType";
			commandHelper.FieldValue = (byte)noteType;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.TableName = "Notes";
			commandHelper.FieldName = "ReferenceID";
			commandHelper.FieldValue = referenceID;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Note Users]";
			commandHelper.FieldName = "UserID";
			commandHelper.FieldValue = new Security(base.DBConfig).GetCurrentUserID();
			commandHelper.SqlFieldType = SqlDbType.Int;
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.AddJointer("Notes", "NoteID", "[Note Users]", "NoteID");
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Notes", sqlBuilder);
			return dataSet;
		}

		public DataSet GetNotesByFields(int noteID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			CommandHelper commandHelper = new CommandHelper();
			commandHelper.TableName = "Notes";
			commandHelper.FieldName = "NoteID";
			commandHelper.FieldValue = noteID;
			sqlBuilder.AddCommandHelper(commandHelper);
			commandHelper = new CommandHelper();
			commandHelper.TableName = "[Note Users]";
			commandHelper.FieldName = "UserID";
			commandHelper.FieldValue = new Security(base.DBConfig).GetCurrentUserID();
			commandHelper.SqlFieldType = SqlDbType.Int;
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.AddJointer("Notes", "NoteID", "[Note Users]", "NoteID");
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Notes", sqlBuilder);
			return dataSet;
		}

		public DataSet GetReminderNote(int userID, DateTime reminderDate)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DataSet dataSet = new DataSet();
			try
			{
				stringBuilder.Append("SELECT Notes.NoteID FROM ").Append("Notes").Append(" Notes");
				stringBuilder.Append(" LEFT OUTER JOIN [Note Users] Users ");
				stringBuilder.Append(" ON Users.NoteID=Notes.NoteID ").Append(" WHERE ").Append("UserID")
					.Append("=")
					.Append(userID.ToString());
				stringBuilder.Append(" AND ").Append("NoteFlag").Append("<>");
				stringBuilder.Append((byte)4).Append(" AND ").Append("ReminderDate");
				stringBuilder.Append("<='").Append(reminderDate.ToString(StoreConfiguration.CurrentCulture)).Append("'");
				stringBuilder.Append(" AND (").Append("IsInactive").Append("=0");
				stringBuilder.Append(" OR ").Append("IsInactive").Append(" IS NULL) ");
				stringBuilder.Append("AND ").Append("IsAlarm").Append("=1 ");
				FillDataSet(dataSet, "Notes", stringBuilder.ToString());
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public int GetReminderNoteCount(int userID, DateTime reminderDate)
		{
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				stringBuilder.Append("SELECT Count(Notes.NoteID) FROM ").Append("Notes").Append(" Notes");
				stringBuilder.Append(" LEFT OUTER JOIN [Note Users] Users ");
				stringBuilder.Append(" ON Users.NoteID=Notes.NoteID ").Append(" WHERE ").Append("UserID")
					.Append("=")
					.Append(userID.ToString());
				stringBuilder.Append(" AND ").Append("NoteFlag").Append("<>");
				stringBuilder.Append((byte)4).Append(" AND ").Append("ReminderDate");
				stringBuilder.Append("<='").Append(reminderDate.ToString(StoreConfiguration.CurrentCulture)).Append("' ");
				stringBuilder.Append(" AND (").Append("IsInactive").Append("=0");
				stringBuilder.Append(" OR ").Append("IsInactive").Append(" IS NULL) ");
				stringBuilder.Append("AND ").Append("IsAlarm").Append("=1 ");
				object obj = ExecuteScalar(stringBuilder.ToString());
				if (obj != null && obj != DBNull.Value)
				{
					return int.Parse(obj.ToString());
				}
			}
			catch
			{
				throw;
			}
			return 0;
		}

		public NoteData GetNoteUsers(int noteID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			NoteData noteData = null;
			try
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.TableName = "[Note Users]";
				commandHelper.FieldName = "NoteID";
				commandHelper.FieldValue = noteID;
				sqlBuilder.AddColumn("[Note Users]", "NoteID");
				sqlBuilder.AddColumn("[Note Users]", "UserID");
				sqlBuilder.AddCommandHelper(commandHelper);
				noteData = new NoteData();
				sqlBuilder.UseDistinct = false;
				FillDataSet(noteData, "[Note Users]", sqlBuilder);
				return noteData;
			}
			catch
			{
				throw;
			}
		}

		public bool AssignNoteUsers(NoteData noteData)
		{
			bool result = true;
			GetInsertCommand();
			SqlTransaction sqlTransaction = null;
			StringBuilder stringBuilder = new StringBuilder();
			string value = noteData.NoteUsersTable.Rows[0]["NoteID"].ToString();
			stringBuilder.Append("DELETE FROM ").Append("[Note Users]").Append(" ");
			stringBuilder.Append("WHERE ").Append("NoteID").Append("=")
				.Append(value);
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				ExecuteNonQuery(stringBuilder.ToString(), sqlTransaction);
				Insert(noteData, "[Note Users]", GetNoteUsersInsertCommand(), sqlTransaction);
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

		public bool RemoveNoteUser(int noteID, int userID)
		{
			bool result = true;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DELETE FROM ").Append("[Note Users]").Append(" ");
			stringBuilder.Append("WHERE ").Append("NoteID").Append("=")
				.Append(noteID.ToString())
				.Append(" AND ");
			stringBuilder.Append("UserID").Append("=").Append(userID.ToString());
			try
			{
				ExecuteNonQuery(stringBuilder.ToString());
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
		}

		public bool DeleteNote(int noteID)
		{
			return DeleteNote(noteID, null);
		}

		internal bool DeleteNote(int noteID, SqlTransaction sqlTransaction)
		{
			return DeleteNote(new int[1]
			{
				noteID
			}, sqlTransaction);
		}

		public bool DeleteNote(int[] notesID)
		{
			return DeleteNote(notesID, null);
		}

		internal bool DeleteNote(int[] notesID, SqlTransaction sqlTransaction)
		{
			try
			{
				if (DeleteTableRowByID("Notes", "NoteID", notesID, sqlTransaction))
				{
					AddActivityLog("Note", null, ActivityTypes.Delete, null);
				}
			}
			catch
			{
				throw;
			}
			return true;
		}

		public NoteData GetNoteByScreenID(int userID, int screendID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			NoteData noteData = null;
			try
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.TableName = "Notes";
				commandHelper.FieldName = "NoteScreenID";
				commandHelper.FieldValue = screendID;
				sqlBuilder.AddCommandHelper(commandHelper);
				if (userID != -1)
				{
					commandHelper = new CommandHelper();
					commandHelper.TableName = "Notes";
					commandHelper.FieldName = "CreatedBy";
					commandHelper.FieldValue = userID;
					sqlBuilder.AddCommandHelper(commandHelper);
				}
				else
				{
					commandHelper = new CommandHelper();
					commandHelper.TableName = "Notes";
					commandHelper.FieldName = "CreatedBy";
					commandHelper.FieldValue = "NULL";
					commandHelper.SqlOp.CompareValueOp = CompareValueOperator.IS;
					sqlBuilder.AddCommandHelper(commandHelper);
				}
				sqlBuilder.AddColumn("Notes", "NoteID");
				sqlBuilder.AddColumn("Notes", "NoteColor");
				sqlBuilder.AddColumn("Notes", "NoteText");
				sqlBuilder.AddColumn("Notes", "DateTimeStamp");
				noteData = new NoteData();
				noteData.EnforceConstraints = false;
				sqlBuilder.UseDistinct = false;
				FillDataSet(noteData, "Notes", sqlBuilder);
				return noteData;
			}
			catch
			{
				throw;
			}
		}

		public DateTime GetNoteDateTimeStamp(int noteID)
		{
			object obj = ExecuteSelectScalar("Notes", "NoteID", noteID, "DateTimeStamp");
			if (obj == null || obj == DBNull.Value)
			{
				return DateTime.MinValue;
			}
			return DateTime.Parse(obj.ToString());
		}

		public bool SetNoteFlag(int userID, int noteID, NoteFlags noteFlag)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE ").Append("[Note Users]");
			stringBuilder.Append(" SET ").Append("NoteFlag").Append("=");
			stringBuilder.Append((byte)noteFlag).Append(" WHERE ");
			stringBuilder.Append("UserID").Append("=");
			stringBuilder.Append(userID.ToString()).Append(" AND ");
			stringBuilder.Append("NoteID").Append("=");
			stringBuilder.Append(noteID.ToString());
			return ExecuteNonQuery(stringBuilder.ToString()) > 0;
		}

		public bool ActivateNote(object id, bool activate)
		{
			activate = !activate;
			bool num = UpdateTableRowByID("Notes", "NoteID", "IsInactive", id, Convert.ToByte(activate));
			if (num)
			{
				if (!activate)
				{
					AddActivityLog("Note", null, ActivityTypes.Activate, null);
					return num;
				}
				AddActivityLog("Note", null, ActivityTypes.Inactivate, null);
			}
			return num;
		}

		public bool SetAlarm(object id, bool isAlarm)
		{
			return UpdateTableRowByID("Notes", "NoteID", "IsAlarm", id, Convert.ToByte(isAlarm));
		}

		public bool SetAlarm(object id, bool isAlarm, DateTime nextReminderDate)
		{
			string exp = "UPDATE Notes SET IsAlarm=" + Convert.ToByte(isAlarm) + ", ReminderDate='" + nextReminderDate.ToString(StoreConfiguration.CurrentCulture) + "' WHERE NoteID=" + id.ToString();
			return ExecuteNonQuery(exp) > 0;
		}

		public bool ClearAllNoteUsers(int notID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DELETE FROM ").Append("[Note Users]");
			stringBuilder.Append(" WHERE ").Append("NoteID").Append("=")
				.Append(notID.ToString());
			return ExecuteNonQuery(stringBuilder.ToString()) > 0;
		}
	}
}
