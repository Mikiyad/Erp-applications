using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CustomLists : StoreObject
	{
		private const string LISTCODE_PARM = "@ListCode";

		private const string LISTNAME_PARM = "@ListName";

		private const string ITEMCODE_PARM = "@ItemCode";

		private const string ITEMNAME_PARM = "@ItemName";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public CustomLists(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Custom_List", new FieldValue("ListCode", "@ListCode", isUpdateConditionField: true), new FieldValue("ListName", "@ListName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Custom_List", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ListCode", SqlDbType.NVarChar);
			parameters.Add("@ListName", SqlDbType.NVarChar);
			parameters["@ListCode"].SourceColumn = "ListCode";
			parameters["@ListName"].SourceColumn = "ListName";
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

		private string GetInsertItemsText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Custom_List_Items", new FieldValue("ListCode", "@ListCode", isUpdateConditionField: true), new FieldValue("ItemCode", "@ItemCode", isUpdateConditionField: true), new FieldValue("ItemName", "@ItemName"));
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertItemsCommand()
		{
			insertCommand = new SqlCommand(GetInsertItemsText(), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@ListCode", SqlDbType.NVarChar);
			parameters.Add("@ItemCode", SqlDbType.NVarChar);
			parameters.Add("@ItemName", SqlDbType.NVarChar);
			parameters["@ListCode"].SourceColumn = "ListCode";
			parameters["@ItemCode"].SourceColumn = "ItemCode";
			parameters["@ItemName"].SourceColumn = "ItemName";
			return insertCommand;
		}

		public bool InsertUpdateCustomList(DataSet customListData, bool isUpdate)
		{
			if (customListData == null || customListData.Tables.Count == 0 || customListData.Tables[0].Rows.Count == 0)
			{
				throw new Exception("Table must have at least one row");
			}
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				string text = customListData.Tables[0].Rows[0]["ListCode"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Transaction = sqlTransaction;
				string text3 = sqlCommand.CommandText = "DELETE \r\n                           FROM Custom_List_Items WHERE ListCode='" + text + "'";
				ExecuteNonQuery(sqlCommand);
				if (customListData.Tables["Custom_List"].Rows.Count > 0)
				{
					sqlCommand = GetInsertUpdateCommand(isUpdate);
					sqlCommand.Transaction = sqlTransaction;
					flag = ((!isUpdate) ? (flag & Insert(customListData, "Custom_List", sqlCommand)) : (flag & Update(customListData, "Custom_List", sqlCommand)));
				}
				if (customListData.Tables["Custom_List_Items"].Rows.Count > 0)
				{
					sqlCommand = GetInsertItemsCommand();
					sqlCommand.Transaction = sqlTransaction;
					flag &= Insert(customListData, "Custom_List_Items", sqlCommand);
				}
				AddActivityLog("Custom List", text, ActivityTypes.Add, sqlTransaction);
				if (isUpdate)
				{
					UpdateTableRowInsertUpdateInfo("Custom_List", "ListCode", text, sqlTransaction, isInsert: false);
					return flag;
				}
				UpdateTableRowInsertUpdateInfo("Custom_List", "ListCode", text, sqlTransaction, isInsert: true);
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

		public bool DeleteCustomList(string listCode)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Custom_List_Items WHERE ListCode = '" + listCode + "'";
				flag = Delete(commandText, null);
				commandText = "DELETE FROM Custom_List WHERE ListCode = '" + listCode + "'";
				flag &= Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Custom List", listCode, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetCustomListByID(string listCode)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT *\r\n                           FROM Custom_List WHERE ListCode='" + listCode + "'";
			FillDataSet(dataSet, "Custom_List", textCommand);
			textCommand = "SELECT *\r\n                           FROM Custom_List_Items WHERE ListCode='" + listCode + "'";
			FillDataSet(dataSet, "Custom_List_Items", textCommand);
			return dataSet;
		}

		public DataSet GetCustomListCodes()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ListCode,ListName\r\n                           FROM Custom_List  ";
			FillDataSet(dataSet, "Custom_List", textCommand);
			return dataSet;
		}

		public DataSet GetCustomListItems(string listCode)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT *\r\n                           FROM Custom_List_Items WHERE ListCode='" + listCode + "'";
			FillDataSet(dataSet, "Custom_List_Items", textCommand);
			return dataSet;
		}

		public DataSet GetCustomListComboList(string listCode)
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT ItemCode [Code],ItemName [Name],ListCode\r\n                           FROM Custom_List_Items ";
			str = (listCode.IsNullOrEmpty() ? (str + " ORDER BY ListCode,ItemCode,ItemName") : (str + " WHERE ListCode = '" + listCode + "' ORDER BY ItemCode,ItemName"));
			FillDataSet(dataSet, "Custom_List_Items", str);
			return dataSet;
		}
	}
}
