using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Shortcut : StoreObject
	{
		private const string SHORTCUTKEY_PARM = "@ShortcutKey";

		private const string SHORTCUTTEXT_PARM = "@ShortcutText";

		private const string SHORTCUTTYPE_PARM = "@ShortcutType";

		private const string USERID_PARM = "@UserID";

		private const string SHORTCUT_TABLE = "Shortcut";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Shortcut(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Shortcut", new FieldValue("ShortcutKey", "@ShortcutKey", isUpdateConditionField: true), new FieldValue("ShortcutText", "@ShortcutText"), new FieldValue("ShortcutType", "@ShortcutType"), new FieldValue("UserID", "@UserID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Shortcut", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ShortcutKey", SqlDbType.NVarChar);
			parameters.Add("@ShortcutText", SqlDbType.NVarChar);
			parameters.Add("@ShortcutType", SqlDbType.NVarChar);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters["@ShortcutKey"].SourceColumn = "ShortcutKey";
			parameters["@ShortcutText"].SourceColumn = "ShortcutText";
			parameters["@ShortcutType"].SourceColumn = "ShortcutType";
			parameters["@UserID"].SourceColumn = "UserID";
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

		public bool InsertShortcut(ShortcutData shortcutData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(shortcutData, "Shortcut", insertUpdateCommand);
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

		public ShortcutData GetShortcuts(byte type, string userID)
		{
			string textCommand = "SELECT * FROM Shortcut WHERE ShortcutType=" + type + " AND UserID='" + userID + "'";
			ShortcutData shortcutData = new ShortcutData();
			FillDataSet(shortcutData, "Shortcut", textCommand);
			return shortcutData;
		}

		public bool DeleteShortcut(byte type, string userID, string key)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Shortcut WHERE ShortcutType = " + type + " AND UserID='" + userID + "' AND ShortcutKey='" + key + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Shortcut", userID, key, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}
	}
}
