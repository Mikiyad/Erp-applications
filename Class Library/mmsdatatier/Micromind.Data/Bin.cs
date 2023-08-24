using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Bin : StoreObject
	{
		private const string BIN_TABLE = "Bin";

		private const string BINID_PARM = "@BinID";

		private const string ISINACTIVE_PARM = "@Inactive";

		private const string BINNAME_PARM = "@BinName";

		private const string REMARKS_PARM = "@Remarks";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Bin(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bin", new FieldValue("BinID", "@BinID", isUpdateConditionField: true), new FieldValue("BinName", "@BinName"), new FieldValue("LocationID", "@LocationID"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Bin", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@BinID", SqlDbType.NVarChar);
			parameters.Add("@BinName", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@BinID"].SourceColumn = "BinID";
			parameters["@BinName"].SourceColumn = "BinName";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@Remarks"].SourceColumn = "Remarks";
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

		public bool InsertBin(BinData binData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(binData, "Bin", insertUpdateCommand);
				string text = binData.BinTable.Rows[0]["BinID"].ToString();
				AddActivityLog("Bin", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Bin", "BinID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateBin(BinData binData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(binData, "Bin", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = binData.BinTable.Rows[0]["BinID"];
				UpdateTableRowByID("Bin", "BinID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = binData.BinTable.Rows[0]["BinName"].ToString();
				AddActivityLog("Bin", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Bin", "BinID", obj, sqlTransaction, isInsert: false);
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

		public BinData GetBin()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Bin");
			BinData binData = new BinData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(binData, "Bin", sqlBuilder);
			return binData;
		}

		public bool DeleteBin(string binID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Bin WHERE BinID = '" + binID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Bin", binID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public BinData GetBinByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "BinID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Bin";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			BinData binData = new BinData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(binData, "Bin", sqlBuilder);
			return binData;
		}

		public DataSet GetJobTaskByFields(params string[] columns)
		{
			return GetJobTaskByFields(null, isInactive: true, columns);
		}

		public DataSet GetJobTaskByFields(string[] binID, params string[] columns)
		{
			return GetJobTaskByFields(binID, isInactive: true, columns);
		}

		public DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Bin");
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
				commandHelper.FieldName = "BinID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Bin";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Bin", sqlBuilder);
			return dataSet;
		}

		public DataSet GetBinList(bool isInactive)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "Select BinID [Bin ID],BinName  FROM Bin WHERE ISNULL(Inactive,'False')='False' ";
			FillDataSet(dataSet, "Bin", textCommand);
			return dataSet;
		}

		public DataSet GetBinComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT BinID [Code],BinName [Name]\r\n                           FROM Bin ORDER BY BinID,BinName";
			FillDataSet(dataSet, "Bin", textCommand);
			return dataSet;
		}
	}
}
