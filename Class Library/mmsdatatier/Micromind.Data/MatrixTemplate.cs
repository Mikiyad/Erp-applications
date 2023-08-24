using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class MatrixTemplate : StoreObject
	{
		private const string MATRIXTEMPLATEID_PARM = "@TemplateID";

		private const string MATRIXTEMPLATENAME_PARM = "@TemplateName";

		private const string NOTE_PARM = "@Note";

		private const string INACTIVE_PARM = "@Inactive";

		private const string DIMENSION_PARM = "@Dimension";

		private const string DIMENSIONID_PARM = "@DimensionID";

		private const string ATTRIBUTEID_PARM = "@AttributeID";

		private const string ATTRIBUTENAME_PARM = "@AttributeName";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public MatrixTemplate(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Matrix_Template", new FieldValue("TemplateID", "@TemplateID", isUpdateConditionField: true), new FieldValue("TemplateName", "@TemplateName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Matrix_Template", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TemplateID", SqlDbType.NVarChar);
			parameters.Add("@TemplateName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@TemplateID"].SourceColumn = "TemplateID";
			parameters["@TemplateName"].SourceColumn = "TemplateName";
			parameters["@Note"].SourceColumn = "Note";
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

		private string GetInsertUpdateMatrixAttributeDimensionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Matrix_Template_Detail", new FieldValue("TemplateID", "@TemplateID", isUpdateConditionField: true), new FieldValue("Dimension", "@Dimension"), new FieldValue("DimensionID", "@DimensionID"), new FieldValue("AttributeID", "@AttributeID"), new FieldValue("AttributeName", "@AttributeName"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateMatrixAttributeDimensionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateMatrixAttributeDimensionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateMatrixAttributeDimensionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@TemplateID", SqlDbType.NVarChar);
			parameters.Add("@Dimension", SqlDbType.NVarChar);
			parameters.Add("@DimensionID", SqlDbType.NVarChar);
			parameters.Add("@AttributeID", SqlDbType.NVarChar);
			parameters.Add("@AttributeName", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@TemplateID"].SourceColumn = "TemplateID";
			parameters["@Dimension"].SourceColumn = "Dimension";
			parameters["@DimensionID"].SourceColumn = "DimensionID";
			parameters["@AttributeID"].SourceColumn = "AttributeID";
			parameters["@AttributeName"].SourceColumn = "AttributeName";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateMatrixTemplate(MatrixTemplateData matrixTemplateData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(matrixTemplateData, "Matrix_Template", insertUpdateCommand) : Update(matrixTemplateData, "Matrix_Template", insertUpdateCommand));
				string text = matrixTemplateData.MatrixTemplateTable.Rows[0]["TemplateID"].ToString();
				flag &= DeleteMatrixTemplateDetail(text, sqlTransaction);
				if (matrixTemplateData.MatrixTemplateDetailTable.Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateMatrixAttributeDimensionCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(matrixTemplateData, "Matrix_Template_Detail", insertUpdateCommand);
				}
				AddActivityLog("Matrix Template", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Matrix_Template", "TemplateID", text, sqlTransaction, !isUpdate);
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

		private bool DeleteMatrixTemplateDetail(string templateID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Matrix_Template_Detail WHERE TemplateID = '" + templateID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Matrix Template", templateID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public MatrixTemplateData GetMatrixTemplate()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Matrix_Template");
			MatrixTemplateData matrixTemplateData = new MatrixTemplateData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(matrixTemplateData, "Matrix_Template", sqlBuilder);
			return matrixTemplateData;
		}

		public bool DeleteMatrixTemplate(string matrixTemplateID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = null;
				sqlTransaction = base.DBConfig.StartNewTransaction();
				flag = DeleteMatrixTemplateDetail(matrixTemplateID, sqlTransaction);
				string commandText = "DELETE FROM Matrix_Template WHERE TemplateID = '" + matrixTemplateID + "'";
				flag &= Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Matrix Template", matrixTemplateID, ActivityTypes.Delete, null);
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

		public MatrixTemplateData GetMatrixTemplateByID(string id)
		{
			MatrixTemplateData matrixTemplateData = new MatrixTemplateData();
			string textCommand = "SELECT * \r\n                           FROM Matrix_Template WHere TemplateID = '" + id + "'";
			FillDataSet(matrixTemplateData, "Matrix_Template", textCommand);
			textCommand = "SELECT MAD.*\r\n                        FROM Matrix_Template_Detail MAD\r\n                        INNER JOIN Matrix_Template PP ON PP.TemplateID= MAD.TemplateID\r\n                        WHERE MAD.TemplateID = '" + id + "'\r\n                        ORDER BY Dimension,RowIndex";
			FillDataSet(matrixTemplateData, "Matrix_Template_Detail", textCommand);
			return matrixTemplateData;
		}

		public DataSet GetMatrixTemplateByFields(params string[] columns)
		{
			return GetMatrixTemplateByFields(null, isInactive: true, columns);
		}

		public DataSet GetMatrixTemplateByFields(string[] matrixTemplateID, params string[] columns)
		{
			return GetMatrixTemplateByFields(matrixTemplateID, isInactive: true, columns);
		}

		public DataSet GetMatrixTemplateByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Matrix_Template");
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
				commandHelper.FieldName = "TemplateID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Matrix_Template";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Matrix_Template", sqlBuilder);
			return dataSet;
		}

		public DataSet GetMatrixTemplateList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TemplateID [Template Code],TemplateName [Template Name],Note,Inactive\r\n                           FROM Matrix_Template ";
			FillDataSet(dataSet, "Matrix_Template", textCommand);
			return dataSet;
		}

		public DataSet GetMatrixTemplateComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TemplateID [Code],TemplateName [Name]\r\n                           FROM Matrix_Template ORDER BY TemplateID,TemplateName";
			FillDataSet(dataSet, "Matrix_Template", textCommand);
			return dataSet;
		}
	}
}
