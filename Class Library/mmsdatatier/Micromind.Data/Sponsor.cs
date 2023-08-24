using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Sponsor : StoreObject
	{
		private const string SPONSORID_PARM = "@SponsorID";

		private const string SPONSORNAME_PARM = "@SponsorName";

		private const string INACTIVE_PARM = "@Inactive";

		private const string MOLID_PARM = "@MOLId";

		public const string NOTE_PARM = "@Note";

		public const string SPONSOR_TABLE = "Sponsor";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Sponsor(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sponsor", new FieldValue("SponsorID", "@SponsorID", isUpdateConditionField: true), new FieldValue("SponsorName", "@SponsorName"), new FieldValue("MOLId", "@MOLId"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Sponsor", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@SponsorID", SqlDbType.NVarChar);
			parameters.Add("@SponsorName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@MOLId", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SponsorID"].SourceColumn = "SponsorID";
			parameters["@SponsorName"].SourceColumn = "SponsorName";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@MOLId"].SourceColumn = "MOLId";
			parameters["@Note"].SourceColumn = "Note";
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

		public bool InsertSponsor(SponsorData accountSponsorData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountSponsorData, "Sponsor", insertUpdateCommand);
				string text = accountSponsorData.SponsorTable.Rows[0]["SponsorID"].ToString();
				AddActivityLog("Sponsor", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Sponsor", "SponsorID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateSponsor(SponsorData accountSponsorData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountSponsorData, "Sponsor", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountSponsorData.SponsorTable.Rows[0]["SponsorID"];
				UpdateTableRowByID("Sponsor", "SponsorID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountSponsorData.SponsorTable.Rows[0]["SponsorName"].ToString();
				AddActivityLog("Sponsor", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Sponsor", "SponsorID", obj, sqlTransaction, isInsert: false);
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

		public SponsorData GetSponsor()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Sponsor");
			SponsorData sponsorData = new SponsorData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(sponsorData, "Sponsor", sqlBuilder);
			return sponsorData;
		}

		public bool DeleteSponsor(string sponsorID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Sponsor WHERE SponsorID = '" + sponsorID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Sponsor", sponsorID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public SponsorData GetSponsorByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "SponsorID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Sponsor";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			SponsorData sponsorData = new SponsorData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(sponsorData, "Sponsor", sqlBuilder);
			return sponsorData;
		}

		public DataSet GetSponsorByFields(params string[] columns)
		{
			return GetSponsorByFields(null, isInactive: true, columns);
		}

		public DataSet GetSponsorByFields(string[] sponsorID, params string[] columns)
		{
			return GetSponsorByFields(sponsorID, isInactive: true, columns);
		}

		public DataSet GetSponsorByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Sponsor");
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
				commandHelper.FieldName = "SponsorID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Sponsor";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Sponsor", sqlBuilder);
			return dataSet;
		}

		public DataSet GetSponsorList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SponsorID [Sponsor Code],SponsorName [Sponsor Name],Note,Inactive  \r\n                           FROM Sponsor ";
			FillDataSet(dataSet, "Sponsor", textCommand);
			return dataSet;
		}

		public DataSet GetSponsorComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SponsorID [Code],SponsorName [Name]\r\n                           FROM Sponsor ORDER BY SponsorID,SponsorName";
			FillDataSet(dataSet, "Sponsor", textCommand);
			return dataSet;
		}
	}
}
