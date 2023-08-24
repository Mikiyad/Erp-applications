using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class InsuranceProvider : StoreObject
	{
		private const string INSURANCEPROVIDERID_PARM = "@InsuranceProviderID";

		private const string INSURANCEPROVIDERNAME_PARM = "@InsuranceProviderName";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string ISMEDICAL_PARM = "@IsMedical";

		public const string DESCRIPTION_PARM = "@Description";

		public const string INSURANCEPROVIDER_TABLE = "Insurance_Provider";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public InsuranceProvider(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Insurance_Provider", new FieldValue("InsuranceProviderID", "@InsuranceProviderID", isUpdateConditionField: true), new FieldValue("InsuranceProviderName", "@InsuranceProviderName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("IsMedical", "@IsMedical"), new FieldValue("Description", "@Description"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Insurance_Provider", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@InsuranceProviderID", SqlDbType.NVarChar);
			parameters.Add("@InsuranceProviderName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@IsMedical", SqlDbType.Bit);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters["@InsuranceProviderID"].SourceColumn = "InsuranceProviderID";
			parameters["@InsuranceProviderName"].SourceColumn = "InsuranceProviderName";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@IsMedical"].SourceColumn = "IsMedical";
			parameters["@Description"].SourceColumn = "Description";
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

		public bool InsertInsuranceProvider(InsuranceProviderData accountInsuranceProviderData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountInsuranceProviderData, "Insurance_Provider", insertUpdateCommand);
				string text = accountInsuranceProviderData.InsuranceProviderTable.Rows[0]["InsuranceProviderID"].ToString();
				AddActivityLog("Insurance Provider", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Insurance_Provider", "InsuranceProviderID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateInsuranceProvider(InsuranceProviderData accountInsuranceProviderData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountInsuranceProviderData, "Insurance_Provider", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountInsuranceProviderData.InsuranceProviderTable.Rows[0]["InsuranceProviderID"];
				UpdateTableRowByID("Insurance_Provider", "InsuranceProviderID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountInsuranceProviderData.InsuranceProviderTable.Rows[0]["InsuranceProviderID"].ToString();
				AddActivityLog("Insurance Provider", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Insurance_Provider", "InsuranceProviderID", obj, sqlTransaction, isInsert: false);
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

		public InsuranceProviderData GetInsuranceProvider()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Insurance_Provider");
			InsuranceProviderData insuranceProviderData = new InsuranceProviderData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(insuranceProviderData, "Insurance_Provider", sqlBuilder);
			return insuranceProviderData;
		}

		public bool DeleteInsuranceProvider(string InsuranceProviderID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Insurance_Provider WHERE InsuranceProviderID = '" + InsuranceProviderID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Insurance Provider", InsuranceProviderID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public InsuranceProviderData GetInsuranceProviderByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "InsuranceProviderID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Insurance_Provider";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			InsuranceProviderData insuranceProviderData = new InsuranceProviderData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(insuranceProviderData, "Insurance_Provider", sqlBuilder);
			return insuranceProviderData;
		}

		public DataSet GetInsuranceProviderByFields(params string[] columns)
		{
			return GetInsuranceProviderByFields(null, isInactive: true, columns);
		}

		public DataSet GetInsuranceProviderByFields(string[] InsuranceProviderID, params string[] columns)
		{
			return GetInsuranceProviderByFields(InsuranceProviderID, isInactive: true, columns);
		}

		public DataSet GetInsuranceProviderByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Insurance_Provider");
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
				commandHelper.FieldName = "InsuranceProviderID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Insurance_Provider";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Insurance_Provider", sqlBuilder);
			return dataSet;
		}

		public DataSet GetInsuranceProviderList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT InsuranceProviderID [InsuranceProvider Code],InsuranceProviderName [InsuranceProvider Name],Description,IsInactive [Inactive]\r\n                           FROM Insurance_Provider ";
			FillDataSet(dataSet, "Insurance_Provider", textCommand);
			return dataSet;
		}

		public DataSet GetInsuranceProviderComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT InsuranceProviderID [Code],InsuranceProviderName [Name]\r\n                           FROM Insurance_Provider where ISINACTIVE<>1 and ISNULL(IsMedical,'False')=0  ORDER BY InsuranceProviderID,InsuranceProviderName";
			FillDataSet(dataSet, "Insurance_Provider", textCommand);
			return dataSet;
		}

		public DataSet GetMedicalInsuranceProviderComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT InsuranceProviderID [Code],InsuranceProviderName [Name]\r\n                           FROM Insurance_Provider where ISINACTIVE<>1 and ISNULL(IsMedical,'False')=1  ORDER BY InsuranceProviderID,InsuranceProviderName";
			FillDataSet(dataSet, "Insurance_Provider", textCommand);
			return dataSet;
		}
	}
}
