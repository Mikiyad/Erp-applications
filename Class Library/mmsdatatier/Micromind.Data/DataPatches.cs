using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class DataPatches : StoreObject
	{
		private const string PATCHID_PARM = "@PatchID";

		private const string PATCHDESCRIPTION_PARM = "@PatchDescription";

		private const string PATCHQUERY_PARM = "@PatchQuery";

		private const string STATUS_PARM = "@Status";

		private const string DATEEXECUTED_PARM = "@DateExecuted";

		private const string DATAVERSION_PARM = "@DataVersion";

		private const string DATAPATCH_TABLE = "Data_Patch";

		public DataPatches(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Data_Patch", new FieldValue("PatchID", "@PatchID", isUpdateConditionField: true), new FieldValue("PatchDescription", "@PatchDescription"), new FieldValue("PatchQuery", "@PatchQuery"), new FieldValue("DataVersion", "@DataVersion"), new FieldValue("Status", "@Status"), new FieldValue("DateExecuted", "@DateExecuted"));
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
			parameters.Add("@PatchID", SqlDbType.NVarChar);
			parameters.Add("@PatchDescription", SqlDbType.NVarChar);
			parameters.Add("@PatchQuery", SqlDbType.NVarChar);
			parameters.Add("@DataVersion", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@DateExecuted", SqlDbType.DateTime);
			parameters["@PatchID"].SourceColumn = "PatchID";
			parameters["@PatchDescription"].SourceColumn = "PatchDescription";
			parameters["@PatchQuery"].SourceColumn = "PatchQuery";
			parameters["@DataVersion"].SourceColumn = "DataVersion";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@DateExecuted"].SourceColumn = "DateExecuted";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertDataPatch(DataPatchData accountDataPatchData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountDataPatchData, "Data_Patch", insertUpdateCommand);
				string text = accountDataPatchData.DataPatchTable.Rows[0]["PatchID"].ToString();
				AddActivityLog("DataPatch", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Data_Patch", "PatchID", text, sqlTransaction, isInsert: true);
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

		private bool InsertDataPatch(int id, string description, string query)
		{
			try
			{
				DataPatchData dataPatchData = new DataPatchData();
				DataRow dataRow = dataPatchData.DataPatchTable.NewRow();
				dataRow["PatchID"] = id;
				dataRow["PatchDescription"] = description;
				dataRow["PatchQuery"] = query;
				dataPatchData.DataPatchTable.Rows.Add(dataRow);
				return InsertDataPatch(dataPatchData);
			}
			catch
			{
				throw;
			}
		}

		public DataPatchData GetDataPatch()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Data_Patch");
			DataPatchData dataPatchData = new DataPatchData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataPatchData, "Data_Patch", sqlBuilder);
			return dataPatchData;
		}

		public bool DeleteDataPatch(string countryID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Data_Patch WHERE PatchID = '" + countryID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("DataPatch", countryID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DataPatchData GetDataPatchByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "PatchID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Data_Patch";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			DataPatchData dataPatchData = new DataPatchData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataPatchData, "Data_Patch", sqlBuilder);
			return dataPatchData;
		}

		public DataSet GetDataPatchByFields(params string[] columns)
		{
			return GetDataPatchByFields(null, isInactive: true, columns);
		}

		public DataSet GetDataPatchByFields(string[] countryID, params string[] columns)
		{
			return GetDataPatchByFields(countryID, isInactive: true, columns);
		}

		public DataSet GetDataPatchByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Data_Patch");
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
				commandHelper.FieldName = "PatchID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Data_Patch";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Data_Patch", sqlBuilder);
			return dataSet;
		}

		public DataSet GetDataPatchList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DataPatchID [DataPatch Code],DataPatchName [DataPatch Name],PhoneCode [Phone Code],Note\r\n                           FROM DataPatch ";
			FillDataSet(dataSet, "Data_Patch", textCommand);
			return dataSet;
		}

		public DataSet GetDataPatchComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DataPatchID [Code],DataPatchName [Name]\r\n                           FROM DataPatch ORDER BY DataPatchID,DataPatchName";
			FillDataSet(dataSet, "Data_Patch", textCommand);
			return dataSet;
		}
	}
}
