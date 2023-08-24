using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CustomFields : StoreObject
	{
		private const string CUSTOMFIELDSETUPID_PARM = "@CustomFieldSetupID";

		private const string CUSTOMFIELDID_PARM = "@CustomFieldID";

		private const string RECORDID_PARM = "@RecordID";

		private const string STRING1_PARM = "@String1";

		private const string STRING2_PARM = "@String2";

		private const string STRING3_PARM = "@String3";

		private const string STRING4_PARM = "@String4";

		private const string STRING5_PARM = "@String5";

		private const string STRING6_PARM = "@String6";

		private const string STRING7_PARM = "@String7";

		private const string STRING8_PARM = "@String8";

		private const string STRING9_PARM = "@String9";

		private const string STRING10_PARM = "@String10";

		private const string MEMO1_PARM = "@Memo1";

		private const string MEMO2_PARM = "@Memo2";

		private const string MEMO3_PARM = "@Memo3";

		private const string MEMO4_PARM = "@Memo4";

		private const string DECIMAL1_PARM = "@Decimal1";

		private const string DECIMAL2_PARM = "@Decimal2";

		private const string DECIMAL3_PARM = "@Decimal3";

		private const string DECIMAL4_PARM = "@Decimal4";

		private const string DECIMAL5_PARM = "@Decimal5";

		private const string DECIMAL6_PARM = "@Decimal6";

		private const string BOOL1_PARM = "@Bool1";

		private const string BOOL2_PARM = "@Bool2";

		private const string BOOL3_PARM = "@Bool3";

		private const string BOOL4_PARM = "@Bool4";

		private const string BOOL5_PARM = "@Bool5";

		private const string BOOL6_PARM = "@Bool6";

		private const string DATETIME1_PARM = "@DateTime1";

		private const string DATETIME2_PARM = "@DateTime2";

		private const string DATETIME3_PARM = "@DateTime3";

		private const string DATETIME4_PARM = "@DateTime4";

		private const string DATETIME5_PARM = "@DateTime5";

		private const string DATETIME6_PARM = "@DateTime6";

		private const string COMBO1_PARM = "@Combo1";

		private const string COMBO2_PARM = "@Combo2";

		private const string COMBO3_PARM = "@Combo3";

		private const string COMBO4_PARM = "@Combo4";

		private const string COMBO5_PARM = "@Combo5";

		private const string COMBO6_PARM = "@Combo6";

		private const string COMBO7_PARM = "@Combo7";

		private const string COMBO8_PARM = "@Combo8";

		private const string COMBO9_PARM = "@Combo9";

		private const string COMBO10_PARM = "@Combo10";

		public CustomFields(Config config)
			: base(config)
		{
		}

		private string GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Custom Fields]", new FieldValue("CustomFieldSetupID", "@CustomFieldSetupID"), new FieldValue("RecordID", "@RecordID"), new FieldValue("String1", "@String1"), new FieldValue("String2", "@String2"), new FieldValue("String3", "@String3"), new FieldValue("String4", "@String4"), new FieldValue("String5", "@String5"), new FieldValue("String6", "@String6"), new FieldValue("String7", "@String7"), new FieldValue("String8", "@String8"), new FieldValue("String9", "@String9"), new FieldValue("String10", "@String10"), new FieldValue("Memo1", "@Memo1"), new FieldValue("Memo2", "@Memo2"), new FieldValue("Memo3", "@Memo3"), new FieldValue("Memo4", "@Memo4"), new FieldValue("Decimal1", "@Decimal1"), new FieldValue("Decimal2", "@Decimal2"), new FieldValue("Decimal3", "@Decimal3"), new FieldValue("Decimal4", "@Decimal4"), new FieldValue("Decimal5", "@Decimal5"), new FieldValue("Decimal6", "@Decimal6"), new FieldValue("Bool1", "@Bool1"), new FieldValue("Bool2", "@Bool2"), new FieldValue("Bool3", "@Bool3"), new FieldValue("Bool4", "@Bool4"), new FieldValue("Bool5", "@Bool5"), new FieldValue("Bool6", "@Bool6"), new FieldValue("DateTime1", "@DateTime1"), new FieldValue("DateTime2", "@DateTime2"), new FieldValue("DateTime3", "@DateTime3"), new FieldValue("DateTime4", "@DateTime4"), new FieldValue("DateTime5", "@DateTime5"), new FieldValue("DateTime6", "@DateTime6"), new FieldValue("Combo1", "@Combo1"), new FieldValue("Combo2", "@Combo2"), new FieldValue("Combo3", "@Combo3"), new FieldValue("Combo4", "@Combo5"), new FieldValue("Combo5", "@Combo5"), new FieldValue("Combo6", "@Combo6"), new FieldValue("Combo7", "@Combo7"), new FieldValue("Combo8", "@Combo8"), new FieldValue("Combo9", "@Combo9"), new FieldValue("Combo10", "@Combo10"));
			return sqlBuilder.GetInsertExpression();
		}

		private string GetUpdateText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Custom Fields]", new FieldValue("CustomFieldID", "@CustomFieldID", isUpdateConditionField: true), new FieldValue("String1", "@String1"), new FieldValue("String2", "@String2"), new FieldValue("String3", "@String3"), new FieldValue("String4", "@String4"), new FieldValue("String5", "@String5"), new FieldValue("String6", "@String6"), new FieldValue("String7", "@String7"), new FieldValue("String8", "@String8"), new FieldValue("String9", "@String9"), new FieldValue("String10", "@String10"), new FieldValue("Memo1", "@Memo1"), new FieldValue("Memo2", "@Memo2"), new FieldValue("Memo3", "@Memo3"), new FieldValue("Memo4", "@Memo4"), new FieldValue("Decimal1", "@Decimal1"), new FieldValue("Decimal2", "@Decimal2"), new FieldValue("Decimal3", "@Decimal3"), new FieldValue("Decimal4", "@Decimal4"), new FieldValue("Decimal5", "@Decimal5"), new FieldValue("Decimal6", "@Decimal6"), new FieldValue("Bool1", "@Bool1"), new FieldValue("Bool2", "@Bool2"), new FieldValue("Bool3", "@Bool3"), new FieldValue("Bool4", "@Bool4"), new FieldValue("Bool5", "@Bool5"), new FieldValue("Bool6", "@Bool6"), new FieldValue("DateTime1", "@DateTime1"), new FieldValue("DateTime2", "@DateTime2"), new FieldValue("DateTime3", "@DateTime3"), new FieldValue("DateTime4", "@DateTime4"), new FieldValue("DateTime5", "@DateTime5"), new FieldValue("DateTime6", "@DateTime6"), new FieldValue("Combo1", "@Combo1"), new FieldValue("Combo2", "@Combo2"), new FieldValue("Combo3", "@Combo3"), new FieldValue("Combo4", "@Combo5"), new FieldValue("Combo5", "@Combo5"), new FieldValue("Combo6", "@Combo6"), new FieldValue("Combo7", "@Combo7"), new FieldValue("Combo8", "@Combo8"), new FieldValue("Combo9", "@Combo9"), new FieldValue("Combo10", "@Combo10"));
			return sqlBuilder.GetUpdateExpression();
		}

		private SqlCommand GetInsertCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@CustomFieldSetupID", SqlDbType.Int);
				parameters.Add("@RecordID", SqlDbType.Int);
				parameters.Add("@String1", SqlDbType.NVarChar);
				parameters.Add("@String2", SqlDbType.NVarChar);
				parameters.Add("@String3", SqlDbType.NVarChar);
				parameters.Add("@String4", SqlDbType.NVarChar);
				parameters.Add("@String5", SqlDbType.NVarChar);
				parameters.Add("@String6", SqlDbType.NVarChar);
				parameters.Add("@String7", SqlDbType.NVarChar);
				parameters.Add("@String8", SqlDbType.NVarChar);
				parameters.Add("@String9", SqlDbType.NVarChar);
				parameters.Add("@String10", SqlDbType.NVarChar);
				parameters.Add("@Memo1", SqlDbType.NText);
				parameters.Add("@Memo2", SqlDbType.NText);
				parameters.Add("@Memo3", SqlDbType.NText);
				parameters.Add("@Memo4", SqlDbType.NText);
				parameters.Add("@Decimal1", SqlDbType.Money);
				parameters.Add("@Decimal2", SqlDbType.Money);
				parameters.Add("@Decimal3", SqlDbType.Money);
				parameters.Add("@Decimal4", SqlDbType.Money);
				parameters.Add("@Decimal5", SqlDbType.Money);
				parameters.Add("@Decimal6", SqlDbType.Money);
				parameters.Add("@Bool1", SqlDbType.Bit);
				parameters.Add("@Bool2", SqlDbType.Bit);
				parameters.Add("@Bool3", SqlDbType.Bit);
				parameters.Add("@Bool4", SqlDbType.Bit);
				parameters.Add("@Bool5", SqlDbType.Bit);
				parameters.Add("@Bool6", SqlDbType.Bit);
				parameters.Add("@DateTime1", SqlDbType.DateTime);
				parameters.Add("@DateTime2", SqlDbType.DateTime);
				parameters.Add("@DateTime3", SqlDbType.DateTime);
				parameters.Add("@DateTime4", SqlDbType.DateTime);
				parameters.Add("@DateTime5", SqlDbType.DateTime);
				parameters.Add("@DateTime6", SqlDbType.DateTime);
				parameters.Add("@Combo1", SqlDbType.Int);
				parameters.Add("@Combo2", SqlDbType.Int);
				parameters.Add("@Combo3", SqlDbType.Int);
				parameters.Add("@Combo4", SqlDbType.Int);
				parameters.Add("@Combo5", SqlDbType.Int);
				parameters.Add("@Combo6", SqlDbType.Int);
				parameters.Add("@Combo7", SqlDbType.Int);
				parameters.Add("@Combo8", SqlDbType.Int);
				parameters.Add("@Combo9", SqlDbType.Int);
				parameters.Add("@Combo10", SqlDbType.Int);
				parameters["@CustomFieldSetupID"].SourceColumn = "CustomFieldSetupID";
				parameters["@RecordID"].SourceColumn = "RecordID";
				parameters["@String1"].SourceColumn = "String1";
				parameters["@String2"].SourceColumn = "String2";
				parameters["@String3"].SourceColumn = "String3";
				parameters["@String4"].SourceColumn = "String4";
				parameters["@String5"].SourceColumn = "String5";
				parameters["@String6"].SourceColumn = "String6";
				parameters["@String7"].SourceColumn = "String7";
				parameters["@String8"].SourceColumn = "String8";
				parameters["@String9"].SourceColumn = "String9";
				parameters["@String10"].SourceColumn = "String10";
				parameters["@Memo1"].SourceColumn = "Memo1";
				parameters["@Memo2"].SourceColumn = "Memo2";
				parameters["@Memo3"].SourceColumn = "Memo3";
				parameters["@Memo4"].SourceColumn = "Memo4";
				parameters["@Decimal1"].SourceColumn = "Decimal1";
				parameters["@Decimal2"].SourceColumn = "Decimal2";
				parameters["@Decimal3"].SourceColumn = "Decimal3";
				parameters["@Decimal4"].SourceColumn = "Decimal4";
				parameters["@Decimal5"].SourceColumn = "Decimal5";
				parameters["@Decimal6"].SourceColumn = "Decimal6";
				parameters["@Bool1"].SourceColumn = "Bool1";
				parameters["@Bool2"].SourceColumn = "Bool2";
				parameters["@Bool3"].SourceColumn = "Bool3";
				parameters["@Bool4"].SourceColumn = "Bool4";
				parameters["@Bool5"].SourceColumn = "Bool5";
				parameters["@Bool6"].SourceColumn = "Bool6";
				parameters["@DateTime1"].SourceColumn = "DateTime1";
				parameters["@DateTime2"].SourceColumn = "DateTime2";
				parameters["@DateTime3"].SourceColumn = "DateTime3";
				parameters["@DateTime4"].SourceColumn = "DateTime4";
				parameters["@DateTime5"].SourceColumn = "DateTime5";
				parameters["@DateTime6"].SourceColumn = "DateTime6";
				parameters["@Combo1"].SourceColumn = "Combo1";
				parameters["@Combo2"].SourceColumn = "Combo2";
				parameters["@Combo3"].SourceColumn = "Combo3";
				parameters["@Combo4"].SourceColumn = "Combo4";
				parameters["@Combo5"].SourceColumn = "Combo5";
				parameters["@Combo6"].SourceColumn = "Combo6";
				parameters["@Combo7"].SourceColumn = "Combo7";
				parameters["@Combo8"].SourceColumn = "Combo8";
				parameters["@Combo9"].SourceColumn = "Combo9";
				parameters["@Combo10"].SourceColumn = "Combo10";
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
				parameters.Add("@CustomFieldID", SqlDbType.Int);
				parameters.Add("@String1", SqlDbType.NVarChar);
				parameters.Add("@String2", SqlDbType.NVarChar);
				parameters.Add("@String3", SqlDbType.NVarChar);
				parameters.Add("@String4", SqlDbType.NVarChar);
				parameters.Add("@String5", SqlDbType.NVarChar);
				parameters.Add("@String6", SqlDbType.NVarChar);
				parameters.Add("@String7", SqlDbType.NVarChar);
				parameters.Add("@String8", SqlDbType.NVarChar);
				parameters.Add("@String9", SqlDbType.NVarChar);
				parameters.Add("@String10", SqlDbType.NVarChar);
				parameters.Add("@Memo1", SqlDbType.NText);
				parameters.Add("@Memo2", SqlDbType.NText);
				parameters.Add("@Memo3", SqlDbType.NText);
				parameters.Add("@Memo4", SqlDbType.NText);
				parameters.Add("@Decimal1", SqlDbType.Money);
				parameters.Add("@Decimal2", SqlDbType.Money);
				parameters.Add("@Decimal3", SqlDbType.Money);
				parameters.Add("@Decimal4", SqlDbType.Money);
				parameters.Add("@Decimal5", SqlDbType.Money);
				parameters.Add("@Decimal6", SqlDbType.Money);
				parameters.Add("@Bool1", SqlDbType.Bit);
				parameters.Add("@Bool2", SqlDbType.Bit);
				parameters.Add("@Bool3", SqlDbType.Bit);
				parameters.Add("@Bool4", SqlDbType.Bit);
				parameters.Add("@Bool5", SqlDbType.Bit);
				parameters.Add("@Bool6", SqlDbType.Bit);
				parameters.Add("@DateTime1", SqlDbType.DateTime);
				parameters.Add("@DateTime2", SqlDbType.DateTime);
				parameters.Add("@DateTime3", SqlDbType.DateTime);
				parameters.Add("@DateTime4", SqlDbType.DateTime);
				parameters.Add("@DateTime5", SqlDbType.DateTime);
				parameters.Add("@DateTime6", SqlDbType.DateTime);
				parameters.Add("@Combo1", SqlDbType.Int);
				parameters.Add("@Combo2", SqlDbType.Int);
				parameters.Add("@Combo3", SqlDbType.Int);
				parameters.Add("@Combo4", SqlDbType.Int);
				parameters.Add("@Combo5", SqlDbType.Int);
				parameters.Add("@Combo6", SqlDbType.Int);
				parameters.Add("@Combo7", SqlDbType.Int);
				parameters.Add("@Combo8", SqlDbType.Int);
				parameters.Add("@Combo9", SqlDbType.Int);
				parameters.Add("@Combo10", SqlDbType.Int);
				parameters["@CustomFieldID"].SourceColumn = "CustomFieldID";
				parameters["@String1"].SourceColumn = "String1";
				parameters["@String2"].SourceColumn = "String2";
				parameters["@String3"].SourceColumn = "String3";
				parameters["@String4"].SourceColumn = "String4";
				parameters["@String5"].SourceColumn = "String5";
				parameters["@String6"].SourceColumn = "String6";
				parameters["@String7"].SourceColumn = "String7";
				parameters["@String8"].SourceColumn = "String8";
				parameters["@String9"].SourceColumn = "String9";
				parameters["@String10"].SourceColumn = "String10";
				parameters["@Memo1"].SourceColumn = "Memo1";
				parameters["@Memo2"].SourceColumn = "Memo2";
				parameters["@Memo3"].SourceColumn = "Memo3";
				parameters["@Memo4"].SourceColumn = "Memo4";
				parameters["@Decimal1"].SourceColumn = "Decimal1";
				parameters["@Decimal2"].SourceColumn = "Decimal2";
				parameters["@Decimal3"].SourceColumn = "Decimal3";
				parameters["@Decimal4"].SourceColumn = "Decimal4";
				parameters["@Decimal5"].SourceColumn = "Decimal5";
				parameters["@Decimal6"].SourceColumn = "Decimal6";
				parameters["@Bool1"].SourceColumn = "Bool1";
				parameters["@Bool2"].SourceColumn = "Bool2";
				parameters["@Bool3"].SourceColumn = "Bool3";
				parameters["@Bool4"].SourceColumn = "Bool4";
				parameters["@Bool5"].SourceColumn = "Bool5";
				parameters["@Bool6"].SourceColumn = "Bool6";
				parameters["@DateTime1"].SourceColumn = "DateTime1";
				parameters["@DateTime2"].SourceColumn = "DateTime2";
				parameters["@DateTime3"].SourceColumn = "DateTime3";
				parameters["@DateTime4"].SourceColumn = "DateTime4";
				parameters["@DateTime5"].SourceColumn = "DateTime5";
				parameters["@DateTime6"].SourceColumn = "DateTime6";
				parameters["@Combo1"].SourceColumn = "Combo1";
				parameters["@Combo2"].SourceColumn = "Combo2";
				parameters["@Combo3"].SourceColumn = "Combo3";
				parameters["@Combo4"].SourceColumn = "Combo4";
				parameters["@Combo5"].SourceColumn = "Combo5";
				parameters["@Combo6"].SourceColumn = "Combo6";
				parameters["@Combo7"].SourceColumn = "Combo7";
				parameters["@Combo8"].SourceColumn = "Combo8";
				parameters["@Combo9"].SourceColumn = "Combo9";
				parameters["@Combo10"].SourceColumn = "Combo10";
			}
			return updateCommand;
		}

		private bool InsertCustomField(CustomFieldData customFieldData)
		{
			bool flag = true;
			SqlCommand insertCommand = GetInsertCommand();
			try
			{
				insertCommand.Transaction = base.DBConfig.SqlTransaction;
				return Insert(customFieldData, "[Custom Fields]", insertCommand);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		private bool UpdateCustomField(CustomFieldData customFieldData)
		{
			bool flag = true;
			SqlCommand updateCommand = GetUpdateCommand();
			try
			{
				updateCommand.Transaction = base.DBConfig.SqlTransaction;
				return Update(customFieldData, "[Custom Fields]", updateCommand);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool InsertUpdateCustomField(CustomFieldData customFieldData, string tableName, object recordID)
		{
			return InsertUpdateCustomField(customFieldData, tableName, CustomTypes.NONE, recordID);
		}

		internal bool InsertUpdateCustomField(CustomFieldData customFieldData, string tableName, CustomTypes cutomType, object recordID)
		{
			if (customFieldData == null)
			{
				return true;
			}
			if (customFieldData.CustomFieldTable.Rows.Count != 1)
			{
				return true;
			}
			if (recordID == null)
			{
				throw new ApplicationException("RecordID cannot be null.");
			}
			int setupIDByTableName = new CustomFieldSetups(base.DBConfig).GetSetupIDByTableName(tableName, cutomType);
			if (setupIDByTableName == -1)
			{
				throw new ApplicationException("Custom setup id for table " + tableName + " not found.");
			}
			CustomFieldData customFieldData2 = new CustomFieldData();
			foreach (DataRow row in customFieldData.CustomFieldTable.Rows)
			{
				DataRow dataRow2 = customFieldData2.CustomFieldTable.NewRow();
				foreach (DataColumn column in customFieldData.CustomFieldTable.Columns)
				{
					dataRow2[column.ColumnName] = row[column.ColumnName];
				}
				customFieldData2.CustomFieldTable.Rows.Add(dataRow2);
			}
			int customFieldIDBySetupIDAndRecordID = GetCustomFieldIDBySetupIDAndRecordID(setupIDByTableName, recordID);
			DataRow dataRow3 = customFieldData2.CustomFieldTable.Rows[0];
			dataRow3["CustomFieldSetupID"] = setupIDByTableName;
			dataRow3["RecordID"] = recordID;
			if (customFieldIDBySetupIDAndRecordID != -1)
			{
				customFieldData2.CustomFieldTable.AcceptChanges();
				dataRow3["CustomFieldID"] = customFieldIDBySetupIDAndRecordID;
				return UpdateCustomField(customFieldData2);
			}
			return InsertCustomField(customFieldData2);
		}

		internal bool ExistRecord(object setupID, object recordID)
		{
			try
			{
				return IsTableFieldValueExist("[Custom Fields]", "CustomFieldSetupID", "RecordID", setupID, recordID);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteRecord(string tableName, object recordID)
		{
			return DeleteRecord(tableName, CustomTypes.NONE, recordID);
		}

		internal bool DeleteRecord(string tableName, CustomTypes cutomType, object recordID)
		{
			int setupIDByTableName = new CustomFieldSetups(base.DBConfig).GetSetupIDByTableName(tableName, cutomType);
			if (setupIDByTableName == -1)
			{
				return true;
			}
			return DeleteCustomRecord(setupIDByTableName, recordID);
		}

		private bool DeleteCustomRecord(object setupID, object recordID)
		{
			string exp = "DELETE FROM [Custom Fields] WHERE CustomFieldSetupID=" + setupID.ToString() + " AND RecordID=" + recordID.ToString();
			return ExecuteNonQuery(exp) > 0;
		}

		internal int GetCustomFieldIDBySetupIDAndRecordID(object setupID, object recordID)
		{
			object obj = ExecuteSelectScalar("[Custom Fields]", "CustomFieldSetupID", "RecordID", setupID, recordID, "CustomFieldID", base.DBConfig.SqlTransaction);
			if (obj != null && obj != DBNull.Value)
			{
				return int.Parse(obj.ToString());
			}
			return -1;
		}

		public CustomFieldData GetCustomField(string tableName, object recordID)
		{
			return GetCustomField(tableName, CustomTypes.NONE, recordID);
		}

		public CustomFieldData GetCustomField(string tableName, CustomTypes cutomType, object recordID)
		{
			int setupIDByTableName = new CustomFieldSetups(base.DBConfig).GetSetupIDByTableName(tableName, cutomType);
			if (setupIDByTableName == -1)
			{
				return null;
			}
			int customFieldIDBySetupIDAndRecordID = GetCustomFieldIDBySetupIDAndRecordID(setupIDByTableName, recordID);
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CustomFieldID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = customFieldIDBySetupIDAndRecordID;
			commandHelper.TableName = "[Custom Fields]";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CustomFieldData customFieldData = new CustomFieldData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customFieldData, "[Custom Fields]", sqlBuilder);
			if (customFieldData.CustomFieldTable.Rows.Count > 0)
			{
				for (int i = 0; i < customFieldData.CustomFieldTable.Columns.Count; i++)
				{
					DataColumn dataColumn = customFieldData.CustomFieldTable.Columns[i];
					if (customFieldData.CustomFieldTable.Rows[0][dataColumn.ColumnName] == DBNull.Value)
					{
						customFieldData.CustomFieldTable.Columns.Remove(dataColumn);
						i--;
					}
				}
			}
			return customFieldData;
		}
	}
}
