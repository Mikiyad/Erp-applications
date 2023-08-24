using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Pivot : StoreObject
	{
		private const string PIVOTID_PARM = "@PivotID";

		private const string PIVOTNAME_PARM = "@PivotName";

		private const string GROUPID_PARM = "@GroupID";

		private const string DATAQUERY_PARM = "@DataQuery";

		private const string CHARTLAYOUT_PARM = "@ChartLayout";

		private const string HIDETOTAL_PARM = "@HideTotal";

		private const string INACTIVE_PARM = "@Inactive";

		private const string NOTE_PARM = "@Note";

		private const string PIVOT_TABLE = "Pivot_Report";

		private const string FIELDNAME_PARM = "@FieldName";

		private const string DISPLAYNAME_PARM = "@DisplayName";

		private const string DATATYPE_PARM = "@DataType";

		private const string AREA_PARM = "@Area";

		private const string AREAINDEX_PARM = "@AreaIndex";

		private const string GROUPINTERVAL_PARM = "@GroupInterval";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Pivot(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Pivot_Report", new FieldValue("PivotID", "@PivotID", isUpdateConditionField: true), new FieldValue("PivotName", "@PivotName"), new FieldValue("GroupID", "@GroupID"), new FieldValue("DataQuery", "@DataQuery"), new FieldValue("ChartLayout", "@ChartLayout"), new FieldValue("HideTotal", "@HideTotal"), new FieldValue("Inactive", "@Inactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Pivot_Report", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PivotID", SqlDbType.NVarChar);
			parameters.Add("@PivotName", SqlDbType.NVarChar);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@DataQuery", SqlDbType.NVarChar);
			parameters.Add("@ChartLayout", SqlDbType.Xml);
			parameters.Add("@HideTotal", SqlDbType.Bit);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@PivotID"].SourceColumn = "PivotID";
			parameters["@PivotName"].SourceColumn = "PivotName";
			parameters["@DataQuery"].SourceColumn = "DataQuery";
			parameters["@ChartLayout"].SourceColumn = "ChartLayout";
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@HideTotal"].SourceColumn = "HideTotal";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		private string GetInsertUpdateFieldsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Pivot_Report_Field", new FieldValue("PivotID", "@PivotID"), new FieldValue("FieldName", "@FieldName"), new FieldValue("DisplayName", "@DisplayName"), new FieldValue("DataType", "@DataType"), new FieldValue("GroupInterval", "@GroupInterval"), new FieldValue("AreaIndex", "@AreaIndex"), new FieldValue("Area", "@Area"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Pivot_Report_Field", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateFieldsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateFieldsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateFieldsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@PivotID", SqlDbType.NVarChar);
			parameters.Add("@FieldName", SqlDbType.NVarChar);
			parameters.Add("@DisplayName", SqlDbType.NVarChar);
			parameters.Add("@DataType", SqlDbType.NVarChar);
			parameters.Add("@Area", SqlDbType.TinyInt);
			parameters.Add("@AreaIndex", SqlDbType.TinyInt);
			parameters.Add("@GroupInterval", SqlDbType.Int);
			parameters["@PivotID"].SourceColumn = "PivotID";
			parameters["@FieldName"].SourceColumn = "FieldName";
			parameters["@DisplayName"].SourceColumn = "DisplayName";
			parameters["@DataType"].SourceColumn = "DataType";
			parameters["@Area"].SourceColumn = "Area";
			parameters["@AreaIndex"].SourceColumn = "AreaIndex";
			parameters["@GroupInterval"].SourceColumn = "GroupInterval";
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

		internal bool DeletePivotFieldsRows(string pivotID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Pivot_Report_Field WHERE PivotID = '" + pivotID + "' ";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Pivot", pivotID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertUpdatePivot(PivotData pivotData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(pivotData, "Pivot_Report", insertUpdateCommand) : Update(pivotData, "Pivot_Report", insertUpdateCommand));
				string text = pivotData.PivotTable.Rows[0]["PivotID"].ToString();
				if (isUpdate)
				{
					flag &= DeletePivotFieldsRows(text, sqlTransaction);
				}
				if (pivotData.PivotFieldTable.Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateFieldsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(pivotData, "Pivot_Report_Field", insertUpdateCommand);
				}
				flag &= UpdateTableRowInsertUpdateInfo("Pivot_Report", "PivotID", text, sqlTransaction, !isUpdate);
				if (!isUpdate)
				{
					AddActivityLog("Pivot", text, ActivityTypes.Add, sqlTransaction);
					return flag;
				}
				AddActivityLog("Pivot", text, ActivityTypes.Update, sqlTransaction);
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

		public PivotData GetPivot()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Pivot_Report");
			PivotData pivotData = new PivotData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(pivotData, "Pivot_Report", sqlBuilder);
			return pivotData;
		}

		public bool DeletePivot(string pivotID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Pivot_Report WHERE PivotID = '" + pivotID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Pivot", pivotID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PivotData GetPivotByID(string pivotID)
		{
			try
			{
				PivotData pivotData = new PivotData();
				string textCommand = "SELECT * FROM Pivot_Report WHERE PivotID= '" + pivotID + "' ";
				FillDataSet(pivotData, "Pivot_Report", textCommand);
				textCommand = "SELECT * FROM Pivot_Report_Field WHERE PivotID = '" + pivotID + "' ORDER BY AreaIndex";
				FillDataSet(pivotData, "Pivot_Report_Field", textCommand);
				return pivotData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPivotByFields(params string[] columns)
		{
			return GetPivotByFields(null, isInactive: true, columns);
		}

		public DataSet GetPivotByFields(string[] pivotID, params string[] columns)
		{
			return GetPivotByFields(pivotID, isInactive: true, columns);
		}

		public DataSet GetPivotByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Pivot_Report");
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
				commandHelper.FieldName = "PivotID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Pivot_Report";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Pivot_Report", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPivotList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT *\r\n                           FROM Pivot_Report ";
			FillDataSet(dataSet, "Pivot_Report", textCommand);
			return dataSet;
		}

		public DataSet GetPivotComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PivotID [Code],PivotName [Name],PG.GroupName AS Category\r\n                           FROM Pivot_Report PR INNER JOIN Pivot_Group PG ON PR.GroupID = PG.GroupID ORDER BY PivotID,PivotName";
			FillDataSet(dataSet, "Pivot_Report", textCommand);
			return dataSet;
		}
	}
}
