using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Visa : StoreObject
	{
		private const string VISAID_PARM = "@VisaID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string SPONSORID_PARM = "@SponsorID";

		private const string VISATYPE_PARM = "@VisaType";

		private const string DAYS_PARM = "@Days";

		private const string BIRTHDATE_PARM = "@BirthDate";

		private const string CONTACTID_PARM = "@ContactID";

		private const string GENDER_PARM = "@Gender";

		private const string NATIONALITY_PARM = "@Nationality";

		private const string APPLICANTNAME_PARM = "@ApplicantName";

		private const string PASSPORTNUMBER_PARM = "@PassportNumber";

		private const string PASSPORTISSUEPLACE_PARM = "@PassportIssuePlace";

		private const string PASSPORTEXPIRYDATE_PARM = "@PassportExpiryDate";

		private const string ISSUEDATE_PARM = "@IssueDate";

		private const string VALIDITYDATE_PARM = "@ValidityDate";

		private const string ISSUEPLACE_PARM = "@IssuePlace";

		private const string ARRIVALDATE_PARM = "@ArrivalDate";

		private const string EXPIRYDATE_PARM = "@ExpiryDate";

		private const string DEPARTUREDATE_PARM = "@DepartureDate";

		private const string STATUS_PARM = "@Status";

		private const string NOTE_PARM = "@Note";

		private const string VISA_TABLE = "Visa";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Visa(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Visa", new FieldValue("VisaID", "@VisaID", isUpdateConditionField: true), new FieldValue("Description", "@Description"), new FieldValue("SponsorID", "@SponsorID"), new FieldValue("VisaType", "@VisaType"), new FieldValue("Days", "@Days"), new FieldValue("BirthDate", "@BirthDate"), new FieldValue("ContactID", "@ContactID"), new FieldValue("Gender", "@Gender"), new FieldValue("Nationality", "@Nationality"), new FieldValue("ApplicantName", "@ApplicantName"), new FieldValue("PassportNumber", "@PassportNumber"), new FieldValue("PassportIssuePlace", "@PassportIssuePlace"), new FieldValue("PassportExpiryDate", "@PassportExpiryDate"), new FieldValue("IssueDate", "@IssueDate"), new FieldValue("ValidityDate", "@ValidityDate"), new FieldValue("IssuePlace", "@IssuePlace"), new FieldValue("ArrivalDate", "@ArrivalDate"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("DepartureDate", "@DepartureDate"), new FieldValue("Status", "@Status"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Visa", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@VisaID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@SponsorID", SqlDbType.NVarChar);
			parameters.Add("@VisaType", SqlDbType.TinyInt);
			parameters.Add("@Days", SqlDbType.SmallInt);
			parameters.Add("@BirthDate", SqlDbType.SmallDateTime);
			parameters.Add("@ContactID", SqlDbType.NVarChar);
			parameters.Add("@Gender", SqlDbType.Char);
			parameters.Add("@Nationality", SqlDbType.NVarChar);
			parameters.Add("@ApplicantName", SqlDbType.NVarChar);
			parameters.Add("@PassportNumber", SqlDbType.NVarChar);
			parameters.Add("@PassportIssuePlace", SqlDbType.NVarChar);
			parameters.Add("@PassportExpiryDate", SqlDbType.DateTime);
			parameters.Add("@IssueDate", SqlDbType.DateTime);
			parameters.Add("@ValidityDate", SqlDbType.DateTime);
			parameters.Add("@IssuePlace", SqlDbType.NVarChar);
			parameters.Add("@ArrivalDate", SqlDbType.DateTime);
			parameters.Add("@ExpiryDate", SqlDbType.DateTime);
			parameters.Add("@DepartureDate", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@VisaID"].SourceColumn = "VisaID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@SponsorID"].SourceColumn = "SponsorID";
			parameters["@VisaType"].SourceColumn = "VisaType";
			parameters["@Days"].SourceColumn = "Days";
			parameters["@BirthDate"].SourceColumn = "BirthDate";
			parameters["@ContactID"].SourceColumn = "ContactID";
			parameters["@Gender"].SourceColumn = "Gender";
			parameters["@Nationality"].SourceColumn = "Nationality";
			parameters["@ApplicantName"].SourceColumn = "ApplicantName";
			parameters["@PassportNumber"].SourceColumn = "PassportNumber";
			parameters["@PassportIssuePlace"].SourceColumn = "PassportIssuePlace";
			parameters["@PassportExpiryDate"].SourceColumn = "PassportExpiryDate";
			parameters["@IssueDate"].SourceColumn = "IssueDate";
			parameters["@ValidityDate"].SourceColumn = "ValidityDate";
			parameters["@IssuePlace"].SourceColumn = "IssuePlace";
			parameters["@ArrivalDate"].SourceColumn = "ArrivalDate";
			parameters["@ExpiryDate"].SourceColumn = "ExpiryDate";
			parameters["@DepartureDate"].SourceColumn = "DepartureDate";
			parameters["@Status"].SourceColumn = "Status";
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

		public bool InsertVisa(VisaData accountVisaData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountVisaData, "Visa", insertUpdateCommand);
				string text = accountVisaData.VisaTable.Rows[0]["VisaID"].ToString();
				AddActivityLog("Visa", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Visa", "VisaID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateVisa(VisaData accountVisaData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountVisaData, "Visa", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountVisaData.VisaTable.Rows[0]["VisaID"];
				UpdateTableRowByID("Visa", "VisaID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountVisaData.VisaTable.Rows[0]["Description"].ToString();
				AddActivityLog("Visa", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Visa", "VisaID", obj, sqlTransaction, isInsert: false);
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

		public VisaData GetVisa()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Visa");
			VisaData visaData = new VisaData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(visaData, "Visa", sqlBuilder);
			return visaData;
		}

		public bool DeleteVisa(string visaID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Visa WHERE VisaID = '" + visaID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Visa", visaID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public VisaData GetVisaByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "VisaID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Visa";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			VisaData visaData = new VisaData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(visaData, "Visa", sqlBuilder);
			return visaData;
		}

		public DataSet GetVisaByFields(params string[] columns)
		{
			return GetVisaByFields(null, isInactive: true, columns);
		}

		public DataSet GetVisaByFields(string[] visaID, params string[] columns)
		{
			return GetVisaByFields(visaID, isInactive: true, columns);
		}

		public DataSet GetVisaByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Visa");
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
				commandHelper.FieldName = "VisaID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Visa";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Visa", sqlBuilder);
			return dataSet;
		}

		public DataSet GetVisaList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT VisaID [Visa Number],Description,VisaType [Type],Days,ApplicantName [Applicant],IssueDate,ArrivalDate,ExpiryDate,Status\r\n                           FROM Visa ";
			FillDataSet(dataSet, "Visa", textCommand);
			return dataSet;
		}

		public DataSet GetVisaComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT VisaID [Code],Description [Name]\r\n                           FROM Visa ORDER BY VisaID,Description";
			FillDataSet(dataSet, "Visa", textCommand);
			return dataSet;
		}
	}
}
