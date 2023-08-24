using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class UDF : StoreObject
	{
		private const string ENTITYID_PARM = "@EntityID";

		private const string ENTITYID1_PARM = "@EntityID1";

		private const string ENTITYID2_PARM = "@EntityID2";

		private const string ENTITYID3_PARM = "@EntityID3";

		public string UDF_TABLE = "UDF_Setup";

		private const string UDFTABLENAME_PARM = "@UDFTableName";

		private const string FORMNAME_PARM = "@FormName";

		private const string FIELDNAME_PARM = "@FieldName";

		private const string DATATYPE_PARM = "@DataType";

		private const string FIELDSIZE_PARM = "@FieldSize";

		private const string DISPLAYNAME_PARM = "@DisplayName";

		private const string LISTTYPE_PARM = "@ListType";

		private const string UDLISTNAME_PARM = "@UDListName";

		private const string CREATEDBY_PARM = "CreatedBy";

		private const string DATECREATED_PARM = "DateCreated";

		private const string UPDATEDBY_PARM = "UpdatedBy";

		private const string DATEUPDATED_PARM = "DateUpdated";

		public UDF(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateUDFSetupText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("UDF_Setup", new FieldValue("UDFTableName", "@UDFTableName", isUpdateConditionField: true), new FieldValue("FieldName", "@FieldName"), new FieldValue("FormName", "@FormName"), new FieldValue("DataType", "@DataType"), new FieldValue("FieldSize", "@FieldSize"), new FieldValue("UDListName", "@UDListName"), new FieldValue("ListType", "@ListType"), new FieldValue("DisplayName", "@DisplayName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("UDF_Setup", new FieldValue("DateUpdated", "DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateUDFSetupCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateUDFSetupText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateUDFSetupText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@UDFTableName", SqlDbType.NVarChar);
			parameters.Add("@FieldName", SqlDbType.NVarChar);
			parameters.Add("@FormName", SqlDbType.NVarChar);
			parameters.Add("@DataType", SqlDbType.TinyInt);
			parameters.Add("@FieldSize", SqlDbType.TinyInt);
			parameters.Add("@DisplayName", SqlDbType.NVarChar);
			parameters.Add("@ListType", SqlDbType.NVarChar);
			parameters.Add("@UDListName", SqlDbType.NVarChar);
			parameters["@UDFTableName"].SourceColumn = "UDFTableName";
			parameters["@FieldName"].SourceColumn = "FieldName";
			parameters["@FormName"].SourceColumn = "FormName";
			parameters["@DataType"].SourceColumn = "DataType";
			parameters["@FieldSize"].SourceColumn = "FieldSize";
			parameters["@UDListName"].SourceColumn = "UDListName";
			parameters["@ListType"].SourceColumn = "ListType";
			parameters["@DisplayName"].SourceColumn = "DisplayName";
			if (isUpdate)
			{
				parameters.Add("DateUpdated", SqlDbType.DateTime);
				parameters["DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEntityUDFText(bool isUpdate, string tableName, DataSet columnData, string idColumn1, string idColumn2, string idColumn3)
		{
			string tableName2 = "UDF_" + tableName;
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters(tableName2, new FieldValue("EntityID1", "@EntityID1", isUpdateConditionField: true));
			if (idColumn2 != "")
			{
				sqlBuilder.AddInsertUpdateParameters(tableName2, new FieldValue("EntityID2", "@EntityID2", isUpdateConditionField: true));
			}
			if (idColumn3 != "")
			{
				sqlBuilder.AddInsertUpdateParameters(tableName2, new FieldValue("EntityID3", "@EntityID3", isUpdateConditionField: true));
			}
			foreach (DataRow row in columnData.Tables["UDF_Setup"].Rows)
			{
				string text = row["FieldName"].ToString();
				if (!(text.ToLower() == "entityid1") && !(text.ToLower() == "entityid2") && !(text.ToLower() == "entityid3"))
				{
					sqlBuilder.AddInsertUpdateParameters(tableName2, new FieldValue(text, "@" + text));
				}
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		public SqlCommand GetInsertUpdateEntityUDFCommand(bool isUpdate, string tableName, string idColumn1, string idColumn2, string idColumn3)
		{
			DataSet uDFList = GetUDFList(tableName);
			_ = "UDF_" + tableName;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateEntityUDFText(isUpdate, tableName, uDFList, idColumn1, idColumn2, idColumn3), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateEntityUDFText(isUpdate, tableName, uDFList, idColumn1, idColumn2, idColumn3), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@EntityID1", SqlDbType.NVarChar);
			parameters["@EntityID1"].SourceColumn = idColumn1;
			if (idColumn2 != "")
			{
				parameters.Add("@EntityID2", SqlDbType.NVarChar);
				parameters["@EntityID2"].SourceColumn = idColumn2;
			}
			if (idColumn3 != "")
			{
				parameters.Add("@EntityID3", SqlDbType.NVarChar);
				parameters["@EntityID3"].SourceColumn = idColumn3;
			}
			foreach (DataRow row in uDFList.Tables["UDF_Setup"].Rows)
			{
				string text = row["FieldName"].ToString();
				if (!(text.ToLower() == "entityid1") && !(text.ToLower() == "entityid2") && !(text.ToLower() == "entityid3"))
				{
					string parameterName = "@" + text;
					string a = row["DataType"].ToString();
					string a2 = row["FieldSize"].ToString();
					if (a == 1.ToString())
					{
						parameters.Add(parameterName, SqlDbType.NVarChar);
					}
					else if (a == 7.ToString())
					{
						parameters.Add(parameterName, SqlDbType.Bit);
					}
					else if (a == 4.ToString())
					{
						parameters.Add(parameterName, SqlDbType.DateTime);
					}
					else if (a == 5.ToString())
					{
						parameters.Add(parameterName, SqlDbType.DateTime);
					}
					else if (a == 6.ToString())
					{
						if (a2 == "1")
						{
							parameters.Add(parameterName, SqlDbType.Decimal);
						}
						else if (a2 == "2")
						{
							parameters.Add(parameterName, SqlDbType.Money);
						}
						else
						{
							parameters.Add(parameterName, SqlDbType.Int);
						}
					}
					else if (a == 3.ToString())
					{
						parameters.Add(parameterName, SqlDbType.NText);
					}
					else if (a == 2.ToString())
					{
						parameters.Add(parameterName, SqlDbType.NVarChar);
					}
					parameters[parameterName].SourceColumn = text;
				}
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateUDFColumn(UDFData udfData, bool isUpdate)
		{
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				DataRow dataRow = udfData.UDFTable.Rows[0];
				sqlTransaction = base.DBConfig.StartNewTransaction();
				SqlCommand insertUpdateUDFSetupCommand = GetInsertUpdateUDFSetupCommand(isUpdate);
				insertUpdateUDFSetupCommand.Transaction = sqlTransaction;
				flag = ((!isUpdate) ? (flag & Insert(udfData, "UDF_Setup", insertUpdateUDFSetupCommand)) : (flag & Update(udfData, "UDF_Setup", insertUpdateUDFSetupCommand)));
				string text = dataRow["UDFTableName"].ToString();
				string text2 = dataRow["FieldName"].ToString();
				UDFDataTypes uDFDataTypes = (UDFDataTypes)byte.Parse(dataRow["DataType"].ToString());
				int num = 0;
				if (!dataRow["FieldSize"].IsDBNullOrEmpty())
				{
					num = int.Parse(dataRow["FieldSize"].ToString());
				}
				string exp = "select COUNT(*) FROM sys.tables WHERE Name = 'UDF_" + text + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (!obj.IsNullOrEmpty() && int.Parse(obj.ToString()) == 0)
				{
					exp = " CREATE TABLE UDF_" + text + "(EntityID1 nvarchar(64), EntityID2 nvarchar(64))";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) >= -1);
					if (!flag)
					{
						throw new CompanyException("Cannot create custom field table. Please make sure you have access right.");
					}
				}
				exp = "SELECT COUNT(*) FROM sys.columns\r\n                                WHERE Name = N'" + text2 + "' AND OBJECT_ID = OBJECT_ID(N'UDF_" + text + "')";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
				{
					throw new CompanyException("Column Name is already exist. Column name must be unique.", 1066);
				}
				SqlDbType sqlDbType = SqlDbType.Int;
				switch (uDFDataTypes)
				{
				case UDFDataTypes.Date:
					sqlDbType = SqlDbType.DateTime;
					break;
				case UDFDataTypes.CheckBox:
					sqlDbType = SqlDbType.Bit;
					break;
				case UDFDataTypes.Text:
					sqlDbType = SqlDbType.NVarChar;
					break;
				case UDFDataTypes.List:
					sqlDbType = SqlDbType.NVarChar;
					num = 64;
					break;
				case UDFDataTypes.Note:
					sqlDbType = SqlDbType.NText;
					break;
				case UDFDataTypes.Time:
					sqlDbType = SqlDbType.DateTime;
					break;
				case UDFDataTypes.Number:
					switch (num)
					{
					case 1:
						sqlDbType = SqlDbType.Decimal;
						break;
					case 2:
						sqlDbType = SqlDbType.Money;
						break;
					default:
						sqlDbType = SqlDbType.Int;
						break;
					}
					break;
				}
				exp = "ALTER TABLE UDF_" + text + " ADD " + text2 + " " + sqlDbType.ToString();
				switch (sqlDbType)
				{
				case SqlDbType.NVarChar:
					exp = exp + "(" + num.ToString() + ")";
					break;
				case SqlDbType.Decimal:
					exp += " (18,5)";
					break;
				}
				int num2 = ExecuteNonQuery(exp, sqlTransaction);
				flag = (flag && num2 >= -1);
				if (!flag)
				{
					throw new CompanyException("Cannot create custom field table. Please make sure you have access right.");
				}
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

		public UDFData GetUDF()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("UDF_Setup");
			UDFData uDFData = new UDFData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(uDFData, "UDF_Setup", sqlBuilder);
			return uDFData;
		}

		public bool DeleteUDF(string tableName, string entityID, SqlTransaction sqlTransaction)
		{
			try
			{
				string commandText = "DELETE FROM " + tableName + " WHERE EntityID = '" + entityID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteUDFColumn(string columnName, string udfTable)
		{
			bool flag = true;
			try
			{
				string uDFTableName = UDFData.GetUDFTableName(udfTable);
				string exp = "ALTER TABLE " + uDFTableName + " DROP COLUMN " + columnName;
				ExecuteNonQuery(exp);
				string exp2 = "DELETE FROM UDF_Setup WHERE UDFTableName = '" + udfTable + "' AND FieldName = '" + columnName + "'";
				return flag & (ExecuteNonQuery(exp2) > 0);
			}
			catch
			{
				throw;
			}
		}

		public bool UpdateUDFDisplayName(string columnName, string udfTable, string displayName)
		{
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				UDFData.GetUDFTableName(udfTable);
				string exp = "UPDATE UDF_Setup SET DisplayName = '" + displayName + "' WHERE UDFTableName = '" + udfTable + "'  AND FieldName = '" + columnName + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
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

		public bool UpdateUDFColumnDataType(string columnName, string udfTable, SqlDbType newDataType, int length)
		{
			bool result = true;
			try
			{
				string uDFTableName = UDFData.GetUDFTableName(udfTable);
				string text = "ALTER TABLE " + uDFTableName + " Alter COLUMN " + columnName + " " + newDataType;
				if (newDataType == SqlDbType.NVarChar)
				{
					text = text + "(" + length.ToString() + ")";
				}
				ExecuteNonQuery(text);
				return result;
			}
			catch
			{
				throw;
			}
		}

		public UDFData GetUDFByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "EntityID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "UDF_Setup";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			UDFData uDFData = new UDFData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(uDFData, "UDF_Setup", sqlBuilder);
			return uDFData;
		}

		public DataSet GetUDFByFields(params string[] columns)
		{
			return GetUDFByFields(null, isInactive: true, columns);
		}

		public DataSet GetUDFByFields(string[] udfID, params string[] columns)
		{
			return GetUDFByFields(udfID, isInactive: true, columns);
		}

		public DataSet GetUDFByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("UDF_Setup");
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
				commandHelper.FieldName = "EntityID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "UDF_Setup";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "UDF_Setup", sqlBuilder);
			return dataSet;
		}

		public DataSet GetUDFList(string udfTableName)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT * FROM  UDF_Setup  WHERE UDFTableName = '" + udfTableName + "' ";
			FillDataSet(dataSet, "UDF_Setup", textCommand);
			return dataSet;
		}

		public bool InsertUpdateEntityUDFData(DataSet dataSet, string tableName, string entityID1Name, string entityID1Value, bool isUpdate)
		{
			try
			{
				return InsertUpdateEntityUDFData(dataSet, tableName, entityID1Name, entityID1Value, "", "", "", "", isUpdate);
			}
			catch
			{
				throw;
			}
		}

		public bool InsertUpdateEntityUDFData(DataSet dataSet, string tableName, string entityID1Name, string entityID1Value, string entityID2Name, string entityID2Value, bool isUpdate)
		{
			try
			{
				return InsertUpdateEntityUDFData(dataSet, tableName, entityID1Name, entityID1Value, entityID2Name, entityID2Value, "", "", isUpdate);
			}
			catch
			{
				throw;
			}
		}

		public bool InsertUpdateEntityUDFData(DataSet dataSet, string tableName, string entityID1Name, string entityID1Value, string entityID2Name, string entityID2Value, string entityID3Name, string entityID3Value, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateEntityUDFCommand = GetInsertUpdateEntityUDFCommand(isUpdate: false, tableName, entityID1Name, entityID2Name, entityID3Name);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateEntityUDFCommand.Transaction = base.DBConfig.StartNewTransaction());
				if (!isUpdate)
				{
					flag = Insert(dataSet, tableName, insertUpdateEntityUDFCommand);
					return flag;
				}
				string text = "DELETE FROM UDF_" + tableName + " WHERE EntityID1 = '" + entityID1Value + "' ";
				if (entityID2Name != "")
				{
					text = text + " AND EntityID2 = '" + entityID2Value + "'";
					if (entityID3Name != "")
					{
						text = text + " AND EntityID3 = '" + entityID3Value + "'";
					}
				}
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				DataSet dataSet2 = dataSet.Copy();
				foreach (DataRow row in dataSet2.Tables[tableName].Rows)
				{
					row.SetAdded();
				}
				flag = Insert(dataSet2, tableName, insertUpdateEntityUDFCommand);
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

		public DataSet ReadEntityUDFData(DataSet dataSet, string entityName, string entityID1Name, string entityID1Value)
		{
			return ReadEntityUDFData(dataSet, entityName, entityID1Name, entityID1Value, "", "");
		}

		public DataSet ReadEntityUDFData(DataSet dataSet, string tableName, string entityID1Name, string entityID1Value, string entityID2Name, string entityID2Value)
		{
			try
			{
				DataSet dataSet2 = new DataSet();
				string text = "SELECT * FROM UDF_" + tableName + " WHERE EntityID1 = '" + entityID1Value + "' ";
				if (!entityID2Name.IsNullOrEmpty())
				{
					text = text + " AND EntityID2 = '" + entityID2Value + "'";
				}
				FillDataSet(dataSet2, "UDF", text);
				if (dataSet2 == null || dataSet2.Tables.Count == 0)
				{
					return dataSet;
				}
				DataTable dataTable = dataSet2.Tables["UDF"];
				dataTable.Columns["EntityID1"].ColumnName = entityID1Name;
				if (entityID2Name != "")
				{
					dataTable.Columns["EntityID2"].ColumnName = entityID2Name;
					dataTable.PrimaryKey = new DataColumn[2]
					{
						dataTable.Columns[entityID1Name],
						dataTable.Columns[entityID2Name]
					};
				}
				else
				{
					dataTable.Columns.Remove("EntityID2");
					dataTable.PrimaryKey = new DataColumn[1]
					{
						dataTable.Columns[entityID1Name]
					};
				}
				dataSet.Tables[tableName].Merge(dataTable, preserveChanges: false, MissingSchemaAction.Add);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteUDFData(string tableName, string entityID1Name, string entityID1Value, SqlTransaction sqlTransaction)
		{
			try
			{
				return DeleteUDFData(tableName, entityID1Name, entityID1Value, "", "", sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteUDFData(string tableName, string entityID1Name, string entityID1Value, string entityID2Name, string entityID2Value, SqlTransaction sqlTransaction)
		{
			try
			{
				new DataSet();
				string text = "DELETE FROM UDF_" + tableName + " WHERE EntityID1 = '" + entityID1Value + "' ";
				if (!entityID2Name.IsNullOrEmpty())
				{
					text = text + " AND EntityID2 = '" + entityID2Value + "'";
				}
				return ExecuteNonQuery(text, sqlTransaction) >= 0;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetEntityUDFSchema(string tableName, string idColumn1, string idColumn2)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT TOP 0 * FROM UDF_" + tableName;
				FillDataSet(dataSet, "UDF", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0)
				{
					return dataSet;
				}
				DataTable dataTable = dataSet.Tables["UDF"];
				dataTable.Columns["EntityID1"].ColumnName = idColumn1;
				if (idColumn2 != "")
				{
					dataTable.Columns["EntityID2"].ColumnName = idColumn2;
					dataTable.PrimaryKey = new DataColumn[2]
					{
						dataTable.Columns[idColumn1],
						dataTable.Columns[idColumn2]
					};
				}
				else
				{
					dataTable.Columns.Remove("EntityID2");
					dataTable.PrimaryKey = new DataColumn[1]
					{
						dataTable.Columns[idColumn1]
					};
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetUDFEntryFields(string udfTable)
		{
			DataSet dataSet = new DataSet();
			string uDFTableName = UDFData.GetUDFTableName(udfTable);
			string textCommand = "Select * FROM " + uDFTableName;
			FillDataSet(dataSet, "UDF", textCommand);
			textCommand = "SELECT column_name,ISC.TABLE_NAME,ISNULL(DisplayName,COLUMN_NAME) AS DisplayName,ORDINAL_POSITION,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH MaxLength FROM information_schema.columns ISC\r\n\t\t \t\t\t\t\t\t LEFT OUTER JOIN UDF_Setup US ON US.TableName= ISC.Table_Name AND US.FieldName = COLUMN_NAME\r\n                                 WHERE table_name = '" + uDFTableName + "'  AND Column_Name <> 'EntityID' ORDER BY ordinal_position ";
			FillDataSet(dataSet, "UDF_Setup", textCommand);
			return dataSet;
		}

		public DataSet GetUDFComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT UDFID [Code],UDFName [Name]\r\n                           FROM UDF ORDER BY UDFID,UDFName";
			FillDataSet(dataSet, "UDF_Setup", textCommand);
			return dataSet;
		}
	}
}
