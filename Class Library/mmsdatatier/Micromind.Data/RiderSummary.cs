using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class RiderSummary : StoreObject
	{
		public const string RIDERSUMMARY_TABLE = "Rider_Summary";

		private const string RIDERSUMMARYID_PARM = "@RiderID";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string RIDERSUMMARYNAME_PARM = "@RiderName";

		private const string TYPE_PARM = "@TYPE";

		private const string LICENSENUMBER_PARM = "@LicenseNumber";

		private const string FEIREGNUMBER_PARM = "@FEIRegisterNumber";

		private const string FATHERSNAME_PARM = "@FathersName";

		private const string FAMILYNAME_PARM = "@FamilyName";

		private const string NATIONALITY_PARM = "@Nationality";

		private const string DATEOFBIRTH_PARM = "@DateofBirth";

		private const string GENDER_PARM = "@Gender";

		private const string CONTACTNUMBER_PARM = "@ContactNumber";

		private const string EMAIL_PARM = "@EMail";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public RiderSummary(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Rider_Summary", new FieldValue("RiderID", "@RiderID", isUpdateConditionField: true), new FieldValue("RiderName", "@RiderName"), new FieldValue("LicenseNumber", "@LicenseNumber"), new FieldValue("Type", "@TYPE"), new FieldValue("FEIRegisterNumber", "@FEIRegisterNumber"), new FieldValue("FathersName", "@FathersName"), new FieldValue("FamilyName", "@FamilyName"), new FieldValue("Nationality", "@Nationality"), new FieldValue("DateofBirth", "@DateofBirth"), new FieldValue("Gender", "@Gender"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("ContactNumber", "@ContactNumber"), new FieldValue("EMail", "@EMail"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Rider_Summary", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@RiderID", SqlDbType.NVarChar);
			parameters.Add("@RiderName", SqlDbType.NVarChar);
			parameters.Add("@TYPE", SqlDbType.NVarChar);
			parameters.Add("@LicenseNumber", SqlDbType.NVarChar);
			parameters.Add("@FEIRegisterNumber", SqlDbType.NVarChar);
			parameters.Add("@FathersName", SqlDbType.NVarChar);
			parameters.Add("@FamilyName", SqlDbType.NVarChar);
			parameters.Add("@Nationality", SqlDbType.NVarChar);
			parameters.Add("@DateofBirth", SqlDbType.DateTime);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Gender", SqlDbType.NVarChar);
			parameters.Add("@ContactNumber", SqlDbType.NVarChar);
			parameters.Add("@EMail", SqlDbType.NVarChar);
			parameters["@RiderID"].SourceColumn = "RiderID";
			parameters["@RiderName"].SourceColumn = "RiderName";
			parameters["@TYPE"].SourceColumn = "Type";
			parameters["@LicenseNumber"].SourceColumn = "LicenseNumber";
			parameters["@FEIRegisterNumber"].SourceColumn = "FEIRegisterNumber";
			parameters["@FathersName"].SourceColumn = "FathersName";
			parameters["@FamilyName"].SourceColumn = "FamilyName";
			parameters["@Nationality"].SourceColumn = "Nationality";
			parameters["@DateofBirth"].SourceColumn = "DateofBirth";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@Gender"].SourceColumn = "Gender";
			parameters["@ContactNumber"].SourceColumn = "ContactNumber";
			parameters["@EMail"].SourceColumn = "EMail";
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

		public bool InsertRiderSummary(RiderSummaryData accountJobTaskData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountJobTaskData, "Rider_Summary", insertUpdateCommand);
				string text = accountJobTaskData.RiderSummaryTable.Rows[0]["RiderID"].ToString();
				AddActivityLog("Rider/Trainer Summary", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Rider_Summary", "RiderID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateRiderSummary(RiderSummaryData accountJobTaskData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountJobTaskData, "Rider_Summary", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountJobTaskData.RiderSummaryTable.Rows[0]["RiderID"];
				UpdateTableRowByID("Rider_Summary", "RiderID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountJobTaskData.RiderSummaryTable.Rows[0]["RiderName"].ToString();
				AddActivityLog("Rider/Trainer Summary", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Rider_Summary", "RiderID", obj, sqlTransaction, isInsert: false);
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

		public RiderSummaryData GetRiderSummary()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Rider_Summary");
			RiderSummaryData riderSummaryData = new RiderSummaryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(riderSummaryData, "Rider_Summary", sqlBuilder);
			return riderSummaryData;
		}

		public bool DeleteRiderSummary(string jobTaskID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Rider_Summary WHERE RiderID = '" + jobTaskID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Client Asset", jobTaskID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public RiderSummaryData GetRiderSummaryByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "RiderID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Rider_Summary";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			RiderSummaryData riderSummaryData = new RiderSummaryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(riderSummaryData, "Rider_Summary", sqlBuilder);
			return riderSummaryData;
		}

		public DataSet GetRiderSummaryList(bool isInactive)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "Select RiderID [RiderSummary ID],RiderName  FROM Rider_Summary WHERE ISNULL(IsInactive,'False')='False' ";
			FillDataSet(dataSet, "Rider_Summary", textCommand);
			return dataSet;
		}

		public DataSet GetRiderSummaryComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT RiderID [Code],RiderName [Name]\r\n                           FROM Rider_Summary ORDER BY Code,Name";
			FillDataSet(dataSet, "Rider_Summary", textCommand);
			return dataSet;
		}
	}
}
