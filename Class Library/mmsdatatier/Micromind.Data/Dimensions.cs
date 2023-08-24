using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Dimensions : StoreObject
	{
		private const string DIMENSIONID_PARM = "@DimensionID";

		private const string DIMENSIONNAME_PARM = "@DimensionName";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string DIMENSION_TABLE = "Product_Dimension";

		private const string ATTRIBUTEID_PARM = "@AttributeID";

		private const string ATTRIBUTEIDNAME_PARM = "@AttributeName";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Dimensions(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Dimension", new FieldValue("DimensionID", "@DimensionID", isUpdateConditionField: true), new FieldValue("DimensionName", "@DimensionName"), new FieldValue("IsInactive ", "@IsInactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Dimension", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@DimensionID", SqlDbType.NVarChar);
			parameters.Add("@DimensionName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@DimensionID"].SourceColumn = "DimensionID";
			parameters["@DimensionName"].SourceColumn = "DimensionName";
			parameters["@IsInactive"].SourceColumn = "IsInactive ";
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

		private string GetInsertUpdateAttributeText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Dimension_Attribute", new FieldValue("DimensionID", "@DimensionID", isUpdateConditionField: true), new FieldValue("AttributeID", "@AttributeID", isUpdateConditionField: true), new FieldValue("AttributeName", "@AttributeName"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("IsInactive ", "@IsInactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Dimension_Attribute", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateAttributeCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateAttributeText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateAttributeText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@AttributeID", SqlDbType.NVarChar);
			parameters.Add("@DimensionID", SqlDbType.NVarChar);
			parameters.Add("@AttributeName", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@AttributeID"].SourceColumn = "AttributeID";
			parameters["@DimensionID"].SourceColumn = "DimensionID";
			parameters["@AttributeName"].SourceColumn = "AttributeName";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@IsInactive"].SourceColumn = "IsInactive ";
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

		private bool DeleteAttributeRows(string dimensionID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Dimension_Attribute WHERE DimensionID = '" + dimensionID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Dimension Attribute", dimensionID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertUpdateDimension(DimensionData accountDimensionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = (isUpdate ? (flag & Update(accountDimensionData, "Dimension", insertUpdateCommand)) : (flag & Insert(accountDimensionData, "Dimension", insertUpdateCommand)));
				string text = accountDimensionData.DimensionTable.Rows[0]["DimensionID"].ToString();
				AddActivityLog(" Product Dimension", text, ActivityTypes.Add, sqlTransaction);
				flag &= DeleteAttributeRows(text, sqlTransaction);
				if (accountDimensionData.DimensionAttributeTable.Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateAttributeCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountDimensionData, "Dimension_Attribute", insertUpdateCommand);
				}
				UpdateTableRowInsertUpdateInfo("Dimension", "DimensionID", text, sqlTransaction, !isUpdate);
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

		public DimensionData GetDimension()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Dimension");
			DimensionData dimensionData = new DimensionData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(dimensionData, "Dimension", sqlBuilder);
			return dimensionData;
		}

		public bool DeleteDimension(string productDimensionID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Dimension WHERE DimensionID = '" + productDimensionID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Dimension", productDimensionID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DimensionData GetDimensionByID(string id)
		{
			try
			{
				DimensionData dimensionData = new DimensionData();
				string text = "SELECT * FROM Dimension WHERE DimensionID = '" + id + "'";
				new SqlCommand(text);
				FillDataSet(dimensionData, "Dimension", text);
				if (dimensionData == null || dimensionData.Tables.Count == 0 || dimensionData.Tables["Dimension"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT * \r\n                        FROM Dimension_Attribute DA INNER JOIN Dimension D ON DA.DimensionID=D.DimensionID\r\n                        WHERE DA.DimensionID = '" + id + "' ORDER BY RowIndex";
				FillDataSet(dimensionData, "Dimension_Attribute", text);
				return dimensionData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDimensionByFields(params string[] columns)
		{
			return GetDimensionByFields(null, isInactive: true, columns);
		}

		public DataSet GetDimensionByFields(string[] productDimensionID, params string[] columns)
		{
			return GetDimensionByFields(productDimensionID, isInactive: true, columns);
		}

		public DataSet GetDimensionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Dimension");
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
				commandHelper.FieldName = "DimensionID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Dimension";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Dimension", sqlBuilder);
			return dataSet;
		}

		public DataSet GetDimensionList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DimensionID [Dimension Code],DimensionName [Dimension Name],Note, IsInactive [Inactive]\r\n                           FROM Product_Dimension ";
			FillDataSet(dataSet, "Dimension", textCommand);
			return dataSet;
		}

		public DataSet GetDimensionAttributes(string dimensionID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AttributeID,AttributeName  \r\n                                FROM DIMENSION_ATTRIBUTE WHERE DimensionID = '" + dimensionID + "' ORDER BY RowIndex";
			FillDataSet(dataSet, "Dimension", textCommand);
			return dataSet;
		}

		public DataSet GetDimensionComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DimensionID [Code],DimensionName [Name]\r\n                           FROM Dimension ORDER BY DimensionID,DimensionName";
			FillDataSet(dataSet, "Dimension", textCommand);
			return dataSet;
		}
	}
}
